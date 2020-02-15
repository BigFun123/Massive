using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive
{
  public class MText : MControl
  {
    public string Text = "123";
    public string OldText = "";
    Bitmap TextImage;
    System.Drawing.Graphics graphics;
   
    Font font = new Font("Arial", 24);
    Brush brush = new SolidBrush(Color.Black);

    //MMaterial material;
    public MText(string sName = "Text") : base(sName)
    {

    }

    public override void Dispose()
    {
      // TextImage.Dispose();
     // base.Dispose();
    }

    public override void Setup()
    {     
      material = new MMaterial("textmat");
      //MMaterial uimat = (MMaterial)MScene.MaterialRoot.FindModuleByName("DefaultGUIMaterial");
      AddMaterial(material);
      material.AddShader(Helper.GetGUIShader());

      //transform.Scale = new Vector3d(10, 10, 10);
      //transform.Position = new Vector3d (10, 10, 10);

      MTexture tex = new MTexture("Texture");
      material.SetDiffuseTexture(tex);

      TextImage = new Bitmap((int)(Width * 16), (int)(Height * 16), PixelFormat.Format32bppArgb);
      graphics = System.Drawing.Graphics.FromImage(TextImage); ;

      material.DiffuseTexture.Filename = "";

      SetText(Text);
      base.Setup();
      // transform.Scale = new Vector3d(1, 1, 1);

    }


    public void SetText(string sText)
    {
      Text = sText;      
     
      if (graphics != null)
      {
       // graphics.FillRectangle(new SolidBrush(Color.Blue), 0, 0, TextImage.Width, TextImage.Height);
        graphics.Clear(Color.Transparent);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        StringFormat format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;
        graphics.DrawString(Text, font, brush, new RectangleF(0, 0, (float)Width*16, (float)Height*16), format);
        graphics.Flush();
        material.DiffuseTexture.LoadTexturFromImage(TextImage);        
      }
    }
  }
}
