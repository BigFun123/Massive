using Massive;
using Massive.Events;
using Massive.Network;
using Massive.Platform;
using Massive.Tools;
using OpenTK;
using OpenWorld.Handlers;
using System;

namespace OpenWorld.Controllers
{
  public class COpenWorld
  {
    MSpawnHandler _spawnHandler;
    MDeleteHandler _deleteHandler;
    MTeleportHandler _teleportHandler;
    MTextureHandler _textureHandler;
    MCameraHandler _cameraHandler;
    MLightHandler _lightHandler;
    MBuildParts _buildParts;
    UserDetails _userDetails;
    MPropertyChangeHandler _propertyHandler;
    MMoveHandler _moveHandler;
    MNavigationPointer _navPointer;
    BookmarkController _bookmarkController;
    MAvatarHandler _avatarHandler;

    MStateMachine _state;

    public COpenWorld()
    {
      Settings.TerrainPhysics = true;
      Settings.DrawTrees = true;
      Settings.DrawTerrains = true;
      Settings.DrawBackdrop = true;
      Settings.DrawPlanets = true;

      Settings.DebugDepth = false;

      Globals.Network.ConnectedToMASSIVEHandler += Network_ConnectedToMASSIVEHandler;
      Globals.Network.ConnectedToServerHandler += Network_ConnectedToServerHandler;
      //Globals.Network.LoggedInHandler += Network_LoggedInHandler;
      MMessageBus.LoggedIn += MMessageBus_LoggedIn;

      _state = new MStateMachine(Globals.GUIThreadOwner);
      MStateMachine.StateChanged += MStateMachine_StateChanged;
      MStateMachine.ChangeState(MStateMachine.eStates.Splash);
    }

    private void MStateMachine_StateChanged(object sender, StateEvent e)
    {

    }

    public void Setup()
    {
      Globals.VERSION = MVersion.VERSION;

      MFileSystem.SetProjectPath(@".\");
      Globals._scene = new MScene(true);
      _spawnHandler = new MSpawnHandler();
      _deleteHandler = new MDeleteHandler();
      
      _teleportHandler = new MTeleportHandler();
      _textureHandler = new MTextureHandler();
      _lightHandler = new MLightHandler();
      _propertyHandler = new MPropertyChangeHandler();
      _navPointer = new MNavigationPointer();
      _bookmarkController = new BookmarkController();
      _moveHandler = new MMoveHandler();
      _avatarHandler = new MAvatarHandler();



      Globals._scene.SetupInitialObjects();
      _cameraHandler = new MCameraHandler();
      _buildParts = new MBuildParts();
      _buildParts.Setup();

      _userDetails = new UserDetails();
      _userDetails.Setup();
      ParseCmdLine();

      //TODO: get from server     
      

      Globals._scene.Setup();
      _navPointer.Setup();

      Globals._scene.Play();
      //Settings.DebugNetwork = true;
      CreateAvatar();
    }

    void CreateAvatar()
    {
      Globals.UserAccount.HomePosition = MassiveTools.ArrayFromVector(new Vector3d(12717655836.0836, 146353256922.555, -7581841295.85195));
      Globals.UserAccount.CurrentPosition = Globals.UserAccount.HomePosition;
      MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
    }

    void ParseCmdLine()
    {
      string[] args = Environment.GetCommandLineArgs();

      if (args.Length > 1)
      {
        Globals.UserAccount.UserID = args[1];
      }
    }

    //1 client connects to MASSIVE server and receives this callback
    private void Network_ConnectedToMASSIVEHandler(object sender, Massive.Events.StatusEvent e)
    {
      Console.WriteLine("Connected To Universe");
      Globals.Network.SendLoginRequest();
    }

    //2 user logs in, gets a userID, ends up here
    private void MMessageBus_LoggedIn(object sender, ChangeDetailsEvent e)
    {
      ParseCmdLine();

      Globals.Network.GetWorld();

      //TODO: Use Server-side home position
      Vector3d Home = MassiveTools.VectorFromArray(Globals.UserAccount.HomePosition);
      //Globals.UserAccount.HomePosition = MassiveTools.ArrayFromVector(Home);

      Globals.Network.SpawnRequest(Globals.UserAccount.AvatarID, MTexture.DEFAULT_TEXTURE, Globals.UserAccount.UserID, "TAG",
        Home, Quaterniond.Identity, Globals.UserAccount.UserID, 0, 10);

      Globals.Network.ChangeAvatarRequest(Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      //12717655889.4, 146353256822.3, -7581841339.4(18.4096672612293, -33.9328163657347, 0)     
      Globals.Network.TeleportRequest(Globals.UserAccount.UserID, Home,
        Quaterniond.Identity);
    }

    //1 client connects to server and receives this callback
    private void Network_ConnectedToServerHandler(object sender, Massive.Events.StatusEvent e)
    {
      Console.WriteLine("Connected to Server");
    }

    void CheckSetupStarted()
    {
      if (Globals.Tasks > 0)
      {
        MStateMachine.ChangeState(MStateMachine.eStates.Setup);
      }
    }


    void CheckSetupComplete()
    {
      if (Globals.Tasks <= 0)
      {
        MStateMachine.ChangeState(MStateMachine.eStates.Viewing);
        MPlanetHandler.GetUpAt(MassiveTools.VectorFromArray((Globals.UserAccount.CurrentPosition)));
        MMessageBus.GravityStateChanged(this, new BooleanEvent(true));
        CreateAvatar();
      }
    }
    public void Update()
    {
      if (MStateMachine.CurrentState == MStateMachine.eStates.Splash)
      {
        MMessageBus.GravityStateChanged(this, new BooleanEvent(false));
        CheckSetupStarted();
      }
      
      if (MStateMachine.CurrentState == MStateMachine.eStates.Setup)
      {
        CheckSetupComplete();
      }      

      Globals._scene.Update();
      _lightHandler.Update();
      _cameraHandler.Update();
    }

    public void Render()
    {
      Globals._scene.ClearBackBuffer();
      Globals._scene.Render();
    }

    public void Dispose()
    {
      Globals.ApplicationExiting = true;
      MScene.Physics.DisposeWorld();
      Globals._scene.Dispose();
    }
  }
}
