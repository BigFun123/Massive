using Massive;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Handlers
{
  class MKeyboardHandler
  {
    Form Parent;
    public static bool ShiftDown = false;
    public static bool ControlDown = false;
    public static bool AltDown = false;
    public static bool[] KeyState = new bool[255];

    public MKeyboardHandler(Form f, Control c)
    {
      Parent = f;
      c.KeyDown += C_KeyDown;
      c.KeyUp += C_KeyUp;
      f.KeyDown += FormKey;
      //f.GetGraView().KeyDown += KeyboardHandler_KeyDown;
    }

    private void C_KeyUp(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.ShiftKey:
          ShiftDown = false;
          break;
        case Keys.F2:
          MMessageBus.ChangeModeRequest(this, MAvatar.eMoveMode.Walking);
          break;
        case Keys.F3:
          MMessageBus.ChangeModeRequest(this, MAvatar.eMoveMode.Flying);
          break;
        case Keys.F4:
          MMessageBus.ToggleGravity(this);
          break;
        case Keys.F:
          MMessageBus.Focus(this, null);
          break;
        case Keys.Y:
          CreateTest();
          break;
      }
    }

    void FormKey(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          //HelpForm hf = new HelpForm();
          //hf.Show();
          break;
      }
    }
    public void HandleKey(KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.ShiftKey:
          ShiftDown = true;
          break;
        case Keys.Add:
          MMessageBus.Rotate(this, new Quaterniond(0, 45 * Math.PI / 180.0, 0));
          break;
        case Keys.Subtract:
          MMessageBus.Rotate(this, new Quaterniond(0, -45 * Math.PI / 180.0, 0));
          break;
      }

    }

    void CreateTest()
    {
      for (int i = 0; i < 1000; i++)
      {
        Vector3d pos = Globals.Avatar.Target.transform.Position + Globals.LocalUpVector * i;

        MCube c = (MCube)Helper.CreateCube(MScene.ModelRoot, "Cube" + i, pos);
        MPhysicsObject po = new MPhysicsObject(c, "cube", 0.1, MPhysicsObject.EShape.Box, false, Vector3d.One);
      }
    }

    public static double GetMinifier()
    {
      double mult = 1;

      if (Control.ModifierKeys == Keys.Shift)
      {
        mult = 0.1;
      }

      if (Control.ModifierKeys == Keys.Control)
      {
        mult = 0.01;
      }

      if (Control.ModifierKeys == Keys.Alt)
      {
        mult = 10.0;
      }
      return mult;
    }

    public static double GetRotationMinifier()
    {
      double mult = 15;

      if (Control.ModifierKeys == Keys.Shift)
      {
        mult = 1;
      }

      if (Control.ModifierKeys == Keys.Control)
      {
        mult = 0.01;
      }

      if (Control.ModifierKeys == Keys.Alt)
      {
        mult = 90.0;
      }
      return mult;
    }

    public static double GetMultiplier()
    {
      double mult = 1;

      if (Control.ModifierKeys == Keys.Shift)
      {
        if (Globals.Avatar.GetMoveMode() == MAvatar.eMoveMode.Walking)
        {
          mult *= 2;
        }
        else
        {
          mult *= 10;
        }
      }

      if (Control.ModifierKeys == Keys.Control)
      {
        mult *= 100;
      }

      if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
      {
        mult *= 10000;
      }

      if (Control.ModifierKeys == Keys.Alt)
      {
        mult *= 0.1;
      }
      return mult;
    }

    private void C_KeyDown(object sender, KeyEventArgs e)
    {
      HandleKey(e);
    }

    public void ClearState()
    {
      for (int i = 0; i < KeyState.Length; i++)
      {
        KeyState[i] = false;
      }
    }

    public void UpdateWalking()
    {
      //ShiftDown = KeyState[(int)Keys.ShiftKey];
      //ControlDown = KeyState[(int)Keys.ControlKey];
      //AltDown = KeyState[(int)Keys.CapsLock];
      double mult = GetMultiplier() * 6;

      if (KeyState[(int)Keys.Space])
      {
        Globals.Avatar.Jump(mult * 0.40 * 6);
      }

      if (KeyState[(int)Keys.W])
      {
        if (ShiftDown == true)
        {
          Globals.Avatar.Run(mult * 0.025);
        }
        else
        {
          Globals.Avatar.Walk(mult * 0.022);
          //Globals.Avatar.InputV(Time.DeltaTime * mult);
        }

      }
      if (KeyState[(int)Keys.A])
      {
        if (ShiftDown == true)
        {
          Globals.Avatar.RunTurn(Time.DeltaTime * (1.0 / mult) * 100);
        }
        else
        {
          Globals.Avatar.Turn(0.01* (1.0 / mult) * 100);
        }
        //Globals.Avatar.InputYawH(Time.DeltaTime * (1.0/mult) *100);
      }

      if (KeyState[(int)Keys.S])
      {
        if (ShiftDown == true)
        {
          Globals.Avatar.Run(Time.DeltaTime * -mult);
        }
        else
        {
          Globals.Avatar.Walk(Time.DeltaTime * -mult);
          //Globals.Avatar.InputV(Time.DeltaTime * mult);
        }
        //Globals.Avatar.InputV(Time.DeltaTime * -mult);
      }

      if (KeyState[(int)Keys.D])
      {
        if (ShiftDown == true)
        {
          Globals.Avatar.RunTurn(-Time.DeltaTime * (1.0 / mult) * 100);
        }
        else
        {
          Globals.Avatar.Turn(-0.01* (1.0 / mult) * 100);
        }

      }
    }

    void UpdateFlying()
    {
      double mult = GetMultiplier();

      double BaseEnergy = 10;

      if (KeyState[(int)Keys.Escape])
      {
        //Globals.Avatar.InputB1(Time.DeltaTime * mult * 40);
        Globals.Avatar.Reset();
      }

      if (KeyState[(int)Keys.Space])
      {
        Globals.Avatar.Jump(mult * 40);
      }

      if (KeyState[(int)Keys.R])
      {
        //Globals.Avatar.InputB1(BaseEnergy * mult);
        Globals.Avatar.Controller.Throttle(mult * 0.5);
      }

      if (KeyState[(int)Keys.F])
      {
        //Globals.Avatar.InputB1(BaseEnergy * -mult );
        Globals.Avatar.Controller.Throttle(-mult * 0.5);
      }

      if (KeyState[(int)Keys.W])
      {
        Globals.Avatar.Controller.Pitch(BaseEnergy * 0.01);
      }

      if (KeyState[(int)Keys.S])
      {
        Globals.Avatar.Controller.Pitch(-BaseEnergy * 0.01);
      }

      if (KeyState[(int)Keys.Up])
      {
        Globals.Avatar.InputPitchV(BaseEnergy);
      }

      if (KeyState[(int)Keys.Down])
      {
        Globals.Avatar.InputPitchV(-BaseEnergy);
      }

      if (KeyState[(int)Keys.Q])
      {
        // Globals.Avatar.InputRollHDirect(-BaseEnergy * mult * 0.05);
        Globals.Avatar.Controller.Yaw(BaseEnergy * 0.01);
      }
      if (KeyState[(int)Keys.E])
      {
        //Globals.Avatar.InputRollHDirect( BaseEnergy * mult * 0.05);
        Globals.Avatar.Controller.Yaw(-BaseEnergy * 0.01);
      }

      if (KeyState[(int)Keys.Left])
      {
        Globals.Avatar.InputRollHDirect(-BaseEnergy);
      }
      if (KeyState[(int)Keys.Right])
      {
        Globals.Avatar.InputRollHDirect(BaseEnergy);
      }

      if (KeyState[(int)Keys.A])
      {
        //Globals.Avatar.InputYawHDirect(BaseEnergy * mult * 0.01);
        Globals.Avatar.Controller.Bank(-BaseEnergy * mult * 0.06);
      }
      if (KeyState[(int)Keys.D])
      {
        //Globals.Avatar.InputYawHDirect(-BaseEnergy * mult * 0.01);
        Globals.Avatar.Controller.Bank(BaseEnergy * mult * 0.06);
      }
    }

    public void Update()
    {
      if (Globals.Avatar.GetMoveMode() == MAvatar.eMoveMode.Walking)
      {
        UpdateWalking();
      }
      else
      {
        UpdateFlying();
      }
    }

  }
}
