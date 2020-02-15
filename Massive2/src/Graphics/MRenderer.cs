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
  public class MRenderer : MObject
  {
    uint quadVAO, quadVBO;
    public Color4 ClearColor = Color4.Red;

    int SceneFBO = 0;
    int SceneTex = 0;
    int RenderBO = 0;
    Vector2 BufferSize = new Vector2(1024, 1024);
    PolygonMode _renderMode = PolygonMode.Fill;
    //int ViewportX = 800, ViewportY = 600;

    float[] quadVertices = { // vertex attributes for a quad that fills the entire screen in Normalized Device Coordinates.
        // positions   // texCoords
        -1.0f,  1.0f,  0.0f, 1.0f,
        -1.0f, -1.0f,  0.0f, 0.0f,
         1.0f, -1.0f,  1.0f, 0.0f,

        -1.0f,  1.0f,  0.0f, 1.0f,
         1.0f, -1.0f,  1.0f, 0.0f,
         1.0f,  1.0f,  1.0f, 1.0f
    };

    string vs = @"#version 330 core
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec2 aTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = aTexCoords;
    gl_Position = vec4(aPos.x, aPos.y, 0.0, 1.0); 
}   ";

    string fs = @"#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D screenTexture;

void main()
{
    vec3 col = texture(screenTexture, TexCoords).rgb;
    FragColor = vec4(col, 1.0);
}";

    MShader FXShader;

    public MRenderer() : base(EType.Other, "Renderer")
    {
      FXShader = new MShader("FX");
      try { 
        FXShader.LoadFromString(vs, fs);
      }
      catch ( Exception e)
      {
        Console.WriteLine("MRenderer: " + e.Message + " . OPENGL Setup?");
      }
      BufferSize = new Vector2(MScreen.Width, MScreen.Height);
    }

    new public void Debug()
    {
#if DEBUG
      Helper.CheckGLError(this);
#endif
      //Debugger.Break();
    }

    public void Resize(int w, int h)
    {
      BufferSize = new Vector2(MScreen.Width, MScreen.Height);

      if ( SceneFBO != 0)
      {
        GL.DeleteFramebuffer(SceneFBO);
        SceneFBO = 0;
      }
      GL.GenFramebuffers(1, out SceneFBO);
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, SceneFBO);

      if ( SceneTex != 0)
      {
        GL.DeleteTexture(SceneTex);
        SceneTex = 0;
      }

      GL.GenTextures(1, out SceneTex);
      GL.BindTexture(TextureTarget.Texture2D, SceneTex);

      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, MScreen.Width, MScreen.Height, 0, PixelFormat.Rgba, PixelType.Float, IntPtr.Zero);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
      GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, SceneTex, 0);
      
      if (RenderBO != 0)
      {
        GL.DeleteRenderbuffer(RenderBO);
        RenderBO = 0;
      }
      GL.GenRenderbuffers(1, out RenderBO);
      GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, RenderBO);
      GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.Depth32fStencil8, (int)BufferSize.X, (int)BufferSize.Y);
      GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
      GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, RenderBO);

      if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
      {
        Console.WriteLine("Framebuffer not complete");
      }
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
    }

    public override void Setup()
    {
     
      FXShader.LoadFromString(vs, fs);
      FXShader.SetInt("screenTexture", 0);

      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);    
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      //GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
      Helper.CheckGLError(this);
    


      //screen quad
      // screen quad VAO to render on
      GL.GenVertexArrays(1, out quadVAO);
      GL.GenBuffers(1, out quadVBO);
      GL.BindVertexArray(quadVAO);
      GL.BindBuffer(BufferTarget.ArrayBuffer, quadVBO);
      GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float)*quadVertices.Length, quadVertices, BufferUsageHint.StaticDraw);
      GL.EnableVertexAttribArray(0);
      GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
      GL.EnableVertexAttribArray(1);
      GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), (2 * sizeof(float)));
      //return;

      GL.ClearColor(ClearColor);
      //   GL.DepthFunc(DepthFunction.Greater);
      GL.Enable(EnableCap.DepthTest);
      GL.DepthFunc(DepthFunction.Less);

      //  Helper.CheckGLError(this);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
      GL.ActiveTexture(TextureUnit.Texture0);

      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

      //gamma correction
      //GL.Enable(EnableCap.FramebufferSrgb);

      GL.Enable(EnableCap.Multisample);
      GL.PatchParameter(PatchParameterInt.PatchVertices, 3);
      float[] innerLevel = new float[] { 5 };
      float[] outerLevel = new float[] { 3, 3, 3 };
      GL.PatchParameter(PatchParameterFloat.PatchDefaultOuterLevel, outerLevel);
      GL.PatchParameter(PatchParameterFloat.PatchDefaultInnerLevel, innerLevel);
      
      // GL.PatchParameter(PatchParameterInt.PatchVertices, 3);

      //setup fbos

      Resize(MScreen.Width, MScreen.Height);
      
    }

    public void Bind()
    {
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, SceneFBO);
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {      
      Bind();
      GL.PolygonMode(MaterialFace.Front, _renderMode);
      GL.Enable(EnableCap.CullFace);
      GL.CullFace(CullFaceMode.Back);
      GL.Viewport(0, 0, MScreen.Width, MScreen.Height);
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
      GL.Enable(EnableCap.DepthTest);            
    }

    public void AfterRender()
    {
      GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
      GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      GL.Disable(EnableCap.DepthTest);
      GL.ClearColor(ClearColor);      

      FXShader.Bind();
      GL.BindVertexArray(quadVAO);
      GL.BindTexture(TextureTarget.Texture2D, SceneTex); // use the color attachment texture as the texture of the quad plane
      GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
    }



    public override void Dispose()
    {
      GL.DeleteVertexArray(quadVAO);
      GL.DeleteBuffer(quadVBO);

      if (SceneFBO != 0)
      {
        GL.DeleteFramebuffer(SceneFBO);
      }

      if (SceneTex != 0)
      {
        GL.DeleteBuffer(SceneTex);
      }
    }
  }
}
