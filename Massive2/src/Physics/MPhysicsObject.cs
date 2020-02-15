using OpenTK;
using BulletSharp;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Massive.Tools;
using System.IO;

namespace Massive
{
  public class MPhysicsObject : MObject
  {
    private double mass = 0;
    public double Mass { get => mass; set => mass = value; }

    public Vector3d VelocityLimit = new Vector3d(3, 3, 3);
    public Vector3d CreateScale = Vector3d.One;
    public enum EShape { NULL, Box, Sphere, Capsule, ConvexHull, ConcaveMesh, ConcaveProxy, HACD };
    public EShape Shape = EShape.Box;

    public bool IsProxy = false;

    // public Vector3d Thrust = new Vector3d(0, 0, 0);
    //private Vector3 cThrust = new Vector3(0, 0, 0);

    private Vector3d dir = new Vector3d(0, 0, 0);
    public Vector3d Dir { get => dir; set => dir = value; }


    CollisionShape collisionShape;
    public RigidBody _rigidBody;
    public MSceneObject Target;

    bool Disposed = false;
    //private bool active = true;
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //privat ebool Active { get => _rigidBody.IsActive;        
    //}
    //}

    public override void Dispose()
    {
      Disposed = true;
      if (MPhysics.Instance != null)
      {
        MPhysics.Instance.Remove(this);
      }
    }   

    public MPhysicsObject(MSceneObject inTarget, string name, double inmass, EShape inshape, bool IsTemplate, Vector3d Scale)
      : base(EType.PhysicsObject, name)
    {
      Shape = inshape;
      Target = inTarget;
      CreateScale = Scale;

      mass = inmass;
      if (inTarget != null)
      {
        inTarget.Add(this);
      }

      if (IsTemplate == true)
      {
        inshape = EShape.NULL;
        collisionShape = new Box2DShape(0.1);
      }

      if (inshape == EShape.Box)
      {
        collisionShape = new BoxShape(new Vector3d(Scale.X, Scale.Y, Scale.Z));
      }
      if (inshape == EShape.Sphere)
      {
        collisionShape = new SphereShape(Scale.X);
        collisionShape.Margin = 0;
        //collisionShape = new MultiSphereShape(new Vector3d[] { Vector3d.Zero }, new double[] { Scale.X });
      }
      if (inshape == EShape.Capsule)
      {
        collisionShape = new CapsuleShape(Scale.X, Scale.Y);
        //collisionShape = new MultiSphereShape(new Vector3d[] { Vector3d.Zero }, new double[] { Scale.X });
      }
      if (inshape == EShape.ConvexHull)
      {       
        collisionShape = CreateConvexHullMesh(inTarget, mass, Scale);
      }

      if (inshape == EShape.ConcaveMesh)
      {        
        collisionShape = CreateConcaveMesh(inTarget, mass, Scale);
      }

      if (inshape == EShape.ConcaveProxy)
      {
        IsProxy = true;
        collisionShape = CreateConcaveMeshProxy(inTarget, mass, Scale);
      }

      if (inshape == EShape.HACD)
      {
        //mass = 0;
        collisionShape = CreateHACDMesh(inTarget, mass, Scale);
      }

      

      Vector3d localInertia = Vector3d.Zero;

      if ((mass > 0) && (collisionShape != null))
      {
        collisionShape.CalculateLocalInertia(mass, out localInertia);
      }

      DefaultMotionState myMotionState;
      if (inTarget != null)
      {
        myMotionState = new DefaultMotionState
        (
        // Matrix4.CreateScale(new Vector3((float)Size.X, (float)Size.Y, (float)Size.Z))
        Matrix4d.CreateFromQuaternion(inTarget.transform.Rotation)
        * Matrix4d.CreateTranslation(inTarget.transform.Position)
        );
      }
      else
      {
        myMotionState = new DefaultMotionState
        (
        // Matrix4.CreateScale(new Vector3((float)Size.X, (float)Size.Y, (float)Size.Z))
        Matrix4d.Identity
        );
      }

      collisionShape.UserObject = inTarget;      

      RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, collisionShape, localInertia);
      _rigidBody = new RigidBody(rbInfo);
      _rigidBody.SetDamping(0.3, 0.1);
      _rigidBody.UserObject = inTarget;
      //_rigidBody.UserObject = Name;
      _rigidBody.Restitution = 0.5;
      _rigidBody.CcdMotionThreshold = 1e-7;
      _rigidBody.CcdSweptSphereRadius = 0.5;      

      _rigidBody.ContactProcessingThreshold = 1e30f;
      _rigidBody.SetSleepingThresholds(0.20, 0.20);

      // when using ccdMode, disable regular CCD
      //if (ccdMode)
      //{
      _rigidBody.CcdMotionThreshold = 0.0001f;
      _rigidBody.CcdSweptSphereRadius = 0.4f;
      //}

      if (IsTemplate == false)
      {
        MPhysics.Add(this);
      }
    }

    public void SetActive(bool b)
    {
      if (b == true)
      {
        // _rigidBody.ActivationState = ActivationState.ActiveTag;

        if (!_rigidBody.IsInWorld)
        {
          MPhysics.Add(this);
        }
        if (!_rigidBody.IsActive) { 
        _rigidBody.Activate();
        }
      }
      else
      {
        // _rigidBody.ActivationState = ActivationState.DisableSimulation;        

        MPhysics.Instance.Remove(this);
        //_rigidBody.LinearFactor = new Vector3d(0, 0, 0);
        //_rigidBody.CollisionFlags = CollisionFlags.DisableSpuCollisionProcessing;
      }
    }

    public void AddHinge(MPhysicsObject dpo)
    {
      Generic6DofConstraint hc = new Generic6DofConstraint(_rigidBody,
        dpo._rigidBody,
        Matrix4d.CreateTranslation(0, 0, 0),
        Matrix4d.CreateTranslation(0.80, 0.21, 0),
        true);

      hc.LinearLowerLimit = new Vector3d(0);
      hc.LinearUpperLimit = new Vector3d(0);
      hc.AngularLowerLimit = new Vector3d(0, MathHelper.DegreesToRadians(-85), 0);
      hc.AngularUpperLimit = new Vector3d(0, MathHelper.DegreesToRadians(85), 0);
      //hc.AngularLowerLimit = new Vector3d(-(float)Math.PI / 4, -0.75f, -(float)Math.PI * 0.4f);
      //hc.AngularUpperLimit = new Vector3d((float)Math.PI / 4, 0.75f, (float)Math.PI * 0.4f);
      //hc.SetLimit(0, -(float)Math.PI * 0.25f, (float)Math.PI * 0.25f);
      MPhysics.Instance.World.AddConstraint(hc);
      _rigidBody.AddConstraintRef(hc);
    }

    public double GetSleep()
    {
      return _rigidBody.LinearSleepingThreshold;
    }

    public void SetSleep(double s)
    {
      _rigidBody.SetSleepingThresholds(s, s);
    }

    public override void Update()
    {
      if (_rigidBody == null) return;

      if ( _rigidBody.IsStaticObject== false)
      {
       // _rigidBody.AngularVelocity = Vector3d.Clamp(_rigidBody.AngularVelocity, -VelocityLimit, VelocityLimit);        
      }

      Matrix4d m = _rigidBody.InterpolationWorldTransform;
      Target.transform.Position = m.ExtractTranslation(); 
      //Target.transform.Position = _rigidBody.CenterOfMassPosition;
      Target.transform.Rotation = m.ExtractRotation();


      base.Update();
    }

    void CreateTerrain()
    {
      //HeightfieldTerrainShape
    }

    CollisionShape CreateConvexHullMesh(MSceneObject mo, double inmass, Vector3d Scale)
    {
      MMesh mesh = (MMesh)mo.FindModuleByType(EType.Mesh);

      mass = inmass;
      //TriangleIndexVertexArray tm = new TriangleIndexVertexArray();

      TriangleMesh trimesh = new TriangleMesh();

      for (int i = 0; i < mesh.Indices.Count(); i += 3)
      {
        TexturedVertex v1 = mesh.Vertices[mesh.Indices[i]];
        TexturedVertex v2 = mesh.Vertices[mesh.Indices[i + 1]];
        TexturedVertex v3 = mesh.Vertices[mesh.Indices[i + 2]];

        trimesh.AddTriangle(MassiveTools.Vector3dFromVector3(v1._position),
          MassiveTools.Vector3dFromVector3(v2._position),
          MassiveTools.Vector3dFromVector3(v3._position));
      }

      // Create a hull approximation

      ConvexHullShape convexShape;
      using (var tmpConvexShape = new ConvexTriangleMeshShape(trimesh))
      {
        tmpConvexShape.Margin = 0.01;

        using (var hull = new ShapeHull(tmpConvexShape))
        {
          hull.BuildHull(tmpConvexShape.Margin);
          convexShape = new ConvexHullShape(hull.Vertices);
        }
      }
      convexShape.Margin = 0.1;
      convexShape.LocalScaling = Scale;
      return convexShape;

    }

    CollisionShape CreateConcaveMesh(MSceneObject mo, double inmass, Vector3d Scale)
    {
      MMesh mesh = null;
      if ((mo.Type == EType.Mesh) || (mo.Type == EType.Terrain))
        mesh = (MMesh)mo;
      else
      {
        mesh = (MMesh)mo.FindModuleByType(EType.Mesh);
      }

      if (mesh == null) return null;

      //  Matrix4d trans = Matrix4d.CreateTranslation(mo.transform.Position);

      Matrix4d trans = Matrix4d.CreateFromQuaternion(mo.transform.Rotation)
      * Matrix4d.CreateTranslation(mo.transform.Position);

      //      CompoundCollisionAlgorithm.CompoundChildShapePairCallback = MyCompoundChildShapeCallback;
      //    convexDecompositionObjectOffset = new Vector3(10, 0, 0);

      mass = inmass;
      //TriangleIndexVertexArray tm = new TriangleIndexVertexArray();
      Scale = new Vector3d(1, 1, 1);
      TriangleMesh trimesh = new TriangleMesh();      

      int tcount = mesh.Indices.Count() / 3;
      for (int i = 0; i < tcount; i++)
      {
        int index0 = mesh.Indices[i * 3];
        int index1 = mesh.Indices[i * 3 + 1];
        int index2 = mesh.Indices[i * 3 + 2];

        TexturedVertex v1 = mesh.Vertices[index0];
        TexturedVertex v2 = mesh.Vertices[index1];
        TexturedVertex v3 = mesh.Vertices[index2];        
        trimesh.AddTriangle(MassiveTools.Vector3dFromVector3(v1._position) * Scale,
          MassiveTools.Vector3dFromVector3(v2._position) * Scale,
          MassiveTools.Vector3dFromVector3(v3._position) * Scale);
      }

      BvhTriangleMeshShape gmp = new BvhTriangleMeshShape(trimesh, true);
      gmp.Margin = 0.1;
      return gmp;
    }

    /// <summary>
    /// creates a temporary placeholder collision mesh, replaced within the physics thread
    /// </summary>
    /// <param name="mo"></param>
    /// <param name="inmass"></param>
    /// <param name="Scale"></param>
    /// <returns></returns>
    CollisionShape CreateConcaveMeshProxy(MSceneObject mo, double inmass, Vector3d Scale)
    {
      MMesh mesh = null;
      if ((mo.Type == EType.Mesh) || (mo.Type == EType.Terrain))
        mesh = (MMesh)mo;
      else
      {
        mesh = (MMesh)mo.FindModuleByType(EType.Mesh);
      }

      if (mesh == null) return null;

      //  Matrix4d trans = Matrix4d.CreateTranslation(mo.transform.Position);

      Matrix4d trans = Matrix4d.CreateFromQuaternion(mo.transform.Rotation)
      * Matrix4d.CreateTranslation(mo.transform.Position);

      //      CompoundCollisionAlgorithm.CompoundChildShapePairCallback = MyCompoundChildShapeCallback;
      //    convexDecompositionObjectOffset = new Vector3(10, 0, 0);

      mass = inmass;
      //TriangleIndexVertexArray tm = new TriangleIndexVertexArray();
      Scale = new Vector3d(1, 1, 1);
      TriangleMesh trimesh = new TriangleMesh();

      int tcount = mesh.Indices.Count() / 3;
      for (int i = 0; i < 2; i++)
      {
        int index0 = mesh.Indices[i * 3];
        int index1 = mesh.Indices[i * 3 + 1];
        int index2 = mesh.Indices[i * 3 + 2];

        TexturedVertex v1 = mesh.Vertices[index0];
        TexturedVertex v2 = mesh.Vertices[index1];
        TexturedVertex v3 = mesh.Vertices[index2];
        trimesh.AddTriangle(MassiveTools.Vector3dFromVector3(v1._position) * Scale,
          MassiveTools.Vector3dFromVector3(v2._position) * Scale,
          MassiveTools.Vector3dFromVector3(v3._position) * Scale);
      }

      BvhTriangleMeshShape gmp = new BvhTriangleMeshShape(trimesh, true);
      gmp.Margin = 0.1;
      return gmp;
    }

    //VERY SLOW! AVOID
    CollisionShape CreateConcaveMesh2(MSceneObject mo, double inmass, Vector3d Scale)
    {

      var compound = new CompoundShape();

      for (int mi = 0; mi < mo.Modules.Count; mi++)
      {
        MObject testmo = mo.Modules[mi];
        if (testmo.Type != EType.Mesh) continue;

        MMesh mesh = (MMesh)testmo;

        //  Matrix4d trans = Matrix4d.CreateTranslation(mo.transform.Position);

        Matrix4d trans = Matrix4d.CreateFromQuaternion(mo.transform.Rotation)
        * Matrix4d.CreateTranslation(mo.transform.Position);

        //      CompoundCollisionAlgorithm.CompoundChildShapePairCallback = MyCompoundChildShapeCallback;
        //    convexDecompositionObjectOffset = new Vector3(10, 0, 0);

        mass = inmass;
        //TriangleIndexVertexArray tm = new TriangleIndexVertexArray();

        TriangleMesh trimesh = new TriangleMesh();

        for (int i = 0; i < mesh.Indices.Count(); i += 3)
        {
          TexturedVertex v1 = mesh.Vertices[mesh.Indices[i]];
          TexturedVertex v2 = mesh.Vertices[mesh.Indices[i + 1]];
          TexturedVertex v3 = mesh.Vertices[mesh.Indices[i + 2]];

          trimesh.AddTriangle(MassiveTools.Vector3dFromVector3(v1._position),
            MassiveTools.Vector3dFromVector3(v2._position),
            MassiveTools.Vector3dFromVector3(v3._position));
        }

        GImpactMeshShape gmp = new GImpactMeshShape(trimesh);
        gmp.LocalScaling = Scale;
        compound.AddChildShape(trans, gmp);
        /*
        //IndexedMesh imesh = new IndexedMesh();
        BvhTriangleMeshShape conc = new BvhTriangleMeshShape(trimesh, true);
        //ConvexTriangleMeshShape conc = new ConvexTriangleMeshShape(trimesh, true);      
        conc.LocalScaling = Scale;
        conc.Margin = 0;
        */

        //compound.AddChildShape(trans, conc);

        //LocalCreateRigidBody(0, trans, conc);
      }
      return compound;
    }


    //VERY SLOW! AVOID
    CollisionShape CreateHACDMesh(MSceneObject mo, double inmass, Vector3d Scale)
    {
      MMesh mesh = (MMesh)mo.FindModuleByType(EType.Mesh);

      //      CompoundCollisionAlgorithm.CompoundChildShapePairCallback = MyCompoundChildShapeCallback;
      //    convexDecompositionObjectOffset = new Vector3(10, 0, 0);

      mass = inmass;
      //TriangleIndexVertexArray tm = new TriangleIndexVertexArray();

      TriangleMesh trimesh = new TriangleMesh();

      for (int i = 0; i < mesh.Indices.Count(); i += 3)
      {
        TexturedVertex v1 = mesh.Vertices[mesh.Indices[i]];
        TexturedVertex v2 = mesh.Vertices[mesh.Indices[i + 1]];
        TexturedVertex v3 = mesh.Vertices[mesh.Indices[i + 2]];

        trimesh.AddTriangle(MassiveTools.Vector3dFromVector3(v1._position),
          MassiveTools.Vector3dFromVector3(v2._position),
          MassiveTools.Vector3dFromVector3(v3._position));
      }


      //var conv = new ConvexTriangleMeshShape(trimesh, true);      
      //conv.LocalScaling = Scale;
      //conv.Margin = 0;
      // return conv;

      List<Vector3d> verts = new List<Vector3d>();
      foreach (TexturedVertex v in mesh.Vertices)
      {
        verts.Add(MassiveTools.Vector3dFromVector3(v._position));
      }
      // HACD
      var hacd = new Hacd();
      hacd.SetPoints(verts);
      hacd.SetTriangles(mesh.Indices);
      hacd.CompacityWeight = 0.01;
      //hacd.ConnectDist = 0.5;
      hacd.VolumeWeight = 0.0;

      // Recommended HACD parameters: 2 100 false false false
      hacd.NClusters = 3;                      // minimum number of clusters
      hacd.Concavity = 100;                    // maximum concavity
      hacd.AddExtraDistPoints = false;
      hacd.AddNeighboursDistPoints = false;
      hacd.AddFacesPoints = false;
      hacd.NumVerticesPerConvexHull = 100;     // max of 100 vertices per convex-hull

      hacd.Compute(true);
      // hacd.Save("output.wrl", false);

      // Generate convex result
      // var outputFile = new FileStream("file_convex.obj", FileMode.Create, FileAccess.Write);
      //var writer = new StreamWriter(outputFile);

      var convexDecomposition = new ConvexDecomposition();
      convexDecomposition.LocalScaling = Scale;

      for (int c = 0; c < hacd.NClusters; c++)
      {
        Vector3d[] points;
        int[] triangles;
        hacd.GetCH(c, out points, out triangles);

        convexDecomposition.ConvexDecompResult(points, triangles);
      }

      // Combine convex shapes into a compound shape
      var compound = new CompoundShape();
      for (int i = 0; i < convexDecomposition.convexShapes.Count; i++)
      {
        Vector3d centroid = convexDecomposition.convexCentroids[i];
        var convexShape2 = convexDecomposition.convexShapes[i];
        Matrix4d trans = Matrix4d.CreateTranslation(centroid);
        /*if (false)
        {
          convexShape2.InitializePolyhedralFeatures();
        }*/
        //CollisionShapes.Add(convexShape2);
        compound.AddChildShape(trans, convexShape2);

        //LocalCreateRigidBody(1.0f, trans, convexShape2);
      }
      //CollisionShapes.Add(compound);


      // writer.Dispose();
      // outputFile.Dispose();

      return compound;

      // Create a hull approximation
      /*
      ConvexHullShape convexShape;
      using (var tmpConvexShape = new ConvexTriangleMeshShape(trimesh))
      {
        using (var hull = new ShapeHull(tmpConvexShape))
        {
          hull.BuildHull(tmpConvexShape.Margin);
          convexShape = new ConvexHullShape(hull.Vertices);
        }
      }
      convexShape.Margin = 0;
      convexShape.LocalScaling = Scale;
      return convexShape;
      */
      //var concaveShape = new BvhTriangleMeshShape(trimesh, true);      


    }

    /*
    public virtual RigidBody LocalCreateRigidBody(float mass, Matrix4d startTransform, CollisionShape shape)
    {
      //rigidbody is dynamic if and only if mass is non zero, otherwise static
      bool isDynamic = (mass != 0.0f);
      
      Vector3d localInertia = Vector3d.Zero;
      if (isDynamic)
        shape.CalculateLocalInertia(mass, out localInertia);

      //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
      DefaultMotionState myMotionState = new DefaultMotionState(startTransform);

      RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
      RigidBody body = new RigidBody(rbInfo);
      rbInfo.Dispose();
      MPhysics.Add(this);
      return body;
    }
    */

    public void StopAllMotion()
    {
      _rigidBody.LinearVelocity = Vector3d.Zero;
      _rigidBody.AngularVelocity = Vector3d.Zero;
      _rigidBody.UpdateInertiaTensor();
      _rigidBody.Activate();
    }

    public void SetLinearFactor(double x, double y, double z)
    {
      _rigidBody.LinearFactor = new Vector3d(x, y, z);
    }

    public void SetAngularFactor(double x, double y, double z)
    {
      _rigidBody.AngularFactor = new Vector3d(x, y, z);
    }

    public void SetDamping(double linear, double angular)
    {
      _rigidBody.SetDamping(linear, angular);
    }

    public void SetRestitution(double d)
    {
      if (_rigidBody != null)
      {
        _rigidBody.Restitution = d;
      }
    }

    public void SetFriction(double d)
    {
      if (_rigidBody != null)
      {
        _rigidBody.Friction = d;
      }
    }

    public void ApplyTorque(Vector3d v)
    {
      _rigidBody.ApplyTorque(v);
      _rigidBody.Activate();
    }

    public Vector3d GetPosition()
    {
      //return _rigidBody.InterpolationWorldTransform.ExtractTranslation();
      //return _rigidBody.MotionState.WorldTransform.ExtractTranslation();
      if ( double.IsNaN(_rigidBody.CenterOfMassPosition.X) )
      {
        return new Vector3d(0, 0, 0);
      }
      return _rigidBody.WorldTransform.ExtractTranslation();
    }

    public Quaterniond GetRotation()
    {
      return _rigidBody.WorldTransform.ExtractRotation();
    }

    public void SetPosRot(Vector3d pos, Quaterniond rot)
    {
      if (Disposed) return;
      


      Matrix4d rm = Matrix4d.CreateFromQuaternion(rot)
        * Matrix4d.CreateTranslation(pos);
      //_rigidBody.MotionState.WorldTransform = rm;            
      _rigidBody.LinearVelocity = new Vector3d(0, 0, 0);
      _rigidBody.AngularVelocity = new Vector3d(0, 0, 0);
      _rigidBody.WorldTransform = rm;
      _rigidBody.CenterOfMassTransform = rm;
    }

    public void SetRotation(Quaterniond rot)
    {
      if (Disposed) return;
      Matrix4d rm = Matrix4d.CreateFromQuaternion(rot);
      Matrix4d pm = rm * Matrix4d.CreateTranslation(_rigidBody.CenterOfMassPosition);
      //_rigidBody.MotionState.WorldTransform = pm* rm;
      //_rigidBody.WorldTransform = pm;
      _rigidBody.CenterOfMassTransform = pm;      
      _rigidBody.AngularVelocity = new Vector3d(0, 0, 0);
//       MScene.Physics.World.UpdateAabbs();     
    }

    public void SetPosition(Vector3d pos)
    {
      if (Disposed) return;

      Matrix4d OldPos = _rigidBody.WorldTransform;

      Quaterniond rot = _rigidBody.WorldTransform.ExtractRotation();
      Matrix4d rm = Matrix4d.CreateFromQuaternion(rot)
        * Matrix4d.CreateTranslation(pos);
      //_rigidBody.MotionState.WorldTransform = rm;            
      _rigidBody.LinearVelocity = new Vector3d(0, 0, 0);
      _rigidBody.AngularVelocity = new Vector3d(0, 0, 0);
      //_rigidBody.WorldTransform = rm;
      _rigidBody.CenterOfMassTransform = rm;

      if (Tag != null)
      {
        MPhysicsObject po = (MPhysicsObject)Tag;
        Vector3d delta = OldPos.ExtractTranslation() - po.GetPosition();
        po.SetPosition(po.GetPosition() + delta);
      }

      Target.transform.Position = GetPosition();

      //MScene.Physics.World.UpdateSingleAabb(_rigidBody);

    }
  }
}
