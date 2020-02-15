using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShaderPlay
{
  public class MTexture
  {
    int Texture1ID = -1;
    int Texture2ID = -1;
    public TextureWrapMode _TextureWrapMode = TextureWrapMode.Repeat;
    string TextureFile1;
    string TextureFile2;

    public MTexture()
    {    
    }

    public static float[] LoadTextureData(string filename, out int width, out int height)
    {
      float[] r;
      using (var bmp = (Bitmap)Image.FromFile(filename))
      {
        width = bmp.Width;
        height = bmp.Height;
        r = new float[width * height * 4];
        int index = 0;
        for (int y = 0; y < height; y++)
        {
          for (int x = 0; x < width; x++)
          {
            var pixel = bmp.GetPixel(x, y);
            r[index++] = pixel.R / 255f;
            r[index++] = pixel.G / 255f;
            r[index++] = pixel.B / 255f;
            r[index++] = pixel.A / 255f;
          }
        }
      }
      return r;
    }

    public void Setup(string path1, string path2)
    {
      TextureFile1 = path1;
      TextureFile2 = path2;

      if ( Texture1ID != -1)
      {
        GL.DeleteTexture(Texture1ID);
      }

      GL.GenTextures(1, out Texture1ID);
      int width, height;
      var data1 = LoadTextureData(path1, out width, out height);
      GL.BindTexture(TextureTarget.Texture2D, Texture1ID);
      GCHandle pp_pixels = GCHandle.Alloc(data1, GCHandleType.Pinned);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.Float, pp_pixels.AddrOfPinnedObject());
      GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)OpenTK.Graphics.OpenGL4.TextureWrapMode.ClampToEdge);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)OpenTK.Graphics.OpenGL4.TextureWrapMode.ClampToEdge);


      if (Texture2ID != -1)
      {
        GL.DeleteTexture(Texture2ID);
      }

      GL.GenTextures(1, out Texture2ID);      
      var data2 = LoadTextureData(path2, out width, out height);
      GL.BindTexture(TextureTarget.Texture2D, Texture2ID);
      GCHandle pp_pixels2 = GCHandle.Alloc(data2, GCHandleType.Pinned);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.Float, pp_pixels2.AddrOfPinnedObject());
      GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)OpenTK.Graphics.OpenGL4.TextureWrapMode.ClampToEdge);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)OpenTK.Graphics.OpenGL4.TextureWrapMode.ClampToEdge);


      float[] borderColor = { 1.0f, 0.0f, 0.0f, 1.0f };
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, borderColor);

      //GL.TexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, format == GL_RGBA? GL_CLAMP_TO_EDGE : GL_REPEAT); // for this tutorial: use GL_CLAMP_TO_EDGE to prevent semi-transparent borders. Due to interpolation it takes texels from next repeat 
      //      GL.TexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, format == GL_RGBA? GL_CLAMP_TO_EDGE : GL_REPEAT);
      //GL.TexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
      //GL.TexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
    }

    public void SetTexture1(string sFile)
    {
      TextureFile1 = sFile;
      Setup(TextureFile1, TextureFile2);
    }

    public void SetTexture2(string sFile)
    {
      TextureFile2 = sFile;
      Setup(TextureFile1, TextureFile2);
    }

    public void Bind()
    {
      GL.ActiveTexture(TextureUnit.Texture0);
      GL.BindTexture(TextureTarget.Texture2D, Texture1ID);
      GL.ActiveTexture(TextureUnit.Texture1);
      GL.BindTexture(TextureTarget.Texture2D, Texture2ID);

      //if (Additive == true)
      //{
      //GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
      //}
    }
  }
}
