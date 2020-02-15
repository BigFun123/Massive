using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive2.src.Graphics
{
  public class MParticle
  {
    public Vector3 pos, speed;
    char r, g, b, a;
    float size, angle, weight;
    public float life = 5;
    float cameradistance;
    
    public static bool operator <(MParticle p1, MParticle that)
    {
      // Sort in reverse order : far particles drawn first.
      return p1.cameradistance > that.cameradistance;
    }
    public static bool operator >(MParticle p1, MParticle that)
    {
      // Sort in reverse order : far particles drawn first.
      return p1.cameradistance < that.cameradistance;
    }

  }
}
