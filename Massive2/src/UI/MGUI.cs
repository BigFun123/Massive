using Massive.Tools;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * TODO: Create a FBO to check against
 * 
 * */

namespace Massive
{
  public class MGUI : MControl
  {
    public static MGUI _instance;
    Matrix4d _projectionMatrix;
    Matrix4d _viewMatrix;
    MObject Clicked;
    public MMaterial Material;

    public MGUI(string sName = "GUI") : base(sName)
    {
      _instance = this;
      CreateGeometry = false;
      _projectionMatrix = Matrix4d.CreateOrthographicOffCenter(0, 100, 0, 100, 0.01, 50);
      _viewMatrix = Matrix4d.LookAt(new Vector3d(0, 0, 10), new Vector3d(0, 0, 0), new Vector3d(0, 1, 0));
    }

    public override void Setup()
    {
      material = ((MMaterial)MScene.MaterialRoot.FindModuleByName("DefaultGUIMaterial"));
      base.Setup();
    }

    public static MPanel AddPanel(MControl parent, double x, double y, double w, double h, string sName)
    {
      MPanel p = new MPanel(sName);
      p.transform.Position = new Vector3d(x, y, 0);
      //p.transform.Scale = new Vector3d(w, h, 0);
      p.Width = w;
      p.Height = h;
      if (parent == null)
      {
        _instance.Add(p);
      }
      else
      {
        parent.Add(p);
      }
      return p;
    }

    public static MText AddText(MControl parent, double x, double y, double w, double h, string sName, string sText)
    {
      MText p = new MText(sName);
      p.transform.Position = new Vector3d(x, y, 0);
      //p.transform.Scale = new Vector3d(w, h, 0);
      p.Width = w;
      p.Height = h;
      p.Text = sText;
      if (parent == null)
      {
        _instance.Add(p);
      }
      else
      {
        parent.Add(p);
      }
      return p;
    }

    public static MButton AddButton(MControl parent, double x, double y, double w, double h, string sName, string sText, string sIconPath = "UI\\Art\\button-bg-default.png")
    {
      MButton p = new MButton(sName, sIconPath);
      p.transform.Position = new Vector3d(x, y, 0);
      //p.transform.Scale = new Vector3d(w, h, 0);
      p.Width = w;
      p.Height = h;
      p.Text = sText;
      if (parent == null)
      {
        _instance.Add(p);
      }
      else
      {
        parent.Add(p);
      }
      return p;
    }

    public static MSplash AddSplash(string ImagePath)
    {
      MSplash splash = new MSplash();
      splash.SetBackground(ImagePath);
      splash.Width = 100;
      splash.Height = 100;
      MScene.GUIRoot.Add(splash);
      return splash;
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      // GL.Disable(EnableCap.DepthTest);
      //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
      GL.Disable(EnableCap.CullFace);
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
      if ((Globals.ShaderOverride == null) && (material != null))
      {
        if (material.shader != null)
        {
          material.shader.Bind();
        }
      }
      GL.BindTexture(TextureTarget.Texture2D, 0);

      base.Render(_viewMatrix * _projectionMatrix, Matrix4d.Identity);
      GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public void RenderPick()
    {
      MScreenPick.Instance.RenderPick(_viewMatrix * _projectionMatrix, this);
    }

  }
}
