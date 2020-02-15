using Massive.GIS;
using Massive.Tools;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Used for drawing many of the same mesh
/// </summary>

namespace Massive
{
  public class MInstanceModel : MSceneObject
  {
    public double MinSize = 0.1;
    public double MaxSize = 0.3;

    Matrix4[] mats;
    MModel tree;
    MMesh mesh;
    bool Planted = true;//start as planted to prevent premature building

    const int MaxInstances = 5000;
    int TotalInstances = 0;
    int sizeofmat = sizeof(float) * 4 * 4;
    int vec4size = sizeof(float) * 4;
    int instanceVBO;
    public string ModelPath = @"Models\planets\asteroid01.3ds";
    public string MeshTexture = @"Textures\roads\dirt2.jpg";

    public MInstanceModel(string sName, string inModelPath, string inModelTexturePath)
      : base(EType.InstanceMesh, sName)
    {
      ModelPath = inModelPath;
      MeshTexture = inModelTexturePath;
      mats = new Matrix4[MaxInstances];
      DistanceThreshold = 30000;
    }

    public void CopyTo(MInstanceModel mo)
    {
      mo.material = material;
      mo.tree = tree;
      mo.mesh = mesh;
      mo.TotalInstances = TotalInstances;
      mo.instanceVBO = instanceVBO;
      mo.mats = mats;
    }

    public override void Dispose()
    {
      GL.DeleteBuffer(instanceVBO);
      if (mesh != null)
      {
        mesh.Dispose();
      }
      if (tree != null)
      {
        tree.Dispose();
      }
      //base.Dispose();
    }

    public void PlaceInstances(Vector3d Anchorpoint)
    {
      //DistanceThreshold = tile.DistanceThreshold;
      this.transform.Position = Anchorpoint;

      Random ran = new Random(1234);

      Matrix4d TreeRotation = Matrix4d.CreateFromQuaternion(Globals.LocalUpRotation());

      int i = 0;

      for (int z = 0; z < 30; z++)
      {
        for (int x = 0; x < 30; x++)
        {
          if (i >= MaxInstances) break;

          //Vector3d pos = new Vector3d(1000 * ran.NextDouble(), 200 * ran.NextDouble(), 1000 * ran.NextDouble());
          Vector3d pos = MassiveTools.RandomPointInSphere(Vector3d.Zero, 1000, ran);
          //Vector3d PlantingPos = planet.GetNearestPointOnSphere(SeedPos + Treepos, 0) - SeedPos;
          double scale = MinSize + ran.NextDouble() * (MaxSize - MinSize);
          Matrix4d TreeScale = Matrix4d.Scale(scale, scale, scale);

          Matrix4d TreePosition = Matrix4d.CreateTranslation(pos);
          TreeRotation = Matrix4d.CreateFromQuaternion(
            Quaterniond.FromEulerAngles(
              ran.NextDouble(),
              ran.NextDouble(),
              ran.NextDouble()));
          //find point at y with raycast
          Matrix4 final = MTransform.GetFloatMatrix(TreeScale * TreeRotation * TreePosition);
          mats[i] = final;
          i++;
        }
      }

      TotalInstances = i;

      //null out the remainder
      for (int j = TotalInstances; j < MaxInstances; j++)
      {
        Matrix4 final = Matrix4.Identity;
        mats[j] = final;
      }

      Planted = false;
    }

    void UploadBuffer()
    {      
      GL.BindBuffer(BufferTarget.ArrayBuffer, instanceVBO);      
      GL.BufferData(BufferTarget.ArrayBuffer, sizeofmat * TotalInstances, mats, BufferUsageHint.StaticDraw);
      //GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, sizeofmat * TotalInstances, mats);

      Matrix4 mat = new Matrix4();
      int mat4size = Marshal.SizeOf(mat);

      GL.EnableVertexAttribArray(3);
      GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, mat4size, 0);

      GL.EnableVertexAttribArray(4);
      GL.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, mat4size, vec4size);

      GL.EnableVertexAttribArray(5);
      GL.VertexAttribPointer(5, 4, VertexAttribPointerType.Float, false, mat4size, 2 * vec4size);

      GL.EnableVertexAttribArray(6);
      GL.VertexAttribPointer(6, 4, VertexAttribPointerType.Float, false, mat4size, 3 * vec4size);

      GL.VertexAttribDivisor(3, 1);
      GL.VertexAttribDivisor(4, 1);
      GL.VertexAttribDivisor(5, 1);
      GL.VertexAttribDivisor(6, 1);
    }

    public override void Setup()
    {
      base.Setup();
      SetupMesh();
      SetupMaterial();
      if (mesh == null) return;
      GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VBO);
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.EBO);

      GL.GenBuffers(1, out instanceVBO);
      Helper.CheckGLError(this);
      UploadBuffer();
      Helper.CheckGLError(this);

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);

      PlaceInstances(this.transform.Position);
      // UploadBuffer();
    }

    void SetupMesh()
    {
      tree = new MModel(EType.Model, "BaseModel");
      tree.DistanceThreshold = 5000;
      tree.Load(ModelPath);
      mesh = (MMesh)tree.FindModuleByType(EType.Mesh);
      if (mesh == null) return;
      mesh.transform.Scale = new Vector3d(1, 1, 1);
      mesh.Setup();
    }

    void SetupMaterial()
    {
      MMaterial InstanceMat = new MMaterial("InstanceMaterial");
      MShader shader = new MShader("InstanceShader");
      //shader.LoadFromString(sVertexShader, sFragmentShader);
      shader.Load("instanced_v.glsl",
        "instanced_f.glsl",
        "", ""
        );
      shader.Bind();
      shader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      shader.SetInt("material.specular", MShader.LOCATION_SPECULAR);
      shader.SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      shader.SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      shader.SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);
      InstanceMat.AddShader(shader);
      InstanceMat.SetDiffuseTexture(Globals.TexturePool.GetTexture(MeshTexture));
      this.SetMaterial(InstanceMat);
      material = InstanceMat;
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (Globals.ShaderOverride != null) return;
      if (DistanceFromAvatar > DistanceThreshold) return;

      if (Planted == false)
      {
        Setup();
        Planted = true;
      }
      if (material == null) return;
      CalculateDrawMatrices(viewproj, parentmodel);

      material.Bind();
      material.shader.SetMat4("mvp", mvp);
      material.shader.SetMat4("model", modelMatrix);
      //material.shader.SetBool("selected", Selected);
      material.shader.SetBool("ShadowEnabled", CastsShadow);

      //GL.BindVertexArray(VAO);      
      GL.BindVertexArray(mesh.VAO);
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.EBO);

      UploadBuffer();
      material.Render(viewproj, parentmodel);
      //GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length);      
      //GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, treemesh.Vertices.Length, TotalInstances);
      GL.DrawElementsInstanced(PrimitiveType.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, mesh.Indices, TotalInstances);
      GL.BindVertexArray(0);
      material.UnBind();
      //base.Render(viewproj, parentmodel);
    }
  }
}
