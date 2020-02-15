using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MBoundingBox
  {
    public Vector3d P1;
    public Vector3d P2;
    public override string ToString()
    {
      return P1.ToString() + "-" + P2.ToString();
    }

    public Vector3d Size()
    {
      return P2 - P1;
    }

    public void Expand(Vector3d e)
    {
      if (e.X < P1.X) P1.X = e.X;
      if (e.Y < P1.Y) P1.X = e.Y;
      if (e.Z < P1.Z) P1.Z = e.Z;

      if (e.X > P2.X) P2.X = e.X;
      if (e.Y > P2.Y) P2.Y = e.Y;
      if (e.Z > P2.Z) P2.Z = e.Z;
    }
  }
}
