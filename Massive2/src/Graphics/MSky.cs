using OpenTK;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MSky : MSceneObject
  {

    Matrix4d _projectionMatrix;
    MObjectAnimation animation;
    MSphere SkyBall;

    public double Speed
    {
      get => animation.Speed;
      set => animation.Speed = value;
    }

    public MSky()
      : base(EType.Sky, "Sky")
    {
      Renderable = true;
      //Prepare();
      //RenderSettings rs = new RenderSettings();
      //rs.RenderCullMode = CullFaceMode.Front;
      //rs.RenderDepthBuffer = false;
      //rs.Enabled = false;
      // this.AddModule(rs);

      // animation = new MAnimation();
      // animation.Speed.Rotation = Quaterniond.FromEulerAngles(0, 0.0006, 0);
      // animation.Enabled = true;
      // this.AddModule(animation);

      //_projectionMatrix = Matrix4d.CreateOrthographic(800, 600, 2, 200000);
      _projectionMatrix = Matrix4d.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(90), 1024 / 768, 2, 20000000);
    }

    public override void Setup()
    {
      if (SkyBall == null)
      {
        SkyBall = new MSphere("SkyBall", 3);
        SkyBall.AddMaterial(this.material);
        SkyBall.transform.Position = new Vector3d(0, 0, 0);
        //SkyBall.transform.Scale = new Vector3d(1000, 1000, 1000);      
        SkyBall.transform.Rotation = Quaterniond.FromEulerAngles(0, 0, 0);
        SkyBall.transform.Scale = new Vector3d(30, 30, 30);
        Add(SkyBall);

        animation = new MObjectAnimation("SkyAni");
        animation.AngleOffset = Quaterniond.FromEulerAngles(0, 0.004, 0);
        animation.Speed = 1;
        animation.Enabled = true;
        SkyBall.Add(animation);
      }

      base.Setup();
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (Enabled == false) return;
      //material.Bind();      
        GL.Enable(EnableCap.CullFace);
      GL.CullFace(CullFaceMode.Front);
      GL.Disable(EnableCap.DepthTest);
     
      if (SkyBall != null)
      {
        SkyBall.transform.Position = this.transform.Position;
        SkyBall.Render(viewproj * _projectionMatrix, Matrix4d.Identity);
      }
     

      GL.Enable(EnableCap.DepthTest);
      GL.CullFace(CullFaceMode.Back);

      
    }

    public override void Update()
    {
      base.Update();
    }

  }
}
