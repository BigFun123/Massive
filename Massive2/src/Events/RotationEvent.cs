using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class RotationEvent
  {
    public string InstanceID;
    public Quaterniond Rotation;

    public RotationEvent(string instanceID, Quaterniond rot)
    {
      InstanceID = instanceID;
      Rotation= rot;
    }
  }
}
