using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massive.Platform;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Massive
{
  public class MGeomShader : MShader
  {
    //int ProgramID;
    private object sPathVert;

    public MGeomShader(string sName = "GeomShader")
      :base (sName)
    {

    }

   // public void Bind()
    //{
     // GL.UseProgram(ProgramID);
   // }

    public void Load()
    {
      ProgramID = GL.CreateProgram();
      int vertexShader = GL.CreateShader(ShaderType.VertexShader); ;
      string svsPath = Path.Combine(MFileSystem.AssetsPath, "Shaders\\Animation\\animated_vs.glsl");
      string sData = File.ReadAllText(svsPath);
      GL.ShaderSource(vertexShader, sData);
      GL.CompileShader(vertexShader);
      var info = GL.GetShaderInfoLog(vertexShader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine($"GL.CompileShader had info log: {info}");
      }

      int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      string sfsPath = Path.Combine(MFileSystem.AssetsPath, "Shaders\\Animation\\fragment.glsl");
      GL.ShaderSource(fragmentShader, File.ReadAllText(sfsPath));
      GL.CompileShader(fragmentShader);
      info = GL.GetShaderInfoLog(fragmentShader);
      if (!string.IsNullOrWhiteSpace(info))
        Console.WriteLine($"GL.CompileShader had info log: {info}");

      int geomShader = GL.CreateShader(ShaderType.GeometryShader);
      string sgPath = Path.Combine(MFileSystem.AssetsPath, "Shaders\\Animation\\geometry.glsl");
      GL.ShaderSource(geomShader, File.ReadAllText(sgPath));
      GL.CompileShader(geomShader);
      info = GL.GetShaderInfoLog(geomShader);
      if (!string.IsNullOrWhiteSpace(info))
        Console.WriteLine($"GL.CompileShader had info log: {info}");

      GL.AttachShader(ProgramID, vertexShader);
      GL.AttachShader(ProgramID, fragmentShader);
      GL.AttachShader(ProgramID, geomShader);
      GL.LinkProgram(ProgramID);

      info = GL.GetProgramInfoLog(ProgramID);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine(info);
        Console.Error.Write(info);
      }
    }

    //public void SetMat4(string sPropName, Matrix4 d)
    //{
      //int vertexColorLocation = GL.GetUniformLocation(ProgramID, sPropName);
      //GL.UniformMatrix4(vertexColorLocation, false, ref d);
    //}
  }
}
