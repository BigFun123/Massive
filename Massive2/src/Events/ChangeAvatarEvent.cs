using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ChangeAvatarEvent : EventArgs
  {
    public string UserID;
    public string TemplateID;
    
    public ChangeAvatarEvent(string sUserID, string sTemplateID)
    {
      UserID = sUserID;
      TemplateID = sTemplateID;
    }
  }
}
