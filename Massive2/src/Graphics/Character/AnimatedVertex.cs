using OpenTK;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct AnimatedVertex
{
  public const int Size = (3 + 3 + 2) * 4; // size of struct in bytes
  public Vector3 _position;
  public Vector3 _normal;
  public Vector2 _textureCoordinate;

  // public Vector4 _BoneID; //4
  // public Vector4 _BoneWeight; //4;

  public AnimatedVertex(Vector3 position, Vector3 Normal, Vector2 textureCoordinate)
  {
    _position = position;
    _normal = Normal;
    _textureCoordinate = textureCoordinate;
  }
  /*public AnimatedVertex(Vector3 position, Vector3 Normal, Vector2 textureCoordinate, float[] BoneIDs, float[] BoneWeights)
  {
    _position = position;
    _normal = Normal;
    _textureCoordinate = textureCoordinate;
    _BoneID = BoneIDs;
    _BoneWeight = BoneWeights;
  }*/

}