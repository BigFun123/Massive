using Massive.Graphics.Character;
using Massive2.Graphics.Character;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MNPC : MObject
  {
    public enum State { Idle, Talking, Walking, Running, TurnToHome };
    public State CurrentState = State.Idle;

    double accum = 0;
    double direction = -1;
    double speed = 0.5;
    Quaterniond OriginalRotation;
    Vector3d OriginalPosition;
    int StateCounter = 0;
    int StateLimit = 200;

    MAnimatedModel model;
    MPhysicsObject _physics;

    public MNPC(MSceneObject parent, string inname = "") : base(EType.NPCPlayer, inname)
    {
      OriginalRotation = parent.transform.Rotation;
      OriginalPosition = parent.transform.Position;
      model = (MAnimatedModel)parent;
      _physics = (MPhysicsObject)model.FindModuleByType(EType.PhysicsObject);
      _physics._rigidBody.Friction = 0.4;
    }

    void SetState(State NewState)
    {
      CurrentState = NewState;
      switch (CurrentState)
      {
        case State.Idle:
          model._animationController.PlayAnimation("idle", 1);
          break;
        case State.Walking:
          model._animationController.PlayAnimation("walk", 1);
          break;
        case State.Running:
          model._animationController.PlayAnimation("run", 1);
          break;
        case State.TurnToHome:
          model._animationController.PlayAnimation("walk", 1);
          break;
      }
    }

    public void CopyTo(MNPC t)
    {
      t.Name = Name;
      t.CurrentState = CurrentState;
    }

    void DoIdle()
    {
      /*
      accum += Time.DeltaTime * direction * speed;
      if (accum > 1)
      {
        direction = -direction;
        accum = 1;
      }
      if (accum <= -1)
      {
        accum = -1;
        direction = -direction;
      }
      MSceneObject msoParent = (MSceneObject)Parent;
      msoParent.SetRotation(OriginalRotation * Quaterniond.FromEulerAngles(0, accum, 0));


      if (Parent is MAnimatedModel)
      {
        MAnimatedModel m = (MAnimatedModel)Parent;
        m._animationController.PlayAnimation("idle", 1);
      }
      */
    }

    void DoWalking()
    {
      double v = 0.12 ;
      Vector3d vo = model.transform.Forward() * v; //OPENGL camera has inverse z    
      _physics.SetActive(true);
      _physics._rigidBody.ApplyCentralImpulse(vo);      
    }

    void DoRunning()
    {
      double v = 0.16;
      Vector3d vo = model.transform.Forward() * v; //OPENGL camera has inverse z    
      _physics.SetActive(true);
      _physics._rigidBody.ApplyCentralImpulse(vo);
    }

    void TurnToHome()
    {
      //Matrix4d m = Matrix4d.LookAt(model.transform.Position, OriginalPosition, Globals.LocalUpVector);
      //Matrix4d m = Matrix4d.RotateY(model.transform.Position, OriginalPosition, Globals.LocalUpVector);
      //Quaterniond q = m.ExtractRotation();
      //Quaterniond q = Quaterniond.FromAxisAngle(model.transform.Up(), 0.1);
      
     // model.SetRotation(q);

      Vector3d delta = model.transform.Position - OriginalPosition;
      Vector3d d2 = model.transform.Position + model.transform.Forward();
      double d = Vector3d.Dot(delta.Normalized(), d2.Normalized());

      Quaterniond s = Quaterniond.Slerp(model.transform.Rotation, 
        model.transform.Rotation * Quaterniond.FromEulerAngles(0, -d, 0),
        0.0150);
      model.SetRotation(s);
    }

    public override void Update()
    {
      StateCounter++;

      switch (CurrentState)
      {
        case State.Idle:
          DoIdle();
          if (StateCounter > StateLimit)
          {            
            SetState(State.Walking);
            StateCounter = 0;
          }
          break;
        case State.Walking:
          DoWalking();
          if (StateCounter > StateLimit)
          {
            SetState(State.Running);
            StateCounter = 0;
          }
          break;
        case State.Running:
          DoRunning();
          if (StateCounter > StateLimit)
          {
            SetState(State.TurnToHome);
            StateCounter = 0;            
          }
          break;
        case State.TurnToHome:
          TurnToHome();
          if (StateCounter > StateLimit)
          {
            SetState(State.Idle);
            StateCounter = 0;
            TurnToHome();
          }
          break;
      }
    }
  }
}
