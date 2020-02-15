using Massive;
using Massive.Events;
using Massive.Network;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Massive.MKey;

namespace Massive.Events
{
  public class MMessageBus
  {

    public static event EventHandler<GraphChangedEvent> GraphChangeHandler;
    public static event EventHandler<ChangeDetailsEvent> LoggedIn;
    public static event EventHandler<ErrorEvent> ErrorHandler;
    public static event EventHandler<ChangeDetailsEvent> UserRegistered;
    public static event EventHandler<ChangeDetailsEvent> UserDetailsChanged;

    public static event EventHandler<MoveEvent> AvatarMovedEvent;
    public static event EventHandler<InfoEvent> UpdateRequiredHandler;
    public static event EventHandler<InfoEvent> AvatarSetupHandler;
    public static event EventHandler<TextEvent> AvatarAnimationChangeRequest;
    public static event EventHandler<TextEvent> AvatarAnimationChanged;

    public static event EventHandler<CreateObjectRequestEvent> CreateObjectRequestHandler;
    public static event EventHandler<CreateEvent> ObjectCreatedHandler;
    public static event EventHandler<DeleteEvent> ObjectDeletedEvent;

    public static event EventHandler<MoveEvent> MoveRequestEventHandler;
    public static event EventHandler<MoveEvent> MoveAvatarRequestEventHandler;
    public static event EventHandler<MoveEvent> ObjectMovedEvent;

    public static event EventHandler<TeleportRequestHandler> TeleportRequestHandler;
    public static event EventHandler<MoveEvent> TeleportedEventHandler;
    public static event EventHandler<MoveEvent> TeleportCompleteHandler;

    public static event EventHandler<ChangePropertyEvent> PropertyChangeEvent;
    public static event EventHandler<RotationRequestEvent> RotateEventHandler;

    public static event EventHandler<InfoEvent> InfoEventHandler;
    //something is loading that may affect UI
    public static event EventHandler<InfoEvent> LoadingStatusHandler;
    public static event EventHandler<InfoEvent> ChangeUserIDHandler;
    public static event EventHandler<InfoEvent> ChangedUserInfoHandler;
    public static event EventHandler<ChangeAvatarEvent> ChangeAvatarRequestHandler;
    public static event EventHandler<ChangeAvatarEvent> AvatarChangedHandler;
    public static event EventHandler<MouseClickEvent> ClickEventHandler;
    public static event EventHandler<SelectEvent> SelectEventHandler;
    public static event EventHandler<FocusEvent> FocusEventHandler;


    public static event EventHandler<DeleteRequestEvent> DeleteObjectRequestHandler;
    public static event EventHandler<TextureRequestEvent> TextureRequestHandler;
    public static event EventHandler<TextureRequestEvent> TextureChangedHandler;
    //public static event EventHandler<UpdateEvent> UpdateHandler;
    public static event EventHandler<KeyboardEvent> KeyboardHandler;

    public static event EventHandler<ChangeModeEvent> ChangeModeHandler;
    public static event EventHandler<ChangeModeEvent> ChangeGravityHandler;

    public static event EventHandler<ZoneEvent> ZoneAddHandler;
    public static event EventHandler<ZoneEvent> ZoneDeleteHandler;
    public static event EventHandler<ZoneEvent> ZoneSelectHandler;

    public static event EventHandler<BookmarkEvent> BookmardAddRequestEvent;
    public static event EventHandler<BookmarkEvent> BookmarkAddedEvent;

    public static event EventHandler<NavigationEvent> NavigationHandler;
    public static event EventHandler<NavigationEvent> NavigationDisableHandler;
    public static event EventHandler<NavigationEvent> NavigationEnableHandler;

    public static event EventHandler<TableEvent> TableHandler;

    public static event EventHandler<TableEvent> LobbyLoadedHandler;
    public static event EventHandler<ChangeDetailsEvent> LobbyLoadRequestHandler;

    /////////////// RENDERING ////////////////
    public static event EventHandler<InfoEvent> DisableRender;
    public static event EventHandler<UpdateEvent> UpdateHandler;
    public static event EventHandler<UpdateEvent> EarlyUpdateHandler;
    public static event EventHandler<UpdateEvent> LateUpdateHandler;

    // User Details
    public static event EventHandler<InfoEvent> SaveUserDetailsHandler;
    public static event EventHandler<InfoEvent> LoadUserDetailsHandler;

    public static event EventHandler<BooleanEvent> GravityStateHandler;



    public MMessageBus()
    {
      Globals.Network.PositionChangeHandler += Network_PositionChangeHandler;
      Globals.Network.DeletedHandler += Network_DeletedHandler; ;
      Globals.Network.PropertyChangeHandler += Network_PropertyChangeHandler;
      Globals.Network.LoggedInHandler += Network_LoggedInHandler; ;
      Globals.Network.USerDetailsChanged += Network_USerDetailsChanged;
      Globals.Network.UserRegisteredHandler += Network_UserRegisteredHandler;
      Globals.Network.ErrorEventHandler += Network_ErrorEventHandler;
      Globals.Network.TableHandler += Network_TableHandler;      
    }

    


    //passes the event onto the gui/render loop thread
    static void GUIEvent<T>(EventHandler<T> e, object sender, T args)
    {
#if DEBUG_EVENTS      
        Console.WriteLine(e.ToString());      
#endif

      if (e != null)
      {
        Globals.GUIThreadOwner.BeginInvoke((MethodInvoker)delegate
        {
          e(sender, args);
        });
      }
    }
    
    public static void NotifyChange(object sender, MObject mo, GraphChangedEvent.ChangeType Reason)
    {
      if (GraphChangeHandler != null)
      {
        GraphChangeHandler(sender, new GraphChangedEvent(mo, Reason));
      }
    }

    public static void LobbyLoadRequest(object sender, ChangeDetailsEvent e)
    {
      GUIEvent(LobbyLoadRequestHandler, sender, e);
    }

    public static void LobbyLoaded(object sender, TableEvent e)
    {
      GUIEvent(LobbyLoadedHandler, sender, new TableEvent(e.Table));
    }

    public static void LoadingStatus(object sender, string sMessage)
    {
      GUIEvent(LoadingStatusHandler, sender, new InfoEvent(sMessage));
    }

    public static void NotifyUpdate(object sender)
    {
      //GUIEvent(UpdateHandler, sender, new UpdateEvent(Time.DeltaTime));
      if (UpdateHandler != null)
      {
        UpdateHandler(sender, new UpdateEvent(Time.DeltaTime));
      }
    }

    public static void NotifyEarlyUpdate(object sender)
    {
      //GUIEvent(LateUpdateHandler, sender, new UpdateEvent(Time.DeltaTime));
      if (EarlyUpdateHandler != null)
      {
        EarlyUpdateHandler(sender, new UpdateEvent(0));
      }
    }

    public static void NotifyLateUpdate(object sender)
    {
      //GUIEvent(LateUpdateHandler, sender, new UpdateEvent(Time.DeltaTime));
      if (LateUpdateHandler != null)
      {
        LateUpdateHandler(sender, new UpdateEvent(0));
      }
    }


    private void Network_TableHandler(object sender, TableEvent e)
    {
      GUIEvent(TableHandler, sender, new TableEvent(e.Table));
    }

    private void Network_ErrorEventHandler(object sender, ErrorEvent e)
    {
      GUIEvent(ErrorHandler, sender, e);
    }

    private void Network_LoggedInHandler(object sender, ChangeDetailsEvent e)
    {
      GUIEvent(LoggedIn, sender, e);
    }

    private void Network_UserRegisteredHandler(object sender, ChangeDetailsEvent e)
    {
      GUIEvent(UserRegistered, sender, e);
    }

    private void Network_USerDetailsChanged(object sender, ChangeDetailsEvent e)
    {
      GUIEvent(UserDetailsChanged, sender, e);
    }

    private void Network_PropertyChangeHandler(object sender, ChangePropertyEvent e)
    {
      GUIEvent(PropertyChangeEvent, sender, e);
    }

    private void Network_DeletedHandler(object sender, DeleteEvent e)
    {
      GUIEvent(ObjectDeletedEvent, sender, e);
    }

    private void Network_PositionChangeHandler(object sender, MoveEvent e)
    {
      GUIEvent(ObjectMovedEvent, sender, e);
    }

    public static void GravityStateChanged(object sender, BooleanEvent b)
    {
      GUIEvent(GravityStateHandler, sender, b);
    }

    public static void AvatarMoved(object sender, string InstanceID, Vector3d Position, Quaterniond Rotation)
    {
      GUIEvent(AvatarMovedEvent, sender, new MoveEvent(InstanceID, Position, Rotation));
    }

    public static void Error(object sender, string sMessage)
    {
      GUIEvent(ErrorHandler,sender, new ErrorEvent(sMessage));
    }

    public static void Select(object sender, SelectEvent mo)
    {
      GUIEvent(SelectEventHandler, sender, mo);      
    }

    public static void ToggleRenderer()
    {
      DisableRender?.Invoke(null, new InfoEvent(""));
    }

    public static void ToggleGravity(object sender)
    {
      ChangeGravityHandler?.Invoke(sender, new ChangeModeEvent(MAvatar.eMoveMode.Flying));
    }

    public static void NavigationEnable(object sender, bool enable)
    {
      if (enable)
      {
        NavigationEnableHandler?.Invoke(sender, new NavigationEvent(Vector3d.Zero));
      }
      else
      {
        NavigationDisableHandler?.Invoke(sender, new NavigationEvent(Vector3d.Zero));
      }
    }

    public static void Navigate(object sender, Vector3d Target)
    {
      NavigationHandler?.Invoke(sender, new NavigationEvent(Target));
    }

    public static void SelectZone(object sender, MServerZone zone)
    {
      if (ZoneSelectHandler != null)
      {
        ZoneSelectHandler(sender, new ZoneEvent(zone));
      }
    }

    public static void UpdateRequired(object sender, string Message)
    {
      UpdateRequiredHandler?.Invoke(sender, new InfoEvent(Message));
    }

    public static void AvatarSetup(object sender)
    {
      AvatarSetupHandler?.Invoke(sender, new InfoEvent("Avatar Setup"));
    }

    public static void AvatarSetAnimation(object sender, string sAnimation)
    {      
      AvatarAnimationChangeRequest?.Invoke(sender, new TextEvent(sAnimation));
    }
    public static void AvatarAnimationChange(object sender, string sAnimation)
    {
      AvatarAnimationChanged?.Invoke(sender, new TextEvent(sAnimation));
    }

    public static void AddZone(object sender, MServerZone zone)
    {
      ZoneAddHandler?.Invoke(sender, new ZoneEvent(zone));
    }

    public static void AddBookmarkRequest(object sender, BookmarkEvent e)
    {
      BookmardAddRequestEvent?.Invoke(sender, e);
    }

    public static void BookmarkAdded(object sender, BookmarkEvent e)
    {
      BookmarkAddedEvent?.Invoke(sender, e);
    }

    public static void ChangeModeRequest(object sender, MAvatar.eMoveMode NewMode)
    {
      ChangeModeHandler?.Invoke(sender, new ChangeModeEvent(NewMode));
    }

    public static void ChangeAvatarRequest(object sender, string sUserID, string sTemplateID)
    {
      ChangeAvatarRequestHandler?.Invoke(sender, new ChangeAvatarEvent(sUserID, sTemplateID));
    }

    public static void AvatarChanged(object sender, string sUserID, string sTemplateID)
    {
      AvatarChangedHandler?.Invoke(sender, new ChangeAvatarEvent(sUserID, sTemplateID));
    }

    public static void TeleportRequest(object sender, Vector3d Pos, Quaterniond Rot)
    {
      TeleportRequestHandler?.Invoke(sender, new TeleportRequestHandler(Pos, Rot));
    }

    public static void TeleportComplete(object sender, string InstanceID, Vector3d Pos, Quaterniond Rot)
    {
      TeleportCompleteHandler?.Invoke(sender, new MoveEvent(InstanceID, Pos, Rot));
    }

   

    public static void ChangeUserID(object sender, string sUID)
    {
      ChangeUserIDHandler?.Invoke(sender, new InfoEvent(sUID));
    }



    public static void Status(object sender, string sMessage)
    {
      GUIEvent(InfoEventHandler, sender, new InfoEvent(sMessage));      
    }

    

    /* public static void Update(object sender, double d)
     {
       if (UpdateHandler != null)
       {
         UpdateHandler(sender, new UpdateEvent(d));
       }
     }*/

    public static void ChangeTextureRequest(object sender, string sInstanceID, string sNewTextureID)
    {
      TextureRequestHandler?.Invoke(sender, new TextureRequestEvent(sInstanceID, sNewTextureID));
    }

    public static void ChangedTexture(object sender, string sInstanceID, string sNewTextureID)
    {
      GUIEvent(TextureChangedHandler, sender, new TextureRequestEvent(sInstanceID, sNewTextureID));      
    }

    public static void DeleteRequest(object sender, MSceneObject mo)
    {
      DeleteObjectRequestHandler?.Invoke(sender, new DeleteRequestEvent(mo));
    }

    //Create Objects offline. Does not persist to the Server
    public static void CreateObjectRequest(object sender, MServerObject mso)
    {
      CreateObjectRequestHandler?.Invoke(sender, new CreateObjectRequestEvent(mso));
    }

    public static void Created(object sender, MSceneObject mo)
    {
      GUIEvent(ObjectCreatedHandler,sender, new CreateEvent(mo));
    }

    public static void Focus(object sender, MSceneObject mo)
    {
      FocusEventHandler?.Invoke(sender, new FocusEvent(mo));
    }

   

    public static void Click(object sender, Point Pos, int Button)
    {
      ClickEventHandler?.Invoke(sender, new MouseClickEvent(new Vector3d(Pos.X, 0, Pos.Y), Button));
    }

    public static void Keyboard(object sender, int KeyCode, bool Down)
    {
      KeyboardHandler?.Invoke(sender, new KeyboardEvent(KeyCode, Down));
    }

    public static void Teleport(object sender, string InstanceID, Vector3d Position)
    {
      TeleportedEventHandler?.Invoke(sender, new MoveEvent(InstanceID, Position, Quaterniond.Identity));
    }

    public static void MoveRequest(object sender, string InstanceID, Vector3d NewPosition, Quaterniond rot)
    {
      MoveRequestEventHandler?.Invoke(sender, new MoveEvent(InstanceID, NewPosition, rot));
    }

    public static void MoveAvatarRequest(object sender, string InstanceID, Vector3d pos, Quaterniond rot)
    {
      MoveAvatarRequestEventHandler?.Invoke(sender, new MoveEvent(InstanceID, pos, rot));
    }

    public static void Rotate(object sender, Quaterniond newRot)
    {
      RotateEventHandler?.Invoke(sender, new RotationRequestEvent(newRot));
    }

    public static void LoadUserDetailsRequest(object sender, InfoEvent e)
    {
      LoadUserDetailsHandler?.Invoke(sender, e);
    }
    public static void SaveUserDetailsRequest(object sender, InfoEvent e)
    {
      LoadUserDetailsHandler?.Invoke(sender, e);
    }
    public static void ChangedUserInfo(object sender)
    {
      ChangedUserInfoHandler?.Invoke(sender, new InfoEvent("Changed"));
    }

  }
}
