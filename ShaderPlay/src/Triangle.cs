using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

namespace ShaderPlay
{
  public class Triangle : IModel
  {
    int VBO = -1;    
    int EBO = -1;

    public override void Setup()
    {
      base.Setup();

      float[] vertices = {
         0.5f,  0.5f, 0.0f,  // top right
         0.5f, -0.5f, 0.0f,  // bottom right
        -0.5f, -0.5f, 0.0f,  // bottom left
        -0.5f,  0.5f, 0.0f   // top left 
    };
      int[] indices = {  // note that we start from 0!
        0, 1, 3,  // first Triangle
        1, 2, 3   // second Triangle
    };
      
      GL.DeleteBuffer(VBO);
      GL.DeleteBuffer(EBO);

      GL.GenVertexArrays(1, out VAO);
      GL.GenBuffers(1, out VBO);
      GL.GenBuffers(1, out EBO);

      GL.BindVertexArray(VAO);

      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

      GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
      GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), IntPtr.Zero);
      GL.EnableVertexAttribArray(0);

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public override void Render()
    {
      GL.BindVertexArray(VAO);
      GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }
  }
}
