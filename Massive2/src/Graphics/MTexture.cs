#define TEX_FLOAT

using System;
using System.Threading;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.ComponentModel;
using System.Net;
using Massive.Tools;
using Massive.Events;
using System.Drawing.Imaging;

namespace Massive
{
  public class MTexture : MObject
  {
    public const string DEFAULT_TEXTURE = "DEFAULT_TEXTURE";


    int TextureID { get; set; }
    public string Filename { get; set; } //file to download from
    public string CacheFilename { get; set; } //will only load data from cached file
    public int Width { get; set; }
    public int Height { get; set; }
    public string texture_type;

    public bool Readable = false;
    protected float[] data = null;
    byte[] rgbValues = null;

    public bool NeedsDownload = false;
    public bool DoAssign = false;
    public bool DataIsReady = false;
    public int ErrorCount = 0;

    bool HasAlpha = false;
    public bool Additive = false;
    public static object TextureLocker = new object();

    public TextureUnit _TextureUnit;
    public TextureWrapMode _TextureWrapMode = TextureWrapMode.Repeat;
    public Vector2 TexCoordScale = new Vector2(1, 1);


    public static event EventHandler<ErrorEvent> TextureError;

    public MTexture(string inname) : base(EType.Texture, inname)
    {
      Helper.CheckGLError(this, "TestPoint 4a");
      _TextureUnit = TextureUnit.Texture0;
      Helper.CheckGLError(this, "TestPoint 4n");
    }

    public override void Dispose()
    {
      if (TextureID != 0)
      {
        GL.DeleteTexture(TextureID);
        TextureID = 0;
        data = null;
      }
      base.Dispose();
    }

    public int[] GetPixelInt(int x, int y)
    {
      if (data == null) return new int[] { 0, 0, 0, 1 };
      int ps = y * Width * 4 + x * 4;
      if (ps < 0) ps = 0;
      if (ps >= data.Length) return new int[] { 0, 0, 0, 1 };
      int[] res = {
        (int)(data[ps]*255), (int)(data[ps+1]*255),(int)(data[ps+2]*255),(int)(data[ps+3]*255) };
      return res;
    }

    public float[] GetPixel(int x, int y)
    {
      if (data == null) return new float[] { 0, 0, 0, 1 };
      int ps = y * Width * 4 + x * 4;
      if (ps < 0) ps = 0;
      if (ps >= data.Length) return new float[] { 0, 0, 0, 1 };
      float[] res = {
        data[ps], data[ps+1],data[ps+2],data[ps+3] };
      return res;
    }

    public void SetPixelInt(int x, int y, int[] val)
    {
      int ps = y * Width * 4 + x * 4;
      if (ps < 0) ps = 0;
      if (ps >= data.Length) return;
      data[ps] = val[0] / 255.0f;
      data[ps + 1] = val[1] / 255.0f;
      data[ps + 2] = val[2] / 255.0f;
      data[ps + 3] = 255;
      DoAssign = true;
    }

    public void SetPixel(int x, int y, float[] val)
    {      
      int ps = y * Width * 4 + x * 4;
      if (ps < 0) ps = 0;
      if (ps >= data.Length) return;
      data[ps] = val[0];
      data[ps + 1] = val[1];
      data[ps + 2] = val[2];
      data[ps + 3] = 1;
      DoAssign = true;
    }

    public void Bind()
    {
      GL.ActiveTexture(_TextureUnit);
      GL.BindTexture(TextureTarget.Texture2D, TextureID);

      if (Additive == true)
      {
        GL.BlendFunc(BlendingFactor.One, BlendingFactor.One);
      }
    }

    public void UnBind()
    {
      if (Additive == true)
      {
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
      }
    }

    public void SetFilename(string sPath)
    {
      Filename = sPath;
    }

    protected virtual bool IsFileLocked(string sFilename)
    {
      //if (!File.Exists(sFilename)) return false;

      FileInfo file = new FileInfo(sFilename);

      FileStream stream = null;

      try
      {
        stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
      }
      catch (IOException)
      {
        //the file is unavailable because it is:
        //still being written to
        //or being processed by another thread
        //or does not exist (has already been processed)
        return true;
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }

      //file is not locked
      return false;
    }

    public void AfterLoad()
    {
      DoAssign = false;
      if (data == null) return;
      //Console.WriteLine(" tex:" + Filename + " W:" + Width + " H:" + Height);
      //Globals.Log(null, "AfterLoadTextureData ");
      if (TextureID != 0)
      {
        GL.DeleteTexture(TextureID);
      }
      int TempID = 0;
      GL.CreateTextures(TextureTarget.Texture2D, 1, out TempID);

      TextureID = TempID;
      Helper.CheckGLError(this);

      // texture units for multi texture
      GL.ActiveTexture(_TextureUnit); // activate the texture unit first before binding texture      
      GL.BindTexture(TextureTarget.Texture2D, TextureID);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)_TextureWrapMode);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)_TextureWrapMode);

      GCHandle pp_pixels = GCHandle.Alloc(data, GCHandleType.Pinned);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height,
        0, OpenTK.Graphics.OpenGL4.PixelFormat.Rgba, PixelType.Float, pp_pixels.AddrOfPinnedObject());
      
      GL.TextureStorage2D(
         TextureID,
         3,                           // levels of mipmapping
         SizedInternalFormat.Rgba32f, // format of texture
         Width,
         Height);

      GL.TextureSubImage2D(TextureID,
         0,                  // this is level 0
         0,                  // x offset
         0,                  // y offset
         Width,
         Height,
         OpenTK.Graphics.OpenGL4.PixelFormat.Rgba,
         PixelType.Float,
         data);      
      GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
      GL.BindTexture(TextureTarget.Texture2D, 0);

      if (Readable == false) {
        data = null;
      }
      

    }



    /**
     * Called Async via TexturePool
     * For best performance do not call directly 
     * */
    public void LoadTextureData(string filename)
    {
      if (DataIsReady == true) return;
      DataIsReady = false;
      // Globals.Log(null, "LoadTextureData " + filename);
      // Console.WriteLine(InstanceID + " LoadTextureData " + filename);

      //float[] r;
      if (filename == null)
      {
        data = new float[1];
        Width = 0;
        Height = 0;
        return;
      }

      if (!File.Exists(filename))
      {
        if (TextureError != null)
        {
          TextureError(null, new ErrorEvent("TEXTURE NOT FOUND:" + filename));
        }
        Console.WriteLine("ERROR: TEXTURE NOT FOUND:" + filename);
        Width = 0;
        Height = 0;
        ErrorCount++;
        Error = "File not found " + filename;
        return;
      }

      FileInfo fi = new FileInfo(filename);
      if (fi.Length == 0)
      {
        Console.WriteLine("ERROR: TEXTURE IS CORRUPT:" + filename);
        Width = 0;
        Height = 0;
        ErrorCount++;
        Error = "Texture corrupt " + filename + " ... attempting delete...";
        File.Delete(fi.FullName);
        return;
      }

      using (Bitmap bmp = (Bitmap)Image.FromFile(filename))
      {
        //Console.WriteLine(InstanceID + " LoadTextureData " + filename);
        //bmp.MakeTransparent();
        BitmapData bd = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadOnly, bmp.PixelFormat);
        Width = bmp.Width;
        Height = bmp.Height;

        if (bd.Width == 0)
        {
          return;
        }

        int bpp = bd.Stride / bmp.Width;
        if (bpp == 1)
        {
          return; //monochrome
        }

        data = new float[Width * Height * 4];

        IntPtr ptr = bd.Scan0;

        int bytes = Math.Abs(bd.Stride) * bmp.Height;
        rgbValues = new byte[bytes];
        Marshal.Copy(ptr, rgbValues, 0, bytes);
        if (rgbValues == null) return;

        int index = 0;
        for (int y = 0; y < Height; y++)
        {
          for (int x = 0; x < Width; x++)
          {
            //var pixel = bmp.GetPixel(x, y);
            int pos = y * bd.Stride + x * bpp;
            //r[index++] = (float)pixel.R / 255f;
            //r[index++] = (float)pixel.G / 255f;
            //r[index++] = (float)pixel.B / 255f;
            //r[index++] = 1;

            data[index++] = (float)(rgbValues[pos + 2] / 255f);
            data[index++] = (float)(rgbValues[pos + 1] / 255f);
            data[index++] = (float)(rgbValues[pos] / 255f);
            if (bpp == 4) data[index++] = (float)rgbValues[pos + 3] / 255f;
            else data[index++] = 1f;
            //r[index++] = 1f;

#if TEX_FLOAT
            //r[index++] = (float)pixel.R / 255f;
            //r[index++] = (float)pixel.G / 255f;
            //r[index++] = (float)pixel.B / 255f;
            //r[index++] = (float)pixel.A / 255f;
#else
            r[index++] = pixel.R;
            r[index++] = pixel.G;
            r[index++] = pixel.B;
            r[index++] = pixel.A;
#endif

          }
        }
        bmp.UnlockBits(bd);
      }
      
      rgbValues = null;
      DataIsReady = true;
    }

    public override void Render(Matrix4d a, Matrix4d b)
    {
      if (DoAssign == true)
      {
        AfterLoad();
        DoAssign = false;
      }
    }

    public void LoadTexturFromImage(Image img)
    {
      DataIsReady = false;
      // int width=0, height=0;

      var bmp = (Bitmap)img;
      // using (var bmp = (Bitmap)img) //don't use using becase it disposes the image
      {
        Width = img.Width;
        Height = img.Height;
        if (data == null)
        {
          data = new float[Width * Height * 4];
        }
        int index = 0;
        for (int y = 0; y < Height; y++)
        {
          for (int x = 0; x < Width; x++)
          {
            var pixel = bmp.GetPixel(x, y);
#if TEX_FLOAT
            data[index++] = (float)pixel.R / 255f;
            data[index++] = (float)pixel.G / 255f;
            data[index++] = (float)pixel.B / 255f;
            data[index++] = (float)pixel.A / 255f;
#else
            data[index++] = pixel.R;
            data[index++] = pixel.G;
            data[index++] = pixel.B;
            data[index++] = pixel.A;
#endif
          }
        }
      }
      DataIsReady = true;
      AfterLoad();
    }
  }
}
