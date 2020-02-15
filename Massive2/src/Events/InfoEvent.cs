using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class InfoEvent : EventArgs
  {
    public object Data;
    public string Message;
    public InfoEvent(string sMessage, object in_data = null)
    {
      Message = sMessage;
      Data = in_data;
    }
  }
}
