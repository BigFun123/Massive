using Massive;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive2.Graphics.Character
{
  public class MBone
  {
    public string sName;
    public Matrix4 offsetmatrix;
    public Matrix4 invmatrix;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scaling;

    public void Calc()
    {
      Position = offsetmatrix.ExtractTranslation();
      Rotation = offsetmatrix.ExtractRotation();
      Scaling = offsetmatrix.ExtractScale();
    }
  }
}
