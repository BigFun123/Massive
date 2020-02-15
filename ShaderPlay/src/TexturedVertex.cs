using OpenTK;

public struct TexturedVertex
{
  public const int Size = (3 + 3 + 2) * 4; // size of struct in bytes

  public Vector3 _position;
  public Vector3 _normal;
  public Vector2 _textureCoordinate;

  public TexturedVertex(Vector3 position, Vector3 Normal, Vector2 textureCoordinate)
  {
    _position = position;
    _normal = Normal;
    _textureCoordinate = textureCoordinate;
  }

}