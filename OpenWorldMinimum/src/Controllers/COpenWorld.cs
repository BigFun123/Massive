using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.controllers
{
  public class COpenWorld
  {
    MScene _scene;

    public COpenWorld()
    {
      Globals.Network.ConnectedToLobbyHandler += Network_ConnectedToLobbyHandler;
      Globals.Network.ConnectedToMASSIVEHandler += Network_ConnectedToMASSIVEHandler;
      Globals.Network.ConnectedToServerHandler += Network_ConnectedToServerHandler;
    }

    //1 client connects to server and receives this callback
    private void Network_ConnectedToServerHandler(object sender, Massive.Events.StatusEvent e)
    {
      Console.WriteLine("Connected to Server");
    }

    //2 client connects to lobby and receives this callback
    private void Network_ConnectedToLobbyHandler(object sender, Massive.Events.StatusEvent e)
    {
      Console.WriteLine("Connected To Lobby");
      Globals.Network.SendConnectToMASSIVERequest();
    }

    //1 client connects to Zone Controller and receives this callback
    private void Network_ConnectedToMASSIVEHandler(object sender, Massive.Events.StatusEvent e)
    {
      Console.WriteLine("Connected To Universe");
    }

    

    public void Setup()
    {
      Globals.VERSION = MVersion.VERSION;

      Globals.SetProjectPath(@".\");

      _scene = new MScene(false, true);
      
      _scene.SetupInitialObjects();
      _scene.Play();
    }

    public void Update()
    {
      _scene.Update();
    }
  }
}
