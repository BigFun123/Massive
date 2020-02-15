using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{

public static class Extensions
  {
    internal static readonly double MachineEpsilonFloat = GetMachineEpsilonFloat();

    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
      if (val.CompareTo(min) < 0) return min;
      else if (val.CompareTo(max) > 0) return max;
      else return val;
    }

    /// <summary>
    /// Calculates the rotation required to turn from source to dest
    /// </summary>
    /// <param name="sourcePoint"></param>
    /// <param name="destPoint"></param>
    /// <param name="UpVector"></param>
    /// <returns></returns>
    public static Quaterniond LookAt(Vector3d sourcePoint, Vector3d destPoint, Vector3d UpVector)
    {
      Vector3d forwardVector = Vector3d.Normalize(destPoint - sourcePoint);

      Vector3d rotAxis = Vector3d.Cross(UpVector, forwardVector);
      double dot = Vector3d.Dot(UpVector, forwardVector);

      Quaterniond q = new Quaterniond(rotAxis.X, rotAxis.Y, rotAxis.Z, dot + 1);

      return q.Normalized();
    }

    public static Vector3d SmoothStep(Vector3d Source, Vector3d Destination, double d)
    {
      Vector3d res = new Vector3d(Source);
      res.X = SmoothStep(Source.X, Destination.X, d);
      res.Y = SmoothStep(Source.Y, Destination.Y, d);
      res.Z = SmoothStep(Source.Z, Destination.Z, d);

      return res;
    }

    public static double SmoothStep(double value1, double value2, double amount)
    {
      /* It is expected that 0 < amount < 1.
       * If amount < 0, return value1.
       * If amount > 1, return value2.
       */
      double result = Clamp(amount, 0, 1);
      result = Hermite(value1, 0, value2, 0, result);

      return result;
    }

    public static double Hermite(
      double value1,
      double tangent1,
      double value2,
      double tangent2,
      double amount
    )
    {
      /* All transformed to double not to lose precision
			 * Otherwise, for high numbers of param:amount the result is NaN instead
			 * of Infinity.
			 */
      double v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount;
      double result;
      double sCubed = s * s * s;
      double sSquared = s * s;

      if (WithinEpsilon(amount, 0f))
      {
        result = value1;
      }
      else if (WithinEpsilon(amount, 1f))
      {
        result = value2;
      }
      else
      {
        result = (
          ((2 * v1 - 2 * v2 + t2 + t1) * sCubed) +
          ((3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared) +
          (t1 * s) +
          v1
        );
      }

      return result;
    }

    internal static bool WithinEpsilon(double floatA, double floatB)
    {
      return Math.Abs(floatA - floatB) < MachineEpsilonFloat;
    }

    private static double GetMachineEpsilonFloat()
    {
      double machineEpsilon = 1.0f;
      double comparison;

      /* Keep halving the working value of machineEpsilon until we get a number that
			 * when added to 1.0f will still evaluate as equal to 1.0f.
			 */
      do
      {
        machineEpsilon *= 0.5f;
        comparison = 1.0f + machineEpsilon;
      }
      while (comparison > 1.0f);

      return machineEpsilon;
    }

    public static Quaterniond ComputeW(Quaterniond q)
    {
      double t = 1.0f - (q.X * q.X) - (q.Y * q.Y) - (q.Z * q.Z);
      double w = 0.0f;
      if (t >= 0.0f)
        w = -Convert.ToSingle(System.Math.Sqrt(t));

      return new Quaterniond(q.Xyz, w);
    }

    public static Matrix4[] InterpolateMatrix(Matrix4[] prev, Matrix4[] next, float blend)
    {
      Matrix4[] result = new Matrix4[prev.Length];

      for (int i = 0; i < prev.Length; i++)
      {
        Vector3 positionInter = Vector3.Lerp(prev[i].ExtractTranslation(), next[i].ExtractTranslation(), blend);
        Vector3 scaleInter = Vector3.Lerp(prev[i].ExtractScale(), next[i].ExtractScale(), blend);
        Quaternion rotationInter = Quaternion.Slerp(prev[i].ExtractRotation(), next[i].ExtractRotation(), blend);

        result[i] = Matrix4.CreateFromQuaternion(rotationInter) * Matrix4.CreateTranslation(positionInter) * Matrix4.CreateScale(scaleInter);
      }
      return result;
    }
  }

}
