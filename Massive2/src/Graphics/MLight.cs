using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

using Massive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Massive.Tools;

/// <summary>
/// A directional light that casts shadows
/// </summary>
/// 
namespace Massive
{
  public class MLight : MSceneObject
  {
    MSceneObject LightSphere;
    public Vector3d TargetVector;
    public Vector3 LightDirection;
    int depthMapFBO;
    public int depthMap;
    private float nearPlane = 10.5f;
    private float farPlane = 50.5f;
    public float NearPlane { get => nearPlane; set => nearPlane = value; }
    public float FarPlane { get => farPlane; set => farPlane = value; }

    private bool orthographic = false;
    public bool Orthographic { get => orthographic; set => orthographic = value; }

    private float fOV = 64;
    public float FOV { get => fOV; set => fOV = value; }

    Matrix4d lightProjection, lightView, lightSpaceMatrix;

    private Vector3 ambient = new Vector3(0.35f, 0.35f, 0.33f);
    public Vector3 Ambient { get => ambient; set => ambient = value; }

    private Vector3 diffuse = new Vector3(0.35f, 0.35f, 0.33f);
    public Vector3 Diffuse { get => diffuse; set => diffuse = value; }

    private Vector3 specular = new Vector3(0.7f, 0.7f, 0.7f);
    public Vector3 Specular { get => specular; set => specular = value; }

    public bool Shadows = true;

    private Color4 color = Color4.White;
    public Color4 Color { get => color; set => color = value; }

    private Vector2 depthMapSize = new Vector2(2048, 2048);
    public Vector2 DepthMapSize

    {
      get => depthMapSize; set
      {
        depthMapSize = value;
        Setup();
      }
    }
    
    public MLight(string inname) : base(EType.DirectionalLight, inname)
    {
      transform.Position = new Vector3d(10, 10, 12);
      LookAt(new Vector3d(0, 0, 0));
    }

    public void Bind(MMaterial mat)
    {
      mat.shader.SetBool("ShadowEnabled", Shadows);
      
      mat.shader.SetVec3("dirLight.direction", LightDirection);
      mat.shader.SetVec3("dirLight.specular", Specular);
      mat.shader.SetVec3("dirLight.ambient", Ambient);
      mat.shader.SetVec3("dirLight.diffuse", Diffuse);
      mat.shader.SetVec3("lightColor", new Vector3(Color.R, Color.G, Color.B));      
      Vector3d deltaLight = transform.Position - Globals.GlobalOffset;
      mat.shader.SetVec3("lightPos", new Vector3((float)deltaLight.X, (float)deltaLight.Y, (float)deltaLight.Z));
      mat.shader.SetMat4("lightSpaceMatrix", MTransform.GetFloatMatrix(GetLightSpaceMatrix()));
    }

    new public void LookAt(Vector3d target)
    {
      TargetVector = target;
      LightDirection = MassiveTools.Vector3FromVector3d((this.transform.Position - TargetVector).Normalized());
    }
    
    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (Shadows == true)
      {
        GL.Viewport(0, 0, (int)DepthMapSize.X, (int)DepthMapSize.Y);
        GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthMapFBO);        
        //clear the 2nd depth buffer        
        GL.Clear(ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.DepthTest);
      }

      
      if (LightSphere != null)
      {
        LightSphere.transform.Position = transform.Position + new Vector3d(0, -5, 0) ;
        base.Render(viewproj, parentmodel);
      }
    }

    public Matrix4d GetLightSpaceMatrix()
    {
      if (Orthographic == true)
      {
        lightProjection = Matrix4d.CreateOrthographicOffCenter(-30.0, 30.0, -30.0, 30.0, NearPlane, FarPlane);
      }
      else
      {
        lightProjection = Matrix4d.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), 1, NearPlane, FarPlane);
      }
      Vector3d pos = transform.Position - Globals.GlobalOffset;      

      lightView = Matrix4d.LookAt(pos, TargetVector - Globals.GlobalOffset, Globals.LocalUpVector);
      transform.Rotation = lightView.ExtractRotation();
      lightSpaceMatrix = lightView * lightProjection;
      return lightSpaceMatrix;
    }

    public override void Setup()
    {
      if (depthMapFBO != 0)
      {
        GL.DeleteFramebuffer(depthMapFBO);
      }

      if (depthMap != 0)
      {
        GL.DeleteBuffer(depthMap);
      }

      GL.GenFramebuffers(1, out depthMapFBO);

      GL.GenTextures(1, out depthMap);
      GL.BindTexture(TextureTarget.Texture2D, depthMap);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToBorder);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToBorder);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent,
                 (int)DepthMapSize.X, (int)DepthMapSize.Y, 0, PixelFormat.DepthComponent, PixelType.Float, IntPtr.Zero);
      //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureCompareFunc, );

      float[] borderColor = { 1.0f, 0.0f, 0.0f, 1.0f };
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, borderColor);

      // attach depth texture as FBO's depth buffer
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, depthMapFBO);
      GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthMap, 0);
      //GL.DrawBuffer(DrawBufferMode.None);
      GL.ReadBuffer(ReadBufferMode.None);
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

      LightSphere = (MSceneObject)FindModuleByName("LightSphere");
      base.Setup();
    }

    public override void Dispose()
    {
      if (depthMapFBO != 0)
      {
        GL.DeleteFramebuffer(depthMapFBO);
      }

      if (depthMap != 0)
      {
        GL.DeleteBuffer(depthMap);
      }
    }
  }
}
