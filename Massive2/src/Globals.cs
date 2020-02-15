using Massive.Network;
using Massive.Events;
using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive.Platform;

namespace Massive
{
  public static class Globals
  {
    public static int VERSION = 1;
    public static int Index = 0;
    public static int Editor = 1;
    public static int DrawCalls = 0;
    public static int Tasks = 0;
   // public static double Tweak1 = 8.5;
    //public static double Tweak2 = 0;

    public static double BuildThreshold = 20; //the closest distance a user can build to another user's block

    public static bool ApplicationExiting = false; //true when we're shutting down. Threads will monitor this 

    public static Control GUIThreadOwner;
    public static MScene _scene;

    public static bool RenderSelectedOnly = false;
    //when set, all objects will render with this shader. Set to null to disable override
    public static Vector3d LocalUpVector = Vector3d.UnitY;
    public static Vector3d LocalGravity = new Vector3d(0, -9.8, 0);
    public static Quaterniond LocalUpRotation()
    {
      //if (Globals.Avatar.Target == null) return Quaterniond.Identity;
      Quaterniond rot = Extensions.LookAt(Globals.Avatar.GetPosition(), 
        Globals.Avatar.GetPosition()+ Globals.LocalUpVector, Vector3d.UnitY);
      
      if (double.IsNaN(rot.X))
      {
        rot = Quaterniond.Identity;
      }
      return rot;
    }

    //Important: The offset required to bring the double-precision scene into single-precision range.
    public static Vector3d GlobalOffset = Vector3d.Zero;
    public static Vector3d GlobalOffsetCalc = Vector3d.Zero;

    public enum eRenderPass { Normal, ShadowDepth, Pick, Outline, FX, Reflection };
    public static eRenderPass RenderPass = eRenderPass.Normal;
    public static MShader ShaderOverride;
    public static event EventHandler<ErrorEvent> ShaderErrorEvent;    
    public static event EventHandler<StatusEvent> ShaderCompilerEvent;
    public static event EventHandler<ErrorEvent> ErrorEventGlobal;    
    public static MScriptHost ScriptHost;
    public static MassiveNetwork Network = new MassiveNetwork();
    public static MMessageBus MessageBus = new MMessageBus();
    public static TexturePool TexturePool;
    public static MAvatar Avatar;
    public static MUserAccount UserAccount = new MUserAccount();

    public static string LobbyDataURL = "http://www.bigfun.co.za/massive/lobby/";    
    
    public static void Log(object sender, string s)
    {
      //InfoEventGlobal?.Invoke(sender, new InfoEvent(s));
      Console.WriteLine(s);
    }    
  }
}
