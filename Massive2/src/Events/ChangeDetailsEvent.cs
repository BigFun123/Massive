using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ChangeDetailsEvent : EventArgs
  {
    public bool Success;
    public string Message;

    public ChangeDetailsEvent(bool bSuccess, string sMessage)
    {
      Success = bSuccess;
      Message = sMessage;
    }
  }
}
