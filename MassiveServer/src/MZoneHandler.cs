using Massive.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Server
{
  public class MZoneHandler
  {
    public List<MServerZone> Zones = new List<MServerZone>();

    bool MakeBackups = false;
    string DataPath = "data";
    string ZoneFile = @"zones.csv";

    public MZoneHandler()
    {
      ReadFromDisk();
      AddDefaultZones();
    }

    void AddDefaultZones()
    {            
      Add(new MServerZone("WelcomeToEarth", "Welcome Zone", "Help", new Vector3d_Server(12725968905.6, 146363992998.2, -7572886975.0)));
      Add(new MServerZone("OrionArt", "Orion Art Gallery", "Art", new Vector3d_Server(100000005.0, 20000000001.0, 100000000005.0)));
    }

    public string GetObjectsAsString()
    {
      string s = JsonConvert.SerializeObject(Zones);
      return s;
    }

    public bool Add(MServerZone m)
    {
      if (Zones.Find(x => x.Name.Equals(m.Name))!=null) return false;

      Zones.Add(m);
      WriteToDisk();
      return true;
    }

    public bool Remove(string UserID, MServerZone m)
    {
      MServerZone zone = Zones.Find(x => x.Name.Equals(m.Name) && x.OwnerID.Equals(UserID));
      if ( zone != null )
      {
        Zones.Remove(zone);
        WriteToDisk();
        return true;
      }
      return false;
    }

    public bool Update(string UserID, MServerZone z)
    {
      MServerZone zone = Zones.Find(x => x.Name.Equals(z.Name) && x.OwnerID.Equals(UserID));
      if ( zone != null)
      {
        z.CopyTo(zone);        
        WriteToDisk();
        return true;
      }

      return false;
    }

    //TODO : Replace with DB request
    public void WriteToDisk()
    {
      string sPath = Path.Combine(DataPath, ZoneFile);
      string s = JsonConvert.SerializeObject(Zones, Formatting.Indented);
      File.WriteAllText(sPath, s);
    }

    public void ReadFromDisk()
    {
      string sPath = Path.Combine(DataPath, ZoneFile);
      if (!File.Exists(sPath))
      {
        Console.WriteLine("WARNING: File not found:" + ZoneFile + " MZone.ReadFromDisk");
        return;
      }

      string sData = File.ReadAllText(sPath);
      Zones = JsonConvert.DeserializeObject<List<MServerZone>>(sData);
    }

    public void MakeBackup(string sPath)
    {
      if (File.Exists(Path.Combine(DataPath, ZoneFile)))
      {
        string dest = Path.Combine(sPath, ZoneFile + DateTime.Now.ToBinary() + ".json");
        File.Copy(Path.Combine(DataPath, ZoneFile), dest);
      }
    }

  }
}
