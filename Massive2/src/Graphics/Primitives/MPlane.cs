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
  public class MPlane : MSceneObject
  {
    int XRes = 4;
    int YRes = 4;
    

    public MPlane(string sName)
      : base(EType.SceneObject, sName)
    {
    }

    public override void Setup()
    {
      base.Setup();
      SetupPlane(XRes, YRes, 1, 1);
      SetupBuffers();
    }

    public void SetupBuffers()
    {
      GL.GenVertexArrays(1, out VAO);
      GL.GenBuffers(1, out VBO);
      GL.GenBuffers(1, out EBO);

      int MSize = TexturedVertex.Size;

      GL.BindVertexArray(VAO);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * MSize, Vertices, BufferUsageHint.StaticDraw);

      GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
      GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(int), Indices, BufferUsageHint.StaticDraw);

      // vertex positions
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, MSize, 0);
      // vertex normals
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 3);
      // vertex texture coords
      GL.EnableVertexAttribArray(2);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 6);

      /*
      // vertex tangent
      GL.EnableVertexAttribArray(3);
      GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 8);
      // vertex bitangent
      GL.EnableVertexAttribArray(4);
      GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 11);
      */

      GL.BindVertexArray(0);
    }

    public void SetupPlane(int x_res, int y_res, float x_scale, float y_scale)
    {
      Vertices = new TexturedVertex[x_res * y_res];
      Indices = new int[6 * x_res * y_res];
      //Texcoords = new Vector2[x_res * y_res];

      int i = 0;
      //for (int y = -y_res / 2; y < y_res / 2; y++)
      for (int y = 0; y < y_res; y++)
      {
        //for (int x = -x_res / 2; x < x_res / 2; x++)
        for (int x = 0; x < x_res ; x++)
        {
          Vertices[i]._position.X = x_scale * (float)x / (float)x_res;
          Vertices[i]._position.Z = y_scale * (float)y / (float)y_res;
          Vertices[i]._position.Y = 0;
          Vertices[i]._normal.X = 0;
          Vertices[i]._normal.Y = 1;
          Vertices[i]._normal.Z = 0;
          Vertices[i]._textureCoordinate = new Vector2((float)x / (float)x_res, (float)y / (float)y_res);

          i++;
        }
      }
     
      i = 0;
      for (int y = 0; y < y_res - 1; y++)
      {
        for (int x = 0; x < x_res - 1; x++)
        {
          Indices[i++] = (y + 0) * x_res + x;
          Indices[i++] = (y + 1) * x_res + x;
          Indices[i++] = (y + 0) * x_res + x + 1;

          Indices[i++] = (y + 0) * x_res + x + 1;
          Indices[i++] = (y + 1) * x_res + x;
          Indices[i++] = (y + 1) * x_res + x + 1;
        }
      }
      
    }
  }
}
