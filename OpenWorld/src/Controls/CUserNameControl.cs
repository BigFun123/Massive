using Massive;
using Massive.Events;
using OpenWorld.Forms;
using OpenWorld.Services;
using System;
using System.Windows.Forms;

namespace ThisIsMassive.src.Controls
{
  public partial class CUserNameControl : UserControl
  {
    public CUserNameControl()
    {
      InitializeComponent();
      MMessageBus.LoggedIn += MMessageBus_LoggedIn;
      MMessageBus.UserDetailsChanged += MMessageBus_UserDetailsChanged;
      MMessageBus.UserRegistered += MMessageBus_UserRegistered;
    }

    private void MMessageBus_UserRegistered(object sender, ChangeDetailsEvent e)
    {
      SetName(Globals.UserAccount.UserName);
      SetStatus(Globals.UserAccount.TotalObjects, Globals.UserAccount.MaxObjects);
      SetCredit(Globals.UserAccount.Credit);
    }

    private void MMessageBus_UserDetailsChanged(object sender, ChangeDetailsEvent e)
    {
      SetName(Globals.UserAccount.UserName);
      SetStatus(Globals.UserAccount.TotalObjects, Globals.UserAccount.MaxObjects);
      SetCredit(Globals.UserAccount.Credit);
    }

    private void MMessageBus_LoggedIn(object sender, ChangeDetailsEvent e)
    {
      SetName(Globals.UserAccount.UserName);
      SetStatus(Globals.UserAccount.TotalObjects, Globals.UserAccount.MaxObjects);
      SetCredit(Globals.UserAccount.Credit);
    }

    void SetCredit(double Credit)
    {
      CreditLabel.Text = string.Format("${0:0.00}", Credit);
    }

    void SetStatus(int Objects, int Max)
    {
      StatusText.Text = string.Format("B: {0}/{1}", Objects, Max);
    }

    void SetName(string s)
    {
      if (string.IsNullOrEmpty(s))
      {
        Username.Text = "Anonymous";
      }
      else
      {
        Username.Text = Globals.UserAccount.UserName;
      }      
    }

    private void Username_Click(object sender, EventArgs e)
    {
      UserInfoForm ui = new UserInfoForm();
      ui.Show();
    }

    private void Username_MouseEnter(object sender, EventArgs e)
    {
      Username.BackColor = ThemeService.OverBG;
    }

    private void Username_MouseLeave(object sender, EventArgs e)
    {
      Username.BackColor = ThemeService.NormalBG;
    }
  }
}
