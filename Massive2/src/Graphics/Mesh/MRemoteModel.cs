using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Massive.Platform;
using OpenTK;

namespace Massive
{
  public class MRemoteModel : MModel
  {
    static object locker = new object();
    string URL;
    string CacheFile;
    public event EventHandler<EventArgs> ReadyEvent;
    bool IsSaved = false;
    bool IsInitialized = false;


    public MRemoteModel() : base(EType.ModelURL, "RemoteModel")
    {
      Enabled = false;
      Visible = false;
    }

    public override void Update()
    {
      if (IsSaved == false) return;
      if ( IsInitialized == false )
      {        
        DoCreate();
      }
      base.Update();
    }

    public override void Render(Matrix4d viewproj, Matrix4d parentmodel)
    {
      if (IsSaved == false) return;
      base.Render(viewproj, parentmodel);
    }

    public override void Load(string sURL)
    {
      URL = sURL;
      string extension = ".3ds";
      if (sURL.EndsWith(".obj"))
      {
        extension = ".obj";
      }

      CacheFile = Path.Combine(MFileSystem.CachePath, Helper.HashString(Globals.UserAccount.UserID + "_" + URL) + extension);

      if (File.Exists(CacheFile))
      {
        //DoCreate();
        IsSaved = true;
        return;
      }

      using (WebClient client = new WebClient())
      {
        //Console.WriteLine(InstanceID + " Downloading : " + Filename);

        //client.DownloadProgressChanged += Client_DownloadProgressChanged;
        client.DownloadFile(new Uri(URL), CacheFile);
        FileInfo fi = new FileInfo(CacheFile);
        if (File.Exists(CacheFile))
        {
          while (IsFileLocked(fi))
          {
            Console.WriteLine("Waiting for lock " + CacheFile);
          }
        }
        
        IsSaved = true;
        //DoCreate();
      }

    }

    private void Bw_DoWork(object sender, DoWorkEventArgs e)
    {
     
    }

    void DoCreate()
    {
      CreateMesh(CacheFile);
      IsInitialized = true;
      Enabled = true;
      Visible = true;

      ReadyEvent?.Invoke(this, new EventArgs());
    }

    private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      Console.WriteLine("Downloading: " + e.ProgressPercentage);
    }

    private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      //Console.WriteLine("complete " + CacheFile);

      DoCreate();
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
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

    public override void Setup()
    {
      base.Setup();
    }
  }
}
