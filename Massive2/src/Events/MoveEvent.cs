using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class MoveEvent : EventArgs
  {
    public string InstanceID;
    public Vector3d Position;
    public Quaterniond Rotation;
    public bool Relative = false;
    public MoveEvent(string sInstanceID, Vector3d inPosition, Quaterniond inRot, bool isRelative=false)
    {
      InstanceID = sInstanceID;
      Position = inPosition;
      Rotation = inRot;
      Relative = isRelative;
    }
  }
}
