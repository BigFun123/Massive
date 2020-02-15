using Massive;
using Massive.Events;
using Massive.Graphics.Character;
using Massive.Network;
using Massive.Tools;
using OpenTK;
using OpenWorld.Widgets;
using OpernWorld.Widgets;
using System;
using System.Data;

namespace OpenWorld.Handlers
{
  public class MSpawnHandler
  {
    public MSpawnHandler()
    {
      Globals.Network.SpawnHandler += Network_SpawnHandler;
      MMessageBus.ChangeAvatarRequestHandler += MMessageBus_ChangeAvatarRequestHandler;
      MMessageBus.CreateObjectRequestHandler += MMessageBus_CreateObjectRequestHandler;
    }

    private void MMessageBus_CreateObjectRequestHandler(object sender, CreateObjectRequestEvent e)
    {
      //throw new NotImplementedException();
      Spawn(e.ServerObject);
    }

    private void MMessageBus_ChangeAvatarRequestHandler(object sender, ChangeAvatarEvent e)
    {
      MBuildingBlock bb = MBuildParts.GetBlock(e.TemplateID);
      if (bb == null)
      {
        Console.WriteLine("MSpawnHandler: Missing Template:" + e.TemplateID);
        return;
      }

      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.UserID);
      if (mo != null)
      {
        MMessageBus.DeleteRequest(this, mo);
      }

      MServerObject m = new MServerObject();
      m.InstanceID = e.UserID;
      m.TemplateID = e.TemplateID;
      m.OwnerID = e.UserID;
      m.TextureID = bb.MaterialID;
      m.Name = Globals.UserAccount.UserName;
      MScene.Camera.CameraOffset = MassiveTools.VectorFromArray(bb.BoneOffset);
      m.Position = Globals.UserAccount.CurrentPosition;
      m.Rotation = MassiveTools.ArrayFromQuaterniond(Globals.Avatar.GetRotation());
      Spawn(m);
    }

    private void Network_SpawnHandler(object sender, Massive.Events.ObjectSpawnedEvent e)
    {
      DataTable dt = e.SpawnedObject;

      foreach (DataRow dr in dt.Rows)
      {
        MServerObject mso = MServerObject.UnpackFromDataRow(dt.Columns, dr);
        if (mso == null) continue;
        Spawn(mso);
      }
    }

    static MPhysicsObject.EShape GetShape(string sShape)
    {
      switch (sShape)
      {
        case "Capsule": return MPhysicsObject.EShape.Capsule;
        case "Sphere": return MPhysicsObject.EShape.Sphere;
        case "Box": return MPhysicsObject.EShape.Box;
        case "ConcaveMesh": return MPhysicsObject.EShape.ConcaveMesh;
        case "ConvexHull": return MPhysicsObject.EShape.ConvexHull;
        case "HACD": return MPhysicsObject.EShape.HACD;
        default: return MPhysicsObject.EShape.NULL;
      }
    }

    public static MSceneObject LoadTemplate(string TemplateID)
    {
      MBuildingBlock bb = MBuildParts.GetBlock(TemplateID);
      if (bb == null)
      {
        Console.WriteLine("WARNING: MSpawnHandler.LoadTemplate " + TemplateID + " not found in blocks");
        return null;
      }        

      MSceneObject o = null;
      if ( bb.Type == MBuildParts.MAnimatedModel)
      {
        o = Helper.CreateAnimatedModel(MScene.TemplateRoot, TemplateID, bb.Model, Vector3d.Zero);
        MAnimatedModel man = (MAnimatedModel)o;
        man.BoneOffset = MassiveTools.VectorFromArray(bb.BoneOffset);
      }

      if (bb.Type == MBuildParts.MModel)
      {
        o = Helper.CreateModel(MScene.TemplateRoot, TemplateID, bb.Model, Vector3d.Zero);       
      }

      MMaterial mat = (MMaterial)MScene.MaterialRoot.FindModuleByName(bb.MaterialID);
      if ( mat == null)
      {
        Console.WriteLine("MSpawnHandler.LoadTemplate " + bb.MaterialID + " was null");
      }
      o.SetMaterial(mat);

      Vector3d size = MassiveTools.VectorFromArray(bb.Size);

      MPhysicsObject.EShape shape = GetShape(bb.PhysicsShape);
      if (shape != MPhysicsObject.EShape.NULL)
      {
        MPhysicsObject mpo = new MPhysicsObject(o, TemplateID + "_physics", bb.Weight, shape,
          true, size);
        mpo.SetSleep(5);
        mpo.SetFriction(0.5);
        if (shape != MPhysicsObject.EShape.Sphere)
        {
          mpo.SetAngularFactor(0.0, 0.0, 0.0);
          mpo.SetDamping(0.5, 0.5);
          mpo.SetRestitution(0.5);
        }
        else
        {
          mpo.SetDamping(0.1, 0.1);
          mpo.SetRestitution(0.8);
        }
      }

      o.TemplateID = TemplateID;
      o.InstanceID = TemplateID;
      o.IsTransparent = bb.IsTransparent;
      o.transform.RotationOffset = Quaterniond.FromEulerAngles(MassiveTools.VectorFromArray(bb.RotationOffset));
      o.Setup();

      AddSubmodules(bb, o);

      return o;
    }

    /// <summary>
    /// Submodules offer extra functionality for e.g. user interaction, linking click handlers to widgets
    /// We don't want to copy their instance, because each has unique values, e.g. door state.
    /// </summary>
    /// <param name="bb"></param>
    /// <param name="o"></param>
    static void AddSubmodules(MBuildingBlock bb, MSceneObject o)
    {

      if (bb.SubModule == "MDoor")
      {
        MDoor door = new MDoor(o);
        o.Add(door);
      }

      if (bb.SubModule == "MLinker")
      {
        MLinker link = new MLinker();
        o.Add(link);
        MClickHandler mc = new MClickHandler();
        mc.DoubleClicked = MLinkerWidget.Mc_DoubleClick;
        mc.RightClicked = MLinkerWidget.Mc_RightClick;
        o.Add(mc);
        o.Tag = "LINKER01|URL:";
      }

      if (bb.SubModule == "MTeleporter")
      {
        MClickHandler mc = new MClickHandler();
        mc.DoubleClicked = MTeleporterWidget.Mc_DoubleClick;
        mc.RightClicked = MTeleporterWidget.Mc_RightClick;
        o.Add(mc);
        o.Tag = "TELEPORTER01|XYZ:";
      }


      if (bb.SubModule == "MPicture")
      {
        MClickHandler mc = new MClickHandler();
        mc.DoubleClicked = MPictureWidget.Mc_DoubleClick;
        mc.RightClicked = MPictureWidget.Mc_RightClick;
        o.Add(mc);
        o.Tag = "PICTURE01|This Picture|Description";
      }

      if (bb.SubModule == "MStatus")
      {
        MClickHandler mc = new MClickHandler();
        mc.DoubleClicked = MStatusWidget.Mc_DoubleClick;
        mc.RightClicked = MStatusWidget.Mc_RightClick;
        o.Add(mc);
        o.Tag = "STATUS01|This Status|Description";
      }

      if ( bb.SubModule == "MNPC")
      {
        MNPC npc = new MNPC(o, "NPC");
        o.Add(npc);
      }
    }

    /// <summary>
    /// Prepares an object for inclusion in the scene graph
    /// If the template does not exist, it is created first
    /// </summary>
    /// <param name="m"></param>
    public void Spawn(MServerObject m)
    {
      MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(m.InstanceID);
      if (mo != null) return; //if the object already exists, never mind

      //check if the object template exists. All user objects must exist as a template first
      MSceneObject mt = (MSceneObject)MScene.TemplateRoot.FindModuleByName(m.TemplateID);
      if (mt == null)
      {
        LoadTemplate(m.TemplateID);
      }

      //if ((m.Name == Globals.UserAccount.UserID) && ( m.OwnerID == Globals.UserAccount.UserID)){
      mo = Helper.Spawn(m,
          MassiveTools.VectorFromArray(m.Position),
          MassiveTools.QuaternionFromArray(m.Rotation));
      if (mo == null)
      {
        Console.WriteLine("MSpawnHandler: Template not found:" + m.TemplateID);
      }
      else
      {
        mo.InstanceID = m.InstanceID;
        mo.SetRotation(MassiveTools.QuaternionFromArray(m.Rotation));

        //If the object spawning is our avatar
        if (mo.InstanceID == Globals.UserAccount.UserID)
        {
          Globals.Avatar.SetSceneObject(mo);
          MBuildingBlock bb = MBuildParts.GetBlock(mo.TemplateID);
          MScene.Camera.CameraOffset = MassiveTools.VectorFromArray(bb.BoneOffset);
          MMessageBus.AvatarChanged(this, Globals.UserAccount.UserID, mo.TemplateID);
        }     
     
        SetMaterial(mo, m.TextureID);    
        
        MMessageBus.Created(this, mo);
      }

      if (mo.OwnerID == Globals.UserAccount.UserID)
      {
        MMessageBus.Select(this, new SelectEvent(mo));
      }

    }

    void SetMaterial(MSceneObject mo, string sMaterialID)
    {
      if (sMaterialID == null)
      {
        sMaterialID = MMaterial.DEFAULT_MATERIAL;
      }
      //MSceneObject mo = (MSceneObject)MScene.ModelRoot.FindModuleByInstanceID(e.InstanceID);
      MObject o = MScene.MaterialRoot.FindModuleByName(sMaterialID);
      MMaterial mat = null;

      if (o != null && o.Type == MObject.EType.Material)
      {
        mat = (MMaterial)o;
        if (mat != null)
        {
          mo.SetMaterial(mat);
          mat.MaterialID = sMaterialID;
        }
      }

      if (MassiveTools.IsURL(sMaterialID))
      {
        mat = (MMaterial)new MMaterial("URLShader");
        MShader DefaultShader = (MShader)MScene.MaterialRoot.FindModuleByName(MShader.DEFAULT_SHADER);
        mat.AddShader(DefaultShader);
        mat.ReplaceTexture(Globals.TexturePool.GetTexture(sMaterialID));
        mat.MaterialID = sMaterialID;
        MScene.MaterialRoot.Add(mat);
        mo.SetMaterial(mat);
        mo.material.Setup();
      }
    }
  }
}
