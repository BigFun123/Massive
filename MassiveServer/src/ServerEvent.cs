using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Server
{
  public class ServerEvent : EventArgs
  {
    public MClient Client;
    public string Message;
    public int ColorCode;

    public ServerEvent(string iMessage, int inColorCode = 0)
    {
      Message = iMessage;
      ColorCode = inColorCode;
    }
  }
}
