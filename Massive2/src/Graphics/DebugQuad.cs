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
  class DebugQuad : MSceneObject
  {
    int quadVAO = 0;
    int quadVBO;

    public DebugQuad(string inname) : base(EType.SceneObject, inname)
    {
    }

    public override void Setup()
    {
      if (quadVAO == 0)
      {
        float[] quadVertices = {
            // positions        // texture Coords
            -1f,     1.0f, 0.0f, 0.0f, 1.0f,
            -1f,   -.1f,   0.0f, 0.0f, 0.0f,
            -0.5f,   1.0f, 0.0f, 1.0f, 1.0f,
            -0.5f,  -0.1f, 0.0f, 1.0f, 0.0f,
        };
        // setup plane VAO
        GL.GenVertexArrays(1, out quadVAO);
        GL.GenBuffers(1, out quadVBO);
        GL.BindVertexArray(quadVAO);
        GL.BindBuffer(BufferTarget.ArrayBuffer, quadVBO);
        GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * quadVertices.Length, quadVertices, BufferUsageHint.StaticDraw);
        GL.EnableVertexAttribArray(0);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), IntPtr.Zero);
        GL.EnableVertexAttribArray(1);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
      }
      base.Setup();
    }

    public override void Bind()
    {
      material.shader.Bind();
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      GL.BindVertexArray(quadVAO);
      GL.DrawArrays(OpenTK.Graphics.OpenGL4.PrimitiveType.TriangleStrip, 0, 4);
      GL.BindVertexArray(0);

      base.Render(viewproj, parentmodel);
    }
  }
}
