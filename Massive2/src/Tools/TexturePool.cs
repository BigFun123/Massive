using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using Massive.Platform;
using Massive.Tools;

namespace Massive
{
  public class TexturePool : MObject
  {
    MTexture Current;
    static Dictionary<string, MTexture> Pool = new Dictionary<string, MTexture>();
    static List<MTexture> Downloads = new List<MTexture>();
    public static string TextureError = "";
    BackgroundWorker _worker;
    bool done = false;

    public TexturePool() : base(EType.Null, "TexturePool")
    {
      //StartWorker();
    }

    void StartWorker()
    {
      if (_worker != null)
      {
        return;
      }
      _worker = new BackgroundWorker();
      _worker.WorkerSupportsCancellation = true;
      _worker.DoWork += Bw_DoWork;
      _worker.RunWorkerAsync();
    }

    private void Bw_DoWork(object sender, DoWorkEventArgs e)
    {
      while ((Globals.ApplicationExiting == false) && (done == false))
      {
        if (Current == null)
        {
          if (Downloads.Count > 0)
          {
            Current = Downloads[0];
            if (Current != null)
            {

              if (MassiveTools.IsURL(Current.Filename))
              {
                if (File.Exists(Current.CacheFilename))
                {
                  //Console.WriteLine("Cache hit:" + Current.Filename + " -> " + Current.CacheFilename);
                }
                else
                {
                  Globals.Log(this, "Downloading:" + Current.Filename);
                  DownloadFromURL();
                }
                Current.LoadTextureData(Current.CacheFilename);
              }
              else
              {
                if (File.Exists(Current.Filename))
                {
                  Current.LoadTextureData(Current.Filename);
                }
                else
                {
                  string sAbsolute = Path.Combine(MFileSystem.AssetsPath, Current.Filename);
                  if (File.Exists(sAbsolute))
                  {
                    Current.LoadTextureData(sAbsolute);
                  }
                  else { Current.Error = "File not found " + Current.Filename; }
                }
              }

              Downloads.RemoveAt(0);
              if (Current.DataIsReady == true)
              {
                Current.DoAssign = true; //tell the texture to upload to the GPU on next GUI cycle
                Current = null;
              }
              else
              {
                //was there a problem? place at the end of the queue to try again                
                //Downloads.Add(Current);
                Current = null;
              }

              Thread.Sleep(30);
            }
          }
          else
          {
            done = true;
          }
        }
      }
      Current = null;
      _worker = null;
    }

    string CreateCacheFilename(string URL)
    {
      string extension = ".jpg";
      if (URL.EndsWith(".png")) extension = ".png";
      return Path.Combine(MFileSystem.CachePath, Helper.HashString(Globals.UserAccount.UserID + "_" + URL) + extension);
    }

    void DownloadFromURL()
    {
      if (Current == null) return;

      using (WebClient client = new WebClient())
      {
        Console.WriteLine(InstanceID + " Downloading : " + Current.Filename);
        client.DownloadFile(new Uri(Current.Filename), Current.CacheFilename);
      }
    }

    public MTexture GetTexture(string TextureFile)
    {
      TextureError = "";
      MTexture tex = null;

      if (Pool.ContainsKey(TextureFile))
      {
        tex = Pool[TextureFile];
      }
      else
      {
        done = false;
        tex = new MTexture("Texture");
        Pool.Add(TextureFile, tex);
        tex.Name = TextureFile;
        tex.Filename = TextureFile;
        tex.CacheFilename = CreateCacheFilename(tex.Filename);
        Downloads.Add(tex);
        StartWorker();
      }
      return tex;
    }

    public override void Dispose()
    {
      foreach (KeyValuePair<string, MTexture> kv in Pool)
      {
        //kv.Value.Di
        kv.Value.Dispose();
      }
      Pool.Clear();
      if (_worker != null)
      {
        _worker.CancelAsync();
        _worker = null;
      }
    }

  }
}
