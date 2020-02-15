using Massive.Network;
using MassiveNetwork;
using MassiveNetwork.NetMessages;
using MassiveServer;
using MassiveServer.src;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.DPSBase;
using NetworkCommsDotNet.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

/***
 * 
 * PacketHandler -> World -> Zones -> Zone.Read/Write
 * 
 **/
namespace Massive.Server
{
  public class MServer
  {
    public string Version = "1.005";
    public const int MAXCONNECTIONS = 100;
    public double DistanceThreshold = 30; //m, distance of avatar movement until a world update is sent
    public bool DisplayEvents = false;

    public MDBAdvocate _DataBase;

    Stopwatch stopwatch;

    public event EventHandler<ServerEvent> ServerInfo;
    public event EventHandler<ServerEvent> MetricInfo;
    public event EventHandler<ServerEvent> ClientConnected;
    public event EventHandler<ServerEvent> ClientLoggedIn;
    public event EventHandler<ServerEvent> ClientDisconnected;

    public event EventHandler<ZoneEvent> ZoneChanged;
    public event EventHandler<ServerEvent> UniverseChanged;

    public const int OUTGOING = 0;
    public const int OUTGOINGBROADCAST = 1;
    public const int INCOMING = 2;
    public const int UTILITY = 3;
    public const int ERROR = 4;

    //public DataSerializer Serializer { get; set; }

    public IPEndPoint lastServerIPEndPoint = null;
    public string ServerIPAddress = "127.0.0.1";
    public int ServerPort = 50895;
    public string SALT = "MSV";
    public ConnectionType ConnectionType { get; set; }
    public List<MClient> MassiveConnections = new List<MClient>(); //BindingList for live view list

    public List<MUniverse> MassiveUniverses;
    //public MUniverse CurrentUniverse;

    public MZoneHandler _ZoneHandler;

    public MEconomy _Economy;

    public const string Admin = "CnXizbR4U0GkUeugc45s7w";

    public MServer()
    {
      _DataBase = new MDBAdvocate();
      stopwatch = new Stopwatch();
      ConnectionType = ConnectionType.TCP;
      ServerIPAddress = GetLocalIPAddress();
      MassiveUniverses = new List<MUniverse>();
      //CurrentUniverse = new MUniverse();
      //MassiveUniverses.Add(CurrentUniverse);

      _Economy = new MEconomy();

      if (!Directory.Exists("data"))
      {
        Directory.CreateDirectory("data");
      }

      _ZoneHandler = new MZoneHandler();
    }

    public void FlushInactiveObjects()
    {
      _DataBase.FlushPlayers();
      //CurrentUniverse.Flush();
    }

    public void Start()
    {
      //NetworkCommsDotNet.DPSBase.RijndaelPSKEncrypter.AddPasswordToOptions(NetworkComms.DefaultSendReceiveOptions.Options, "_MASSIVE123");
      Log("Encryption active", 2);
      DataSerializer dataSerializer = DPSManager.GetDataSerializer<ProtobufSerializer>(); ;
      Log("Data SerializeR:" + dataSerializer.ToString(), UTILITY);
      List<DataProcessor> dataProcessors = new List<DataProcessor>();
      //dataProcessors.Add(DPSManager.GetDataProcessor<QuickLZ>());

      NetworkComms.AppendGlobalConnectionEstablishHandler(HandleConnection);
      NetworkComms.AppendGlobalIncomingPacketHandler<string>("Message", MessageReceived);
      NetworkComms.AppendGlobalConnectionCloseHandler(HandleConnectionClosed);
      NetworkComms.DefaultSendReceiveOptions = new SendReceiveOptions(dataSerializer, NetworkComms.DefaultSendReceiveOptions.DataProcessors, NetworkComms.DefaultSendReceiveOptions.Options);

      IPAddress ip = IPAddress.Parse(ServerIPAddress);
      Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(ip, ServerPort));
      Log("Listening for TCP messages on:", UTILITY);
      foreach (System.Net.IPEndPoint localEndPoint in Connection.ExistingLocalListenEndPoints(ConnectionType.TCP))
      {
        Log(localEndPoint.Address + ":" + localEndPoint.Port.ToString(), UTILITY);
      }

      Log("Data Directory is:" + Path.Combine(Directory.GetCurrentDirectory(), "data"), UTILITY);

      MExternalIPSniffer.SniffIP();
    }

    MClient GetClientFromMessage(Connection connection)
    {
      IPEndPoint ipe = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;
      foreach (MClient c in MassiveConnections)
      {
        //if (c.Address.Equals(ipe.Address))
        if (c.connection == connection)
        {
          return c;
        }
      }

      return null;
    }

    private void HandleConnection(Connection connection)
    {
      IPEndPoint ipe = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;
      //string s = connection.ConnectionInfo.RemoteIPEndPoint.Address + ":" + connection.ConnectionInfo.RemoteIPEndPoint.Port;
      string s = ipe.Address + ":" + ipe.Port;

      if ((ipe.Address.ToString() == ServerIPAddress)
      && (ipe.Port == ServerPort))
      {
        return;
      }

      MClient c = new MClient();
      c.State = MClient.STATE_CONNECTING;
      c.connection = connection;
      c.Address = ipe.Address;
      c.Port = ipe.Port;
      c.Account.ClientIP = ipe.Address.ToString();
      MassiveConnections.Add(c);

      if (ClientConnected != null)
      {
        ServerEvent se = new ServerEvent("CONNECTED");
        se.Client = c;
        ClientConnected(this, se);
      }

      Log("Connected:" + c.ToString(), UTILITY);

      MNetMessage m = new MNetMessage();
      int Count = MassiveConnections.Count;
      if (Count < MAXCONNECTIONS)
      {
        m.Command = MNetMessage.READY;
        m.UserID = MNetMessage.SERVER;
        Send(c, "Message", m.Serialize());
      }
      else
      {
        DisconnectClient(connection, "Maximum Connections Reached. Please try a different server, or come back later.");
      }
    }

    void DisconnectClient(Connection connection, string Reason)
    {
      MClient c = GetClientFromMessage(connection);
      MNetMessage m = new MNetMessage();
      m.Command = MNetMessage.MAXCONNECTIONS;
      m.UserID = MNetMessage.SERVER;
      m.Version = 1;
      m.Payload = Reason;
      c.State = MClient.STATE_DISCONNECTED;
      Send(c, "Message", m.Serialize());
      connection.CloseConnection(false);
    }

    private void HandleConnectionClosed(Connection connection)
    {
      //IPEndPoint ipe = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;
      //string s = ipe.Address + ":" + ipe.Port;

      MClient ToRemove = null;
      foreach (MClient m in MassiveConnections)
      {
        if (m.connection == connection)
        {
          ToRemove = m;
        }
      }

      _DataBase.RemoveUserFromUniverse(ToRemove.Account.UserID);
      //CurrentUniverse.Cleanup(ToRemove.Account.UserID);

      if (ToRemove != null)
      {
        ToRemove.Account.LastActivity = DateTime.Now;
        ToRemove.Save();
        MassiveConnections.Remove(ToRemove);
      }

      if (ClientDisconnected != null)
      {
        ServerEvent se = new ServerEvent("DISCONNECTED");
        ClientDisconnected(this, se);
      }

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.LOGGEDOUT;
      mn.UserID = ToRemove.Account.UserID;
      SendToAllClients(ToRemove, mn.Serialize());

      Log("Disconnected:" + ToRemove.ToString(), UTILITY);

    }

    /**
     * 
     * */
    private void MessageReceived(PacketHeader packetHeader, Connection connection, string incomingString)
    {
      stopwatch.Restart();
      IPEndPoint ipe = (IPEndPoint)connection.ConnectionInfo.RemoteEndPoint;
      //Console.WriteLine("\n  ... Incoming message from " + connection.ToString() + " saying '" + incomingString + "'.");

      Log("[" + packetHeader.PacketSequenceNumber.ToString()
        + " " + ipe.Address.ToString() + ":" + ipe.Port + "]"
        + " " + incomingString, INCOMING);

      //if its a spawn message, save the spawn into the world (if there's quota)      
      MNetMessage m = MNetMessage.Deserialize(incomingString);
      MClient c = GetClientFromMessage(connection);
      c.Account.LastActivity = DateTime.Now;
      c.ActivityFlag = true;
      switch (m.Command)
      {
        case MNetMessage.CONNECTTOMASSIVEREQ:
          ConnectToMASSIVE(c, m);
          break;
        case MNetMessage.LOGINREQ:
          LoginRequest(c, m);
          break;
        case MNetMessage.CHATREQ:
          ChatMessage(c, m);
          break;
        case MNetMessage.SPAWNREQUEST:
          SpawnRequest(c, m);
          break;
        case MNetMessage.REGISTERUSERREQ: //Register user details or change/update user details
          RegisterUser(c, m);
          break;
        case MNetMessage.CHANGEDETAILSREQ: //Register user details or change/update user details
          ChangeDetails(c, m);
          break;
        case MNetMessage.CHANGEPROPERTYREQ: // object properties
          ChangeProperty(c, m);
          break;
        case MNetMessage.CHANGEAVATARREQUEST:
          ChangeAvatar(c, m);
          break;
        case MNetMessage.GETWORLD:
          SyncWorld(c);
          break;
        case MNetMessage.GETZONES:
          SyncZones(c);
          break;
        case MNetMessage.GETTABLEREQ:
          GetTable(c, m);
          break;
        case MNetMessage.CREATEZONEREQ:
          CreateZone(c, m);
          break;
        case MNetMessage.DELETEZONEREQ:
          DeleteZone(c, m);
          break;
        case MNetMessage.UPDATEZONEREQ:
          UpdateZone(c, m);
          break;
        case MNetMessage.TEXTUREREQ:
          SetTexture(c, m);
          break;
        case MNetMessage.POSITIONREQ:
          ChangePosition(c, m);
          break;
        case MNetMessage.TELEPORTREQ:
          Teleport(c, m);
          break;
        case MNetMessage.DELETEREQUEST:
          DeleteObject(c, m);
          break;
        case MNetMessage.INFODUMPREQ:
          InfoDump(c, m);
          break;
        case MNetMessage.MERGEREQ: //TODO: Should only be called by admins
          //Merge(c, m);
          break;
      }

      stopwatch.Stop();

      if (MetricInfo != null)
      {
        if (m != null)
        {
          string sMessage = m.Command + " Executed in: " + stopwatch.ElapsedMilliseconds + "ms";
          MetricInfo(this, new ServerEvent(sMessage));
        }
      }
    }

    public void InfoDump(MClient c, MNetMessage m)
    {
      MNetMessage mr = new MNetMessage();
      mr.Command = MNetMessage.INFODUMP;
      mr.UserID = "SERVER";

      //mr.Payload = CurrentUniverse.DumpUser(m.UserID);
      mr.Payload = _DataBase.DumpUser(m.UserID);
      Send(c, "Message", mr.Serialize());
    }

    /**
     * ADMIN ONLY
     * Merges a dictionary of objects into the store
     * TODO: check that the client is an admin     
     * */
    /*
   public void Merge(MClient c, MNetMessage m)
   {
     Dictionary<string, MServerObject> Results = null;
     try
     {
       Results = JsonConvert.DeserializeObject<Dictionary<string, MServerObject>>(m.Payload);
     }
     catch (Exception e)
     {
       MNetMessage mr = new MNetMessage(1, "SERVER", MNetMessage.MERGE, "FAILED: " + e.Message);
       Send(c, "Message", mr.Serialize());
     }
     finally
     {
       if (Results != null)
       {
         foreach (KeyValuePair<string, MServerObject> kv in Results)
         {
           AddToWorld(kv.Value);
         }
       }
       MNetMessage mr = new MNetMessage(1, "SERVER", MNetMessage.MERGE, "SUCCESS");
       Send(c, "Message", mr.Serialize());
     }
   }
   */

    public void ConnectToMASSIVE(MClient c, MNetMessage m)
    {
      c.State = MClient.STATE_CONNECTOWORLD;
      MNetMessage mr = new MNetMessage();
      mr.Command = MNetMessage.CONNECTTOMASSIVE;
      mr.UserID = c.Account.UserID;
      Send(c, "Message", mr.Serialize());

      ClientConnected?.Invoke(this, new ServerEvent("Connected to MASSIVE:" + c.ToString()));
    }

    //log in registered player
    //player sends updatedetails message to register
    public void LoginRequest(MClient c, MNetMessage m)
    {
      if (MassiveConnections.Count >= MAXCONNECTIONS)
      {
        DisconnectClient(c.connection, "Maximum Connections exceded");
        return;
      }

      MLoginMessageRequest mlir = MLoginMessageRequest.Deserialize<MLoginMessageRequest>(m.Payload);
      //check database
      //if password matches, continue
      //if not, send failed message
      IPEndPoint ipe = (IPEndPoint)c.connection.ConnectionInfo.RemoteEndPoint;
      string s = ipe.Address + ":" + ipe.Port;
      c.Account.ClientIP = s;
      if (c.Account.UserID == null)
      {
        c.Account.UserID = m.UserID;
      }
      _DataBase.UpdatePlayerIP(c.Account);
      _DataBase.UpdatePlayerUsage(c.Account);

      MUserAccount mu = _DataBase.GetPlayerByEmail(mlir.Email, mlir.Password);
      if (mu == null)
      {
        MNetMessage mli = new MNetMessage();
        mli.Command = MNetMessage.ERROR;
        mli.Payload = "User not found. Building Disabled. Register User and Login again.";
        Send(c, "Message", mli.Serialize());
      }
      else
      {
        MNetMessage mli = new MNetMessage();
        mli.Command = MNetMessage.LOGIN;
        mli.UserID = mu.UserID;
        mli.Payload = mu.Serialize();
        Send(c, "Message", mli.Serialize());
        c.Account = mu;
      }

      ClientLoggedIn?.Invoke(this, new ServerEvent("Logged In:" + c.ToString()));
    }

    public void SetTexture(MClient c, MNetMessage m)
    {
      MTextureMessage mt = MTextureMessage.Deserialize<MTextureMessage>(m.Payload);


      bool Success = _DataBase.SetTexture(m.UserID, mt.InstanceID, mt.TextureID);
      //bool Success = CurrentUniverse.SetTexture(m.UserID, mt.InstanceID, mt.TextureID);

      MNetMessage mr = new MNetMessage();
      if (Success == true)
      {
        mr.Command = MNetMessage.TEXTURE;
        mr.Payload = mt.Serialize();
        SendToAllClients(c, mr.Serialize());
      }
      else
      {
        mr.Command = MNetMessage.ERROR;
        mr.Payload = "ERROR UPDATING TEXTURE " + _DataBase.ResultText;
        Send(c, "Message", mr.Serialize());
      }
    }

    public void ChatMessage(MClient c, MNetMessage m)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.CHAT;

      MChatMessage chat = MChatMessage.Deserialize<MChatMessage>(m.Payload);
      //TODO: Check message integrity;
      if (chat.Message != null)
      {
        if (chat.Message.Length > 1024)
        {
          chat.Message = chat.Message.Substring(0, 1024);
        }
      }
      mn.Payload = chat.Serialize();
      SendToAllClients(c, mn.Serialize());
    }

    public void SpawnRequest(MClient c, MNetMessage m)
    {
      MSpawnMessage sm = MSpawnMessage.Deserialize<MSpawnMessage>(m.Payload);
      //List<MServerObject> spawns = JsonConvert.DeserializeObject<List<MServerObject>>(sm.Spawnables);
      foreach (DataRow dr in sm.SpawnTable.Rows)
      {
        if (string.IsNullOrEmpty((string)dr["instanceid"]))
        {
          dr[DB.INSTANCEID] = UidGen.GUID();
          dr[DB.DATECREATED] = DateTime.Now;
          dr[DB.DATEMODIFIED] = DateTime.Now;
        }
      }

      _DataBase.AddToWorld(sm.SpawnTable);

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.SPAWN;
      mn.Version = 1;
      mn.Payload = sm.Serialize();
      SendToAllClients(c, mn.Serialize());

      UniverseChanged?.Invoke(this, new ServerEvent("+1", 3));
    }

    public void ChangePosition(MClient c, MNetMessage m)
    {
      MPosMessage mp2 = MPosMessage.Deserialize<MPosMessage>(m.Payload);

      if (c.Account.UserID.Equals(mp2.InstanceID))
      {
        c.Account.CurrentPosition[0] = mp2.Position[0];
        c.Account.CurrentPosition[1] = mp2.Position[1];
        c.Account.CurrentPosition[2] = mp2.Position[2];
        //CurrentUniverse.MoveObject(c.Account.UserID, c.Account.UserID, mp2.Position, mp2.Rotation);
        CheckSync(c, mp2.Position[0], mp2.Position[1], mp2.Position[2]);
      }

      MNetMessage mn = new MNetMessage();
      bool success = _DataBase.MoveObject(mp2.UserID, mp2.InstanceID, mp2.Position, mp2.Rotation);
      if (success == true)
      {
        mn.Command = MNetMessage.POSITION;
        mn.Payload = mp2.Serialize();
        SendToAllClients(c, mn.Serialize());
      }
      else
      {
        mn.Command = MNetMessage.ERROR;
        mn.Payload = "ChangePosition:" + _DataBase.ResultText;
        Send(c, "Message", mn.Serialize());
      }
    }

    public void Teleport(MClient c, MNetMessage m)
    {
      MTeleportMessage tp = MTeleportMessage.Deserialize<MTeleportMessage>(m.Payload);

      m.Command = MNetMessage.TELEPORT;
      m.Payload = tp.Serialize();
      //CheckSync(c, tp.Locus[0], tp.Locus[1], tp.Locus[2]);

      c.Account.CurrentPosition[0] = tp.Locus[0];
      c.Account.CurrentPosition[1] = tp.Locus[1];
      c.Account.CurrentPosition[2] = tp.Locus[2];

      _DataBase.MoveObject(c.Account.UserID, tp.InstanceID, tp.Locus, tp.Rotation);
      //CurrentUniverse.MoveObject(c.Account.UserID, tp.InstanceID, tp.Locus, tp.Rotation);
      SendToAllClients(c, m.Serialize());

      /* MServerObject mso = DB.GetObject(c.Account.UserID);
       if (mso != null)
       {
         List<MServerObject> objects = new List<MServerObject>();
         objects.Add(mso);
         MSpawnMessage sm = new MSpawnMessage(objects);
         m.Command = MNetMessage.SPAWN;
         m.Payload = sm.Serialize();
         SpawnRequest(c, m);
       }
       SyncWorld(c);
       */

      //CurrentUniverse.WriteToDisk();
    }

    void CheckSync(MClient c, double x1, double y1, double z1)
    {
      double dist = c.Account.DistanceToLastSync(x1, y1, z1);
      if (dist > DistanceThreshold)
      {
        c.Account.LastSyncPosition[0] = x1;
        c.Account.LastSyncPosition[1] = y1;
        c.Account.LastSyncPosition[2] = z1;
        SyncWorld(c);
      }
      //Log("Distance from last sync:" + dist.ToString() + "/" + CurrentUniverse.DistanceThreshold, UTILITY);
    }

    public void SyncWorld(MClient c)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.SPAWN;
      mn.UserID = MNetMessage.SERVER;
      if (c.Account != null)
      {
        //List<MServerObject> items = DB.GetObjectsNear(c.Account.UserID, c.Account.CurrentPosition[0], c.Account.CurrentPosition[1], c.Account.CurrentPosition[2]);

        //if (items.Count > 0)
        {
          MSpawnMessage msm = new MSpawnMessage(_DataBase.GetObjectsNear(c.Account.UserID, c.Account.CurrentPosition[0], c.Account.CurrentPosition[1], c.Account.CurrentPosition[2]));
          mn.Payload = msm.Serialize();
          Send(c, "Message", mn.Serialize());
        }
      }
    }

    public void SyncZones(MClient c)
    {
      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.CREATEZONE;
      mn.UserID = MNetMessage.SERVER;

      mn.Payload = _ZoneHandler.GetObjectsAsString();

      Send(c, "Message", mn.Serialize());
    }

    public void GetTable(MClient c, MNetMessage m)
    {
      DataTable dt = _DataBase.GetTable(m.Payload);

      MTableMessage mtm = new MTableMessage(dt);

      MNetMessage mr = new MNetMessage();
      mr.Command = MNetMessage.GETTABLE;
      mr.Payload = mtm.Serialize();

      Send(c, "Message", mr.Serialize());
    }

    public void CreateZone(MClient c, MNetMessage m)
    {
      MServerZone zone = MServerZone.Deserialize<MServerZone>(m.Payload);

      MNetMessage mr = new MNetMessage();

      if (MRudeWords.IsRude(zone.Name) == true)
      {
        mr.Command = MNetMessage.ERROR;
        mr.Payload = "CreateZone:Not allowed";
        Send(c, "Message", mr.Serialize());
        return;
      }

      if (_ZoneHandler.Add(zone) == true)
      {
        mr.Command = MNetMessage.CREATEZONE;
        //TODO: Only send the zones we need
        mr.Payload = _ZoneHandler.GetObjectsAsString();
        SendToAllClients(c, mr.Serialize());
      }
      else
      {
        mr.Command = MNetMessage.ERROR;
        mr.Payload = "ZONE(" + zone.Name + ") already in Use";
        Send(c, "Message", mr.Serialize());
      }

      ZoneChanged?.Invoke(this, new ZoneEvent(zone));
    }


    public void DeleteZone(MClient c, MNetMessage m)
    {
      MServerZone zone = MServerZone.Deserialize<MServerZone>(m.Payload);

      MNetMessage mr = new MNetMessage();
      if (_ZoneHandler.Remove(c.Account.UserID, zone) == true)
      {
        mr.Command = MNetMessage.DELETEZONE;
        mr.Payload = zone.Serialize();

        SendToAllClients(c, mr.Serialize());
      }
      else
      {
        mr.Command = MNetMessage.ERROR;
        mr.Payload = "You can't delete " + zone.Name + " because you are not the owner";
        Send(c, "Message", mr.Serialize());
      }

      ZoneChanged?.Invoke(this, new ZoneEvent(zone));
    }

    public void UpdateZone(MClient c, MNetMessage m)
    {
      MServerZone zone = MServerZone.Deserialize<MServerZone>(m.Payload);
      MNetMessage mr = new MNetMessage();
      if (_ZoneHandler.Update(c.Account.UserID, zone))
      {
        mr.Command = MNetMessage.UPDATEZONE;
        mr.Payload = zone.Serialize();
        Send(c, "Message", mr.Serialize());
      }
      else
      {
        mr.Command = MNetMessage.ERROR;
        mr.Payload = "You can't delete " + zone.Name + " because you are not the owner";
        Send(c, "Message", mr.Serialize());
      }
      ZoneChanged?.Invoke(this, new ZoneEvent(zone));
    }

    public void RegisterUser(MClient c, MNetMessage m)
    {
      MUserAccount mu = MUserAccount.Deserialize<MUserAccount>(m.Payload);
      mu.CopyTo(c.Account);
      c.Account.ClientIP = c.Address.ToString();
      string UserID = _DataBase.RegisterUser(c.Account);

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.REGISTERUSER;
      mn.UserID = UserID;

      MChangeDetailsResult res = new MChangeDetailsResult();
      res.Success = true;
      res.Message = "User Registered";
      mn.Payload = res.Serialize();
      Send(c, "Message", mn.Serialize());
    }

    public void ChangeDetails(MClient c, MNetMessage m)
    {
      MUserAccount mu = MUserAccount.Deserialize<MUserAccount>(m.Payload);
      // TODO: Validate account
      mu.CopyTo(c.Account);
      // c.Save();
      c.Account.ClientIP = c.Address.ToString();

      string UserID = _DataBase.UpdatePlayer(c.Account);

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.CHANGEDETAILS;
      mn.UserID = UserID;

      //TODO: VALIDATE USERACCOUNT

      MChangeDetailsResult res = new MChangeDetailsResult();
      res.Success = true;
      res.Message = "Details Changed";
      mn.Payload = res.Serialize();
      Send(c, "Message", mn.Serialize());
    }

    public void ChangeProperty(MClient c, MNetMessage m)
    {
      MChangeProperty cp = MChangeProperty.Deserialize<MChangeProperty>(m.Payload);
      MNetMessage mn = new MNetMessage();

      if (_DataBase.SetProperty(c.Account.UserID, cp.InstanceID, cp.PropertyTag))
      {
        mn.Command = MNetMessage.CHANGEPROPERTY;
        mn.Payload = cp.Serialize();
        SendToAllClients(c, mn.Serialize());
      }
      else
      {
        mn.Command = MNetMessage.ERROR;
        mn.Payload = "ERROR updating Property. Please check syntax.";
        Send(c, "Message", mn.Serialize());
      }
    }

    void ChangeAvatar(MClient c, MNetMessage m)
    {
      MChangeAvatarRequest ca = MChangeAvatarRequest.Deserialize<MChangeAvatarRequest>(m.Payload);
      MNetMessage mn = new MNetMessage();

      if (_DataBase.ChangeAvatar(c.Account.UserID, ca.AvatarID))
      {
        mn.Command = MNetMessage.CHANGEAVATAR;
        mn.UserID = c.Account.UserID;
        MChangeAvatarResponse car = new MChangeAvatarResponse();
        car.AvatarID = ca.AvatarID;
        mn.Payload = car.Serialize();
        SendToAllClients(c, mn.Serialize());
      }
      else
      {
        mn.Command = MNetMessage.ERROR;
        mn.Payload = "NOT THE OWNER";
        Send(c, "Message", mn.Serialize());
      }
    }

    //adds a collection of objects to the world 
    //(rows should mimic the db properties)
    public bool AddToWorld(DataTable dt)
    {
      return _DataBase.AddToWorld(dt);
    }

    public int GetObjectCount()
    {
      return _DataBase.GetObjectCount();
    }

    public void DeleteObject(MClient c, MNetMessage m)
    {
      MDeleteMessage mp = MDeleteMessage.Deserialize<MDeleteMessage>(m.Payload);

      _DataBase.DeleteObject(mp.InstanceID, c.Account.UserID);

      c.Account.TotalObjects -= _DataBase.DeleteObject(mp.InstanceID, c.Account.UserID);
      if (c.Account.TotalObjects < 0)
      {
        c.Account.TotalObjects = 0;
      }

      MNetMessage mn = new MNetMessage();
      mn.Command = MNetMessage.DELETE;
      mn.Payload = mp.Serialize();

      SendToAllClients(c, mn.Serialize());

      UniverseChanged?.Invoke(this, new ServerEvent("-1", 3));
    }

    public void SendToAllClientsInProximity(MClient client, string sMessage)
    {
      SendToAllClients(client, sMessage);
    }

    /**
     * TODO: Send to all clients within range
     * 
     * */
    public void SendToAllClients(MClient client, string sMessage)
    {
      List<ConnectionInfo> otherConnectionInfos = GetConnections();
      Log(sMessage, OUTGOINGBROADCAST);

      //NetworkCommsDotNet.Connections.TCP.TCPConnection.GetConnection(clientInfo).SendObject("Message", sMessage);
      /// foreach (ConnectionInfo info in otherConnectionInfos)
      //{
      ///  TCPConnection.GetConnection(info).SendObject("Message", sMessage);
      // }

      foreach (MClient c in MassiveConnections)
      {
        //if (c.Address.ToString() == this.ServerIPAddress) continue; //don't send to self
        if (client != null)
        {
          //double dist = c.Account.Distance(client.Account.CurrentPosition[0], client.Account.CurrentPosition[1], client.Account.CurrentPosition[2]);
        }

        //ConnectionInfo clientInfo = new ConnectionInfo(new IPEndPoint(c.Address, c.Port));
        c.connection.SendObject("Message", sMessage);
        //NetworkCommsDotNet.Connections.TCP.TCPConnection.GetConnection(clientInfo).SendObject("Message", sMessage);
      }
    }

    public void Send(MClient c, string sType, string sMessage)
    {
      //ConnectionInfo clientInfo = new ConnectionInfo(new IPEndPoint(c.Address, c.Port));
      //NetworkCommsDotNet.Connections.TCP.TCPConnection.GetConnection(clientInfo).SendObject(sType, sMessage);
      c.connection.SendObject(sType, sMessage);
      Log(sMessage, OUTGOING);
    }

    public ConnectionInfo GetServerDetails(ApplicationLayerProtocolStatus applicationLayerProtocol = ApplicationLayerProtocolStatus.Enabled)
    {
      string userEnteredStr = ServerIPAddress + ":" + ServerPort;

      try
      {
        lastServerIPEndPoint = IPTools.ParseEndPointFromString(userEnteredStr);
      }
      catch (Exception)
      {
        Log("Unable to determine host IP address and port. Check format and try again:", UTILITY);
      }

      return new ConnectionInfo(lastServerIPEndPoint, applicationLayerProtocol);
    }

    public List<ConnectionInfo> GetConnections()
    {
      List<ConnectionInfo> otherConnectionInfos = new List<ConnectionInfo>();
      ConnectionInfo serverConnectionInfo = null;
      if (ServerIPAddress != "")
      {
        try { serverConnectionInfo = new ConnectionInfo(ServerIPAddress, ServerPort); }
        catch (Exception)
        {
          // ShowMessage("Failed to parse the server IP and port. Please ensure it is correct and try again");
          return otherConnectionInfos;
        }

        if (serverConnectionInfo != null)
          otherConnectionInfos = (from current in NetworkComms.AllConnectionInfo() where current.RemoteEndPoint != serverConnectionInfo.RemoteEndPoint select current).ToList();
        else
          otherConnectionInfos = NetworkComms.AllConnectionInfo();
      }
      return otherConnectionInfos;
    }

    /*
     * This may work better for more complex setups, VMS etc
     *string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }

      Get the local IP address that you see in the thernet properties for IP4
    */
    public static string GetLocalIPAddress()
    {
      var host = Dns.GetHostEntry(Dns.GetHostName());
      foreach (var ip in host.AddressList)
      {
        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
          return ip.ToString();
        }
      }
      throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public void Close()
    {
      _DataBase.Disconnect();
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
      FileStream stream = null;

      try
      {
        stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
      }
      catch (IOException)
      {
        //the file is unavailable because it is:
        //still being written to
        //or being processed by another thread
        //or does not exist (has already been processed)
        return true;
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }

      //file is not locked
      return false;
    }

    public void Log(string s, int ColorCode)
    {
      if (ColorCode > 2)
      {
        Logger.WriteLog(s);
      }

      if (DisplayEvents == true)
      {
        ServerInfo?.Invoke(this, new ServerEvent(s, ColorCode));
      }
    }
  }
}
