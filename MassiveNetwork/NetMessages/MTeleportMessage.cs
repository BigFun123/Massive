using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MTeleportMessage : IMSerializable
  {
    public string InstanceID = "";
    public double[] Locus = new double[3] { 0, 0, 0 };
    public double[] Rotation = new double[4] { 0, 0, 0, 1 };

    public MTeleportMessage(string inInstsanceID, 
      double x, double y, double z,
      double rx, double ry, double rz, double rw)
    {
      Locus[0] = x;
      Locus[1] = y;
      Locus[2] = z;
      Rotation[0] = rx;
      Rotation[1] = ry;
      Rotation[2] = rz;
      Rotation[3] = rw;
      InstanceID = inInstsanceID;
    }
  }
}
