using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MSceneObject : MObject
  {
    public MTransform transform = new MTransform();
    public Matrix4d WorldTransform = Matrix4d.Identity;
    public MBoundingBox BoundingBox;
    public double SizeHint = 0;
    public string TemplateID { get; set; } = "CUBE1";
    public bool CastsShadow = true;
    public bool IsAvatar = false;
    public bool IsTransparent = false;
    public double DistanceFromAvatar { get; set; } = 0;
    public double DistanceThreshold { get; set; } = 1000;
    public bool Culled { get; set; } =false;
    public bool Clipped { get; set; } = false;

   // public Vector3d Offset;

    // public MTransform transform { get => _transform; set => _transform = value; }
    public TexturedVertex[] Vertices = new TexturedVertex[0];
    public int VerticesLength = 0;
    public Vector3[] Normals;
    public int[] Indices = new int[0];
    public int IndicesLength = 0;
    public Vector2[] TextureCoords;

    public int VAO = 0;
    public int VBO = 0;
    public int EBO = 0;

    public Matrix4 mvp = Matrix4.Identity;
    public Matrix4 modelMatrix = Matrix4.Identity;

    public MMaterial material;
    public bool Visible = true;
    public static object Locker = new object();

    MPhysicsObject _physics;

    public MSceneObject(EType mtype, string inname) : base(mtype, inname)
    {
      Renderable = true;
      BoundingBox = new MBoundingBox();
    }

    public virtual void Bind()
    {
    }

    MPhysicsObject GetPhysics()
    {
      if (_physics== null)
      {
        _physics = (MPhysicsObject)FindModuleByType(EType.PhysicsObject);
      }
      return _physics;
    }

    public override void Setup()
    {
      IndicesLength = Indices.Length;
      VerticesLength = Vertices.Length;
      base.Setup();
    }

    /// <summary>
    /// prepare matrices for rendering
    /// TODO: Optimize calcs only when dirty
    /// </summary>
    /// <param name="viewproj"></param>
    /// <param name="parentmodel"></param>
    public void CalculateDrawMatrices(Matrix4d viewproj, Matrix4d parentmodel)
    {
      WorldTransform = transform.GetMatrix() * parentmodel;
      transform.WorldPosition = WorldTransform.ExtractTranslation();
      mvp = MTransform.GetFloatMatrix(WorldTransform * viewproj);
      modelMatrix = MTransform.GetFloatMatrix(WorldTransform);
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      //early outs
      if (Culled == true) return;
      if (Clipped == true) return;
      if (!Enabled) return;
      if (!Visible) return;
      if ((Globals.RenderPass == Globals.eRenderPass.ShadowDepth) && (CastsShadow == false)) return;
      if (DistanceFromAvatar > DistanceThreshold)
      {
        Index = -2;
        return;
      }

      Globals.DrawCalls++;
      //matrices
      CalculateDrawMatrices(viewproj, parentmodel);

      //material bindings
      if (Globals.ShaderOverride == null)
      {
        if (material != null)
        {
          material.IsUsed = true;
          material.Bind();
          if (material.shader != null)
          {
            material.shader.SetMat4("mvp", mvp);
            material.shader.SetMat4("model", modelMatrix);
            material.shader.SetBool("selected", Selected);
            material.shader.SetBool("CastsShadow", CastsShadow);
          }
        }
      }
      else
      {
        Globals.ShaderOverride.SetMat4("mvp", mvp);
        Globals.ShaderOverride.SetMat4("model", modelMatrix);
        if (Globals.RenderPass == Globals.eRenderPass.Pick)
        {
          Globals.ShaderOverride.SetVec3("object_index", GetIndexColor());
        }
        else
        {
          Globals.ShaderOverride.SetVec3("object_index", Vector3.Zero);
        }

        Globals.ShaderOverride.SetBool("CastsShadow", CastsShadow);
      }

      //mesh activation
      if (IndicesLength > 0)
      {
        if (VAO != 0)
        {
         // lock (Locker)
          {
            // draw mesh from inddices
            GL.BindVertexArray(VAO);
            GL.DrawElements(BeginMode.Triangles, IndicesLength, DrawElementsType.UnsignedInt, 0);
            
            GL.BindVertexArray(0);
          }
        }
      }
      else
      {

        if (VAO != 0)
        {
          // render mesh from triangles
         // lock (Locker)
          {
            GL.BindVertexArray(VAO);
            GL.DrawArrays(PrimitiveType.Triangles, 0, VerticesLength);
            //Helper.CheckGLError(this, "MSky 2");
            GL.BindVertexArray(0);
          }
        }
      }

      // FOR TESSELLATION glDrawArrays(GL_PATCHES,


      base.Render(viewproj, WorldTransform);

      if (Globals.ShaderOverride == null)
      {
        if (material != null)
        {
          material.UnBind();
        }
      }
    }


    public void AddMaterial(MMaterial m)
    {
      Add(m);
      material = m;
    }

    public virtual void SetMaterial(MMaterial m)
    {
      Modules.Remove(material);
      material = m;
      Add(material);
    }

    //either transform or physics
    public void SetPosition(Vector3d p)
    {
      //a phyics module may be added at any time, so we need to query for it
      MPhysicsObject po = GetPhysics();
      if (po != null)
      {
        po.SetPosition(p);
      }
      else
      {
        transform.Position = p;
      }
    }

    public void SetRotation(Quaterniond q, bool NotifyDescendents = true)
    {
      //a phyics module may be added at any time, so we need to query for it
      MPhysicsObject po = GetPhysics();
      if (po != null)
      {
        _physics.SetRotation(q);
      }
      else
      {
        transform.Rotation = q;
      }
      
      if (NotifyDescendents == true)
      {
        ParentChanged();
      }
    }

    public void LookAt(Vector3d p)
    {
      if (p.Equals(transform.Position)) p += Vector3d.UnitZ;
      MPhysicsObject po = (MPhysicsObject)FindModuleByType(EType.PhysicsObject);

      //Matrix4d mat = Matrix4d.LookAt(this.transform.Position , p , Vector3d.UnitY);
      Quaterniond q = Extensions.LookAt(this.transform.Position, p, Vector3d.UnitY);
      if (po != null)
      {
        po.SetRotation(q);
      }
      transform.Rotation = q;
    }

    public override void CopyTo(MObject m1)
    {
      base.CopyTo(m1);

      MSceneObject mo = (MSceneObject)m1;
      mo.transform.Scale = transform.Scale;
      mo.transform.RotationOffset = transform.RotationOffset;
      
      //we don't want to use the template position, since these will be set by the world map
      //mo.transform.Position = transform.Position;
      //mo.transform.Rotation = transform.Rotation;
      mo.SetMaterial(material);
      mo.TemplateID = TemplateID;
      mo.IsAvatar = IsAvatar;
      mo.CastsShadow = CastsShadow;
      mo.BoundingBox = BoundingBox;
      mo.IndicesLength = IndicesLength;
      mo.VerticesLength = VerticesLength;
      mo.IsTransparent = IsTransparent;

      foreach (MObject m in Modules)
      {

        if (m.Type == EType.PhysicsObject)
        {
          MPhysicsObject p1 = (MPhysicsObject)m;
          MPhysicsObject p2 = new MPhysicsObject(mo, mo.Name, (float)p1.Mass, p1.Shape, false, p1.CreateScale);
          p2.Name = p1.Name;
          p2.SetDamping(p1._rigidBody.LinearDamping, p1._rigidBody.AngularDamping);
          p2._rigidBody.AngularFactor = p1._rigidBody.AngularFactor;
          p2._rigidBody.LinearFactor = p1._rigidBody.LinearFactor;
          p2._rigidBody.Restitution = p1._rigidBody.Restitution;
          p2.SetPosRot(mo.transform.Position, mo.transform.Rotation);
          //p2.SetPosition(mo.transform.Position);
          //p2.SetRotation(mo.transform.Rotation);
          p2.StopAllMotion();
        }

        if (m.Type == EType.PointLight)
        {
          MPointLight p = new MPointLight(m.Name);
          MScene.LightRoot.Add(p);
          m.CopyTo(p);
          mo.Add(p);
        }

        if (m.Type == EType.Sound)
        {
          MSound os = (MSound)m;
          MSound s = new MSound();
          os.CopyTo(s);
          s.Error = os.Error;
          s.Load(os.Filename, mo);
          mo.Add(s);

        }

        //network objects can only be owned by the user. When the user is offline the network object is dormant
        //if (mo.OwnerID.Equals(Globals.UserAccount.UserID))
        if (m.Type == EType.NetworkObject)
        {
          MNetworkObject mn = (MNetworkObject)m.FindModuleByType(MObject.EType.NetworkObject);
          if (mn != null)
          {
            mo.Add(mn);
          }
        }

        if (m.Type == EType.NPCPlayer)
        {
          MNPC p = new MNPC(mo, m.Name);
          m.CopyTo(p);
          mo.Add(p);
        }

        if (m.Type == EType.Door)
        {
          MDoor d2 = new MDoor(mo);
          m.CopyTo(d2);
          mo.Add(d2);
        }

      }

    }

    public override void Debug()
    {
      string sout = "";
      for (int i = 0; i < Indent; i++)
      {
        sout += "-";
      }
      Console.WriteLine(sout + this.transform.Position);
      base.Debug();
    }
  }
}
