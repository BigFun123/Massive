using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Massive.Network;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;

namespace Massive.Server
{
  public class MClient : INotifyPropertyChanged
  {
    public int Port = 0;
    [JsonIgnore]
    public Connection connection;
    [JsonIgnore]

    public IPAddress Address { get; set; }
    public bool IsServer = false;
    public bool Synced = false;
    
    public const string STATE_DISCONNECTED = "DISCONNECTED";
    public const string STATE_CONNECTING = "CONNECTING";
    public const string STATE_CONNECTOLOBBY = "CONNECTOLOBBY";
    public const string STATE_CONNECTOWORLD = "CONNECTOWORLD";
    public const string STATE_INWORLD = "INWORLD";
    public string State = "DISCONNECTED";

    public MUserAccount Account;
    //Vector3d UniversePosition;

    //public int TotalObjectsInWorld = 0;
    //public int MaxObjects = 20;
    public bool IsOnline = false;

    public bool ActivityFlag = false;

    public MBox Fence = new MBox();

    //Cape Town XYZ
    public double[] DefaultHome = new double[3] { 12717655836.1, 146353256922.54, -7581841295.83795 };

    public event PropertyChangedEventHandler PropertyChanged;

    public MClient()
    {
      Account = new MUserAccount();
      Account.HomePosition = DefaultHome;
    }

    void Change(string s)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(s));
      }
    }

    public void Save()
    {
      if (string.IsNullOrEmpty(Account.UserID))
      {
        return;
      }
      string sJson = JsonConvert.SerializeObject(Account, Formatting.Indented);
      File.WriteAllText(GetDataPath(), sJson);
      Change("Account");
    }

    public bool Load()
    {
      if (!File.Exists(GetDataPath()))
      {
        return false;
      }

      string s = File.ReadAllText(GetDataPath());
      Account = JsonConvert.DeserializeObject<MUserAccount>(s);
      if (Account == null) return false;

      if ((Account.HomePosition[0] == 0)
        && (Account.HomePosition[1] == 0)
        && (Account.HomePosition[2] == 0))
      {
        Account.HomePosition = DefaultHome;
      }

      Change("Account");
      return true;
    }

    public void CreateNewAccount()
    {
      Account.UserID = UidGen.GUID();
    }

    string GetDataPath()
    {
      if (Account.UserID == null)
      {
        return "";
      }
      string safeid = Account.UserID.Replace("/", "-");
      safeid = safeid.Replace("\\", "-");

      return Path.Combine(Globals.DataPath, safeid + ".json");
    }

    public override string ToString()
    {
      return Address.ToString() + " : " + Port.ToString() + (IsServer ? "(S)" : "") + " UserID:" + Account.UserID + " Email:" + Account.Email + " Username:" + Account.UserName;
    }
  }
}
