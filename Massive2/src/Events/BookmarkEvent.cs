using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Events
{
  public class BookmarkEvent : EventArgs
  {
    public string Name;
    public Vector3d Position;
    public Quaterniond Rotation;    
    public BookmarkEvent(string sName, Vector3d inPosition, Quaterniond inRot)
    {
      Name = sName;
      Position = inPosition;
      Rotation = inRot;
    }
  }
}
