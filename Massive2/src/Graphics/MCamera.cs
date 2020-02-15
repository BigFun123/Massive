using Massive.Tools;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * NOTE: OPENGL Camera forward vector is "-Z"
 * 
 * */

namespace Massive
{
  public class MCamera : MSceneObject
  {
    public MSceneObject Focus = new MSceneObject(EType.Null, "CameraFocus");
    //offset relative to the destination position
    public Vector3d CameraOffset = Vector3d.Zero;
    public Vector3d TargetOffset = Vector3d.Zero;
    public Vector3d UpVector;

    double fov = 50;
    public double FOV { get => fov; set => fov = value; }

    private double nearPlane = 0.1;
    public double NearPlane { get => nearPlane; set => nearPlane = value > 0 ? value : 0.01; }

    private double midPlane = 1000;
    public double MidPlane { get => midPlane; set => midPlane = value; }

    private double farPlane = 8000000.0;
    public double FarPlane { get => farPlane; set => farPlane = value < midPlane ? midPlane + 1 : value; }

    public MCamera(string inname) : base(EType.Camera, inname)
    {
      transform.Position = new Vector3d(15, 15, 15);
      Focus.transform.Position = new Vector3d(-1, -1, -1);
      UpVector = new Vector3d(0, 1, 0);
    }

    public Vector3d Forward()
    {
      return Vector3d.Transform(new Vector3d(0, 0, 1), GetViewMatrix().Inverted().ExtractRotation());
      //return GetViewMatrix().Inverted().ExtractRotation() * new Vector3d(0, 0, 1);
    }

    public void SetTarget(MSceneObject mo)
    {
      Focus = mo;
    }

    public Matrix4d GetProjection(bool Near = true)
    {
      double near = Near == true ? NearPlane : MidPlane;
      double far = Near == true ? MidPlane + 100 : FarPlane;
      return Matrix4d.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV),
        (double)MScreen.Width / (double)MScreen.Height,
        near, far
        );
    }

    public Matrix4 getProjectionMatrix()
    {
      return Matrix4.CreatePerspectiveFieldOfView((float)MathHelper.DegreesToRadians(FOV),
      (float)MScreen.Width / (float)MScreen.Height,
      0.1f, 10000
      );
    }


    public Matrix4 getViewMatrix()
    {
      return MTransform.GetFloatMatrix(GetViewMatrix());
    }

    //from 1m to far
    public Matrix4d GetFullProjection(double Near=1, double Far=20000)
    {
      Near = Near < 0.5 ? Near = 0.5 : Near;
      Near = Near > Far ? Near = Far - 0.1 : Near;
      return Matrix4d.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV),
        (double)MScreen.Width / (double)MScreen.Height,
        Near, Far
        );
    }

    public Matrix4d GetOverlayProjection()
    {
      return Matrix4d.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV),
        (double)MScreen.Width / (double)MScreen.Height,
        1, 200
        );
    }

    public void Move(Vector3d delta)
    {
      transform.Position += delta;
      Focus.transform.Position += delta;
    }

    public Vector3d Direction()
    {
      return (Focus.transform.Position - transform.Position).Normalized();
    }

    public Vector3d SideDirection()
    {
      return Vector3d.Cross(Vector3d.UnitY, Direction());
    }


    public Matrix4 getMVP()
    {
      Matrix4d offsetmat = Matrix4d.CreateTranslation(-Globals.GlobalOffset);
      Matrix4d view = GetViewMatrix();
      Matrix4d projection = GetProjection(true);
      Matrix4d viewproj = view * projection;
      return mvp = MTransform.GetFloatMatrix(viewproj);      
    }

    public Matrix4d GetViewMatrix()
    {
      Matrix4d m4 = Matrix4d.Identity;
      Vector3d Offset = new Vector3d(0, 0, 0);
      if (Globals.Avatar.Target != null)
      {
        Offset = Globals.Avatar.Forward();
      };
      //lookat = Target.transform.Position;       
      Vector3d delta = transform.Position - Globals.GlobalOffset;

      //need to use avatar positions to avoid successive frames jumping after lookat adjusts forward
      m4 = Matrix4d.LookAt(
      delta,
      (Focus.transform.Position - Globals.GlobalOffset) + Offset,
      // lookat, 
      //Globals.Avatar.Up()
      UpVector
      //transform.Up()
      );

      transform.Rotation = m4.Inverted().ExtractRotation(false); //view matrix is inverse

      return m4;
    }

    public Matrix4d GetOverlayViewMatrix()
    {
      Matrix4d m4 = Matrix4d.Identity;
      m4 = Matrix4d.LookAt(
     new Vector3d(0, 0, -10),
     Vector3d.Zero,
     Vector3d.UnitY
     );
      return m4;
    }

    //viewport Z = width and W = height
    public Vector3d UnProject(Vector3d mouse, RectD viewport)
    {
      Vector4d vec;
      vec.X = 2.0f * mouse.X / (float)viewport.Max.X - 1;
      vec.Y = -(2.0f * mouse.Y / (float)viewport.Max.Y - 1);
      vec.Z = mouse.Z;
      vec.W = 1.0f;

      Matrix4d viewInv = Matrix4d.Invert(GetViewMatrix());
      Matrix4d projInv = Matrix4d.Invert(GetProjection(true));

      Vector4d.Transform(ref vec, ref projInv, out vec);
      Vector4d.Transform(ref vec, ref viewInv, out vec);

      if (vec.W > 0.000001f || vec.W < -0.000001f)
      {
        vec.X /= vec.W;
        vec.Y /= vec.W;
        vec.Z /= vec.W;
      }

      return vec.Xyz;
    }

    public static Vector3d Project(Vector4d point, Matrix4d projection, Matrix4d modelview, int[] view)
    {
      Matrix4d temp = Matrix4d.Mult(projection, modelview);
      // multiply matrix by vector
      Vector4d v_prime = new Vector4d(
          Vector4d.Dot(temp.Row0, point),
          Vector4d.Dot(temp.Row1, point),
          Vector4d.Dot(temp.Row2, point),
          Vector4d.Dot(temp.Row3, point)
          );

      v_prime = Vector4d.Divide(v_prime, v_prime.W);

      return new Vector3d(
          view[0] + view[2] * (v_prime.X + 1) / 2,
          view[1] + view[3] * (v_prime.Y + 1) / 2,
          (v_prime.Z + 1) / 2
          );
    }
  }
}
