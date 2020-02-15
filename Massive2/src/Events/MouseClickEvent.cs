using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class MouseClickEvent : EventArgs
  {
    public Vector3d Position;
    public int Button;
    public MouseClickEvent(Vector3d inPos, int inButton)
    {
      Position= inPos;
    }
  }
}
