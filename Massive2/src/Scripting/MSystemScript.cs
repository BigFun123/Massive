using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MSystemScript : MObject
  {
    internal string ClassName;
    MScriptHost _ScriptHost;
    MObject _ref;
    object _Activator;

    public MSystemScript(string inClassName, MObject Target)
      : base(EType.Script, "Script:" + inClassName)
    {
      _ref = Target;
      _ScriptHost = Globals.ScriptHost;
      ClassName = inClassName;
      // _compiler.CreateInstance(ClassName);
    }

    public void SetActivator(object o)
    {
      _Activator = o;
    }

    public override void Setup()
    {
      base.Setup();
      if (_Activator == null)
      {
        _Activator = _ScriptHost.CreateInstance(ClassName);
      }

      string result = _ScriptHost.Invoke(_Activator, "Init", new object[] { _ref });
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }

      result = _ScriptHost.Invoke(_Activator, "Setup", null);
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }
    }

    /*
    public void Prepare()
    {
      if (_Activator == null) return;
      string result = _compiler.Invoke(_Activator, "Prepare", null);
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }
    }
    */

    public override void OnClick(bool DoubleClick, bool RightClick)
    {
     // base.OnClick();
      if (_Activator == null) return;
      string result = _ScriptHost.Invoke(_Activator, "OnClick", null);
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }
      base.OnClick(DoubleClick);
    }

    public override void OnSelected(MSceneObject mo)
    {
      // base.OnClick();
      if (_Activator == null) return;
      string result = _ScriptHost.Invoke(_Activator, "OnSelected", new object[] { mo });
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }
      base.OnClick();
    }

    public override void Update()
    {
      if (Enabled == false) return;
     
      //call Update in the assembly
      if (_Activator == null) return;

      string result = _ScriptHost.Invoke(_Activator, "Update", null);
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }

      base.Update();
    }

    public override void OnKey(MKey key)
    {
      if (Enabled == false) return;
     
      //call Update in the assembly
      if (_Activator == null) return;

      string result = _ScriptHost.Invoke(_Activator, "OnKey", new object[] { key });
      if (!string.IsNullOrEmpty(result))
      {
        Console.WriteLine(result);
      }

      base.OnKey(key);
    }

    public override void Dispose()
    {
      base.Dispose();
      _Activator = null;
    }
  }
}
