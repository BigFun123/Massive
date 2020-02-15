using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Renders every object with a unique color and returns the selected object
 * 
 * */

namespace Massive.Tools
{
  public class MScreenPick : MObject
  {
    public MMaterial pickmaterial;
    public MMaterial outlinematerial;
    public static MScreenPick Instance;
    // public int Index;

    DepthFunction df;
    PolygonMode pm;

    public MScreenPick() : base(EType.Other, "ScreenPick")
    {
      Instance = this;
    }

    public override void Setup()
    {
      pickmaterial = new MMaterial("ScreenPick");
      pickmaterial.shader = new MShader("ScreenPickShader");
      pickmaterial.shader.Load("default_v.glsl", 
        "pick_select_f.glsl",        
        "Terrain\\eval.glsl", "Terrain\\control.glsl");
      Add(pickmaterial);

      outlinematerial = new MMaterial("OutlineMaterial");
      outlinematerial.shader = new MShader("OutlineShader");
      outlinematerial.shader.Load("default_v.glsl", 
        "outline_f.glsl",        
        "Terrain\\eval.glsl", "Terrain\\control.glsl");
      Add(outlinematerial);
    }

    public void PrepareRender()
    {      
      df = (DepthFunction)GL.GetInteger(GetPName.DepthFunc);
     
      if (outlinematerial != null)
      {
        Globals.ShaderOverride = outlinematerial.shader;
        Globals.ShaderOverride.Bind();
      }     
      // Render the object
    }

    public void AfterRender()
    {
      //Globals.ShaderOverride = null;
      GL.Disable(EnableCap.PolygonOffsetFill);
      GL.PolygonOffset(0, 0);      
      // GL.DepthFunc(df);
      // GL.Enable(EnableCap.DepthTest);
      GL.Enable(EnableCap.DepthTest);
      Globals.ShaderOverride = null;
    }
    public int GetPick(Vector3d ScreenPos)
    {
      float[] vals = { 0, 0, 0 };
      GL.ReadBuffer(ReadBufferMode.Back);
      int sx = (int)ScreenPos.X;
      int sy = (int)ScreenPos.Y;
      GL.ReadPixels<float>(sx, sy, 1, 1, PixelFormat.Rgb, PixelType.Float, vals);
      int index = RGBtoInt(new Vector3(vals[0], vals[1], vals[2]));
      if (index == 16777215) return -1; //pure white, ie all 1,1,1
      return index;
    }

    public void RenderPick(MObject inRoot)
    {
      Globals.Index = 2;
      GL.DepthFunc(DepthFunction.Less);
      GL.Enable(EnableCap.DepthTest);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      Matrix4d view = MScene.Camera.GetViewMatrix();

      //double mid = MScene.Camera.MidPlane;
      //double far = MScene.Camera.FarPlane;
      //MScene.Camera.MidPlane = far;
      Matrix4d projection = MScene.Camera.GetProjection(true);
      Matrix4d viewproj = view * projection;
      //MScene.Camera.MidPlane = mid;
      Globals.RenderPass = Globals.eRenderPass.Pick;
      RenderPick(viewproj, inRoot);
    }

    public void RenderPick(Matrix4d viewproj, MObject Root)
    {
      GL.Viewport(0, 0, MScreen.Width, MScreen.Height);
      // Globals.Index = 0;
      if ( pickmaterial == null )
      {
        Console.Error.WriteLine("PickMaterial is NULL");
        return;
      }
      Globals.ShaderOverride = pickmaterial.shader;
      pickmaterial.shader.Bind();
      GL.ClearColor(Color4.White);
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

      Globals.Index++;
      Root.Index = Index;

      Vector3 rgb = IntToRgb(Index);
      int ccc = RGBtoInt(rgb);
      Matrix4d offsetmat = Matrix4d.CreateTranslation(-Globals.GlobalOffset);

      pickmaterial.shader.SetVec3("object_index", rgb);
      Root.Render(viewproj, offsetmat);

      Globals.ShaderOverride = null;
    }

    public Vector3 IntToRgb(int value)
    {
      byte red = (byte)((value >> 0) & 255);
      float fred = red / 255.0f;
      byte green = (byte)((value >> 8) & 255);
      float fgreen = green / 255.0f;
      byte blue = (byte)((value >> 16) & 255);
      float fblue = blue / 255.0f;
      return new Vector3(fred, fgreen, fblue);
    }

    public Vector3 IntToRgbb(int value)
    {
      float red = (value >> 0) & 255;
      float green = (value >> 8) & 255;
      float blue = (value >> 16) & 255;

      return new Vector3(red, green, blue);
    }

    public int RGBtoInt(Vector3 c)
    {
      int r = (int)(c.X * 255);
      int g = (int)(c.Y * 255);
      int b = (int)(c.Z * 255);
      return (r << 0) | (g << 8) | (b << 16);
    }

    public int RGBtoInt(int r, int g, int b)
    {
      return (r << 0) | (g << 8) | (b << 16);
    }
  }
}
