using Massive;
using Massive.Platform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Services
{
  public class ImageUploadService
  {
    public static void UploadImage(string sLocalFile,
     UploadFileCompletedEventHandler Client_UploadFileCompleted, 
     UploadProgressChangedEventHandler Client_UploadProgressChanged, 
     bool HD = false)
    {
      if (string.IsNullOrEmpty(sLocalFile))
      {
        Console.WriteLine("ImageUploadService: File empty");
        return;
      }
      //string sLocalCache = Path.Combine(Globals.AppDataPath, Helper.GUID() + ".jpg");
      System.Net.WebClient Client = new System.Net.WebClient();
      // Client.Credentials = CredentialCache.DefaultCredentials;
     
      Image bmp = Bitmap.FromFile(sLocalFile);

      int size = 800;
      if (HD == true) size = 1024;

      Image bmp2 = ImageTools.ScaleImageProportional(bmp, size, size);

      FileInfo fi = new FileInfo(sLocalFile);
      string extension = fi.Extension;

      string tempname = Helper.HashString(DateTime.Now.ToString()) + extension;
      string TempName = Path.Combine(MFileSystem.AppDataPath, tempname);

      if (File.Exists(TempName))
      {
        File.Delete(TempName);
      }

      bmp2.Save(TempName);

      bmp.Dispose();
      bmp2.Dispose();

      Client.Headers.Add("Content-Type", "binary/octet-stream");
      Client.Headers.Add("UserID:" + Globals.UserAccount.UserID);

      string CDNLocation = "";
      if ( !string.IsNullOrEmpty(Globals.Network.ServerDomain))
      {
        CDNLocation = Globals.Network.ServerDomain;
      }
      else
      {
        CDNLocation = Globals.Network.ServerIP;
      }
      
      Client.UploadFileAsync(new Uri("http://" + CDNLocation + "/massive/fu/fu.php"), "POST", TempName);
      Client.UploadFileCompleted += Client_UploadFileCompleted;
      Client.UploadProgressChanged += Client_UploadProgressChanged;
    }
  }
}
