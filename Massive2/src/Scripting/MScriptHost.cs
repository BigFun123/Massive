using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Massive.Events;
using Massive.Platform;

namespace Massive
{  
  public class MScriptHost : MObject
  {
    public event EventHandler<ErrorEvent> ScriptError;

    AppDomain MassiveDomain;
    CSharpCodeProvider provider = new CSharpCodeProvider();
    CompilerParameters parameters = new CompilerParameters();
    CompilerResults results = null;    
    object  MainActivator;
    
    //object invoked;  
    Assembly ScriptsDLL;

    public MScriptHost() : base(EType.ScriptHost, "ScriptHost")
    {

    }

    public object GetMainActivator()
    {
      return MainActivator;   
    }

    public override void Dispose()
    {
      MainActivator = null;
    }

    public void LoadDLL()
    {
      if (MassiveDomain != null)
      {
        AppDomain.Unload(MassiveDomain);
        MassiveDomain = null;
      }
      MassiveDomain = AppDomain.CreateDomain("MassiveScripts");

      string absPath = MFileSystem.ProjectPath + "bin\\Scripts.dll";
      if (!File.Exists(absPath)) return;


      //TODO: Replace this with a domain when we can load dlls from the bin folder
      //load non locking
      byte[] b = File.ReadAllBytes(absPath);
      ScriptsDLL = Assembly.Load(b);

      //Assembly processLibrary = MassiveDomain.Load(absPath);

      /*
      Loader loader = (Loader)MassiveDomain.CreateInstanceAndUnwrap(
        typeof(Loader).Assembly.FullName,
        typeof(Loader).FullName);      
      loader.LoadAssembly(absPath);

      loader.ExecuteStaticMethod(
        "Massive.Startup",
        "Test",
        new object[] { @"Hello from Massive" });
        */

      //ScriptsDLL = Assembly.LoadFile(absPath);      
      //AssemblyName aname = AssemblyName.GetAssemblyName(absPath);
      //ScriptsDLL = MassiveDomain.Load(aname);

      try
      {
       // var theType = ScriptsDLL.GetType("Massive.Startup");
        //var c = Activator.CreateInstance(theType);
        //var method = theType.GetMethod("Test");
        //method.Invoke(c, new object[] { @"Hello from Massive" });

        var theType2 = ScriptsDLL.GetType("Massive.Main");
        MainActivator = Activator.CreateInstance(theType2);
       // var method2 = theType2.GetMethod("Setup");
       // method2.Invoke(MainActivator, null);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        if (ScriptError != null)
        {
          string sMessage = e.Message;
          if (e.InnerException != null)
          {
            sMessage += "\r\n" + e.InnerException.Message;
            sMessage += "\r\n" + e.InnerException.StackTrace;
          }

          ScriptError(this, new ErrorEvent(sMessage));
        }
      }
    }

    public void Init()
    {

    }

    public object CreateInstance(string sClass)
    {
      if (ScriptsDLL == null) return "";

      var theType = ScriptsDLL.GetType(sClass);
      if (theType == null)
      {
        return "Could not find " + sClass;
      }

      object c = null;
      c = Activator.CreateInstance(theType);
      return c;
    }
    //TODO : lookup DynamicMethod
    public string Invoke(object instance, string Method, object[] data )
    {
      if (ScriptsDLL == null) return "";

      MethodInfo method = null;
     // long hash = instance.GetHashCode() + Method.GetHashCode();
     // if (MethodPool.ContainsKey(hash)) method = MethodPool[hash];
     // else
      //{
        method = instance.GetType().GetMethod(Method);
       // MethodPool.Add(hash, method);
      //}
      if (method == null)
      {
        return "Could not find method " + Method;
      }
      else
      {
        try
        {
          method.Invoke(instance, data);
        }
        catch (Exception e)
        {
          return e.Message;
        }
      }

      return "";
    }

    public void Stop()
    {
     // invoked = null;
     // MainActivator = null;
    }
  }

  class Loader : MarshalByRefObject
  {
    private Assembly _assembly;

    public override object InitializeLifetimeService()
    {
      return null;
    }

    public void LoadAssembly(string path)
    {
      _assembly = Assembly.Load(AssemblyName.GetAssemblyName(path));
    }

    public object ExecuteStaticMethod(string typeName, string methodName, params object[] parameters)
    {
      Type type = _assembly.GetType(typeName);
      // TODO: this won't work if there are overloads available
      MethodInfo method = type.GetMethod(
        methodName,
        BindingFlags.Static | BindingFlags.Public);
      return method.Invoke(null, parameters);
    }
  }
}
