using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class Vector3d_Server
  {
    public double X, Y, Z;

    public Vector3d_Server(double ix, double iy, double iz)
    {
      X = ix;
      Y = iy;
      Z = iz;
    }

    public override string ToString()
    {
      return X.ToString() + "," + Y.ToString() + "," + Z.ToString();
    }
  }
}
