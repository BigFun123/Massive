using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MGravityIndicator : MSphere
  {
    public MGravityIndicator():base("GravityIndicator", 2)
    {
      this.transform.Scale = new OpenTK.Vector3d(1.1, 1.1, 1.1);
    }

    public override void Update()
    {
      this.transform.Position = Globals.Avatar.GetPosition() + Globals.LocalGravity;
      base.Update();
    }
  }
}
