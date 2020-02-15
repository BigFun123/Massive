using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive2.Events
{
  public class PositionEvent : EventArgs
  {
    public string InstanceID;
    public Vector3d Position;
    public Quaterniond Rotation;

    public PositionEvent(string inInstanceID, Vector3d pos, Quaterniond rot)
    {
      InstanceID = inInstanceID;
      Position = pos;
      Rotation = rot;
    }
  }
}
