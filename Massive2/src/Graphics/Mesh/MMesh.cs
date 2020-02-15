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
  public class MMesh : MSceneObject
  {  
    List<MTexture> textures;
    
    public int FaceCount = 0;
   
    public MMesh(string sName = "",EType type = EType.Mesh) : base(type, sName)
    {

    }

    public void SetupMesh(List<TexturedVertex> vertices, List<int> indices, List<MTexture> textures)
    {
      //this.vertices = vertices;
      this.Vertices = vertices.ToArray();
      VerticesLength = Vertices.Length;
      //this.indices = indices;
      
      this.Indices = indices.ToArray();
      IndicesLength = Indices.Length;
      
      this.textures = textures;

      Setup();
    }

    public override void Setup()
    {
      GL.GenVertexArrays(1, out VAO);
      GL.GenBuffers(1, out VBO);
      GL.GenBuffers(1, out EBO);

      int MSize = TexturedVertex.Size;

      GL.BindVertexArray(VAO);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * MSize, Vertices, BufferUsageHint.DynamicDraw);

      GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
      GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(int), Indices, BufferUsageHint.DynamicDraw);

      // vertex positions
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, MSize, 0);
      // vertex normals
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 3);
      // vertex texture coords
      GL.EnableVertexAttribArray(2);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 6);

      // vertex tangent
      //GL.EnableVertexAttribArray(3);
      //GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 8);
      // vertex bitangent
      //GL.EnableVertexAttribArray(4);
      //GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 11);


      GL.BindVertexArray(0);

      base.Setup();
    }    
  }
}
