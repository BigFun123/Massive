using Massive.Events;
using Massive.Platform;
using Massive.Tools;
using Newtonsoft.Json;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Controllers
{
  public class MBookmark
  {
    public string Name;
    public double[] Position;
    public double[] Rotation;
  }

  public class BookmarkController
  {
    public static string BookmarkFile = "bookmarks.json";
    static List<MBookmark> Bookmarks = new List<MBookmark>();

    public BookmarkController()
    {
      MMessageBus.BookmardAddRequestEvent += MMessageBus_BookmardAddRequestEvent;
      Load();
    }

    public static List<MBookmark> GetBookmarks()
    {
      return Bookmarks;
    }

    public static void Delete(MBookmark b)
    {
      Bookmarks.Remove(b);
      Save();
    }

    public static void Rename(MBookmark b, string sName)
    {
      b.Name = sName;
      Save();
    }

    private void MMessageBus_BookmardAddRequestEvent(object sender, BookmarkEvent e)
    {
      MBookmark b = new MBookmark();
      b.Name = e.Name;
      b.Position = MassiveTools.ArrayFromVector(e.Position);
      b.Rotation = MassiveTools.ArrayFromQuaterniond(e.Rotation);
      Bookmarks.Add(b);
      Save();
      MMessageBus.BookmarkAdded(this, e);
    }

    static void Save()
    {
      string sPath = Path.Combine(MFileSystem.AppDataPath, BookmarkFile);
      string sData = JsonConvert.SerializeObject(Bookmarks);
      if (!string.IsNullOrEmpty(sData))
      {
        File.WriteAllText(sPath, sData);
      }
    }

    void Load()
    {
      string sPath = Path.Combine(MFileSystem.AppDataPath, BookmarkFile);
      if (!File.Exists(sPath)) return;
      string sData = File.ReadAllText(sPath);
      Bookmarks = JsonConvert.DeserializeObject<List<MBookmark>>(sData);
    }
  }


}
