using Newtonsoft.Json;

namespace Massive.Network
{
  public class MNetMessage : IMSerializable
  {   

    public const string CONNECTTOMASSIVEREQ = "CONNECTTOMASSIVEREQ";
    public const string CONNECTTOMASSIVE = "CONNECTTOMASSIVE";

    public const string READY = "READY";
    public const string UNKNOWN = "UNKNOWN";
    public const string ERROR = "ERROR";


    public const string SPAWN = "SPAWN";
    public const string SPAWNREQUEST = "SPAWNREQ";

    public const string CREATEZONE = "CREATEZONE";
    public const string CREATEZONEREQ = "CREATEZONEREQ";

    public const string DELETEZONE = "DELETEZONE";
    public const string DELETEZONEREQ = "DELETEZONEREQ";

    public const string UPDATEZONE = "UPDATEZONE";
    public const string UPDATEZONEREQ = "UPDATEZONEREQ";

    public const string DELETEREQUEST = "DELETEREQ";
    public const string DELETE = "DELETE";

    public const string LOGINREQ = "LOGINREQ";
    public const string LOGIN = "LOGIN";
    public const string LOGGEDOUT = "LOGGEDOUT"; //global

    public const string TELEPORTREQ = "TPREQ";
    public const string TELEPORT = "TP";

    public const string POSITIONREQ = "POSREQ";
    public const string POSITION = "POS";

    public const string TEXTUREREQ = "TEXREQ";
    public const string TEXTURE = "TEX";

    public const string POSROTATE = "PSR";

    public const string GETWORLD = "GETWORLD";
    public const string GETZONES = "GETZONES";
    public const string GETTABLEREQ = "GETTABLEREQ";
    public const string GETTABLE = "GETTABLE";


    public const string CHATREQ = "CHATREQ";
    public const string CHAT = "CHAT";

    public const string INFODUMPREQ = "INFODUMPREQ";
    public const string INFODUMP = "INFODUMP";
    public const string MERGEREQ = "MERGEREQ";
    public const string MERGE = "MERGE";

    public const string REGISTERUSERREQ = "REGISTERUSERREQ";
    public const string REGISTERUSER = "REGISTERUSER";

    public const string CHANGEDETAILSREQ = "CHANGEDETAILSREQ";
    public const string CHANGEDETAILS = "CHANGEDETAILS";

    public const string CHANGEPROPERTYREQ = "CHANGEPROPERTYREQ";
    public const string CHANGEPROPERTY = "CHANGEPROPERTY";
    
    public const string CHANGEAVATARREQUEST = "CHANGEAVATARREQUEST";
    public const string CHANGEAVATAR = "CHANGEAVATAR";

    public const string MAXCONNECTIONS = "MAXCONNECTIONS";

    public const string SERVER = "SERVER";
    public const int STATIC = 1;
    public const int DYNAMIC = 0;

    public int Version = 1;
    public string UserID;
    //public string InstanceID; //id to set object to on client side
    public string Command;
    public string Payload;
    //public int StaticStorage = 1; //when true, spawned object goes into static pool, when false, dynamic (ephemerial) pool that is deleted when user logs off    

    public MNetMessage()
    {

    }

    public MNetMessage(string sPacket)
    {
      Deserialize(sPacket);
    }

    public MNetMessage(int iVersion, string iUserID, string iCommand, string iPayload)
    {
      Version = iVersion;
      UserID = iUserID;
      Command = iCommand;
      Payload = iPayload;
    }

    public static MNetMessage Deserialize(string sPacket)
    {
      return JsonConvert.DeserializeObject<MNetMessage>(sPacket);    
    }

    public new virtual string Serialize()
    {
      return JsonConvert.SerializeObject(this);      
    }
  }
}