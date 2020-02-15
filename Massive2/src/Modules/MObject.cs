using Massive.Events;
using OpenTK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  /// <summary>
  /// Every object in the scenegraph inherits from MObject
  /// Functions for update, add, remove, etc
  /// FindModuleBy...
  /// </summary>
  public class MObject : IDisposable
  {
    public static int Indent = 0; //fore debugging
    public bool Deletable = true;
    public string Name { get; set; }
    public string OwnerID { get; set; } = OWNER_NONE;
    public const string OWNER_NONE = "NONE";
    public const string OWNER_SYSTEM = "SYSTEM";
    public const string OWNER_ADMIN = "ADMIN";
    public const string OWNER_MAYOR = "MAYOR";
    public const string OWNER_ASTRONOMER = "ASTRONOMER";
    public event EventHandler GraphChanged;    
    public string Error;
    public bool IsCopy = false; //when true, don't dispose items

    public enum EType
    {
      Null, Debug, SceneObject, Transform, Other,
      SystemScript, Script, ScriptHost,
      Player, MultiPlayer, NPCPlayer, NetworkObject, NetworkHelper,
      Primitive, PrimitiveCube, PrimitiveSphere, PrimitiveTriangle, Model, ModelURL, Mesh, BoneMesh, Terrain,
      AstroBody, InstanceMesh,
      Grass, Water, Sand, Rock,
      AnimatedModel, Animation, Animator, Teleport, MoveSync,
      Shader, Material, Texture,
      Camera, DirectionalLight, PointLight,
      Text2D, Text3D, GUI,
      RenderSettings,
      Sky, FrustrumCuller, DistanceClipper,
      Sound, Music, AudioListener,
      PhysicsObject, PhysicsEngine, PhysicsCharacter,
      Fog,
      Flare, Particle,
      ClickHandler, Door, Linker
    };

    public EType Type { get; set; }
    public virtual bool Enabled { get; set; } = true;
    public bool Renderable { get; set; }
    private bool selected = false;
    public bool Selected { get => selected; set => selected = value; }


    public MObject Parent;
    public static readonly object ModuleLock = new object();
    public List<MObject> Modules = new List<MObject>(20);
    public object Tag;

    public int Index = 0;


    public Vector3 IndexColor = Vector3.Zero;

    //Universal ID
    private string instanceID = "";
    public string InstanceID { get => instanceID; set => instanceID = value; }

    public MObject(EType intype, string inname)
    {
      Type = intype;
      Name = inname;
      InstanceID = Helper.GUID();
      //Globals.NotifyChange(this, this, GraphChangedEvent.ChangeType.Created);
    }

    //called when user moves his object to new location
    public virtual void ParentChanged()
    {
      for (int i = 0; i < Modules.Count; i++)
      {
        Modules[i].ParentChanged();
      }
    }

    public void Add(MObject mo)
    {
      if (mo == null) return;
      // Globals.NotifyChange(this, mo, GraphChangedEvent.ChangeType.Added);
      mo.Parent = this;
      //lock (ModuleLock)
      {
        Modules.Add(mo);
      }
    }

    public void Remove(MObject mo)
    {
      if (mo.Deletable == true)
      {
        //Globals.NotifyChange(this, mo, GraphChangedEvent.ChangeType.Removed);
        //lock (ModuleLock)
        {
          MObject[] objects = Modules.ToArray();
          foreach (MObject m in objects)
          {
            if (m == mo)
            {
              m.Parent = null;
              Modules.Remove(m);
            }
          }
        }
      }
    }

    //Do not use in a loop as it is quite slow
    public MObject FindModuleByName(string sName)
    {
      if (Name == sName) return this;

      for (int i = 0; i < Modules.Count(); i++)
      {
        MObject mo = Modules[i];
        if (mo != null)
        {
          MObject f = mo.FindModuleByName(sName);
          if (f != null) return f;
        }
      }
      return null;
    }

    /**
     * Returns the first object of type EType
     * */
    public MObject FindModuleByType(EType inType)
    {
      if (inType == Type) return this;

      for (int i = 0; i < Modules.Count(); i++)
      {
        MObject mo = Modules[i];
        if (mo != null)
        {
          MObject f = mo.FindModuleByType(inType);
          if (f != null) return f;
        }
      }
      return null;
    }

    /**
     * Returns the first object of type EType
     * */
    public MObject FindModuleByInstanceID(string uid)
    {
      if (uid == InstanceID) return this;

      for (int i = 0; i < Modules.Count(); i++)
      {
        MObject mo = Modules[i];
        if (mo != null)
        {
          MObject f = mo.FindModuleByInstanceID(uid);
          if (f != null) return f;
        }
      }
      return null;
    }

    /**
     * Returns the first object of type EType
     * */
    public MObject FindModuleByIndex(int inIndex, MObject imo)
    {
      //if (Enabled == false) return null;
      if (Index == inIndex)
      {
        return this;
      }

      for (int i = 0; i < Modules.Count(); i++)
      {
        MObject mo = Modules[i];
        mo = mo.FindModuleByIndex(inIndex, mo);
        if (mo != null) return mo;
      }
      return null;
    }

    public virtual void Setup()
    {
      foreach (MObject mo in Modules.ToArray())
      {
        mo.Setup();
      }
    }

    public virtual void Notify(string sMessage)
    {
      
    }

    public virtual void OnPlay()
    {
      foreach (MObject mo in Modules)
      {
        mo.OnPlay();
      }
    }

    public virtual void OnStop()
    {
      foreach (MObject mo in Modules)
      {
        mo.OnStop();
      }
    }

    public virtual void Update()
    {
      if (!Enabled) return;
      //lock (ModuleLock)
      {
        int counter = 0;
        while (counter < Modules.Count)
        {
          MObject mo = Modules[counter];
          if (mo != null)
          {
            mo.Update();
          }
          counter++;
        }
      }
    }

    public virtual void OnKey(MKey key)
    {
      if (!Enabled) return;
      foreach (MObject mo in Modules)
      {
        mo.OnKey(key);
      }
    }

    public virtual void OnSelected(MSceneObject mso)
    {
      if (!Enabled) return;
      foreach (MObject mo in Modules.ToArray())
      {
        mo.OnSelected(mso);
      }
    }

    public virtual void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (!Enabled) return;     
      // lock (ModuleLock)
      {
        int counter = 0;
        while (counter < Modules.Count)
        {
          MObject mo = Modules[counter];
          if (mo != null)
          {
            mo.Render(viewproj, parentmodel);
          }
          counter++;
        }
      }
    }

    public Vector3 GetIndexColor()
    {
      Index = ++Globals.Index;
      IndexColor = IntToRgb(Index);
      return IndexColor;
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

    public virtual void RenderSelected(Matrix4d viewproj, Matrix4d parentModel)
    {
      foreach (MObject mo in Modules)
      {
        mo.RenderSelected(viewproj, parentModel);
      }
    }

    public virtual void Deselect()
    {
      Selected = false;
      foreach (MObject m in Modules)
      {
        m.Deselect();
      }
    }

    public virtual void Dispose()
    {
      //lock (ModuleLock)
      {
        //Globals.NotifyChange(this, this, GraphChangedEvent.ChangeType.Disposed);
        List<MObject> Disposables = Modules.ToList();
        Disposables.Reverse(); //reverse it so that utility nodes (created first) are destroyed last
        foreach (MObject m in Disposables)
        {
          Modules.Remove(m);
          try
          {
            m.Dispose();
          }
          catch (Exception e)
          {
            Console.WriteLine("MOBJECT:" + Name + " EXCEPTION " + e.Message);
          }
        }
      }
      //Console.WriteLine(Name + " DISPOSED");
    }

    /**
     * Disposes everything except materials
     * */
    public virtual void SafeDispose()
    {
      lock (ModuleLock)
      {
        //Globals.NotifyChange(this, this, GraphChangedEvent.ChangeType.Disposed);
        foreach (MObject m in Modules.ToList())
        {
          if (m.Type != EType.Material)
          {
            Modules.Remove(m);
            m.Dispose();
          }
        }
      }
    }

    public virtual void OnClick(bool DoubleClick = false, bool RightClick = false)
    {
      foreach (MObject m in Modules.ToArray())
      {
        m.OnClick(DoubleClick, RightClick);
      }
    }

    public virtual void Debug()
    {
      string sout = "";
      Indent++;
      for (int i = 0; i < Indent; i++)
      {
        sout += "-";
      }
      Console.WriteLine(sout + this.Name + " : " + this.Type.ToString());
      for (int i = 0; i < Modules.Count; i++)
      {
        MObject mo = Modules[i];
        mo.Debug();
      }
      Indent--;
    }

    public virtual void CopyTo(MObject mo)
    {
      mo.Deletable = Deletable;
      mo.Error = Error;
    }

    public override string ToString()
    {
      return Name + "<-" + Parent;
    }
  }
}
