using Massive.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ChatEvent : EventArgs
  {
    public MChatMessage message;
    public const string TYPESERVER = "SERVER";
    public const string TYPEADMIN = "ADMIN";
    public const string TYPEFRIEND = "FRIEND";
    public const string TYPERANDOM = "RANDOM";

    public ChatEvent(MChatMessage m)
    {
      message = m;
    }
  }
}
