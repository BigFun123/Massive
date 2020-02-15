using Massive.Events;
using Massive.GIS;
using Massive.Optimize;
using Massive.Platform;
using Massive.Services;
using Massive.Tools;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;

/***
 * MScene updates and renders the scene in OpenGL, shunts data between 32->64bit, and renders effects and overlays
 * 
 * TODO: Formalize order of operations:
 * 1. Update Physics
 * 2. Update Camera
 * 3. Update Overlays (they require cam update)
 * 4. Render far
 * 5. Render near
 * 4. Render overlay
 * 6. Render Debug
 * */

namespace Massive
{
  public class MScene : MObject
  {
    public static MObject Root;
    public static MObject ModelRoot;
    public static MObject Background;
    public static MObject Background2;
    public static MObject Priority1;
    public static MObject Priority2; //transparent objects
    public static MObject Overlay;
    public static MObject AstroRoot; //for astronimical bodies that affect gravity
    public static MObject SelectionRoot;
    public static MObject TemplateRoot;
    public static MObject UtilityRoot;
    public static MObject MaterialRoot;
    public static MObject LightRoot;

    public static MDistanceClipper DistanceClipper;
    public static MFrustrumCuller FrustrumCuller;

    public static MGUI GUIRoot;

    public static MCamera Camera;

    public static MSceneObject SelectedObject;
    public static MPhysics Physics;

    new public EventHandler<GraphChangedEvent> GraphChanged;
    public static EventHandler<ClickEvent> OnClicked;
    public event EventHandler<RenderEvent> OnPostRender;

    public Time time;
    public static MLight light;

    public static MClimate Climate;
    //MShader shader;
    MShader simpleDepthShader;
    //MShader debugDepthQuad;
    public const int MAXLIGHTS = 8;

    public bool Stereo = false;

    public static MFog Fog;
    public static MAudioListener audioListener;
    public static MRenderer Renderer;
    public static int Closeup = 0;

    //MCube cube;
    // MModel model;
    MSky backdrop;
    DebugQuad debugQuad;


    public static MScreenPick ScreenPick;
    MShader DefaultShader;
    MMaterial DefaultMaterial;

    //int WoodTex;
    MMaterial Wood;
    public bool Playing = false;

    public MScene(bool AddBackdrop) : base(EType.Other, "Scene")
    {
      Globals._scene = this;
      MZoneService ms = new MZoneService();
      // Globals.SetProjectPath(@"I:\root\dev\_Massive_64bit\Massive\MassiveTest2");
    }

    public void Clear()
    {
      if (Root != null)
      {
        Root.Dispose();
        Root.Modules.Clear();
      }
    }

    public void LoadScripts()
    {
      Globals.ScriptHost.LoadDLL();
      Globals.ScriptHost.Init();
    }

    public void Play()
    {
      Playing = true;
      Root.OnPlay();
    }

    public void Stop()
    {
      Playing = false;
      Root.OnStop();
    }

    public void SetRenderMode(PolygonMode inMode)
    {
      //_renderMode = inMode;
      //GL.PolygonMode(MaterialFace.Front, _renderMode);
    }

    /// <summary>
    /// The main setup point for the initial scene graph and tools
    /// </summary>
    public void SetupInitialObjects()
    {
      Root = new MObject(MObject.EType.Null, "Root");
      //Root.Deletable = false;

      TemplateRoot = new MObject(MObject.EType.Null, "TemplateRoot");
      Root.Add(TemplateRoot);
      TemplateRoot.Enabled = false; //disable updates      

      SelectionRoot = new MObject(MObject.EType.Null, "Selection");
      Root.Add(SelectionRoot);

      UtilityRoot = new MObject(MObject.EType.Null, "Utility");
      UtilityRoot.Deletable = false;
      Root.Add(UtilityRoot);
      time = new Time();
      UtilityRoot.Add(time);

      LightRoot = new MObject(MObject.EType.Null, "LightRoot");
      Root.Add(LightRoot);

      Climate = new MClimate();
      UtilityRoot.Add(Climate);

      Globals.ScriptHost = new MScriptHost();
      UtilityRoot.Add(Globals.ScriptHost);
      MSystemScript sc = new MSystemScript("Massive.Main", Root);
      sc.SetActivator(Globals.ScriptHost.GetMainActivator());
      Root.Add(sc);

      Globals.TexturePool = new TexturePool();
      UtilityRoot.Add(Globals.TexturePool);

      Renderer = new MRenderer();
      UtilityRoot.Add(Renderer);

      DefaultShader = new MShader(MShader.DEFAULT_SHADER);
      DefaultShader.Load("default_v.glsl",
        "default_f.glsl",
        "Terrain\\eval.glsl", "Terrain\\control.glsl");
      DefaultShader.Bind();
      DefaultShader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      DefaultShader.SetInt("material.specular", MShader.LOCATION_SPECULAR);
      DefaultShader.SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      DefaultShader.SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      DefaultShader.SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);
      //DefaultShader.Deletable = false;
      Helper.CheckGLError(this, "TestPoint 1");

      // MShader GUIShader = new MShader("DefaultGUIShader");
      // GUIShader.Load("gui_v.glsl", "gui_f.glsl");
      //GUIShader.Bind();
      //GUIShader.Deletable = false;

      MaterialRoot = new MObject(MObject.EType.Null, "MaterialRoot");
      //MaterialRoot.Deletable = false;
      Root.Add(MaterialRoot);
      Helper.CheckGLError(this);
      if (Physics != null)
      {
        Physics.Dispose();
      }
      Physics = new MPhysics();
      Physics.Setup(); //only need to do this once
      //Physics.Deletable = false;
      UtilityRoot.Add(Physics);
      Helper.CheckGLError(this, "TestPoint 2");
      //UtilityRoot.Add(Globals.Network);
      //Globals.Network.Deletable = false;

      DistanceClipper = new MDistanceClipper();
      UtilityRoot.Add(DistanceClipper);
      FrustrumCuller = new MFrustrumCuller();
      UtilityRoot.Add(FrustrumCuller);

      Globals.Avatar = new MAvatar("Player1");
      UtilityRoot.Add(Globals.Avatar);

      Background = new MObject(MObject.EType.Null, "Background");
      Root.Add(Background);
      Background2 = new MObject(MObject.EType.Null, "Background2");
      Root.Add(Background2);

      ModelRoot = new MObject(MObject.EType.Null, "ModelRoot");
      Root.Add(ModelRoot);
      ModelRoot.Deletable = false;

      AstroRoot = new MObject(MObject.EType.Null, "AstroRoot");
      //ModelRoot.Add(AstroRoot);
      UtilityRoot.Add(AstroRoot);
      AstroRoot.Deletable = false;

      if (Settings.DrawPlanets == true)
      {
        MPlanetHandler mpi = new MPlanetHandler();
        UtilityRoot.Add(mpi);
        MGrass grass = new MGrass();
        Background2.Add(grass);
      }

      Priority1 = new MObject(MObject.EType.Null, "Priority1");
      ModelRoot.Add(Priority1);
      Priority2 = new MObject(MObject.EType.Null, "Priority2");
      ModelRoot.Add(Priority2);
      Overlay = new MObject(MObject.EType.Null, "Overlay");
      //ModelRoot.Add(Overlay); //manually drawn in render


      if (Settings.DrawBackdrop == true)
      {
        AddBackdrop();
      }

      Fog = new MFog();
      UtilityRoot.Add(Fog);

      DefaultMaterial = new MMaterial(MMaterial.DEFAULT_MATERIAL);
      DefaultMaterial.Deletable = false;
      DefaultMaterial.AddShader(DefaultShader);
      //DefaultMaterial.SetDiffuseTexture(Globals.TexturePool.GetTexture("Textures\\default.jpg"));
      MaterialRoot.Add(DefaultMaterial);

      MTexture DefaultTexture = new MTexture(MTexture.DEFAULT_TEXTURE);
      DefaultTexture.LoadTextureData(MFileSystem.ProjectPath + "Assets\\Textures\\default.jpg");
      DefaultTexture.DoAssign = true;
      DefaultMaterial.SetDiffuseTexture(DefaultTexture);


      MMaterial GUIMat = new MMaterial("DefaultGUIMaterial");
      //GUIMat.Deletable = false;
      //GUIMat.AddShader(GUIShader);
      //GUIMat.SetDiffuseTexture(Globals.TexturePool.GetTexture("Textures\\unwrap_helper_1024.jpg"));
      //MaterialRoot.Add(GUIMat);

      MMaterial DepthMaterial = new MMaterial("Depth");
      DepthMaterial.Deletable = false;
      simpleDepthShader = new MShader("simpleDepthShader");
      simpleDepthShader.Load("shadow_mapping_depth_v.glsl",
        "shadow_mapping_depth_f.glsl",
        "", "")
        ;
      DepthMaterial.shader = simpleDepthShader;
      DepthMaterial.Add(simpleDepthShader);
      MaterialRoot.Add(DepthMaterial);
      Helper.CheckGLError(this, "TestPoint 5");


      //UtilityRoot.Add(audioListener);
      //audioListener.Deletable = false;

      Camera = new MCamera("MainCam");
      Camera.OwnerID = MObject.OWNER_SYSTEM;
      Camera.transform.Position = new Vector3d(9, 5, 9);
      UtilityRoot.Add(Camera);
      audioListener = new MAudioListener();
      Camera.Add(audioListener);
      // Camera.Deletable = false;

      light = new MLight("DirLight");
      light.OwnerID = MObject.OWNER_SYSTEM;
      light.transform.Position = new Vector3d(-10, 20.0f, -10.0f);
      UtilityRoot.Add(light);
      //light.Deletable = false;

      ScreenPick = new MScreenPick();
      // ScreenPick.Setup();
      UtilityRoot.Add(ScreenPick);
      // ScreenPick.Deletable = false;

      MShader debugDepthQuad = new MShader("debugDepthQuad");
      debugDepthQuad.Load("debug_quad_v.glsl",
        "debug_quad_f.glsl",
        "Terrain\\eval.glsl", "Terrain\\control.glsl");
      debugDepthQuad.Bind();
      debugDepthQuad.SetInt("depthMap", 0);
      //debugDepthQuad.SetFloat("near_plane", light.NearPlane);
      //debugDepthQuad.SetFloat("far_plane", light.FarPlane);
      UtilityRoot.Add(debugDepthQuad);
      debugDepthQuad.Deletable = false;

      MMaterial debugmat = new MMaterial("DEBUG");
      debugmat.shader = debugDepthQuad;
      //debugmat.Deletable = false;
      debugQuad = new DebugQuad("DEBUGQUAD");
      debugQuad.material = debugmat;
      UtilityRoot.Add(debugQuad);
      debugQuad.Deletable = false;

      GUIRoot = new MGUI();
      Root.Add(GUIRoot);
      Helper.CheckGLError(this, "TestPoint 6");
    }

    public override void Setup()
    {
      Root.Setup();
    }

    void AddBackdrop()
    {
      Helper.CheckGLError(this, "TestPoint 3a");
      MShader BackgroundShader = new MShader("BackgroundShader");
      BackgroundShader.Load("default_v.glsl",
        "unlit_f.glsl",
        "Terrain\\eval.glsl", "Terrain\\control.glsl"
        );

      BackgroundShader.Bind();
      BackgroundShader.SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      Helper.CheckGLError(this, "TestPoint 3b");
      //SkyShader.SetInt("shadowMap", 2);

      MMaterial BGMat = new MMaterial("BackgroundMaterial");
      BGMat.AddShader(BackgroundShader);
      Helper.CheckGLError(this, "TestPoint 4a");
      MTexture BackgroundTexure = new MTexture("BackgroundT");
      Helper.CheckGLError(this, "TestPoint 4b");
      BackgroundTexure.LoadTextureData(MFileSystem.ProjectPath + "Assets\\Textures\\Planets\\8k_stars_milky_way.jpg");
      BackgroundTexure.DoAssign = true;

      MaterialRoot.Add(BGMat);
      BGMat.SetDiffuseTexture(BackgroundTexure);
      backdrop = new MSky();
      backdrop.AddMaterial(BGMat);
      UtilityRoot.Add(backdrop);
    }

    public int GetPick(Vector3d ScreenPos)
    {
      return ScreenPick.GetPick(ScreenPos);
    }

    // double c;


    public override void Update()
    {
      if (Playing == false) return;

      //Console.WriteLine(Globals.Tasks);


      MMessageBus.NotifyEarlyUpdate(this);
      if (Background.Modules.Count > 0)
      {
        Background.Update();
        Background2.Update();
      }
      ModelRoot.Update();
      UtilityRoot.Update();

      // MMessageBus.NotifyUpdate(this);
      Overlay.Update();
      MMessageBus.NotifyLateUpdate(this);
    }

    public void ClearBackBuffer()
    {
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    /**
     * During the Rendering phase all matrices are pre-multiplied (to bring coordinates back to local 32bit space)
     * and all offsets and set relative to the camera, to aid with shadow calculations - 
     * */
    public void Render()
    {
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      Globals.ShaderOverride = null;
      Globals.DrawCalls = 0;
      Globals.Index = 0;
      Globals.GlobalOffset = Camera.transform.Position;
      Globals.GlobalOffsetCalc = Camera.transform.Position;
      Matrix4d offsetmat = Matrix4d.CreateTranslation(-Globals.GlobalOffset);
      Matrix4d lightmatrix = light.GetLightSpaceMatrix();

      Matrix4d view = Camera.GetViewMatrix();
      Matrix4d projection = Camera.GetProjection(true);
      Matrix4d viewproj = view * projection;

      // GL.Viewport(0, 0, MScreen.Width, MScreen.Height);
      //============================
      // Globals.GlobalOffset = Vector3d.Zero;

      // GL.CullFace(CullFaceMode.Back);
      // reset viewport
      // 2. render scene as normal using the generated depth/shadow map  
      // --------------------------------------------------------------

      for (int i = 0; i < MaterialRoot.Modules.Count; i++)
      {
        MMaterial mat = (MMaterial)MaterialRoot.Modules[i];
        if (mat.IsUsed == false) continue;
        mat.shader.Bind();
        mat.shader.SetMat4("view", MTransform.GetFloatMatrix(view));
        mat.shader.SetMat4("projection", MTransform.GetFloatMatrix(projection));
        mat.shader.SetVec4("Tweak", new Vector4(Settings.Tweak1, Settings.Tweak2, Settings.Tweak3, Settings.Tweak4));

        // set light uniforms
        Fog.Bind(mat.shader);

        //TODO: use nearest lights
        mat.shader.SetInt("NumLights", LightRoot.Modules.Count);
        //TODO sort lights closest to furthest (because there is max lights =10 in shader)

        for (int il = 0; il < LightRoot.Modules.Count; il++)
        {
          if (il >= MAXLIGHTS) continue;
          //pointLightPositions[0]
          MPointLight p = (MPointLight)LightRoot.Modules[il];
          p.Bind(mat, il);
        }

        mat.shader.SetVec3("viewPos", MTransform.GetVector3(Camera.transform.Position - Globals.GlobalOffset));
        mat.shader.SetVec3("sunPos", MTransform.GetVector3(light.transform.Position -Globals.GlobalOffset));
        mat.shader.SetInt("Closeup", Closeup);
        light.Bind(mat);
      }

      /////////////////////// DEPTH LIGHT FOR SHADOWS ///////////////////////
      GL.Viewport(0, 0, MScreen.Width, MScreen.Height);

//      GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

      if (light.Shadows)
      {
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Back);
        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
        Globals.RenderPass = Globals.eRenderPass.ShadowDepth;
        //============================
        //render depthmap from light using depth shader        
        light.Render(lightmatrix, offsetmat);
        simpleDepthShader.Bind();

        Globals.ShaderOverride = simpleDepthShader;
        GL.DepthFunc(DepthFunction.Less);
        GL.Enable(EnableCap.DepthTest);
       
        //Background.Render(lightmatrix, offsetmat);
        //GL.Clear(ClearBufferMask.DepthBufferBit);
        Background2.Render(lightmatrix, offsetmat);        
        ModelRoot.Render(lightmatrix, offsetmat);
        Globals.RenderPass = Globals.eRenderPass.Normal;
        Globals.ShaderOverride = null;
      }

      //============================
      ////////////////// NORMAL RENDER ////////////////////
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      //Helper.CheckGLError(this, "Render 1");
      //set FBO      
      Renderer.Render(viewproj, offsetmat);
      //Helper.CheckGLError(this, "Render 1");

      //load the shadow map into texture slot 4
      GL.ActiveTexture(TextureUnit.Texture0 + MShader.LOCATION_SHADOWMAP);
      GL.BindTexture(TextureTarget.Texture2D, light.depthMap);
      ////////////////////////////////////////////////////////   
      //  GL.PatchParameter(PatchParameterInt.PatchVertices, 16);

      RenderScene(ref view, ref offsetmat, ref projection, ref viewproj);

      //////////// OVERLAY LAYER ////////////
      //============================    
      GL.Disable(EnableCap.DepthTest);
      GL.Clear(ClearBufferMask.DepthBufferBit);
      if (Overlay.Modules.Count > 0)
      {
        projection = Camera.GetOverlayProjection();
        view = Camera.GetOverlayViewMatrix();
        viewproj = view * projection;
        //UpdateOverlay();
        Overlay.Render(viewproj, Matrix4d.Identity);
      }

      if (Stereo == true)
      {
        GL.Viewport(0, 0, MScreen.Width / 2, MScreen.Height);
        Vector3d original = Camera.transform.Position;
        Camera.transform.Position -= Camera.transform.Right();
        view = Camera.GetViewMatrix();
        viewproj = view * projection;
        GL.Viewport(MScreen.Width / 2, 0, MScreen.Width / 2, MScreen.Height);
        RenderScene(ref view, ref offsetmat, ref projection, ref viewproj);
        Camera.transform.Position = original;
        GL.Viewport(0, 0, MScreen.Width, MScreen.Height);
      }

      Renderer.AfterRender();

      // GL.Disable(EnableCap.DepthTest);
      // GL.Disable(EnableCap.DepthTest);      
      // render Depth map to quad for visual debugging
      // ---------------------------------------------
      //GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
      if (Settings.DebugDepth == true)
      {
        debugQuad.Bind();

        debugQuad.material.shader.SetFloat("near_plane", light.NearPlane);
        debugQuad.material.shader.SetFloat("far_plane", light.FarPlane);
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, light.depthMap);
        GL.Clear(ClearBufferMask.DepthBufferBit);
        debugQuad.Render(lightmatrix, Matrix4d.Identity);
      }

     ///do not reset
      /// Globals.GlobalOffset = Vector3d.Zero;

      //projection = Camera.GetProjection(true);
      //GUIRoot.Render(Matrix4d.Identity, Matrix4d.Identity);




      ErrorCode err = GL.GetError();
      if (err != ErrorCode.NoError)
      {
        //Console.WriteLine("MScene Render:" + err);
      }

    }

    void RenderScene(ref Matrix4d view, ref Matrix4d offsetmat, ref Matrix4d projection, ref Matrix4d viewproj)
    {
      ///////////////// SKY ///////////////////  
      if (Settings.DrawBackdrop == true)
      {
        GL.Disable(EnableCap.DepthTest);
        backdrop.Render(view, offsetmat);
        GL.Enable(EnableCap.DepthTest);
        GL.Clear(ClearBufferMask.DepthBufferBit);
      }
      else
      {
        GL.ClearColor(Renderer.ClearColor);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      }

      //Far field 
      //GL.Disable(EnableCap.DepthTest);      
      //render a planet if we are near one
      if (MPlanetHandler.CurrentNear != null)
      {
        projection = Camera.GetFullProjection(MPlanetHandler.CurrentNear.AvatarDistanceToSurface / 2.0, 20000000);
        viewproj = view * projection;
        AstroRoot.Render(viewproj, offsetmat);
      }

      GL.Clear(ClearBufferMask.DepthBufferBit);

      //============================
      if (Background.Modules.Count > 0)
      {        
        projection = Camera.GetFullProjection();
        viewproj = view * projection;        
        Background.Render(viewproj, offsetmat);
        Background2.Render(viewproj, offsetmat);
      }      

      
      ModelRoot.Render(viewproj, offsetmat);

      // Near Field
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
      //============================      
      //GL.Clear(ClearBufferMask.DepthBufferBit);
      projection = Camera.GetProjection(true);
      viewproj = view * projection;

     // Background.Render(viewproj, offsetmat);
      //GL.Clear(ClearBufferMask.DepthBufferBit);
      ModelRoot.Render(viewproj, offsetmat);

      if (SelectedObject != null)
      {
        GL.Clear(ClearBufferMask.DepthBufferBit);
        ScreenPick.PrepareRender();
        SelectedObject.Render(viewproj, offsetmat);
        Globals.ShaderOverride = null; ;
        ScreenPick.AfterRender();
      }

      if (OnPostRender != null)
      {
        OnPostRender(this, new RenderEvent());
      }

      GL.ActiveTexture(TextureUnit.Texture0);
      GL.BindTexture(TextureTarget.Texture2D, 0);

      //GL.Enable(EnableCap.DepthTest);
      //////////////////////// MAIN RENDER COMPLETE

      if (Physics.DebugWorld == true)
      {
        Physics.Render(viewproj, offsetmat);
      }

      //selection overlay

      ///////////// SELECTION ///////////////////

     


      if (SelectionRoot.Modules.Count > 0)
      {
        //GL.Disable(EnableCap.DepthTest);
        ScreenPick.PrepareRender();
        // Globals.RenderSelectedOnly = true;
        //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        // GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);        
        SelectionRoot.Render(viewproj, offsetmat);
        Globals.ShaderOverride = null; ;
        //Globals.RenderSelectedOnly = false;
        ScreenPick.AfterRender();
      }
    }



    public override void Dispose()
    {
      if (MScene.Root != null)
      {
        MScene.Root.Dispose();
      }

      base.Dispose();
    }
  }
}
