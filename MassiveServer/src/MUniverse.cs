using Massive.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 *  Manages a geo zone and sync
 *  TODO: Create zones dynamically using K-MEANS clustering to serve active areas
 */
namespace Massive.Server
{
  public class MUniverse
  {
    public string DataPath = "data";
    public string UniverseFile = @"universe_0_0_0.csv";

    public string UniverseID = "UNIVERSE1";

    public bool MakeBackups = false;
    //anything further will not be sent to the client
    public double DistanceThreshold = 30;

    //instanceid, spawnpacket
    Dictionary<string, MServerObject> Objects = new Dictionary<string, MServerObject>();

    public MUniverse()
    {
      ReadFromDisk();
    }

    public MServerObject GetObject(string InstanceID)
    {
      if (Objects.ContainsKey(InstanceID)) return Objects[InstanceID];
      return null;
    }

    public long GetCount()
    {
      return Objects.Count;
    }

    public override string ToString()
    {
      return UniverseID + "(" + Objects.Count + ")";
    }

    /**
     * Remove all inactive, non static-storage objects owned by OwnerID
     * */
    public void Flush()
    {
      foreach (var item in Objects.Where(kvp => (kvp.Value.StaticStorage == MServerObject.iDYNAMICSTORAGE)).ToList())
      {
        Objects.Remove(item.Key);
      }

      WriteToDisk();
    }

    //delete all dynamic objects with userid
    public void Cleanup(string UserID)
    {
      if (string.IsNullOrEmpty(UserID)) return;

      lock (Objects)
      {
        try
        {
          foreach (KeyValuePair<string, MServerObject> item in Objects.ToArray())
          {
            if (item.Value.OwnerID == null)
            {
              Console.WriteLine("Corrupt Entry Removed:" + item.Key);
              Objects.Remove(item.Key);
              continue; //corrupt entry 
            }
            if (item.Value.OwnerID.Equals(UserID))
            {
              if (item.Value.StaticStorage == MServerObject.iDYNAMICSTORAGE)
              {
                Objects.Remove(item.Key);
              }
            }
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }

      WriteToDisk();
    }

    //Add or update position of existing object
    public bool AddObject(MServerObject ms)
    {
      //MServerObject ms = new MServerObject();            
      if (!Objects.ContainsKey(ms.InstanceID))
      {
        Objects.Add(ms.InstanceID, ms);
        WriteToDisk();
        return true;
      }
      else
      {
        MServerObject mo = Objects[ms.InstanceID];
        mo.Position = ms.Position;
        mo.Rotation = ms.Rotation;
        WriteToDisk();
      }
      return false;
    }

    public bool MoveObject(string OwnerID, string InstanceID, double[] pos, double[] rot)
    {
      if (Objects.ContainsKey(InstanceID))
      {
        MServerObject m = Objects[InstanceID];
        if (m.OwnerID.Equals(OwnerID))
        {
          m.DateModified = DateTime.Now;
          m.Position = pos;
          m.Rotation = rot;
          // WriteToDisk();
          return true;
        }
      }
      return false;
    }

    public bool RotateObject(string UserID, string InstanceID, double[] rot)
    {
      if (Objects.ContainsKey(InstanceID))
      {
        MServerObject m = Objects[InstanceID];
        if (m.OwnerID.Equals(UserID))
        {
          m.DateModified = DateTime.Now;
          m.Rotation = rot;
          //WriteToDisk();
          return true;
        }
      }
      return false;

    }

    public bool SetTexture(string UserID, string InstanceID, string TextureID)
    {
      if (Objects.ContainsKey(InstanceID))
      {
        MServerObject m = Objects[InstanceID];
        if (m.OwnerID.Equals(UserID))
        {
          m.DateModified = DateTime.Now;
          m.TextureID = TextureID;
          WriteToDisk();
          return true;
        }
      }
      return false;
    }

    public bool SetProperty(string UserID, string InstanceID, string PropertyTag)
    {
      if (Objects.ContainsKey(InstanceID))
      {
        MServerObject m = Objects[InstanceID];
        if (m.OwnerID.Equals(UserID))
        {
          m.DateModified = DateTime.Now;
          m.Tag = PropertyTag;
          WriteToDisk();
          return true;
        }
      }
      return false;
    }

    //TODO : Replace with DB request
    public void WriteToDisk()
    {
      string sPath = Path.Combine(DataPath, UniverseFile);
      string s = JsonConvert.SerializeObject(Objects, Formatting.Indented);
      File.WriteAllText(sPath, s);
    }

    public void ReadFromDisk()
    {
      string sPath = Path.Combine(DataPath, UniverseFile);
      if (!File.Exists(sPath))
      {
        Console.WriteLine("WARNING: File not found:" + UniverseFile + " MZone.ReadFromDisk");
        return;
      }


      string sData = File.ReadAllText(sPath);
      Objects = JsonConvert.DeserializeObject<Dictionary<string, MServerObject>>(sData);

      if (Objects == null)
      {
        Objects = new Dictionary<string, MServerObject>();
      }

      /*
      //TODO: Get client zone
      Objects.Clear();
      string[] f = File.ReadAllLines(WorldFile);
      foreach (string s in f)
      {
        if (string.IsNullOrEmpty(s)) continue;
        //MPosMessage m = new MPosMessage(s);
        if (Objects.ContainsKey(m.InstanceID))
        {
          Console.WriteLine("WARNING: ReadFromDisk: Object already in database " + m.InstanceID + " IGNORED");
        }
        else
        {
          Objects.Add(m.InstanceID, m);
        }
      }
      */
    }

    public int RemoveObject(string id)
    {
      if (id == null) return 0;
      if (Objects.ContainsKey(id))
      {
        MServerObject mo = Objects[id];
        Objects.Remove(id);
        if (mo.StaticStorage == 1)
        {
          return 1;
        }
        else
        {
          return 0;
        }
      }
      return 0;
    }

    public bool InsideSphere(double sx, double sy, double sz, double x, double y, double z, double Radius)
    {
      double d = (double)Math.Sqrt((x - sx) * (x - sx) + (y - sy) * (y - sy) + (z - sz) * (z - sz));

      //double d = Math.Pow(x - sx, 2) + Math.Pow(y - sy, 2) + Math.Pow(z - sz, 2);
      //double r2 = Math.Pow(Radius, 2);
      return d < Radius;
    }

    /**
     * TODO: Create a 3 dimensional database b-tree. (rounded coordinates)
     * Return all objects within bucket.
     * 
     * */
    public List<MServerObject> GetObjectsNear(string UserID, double x, double y, double z)
    {
      if (Objects == null) return new List<MServerObject>();
      //TODO: create new list of objects based on distance and only return the objects within threshold
      List<MServerObject> Results = new List<MServerObject>();

      foreach (KeyValuePair<string, MServerObject> kvp in Objects)
      {
        //if (kvp.Value.Distance(x, y, z) < DistanceThreshold * 3)
        if (InsideSphere(kvp.Value.Position[0], kvp.Value.Position[1], kvp.Value.Position[2], x, y, z, kvp.Value.Radius))
        {
          //don't sent client avatar back to himself.
          if (!kvp.Value.InstanceID.Equals(UserID))
          {
            Results.Add(kvp.Value);
          }
        }
      }
      return Results;
    }

    public string DumpUser(string UserID)
    {
      Dictionary<string, MServerObject> Results = new Dictionary<string, MServerObject>();
      foreach (KeyValuePair<string, MServerObject> kvp in Objects)
      {
        if (kvp.Value.OwnerID.Equals(UserID)) Results.Add(kvp.Key, kvp.Value);
      }

      string s = JsonConvert.SerializeObject(Results, Formatting.Indented);
      return s;
    }

    public void MakeBackup(string sPath)
    {
      if (File.Exists(Path.Combine(DataPath, UniverseFile)))
      {
        string dest = Path.Combine(sPath, UniverseFile + DateTime.Now.ToBinary() + ".json");
        File.Copy(Path.Combine(DataPath, UniverseFile), dest);
      }
    }

    //public MNetMessage[] GetObjects()
    //{
    //return Objects.Values.ToArray();
    //}
  }
}
