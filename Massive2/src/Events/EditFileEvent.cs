using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class EditFileEvent : EventArgs
  {
    public string Target;
    public EditFileEvent(string _Target)
    {
      Target = _Target;
    }
  }
}
