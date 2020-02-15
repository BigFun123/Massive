using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class StatusEvent : EventArgs
  {
    public bool Succeded = true;
    public string Message = "";
    public StatusEvent(bool bSucceded, string sMessage="")
    {
      Succeded = bSucceded;
      Message = sMessage;
    }
  }
}
