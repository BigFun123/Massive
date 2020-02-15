using OpenTK;

namespace Massive2.Graphics.Character
{
  class MPose
  {
    string Name;
    private Matrix4[] boneTransformations;

    public Matrix4 this[int i]
    {
      get { return boneTransformations[i]; }
      set { boneTransformations[i] = value; }
    }

    public Matrix4[] MatrixArray { get { return boneTransformations; } }

    #region Constructors

    public MPose(int count)
    {
      boneTransformations = new Matrix4[count];
    }

    public MPose(int count, Matrix4 template)
    {
      boneTransformations = new Matrix4[count];
      for (int i = 0; i < count; i++)
        boneTransformations[i] = template;
    }

    public MPose(Matrix4[] matrices)
    {
      boneTransformations = matrices;
    }

    #endregion

    #region Conversion

    public Vector3 Position(int boneIndex)
    {
      return boneTransformations[boneIndex].ExtractTranslation();
    }

    public Quaternion Rotation(int boneIndex)
    {
      return boneTransformations[boneIndex].ExtractRotation();
    }

    public void Set(int boneIndex, Vector3 position, Quaternion rotation)
    {
      boneTransformations[boneIndex] = Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateTranslation(position);
    }

    #endregion

    #region Clone

    public MPose Clone(string name)
    {
      return new MPose((Matrix4[])this.boneTransformations.Clone()) { Name = name };
    }
    #endregion
  }
}
