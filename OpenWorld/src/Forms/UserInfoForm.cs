using Massive;
using Massive.Events;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class UserInfoForm : DToolForm
  {
    Color OldBackColor = Color.Gray;
    Color ErrorColor = Color.Pink;
    Color SuccessTextColor = Color.DarkGreen;

    public UserInfoForm()
    {
      InitializeComponent();
      SetTitle("User Registration for Server");
    }

    private void UserInfoForm_Shown(object sender, EventArgs e)
    {
      Setup();
    }

    void Setup()
    {
      OldBackColor = EmailBox.BackColor;
      LoadData();
      CheckValidate();
      SetupEvents();
#if DEBUG
      UserIDBox.ReadOnly = false;
#else
      //ServersCombo.Enabled = false;
      //ServersCombo.Visible = false;
#endif
    }

    void SetupEvents()
    {
      //Globals.Network.USerDetailsChanged += Network_DetailsChanged;
      MMessageBus.UserDetailsChanged += MMessageBus_UserRegistered;
      MMessageBus.UserRegistered += MMessageBus_UserRegistered;
    }

    private void MMessageBus_UserRegistered(object sender, ChangeDetailsEvent e)
    {
      Status(e.Success, e.Message);

      UserIDBox.Text = Globals.UserAccount.UserID;
      EmailBox.Text = Globals.UserAccount.Email;
      UserNameBox.Text = Globals.UserAccount.UserName;
    }

    void ClearEvents()
    {
      MMessageBus.UserRegistered -= MMessageBus_UserRegistered;
    }

    bool ValidateEmail(string s)
    {
      if (string.IsNullOrEmpty(s)) return false;
      bool result = true;

      if (s.Contains(' ')) result = false;
      if (!s.Contains('@')) result = false;
      if (!s.Contains('.')) result = false;
      return result;
    }

    bool CheckValidate()
    {
      bool valid = true;
      if (!ValidateEmail(Globals.UserAccount.Email))
      {
        EmailBox.BackColor = ErrorColor;
        valid = false;
      }
      else
      {
        EmailBox.BackColor = OldBackColor;
      }

      if (string.IsNullOrEmpty(UserNameBox.Text))
      {
        UserNameBox.BackColor = ErrorColor;
        valid = false;
      }
      else
      {
        UserNameBox.BackColor = OldBackColor;
      }

      return valid;

    }

    void LoadData()
    {
      EmailBox.Text = Globals.UserAccount.Email;
      UserNameBox.Text = Globals.UserAccount.UserName;
      PasswordBox.Text = Globals.UserAccount.Password;
      UserIDBox.Text = Globals.UserAccount.UserID;

      switch (Globals.UserAccount.AvatarID)
      {
        case MBuildParts.AVATAR01:
          Avatar1.Checked = true;
          break;
        case MBuildParts.AVATAR02:
          Avatar2.Checked = true;
          break;
        case MBuildParts.AVATAR03:
          Avatar3.Checked = true;
          break;
      }


    }

    void SaveData()
    {
      Globals.UserAccount.Email = EmailBox.Text;
      Globals.UserAccount.UserName = UserNameBox.Text;
      Globals.UserAccount.Password = PasswordBox.Text;
      CheckValidate();
      //Globals.UserAccount.UserID = UserIDBox.Text;
    }

    void Status(bool Success, string s)
    {
      if (Success == true)
      {
        StatusLabel.ForeColor = SuccessTextColor;
      }
      else
      {
        StatusLabel.ForeColor = Color.Red;
      }
      StatusLabel.Text = s;
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
      SaveData();
      if (CheckValidate() == false)
      {
        Status(false, "Please fill in the required fields");
        return;
      }
      StatusLabel.Text = "Update Details...";

      if (Globals.Network.Connected == false)
      {
        Status(false, "Not Logged in to a server... log in to a server to register with it.");
      }
      else
      {
        //Globals.Network.ChangeDetailsRequest(Globals.UserAccount);
        Globals.Network.UserRegisterRequest(Globals.UserAccount);
      }
    }

    private void DoneButton_Click(object sender, EventArgs e)
    {
      SaveData();
      ClearEvents();
      MMessageBus.ChangedUserInfo(this);
      Close();
    }

    private void UserIDBox_TextChanged(object sender, EventArgs e)
    {
      // if (!string.IsNullOrEmpty(UserIDBox.Text))
      //{
      //        MMessageBus.ChangeUserID(this, UserIDBox.Text);
      //    }
    }

    private void Avatar1_CheckedChanged(object sender, EventArgs e)
    {
      if (Avatar1.Checked == true)
      {
        Globals.UserAccount.AvatarID = MBuildParts.AVATAR01;
        MMessageBus.ChangedUserInfo(this);
        MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      }
    }

    private void Avatar2_CheckedChanged(object sender, EventArgs e)
    {
      if (Avatar2.Checked == true)
      {
        Globals.UserAccount.AvatarID = MBuildParts.AVATAR02;
        MMessageBus.ChangedUserInfo(this);
        MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      }
    }

    private void Avatar3_CheckedChanged(object sender, EventArgs e)
    {
      if (Avatar3.Checked == true)
      {
        Globals.UserAccount.AvatarID = MBuildParts.AVATAR03;
        MMessageBus.ChangedUserInfo(this);
        MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      }
    }

    private void Avatar4_CheckedChanged(object sender, EventArgs e)
    {
      if (Avatar4.Checked == true)
      {
        Globals.UserAccount.AvatarID = MBuildParts.AVATAR04;
        MMessageBus.ChangedUserInfo(this);
        MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      }
    }

    private void Avatar5_CheckedChanged(object sender, EventArgs e)
    {
      if (Avatar5.Checked == true)
      {
        Globals.UserAccount.AvatarID = MBuildParts.AVATAR05;
        MMessageBus.ChangedUserInfo(this);
        MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      }
    }

    private void Avatar6_CheckedChanged(object sender, EventArgs e)
    {
      if (Avatar6.Checked == true)
      {
        Globals.UserAccount.AvatarID = MBuildParts.AVATAR06;
        MMessageBus.ChangedUserInfo(this);
        MMessageBus.ChangeAvatarRequest(this, Globals.UserAccount.UserID, Globals.UserAccount.AvatarID);
      }
    }

    private void UserInfoForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      ClearEvents();
    }

    void UpdateData()
    {
      SaveData();
      if (CheckValidate() == false)
      {
        Status(false, "Please fill in the required fields");
        return;
      }
      StatusLabel.Text = "Update Details...";

      if (Globals.Network.Connected == false)
      {
        Status(false, "Not Logged in to a server... log in to a server to register with it.");
      }
      else
      {
        //Globals.Network.ChangeDetailsRequest(Globals.UserAccount);
        Globals.Network.ChangeDetailsRequest(Globals.UserAccount);
      }
    }

    private void UpdateDetailsButton_Click(object sender, EventArgs e)
    {
      UpdateData();
    }


  }
}
