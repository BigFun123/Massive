using Massive;
using Massive.Platform;
using Massive.Tools;
using OpenWorld.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class ImageUploader : DToolForm
  {
    public string sLocalFile;
    public string sPublicFile;
    public string sTargetID;

    public ImageUploader()
    {
      InitializeComponent();
      SetTitle("Image Uploader");
    }

    void Status(string s)
    {
      StatusText.Text = s;
    }

    private void SelectFileButton_Click(object sender, EventArgs e)
    {
      openFileDialog1.Filter = "Image Files (*.jpg,*.png)|*.jpg;*.png";
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        sLocalFile = openFileDialog1.FileName;
        pictureBox1.ImageLocation = sLocalFile;
      }
    }

    private void UploadButton_Click(object sender, EventArgs e)
    {      
      ImageUploadService.UploadImage(sLocalFile,
        Client_UploadFileCompleted, Client_UploadProgressChanged, HDBox.Checked);     
    }    

    private void Client_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
    {
      if (e.Error != null)
      {
        Globals.Log(this, e.Error.Message);
        Status(e.Error.Message);
        Console.WriteLine(e.Error.Message);
        return;
      }
      string s = System.Text.Encoding.UTF8.GetString(e.Result, 0, e.Result.Length);
      //Console.WriteLine(s);
      Status(s);
      sPublicFile = s.Replace("{HOST}", Globals.Network.ServerIP);
      DialogResult = DialogResult.OK;
      Close();
    }

    private void Client_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
    {
      float progress = ((float)e.BytesSent / (float)e.TotalBytesToSend) * 100.0f;
      UploadProgressBar.Value = (int)progress;
      string s = string.Format("Progress: {0:F2}%", progress);
      //Console.WriteLine(s);
      Status(s);
    }

    private void CloseButton_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
