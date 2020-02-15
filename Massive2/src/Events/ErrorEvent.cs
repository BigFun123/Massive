using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ErrorEvent : EventArgs
  {
    public string ErrorMessage;
    public ErrorEvent(string sMessage)
    {
      ErrorMessage = sMessage;
    }
  }
}
