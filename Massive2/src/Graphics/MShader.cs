using Massive.Platform;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MShader : MObject
  {
    public const int LOCATION_DIFFUSE = 0;
    public const int LOCATION_SPECULAR = 1;
    public const int LOCATION_MULTITEX = 2;
    public const int LOCATION_NORMALMAP = 3;
    public const int LOCATION_SHADOWMAP = 4;

    //public EventHandler<ErrorEvent> CompilerEvent;
    //TODO: Cache GetUniformLocation calls
    public const string DEFAULT_SHADER = "DefaultShader";
    public const string TERRAIN_SHADER = "TerrainShader";
    private int programID;
    public int ProgramID { get => programID; set => programID = value; }

    public string VertexShaderPath { get; set; }
    public string FragmentShaderPath { get; set; }
    public string FunctionsShaderPath { get; set; }
    public string VertexShaderCode;
    public string FragmentShaderCode;
    public string FragmentFunctionsShaderCode;

    public string EvalShaderPath { get; set; }
    public string ControlShaderPath { get; set; }

    public MShader(string inname) : base(EType.Shader, inname)
    {
    }

    public int GetLocation(string sName)
    {
      return GL.GetUniformLocation(ProgramID, sName);
    }

    public void Bind()
    {
      GL.UseProgram(ProgramID);
      //Helper.CheckGLError(this, "TestPoint MShader");
    }

    public void SetBool(string name, bool value)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      GL.Uniform1(location, value == true ? 1 : 0);
    }

    public void SetInt(string name, int value)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      GL.Uniform1(location, value);
    }

    public void SetFloat(string name, float value)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      if (location != -1)
      {
        GL.Uniform1(location, value);
      }
    }

    public void SetVec2(string name, Vector2 v)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      GL.Uniform2(location, ref v);
    }

    public void SetVec3(string name, Vector3 v)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      GL.Uniform3(location, ref v);
    }

    public void SetVec4(string name, Vector4 v)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      GL.Uniform4(location, ref v);
    }

    public void SetVec3d(string name, Vector3d v)
    {
      //int location = GL.GetUniformLocation(ProgramID, name);
      int location = GetLocation(name);
      double[] d = { v.X, v.Y, v.Z };
      GL.Uniform3(location, v.X, v.Y, v.Z);
    }

    public void SetMat4(string sPropName, Matrix4 d)
    {
      int location = GetLocation(sPropName);
      GL.UniformMatrix4(location, false, ref d);
    }

    public void SetMatrices(string sPropName, Matrix4[] matrices)
    {
      int location = GetLocation(sPropName);
      GL.UniformMatrix4(location, matrices.Length, true, ref matrices[0].Row0.X);
    }

    //public void SetMat4d(string sPropName, Matrix4d d)
    //{
    //int location = GL.GetUniformLocation(ProgramID, sPropName);
    //int location = GetLocation(sPropName);
    //GL.UniformMatrix4(location, 1, false, d.);
    //}

    public override void Dispose()
    {
      if (ProgramID != 0)
      {
        //GL.DeleteProgram(ProgramID);
        //ProgramID = 0;
      }
    }

    public string Recompile()
    {
      string sResult = "OK";
      if (ProgramID != 0)
      {
        GL.DeleteProgram(ProgramID);
      }

      ProgramID = GL.CreateProgram();
      int vertexShader = GL.CreateShader(ShaderType.VertexShader);
      GL.ShaderSource(vertexShader, VertexShaderCode);
      GL.CompileShader(vertexShader);
      sResult += GL.GetShaderInfoLog(vertexShader);

      int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

      string Combined = FragmentShaderCode.Replace("#include \"functions_f.glsl\";", FragmentFunctionsShaderCode);
      GL.ShaderSource(fragmentShader, Combined);
      GL.CompileShader(fragmentShader);
      sResult += GL.GetShaderInfoLog(fragmentShader);

      //==================

      GL.AttachShader(ProgramID, vertexShader);
      GL.AttachShader(ProgramID, fragmentShader);

      GL.LinkProgram(ProgramID);

      sResult += GL.GetProgramInfoLog(ProgramID);      

      GL.UseProgram(0);
      GL.DetachShader(ProgramID, vertexShader);
      GL.DetachShader(ProgramID, fragmentShader);
      GL.DeleteShader(vertexShader);
      GL.DeleteShader(fragmentShader);

      sResult += ". Program:" + ProgramID;

      Bind();
      SetInt("material.diffuse", MShader.LOCATION_DIFFUSE);
      SetInt("material.specular", MShader.LOCATION_SPECULAR);
      SetInt("material.multitex", MShader.LOCATION_MULTITEX);
      SetInt("material.normalmap", MShader.LOCATION_NORMALMAP);
      SetInt("material.shadowMap", MShader.LOCATION_SHADOWMAP);

      return sResult;
    }

    public int Compile()
    {      
      string sVertexPath = Path.Combine(MFileSystem.ShadersPath, VertexShaderPath);
      if (!File.Exists(sVertexPath))
      {
        Log(" : File not found:" + sVertexPath + ". Needs AppPathConfig?");
      }
      else
      {
        VertexShaderCode = File.ReadAllText(Path.Combine(MFileSystem.ShadersPath, VertexShaderPath));        
      }
      
      string sFragmentPath = Path.Combine(MFileSystem.ShadersPath, FragmentShaderPath);
      if (!File.Exists(sFragmentPath))
      {
        Log(sFragmentPath + " : File not found");
      }
      else
      {
        FragmentShaderCode = File.ReadAllText(Path.Combine(MFileSystem.ShadersPath, FragmentShaderPath));      
      }

      string sFunctionPath = Path.Combine(MFileSystem.ShadersPath, FunctionsShaderPath);
      if (!File.Exists(sFunctionPath))
      {
        Log(sFunctionPath + " : File not found");
      }
      else
      {        
        FragmentFunctionsShaderCode = File.ReadAllText(sFunctionPath);
      }

      Error = Recompile();
      return ProgramID;
    }

    public int Load(string sPathVert, string sPathFrag, string sPathEval, string sPathControl)
    {
      VertexShaderPath = sPathVert;
      FragmentShaderPath = sPathFrag;
      EvalShaderPath = sPathEval;
      ControlShaderPath = sPathControl;
      FunctionsShaderPath = "Common\\functions_f.glsl";

      return Compile();
    }

    public string LoadFromString(string sVertexShader, string sFragmentShader)
    {
      if (ProgramID > 0)
      {
        GL.DeleteProgram(ProgramID);
      }

      ProgramID = GL.CreateProgram();
      string info = "";

      int vertexShader = GL.CreateShader(ShaderType.VertexShader);
      GL.ShaderSource(vertexShader, sVertexShader);
      GL.CompileShader(vertexShader);

      info = GL.GetShaderInfoLog(vertexShader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine($"VERTEX GL.CompileShader had info log: {info}");
        Log(info);
      }
      GL.AttachShader(ProgramID, vertexShader);


      int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fragmentShader, sFragmentShader);
      GL.CompileShader(fragmentShader);

      info += GL.GetShaderInfoLog(fragmentShader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.WriteLine($"FRAGMENT GL.CompileShader had info log: {info}");
        Log(info);
      }

      GL.AttachShader(ProgramID, fragmentShader);
      GL.LinkProgram(ProgramID);

      info += GL.GetProgramInfoLog(ProgramID);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Console.Error.WriteLine(info);
        Log(info);
      }
      GL.UseProgram(ProgramID);
      GL.DetachShader(ProgramID, vertexShader);
      GL.DetachShader(ProgramID, fragmentShader);
      GL.DeleteShader(vertexShader);
      GL.DeleteShader(fragmentShader);
      return info;
    }

    public void Log(string s)
    {
      Globals.Log(this, s);
      Console.WriteLine(s);
    }

  }
}
