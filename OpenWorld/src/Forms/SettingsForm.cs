using Massive;
using Massive.Events;
using System;

namespace OpenWorld.Forms
{
  public partial class SettingsForm : DToolForm
  {
    public SettingsForm()
    {
      InitializeComponent();
      SetTitle("Settings");
    }

    private void GravityCheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.Gravity = GravityCheck.Checked;
      MMessageBus.GravityStateChanged(this, new BooleanEvent(Settings.Gravity));
    }

    private void TerrainPhysicsCheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.TerrainPhysics = TerrainPhysicsCheck.Checked;
    }

    private void SettingsForm_Load(object sender, EventArgs e)
    {
      TerrainPhysicsCheck.Checked = Settings.TerrainPhysics;
      GravityCheck.Checked = Settings.Gravity;
    }

    private void DebugPhysicsCheck_CheckedChanged(object sender, EventArgs e)
    {
      MScene.Physics.DebugWorld = DebugPhysicsCheck.Checked;
    }

    private void DebugDepthCheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.DebugDepth = DebugDepthCheck.Checked;
    }

    private void TweakBar1_Scroll(object sender, EventArgs e)
    {
      Settings.Tweak1 = TweakBar1.Value;
      Tweak1Label.Text = Settings.Tweak1.ToString();
    }

    private void TweakBar2_Scroll(object sender, EventArgs e)
    {
      Settings.Tweak2 = TweakBar2.Value;
      Tweak2Label.Text = Settings.Tweak2.ToString();
    }

    private void TweakBar3_Scroll(object sender, EventArgs e)
    {
      Settings.Tweak3 = TweakBar3.Value;
      Tweak3Label.Text = Settings.Tweak3.ToString();
    }

    private void TweakBar4_Scroll(object sender, EventArgs e)
    {
      Settings.Tweak4 = TweakBar4.Value;
      Tweak4Label.Text = Settings.Tweak4.ToString();
    }

    private void DebugPicking_CheckedChanged(object sender, EventArgs e)
    {
      Settings.DebugRender = !Settings.DebugRender;
    }

    private void FrustrumCullingCheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.FrustrumCullingEnabled = FrustrumCullingCheck.Checked;
    }

    private void DistanceClippingcheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.DistanceClippingEnabled = DistanceClippingcheck.Checked;
    }

    private void DrawTreesCheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.DrawTrees = DrawTreesCheck.Checked;
    }

    private void DrawGrassCheck_CheckedChanged(object sender, EventArgs e)
    {
      Settings.DrawGrass = DrawGrassCheck.Checked;
    }

    private void DrawTerrains_CheckedChanged(object sender, EventArgs e)
    {
      Settings.DrawTerrains = DrawTerrains.Checked;
    }
  }
}
