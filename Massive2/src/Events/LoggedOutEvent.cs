using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class LoggedOutEvent
  {
    public string UserID;

    public LoggedOutEvent(string inUserID)
    {
      UserID = inUserID;
    }
  }
}
