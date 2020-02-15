using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class ClickEvent : EventArgs
  {
    public MObject Target;
    public ClickEvent(MObject mo )
    {
      Target = mo;
    }
  }
}
