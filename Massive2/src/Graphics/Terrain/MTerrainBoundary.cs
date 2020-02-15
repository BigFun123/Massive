using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MTerrainBoundary
  {
    public Vector3d LonLatTL;
    public Vector3d LonLatTR;
    public Vector3d LonLatBL;
    public Vector3d LonLatBR;

    public Vector3d TL;
    public Vector3d TR;
    public Vector3d BL;
    public Vector3d BR;

    public override string ToString()
    {
      return LonLatTL + "," + LonLatTR + " ~ " + LonLatBL + "," + LonLatBR + "\n(" + TL.ToString() + "|" + TR.ToString() + "|" + BL.ToString() + "|" + BR.ToString() + ")";
    }
  }
}
