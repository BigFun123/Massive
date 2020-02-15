using OpenTK;

public struct TexturedVertexAnim
{
  public const int Size = (3 + 3 + 2 + 4 + 4 ) * 4; // size of struct in bytes

  public Vector3 _position;
  public Vector3 _normal;
  public Vector2 _textureCoordinate;
  public Vector4 _boneIndex;
  public Vector4 _weight; //3 bones per vertex. 

  public TexturedVertexAnim(Vector3 position, 
    Vector3 Normal, 
    Vector2 textureCoordinate,     
    Vector4 Index,
    Vector4 Weight)
  {
    _position = position;
    _normal = Normal;
    _textureCoordinate = textureCoordinate;
    _boneIndex = Index;
    _weight = Weight;
  }

}