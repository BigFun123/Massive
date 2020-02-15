using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MChatMessage : IMSerializable
  {
    public string Message;
    public string OwnerID;
    public string OwnerName;
    public string TargetID; // * for broadcast e.g. server message
    public string MessageType;
    public int MessageID;
    public int Flags = 0;
    public const string TYPEADMIN = "ADMIN";
    public const string TYPEFRIEND = "FRIEND";
    public const string TYPERANDOM = "RANDOM";
  }
}
