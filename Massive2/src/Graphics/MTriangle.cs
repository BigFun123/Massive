using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MTriangle : MSceneObject
  {

    TexturedVertex[] Vertices;

    public MTriangle()
      : base(EType.PrimitiveTriangle, "Triangle")
    {
      
    }

    public override void Setup()
    {
      Vertices = new TexturedVertex[] {
                new TexturedVertex( new Vector3(0f, 0f, 0f), new Vector3(0, 1, 0), new Vector2(0,0f)),
                new TexturedVertex( new Vector3(0, 1f, 0f),  new Vector3(0, 1, 0), new Vector2(0f,1f) ),
                new TexturedVertex( new Vector3(1f, 1f, 0f), new Vector3(0, 1, 0), new Vector2(1f,1f))};

      base.Setup();
    }

  }
}
