using Massive;
using Massive.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWorld.Forms
{
  public partial class LinkerConfigForm : DToolForm
  {
    public MSceneObject Target;
    public LinkerConfigForm()
    {
      InitializeComponent();
      SetTitle("Link Config");
      MMessageBus.PropertyChangeEvent += MMessageBus_PropertyChangeEvent;
    }

    private void MMessageBus_PropertyChangeEvent(object sender, ChangePropertyEvent e)
    {
      if (Target == null) return;
      if (e.InstanceID == Target.InstanceID)
      {
        StatusLabel.Text = "Updated. You can now close this form or test the link.";
      }
    }

    public void Setup(MObject inSceneObject)
    {
      Target = (MSceneObject)inSceneObject;
      if (Target == null) return;
      if (Target.Tag != null)
      {
        LinkBox.Text = (string)Target.Tag;
      }
    }

    private void UpdateButton_Click(object sender, EventArgs e)
    {
      if (Target == null) return;
      StatusLabel.Text = "Updating...";
      //if (string.IsNullOrEmpty(LinkBox.Text)) return;

      Globals.Network.ChangePropertyRequest(Target.InstanceID, LinkBox.Text);
    }

    private void TestButton_Click(object sender, EventArgs e)
    {
      if (Target == null) return;

      try { 
        Process.Start((string)Target.Tag);
      }
      catch ( Exception ee)
      {
        Console.WriteLine(ee.Message);
        MMessageBus.Error(this, ee.Message);
      }
    }
  }
}
