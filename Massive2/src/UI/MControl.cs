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
  public class MControl : MSceneObject
  {
    public double Width = 100;
    public double Height = 100;
    public bool CreateGeometry = true;    


    public MControl(string sName = "Control") : base(EType.GUI, sName)
    {
      Renderable = true;
    }

    public virtual void DoClick()
    {
      if (Parent.Type == EType.GUI)
      {
        ((MControl)Parent).DoClick();
      }
      base.OnClick();
    }

    public override void Setup()
    {
      if (CreateGeometry == true) SetupGeometry();
     
      SetupBuffers();
      SetupMaterial();
      base.Setup();
    }

    void SetupGeometry()
    {
      int x_res = 2;
      int y_res = 2;
      float x_scale = (float)(2 * Width);
      float y_scale = (float)(2 * Height);
      Vertices = new TexturedVertex[x_res * y_res];
      Indices = new int[6 * x_res * y_res];
      //Texcoords = new Vector2[x_res * y_res];

      int i = 0;
      //for (int y = -y_res / 2; y < y_res / 2; y++)
      for (int y = 0; y < y_res; y++)
      {
        //for (int x = -x_res / 2; x < x_res / 2; x++)
        for (int x = 0; x < x_res; x++)
        {
          Vertices[i]._position.X = x_scale * (float)x / (float)x_res;
          Vertices[i]._position.Y = y_scale * (float)y / (float)y_res;
          Vertices[i]._position.Z = 0;
          Vertices[i]._normal.X = 0;
          Vertices[i]._normal.Y = 0;
          Vertices[i]._normal.Z = 1;
          Vertices[i]._textureCoordinate = new Vector2((float)(x) / (float)(x_res - 1), (float)(1 - y) / (float)(y_res - 1));

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

    public void SetupMaterial()
    {
      if (material == null)
      {
        material = new MMaterial("Material");
        material.AddShader(Helper.GetGUIShader());
        AddMaterial(material);
        material.SetDiffuseTexture(Globals.TexturePool.GetTexture("UI\\art\\grey-bg.jpg"));
      }
    }
  }
}
