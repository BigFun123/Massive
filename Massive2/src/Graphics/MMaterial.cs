using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MMaterial : MObject
  {
    public const string DEFAULT_MATERIAL = "MATERIAL01";

    public bool IsUsed = false;
    public MShader shader { get; set; }
    public string MaterialID;

    public MTexture DiffuseTexture { get; set; }
    public MTexture MultiTexture { get; set; }
    public MTexture NormalMap { get; set; }

    private double ambient = 1;
    public double Ambient { get => ambient; set => ambient = value; }

    private double opacity = 1;
    public double Opacity { get => opacity; set => opacity = value; }

    public double Shininess = 32f;

    public Vector2 TexCoordScale = new Vector2(1, 1);
    public Vector2 Tex2CoordScale = new Vector2(3, 3);

    public bool UseMultitexture = false;
    public bool UseNormalMap = false;

    public int IsSky = 0;
    public bool Dirty = true;

    public int pEditor = 1;

    public MMaterial(string inname) : base(EType.Material, inname)
    {
    }

    public void Bind()
    {
      if (shader != null)
      {
        shader.Bind();
        if (Dirty == true)
        {
          shader.SetInt("isSky", IsSky);          
          shader.SetInt("Editor", Globals.Editor);
          pEditor = Globals.Editor;
          shader.SetBool("UseMultitexture", UseMultitexture);
          shader.SetBool("UseNormalMap", UseNormalMap);
          shader.SetFloat("material.shininess", (float)Shininess);
          shader.SetVec2("TexCoordScale", TexCoordScale);
          shader.SetVec2("Tex2CoordScale", Tex2CoordScale);
          shader.SetFloat("Opacity", (float)Opacity);
          shader.SetFloat("Ambient", (float)Ambient);          
          Dirty = false;
        }
        shader.SetFloat("iTime", (float)Time.TotalTime * 0.001f);
      }

      if (DiffuseTexture == null)
      {
        //GL.BindTexture(TextureTarget.Texture2D, 0);
      }

      if (DiffuseTexture != null)
      {
        DiffuseTexture.Bind();
      }
      if (MultiTexture != null)
      {
        MultiTexture.Bind();
      }
      if (NormalMap != null)
      {
        NormalMap.Bind();
      }
    }


    public void UnBind()
    {
      if (DiffuseTexture != null)
      {
        DiffuseTexture.UnBind();
      }
      if (MultiTexture != null)
      {
        MultiTexture.UnBind();
      }
      if (NormalMap != null)
      {
        NormalMap.UnBind();
      }
    }
    public void AddShader(MShader m)
    {
      Add(m);
      shader = m;
    }

    public void SetDiffuseTexture(MTexture t)
    {
      Add(t);
      DiffuseTexture = t;
      t._TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit.Texture0;
    }

    public void SetMultiTexture(MTexture t)
    {
      UseMultitexture = true;
      Add(t);
      MultiTexture = t;
      t._TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit.Texture0 + MShader.LOCATION_MULTITEX;
    }

    public void SetNormalMap(MTexture t)
    {
      UseNormalMap = true;
      Add(t);
      NormalMap = t;
      t._TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit.Texture0 + MShader.LOCATION_NORMALMAP;
    }

    public void ReplaceTexture(MTexture t)
    {
      Modules.Remove(DiffuseTexture);
      SetDiffuseTexture(t);
    }
  }
}
