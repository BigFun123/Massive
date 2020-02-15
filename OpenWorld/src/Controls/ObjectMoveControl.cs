using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Massive;
using Massive.Events;
using OpenTK;
using OpenWorld.Handlers;

namespace OpenWorld.src.Controls
{
  public partial class ObjectMoveControl : UserControl
  {
    MSceneObject SelectedItem;

    public ObjectMoveControl()
    {
      InitializeComponent();
      MMessageBus.SelectEventHandler += MMessageBus_SelectEventHandler;
      MMessageBus.ObjectDeletedEvent += MMessageBus_ObjectDeletedEvent;
    }

    private void MMessageBus_ObjectDeletedEvent(object sender, DeleteEvent e)
    {
      if (SelectedItem == null) return;
      if (e.InstanceID == SelectedItem.InstanceID)
      {
        SelectedItem = null;
      }
    }

    private void MMessageBus_SelectEventHandler(object sender, SelectEvent e)
    {
      if (e == null) return;
      SelectedItem = e.Selected;
    }

    void MoveSelection(double x, double y, double z)
    {
      if (SelectedItem == null) return;
      double mult = MKeyboardHandler.GetMinifier();

      // complete any pending transitions
      MMoveSync ms = (MMoveSync)SelectedItem.FindModuleByType(MObject.EType.MoveSync);
      if (ms != null)
      {
        ms.Complete();
      }

      Vector3d v = SelectedItem.transform.Position
        + SelectedItem.transform.Right() * x * mult
        + SelectedItem.transform.Forward() * y * mult
        + SelectedItem.transform.Up() * z * mult;
      MMessageBus.MoveRequest(this, SelectedItem.InstanceID, v, SelectedItem.transform.Rotation);
    }

    void RotateSelection(double x, double y, double z)
    {
      if (SelectedItem == null) return;
      double mult = MKeyboardHandler.GetRotationMinifier();

      x = MathHelper.DegreesToRadians(x * mult);
      y = MathHelper.DegreesToRadians(y * mult);
      z = MathHelper.DegreesToRadians(z * mult);
      
      // complete any pending transitions
      MMoveSync ms = (MMoveSync)SelectedItem.FindModuleByType(MObject.EType.MoveSync);
      if ( ms != null)
      {
        ms.Complete();
      }

      Quaterniond rot = SelectedItem.transform.Rotation * Quaterniond.FromEulerAngles(x, y, z);
      MMessageBus.MoveRequest(this, SelectedItem.InstanceID, SelectedItem.transform.Position, rot);
    }

    private void MoveXMinus_Click(object sender, EventArgs e)
    {
      MoveSelection(-1, 0, 0);
    }

    private void MoveXPlus_Click(object sender, EventArgs e)
    {
      MoveSelection(1, 0, 0);
    }

    private void MoveYPositive_Click(object sender, EventArgs e)
    {
      MoveSelection(0, 1, 0);
    }

    private void MoveYMinus_Click(object sender, EventArgs e)
    {
      MoveSelection(0, -1, 0);
    }

    private void MoveZPlus_Click(object sender, EventArgs e)
    {
      MoveSelection(0, 0, 1);
    }

    private void MoveZMinus_Click(object sender, EventArgs e)
    {
      MoveSelection(0, 0, -1);
    }

    private void RotateYM_Click(object sender, EventArgs e)
    {
      RotateSelection(0, -1, 0);
    }

    private void RotateYP_Click(object sender, EventArgs e)
    {
      RotateSelection(0, 1, 0);
    }

    private void RotateXP_Click(object sender, EventArgs e)
    {
      RotateSelection(1, 0, 0);
    }

    private void RotateXM_Click(object sender, EventArgs e)
    {
      RotateSelection(-1, 0, 0);
    }

    private void RotateZM_Click(object sender, EventArgs e)
    {
      RotateSelection(0, 0, -1);
    }

    private void RotateZP_Click(object sender, EventArgs e)
    {
      RotateSelection(0, 0, 1);
    }
  }
}
