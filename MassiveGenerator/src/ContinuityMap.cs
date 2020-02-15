using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveGenerator.src
{
  public class ContinuityMap
  {
    int SizeInPixels;
    Image Center;
    Image Right;
    Image Bottom;
    Image BottomRight;
    string basepathtype;

    public ContinuityMap(string sBaseType, int in_SizeInPixels)
    {
      basepathtype = sBaseType;
      SizeInPixels = in_SizeInPixels;
    }

    public bool Combine(int TX, int TY, int Zoom, string Ext)
    {      
      Directory.CreateDirectory("data\\" + Zoom + "\\continuity");

      string sCenter = FilePath.GetPath(new vector3(TX, TY, Zoom), basepathtype);
      string sRight = FilePath.GetPath(new vector3(TX+1, TY, Zoom), basepathtype);
      string sBottom = FilePath.GetPath(new vector3(TX, TY+1, Zoom), basepathtype);
      string sBottomRight = FilePath.GetPath(new vector3(TX+1, TY + 1, Zoom), basepathtype);

      if (!File.Exists(sCenter + Ext)) return false;
      if (!File.Exists(sRight + Ext)) return false;
      if (!File.Exists(sBottom + Ext)) return false;
      if (!File.Exists(sBottomRight + Ext)) return false;

      Center = (Image)Bitmap.FromFile(sCenter + Ext);
      Right = (Image)Bitmap.FromFile(sRight + Ext);
      Bottom = (Image)Bitmap.FromFile(sBottom + Ext);
      BottomRight = (Image)Bitmap.FromFile(sBottomRight + Ext);

      Bitmap b = new Bitmap(SizeInPixels+1, SizeInPixels+1);
      Graphics g = Graphics.FromImage(b);
      g.Clear(Color.Red);
      g.DrawImage(Center, new Point(0, 0));
      g.DrawImage(Right, SizeInPixels, 0);
      g.DrawImage(Bottom, 0, SizeInPixels);
      g.DrawImage(BottomRight, SizeInPixels, SizeInPixels);

      string ContPath = FilePath.GetPath(new vector3(TX, TY, Zoom), "continuity");
      b.Save(ContPath + Ext);

      b.Dispose();
      Center.Dispose();
      Right.Dispose();
      Bottom.Dispose();
      BottomRight.Dispose();

      return true;
    }
  }
}

