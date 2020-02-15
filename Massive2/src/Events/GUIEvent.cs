using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class GUIEvent : EventArgs
  {
    public MControl Control;
    public GUIEvent(MControl inControl)
    {
      Control = inControl;
    }
  }
}
