using Massive.Events;
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
  public class MGrass : MSceneObject
  {
    object matlocker = new object();
    Matrix4[] mats;

    MModel grass;
    MMesh treemesh;
    MTerrainTile Tile;
    bool Prepared = false;
    bool Planted = true;//start as planted to prevent premature building

    double HeightThreshold = 60;  //when we are above this height (above the surface) don't draw

    //const int MaxInstances = 2000;
    int TotalInstances = 0;
    int sizeofmat = sizeof(float) * 4 * 4;
    int vec4size = sizeof(float) * 4;
    int instanceVBO;
    string TreeModel = @"Models\foliage\tree01.3ds";
    string TreeTexture = @"Textures\foliage\grass02.png";

    BackgroundWorker worker;

    public MGrass() : base(EType.Other, "Grass")
    {
      mats = new Matrix4[Settings.MaxGrassPerTerrain];

      MMessageBus.AvatarMovedEvent += MMessageBus_AvatarMovedEvent;
      DistanceThreshold = 6000;
      worker = new BackgroundWorker();
      worker.WorkerSupportsCancellation = true;
      worker.DoWork += Worker_DoWork;
    }

    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
      MAstroBody b = MPlanetHandler.CurrentNear;
      if (b == null) return;
      MTerrainTile tile = b.CurrentTile;
      if (tile == null) return;

      /*
      if ((tile.PhysicsIsComplete == true) && (tile.GrassPlanter.IsComplete == false))
      {
        tile.GrassPlanter.PrepareGrass(tile);
      }
      */
      lock (matlocker)
      {
        PlantGrass(b, tile);
      }

    }

    public override void Update()
    {
      /*
      MAstroBody b = MPlanetHandler.CurrentNear;
      if (b == null) return;
      MTerrainTile tile = b.CurrentTile;
      if (tile == null) return;

      if ((tile.PhysicsIsComplete == true) && (tile.GrassPlanter.IsComplete == false))
      {
        tile.GrassPlanter.PrepareGrass(tile);
      }
      PlantGrass(b, tile);
      */
    }

    private void MMessageBus_AvatarMovedEvent(object sender, MoveEvent e)
    {
      /*
       MAstroBody b = MPlanetHandler.CurrentNear;
       if (b == null) return;
       MTerrainTile tile = b.CurrentTile;
       if (tile == null) return;
       */
      /*
      if ((tile.PhysicsIsComplete == true) && (tile.GrassPlanter.IsComplete == false))
      {
        tile.GrassPlanter.PrepareGrass(tile);
      }
      */
      //PlantGrass(b, tile);
      if (worker.IsBusy)
      {
        worker.CancelAsync();
        return;
      }
      worker.RunWorkerAsync();
    }

    public override void Dispose()
    {
      GL.DeleteBuffer(instanceVBO);
      if (treemesh != null)
      {
        treemesh.Dispose();
      }
      if (grass != null)
      {
        grass.Dispose();
      }
      //base.Dispose();
    }

    //TODO: pull the nearest blades from the Grassmats and add to mats
    //OR: Rolling buffer, taking from behind and adding edges of direction of travel
    public void PlantGrass3(MAstroBody planet, MTerrainTile tile)
    {
      Tile = tile;
      if (tile.material == null) return;
      //if (Tile.GrassPlanter.IsComplete == false) return;

      Vector3d AP = Globals.Avatar.GetPosition();
      Barycentric bary = new Barycentric(Tile.Boundary.TL, Tile.Boundary.BR, Tile.Boundary.TR, AP);

      Vector3d Centroid = new Vector3d((1 - bary.u) * MGrassPlanter.LOD0Size, 0, bary.v * MGrassPlanter.LOD0Size);

      int cx = (int)(bary.u * MGrassPlanter.LOD0Size);
      int cz = (int)(bary.v * MGrassPlanter.LOD0Size);

      float numxinterps = MGrassPlanter.LOD0Size / Tile.x_res;
      float numzinterps = MGrassPlanter.LOD0Size / Tile.z_res;

      TotalInstances = 0;
      for (int x = 0; x < 100; x++)
        for (int z = 0; z < 100; z++)
        {
          if (TotalInstances >= Settings.MaxGrassPerTerrain) continue;
          if (cx + x > MGrassPlanter.LOD0Size) continue;
          if (cz + z > MGrassPlanter.LOD0Size) continue;

          //mats[TotalInstances] = Tile.GrassPlanter.Grassmats[cx + x, cz + z];
          TotalInstances++;
        }

      Planted = true;
    }


    public void PlantPatch(Barycentric bary, int BladeCount = 4096, double Area = 1.1, double MaxHeight = 1)
    {
      double sq2 = Math.Sqrt(BladeCount);

      Matrix4d TreeRotation = Matrix4d.CreateFromQuaternion(Globals.LocalUpRotation());
      Matrix4d TreePosition;
      Matrix4 final;
      Matrix4d TreeScale;
      Vector3d PlantingPos = new Vector3d();

      int bposx = (int)(Tile.x_res * (1 - bary.u));
      int bposz = (int)(Tile.z_res * (bary.v));
      // Tile.Biome.SetPixel(bposx, bposz, new float[] { 0, 1, 0, 1 });
      for (int x = (int)-sq2; x < sq2; x++)
        for (int z = (int)-sq2; z < sq2; z++)
        {
          bposx = (int)(Tile.x_res * (1-bary.u)) - x;
          bposz = (int)(Tile.z_res * (bary.v)) - z;      

          int t = Tile._biome.GetBiomeAt(bposx, bposz);
          if ((t != MBiome.GRASS)
            && (t != MBiome.TREES)
            && (t != MBiome.TREES2)
            && (t != MBiome.WATER))
            continue;
            

          PlantingPos.X = (int)(Tile.x_res * (1 - bary.u)) - x * 0.05 * Area;
          PlantingPos.Y = 0;
          PlantingPos.Z = (int)(Tile.z_res * (bary.v)) - z * 0.05 * Area;

          double yv =
            (MPerlin.Noise(PlantingPos.X, PlantingPos.Z) * 0.4)
            + (MPerlin.Noise(PlantingPos.X * 1.1, PlantingPos.Z * 1.1) * 0.4)
            ;
          //if (yv < 0.01) continue;

          TreeScale = Matrix4d.Scale(0.05 + 0.06 * yv, 0.01 + Math.Abs(yv) * 0.15 * MaxHeight, 0.05 + 0.06 * yv);

          PlantingPos.X += MPerlin.Noise(PlantingPos.X * 4.0, 0, PlantingPos.Z * 2.0) * 1.1;
          PlantingPos.Z += MPerlin.Noise(PlantingPos.X * 4.0, 0, PlantingPos.Z * 2.0) * 1.1;
          PlantingPos = Tile.GetInterpolatedPointOnSurfaceFromGrid2(PlantingPos);

          TreePosition = Matrix4d.CreateTranslation(PlantingPos);
          final = MTransform.GetFloatMatrix(TreeScale * TreeRotation * TreePosition);
          if (TotalInstances < Settings.MaxGrassPerTerrain)
          {
            mats[TotalInstances] = final;
            TotalInstances++;
          }
        }
    }


    public void PlantGrass(MAstroBody planet, MTerrainTile tile)
    {
      // if (Planted == true) return;
      //DistanceThreshold = tile.DistanceThreshold;
      Tile = tile;
      if (tile.material == null) return;
      MTexture tex = Tile.Biome;
      if (tex == null) return;

      this.transform.Position = tile.transform.Position;

      //Random ran = new Random(1234);

      Matrix4d TreeRotation = Matrix4d.CreateFromQuaternion(Globals.LocalUpRotation());

      //int i = 0;
      TotalInstances = 0;

      Vector3d AP = Globals.Avatar.GetPosition();
      // Console.WriteLine(AP);

      Barycentric bary = new Barycentric(Tile.Boundary.TL, Tile.Boundary.BR, Tile.Boundary.TR, AP);

      PlantPatch(bary, 64 * 64, 2, 0.8);
      //PlantPatch(bary, 32 * 32, 8, 0.3);

      // Console.WriteLine(bary.ToString());

      /*
      double sq = 64 * 64;
      double sq2 = Math.Sqrt(sq);

      Matrix4d TreePosition;
      Matrix4 final;
      Matrix4d TreeScale;
      Vector3d PlantingPos = new Vector3d();
      for (int x = (int)-sq2; x < sq2; x++)
        for (int z = (int)-sq2; z < sq2; z++)
        {
          PlantingPos.X = (int)(Tile.x_res * (1 - bary.u)) - x * 0.05;
          PlantingPos.Y = 0;
          PlantingPos.Z = (int)(Tile.z_res * (bary.v)) - z * 0.05;
         
          double yv = 
            (MPerlin.Noise(PlantingPos.X, PlantingPos.Z) * 0.4)
            + (MPerlin.Noise(PlantingPos.X * 1.1, PlantingPos.Z * 1.1) * 0.4)
            ;
          if (yv < 0.1) continue;

          TreeScale = Matrix4d.Scale(0.03+0.04 * yv,  0.01 + yv * 0.14 , 0.03+0.04* yv);
          
          PlantingPos.X += MPerlin.Noise(PlantingPos.X * 4.0, PlantingPos.Z * 4.1) * 1.1;
          PlantingPos.Z += MPerlin.Noise(PlantingPos.X * 3.8, PlantingPos.Z * 4.2) * 1.1;
          PlantingPos = Tile.GetInterpolatedPointOnSurfaceFromGrid2(PlantingPos);
          
          TreePosition = Matrix4d.CreateTranslation(PlantingPos);
          final = MTransform.GetFloatMatrix(TreeScale * TreeRotation * TreePosition);
          if (i < Settings.MaxGrassPerTerrain)
          {
            mats[i] = final;
            i++;
          }
        }

      TotalInstances = i;
      */

      for (int j = TotalInstances; j < Settings.MaxGrassPerTerrain; j++)
      {
        Matrix4 final = Matrix4.CreateTranslation(9999999999, 0, 0);
        mats[j] = final;
      }


      //Setup();
      //UploadBuffer();
      Planted = true;
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
      GL.BufferData(BufferTarget.ArrayBuffer, sizeofmat * TotalInstances, mats, BufferUsageHint.StaticDraw);
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
      grass = new MModel(EType.Model, "InstanceGrassModel");
      grass.DistanceThreshold = 15000;
      grass.Load(TreeModel);
      treemesh = (MMesh)grass.FindModuleByType(EType.Mesh);
      treemesh.transform.Scale = new Vector3d(1, 1, 1);
      treemesh.Setup();
      //MScene.Background.Add(tree);
    }

    void SetupMaterial()
    {
      MMaterial InstanceMat = new MMaterial("InstanceGrassMaterial");
      MShader shader = new MShader("InstanceGrassShader");
      //shader.LoadFromString(sVertexShader, sFragmentShader);
      shader.Load("instanced_v.glsl",
        "instanced_f.glsl",
        "", ""
        );
      shader.Bind();
      shader.SetFloat("Fade", 68);
      shader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      shader.SetInt("material.specular", MShader.LOCATION_SPECULAR);
      shader.SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      shader.SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      shader.SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);
      InstanceMat.AddShader(shader);
      MTexture GrassTex = Globals.TexturePool.GetTexture(TreeTexture);
      GrassTex._TextureWrapMode = TextureWrapMode.ClampToBorder;
      GrassTex.DoAssign = true;
      InstanceMat.SetDiffuseTexture(GrassTex);
      this.SetMaterial(InstanceMat);
      grass.SetMaterial(InstanceMat);
      MScene.MaterialRoot.Add(InstanceMat);
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (Settings.DrawGrass == false) return;
      //if (Globals.ShaderOverride != null) return;
      if (Globals.Avatar.DistanceToSurface > HeightThreshold) return;
      if (grass != null)
      {
        grass.transform.Position = this.transform.Position;
      }

      //GL.Enable(EnableCap.Blend);
      // GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

      if (material == null) return;
      CalculateDrawMatrices(viewproj, parentmodel);

      MShader temp = material.shader;
      if (Globals.ShaderOverride != null)
      {
        temp = Globals.ShaderOverride;
        temp.SetInt("HasBones", 0);
      }
      else
      {
        material.IsUsed = true;
        material.Render(viewproj, parentmodel);
        material.Bind();
        material.shader.SetFloat("Fade", 68);
        //49 6

        temp.SetBool("ShadowEnabled", CastsShadow);
      }
      temp.SetMat4("mvp", mvp);
      temp.SetMat4("model", modelMatrix);

      GL.BindVertexArray(treemesh.VAO);
      lock (matlocker)
      {
        UploadBufferFull();
      }

      GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, treemesh.Indices.Length, TotalInstances);
      // Helper.CheckGLError(this);
      GL.BindVertexArray(0);
      if (Globals.ShaderOverride == null)
      {
        material.UnBind();
      }
      //base.Render(viewproj, parentmodel);
    }
  }
}
