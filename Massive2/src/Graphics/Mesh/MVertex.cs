using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public struct MVertex
  {
    public Vector3 Position;
    public Vector3 Normal;
    public Vector2 TexCoords;
    public Vector3 Tangent;
    public Vector3 BiTangent;

    public const int Size = (3 + 3 + 2 +3 +3) * 4; // size of struct in bytes
  }
}
