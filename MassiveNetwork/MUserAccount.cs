using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massive.Network
{
  public class MUserAccount : IMSerializable
  {
    public string UserID { get; set; }
    public string SessionToken;
    public string UserName;
    public string Email;
    public string Password;
    public string FirstName;
    public string LastName;    
    public string Age;
    public string Zone;
    public string Phone;
    public string ServerIP;
    public string ClientIP;
    public string AvatarID = "AVATAR04";
    public double Credit = 0;
    public int MaxObjects = 100;
    public int MaxFoundations = 1;
    public int TotalObjects = 0;
    public bool TermsAccepted = false;
    public DateTime LastActivity;
    public double[] HomePosition = new double[3];
    public double[] LastSyncPosition = new double[3];
    public double[] CurrentPosition = new double[3];

    public void CopyTo(MUserAccount mu)
    {
      mu.UserID = UserID;
      //mu.SessionToken = SessionToken;
      mu.UserName = UserName;
      mu.Email = Email;
      mu.Password = Password;
      mu.FirstName = FirstName;
      mu.LastName = LastName;
      mu.Age = Age;
      mu.Zone = Zone;
      mu.Phone = Phone;
      mu.ServerIP = ServerIP;
      mu.AvatarID = AvatarID;
      //mu.MaxObjects = MaxObjects;
      //mu.TotalObjects = TotalObjects;
      mu.TermsAccepted = TermsAccepted;
      mu.LastName = LastName;
      mu.HomePosition = HomePosition;
      mu.LastSyncPosition = LastSyncPosition;
      mu.CurrentPosition = CurrentPosition;
    }

    //TODO: FRIENDS
    public bool FriendOf(string UserID)
    {
      return true;
    }

    public double DistanceToLastSync(double x2, double y2, double z2 )
    {
      return Math.Sqrt((x2 - LastSyncPosition[0]) * (x2 - LastSyncPosition[0]) 
        + (y2 - LastSyncPosition[1]) * (y2 - LastSyncPosition[1]) 
        + (z2 - LastSyncPosition[2]) * (z2 - LastSyncPosition[2]));
    }
  }
}
