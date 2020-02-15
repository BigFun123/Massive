using MassiveGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public struct RectD
  {
    public Vector2d Min { get; private set; }
    public Vector2d Max { get; private set; }
    //size is absolute width&height so Min+size != max
    public Vector2d Size { get; private set; }
    public Vector2d Center { get; private set; }

    public RectD(Vector2d min, Vector2d size)
    {
      Min = min;
      Max = min + size;
      Center = new Vector2d(Min.x + size.x / 2, Min.x + size.y / 2);
      Size = new Vector2d(Math.Abs(size.x), Math.Abs(size.y));
    }

    public bool Contains(Vector2d point)
    {
      bool flag = Size.x < 0.0 && point.x <= Min.x && point.x > (Min.x + Size.x) || Size.x >= 0.0 && point.x >= Min.x && point.x < (Min.x + Size.x);
      return flag && (Size.y < 0.0 && point.y <= Min.y && point.y > (Min.y + Size.y) || Size.y >= 0.0 && point.y >= Min.y && point.y < (Min.y + Size.y));
    }
  }
}
