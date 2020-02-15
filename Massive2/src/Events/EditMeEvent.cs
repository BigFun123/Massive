using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class EditMeEvent : EventArgs
  {
    public MObject Target;
    public EditMeEvent(MObject _Target)
    {
      Target = _Target;
    }
  }
}
