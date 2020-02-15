using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaderPlay
{
  public class MCamera
  {
    public Matrix4 Projection;
    public Matrix4 Position;
    float fov = 90;

    public void Setup()
    {
      Projection = Matrix4.CreatePerspectiveFieldOfView(((float)Math.PI / 180.0f) * (fov), 500.0f / 600.0f, 0.1f, 100.0f);
      Position = Matrix4.CreateTranslation(new Vector3(0, 0, -2.2f));
    }

    public Matrix4 GetMatrix()
    {
      return Projection;
    }

    public Matrix4 GetView()
    {
      return Position;
    }


  }
}
