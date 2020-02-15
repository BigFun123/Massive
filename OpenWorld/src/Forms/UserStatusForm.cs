using Massive;
using Massive.Events;
using OpenWorld.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class UserStatusForm : DToolForm
  {
    string InstanceID;
    MSceneObject Target;
    public string Description;
    public string sLocalFile;
    public string sPublicFile;
    Color BGColor = Color.White;

    public UserStatusForm()
    {
      InitializeComponent();
      SetTitle("Status Update");

      MMessageBus.TextureChangedHandler += MMessageBus_TextureChangedHandler;
    }

    private void MMessageBus_TextureChangedHandler(object sender, TextureRequestEvent e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    void UpdateData()
    {
      SetTitle("Info");
      if (Target.Tag != null)
      {
        string sTag = (string)Target.Tag;
        if (string.IsNullOrEmpty(sTag)) return;
        string[] Items = sTag.Split('|');

        if (Items.Length > 1)
        {
          Description = Items[1];
          UserStatusText.Text = Description;
        }
      }
    }

    void UpdateImage()
    {
      MTexture mt = (MTexture)Target.FindModuleByType(MObject.EType.Texture);
      if (mt == null) return;

      if (File.Exists(mt.CacheFilename))
      {
        pictureBox1.ImageLocation = mt.CacheFilename;
      }
      else
      if (File.Exists(mt.Filename))
      {
        pictureBox1.ImageLocation = mt.Filename;
      }
      else
      {
        //URL?
        //pictureBox1.ImageLocation = mt.Filename;
      }
    }

    public void Setup(MObject o, bool ReadOnly = false)
    {
      progressBar1.Visible = false;
      MSceneObject mo = (MSceneObject)o;
      if (mo == null) return;
      Target = mo;
      InstanceID = Target.InstanceID;
      UpdateImage();
      UpdateData();

      if (ReadOnly == true)
      {
        UpdateButton.Visible = false;
        UserStatusText.ReadOnly = true;
      }
    }
    string CreateBitmap(string sText)
    {
      Bitmap b = new Bitmap(512, 512);
      Graphics g = Graphics.FromImage(b);

      Font font = new Font("Arial", 22);
      Font fontb = new Font("Arial", 18, FontStyle.Bold);
      Brush brush = new SolidBrush(Color.Black);
      Brush whitebrush = new SolidBrush(BGColor);
      Pen blackpen = new Pen(Color.Black, 2);

      g.FillRectangle(whitebrush, new RectangleF(0, 0, b.Width, b.Height));

      g.DrawString(Globals.UserAccount.UserName, fontb, brush, new Rectangle(5, 5, 510, 510));
      string sDate = DateTime.Now.ToString();
      g.DrawString(sDate, font, brush, new Rectangle(5, 38, 510, 510));
      g.DrawLine(blackpen, 0, 70, b.Width, 70);

      g.DrawString(sText, font, brush, new Rectangle(5, 74, 510, 510));

      g.Dispose();
      string sTempFile = "UserData\\temp.png";
      if ( !Directory.Exists("UserData"))
      {
        Directory.CreateDirectory("UserData");
      }
      b.Save(sTempFile, ImageFormat.Png);
      return sTempFile;
    }

    private void Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
    {
      progressBar1.Visible = false;
      if (e.Error != null)
      {
        Globals.Log(this, e.Error.Message);
        Status(e.Error.Message);
        Console.WriteLine(e.Error.Message);
        return;
      }
      string s = System.Text.Encoding.UTF8.GetString(e.Result, 0, e.Result.Length);
      //Console.WriteLine(s);
      Status("Upload Complete");
      sPublicFile = s.Replace("{HOST}", Globals.Network.ServerIP);
      Globals.Network.ChangePropertyRequest(Target.InstanceID, Description);
      MMessageBus.ChangeTextureRequest(this, Target.InstanceID, sPublicFile);     
    }

    private void Client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
    {
      float progress = ((float)e.BytesSent / (float)e.TotalBytesToSend) * 100.0f;
      progressBar1.Value = (int)progress;      
      string s = string.Format("Progress: {0:F2}%", progress);
      //Console.WriteLine(s);
      Status(s);
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
      sLocalFile = CreateBitmap(UserStatusText.Text);
      Description = "STATUS|"+UserStatusText.Text;
      progressBar1.Visible = true;
      ImageUploadService.UploadImage(sLocalFile,
        Client_UploadFileCompleted, Client_UploadProgressChanged, false);
    }

    void Status(string s)
    {
      UploadStatusLabel.Text = s;
    }

    private void WhiteButton_CheckedChanged(object sender, EventArgs e)
    {
      RadioButton b = (RadioButton)sender;
      BGColor = b.BackColor;
      UserStatusText.BackColor = b.BackColor;
    }
  }

  
}
