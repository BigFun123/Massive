using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  struct SpFace
  {
    public Vector3 V1;
    public Vector3 V2;
    public Vector3 V3;

    public SpFace(Vector3 v1, Vector3 v2, Vector3 v3)
    {
      V1 = v1;
      V2 = v2;
      V3 = v3;
    }
  }

  public class MSphere : MSceneObject
  { 
    int recursionLevel = 3;
    private List<Vector3> _points;
    private int _index;
    private Dictionary<long, int> _middlePointIndexCache;
   


    public MSphere(string inname, int Recursion) : base(EType.PrimitiveSphere, inname)
    {
      recursionLevel = Recursion;
    }
    public override void Setup()
    {

      _middlePointIndexCache = new Dictionary<long, int>();
      _points = new List<Vector3>();
      _index = 0;
      var t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0);
      var s = 1;

      AddVertex(new Vector3(-s, t, 0));
      AddVertex(new Vector3(s, t, 0));
      AddVertex(new Vector3(-s, -t, 0));
      AddVertex(new Vector3(s, -t, 0));

      AddVertex(new Vector3(0, -s, t));
      AddVertex(new Vector3(0, s, t));
      AddVertex(new Vector3(0, -s, -t));
      AddVertex(new Vector3(0, s, -t));

      AddVertex(new Vector3(t, 0, -s));
      AddVertex(new Vector3(t, 0, s));
      AddVertex(new Vector3(-t, 0, -s));
      AddVertex(new Vector3(-t, 0, s));

      var faces = new List<SpFace>();

      // 5 faces around point 0
      faces.Add(new SpFace(_points[0], _points[11], _points[5]));
      faces.Add(new SpFace(_points[0], _points[5], _points[1]));
      faces.Add(new SpFace(_points[0], _points[1], _points[7]));
      faces.Add(new SpFace(_points[0], _points[7], _points[10]));
      faces.Add(new SpFace(_points[0], _points[10], _points[11]));

      // 5 adjacent faces 
      faces.Add(new SpFace(_points[1], _points[5], _points[9]));
      faces.Add(new SpFace(_points[5], _points[11], _points[4]));
      faces.Add(new SpFace(_points[11], _points[10], _points[2]));
      faces.Add(new SpFace(_points[10], _points[7], _points[6]));
      faces.Add(new SpFace(_points[7], _points[1], _points[8]));

      // 5 faces around point 3
      faces.Add(new SpFace(_points[3], _points[9], _points[4]));
      faces.Add(new SpFace(_points[3], _points[4], _points[2]));
      faces.Add(new SpFace(_points[3], _points[2], _points[6]));
      faces.Add(new SpFace(_points[3], _points[6], _points[8]));
      faces.Add(new SpFace(_points[3], _points[8], _points[9]));

      // 5 adjacent faces 
      faces.Add(new SpFace(_points[4], _points[9], _points[5]));
      faces.Add(new SpFace(_points[2], _points[4], _points[11]));
      faces.Add(new SpFace(_points[6], _points[2], _points[10]));
      faces.Add(new SpFace(_points[8], _points[6], _points[7]));
      faces.Add(new SpFace(_points[9], _points[8], _points[1]));



      // refine triangles
      for (int i = 0; i < recursionLevel; i++)
      {
        var faces2 = new List<SpFace>();
        foreach (var tri in faces)
        {
          // replace triangle by 4 triangles
          int a = GetMiddlePoint(tri.V1, tri.V2);
          int b = GetMiddlePoint(tri.V2, tri.V3);
          int c = GetMiddlePoint(tri.V3, tri.V1);

          faces2.Add(new SpFace(tri.V1, _points[a], _points[c]));
          faces2.Add(new SpFace(tri.V2, _points[b], _points[a]));
          faces2.Add(new SpFace(tri.V3, _points[c], _points[b]));
          faces2.Add(new SpFace(_points[a], _points[b], _points[c]));
        }
        faces = faces2;
      }


      // done, now add triangles to mesh
      var vertices = new List<TexturedVertex>();

      Random r = new Random();
      foreach (var tri in faces)
      {
        var uv1 = GetSphereCoord(tri.V1);
        var uv2 = GetSphereCoord(tri.V2);
        var uv3 = GetSphereCoord(tri.V3);
        FixColorStrip(ref uv1, ref uv2, ref uv3);

        float rf = (float)r.NextDouble();
        vertices.Add(new TexturedVertex(new Vector3(tri.V1), new Vector3(rf,rf, 0), uv1));
        rf = (float)r.NextDouble();
        vertices.Add(new TexturedVertex(new Vector3(tri.V2), new Vector3(rf, rf, 0), uv2));
        rf = (float)r.NextDouble();
        vertices.Add(new TexturedVertex(new Vector3(tri.V3), new Vector3(rf, rf, 0), uv3));
      }

      Vertices = vertices.ToArray();

      GL.GenVertexArrays(1, out VAO);
      GL.GenBuffers(1, out VBO);
      // fill buffer      
      GL.BindBuffer(BufferTarget.ArrayBuffer,VBO);
      GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * TexturedVertex.Size, Vertices, BufferUsageHint.StaticDraw);
      // link vertex attributes
      GL.BindVertexArray(VAO);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), IntPtr.Zero);
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
      GL.EnableVertexAttribArray(2);
      GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);

      base.Setup();
    }

    /*
    public override void Render(Matrix4d viewproj)
    {
      mvp = MTransform.GetFloatMatrix(GetMatrix() * viewproj);
      model = MTransform.GetFloatMatrix(GetMatrix());

      if (Globals.ShaderOverride == null)
      {
        material.shader.Bind();
        //material.shader.SetMat4("model", model);
        material.shader.SetMat4("mvp", mvp);
        material.shader.SetMat4("model", model);
        material.texture.Bind();
      }
      else
      {
        Globals.ShaderOverride.SetMat4("mvp", mvp);
      }

      // render Cube
      GL.BindVertexArray(VAO);
      GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length);
      GL.BindVertexArray(0);

      // base.Render();
    }
    */

    private int AddVertex(Vector3 p)
    {
      _points.Add(p.Normalized());
      return _index++;
    }

    public static Vector2 GetSphereCoord(Vector3 i)
    {
      var len = i.Length;
      Vector2 uv;
      uv.Y = (float)(Math.Acos(i.Y / len) / Math.PI);
      uv.X = -(float)((Math.Atan2(i.Z, i.X) / Math.PI + 1.0f) * 0.5f);
      return uv;
    }

    private static void FixColorStrip(ref Vector2 uv1, ref Vector2 uv2, ref Vector2 uv3)
    {
      if ((uv1.X - uv2.X) >= 0.8f)
        uv1.X -= 1;
      if ((uv2.X - uv3.X) >= 0.8f)
        uv2.X -= 1;
      if ((uv3.X - uv1.X) >= 0.8f)
        uv3.X -= 1;

      if ((uv1.X - uv2.X) >= 0.8f)
        uv1.X -= 1;
      if ((uv2.X - uv3.X) >= 0.8f)
        uv2.X -= 1;
      if ((uv3.X - uv1.X) >= 0.8f)
        uv3.X -= 1;
    }


    // return index of point in the middle of p1 and p2
    private int GetMiddlePoint(Vector3 point1, Vector3 point2)
    {
      long i1 = _points.IndexOf(point1);
      long i2 = _points.IndexOf(point2);
      // first check if we have it already
      var firstIsSmaller = i1 < i2;
      long smallerIndex = firstIsSmaller ? i1 : i2;
      long greaterIndex = firstIsSmaller ? i2 : i1;
      long key = (smallerIndex << 32) + greaterIndex;

      int ret;
      if (_middlePointIndexCache.TryGetValue(key, out ret))
      {
        return ret;
      }

      // not in cache, calculate it

      var middle = new Vector3(
          (point1.X + point2.X) / 2.0f,
          (point1.Y + point2.Y) / 2.0f,
          (point1.Z + point2.Z) / 2.0f);

      // add vertex makes sure point is on unit sphere
      int i = AddVertex(middle);

      // store it, return index
      _middlePointIndexCache.Add(key, i);
      return i;
    }
  }
}
