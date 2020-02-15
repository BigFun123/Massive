using Massive;
using Massive.Events;
using Massive.Network;
using Massive.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWorld.Controllers
{
  public class UserDetails
  {
    public UserDetails()
    {
      Globals.Network.USerDetailsChanged += Network_USerDetailsChanged;
      MMessageBus.UserRegistered += MMessageBus_UserRegistered;
    }

    private void MMessageBus_UserRegistered(object sender, ChangeDetailsEvent e)
    {
      Save();
    }

    private void Network_USerDetailsChanged(object sender, ChangeDetailsEvent e)
    {
      Save();
    }

    public void Setup()
    {
      Load();
    }

    public void Save()
    {
      String sDetails = Globals.UserAccount.Serialize();
      MFileSystem.SaveFile("settings.json", sDetails);
    }

    public void Load()
    {
      String sDetails = MFileSystem.GetFile("settings.json");
      try
      {
        MUserAccount acc = MUserAccount.Deserialize<MUserAccount>(sDetails);
        if (acc != null)
        {
          Globals.UserAccount = acc;
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }
  }
}
