using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics;
using System.IO;

namespace ShaderPlay
{
  public class MShader
  {
    string Name = "";
    public string PathToFragmentShader;
    public string PathToVertexShader;
    public string PathToTesselationControl;
    public string PathToTesselationEval;
    public string PathToTesselationCompute;

    int fragmentShader = -1;
    int vertexShader = -1;
    int tessc = -1;
    int tesse = -1;

    private int programID;
    public int ProgramID { get => programID; set => programID = value; }

    private string textureFile;
    public string TextureFile { get => textureFile; set => textureFile = value; }

    public MShader(string _name)
    {
      Name = _name;

      Load(@"shaders\default_vs.glsl", @"shaders\default_fs.glsl");
      //Load(@"shaders\grass\grass.vert", @"shaders\grass\grass.frag"
///, @"shaders\grass\grass.tesc", @"shaders\grass\grass.tese");
      Bind();
      SetInt("texturemap1", 0);
      SetInt("texturemap2", 1);
      SetInt("shadowMap", 1);
    }

    public string CompileFragment(string sData)
    {
      string sResult = "OK";
      GL.DeleteShader(fragmentShader);
      fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fragmentShader, sData);
      GL.CompileShader(fragmentShader);
      var info = GL.GetShaderInfoLog(fragmentShader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        // Debug.WriteLine($"GL.CompileShader had info log: {info}");
        sResult = info;
      }
      int num = Link();

      return "Fragment Shader:" + sResult + "\r\nProgramID:" + num;
    }

    public string CompileVertexShader(string sData)
    {
      string sResult = "OK";
      GL.DeleteShader(vertexShader);
      vertexShader = GL.CreateShader(ShaderType.VertexShader);
      GL.ShaderSource(vertexShader, sData);
      GL.CompileShader(vertexShader);
      var info = GL.GetShaderInfoLog(vertexShader);
      if (!string.IsNullOrWhiteSpace(info))
      {
        // Debug.WriteLine($"GL.CompileShader had info log: {info}");
        sResult = info;
      }
      int num = Link();

      return "Vertex Shader:" + sResult + "\r\nProgramID:" + num;
    }

    public int Link()
    {
      GL.AttachShader(ProgramID, vertexShader);
      GL.AttachShader(ProgramID, fragmentShader);
      GL.LinkProgram(ProgramID);

      string info = GL.GetProgramInfoLog(ProgramID);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Debug.WriteLine(info);
        Console.Error.Write(info);
      }

      GL.DetachShader(ProgramID, vertexShader);
      GL.DetachShader(ProgramID, fragmentShader);
      //GL.DeleteShader(vertexShader);
      //GL.DeleteShader(fragmentShader);
      return ProgramID;
    }

    public int Load(string sPathVert, string sPathFrag,
      string sTessControl = "", string sTessEval = "", string sCompute = "")
    {
      PathToFragmentShader = sPathFrag;
      PathToVertexShader = sPathVert;
      PathToTesselationControl = sTessControl;
      PathToTesselationEval = sTessEval;
      PathToTesselationCompute = sCompute;

      ProgramID = GL.CreateProgram();

      vertexShader = GL.CreateShader(ShaderType.VertexShader);

      string sData = File.ReadAllText(sPathVert);
      GL.ShaderSource(vertexShader, sData);
      GL.CompileShader(vertexShader);

      var info = GL.GetShaderInfoLog(vertexShader);
      if (!string.IsNullOrWhiteSpace(info))
        Debug.WriteLine($"GL.CompileShader had info log: {info}");

      fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
      GL.ShaderSource(fragmentShader, File.ReadAllText(sPathFrag));
      GL.CompileShader(fragmentShader);

      info = GL.GetShaderInfoLog(fragmentShader);
      if (!string.IsNullOrWhiteSpace(info))
        Debug.WriteLine($"GL.CompileShader had info log: {info}");

      if (!string.IsNullOrEmpty(PathToTesselationControl))
      {
        string sTessC = File.ReadAllText(PathToTesselationControl);
        tessc = GL.CreateShader(ShaderType.TessControlShader);
        GL.ShaderSource(tessc, sTessC);
        GL.CompileShader(tessc);
        info = GL.GetShaderInfoLog(fragmentShader);
        if (!string.IsNullOrWhiteSpace(info))
          Debug.WriteLine($"GL.CompileShader had info log: {info}");
      }
      else
      {
        GL.DeleteShader(tessc);
        tessc = -1;
      }

      if (!string.IsNullOrEmpty(PathToTesselationEval))
      {
        string sTessE = File.ReadAllText(PathToTesselationEval);
        tesse = GL.CreateShader(ShaderType.TessEvaluationShader);
        GL.ShaderSource(tesse, sTessE);
        GL.CompileShader(tesse);
        info = GL.GetShaderInfoLog(fragmentShader);
        if (!string.IsNullOrWhiteSpace(info))
          Debug.WriteLine($"GL.CompileShader had info log: {info}");
      }
      else
      {
        GL.DeleteShader(tesse);
        tesse = -1;
      }

      GL.AttachShader(ProgramID, tessc);
      GL.AttachShader(ProgramID, tesse);
      GL.AttachShader(ProgramID, vertexShader);
      GL.AttachShader(ProgramID, fragmentShader);
      GL.LinkProgram(ProgramID);

      info = GL.GetProgramInfoLog(ProgramID);
      if (!string.IsNullOrWhiteSpace(info))
      {
        Debug.WriteLine(info);
        Console.Error.Write(info);
      }



      GL.DetachShader(ProgramID, vertexShader);
      GL.DetachShader(ProgramID, fragmentShader);
      //GL.DeleteShader(vertexShader);
      //GL.DeleteShader(fragmentShader);
      return ProgramID;
    }

    public void Bind()
    {
      GL.UseProgram(ProgramID);

      int t1Location = GL.GetUniformLocation(ProgramID, "texturemap1");
      int t2Location = GL.GetUniformLocation(ProgramID, "texturemap2");

      GL.Uniform1(t1Location, 0);
      GL.Uniform1(t2Location, 1);
      //glUniform1i(t1Location, 0);
      //glUniform1i(t2Location, 1);

      //SetInt("texturemap1", t1Location);
      //SetInt("texturemap2", t2Location);
      //SetInt("shadowMap", 1);
    }

    public void SetInt(string name, int value)
    {
      int location = GL.GetUniformLocation(ProgramID, name);
      GL.Uniform1(location, value);
    }

    public void SetFloat(string name, float value)
    {
      int location = GL.GetUniformLocation(ProgramID, name);
      GL.Uniform1(location, value);
    }

    public void SetVec3(string name, Vector3 v)
    {
      int location = GL.GetUniformLocation(ProgramID, name);
      GL.Uniform3(location, ref v);
    }

    public void SetMat4(string sPropName, Matrix4 d)
    {
      int vertexColorLocation = GL.GetUniformLocation(ProgramID, sPropName);
      GL.UniformMatrix4(vertexColorLocation, false, ref d);
    }

    public void Dispose()
    {
      GL.DeleteProgram(ProgramID);
    }

  }

}