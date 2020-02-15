using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class RotationRequestEvent
  {
    public Quaterniond Rotation;    
    public RotationRequestEvent(Quaterniond inRot)
    {
      Rotation = inRot;
    }
  }
}
