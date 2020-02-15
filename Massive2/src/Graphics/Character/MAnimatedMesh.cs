using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace Massive.Graphics.Character
{
  class MAnimatedMesh : MMesh
  {
    new AnimatedVertex[] Vertices;        
    VertexBoneData[] bones_id_weights_for_each_vertex;

    int VBO_bones;    
    public MAnimatedMesh(string sName):
      base ("BoneMesh", EType.BoneMesh)
    {

    }

    public MAnimatedMesh(List<AnimatedVertex> vertic, int[] ind, VertexBoneData[] bone_id_weights)
      : base("BoneMesh", EType.BoneMesh)
    {
      Vertices = vertic.ToArray();
      VerticesLength = Vertices.Length;
      Indices = ind;
      IndicesLength = Indices.Length;    
      bones_id_weights_for_each_vertex = bone_id_weights;

      // Now that we have all the required data, set the vertex buffers and its attribute pointers.
     
    }

    public override void Setup()
    {      
      SetupMesh();
    }

    void SetupMesh()
    {
      //vertices data
      int MSize = AnimatedVertex.Size;

      GL.GenBuffers(1, out VBO);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      Helper.CheckGLError(this, "Setup");
      GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * MSize, Vertices, BufferUsageHint.StaticDraw);
      Helper.CheckGLError(this, "Setup");
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

      
      //bones data
      int BoneDataSize = (4 + 4) * 4;
      GL.GenBuffers(1, out VBO_bones);
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO_bones);      
      GL.BufferData(BufferTarget.ArrayBuffer, bones_id_weights_for_each_vertex.Length 
        * BoneDataSize, bones_id_weights_for_each_vertex, BufferUsageHint.StaticDraw);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

      GL.GenBuffers(1, out EBO);
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
      GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(int), Indices, BufferUsageHint.StaticDraw);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

      //numbers for sequence indices

      GL.GenVertexArrays(1, out VAO);
      GL.BindVertexArray(VAO);           
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
      //vertex position
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, MSize, 0);

      GL.EnableVertexAttribArray(1); // offsetof(Vertex, normal) = returns the byte offset of that variable from the start of the struct
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 3);

      GL.EnableVertexAttribArray(2);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, MSize, sizeof(float) * 6);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      
      //bones
      GL.BindBuffer(BufferTarget.ArrayBuffer, VBO_bones);
      GL.EnableVertexAttribArray(3);
      GL.VertexAttribIPointer(3, 4, VertexAttribIntegerType.UnsignedInt, BoneDataSize, IntPtr.Zero); // for INT Ipointer      
      GL.EnableVertexAttribArray(4);
      GL.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, BoneDataSize, sizeof(uint) * 4);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      
      //indices
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
      GL.BindVertexArray(0);
     
     // base.Setup();
    }

    
    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      base.Render(viewproj, parentmodel);
      //Helper.CheckGLError(this);
    }   

  }
}
