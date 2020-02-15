using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class TeleportRequestHandler
  {    
    public Vector3d Position;
    public Quaterniond Rotation;

    public TeleportRequestHandler(Vector3d pos, Quaterniond rot)
    {   
      Position = pos;
      Rotation = rot;
    }
  }
}
