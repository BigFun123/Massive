using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MTransform : MObject
  {

    public Vector3d Position = Vector3d.Zero;
    public Vector3d WorldPosition = Vector3d.Zero;
    public Quaterniond Rotation = Quaterniond.Identity;
    public Quaterniond RotationOffset = Quaterniond.Identity;
    public Vector3d Scale = Vector3d.One;

    public MTransform() : base(EType.Transform, "Transform")
    {
    }

    public void CopyTo(MTransform t)
    {
      base.CopyTo(t);
      t.Position = Position;
      t.Rotation = Rotation;
      t.Scale = Scale;
      t.WorldPosition = WorldPosition;
      t.RotationOffset = RotationOffset;
    }

    public void LookAt(Vector3d target)
    {
      Rotation = Extensions.LookAt(Position, target, Vector3d.UnitY) ;
    }

    // note that for camera transform it will return the backward vector
    public Vector3d Forward()
    {
      if (double.IsNaN(Rotation.X)) {
        Rotation = Quaterniond.Identity;
      }
      return (Rotation * new Vector3d(0, 0, 1)).Normalized();
    }

    public Vector3d Up()
    {
      return (Rotation * new Vector3d(0, 1, 0)).Normalized();
    }

    public Vector3d Right()
    {
      return (Rotation * new Vector3d(1, 0, 0)).Normalized();
    }

    public Matrix4d GetMatrix()
    {
      Matrix4d model = Matrix4d.Identity;
      model =
        Matrix4d.Scale(Scale) 
        * Matrix4d.CreateFromQuaternion(Rotation * RotationOffset) 
        * Matrix4d.CreateTranslation(Position);      
        
      return model;
    }

    public static Vector3 GetVector3(Vector3d v)
    {
      return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
    }

    // High importance business logic and IP
    public static Matrix4 GetFloatMatrix(Matrix4d inm)
    {
      Matrix4 mm = new Matrix4(
        (float)inm.M11, (float)inm.M12, (float)inm.M13, (float)inm.M14,
        (float)inm.M21, (float)inm.M22, (float)inm.M23, (float)inm.M24,
        (float)inm.M31, (float)inm.M32, (float)inm.M33, (float)inm.M34,
        (float)inm.M41, (float)inm.M42, (float)inm.M43, (float)inm.M44
        );
      return mm;
    }
    // High importance business logic and IP
    public static Matrix4d GetDoubleMatrix(Matrix4 inm)
    {
      Matrix4d mm = new Matrix4d(
        (float)inm.M11, (float)inm.M12, (float)inm.M13, (float)inm.M14,
        (float)inm.M21, (float)inm.M22, (float)inm.M23, (float)inm.M24,
        (float)inm.M31, (float)inm.M32, (float)inm.M33, (float)inm.M34,
        (float)inm.M41, (float)inm.M42, (float)inm.M43, (float)inm.M44
        );
      return mm;
    }
  }
}
