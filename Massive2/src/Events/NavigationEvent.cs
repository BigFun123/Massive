using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class NavigationEvent : EventArgs
  {
    public Vector3d Target;
    public NavigationEvent(Vector3d inTarget)
    {
      Target = inTarget;
    }
  }
}
