using Massive;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using System;

/// <summary>
/// Controls a camera, animation, prevents clipping, etc
/// syncs the avatar position on the network (todo: move to avatar handler)
/// </summary>

namespace OpenWorld.Handlers
{
  public class MCameraHandler : MObject
  {
    MCamera _camera;

    double MaxSpeed = 0.002;
    double Speed = 15;
    Vector3d PreviousPosition = Vector3d.Zero;
    Vector3d PreviousTarget = Vector3d.Zero;
    double MaxNetworkThrottle = 0.5;
    double Throttle = 0;

    bool ThirdPerson = true;
    MPhysicsObject po;

    //the physical location of the camera
    Vector3d DestinationPosition;
    //the position of the camera, minus wall clipping
    Vector3d RenderedPosition;
    Vector3d TargetUp;

    //MSceneObject Target;

    public MCameraHandler()
      :base(EType.Other, "MCameraHandler")
    {
      //MScene.UtilityRoot.Add(this);
      _camera = MScene.Camera;

      Globals.Network.TeleportHandler += Network_TeleportHandler;
      //MMessageBus.UpdateHandler += MMessageBus_UpdateHandler;
      MStateMachine.StateChanged += MStateMachine_StateChanged;
    }

    private void MStateMachine_StateChanged(object sender, EventArgs e)
    {

    }

    private void MMessageBus_UpdateHandler(object sender, UpdateEvent e)
    {
     // Update();
    }

    private void Network_TeleportHandler(object sender, MoveEvent e)
    {
      MScene.Camera.TargetOffset = Vector3d.Zero;
    }

    //private void MMessageBus_TeleportEventHandler(object sender, Events.MoveEvent e)
    // {
    //_camera.Move(new Vector3d(e.Delta.X, 0, e.Delta.Z));
    // Speed = e.Locus;
    //}
    void CheckIfCloseToWall(Vector3d AP)
    {
      Vector3d result = Vector3d.Zero;
      MScene.Physics.RayCastRequest(AP, AP + Globals.Avatar.Forward() * 2.5, this, (RayResult) =>
      {
        if (RayResult.Result)
        {
          double d = Vector3d.Distance(result, AP);
          //MScene.Camera.NearPlane = d/6.0;
          MScene.Camera.NearPlane = 0.01;
          //MScene.Camera.FarPlane = 1000;
          MScene.Closeup = 1;
          // Console.WriteLine("Hit:" + (d.ToString()));
          //MMessageBus.NavigationEnable(this, false);
        }
        else
        {
          MScene.Camera.NearPlane = 1;
          //MScene.Camera.FarPlane = 1000000000;
          MScene.Closeup = 0;
          //MMessageBus.NavigationEnable(this, true);
        }
      });

    }

    void SetDestinationPosition(Vector3d AP)
    {
      if (Globals.Avatar.GetMoveMode() == MAvatar.eMoveMode.Walking)
      {
        double offy = Settings.OffsetThirdPerson.Y;
        if (Globals.Avatar.Target != null)
        {
          //offy += Globals.Avatar.Target.Offset.Y;
        }

        DestinationPosition = AP 
               + Globals.Avatar.Up() * (offy) 
               + Globals.Avatar.Up() * _camera.CameraOffset.Y
               - Globals.Avatar.Forward() * Settings.OffsetThirdPerson.Z 
               + Globals.Avatar.Forward() * _camera.CameraOffset.Z
               ;
      }
      else
      {
        DestinationPosition = AP 
               + Globals.Avatar.Up() * Settings.OffsetThirdPerson.Y
               + Globals.Avatar.Up() * _camera.CameraOffset.Y
               - Globals.Avatar.Forward() * Settings.OffsetThirdPerson.Z
               + Globals.Avatar.Forward() * _camera.CameraOffset.Z
               ;

        MScene.Camera.Focus.transform.Position = AP + Globals.Avatar.Forward() * 10;
      }
    }

    public void CheckClipping(Vector3d AP)
    {
      MRaycastTask task = MScene.Physics.RayCast(AP - Globals.Avatar.Forward() * 1 + Globals.Avatar.Up() * Globals.Avatar.height,
        DestinationPosition);

      if (task.Result == true)
      {
        RenderedPosition = AP 
            + Globals.Avatar.Up() * Settings.OffsetThirdPerson.Y
            - Globals.Avatar.Forward() * 0.5;
      }
      else
      {
        RenderedPosition = DestinationPosition;
      }
    }

    public void UpdateMovement(Vector3d AP)
    {
      double dist = Vector3d.Distance(MScene.Camera.transform.Position, DestinationPosition);

      double mult = 10.25;
      if (Globals.Avatar.MoveState == MAvatar.eMoveState.Run)
      {
        mult = 9;
      }

      if (Globals.Avatar.GetMoveMode() == MAvatar.eMoveMode.Flying)
      {
        mult = 7;
      }

      if (dist > 1000)
      {
        mult = 20;
      }
      //{
      //dist = MathHelper.Clamp(Speed * dist, 1, 10);
      /*
      MScene.Camera.transform.Position = RenderedPosition;

      MScene.Camera.Focus.transform.Position = MScene.Camera.transform.Position + Globals.Avatar.Forward() * 10
        + MScene.Camera.TargetOffset
        ;
        */
     
      MScene.Camera.transform.Position = Extensions.SmoothStep(MScene.Camera.transform.Position,
        RenderedPosition, mult * 0.05);
      MScene.Camera.Focus.transform.Position = Extensions.SmoothStep(MScene.Camera.Focus.transform.Position, 
        MScene.Camera.transform.Position + Globals.Avatar.Forward() * 10
        + MScene.Camera.TargetOffset, mult * 0.05);
     

      // }
      //else
      /*
            MScene.Camera.transform.Position = Vector3d.Lerp(MScene.Camera.transform.Position,
              RenderedPosition, mult * Time.DeltaTime);
            MScene.Camera.Focus.transform.Position = Vector3d.Lerp(MScene.Camera.Focus.transform.Position,
              MScene.Camera.transform.Position + Globals.Avatar.Forward() * 10
              + MScene.Camera.TargetOffset, mult * Time.DeltaTime);
              */
      {
        //MScene.Camera.transform.Position = RenderedPosition;        
        //MScene.Camera.Focus.transform.Position = AP + Globals.Avatar.Forward() * 10
        //+ MScene.Camera.TargetOffset;
      }

      Vector3d upv = MScene.Camera.UpVector;
      if (double.IsNaN(upv.X))
      {
        upv = Vector3d.UnitY;
      }
      if (double.IsNaN(TargetUp.X))
      {
        TargetUp = Vector3d.UnitY;
      }

      MScene.Camera.UpVector = Vector3d.Lerp(upv, TargetUp, 0.01 * Speed * 1);
    }

    /// <summary>
    /// If we have moved greated than 1m from our last position, inform the network
    /// TODO: move to avatar handler
    /// </summary>
    /// <param name="AP"></param>
    void CheckNetworkUpdating(Vector3d AP)
    {
      double dist = Vector3d.Distance(PreviousPosition, MScene.Camera.transform.Position);
      double td = Math.Abs(Vector3d.Distance(PreviousTarget, MScene.Camera.Focus.transform.Position));

      if (((dist > 0.25) || (td > 1))
        && (Throttle > MaxNetworkThrottle))
      {
        Throttle = 0;
        PreviousPosition = MScene.Camera.transform.Position;
        PreviousTarget = MScene.Camera.Focus.transform.Position;
        if (Globals.Network.Connected == true)
        {
          MMessageBus.MoveAvatarRequest(this, Globals.UserAccount.UserID, AP, Globals.Avatar.GetRotation());
        }
        else
        {
          //TODO: follow this event down the rabbit hole and optimize out where possible
          MMessageBus.AvatarMoved(this, Globals.UserAccount.UserID, AP, Globals.Avatar.GetRotation());
          //Console.WriteLine("Avatar " + Globals.Avatar.GetRotation());
        }
      }
    }

    public override void Update()
    {
      Throttle += Time.DeltaTime;

      Vector3d AP = Globals.Avatar.GetPosition();

      // CheckIfCloseToWall(AP + Globals.Avatar.Forward() * 0.1);        

      TargetUp = Globals.Avatar.Up();

      SetDestinationPosition(AP);
      //CheckClipping(AP);
      RenderedPosition = DestinationPosition;
      UpdateMovement(AP);
      CheckNetworkUpdating(AP);

    }

  }
}
