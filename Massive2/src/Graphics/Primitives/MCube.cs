using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Massive
{
  public class MCube : MSceneObject
  {
    public MCube(string inname)
      : base(EType.PrimitiveCube, inname)
    {

    }

    public override void Setup()
    {
      //float[] vertices = {
      float w = 0.5f;
      Vertices = new TexturedVertex[] { 
            // back face


            new TexturedVertex(new Vector3(-w, -w, -w), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(0.0f, 0.0f)), // bottom-left
            new TexturedVertex(new Vector3( w,  w, -w), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2( 1.0f, 1.0f)),
            new TexturedVertex(new Vector3( w, -w, -w), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(1.0f, 0.0f)),
            new TexturedVertex(new Vector3( w,  w, -w), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(1.0f, 1.0f)),
            new TexturedVertex(new Vector3( -w, -w, -w), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(0.0f, 0.0f)),
            new TexturedVertex(new Vector3( -w,  w, -w), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2( 0.0f, 1.0f )),

            new TexturedVertex(new Vector3( -w, -w,  w), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 0.0f, 0.0f )),
            new TexturedVertex(new Vector3(  w, -w,  w), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 1.0f, 0.0f )),
            new TexturedVertex(new Vector3(  w,  w,  w), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 1.0f, 1.0f )),
            new TexturedVertex(new Vector3(  w, w,  w), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 1.0f, 1.0f )),
            new TexturedVertex(new Vector3( -w,w,  w), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 0.0f, 1.0f )),
            new TexturedVertex(new Vector3( -w, -w,  w), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 0.0f, 0.0f )),

            new TexturedVertex(new Vector3( -w, w, w ), new Vector3( -1.0f,  0.0f,  0.0f ), new Vector2( 1.0f, 0.0f )),
            new TexturedVertex(new Vector3( -w,  w, -w ), new Vector3( -1.0f,  0.0f,  0.0f ), new Vector2( 0.0f, 0.0f )),
            new TexturedVertex(new Vector3( -w, -w, w), new Vector3( -1.0f,  0.0f,  0.0f ), new Vector2( 1.0f, 1.0f )),
            new TexturedVertex(new Vector3( -w, w,  -w), new Vector3( -1.0f,  0.0f,  0.0f), new Vector2(   0.0f, 0.0f)),
            new TexturedVertex(new Vector3( -w,-w,  -w), new Vector3( -1.0f,  0.0f,  0.0f), new Vector2(   0.0f, 1.0f)),
            new TexturedVertex(new Vector3( -w, -w,  w), new Vector3( -1.0f,  0.0f,  0.0f), new Vector2(   1.0f, 1.0f)),

            new TexturedVertex(new Vector3( w,  w, -w), new Vector3( 1.0f,  0.0f,  0.0f), new Vector2(   1.0f, 0.0f)),
            new TexturedVertex(new Vector3( w, w, w), new Vector3(  1.0f,  0.0f,  0.0f), new Vector2(  0.0f, 0.0f)),
            new TexturedVertex(new Vector3( w,  -w, w), new Vector3(  1.0f,  0.0f,  0.0f), new Vector2(  0.0f, 1.0f)),

            new TexturedVertex(new Vector3( w,  w, -w), new Vector3(  1.0f,  0.0f,  0.0f), new Vector2(  1.0f, 0.0f)),
            new TexturedVertex(new Vector3( w, -w, w), new Vector3( 1.0f,  0.0f,  0.0f), new Vector2(  0.0f, 1.0f)),
            new TexturedVertex(new Vector3( w, -w, -w), new Vector3( 1.0f,  0.0f,  0.0f), new Vector2( 1.0f, 1.0f)),

            new TexturedVertex(new Vector3( -w, -w, -w), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2( 0.0f, 1.0f)),
            new TexturedVertex(new Vector3(  w, -w, -w), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2( 1.0f, 1.0f)),
            new TexturedVertex(new Vector3( w,-w,  w), new Vector3(  0.0f, -1.0f,  0.0f), new Vector2( 1.0f, 0.0f)),
            new TexturedVertex(new Vector3( w, -w,  w), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2(1.0f, 0.0f)),
            new TexturedVertex(new Vector3(-w, -w,  w), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2(0.0f, 0.0f)),
            new TexturedVertex(new Vector3(-w,-w, -w), new Vector3(0.0f, -1.0f,  0.0f), new Vector2(0.0f, 1.0f)),

            new TexturedVertex(new Vector3(-w,  w, -w), new Vector3(0.0f,  1.0f,  0.0f), new Vector2(1.0f, 1.0f)),
            new TexturedVertex(new Vector3( w, w , w), new Vector3( 0.0f,  1.0f,  0.0f), new Vector2(0.0f, 0.0f)),
            new TexturedVertex(new Vector3( w,  w, -w), new Vector3( 0.0f,  1.0f,  0.0f), new Vector2(0.0f, 1.0f)),
            new TexturedVertex(new Vector3( w,  w,  w), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 0.0f, 0.0f)),
            new TexturedVertex(new Vector3(-w,  w, -w), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 1.0f, 1.0f)),
            new TexturedVertex(new Vector3(-w,  w,  w), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 1.0f, 0.0f))
            /*new TexturedVertex(new Vector3(-1.0f, -1.0f, -1.0f), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(0.0f, 0.0f)), // bottom-left
            new TexturedVertex(new Vector3( 1.0f,  1.0f, -1.0f), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2( 1.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f, -1.0f, -1.0f), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(1.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f, -1.0f), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(1.0f, 1.0f)),
            new TexturedVertex(new Vector3( -1.0f, -1.0f, -1.0f), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2(0.0f, 0.0f)),

            new TexturedVertex(new Vector3( -1.0f,  1.0f, -1.0f), new Vector3( 0.0f,  0.0f, -1.0f), new Vector2( 0.0f, 1.0f )),
            new TexturedVertex(new Vector3( -1.0f, -1.0f,  1.0f), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 0.0f, 0.0f )),
            new TexturedVertex(new Vector3(  1.0f, -1.0f,  1.0f), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 1.0f, 0.0f )),
            new TexturedVertex(new Vector3(  1.0f,  1.0f,  1.0f), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 1.0f, 1.0f )),
            new TexturedVertex(new Vector3(  1.0f,  1.0f,  1.0f), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 1.0f, 1.0f )),
            new TexturedVertex(new Vector3( -1.0f,  1.0f,  1.0f), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 0.0f, 1.0f )),
            new TexturedVertex(new Vector3( -1.0f, -1.0f,  1.0f), new Vector3( 0.0f,  0.0f,  1.0f), new Vector2( 0.0f, 0.0f )),

            new TexturedVertex(new Vector3( -1.0f,  1.0f, 1.0f ), new Vector3( -1.0f,  0.0f,  0.0f ), new Vector2( 1.0f, 0.0f )),
            new TexturedVertex(new Vector3( -1.0f,  1.0f, -1.0f ), new Vector3( -1.0f,  0.0f,  0.0f ), new Vector2( 0.0f, 0.0f )),
            new TexturedVertex(new Vector3( -1.0f,  -1.0f,  1.0f ), new Vector3( -1.0f,  0.0f,  0.0f ), new Vector2( 1.0f, 1.0f )),
            new TexturedVertex(new Vector3( -1.0f, 1.0f, -1.0f), new Vector3( -1.0f,  0.0f,  0.0f), new Vector2(   0.0f, 0.0f)),
            new TexturedVertex(new Vector3( -1.0f, -1.0f,  -1.0f), new Vector3( -1.0f,  0.0f,  0.0f), new Vector2(   0.0f, 1.0f)),
            new TexturedVertex(new Vector3( -1.0f,  -1.0f,  1.0f), new Vector3( -1.0f,  0.0f,  0.0f), new Vector2(   1.0f, 1.0f)),

            new TexturedVertex(new Vector3( 1.0f,  1.0f,  -1.0f), new Vector3( 1.0f,  0.0f,  0.0f), new Vector2(   1.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f, 1.0f), new Vector3(  1.0f,  0.0f,  0.0f), new Vector2(  0.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f,  -1.0f, 1.0f), new Vector3(  1.0f,  0.0f,  0.0f), new Vector2(  0.0f, 1.0f)),

            new TexturedVertex(new Vector3( 1.0f,  1.0f, -1.0f), new Vector3(  1.0f,  0.0f,  0.0f), new Vector2(  1.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f,  -1.0f, 1.0f), new Vector3( 1.0f,  0.0f,  0.0f), new Vector2(  0.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f,  -1.0f, -1.0f), new Vector3( 1.0f,  0.0f,  0.0f), new Vector2( 1.0f, 1.0f)),

            new TexturedVertex(new Vector3( -1.0f, -1.0f, -1.0f), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2( 0.0f, 1.0f)),
            new TexturedVertex(new Vector3(  1.0f, -1.0f, -1.0f), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2( 1.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f, -1.0f,  1.0f), new Vector3(  0.0f, -1.0f,  0.0f), new Vector2( 1.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f, -1.0f,  1.0f), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2(1.0f, 0.0f)),
            new TexturedVertex(new Vector3(-1.0f, -1.0f,  1.0f), new Vector3( 0.0f, -1.0f,  0.0f), new Vector2(0.0f, 0.0f)),
            new TexturedVertex(new Vector3(-1.0f, -1.0f, -1.0f), new Vector3(0.0f, -1.0f,  0.0f), new Vector2(0.0f, 1.0f)),

            new TexturedVertex(new Vector3(-1.0f,  1.0f, -1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2(1.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f , 1.0f), new Vector3( 0.0f,  1.0f,  0.0f), new Vector2(0.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f, -1.0f), new Vector3( 0.0f,  1.0f,  0.0f), new Vector2(0.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f,  1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 0.0f, 0.0f)),
            new TexturedVertex(new Vector3(-1.0f,  1.0f, -1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 1.0f, 1.0f)),
            new TexturedVertex(new Vector3(-1.0f,  1.0f,  1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 1.0f, 0.0f))
            */

            /*
            new TexturedVertex(new Vector3(-1.0f,  1.0f, -1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2(0.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f , 1.0f), new Vector3( 0.0f,  1.0f,  0.0f), new Vector2(1.0f, 0.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f, -1.0f), new Vector3( 0.0f,  1.0f,  0.0f), new Vector2(1.0f, 1.0f)),
            new TexturedVertex(new Vector3( 1.0f,  1.0f,  1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 1.0f, 0.0f)),
            new TexturedVertex(new Vector3(-1.0f,  1.0f, -1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 0.0f, 1.0f)),
            new TexturedVertex(new Vector3(-1.0f,  1.0f,  1.0f), new Vector3(0.0f,  1.0f,  0.0f), new Vector2( 0.0f, 0.0f))
            */
        };

      GL.GenVertexArrays(1, out VAO);
      GL.GenBuffers(1, out VBO);
      // fill buffer      
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, (Vertices.Length) * TexturedVertex.Size, Vertices, BufferUsageHint.StaticDraw);
      // link vertex attributes
      GL.BindVertexArray(VAO);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(2);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);

      base.Setup();
    }

  }
}
