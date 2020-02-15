using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace ShaderPlay
{
  public class MGeomShader
  {
    int ProgramID;
    private object sPathVert;

    public void Bind()
    {
      GL.UseProgram(ProgramID);
    }

    public void Setup()
    {
      ProgramID = GL.CreateProgram();
      int vertexShader = GL.CreateShader(ShaderType.VertexShader); ;
      string sData = File.ReadAllText(@"shaders\normal_vs.glsl");
      GL.ShaderSource(vertexShader, sData);
      GL.CompileShader(vertexShader);


      var info = GL.GetShaderInfoLog(vertexShader);
      if (!string.IsNullOrWhiteSpace(info))
        Debug.WriteLine($"GL.CompileShader had info log: {info}");

      int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fragmentShader, File.ReadAllText(@"shaders\normal_fs.glsl"));
      GL.CompileShader(fragmentShader);

      info = GL.GetShaderInfoLog(fragmentShader);
      if (!string.IsNullOrWhiteSpace(info))
        Debug.WriteLine($"GL.CompileShader had info log: {info}");

      int geomShader = GL.CreateShader(ShaderType.GeometryShader);
      GL.ShaderSource(geomShader, File.ReadAllText(@"shaders\normal_geo.glsl"));
      GL.CompileShader(geomShader);

      info = GL.GetShaderInfoLog(geomShader);
      if (!string.IsNullOrWhiteSpace(info))
        Debug.WriteLine($"GL.CompileShader had info log: {info}");


      GL.AttachShader(ProgramID, vertexShader);
      GL.AttachShader(ProgramID, fragmentShader);
      GL.AttachShader(ProgramID, geomShader);
      GL.LinkProgram(ProgramID);

      info = GL.GetProgramInfoLog(ProgramID);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Debug.WriteLine(info);
        Console.Error.Write(info);
      }
    }

    public void SetMat4(string sPropName, Matrix4 d)
    {
      int vertexColorLocation = GL.GetUniformLocation(ProgramID, sPropName);
      GL.UniformMatrix4(vertexColorLocation, false, ref d);
    }
  }
}
