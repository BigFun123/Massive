using OpenTK;
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

    public RectD(double x1, double y1, double x2, double y2)
    {
      Min = new Vector2d(x1, y1);      
      Size = new Vector2d(x2, y2);
      Max = Min + Size;
      Center = new Vector2d(Min.X + Size.X / 2, Min.X + Size.Y / 2);
      Size = new Vector2d(Math.Abs(Size.X), Math.Abs(Size.Y));
    }

    public RectD(Vector2d min, Vector2d size)
    {
      Min = min;
      Max = min + size;
      Center = new Vector2d(Min.X + size.X / 2, Min.X + size.Y / 2);
      Size = new Vector2d(Math.Abs(size.X), Math.Abs(size.Y));
    }

    public bool Contains(Vector2d point)
    {
      bool flag = Size.X < 0.0 && point.X <= Min.X && point.X > (Min.X + Size.X) || Size.X >= 0.0 && point.X >= Min.X && point.X < (Min.X + Size.X);
      return flag && (Size.Y < 0.0 && point.Y <= Min.Y && point.Y > (Min.Y + Size.Y) || Size.Y >= 0.0 && point.Y >= Min.Y && point.Y < (Min.Y + Size.Y));
    }
  }
}
