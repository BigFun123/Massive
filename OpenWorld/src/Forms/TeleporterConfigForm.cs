using Massive;
using Massive.Events;
using Massive.Tools;
using Massive2.Modules.Widgets;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class TeleporterConfigForm : DToolForm
  {
    string InstanceID;
    Vector3d Destination;
    string Description;
    bool Success = false;

    public TeleporterConfigForm()
    {
      InitializeComponent();
      MMessageBus.PropertyChangeEvent += Network_PropertyChangeHandler;
      timer1.Tick += Timer1_Tick;
      SetTitle("Teleporter");
    }

    private void MMessageBus_PropertyChangeEvent(object sender, ChangePropertyEvent e)
    {
      throw new NotImplementedException();
    }

    private void Timer1_Tick(object sender, EventArgs e)
    {
      Globals.Network.PropertyChangeHandler -= Network_PropertyChangeHandler;
      timer1.Tick -= Timer1_Tick;
      Close();
    }

    private void Network_PropertyChangeHandler(object sender, ChangePropertyEvent e)
    {

      MObject mo = MScene.Root.FindModuleByInstanceID(e.InstanceID);
      if (mo != null)
      {
        mo.Tag = e.PropertyTag;
      }

      if (e.InstanceID.Equals(InstanceID))
      {
        Invoke((MethodInvoker)delegate
        {
          ErrorLabel.Text = "Success!";
          ErrorLabel.ForeColor = Color.DarkGreen;
          Success = true;
          //UpdatePropertiesButton.Text = "OK";
          timer1.Start();
        });
      }
      else
      {
        ErrorLabel.Text = "Can't Update. Try Again?";
        ErrorLabel.ForeColor = Color.DarkRed;
        UpdatePropertiesButton.Text = "Try Again";
      }
    }

    public void Setup(MObject o)
    {
      MSceneObject mo = (MSceneObject)o;
      if (mo == null) return;

      InstanceID = mo.InstanceID;
      Destination = MTeleporter.GetDestination(mo);
      DestinationBox.Text = MassiveTools.VectorToString(Destination);
      Description = MTeleporter.GetDescription(mo);
      DescriptionBox.Text = Description;
    }

    private void UpdatePropertiesButton_Click(object sender, EventArgs e)
    {
      if (Success == true)
      {
        Globals.Network.PropertyChangeHandler -= Network_PropertyChangeHandler;
        Close();
      }
      if (string.IsNullOrEmpty(DestinationBox.Text))
      {
        return;
      }
      Destination = MassiveTools.VectorFromString(DestinationBox.Text);
      string DestString = Destination.ToString();
      DestString = DestString.Replace("(", "");
      DestString = DestString.Replace(")", "");
      DestString = DestString.Replace(" ", "");
      Description = DescriptionBox.Text;

      Globals.Network.ChangePropertyRequest(InstanceID, MBuildParts.TELEPORT01 + "|" + DestString + "|" + Description);
    }

    private void TeleporterConfigForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      MMessageBus.PropertyChangeEvent -= Network_PropertyChangeHandler;
    }
  }
}
