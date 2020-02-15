using Massive.Events;
using Massive.Network;
using Massive.Tools;
using MassiveNetwork;
using MassiveNetwork.NetMessages;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Tools;
using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;

/**
 * 
 * Setup() initiates connection
 * CONNECT
 *   LOGIN
 *   GETWORLD
 *   TELEPORT - move fast
 *   MOVE - move, rotate an object
 *   SPAWNREQUEST - to build something
 * */

namespace Massive
{
  /*
  public class MassivePacket
  {
    public string sType;
    public string sData;
  }
  */
  public class MassiveNetwork : MObject
  {
    public const string TYPE_POSROT = "PR";
    public const string TYPE_CHAT = "CH";

    public event EventHandler<StatusEvent> ConnectedToServerHandler;    
    public event EventHandler<StatusEvent> ConnectedToMASSIVEHandler;

    //note: sent from a different thread
    public event EventHandler<ErrorEvent> ErrorEventHandler;
    //note: sent from a different thread
    public event EventHandler<InfoEvent> InfoEventHandler;
    //note: sent from a different thread
    public event EventHandler<NetworkStatusEvent> StatusEventHandler;
    //public event EventHandler<GraphChangedEvent> GraphChangedHandler;
    public event EventHandler<DeleteEvent> DeletedHandler;
    public event EventHandler<ObjectSpawnedEvent> SpawnHandler;
    public event EventHandler<MoveEvent> PositionChangeHandler;
    public event EventHandler<MoveEvent> TeleportHandler;
    public event EventHandler<TextureEvent> TextureHandler;
    public event EventHandler<ChangePropertyEvent> PropertyChangeHandler;
    public event EventHandler<ChangeDetailsEvent> USerDetailsChanged;

    public event EventHandler<ChangeDetailsEvent> UserRegisteredHandler;

    public event EventHandler<ChangeDetailsEvent> LoggedInHandler;
    public event EventHandler<LoggedOutEvent> LoggedOutHandler;
    public event EventHandler<ChatEvent> ChatEventHandler;

    public event EventHandler<ZoneEvent> ZoneCreateHandler;
    public event EventHandler<ZoneEvent> ZoneDeletedHandler;
    public event EventHandler<EventArgs> ZoneCompleteEventHandler;
    public event EventHandler<ZoneEvent> ZoneUpdatedEventHandler;

    public event EventHandler<InfoEvent> InfoDumpHandler;
    public event EventHandler<InfoEvent> MergeResultHandler;
    public event EventHandler<TableEvent> TableHandler;

    List<IPEndPoint> Listeners = new List<IPEndPoint>();

    List<DataTable> SpawnTasks = new List<DataTable>();

    // public DataSerializer Serializer { get; set; }

    private int packetVersion = 1;
    public int PacketVersion { get => packetVersion; set => packetVersion = value; }

    //public string _UserID = "";
    //public string SettingsFile = @"settings.txt";

    //private string serverIP = "127.0.0.1";
    private string serverIP = "127.0.0.1";
    public string ServerIP { get => serverIP; set => serverIP = value; }
    public string LocalIP = "127.0.0.1";

    private string serverDomain = "bigfun.co.za";
    public string ServerDomain { get => serverDomain; set => serverDomain = value; }

    private int serverPort = 50895;
    public int ServerPort { get => serverPort; set => serverPort = value; }

    public static bool isServer => _instance.IsServer;
    private bool isServer1 = false;
    public bool IsServer { get => isServer1; set => isServer1 = value; }    

    public bool Connected = false;
    public bool Ready = false;

    ConnectionInfo targetServerConnectionInfo;
    static MassiveNetwork _instance;

    public MassiveNetwork() : base(EType.NetworkHelper, "Network")
    {
      _instance = this;
    }

    public static MassiveNetwork GetInstance()
    {
      return _instance;
    }

    public void ConnectToLobby()
    {
      Setup();
    }

    public void Disconnect()
    {
      Dispose();
    }

    public void HandleV1(MNetMessage m, string sDataString)
    {
      if (Settings.DebugNetwork == true)
      {
        Console.WriteLine(m.Command + " :: " + m.Payload);
      }

      switch (m.Command)
      {        
        case MNetMessage.CONNECTTOMASSIVE:
          HandleConnectedToMASSIVE(m);
          break;
        case MNetMessage.LOGIN:
          Globals.Log(this, "LOGGED IN>>>");
          LoggedIn(m);
          break;
        case MNetMessage.MAXCONNECTIONS:
          Globals.Log(this, "CAN'T CONNECT. MAXIMUM CONNECTIONS REACHED");
          break;
        case MNetMessage.ERROR:
          Error(m);
          break;
        case MNetMessage.REGISTERUSER:
          UserRegistered(m);
          break;
        case MNetMessage.CHANGEDETAILS:
          HandleDetailsChanged(m);
          break;
        case MNetMessage.READY:
          Globals.Log(this, "READY>>>");
          Ready = true;
          //SendAsync(new MNetMessage(1, Globals.UserAccount.UserID, MNetMessage.CONNECT, MZone.ZoneCode));
          break;
        case MNetMessage.CHAT:
          Chat(m);
          break;
        case MNetMessage.TEXTURE:
          Texture(m);
          break;
        case MNetMessage.POSITION:
          MoveObject(m);
          break;
        case MNetMessage.TELEPORT:
          Teleport(m);
          break;
        case MNetMessage.CHANGEPROPERTY:
          ChangeProperty(m);
          break;
        case MNetMessage.CHANGEAVATAR:
          ChangeAvatar(m);
          break;
        case MNetMessage.DELETE:
          Delete(m);
          break;
        case MNetMessage.SPAWN:
          SpawnObjects(m);
          break;
        case MNetMessage.CREATEZONE:
          CreateZone(m);
          break;
        case MNetMessage.DELETEZONE:
          DeleteZone(m);
          break;
        case MNetMessage.UPDATEZONE:
          UpdateZone(m);
          break;
        case MNetMessage.LOGGEDOUT: //when anyone logs out
          Loggedout(m);
          break;
        case MNetMessage.INFODUMP:
          InfoDump(m);
          break;
        case MNetMessage.MERGE:
          MergeResult(m);
          break;
        case MNetMessage.GETTABLE:
          GetTable(m);
          break;
      }
    }

    public void Info(string s)
    {
      if (InfoEventHandler != null)
      {
        InfoEventHandler(this, new InfoEvent(s));
      }
    }

    public void Status(bool connected, bool failed, string message)
    {
      if (StatusEventHandler != null)
      {
        StatusEventHandler(this, new NetworkStatusEvent(connected, failed, message));
      }
    }

    public override void Dispose()
    {
      //Console.WriteLine("Disposing Network");
      Status(false, false, "Network Reset");

      //NetworkComms.OnCommsShutdown -= NetworkComms_OnCommsShutdown;
      NetworkComms.RemoveGlobalConnectionEstablishHandler(HandleConnection);
      NetworkComms.RemoveGlobalConnectionCloseHandler(HandleDisConnection);
      NetworkComms.RemoveGlobalIncomingPacketHandler<string>("Message", Receive);
      NetworkComms.CloseAllConnections();

      //Connection.StopListening();      
      //NetworkComms.CloseAllConnections();
      if (Connected == true)
      {
        Connected = false;
        try
        {
          //NetworkComms.Shutdown();
        }
        catch (Exception e)
        {
          Console.WriteLine("MassiveNetwork Exception : " + e.Message);
        }
      }

      if (ConnectedToServerHandler != null)
      {
        ConnectedToServerHandler(this, new StatusEvent(false, "Disconnected"));
      }

      base.Dispose();
    }

    public string GetUserID()
    {
      if (string.IsNullOrEmpty(Globals.UserAccount.UserID))
      {
        //if (File.Exists(SettingsFile))
        //{
        //Globals.UserAccount.UserID = File.ReadAllText(SettingsFile);
        //}
        //else
        //{
        //Globals.UserAccount.UserID = Helper.GUID();
        // File.WriteAllText(SettingsFile, Globals.UserAccount.UserID);
        //}
      }

      return Globals.UserAccount.UserID;
    }

    public void SetUserID(string sID)
    {
      Globals.UserAccount.UserID = sID;
    }

    public override void Setup()
    {
      base.Setup();

      //Globals.UserAccount.UserID = GetUserID();

      NetworkComms.DisableLogging();

      if (Connected == false)
      {
        //Serializer = DPSManager.GetDataSerializer<ProtobufSerializer>();
        try
        {
          IPEndPoint ep = IPTools.ParseEndPointFromString(ServerIP + ":" + ServerPort.ToString());
          targetServerConnectionInfo = new ConnectionInfo(ep, ApplicationLayerProtocolStatus.Enabled);

          NetworkComms.OnCommsShutdown += NetworkComms_OnCommsShutdown;
          NetworkComms.AppendGlobalConnectionEstablishHandler(HandleConnection);
          NetworkComms.AppendGlobalConnectionCloseHandler(HandleDisConnection);
          NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", Receive);

          IPAddress ip = IPAddress.Parse(LocalIP);
          //Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(ip, 0));          


          Listeners.Clear();
          foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
          {
            Listeners.Add(localEndPoint);
            Info("Listening on:  " + localEndPoint.Address + ":" + localEndPoint.Port.ToString());
            Console.WriteLine(localEndPoint.Address + ":" + localEndPoint.Port.ToString());
          }

          Connected = true;

          //send a blank message to activate the connection
          SendConnectToMASSIVERequest();
          //Send(new MNetMessage(1, Globals.UserAccount.UserID, MNetMessage.CONNECTREQ, ip.ToString()));

          MScene.UtilityRoot.Add(this);

        }
        catch (Exception e)
        {
          Connected = false;
          if (ErrorEventHandler != null)
          {
            ErrorEventHandler(this, new ErrorEvent(e.Message));
          }
          Status(false, true, e.Message);
        }
        finally
        {

        }
      }
    }

    private void NetworkComms_OnCommsShutdown(object sender, EventArgs e)
    {
      Connected = false;
      if (ConnectedToServerHandler != null)
      {
        ConnectedToServerHandler(this, new StatusEvent(false, "Disconnected"));
      }
      NetworkComms.OnCommsShutdown -= NetworkComms_OnCommsShutdown;
    }

    private void HandleConnection(Connection connection)
    {
      //connection.ConnectionDefaultSendReceiveOptions.DataProcessors.Add(NetworkCommsDotNet.DPSBase.DPSManager.GetDataProcessor<RijndaelPSKEncrypter>());
      //Set a password to use when encrypted data is sent/recieved on the Connection connection 
      //RijndaelPSKEncrypter.AddPasswordToOptions(connection.ConnectionDefaultSendReceiveOptions.Options, "_MASSIVE123");
      Console.WriteLine(connection.ConnectionInfo.RemoteEndPoint.ToString());
      Connected = true;
      Status(true, false, "Connected");

      //SendLoginRequest();
      if (ConnectedToServerHandler != null)
      {
        ConnectedToServerHandler(this, new StatusEvent(true, "Connected"));
      }

    }

    private void HandleDisConnection(Connection connection)
    {
      Connected = false;
      //if (connection.ConnectionInfo.IsConnectable)
      {
        //connection.CloseConnection(false);
      }

      // Dispose();
      Status(false, false, "Disconnected");
    }

    public void SendConnectToMASSIVERequest()
    {
      MNetMessage m = new MNetMessage();
      m.UserID = Globals.UserAccount.UserID;
      m.Command = MNetMessage.CONNECTTOMASSIVEREQ;
      Send(m);
    }

    public void SendLoginRequest()
    {
      if (Connected == false) return;
      MNetMessage m = new MNetMessage();
      m.UserID = Globals.UserAccount.UserID;
      m.Command = MNetMessage.LOGINREQ;
      //TODO: Cast as connect message
      MLoginMessageRequest lir = new MLoginMessageRequest();
      lir.Email = Globals.UserAccount.Email;
      lir.UserName = Globals.UserAccount.UserName;
      lir.Password = Globals.UserAccount.Password;
      lir.Zone = Globals.UserAccount.Zone;
      m.Payload = lir.Serialize();
      SendAsync(m);
    }

    public void ChangeDetailsRequest(MUserAccount mu)
    {
      MNetMessage m = new MNetMessage();
      m.Command = MNetMessage.CHANGEDETAILSREQ;
      m.UserID = GetUserID();

      m.Payload = mu.Serialize();

      SendAsync(m);
    }

    public void Receive(PacketHeader packetHeader, Connection connection, string incomingObject)
    {
      MNetMessage m = JsonConvert.DeserializeObject<MNetMessage>(incomingObject);
      if (m.Version == 1)
      {
        HandleV1(m, incomingObject);
      }
    }

    new void Error(MNetMessage m)
    {
      Globals.Log(this, m.Payload);
      ErrorEventHandler?.Invoke(this, new ErrorEvent(m.Payload));
    }

    public void MergeRequest(string sJson)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.MERGEREQ;
      mn.Payload = sJson;
      SendAsync(mn);
    }

    public void MergeResult(MNetMessage m)
    {
      MergeResultHandler?.Invoke(this, new InfoEvent(m.Payload));
    }

    public void GetTable(MNetMessage m)
    {
      MTableMessage mtm = JsonConvert.DeserializeObject<MTableMessage>(m.Payload);
      TableHandler?.Invoke(this, new TableEvent(mtm.SpawnTable));
    }

    public void InfoDumpRequest(string UserID)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.INFODUMPREQ;
      mn.UserID = UserID;
      Send(mn);
    }

    void InfoDump(MNetMessage m)
    {
      //Dictionary<string, MServerObject> Results = JsonConvert.DeserializeObject<Dictionary<string, MServerObject>>(m.Payload);
      InfoDumpHandler?.Invoke(this, new InfoEvent("Results", m.Payload));
    }

    void HandleConnectedToMASSIVE(MNetMessage m)
    {
      ConnectedToMASSIVEHandler?.Invoke(this, new StatusEvent(true, "Connected to _MASSIVE"));
    }
    /**
     * When any user logs out (within range)
     * */
    void Loggedout(MNetMessage m)
    {
      LoggedOutHandler?.Invoke(this, new LoggedOutEvent(m.UserID));
    }

    void LoggedIn(MNetMessage m)
    {
      MUserAccount mu = JsonConvert.DeserializeObject<MUserAccount>(m.Payload);
      Globals.UserAccount.UserName = mu.UserName;
      //Globals.UserAccount.HomePosition = mu.HomePosition;
      Globals.UserAccount.UserID = mu.UserID;
      Globals.UserAccount.TotalObjects = mu.TotalObjects;
      Globals.UserAccount.MaxObjects = mu.MaxObjects;
      Globals.UserAccount.MaxFoundations = mu.MaxFoundations;
      Globals.UserAccount.Credit = mu.Credit;

      LoggedInHandler(this, new ChangeDetailsEvent(true, "Logged In"));

      //GetZones();
      //GetWorld();

    }

    public void GetWorld()
    {
      MNetMessage mn = new MNetMessage(1, Globals.UserAccount.UserID, MNetMessage.GETWORLD, MZone.ZoneCode);
      SendAsync(mn);
    }

    public void GetTableRequest(string sTable)
    {
      MNetMessage mn = new MNetMessage(1, Globals.UserAccount.UserID, MNetMessage.GETTABLEREQ, sTable);
      SendAsync(mn);
    }

    public void GetZones()
    {
      MNetMessage mn = new MNetMessage(1, Globals.UserAccount.UserID, MNetMessage.GETZONES, MZone.ZoneCode);
      SendAsync(mn);
    }

    public void ChatRequest(string UserIDDestination, string Message, int IncrementingID)
    {
      MNetMessage m = new MNetMessage();
      m.Command = MNetMessage.CHATREQ;
      m.UserID = Globals.UserAccount.UserID;

      MChatMessage c = new MChatMessage();
      c.Message = Message;
      c.TargetID = UserIDDestination;
      c.OwnerID = Globals.UserAccount.UserID;
      c.OwnerName = Globals.UserAccount.UserName;
      c.MessageType = "Chat";
      c.MessageID = IncrementingID;
      m.Payload = c.Serialize();

      SendAsync(m);
    }

    void Chat(MNetMessage m)
    {
      MChatMessage cm = MChatMessage.Deserialize<MChatMessage>(m.Payload);
      ChatEventHandler?.Invoke(this, new ChatEvent(cm));
    }

    void UpdateAvatar(string OldID, string NewID)
    {
      //update avatar
      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(OldID);
      if (mo != null)
      {
        mo.OwnerID = NewID;
        mo.InstanceID = NewID;
      }
    }

    public void UserRegisterRequest(MUserAccount mu)
    {
      MNetMessage m = new MNetMessage();
      m.Command = MNetMessage.REGISTERUSERREQ;
      //m.UserID = GetUserID();
      m.Payload = mu.Serialize();
      SendAsync(m);
    }

    void UserRegistered(MNetMessage m)
    {
      UpdateAvatar(Globals.UserAccount.UserID, m.UserID);

      if (Globals.UserAccount.UserID == null)
      {
        Globals.UserAccount.UserID = m.UserID;
      }
      if (UserRegisteredHandler != null)
      {
        MChangeDetailsResult cd = MChangeDetailsResult.Deserialize<MChangeDetailsResult>(m.Payload);
        UserRegisteredHandler(this, new ChangeDetailsEvent(cd.Success, cd.Message));
      }
    }

    void HandleDetailsChanged(MNetMessage m)
    {
      if (USerDetailsChanged != null)
      {
        MChangeDetailsResult cd = MChangeDetailsResult.Deserialize<MChangeDetailsResult>(m.Payload);
        USerDetailsChanged(this, new ChangeDetailsEvent(cd.Success, cd.Message));
      }
    }

    void SpawnObjects(MNetMessage m)
    {
      MSpawnMessage msm = MSpawnMessage.Deserialize<MSpawnMessage>(m.Payload);
      SpawnTasks.Add(msm.SpawnTable);
      //foreach( DataRow dr in dt.Rows)
      //{
      //MServerObject mso = new MServerObject();
      //mso.UnpackFromDataRow(dt.Columns, dr);
      //}
      // if (msm.Spawnables == null) return;

      //foreach (MServerObject mso in msm.Spawnables)
      //{
      //        SpawnTasks.Add(mso);
      //    }
    }

    //called from main thread
    public void SpawnCheck()
    {
      if (SpawnTasks.Count() <= 0)
      {
        return;
      }

      DataTable mp = SpawnTasks[0];
      SpawnTasks.RemoveAt(0);
      if (mp == null)
      {
        return;
      }
      if (SpawnHandler != null)
      {
        SpawnHandler(this, new ObjectSpawnedEvent(mp));
      }
      else
      {
        Console.Error.WriteLine("ERROR: NO SpawnHandler for object in MassiveNetwork {0}", this.Name);
      }
    }

    public void SendAsync(MNetMessage m)
    {
      if (!Connected) return;
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += Bw_DoWork;
      worker.RunWorkerAsync(m);
      //Bw_DoWork(this, new DoWorkEventArgs(m));
      if (Settings.DebugNetwork == true)
      {
        Console.WriteLine(m.Command);
      }
    }

    private void Bw_DoWork(object sender, DoWorkEventArgs e)
    {
      MNetMessage p = (MNetMessage)e.Argument;

      NetworkComms.SendObject("Message",
         ((System.Net.IPEndPoint)targetServerConnectionInfo.RemoteEndPoint).Address.ToString(),
         ((System.Net.IPEndPoint)targetServerConnectionInfo.RemoteEndPoint).Port,
         p.Serialize());
    }

    public void Send(MNetMessage m)
    {
      if (!Connected) return;
      NetworkComms.SendObject("Message",
          ((System.Net.IPEndPoint)targetServerConnectionInfo.RemoteEndPoint).Address.ToString(),
          ((System.Net.IPEndPoint)targetServerConnectionInfo.RemoteEndPoint).Port,
          m.Serialize());
    }

    public void ChangePropertyRequest(string InstanceID, string PropertyTag)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.CHANGEPROPERTYREQ;
      mn.UserID = Globals.UserAccount.UserID;

      MChangeProperty cp = new MChangeProperty();
      cp.InstanceID = InstanceID;
      //PropertyTag = Uri.EscapeUriString(PropertyTag);
      cp.PropertyTag = PropertyTag;

      mn.Payload = cp.Serialize();

      SendAsync(mn);
    }

    public void ChangeAvatarRequest(string UserID, string AvatarID)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.CHANGEAVATARREQUEST;
      mn.UserID = Globals.UserAccount.UserID;

      MChangeAvatarRequest mc = new MChangeAvatarRequest();
      mc.AvatarID = AvatarID;

      mn.Payload = mc.Serialize();

      SendAsync(mn);
    }


    /**
     * Sends a request to the server to spawn an object
     * */
    public void SpawnRequest(string TemplateID, string TextureID, string sName, string sTag,
      Vector3d pos, Quaterniond Rot, string RequestedInstanceID = "", 
      int StaticStorage = 1, double radius = 200)
    {
      if (Connected == false) return;

      List<MServerObject> Temp = new List<MServerObject>();

      DataTable dt = DB.CreateObjectTable();
      DataRow dr = dt.NewRow();
      dr[DB.OWNERID] = Globals.UserAccount.UserID;
      dr[DB.TEMPLATEID] = TemplateID;
      dr[DB.INSTANCEID] = RequestedInstanceID;
      dr[DB.TEXTUREID] = TextureID;
      dr[DB.PERSIST] = StaticStorage;
      dr[DB.NAME] = sName;
      dr[DB.TAG] = sTag;
      dr[DB.RADIUS] = radius;
      dr[DB.NAME] = sName;
      dr[DB.RADIUS] = radius;
      dr[DB.X] = pos.X;
      dr[DB.Y] = pos.Y;
      dr[DB.Z] = pos.Z;
      dr[DB.RX] = Rot.X;
      dr[DB.RY] = Rot.Y;
      dr[DB.RZ] = Rot.Z;
      dr[DB.RW] = Rot.W;
      dt.Rows.Add(dr);

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.SPAWNREQUEST;
      //string sJSON = JsonConvert.SerializeObject(Temp);
      MSpawnMessage msr = new MSpawnMessage(dt);
      mn.Payload = msr.Serialize();

      Send(mn);
    }

    public void TextureRequest(string InstanceID, string TextureID)
    {
      MTextureMessage mt = new MTextureMessage();
      mt.InstanceID = InstanceID;
      mt.TextureID = TextureID;
      mt.OwnerID = Globals.UserAccount.UserID;

      MNetMessage mn = new MNetMessage();
      mn.UserID = Globals.UserAccount.UserID;
      mn.Command = MNetMessage.TEXTUREREQ;
      mn.Payload = mt.Serialize();
      Send(mn);
    }

    public void Texture(MNetMessage m)
    {
      MTextureMessage mt = MTextureMessage.Deserialize<MTextureMessage>(m.Payload);
      TextureHandler(this, new TextureEvent(mt.InstanceID, mt.TextureID));
    }

    public void CreateZoneRequest(MServerZone z)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.CREATEZONEREQ;
      mn.Payload = z.Serialize();
      SendAsync(mn);
    }

    public void DeleteZone(MNetMessage m)
    {
      MServerZone zone = MServerZone.Deserialize<MServerZone>(m.Payload);
      ZoneDeletedHandler(this, new ZoneEvent(zone));
      if (zone.OwnerID.Equals(Globals.UserAccount.UserID))
      {
        Globals.Log(this, "Zone " + zone.Name + " Deleted");
      }
    }

    public void UpdateZone(MNetMessage m)
    {
      MServerZone z = MServerZone.Deserialize<MServerZone>(m.Payload);
      if (ZoneUpdatedEventHandler != null)
      {
        ZoneUpdatedEventHandler(this, new ZoneEvent(z));
      }
    }

    public void UpdateZoneRequest(MServerZone z)
    {
      MNetMessage m = new MNetMessage();
      m.Command = MNetMessage.UPDATEZONEREQ;
      m.Payload = z.Serialize();
      SendAsync(m);
    }

    public void CreateZone(MNetMessage m)
    {
      List<MServerZone> Zones = JsonConvert.DeserializeObject<List<MServerZone>>(m.Payload);
      foreach (MServerZone zon in Zones)
      {
        ZoneCreateHandler?.Invoke(this, new ZoneEvent(zon));
      }
      ZoneCompleteEventHandler?.Invoke(this, new EventArgs());
    }

    public void DeleteZoneRequest(MServerZone m)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.DELETEZONEREQ;
      mn.Payload = m.Serialize();
      Send(mn);
    }

    public void PositionRequest(string InstanceID, Vector3d NewPos, Quaterniond NewRot)
    {
      MPosMessage p = new MPosMessage();
      p.UserID = Globals.UserAccount.UserID;
      p.Position = new double[3] { NewPos.X, NewPos.Y, NewPos.Z };
      p.Rotation = new double[4] { NewRot.X, NewRot.Y, NewRot.Z, NewRot.W };
      p.InstanceID = InstanceID;

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.POSITIONREQ;
      mn.UserID = GetUserID();
      mn.Payload = p.Serialize();

      SendAsync(mn);
    }

    public void TeleportRequest(string InstanceID, Vector3d NewPos, Quaterniond NewRot)
    {
      MTeleportMessage p = new MTeleportMessage(InstanceID,
        NewPos.X, NewPos.Y, NewPos.Z,
        NewRot.X, NewRot.Y, NewRot.Z, NewRot.W);

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.TELEPORTREQ;
      mn.UserID = GetUserID();
      mn.Payload = p.Serialize();

      Send(mn);
    }

    public void Teleport(MNetMessage m)
    {
      MTeleportMessage tm = MTeleportMessage.Deserialize<MTeleportMessage>(m.Payload);
      Vector3d pos = new Vector3d(tm.Locus[0], tm.Locus[1], tm.Locus[2]);
      Quaterniond rot = new Quaterniond(tm.Rotation[0], tm.Rotation[1], tm.Rotation[2], tm.Rotation[3]);
      //if a teleport handler is connected, use that, otherwise move it directly

      TeleportHandler?.Invoke(this, new MoveEvent(tm.InstanceID, pos, rot));
    }

    public void ChangeProperty(MNetMessage m)
    {
      MChangeProperty cp = IMSerializable.Deserialize<MChangeProperty>(m.Payload);

      PropertyChangeHandler?.Invoke(this, new ChangePropertyEvent(cp.InstanceID, cp.PropertyTag));
    }

    public void ChangeAvatar(MNetMessage m)
    {
      MChangeAvatarResponse mr = MChangeAvatarResponse.Deserialize<MChangeAvatarResponse>(m.Payload);
      MMessageBus.AvatarChanged(this, m.UserID, mr.AvatarID);
    }



    public void MoveObject(MNetMessage mn)
    {
      MPosMessage pm = MPosMessage.Deserialize<MPosMessage>(mn.Payload);

      PositionChangeHandler?.Invoke(this, new MoveEvent(pm.InstanceID,
       MassiveTools.VectorFromArray(pm.Position),
       MassiveTools.QuaternionFromArray(pm.Rotation)));
    }

    /*
     * Requests SERVER delete an object
     * */
    public void DeleteRequest(string UniverseID)
    {
      if (Connected == false) return;

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.DELETEREQUEST;
      mn.UserID = Globals.UserAccount.UserID;

      MDeleteMessage p = new MDeleteMessage();
      p.UserID = Globals.UserAccount.UserID;
      p.InstanceID = UniverseID;

      mn.Payload = p.Serialize();
      Send(mn);
    }

    public void Delete(MNetMessage mn)
    {
      MDeleteMessage pmd = MDeleteMessage.Deserialize<MDeleteMessage>(mn.Payload);
      DeletedHandler?.Invoke(this, new DeleteEvent(pmd.InstanceID));
    }

    public override void Update()
    {
      if (SpawnTasks.Count() > 0)
      {
        SpawnCheck();
      }
      base.Update();
    }


  }
}
