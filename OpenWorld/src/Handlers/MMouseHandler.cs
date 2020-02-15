using Massive;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Drawing;
using System.Windows.Forms;
using static Massive.MObject;

namespace OpenWorld
{
  class MMouseHandler
  {
    MScene _Scene;

    public static bool isDown = false;
    bool isInitialized = false;
    Point DownPoint = new Point(0, 0);
    Point FirstDownPoint = new Point(0, 0);
    GLControl _glcontrol;
    public EventHandler<InfoEvent> MouseInfo;

    double accx = 0;
    double accy = 0;

    public MMouseHandler(GLControl c)
    {
      if (Globals._scene == null)
      {
        Console.WriteLine("MMouseHandler ERROR: Scene is not initialized");
        return;
      }
      _Scene = Globals._scene;

      _glcontrol = c;

      c.MouseWheel += GlControl1_MouseWheel;
      c.MouseDown += C_MouseDown;
      c.MouseUp += C_MouseUp;
      c.MouseMove += C_MouseMove;
      c.MouseDoubleClick += C_MouseDoubleClick;

      MMessageBus.ChangeModeHandler += MMessageBus_ChangeModeHandler;
      Globals.Network.TeleportHandler += Network_TeleportHandler;
      MMessageBus.AvatarChangedHandler += MMessageBus_AvatarChangedHandler;
    }

    private void MMessageBus_AvatarChangedHandler(object sender, ChangeAvatarEvent e)
    {
      Initialize();
    }

    private void Network_TeleportHandler(object sender, MoveEvent e)
    {
      if (e.InstanceID == null) return;
      if (e.InstanceID.Equals(Globals.UserAccount.UserID))
      {
        Reset();
      }
    }

    private void MMessageBus_ChangeModeHandler(object sender, ChangeModeEvent e)
    {
      Reset();
    }

    public void Reset()
    {
      // Globals.Avatar.SetRotation(MScene.Camera.transform.Rotation);
      //Globals.Avatar.SetRotation(MScene.Camera.transform.Rotation);
      //MScene.Camera.transform.Position = Globals.Avatar.GetPosition();
      //MScene.Camera.Target.transform.Position = MScene.Camera.transform.Forward() * 10;

      accx = 0;
      accy = 0;
    }

    private void C_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        //Globals.Avatar.Hide();
        CheckPick(e.Location, true);
        //Globals.Avatar.Show();
      }
      MMessageBus.Click(this, e.Location, e.Button == MouseButtons.Left ? 0 : 1);

    }

    public static double GetMouseMult()
    {
      double mult = 1;
      if ((Control.ModifierKeys & Keys.Control) == Keys.Control) mult = 0.0001;
      if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) mult = 0.1;
      //if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) mult = 100;
      return mult;
    }

    void Initialize()
    {
      if (isInitialized == false)
      {
        isInitialized = true;
        RotateAvatarWalking(new Point(10, 0));
      }
    }

    private void C_MouseMove(object sender, MouseEventArgs e)
    {
      // Initialize();

      if (isDown == true)
      {
        if (MStateMachine.CurrentState == MStateMachine.eStates.Building)
        {
          RotateCameraAroundTarget(e.Location);
        }

        if (MStateMachine.CurrentState == MStateMachine.eStates.Viewing)
        {
          if (Globals.Avatar.GetMoveMode() == MAvatar.eMoveMode.Walking)
          {
            RotateAvatarWalking(e.Location);
          }
          else
          {
            RotateAvatarSpace(e.Location);
          }
        }
      }
    }

    void RotateAvatarWalking(Point e)
    {
      //Vector3d tp = Vector3d.Zero;
      //tp = MScene.Camera.transform.Position;

      double mult = GetMouseMult();
      double dy = (DownPoint.Y - e.Y) * mult;
      double dx = (DownPoint.X - e.X) * mult;

      accx += dx;

      Vector3d op = MScene.Camera.Focus.transform.Position;

      //Quaterniond rota = Extensions.LookAt(Globals.Avatar.GetPosition(),
      //         Globals.Avatar.GetPosition() + Globals.LocalUpVector, Vector3d.UnitY);
      Quaterniond rota = Extensions.LookAt(MScene.Camera.transform.Position,
               MScene.Camera.transform.Position + Globals.LocalUpVector, Vector3d.UnitY);

      if (double.IsNaN(rota.X))
      {
        //rota = Quaterniond.Identity;
        return;
      }

      Quaterniond rot = rota
      * Quaterniond.FromEulerAngles(0, 0.8 * accx * (Math.PI / 180.0), 0);

      //Quaterniond rot2 =        
      //Quaterniond.FromEulerAngles(0, dx, 0);
      Globals.Avatar.SetRotation(rot);
      Globals.Avatar.InputRollHDirect(accx * 100);
      double dist = Vector3d.Distance(MScene.Camera.transform.Position, MScene.Camera.transform.Position + MScene.Camera.TargetOffset);
      if (dist < 90)
      {
        MScene.Camera.TargetOffset += Globals.LocalUpVector * dy * 0.05;
      }
      else
      {
        MScene.Camera.TargetOffset *= 0.999;
      }

      DownPoint = e;
    }

    void RotateAvatarSpace(Point e)
    {
      double mult = GetMouseMult();
      double dy = (e.Y - DownPoint.Y);
      double dx = (e.X - DownPoint.X) * mult;

      //Quaterniond rot = Globals.Avatar.GetTargetRotation()
      //* Quaterniond.FromEulerAngles(0, -dx * Math.PI / 180.0, 0)
      //* Quaterniond.FromEulerAngles(0, 0, dy * Math.PI / 180.0);

      //Globals.Avatar.SetRotation(rot);
      Globals.Avatar.InputYawHDirect(-dx * 0.05);
      Globals.Avatar.InputPitchVDirect(dy * 0.15);

      DownPoint = e;

      /*
      Vector3d tp = Vector3d.Zero;
      tp = MScene.Camera.transform.Position;

      double mult = GetMouseMult();
      double dy = (DownPoint.Y - e.Y) * mult;
      double dx = (DownPoint.X - e.X) * mult;

      accx += dx * 0.017;
      accx = MathHelper.Clamp(accx, -0.05, 0.05);
      accy += dy * 0.017;
      accy = MathHelper.Clamp(accy, -0.05, 0.05);

      Quaterniond rot = Globals.Avatar.GetRotation()
        * Quaterniond.FromEulerAngles(0, accx, 0)
      * Quaterniond.FromEulerAngles(0, 0, -accy);

      Globals.Avatar.SetRotation(rot);

      DownPoint = e;
      */
    }

    void RotateCameraAroundTarget(Point e)
    {
      Vector3d tp = Vector3d.Zero;
      if (MScene.Camera.Focus != null)
      {
        tp = MScene.Camera.Focus.transform.Position;
      }

      ///Globals.GlobalOffset

      double mult = GetMouseMult();
      double dy = (DownPoint.Y - e.Y) * mult;
      double dx = (DownPoint.X - e.X) * mult;

      Vector3d op = MScene.Camera.transform.Position;
      Quaterniond q = Quaterniond.FromEulerAngles(0, dx, 0);
      Vector3d p = RotateOffset(op - tp, -dx);

      double dist = 0;
      Vector3d.Distance(ref MScene.Camera.Focus.transform.Position, ref MScene.Camera.transform.Position, out dist);

      MScene.Camera.transform.Position = p + tp + new Vector3d(0, dist * -dy, 0);

      DownPoint = e;
    }

    public Vector3d RotateOffset(Vector3d op, double dx)
    {
      return new Vector3d(Math.Cos(dx) * op.X - Math.Sin(dx) * op.Z,
         op.Y,
         Math.Sin(dx) * op.X + Math.Cos(dx) * op.Z);
    }

    public Vector3d RotatePointAroundPivot(Vector3d point, Vector3d pivot, Quaterniond rotation)
    {
      Vector3d direction = point - pivot; //Get point's direction relative to the pivot
      direction = rotation * direction; //rotate the direction
      point = direction + pivot; //Calculate rotated point
      return point;
    }

    private void C_MouseUp(object sender, MouseEventArgs e)
    {
      isDown = false;
      //accx =0;
      //accy =0;


      double d = Vector2.Distance(new Vector2(e.Location.X, e.Location.Y), new Vector2(FirstDownPoint.X, FirstDownPoint.Y));

      if ((e.Button == MouseButtons.Left) && (d < 3))
      {
        //hide own avatar
        bool previous = true;
        //if (Globals.Avatar.Target != null)
        //{
        //previous = Globals.Avatar.Target.Visible;
        //}

        CheckPick(e.Location, false);

        //if (Globals.Avatar.Target != null)
        //{
        //Globals.Avatar.Target.Visible = previous;
        //}
      }

      if ((e.Button == MouseButtons.Right) && (d < 3))
      {
        CheckPick(e.Location, false, true);
      }

      if (e.Button == MouseButtons.Right)
      {
        MMessageBus.Select(this, null);
      }


      //MMessageBus.Click(this, e.Location, e.Button == MouseButtons.Left ? 0 : 1);
    }

    private void C_MouseDown(object sender, MouseEventArgs e)
    {
      isDown = true;
      DownPoint = e.Location;
      FirstDownPoint = e.Location;
    }

    void CheckPick(Point p, bool DoubleClick = false, bool RightClick = false)
    {
      GL.Enable(EnableCap.DepthTest);
      GL.ClearColor(Color.Black);
      GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

      MScene.ScreenPick.RenderPick(MScene.ModelRoot);
      //_glcontrol.SwapBuffers();

      Vector3d p3d = new Vector3d(p.X, _glcontrol.Height - p.Y, 0);
      int index = _Scene.GetPick(p3d);
      //Log("Pick id:" + index + " @" + p.ToString());
      if (index != -1)
      {
        //Console.WriteLine("Clicked:" + index);
        MObject mobj = MScene.ModelRoot.FindModuleByIndex(index, null);
        if (mobj == null) return;
        if (!mobj.Renderable) return;
        MSceneObject mo = (MSceneObject)mobj;

        if (mo != null)
        {
          if ((mo.Type == MObject.EType.Mesh) && (mo.Parent.Renderable))
          {
            mo = (MSceneObject)mo.Parent;
          }
          else { return; }
          //Console.WriteLine("Pick id:" + index + " @" + p.ToString() + " Owner:" + mo.OwnerID);
          if (mo.OwnerID == null)
          {
            MMessageBus.Status(this, "ID: + " + mo.Index + " Object " + mo.Name + " with ID:" + mo.InstanceID + " has no owner");
          }
          else
          if (!mo.OwnerID.Equals(Globals.UserAccount.UserID))
          {
            MMessageBus.Status(this, "ID:" + mo.Index + " Object " + mo.Name + "," + mo.InstanceID);
          }
          // else
          {
            mo.OnClick(DoubleClick, RightClick);
            MMessageBus.Select(this, new SelectEvent(mo));
            MMaterial m = (MMaterial)mo.FindModuleByType(EType.Material);
            MMessageBus.Status(this, "Selected:" + mo.TemplateID + "," + mo.InstanceID);
          }
        }


        MScene.SelectedObject = mo;
      }
      else
      {
        MScene.SelectedObject = null;
      }
    }

    private void GlControl1_MouseWheel(object sender, MouseEventArgs e)
    {
      Globals.Avatar.MouseWheel(e.Delta > 0 ? 1 : -1);
      /*
      if (MStateMachine.CurrentState == MStateMachine.eStates.Viewing)
      {
        double mult = 1;
        if ((Control.ModifierKeys & Keys.Control) == Keys.Control) mult = 0.31;
        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) mult = 2.0;
        if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) mult = 5.0;

        if (e.Delta < 0)
        {
          //Globals.Avatar.SetPosition(Globals.Avatar.GetPosition() + Globals.Avatar.Forward() * mult);
          Globals.Avatar.InputV(Time.DeltaTime * 200 * mult);
        }
        else
        {
          Globals.Avatar.InputV(Time.DeltaTime * -200 * mult);
        }
      }
      */
      //if (Math.Abs(_Scene.camera.Offset.X + _Scene.camera.Offset.Y + _Scene.camera.Offset.Z) < 0.1)
      //{
      //        _Scene.camera.Offset = new Vector3d(1, 1, 1);
      //}
      //_Scene.camera.Offset *= e.Delta * mult;      
    }
  }
}
