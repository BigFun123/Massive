using Massive.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MassiveServer.src.forms
{
  public partial class RegisterForm : Form
  {
    string GUID = "000";
    public RegisterForm()
    {
      InitializeComponent();
    }

    private void RegisterButton_Click(object sender, EventArgs e)
    {
      ResponseText.Text = "Registering...";
      string sName = UniName.Text;
      string sStyle = StyleCombo.Text;
      string sDomain = HostNameBox.Text;

      WebClient wc = new WebClient();
      try { 
      string Result = wc.DownloadString("http://bigfun.co.za/massive/lobby/?f=register&name=" 
        + sName 
        + "&style=" + sStyle 
        + "&domain=" + sDomain
        + "&ip=" + MExternalIPSniffer.externalip
        + "&ownerid=" + GUID);
      if (!string.IsNullOrEmpty(Result))
      {
        ResponseText.Text = Result;
      }
      }
      catch( Exception ee)
      {
        ResponseText.Text = ee.Message;
      }
    }

    void GenerateGUID()
    {
      GUID = UidGen.GetMachineID();
    }

    private void RegisterForm_Load(object sender, EventArgs e)
    {
      IPLabel.Text = MExternalIPSniffer.externalip;
      if ( IPLabel.Text.CompareTo("0.0.0.0") == 0)
      {
        IPLabel.Text = "No External IP Yet! Close this form and try again.";
      }
      GenerateGUID();
    }
  }
}
