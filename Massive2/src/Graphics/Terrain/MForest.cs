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

namespace Massive
{
  public class MForest : MSceneObject
  {
    Matrix4[] mats;
    MModel tree;
    MMesh treemesh;
    MTerrainTile Tile;
    bool Planted = true;//start as planted to prevent premature building

    //const int MaxInstances = 2000;
    int TotalInstances = 0;
    int sizeofmat = sizeof(float) * 4 * 4;
    int vec4size = sizeof(float) * 4;
    int instanceVBO;
    string TreeModel = @"Models\foliage\tree01.3ds";
    string TreeTexture = @"Textures\foliage\clovetree.png";

    public MForest() : base(EType.Other, "Forest")
    {
      mats = new Matrix4[Settings.MaxTreesPerTerrain];
    }

    public override void Dispose()
    {
      GL.DeleteBuffer(instanceVBO);
      if (treemesh != null)
      {
        treemesh.Dispose();
      }
      if (tree != null)
      {
        tree.Dispose();
      }
      //base.Dispose();
    }

    public void PlantTrees(MAstroBody planet, MTerrainTile tile)
    {
      //DistanceThreshold = tile.DistanceThreshold;
      Tile = tile;
      if (tile.material == null) return;
      MTexture tex = Tile.Biome;
      if (tex == null) return;

      this.transform.Position = tile.transform.Position;

      Random ran = new Random(1234);

      Matrix4d TreeRotation = Matrix4d.CreateFromQuaternion(Globals.LocalUpRotation());

      int i = 0;

      for (int z = 0; z < tile.z_res - 1; z++)
      {
        for (int x = 0; x < tile.x_res - 1; x++)
        {
          float[] c = tex.GetPixel(x, z);
          float r = c[0];
          float g = c[1];
          float b = c[2];
          float a = c[3];

          float t = 0;
          //if ((b > r) && (b > g)) t = g;
          if ((r < 0.05) && (g > 0.1) && (b < 0.05)) t = g;
          else continue;

          if (ran.NextDouble() > Settings.TreeDensity) continue;

          //if (g < 0.7) continue;
          //Console.WriteLine(c[0] + " " + c[1] + " " + c[2] + " " + c[3]);
          if (i >= Settings.MaxTreesPerTerrain) break;
          Vector3d Treepos = new Vector3d(x,
            0,
            z);
          //Vector3d PlantingPos = planet.GetNearestPointOnSphere(Treepos, 0);
          Matrix4d TreeScale = Matrix4d.Scale(1 + ran.NextDouble(), 1 + ran.NextDouble() * 2, 1 + ran.NextDouble());
          Vector3d PlantingPos = Tile.GetPointOnSurfaceFromGrid(Treepos); //; + new Vector3d(r.NextDouble()*5, r.NextDouble() * 5, r.NextDouble()*5);
          Matrix4d TreePosition = Matrix4d.CreateTranslation(PlantingPos);
          //find point at y with raycast
          Matrix4 final = MTransform.GetFloatMatrix(TreeScale * TreeRotation * TreePosition);
          mats[i] = final;
          i++;
        }
      }

      TotalInstances = i;

      for (int j = TotalInstances; j < Settings.MaxTreesPerTerrain; j++)
      {
        Matrix4 final = Matrix4.CreateTranslation(j, 0, 0);
        mats[j] = final;
      }

      //Setup();
      //UploadBuffer();
      Planted = false;
    }

    /*
    private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      //UploadBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, instanceVBO);
      GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, sizeofmat * TotalInstances, mats);
    }
    */

    void UploadBufferFull()
    {
      GL.BindBuffer(BufferTarget.ArrayBuffer, instanceVBO);
      GL.BufferData(BufferTarget.ArrayBuffer, sizeofmat * Settings.MaxTreesPerTerrain, mats, BufferUsageHint.StaticDraw);
      //GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, sizeofmat * MaxInstances, mats);

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
      SetupTree();
      SetupMaterial();

      float w = 0.5f;

      Vertices = new TexturedVertex[] {
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
      };

      GL.BindBuffer(BufferTarget.ArrayBuffer, treemesh.VBO);
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, treemesh.EBO);

      GL.GenBuffers(1, out instanceVBO);
      //Helper.CheckGLError(this);      
      UploadBufferFull();
      //Helper.CheckGLError(this);

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
    }

    void SetupTree()
    {
      tree = new MModel(EType.Model, "InstanceTree");
      tree.DistanceThreshold = 15000;
      tree.Load(TreeModel);
      treemesh = (MMesh)tree.FindModuleByType(EType.Mesh);
      treemesh.transform.Scale = new Vector3d(1, 1, 1);
      treemesh.Setup();
      //MScene.Background.Add(tree);
    }

    void SetupMaterial()
    {
      /*
      MShader TreeShader = new MShader("TreeShader");
      TreeShader.Load("default_v.glsl",
        "default_f.glsl",
        "",
        ""
        );
      TreeShader.Bind();
      TreeShader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      TreeShader.SetInt("material.specular", MShader.LOCATION_SPECULAR);
      TreeShader.SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      TreeShader.SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      TreeShader.SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);

      MMaterial Avatar1Mat = new MMaterial("TREE01M");
      Avatar1Mat.AddShader(TreeShader);
      Avatar1Mat.SetDiffuseTexture(Globals.TexturePool.GetTexture("Textures\\avatar01.jpg"));
      MScene.MaterialRoot.Add(Avatar1Mat);
     */

      MMaterial InstanceMat = new MMaterial("ForestMaterialInstanced");
      MShader shader = (MShader)MScene.MaterialRoot.FindModuleByName("ForestShaderInstanced");
      if (shader == null)
      {
        shader = new MShader("ForestShaderInstanced");
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
      }
      InstanceMat.AddShader(shader);
      InstanceMat.SetDiffuseTexture(Globals.TexturePool.GetTexture(TreeTexture));
      this.SetMaterial(InstanceMat);
      tree.SetMaterial(InstanceMat);
      MScene.MaterialRoot.Add(InstanceMat);
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (Settings.DrawTrees == false) return;
      //if (Globals.ShaderOverride != null) return;
      if (DistanceFromAvatar > DistanceThreshold) return;
      /*if (tree != null)
      {
        tree.transform.Position = this.transform.Position;
      }*/
      if (Planted == false)
      {
        Setup();
        Planted = true;
      }
      if (material == null) return;
      CalculateDrawMatrices(viewproj, parentmodel);

      MShader temp = material.shader;
      if (Globals.ShaderOverride != null)
      {
        temp = Globals.ShaderOverride;
      }
      else
      {
        material.Bind();
      }

      temp.SetMat4("mvp", mvp);
      temp.SetMat4("model", modelMatrix);
      //material.shader.SetBool("selected", Selected);
      temp.SetBool("ShadowEnabled", CastsShadow);

      GL.BindVertexArray(treemesh.VAO);
      //GL.BindBuffer(BufferTarget.ElementArrayBuffer, treemesh.EBO);      

      UploadBufferFull();

      material.Render(viewproj, parentmodel);
      GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, treemesh.Vertices.Length, TotalInstances);
      // Helper.CheckGLError(this);
     // GL.BindVertexArray(0);
     // material.UnBind();
      base.Render(viewproj, parentmodel);
    }
  }
}
