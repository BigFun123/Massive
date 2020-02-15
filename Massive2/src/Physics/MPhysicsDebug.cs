using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;
using Massive.Tools;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Massive
{
  [StructLayout(LayoutKind.Sequential)]
  public struct PositionColored
  { 
    public static readonly int Stride = Vector3d.SizeInBytes + sizeof(int);
    public Vector3d Position;
    public Vector4 Color;    

    public PositionColored(Vector3d pos, Vector4 v)
    {
      Position = pos;
      Color = v;
    }

    public PositionColored(ref Vector3d pos, Vector4 col)
    {
      Position = pos;
      Color = col;
    }
  }

  public class MPhysicsDebug : DebugDraw
  {
    static int shaderProgram;
    static int vertexInfo;
    static int lineVertexBuffer;
    static int vertexCount;
    protected List<PositionColored> lines = new List<PositionColored>();
    public bool UserColorCoding = true;

    public MPhysicsDebug()
    {
      Setup();      
    }

    void Setup()
    {
     
      int vshader = GL.CreateShader(ShaderType.VertexShader);
      GL.ShaderSource(vshader, @"#version 430
            in vec3 vPosition;            
            uniform mat4 mvp;            
            void main()
            {                                          
               gl_Position = mvp * vec4(vPosition,1);
            }");
      GL.CompileShader(vshader);
      var info = GL.GetShaderInfoLog(vshader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine(info);
      }
      
      //Helper.CheckGLError(new MObject(MObject.EType.Other, "MPhysicsDebug"));
      int fshader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fshader, @"#version 430
            out vec4 fragColor;
            uniform vec4 mcolor;
            void main()
            {
               //fragColor = vec4(1.0, 0.5, 0.5, 0.5);
              fragColor = mcolor;
            }");
      GL.CompileShader(fshader);
      info = GL.GetShaderInfoLog(fshader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine(info);
      }
      shaderProgram = GL.CreateProgram();
      info = GL.GetProgramInfoLog(shaderProgram);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine(info);
      }


      GL.AttachShader(shaderProgram, vshader);
      GL.AttachShader(shaderProgram, fshader);
      GL.LinkProgram(shaderProgram);
      GL.DetachShader(shaderProgram, vshader);
      GL.DetachShader(shaderProgram, fshader);
      GL.UseProgram(shaderProgram);
      //lineVertexBuffer = GL.GenBuffer();
      //Vector3[] lineVertices = { new Vector3(0, 0, 0), new Vector3(.5f, .5f, 0.5f) };
      //vertexCount = lineVertices.Length;
      //GL.BindBuffer(BufferTarget.ArrayBuffer, lineVertexBuffer);
      //GL.BufferData(BufferTarget.ArrayBuffer, System.Runtime.InteropServices.Marshal.SizeOf(lineVertices[0]) * vertexCount,
        // lineVertices, BufferUsageHint.StreamDraw);
      //vertexInfo = GL.GenVertexArray();
      //GL.BindVertexArray(vertexInfo);
      //int locVPosition = GL.GetAttribLocation(shaderProgram, "vPosition");
      //GL.EnableVertexAttribArray(locVPosition);
      //GL.VertexAttribPointer(locVPosition, 3, VertexAttribPointerType.Float, false,
        // System.Runtime.InteropServices.Marshal.SizeOf(lineVertices[0]), 0);
      GL.BindVertexArray(0);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.UseProgram(0);
    }

    protected virtual int ColorToInt(ref Color4 c)
    {
      int ci = c.ToArgb();
      return (ci & 0xff0000) >> 16 | (ci & 0xff00) | (ci & 0xff) << 16;
    }

    public override DebugDrawModes DebugMode {
      //get => throw new NotImplementedException(); set => throw new NotImplementedException();
      get {
        return DebugDrawModes.MaxDebugDrawMode;
      } set {
      }
    }

    public object MasstiveTools { get; private set; }

    public override void Draw3dText(ref Vector3d location, string textString)
    {
      Console.WriteLine(textString);      
    }

    public override void DrawContactPoint(ref Vector3d pointOnB, ref Vector3d normalOnB, double distance, int lifeTime, Color4 color)
    {
      //throw new NotImplementedException();
      lines.Add(new PositionColored(pointOnB, new Vector4(color.R, color.G, color.G, color.A)));
      lines.Add(new PositionColored(pointOnB + normalOnB, new Vector4(color.R, color.G, color.G, color.A)));
    }

    public override void DrawLine(ref Vector3d from, ref Vector3d to, Color4 color)
    {
      //Console.WriteLine(from + " : " + to);
      //if (lines.Count > 500) return;
      lines.Add(new PositionColored(from, new Vector4(color.R, color.G, color.G, color.A)));
      lines.Add(new PositionColored(to, new Vector4(color.R, color.G, color.G, color.A)));
      //throw new NotImplementedException();
    }

    public override void DrawTriangle(ref Vector3d v0, ref Vector3d v1, ref Vector3d v2, Color4 color, double __unnamed004)
    {
      lines.Add(new PositionColored(v0, new Vector4(color.R, color.G, color.G, color.A)));
      lines.Add(new PositionColored(v1, new Vector4(color.R, color.G, color.G, color.A)));
      lines.Add(new PositionColored(v2, new Vector4(color.R, color.G, color.G, color.A)));
    }

    public override void DrawTriangle(ref Vector3d v0, ref Vector3d v1, ref Vector3d v2, ref Vector3d __unnamed003, ref Vector3d __unnamed004, ref Vector3d __unnamed005, Color4 color, double alpha)
    {
      lines.Add(new PositionColored(v0, new Vector4(color.R, color.G, color.G, color.A)));
      lines.Add(new PositionColored(v1, new Vector4(color.R, color.G, color.G, color.A)));
      lines.Add(new PositionColored(v2, new Vector4(color.R, color.G, color.G, color.A)));
    }

    public override void ReportErrorWarning(string warningString)
    {
      //throw new NotImplementedException();
    }


    //TODO: Make work
    public void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      Vector3[] positionArray = new Vector3[lines.Count];
      Vector4[] colorArray = new Vector4[lines.Count];
      int i;

      //Random r = new Random();
      if (lines.Count() == 0) return;

      for (i = 0; i < lines.Count; i++)
      {
        positionArray[i] = MassiveTools.Vector3FromVector3d(lines[i].Position - Globals.GlobalOffset);
        colorArray[i] = lines[i].Color;
      }
      lines.Clear();
      vertexCount = positionArray.Length ;

      //GL.ClearColor(new Color4(0, 0, 0, 0.5f));
      //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

      Matrix4 mvp = MTransform.GetFloatMatrix(viewproj);
      Matrix4 model = MTransform.GetFloatMatrix(parentmodel);

      GL.UseProgram(shaderProgram);

      int loc = GL.GetUniformLocation(shaderProgram, "mvp");      
      GL.UniformMatrix4(loc, false, ref mvp);
      loc = GL.GetUniformLocation(shaderProgram, "model");
      GL.UniformMatrix4(loc, false, ref model);
      
      lineVertexBuffer = GL.GenBuffer();
      if (Globals.Avatar.Target == null) return;
      Vector3 tp = MassiveTools.Vector3FromVector3d(Globals.Avatar.Target.transform.Position);
      //Vector3[] lineVertices = { new Vector3(0, 0, 0), new Vector3((float)r.NextDouble(), .5f, 0.5f) - MassiveTools.FromV3d(Globals.GlobalOffset)};
      //vertexCount = lineVertices.Length;
      
      GL.BindBuffer(BufferTarget.ArrayBuffer, lineVertexBuffer);
      GL.BufferData(BufferTarget.ArrayBuffer, System.Runtime.InteropServices.Marshal.SizeOf(positionArray[0]) * vertexCount,
         positionArray, BufferUsageHint.StreamDraw);
      vertexInfo = GL.GenVertexArray();
      GL.BindVertexArray(vertexInfo);
      int locVPosition = GL.GetAttribLocation(shaderProgram, "vPosition");
      GL.EnableVertexAttribArray(locVPosition);
      GL.VertexAttribPointer(locVPosition, 3, VertexAttribPointerType.Float, false,
         System.Runtime.InteropServices.Marshal.SizeOf(positionArray[0]), 0);
      //GL.BindVertexArray(0);
      //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      //GL.UseProgram(0);
      //GL.ClearColor(Color4.Black);
      GL.Clear(ClearBufferMask.DepthBufferBit);      

      int loca = GL.GetUniformLocation(shaderProgram, "mcolor");
      if ( UserColorCoding == true)
      {
        for (int ri = 0; ri <= positionArray.Count() - 2; ri += 2)
        {
          Vector4 v = colorArray[ri];
          GL.Uniform4(loca, ref v);
          GL.DrawArrays(PrimitiveType.LineStrip, ri, 2);
        }
      }
      else
      {
        Vector4 v = new Vector4(1, 1, 1, 0.9f);
        GL.Uniform4(loca, ref v);
        GL.DrawArrays(PrimitiveType.LineStrip, 0, positionArray.Count());
      }



      //GL.DrawArrays(PrimitiveType.LineStrip, ri, vertexCount);


      GL.BindVertexArray(0);
      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.UseProgram(0);
    }
  }
}
