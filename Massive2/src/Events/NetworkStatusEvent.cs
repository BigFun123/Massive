using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class NetworkStatusEvent
  {
    public bool Failure;
    public bool Connected;
    public string Message;
    public NetworkStatusEvent(bool inConnected, bool Failed, string inMessage)
    {
      Connected = inConnected;
      Message = inMessage;
      Failure = Failed;
    }
  }
}
