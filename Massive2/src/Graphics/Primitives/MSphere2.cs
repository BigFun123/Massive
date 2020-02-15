using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MSphere2 : MSceneObject
  {
    public MSphere2(string inname) :
      base(EType.PrimitiveSphere, inname)
    {

    }

    public override void Setup()
    {

    }

    void push_indices(List<int> indices, int sectors, int r, int s)
    {
      int curRow = r * sectors;
      int nextRow = (r + 1) * sectors;

      indices.Add(curRow + s);
      indices.Add(nextRow + s);
      indices.Add(nextRow + (s + 1));

      indices.Add(curRow + s);
      indices.Add(nextRow + (s + 1));
      indices.Add(curRow + (s + 1));
    }

    void createSphere(List<Vector3d> vertices, List<int> indices, List<Vector2> texcoords,
                 float radius, int rings, int sectors)
    {
      float R = 1.0f / (float)(rings - 1);
      float S = 1.0f / (float)(sectors - 1);
      double M_PI_2 = Math.PI / 2.0;

      for (int r = 0; r < rings; ++r)
      {
        for (int s = 0; s < sectors; ++s)
        {
          double y = Math.Sin(-M_PI_2 + Math.PI * r * R);
          double x = Math.Cos(2 * Math.PI * s * S) * Math.Sin(Math.PI * r * R);
          double z = Math.Sin(2 * Math.PI * s * S) * Math.Sin(Math.PI * r * R);

          texcoords.Add(new Vector2(s * S, r * R));
          vertices.Add(new Vector3d(x, y, z) * radius);
          push_indices(indices, sectors, r, s);
        }
      }
    }
  }
}
