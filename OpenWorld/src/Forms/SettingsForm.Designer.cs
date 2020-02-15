namespace OpenWorld.Forms
{
  partial class SettingsForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.DrawGrassCheck = new System.Windows.Forms.CheckBox();
      this.DrawTreesCheck = new System.Windows.Forms.CheckBox();
      this.DistanceClippingcheck = new System.Windows.Forms.CheckBox();
      this.FrustrumCullingCheck = new System.Windows.Forms.CheckBox();
      this.Tweak4Label = new System.Windows.Forms.Label();
      this.Tweak3Label = new System.Windows.Forms.Label();
      this.Tweak2Label = new System.Windows.Forms.Label();
      this.DebugPicking = new System.Windows.Forms.CheckBox();
      this.DebugDepthCheck = new System.Windows.Forms.CheckBox();
      this.DebugPhysicsCheck = new System.Windows.Forms.CheckBox();
      this.TerrainPhysicsCheck = new System.Windows.Forms.CheckBox();
      this.GravityCheck = new System.Windows.Forms.CheckBox();
      this.TweakBar1 = new System.Windows.Forms.TrackBar();
      this.TweakBar3 = new System.Windows.Forms.TrackBar();
      this.TweakBar4 = new System.Windows.Forms.TrackBar();
      this.TweakBar2 = new System.Windows.Forms.TrackBar();
      this.Tweak1Label = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.DrawTerrains = new System.Windows.Forms.CheckBox();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar2)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.DrawTerrains, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.DrawGrassCheck, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.DrawTreesCheck, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.DistanceClippingcheck, 0, 5);
      this.tableLayoutPanel2.Controls.Add(this.FrustrumCullingCheck, 1, 5);
      this.tableLayoutPanel2.Controls.Add(this.Tweak4Label, 1, 9);
      this.tableLayoutPanel2.Controls.Add(this.Tweak3Label, 1, 8);
      this.tableLayoutPanel2.Controls.Add(this.Tweak2Label, 1, 7);
      this.tableLayoutPanel2.Controls.Add(this.DebugPicking, 1, 4);
      this.tableLayoutPanel2.Controls.Add(this.DebugDepthCheck, 1, 3);
      this.tableLayoutPanel2.Controls.Add(this.DebugPhysicsCheck, 1, 2);
      this.tableLayoutPanel2.Controls.Add(this.TerrainPhysicsCheck, 1, 1);
      this.tableLayoutPanel2.Controls.Add(this.GravityCheck, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this.TweakBar1, 0, 6);
      this.tableLayoutPanel2.Controls.Add(this.TweakBar3, 0, 8);
      this.tableLayoutPanel2.Controls.Add(this.TweakBar4, 0, 9);
      this.tableLayoutPanel2.Controls.Add(this.TweakBar2, 0, 7);
      this.tableLayoutPanel2.Controls.Add(this.Tweak1Label, 1, 6);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 11;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(296, 435);
      this.tableLayoutPanel2.TabIndex = 2;
      // 
      // DrawGrassCheck
      // 
      this.DrawGrassCheck.AutoSize = true;
      this.DrawGrassCheck.Checked = true;
      this.DrawGrassCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.DrawGrassCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DrawGrassCheck.ForeColor = System.Drawing.Color.White;
      this.DrawGrassCheck.Location = new System.Drawing.Point(3, 35);
      this.DrawGrassCheck.Name = "DrawGrassCheck";
      this.DrawGrassCheck.Size = new System.Drawing.Size(142, 26);
      this.DrawGrassCheck.TabIndex = 22;
      this.DrawGrassCheck.Text = "Grass";
      this.DrawGrassCheck.UseVisualStyleBackColor = true;
      this.DrawGrassCheck.CheckedChanged += new System.EventHandler(this.DrawGrassCheck_CheckedChanged);
      // 
      // DrawTreesCheck
      // 
      this.DrawTreesCheck.AutoSize = true;
      this.DrawTreesCheck.Checked = true;
      this.DrawTreesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.DrawTreesCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DrawTreesCheck.ForeColor = System.Drawing.Color.White;
      this.DrawTreesCheck.Location = new System.Drawing.Point(3, 3);
      this.DrawTreesCheck.Name = "DrawTreesCheck";
      this.DrawTreesCheck.Size = new System.Drawing.Size(142, 26);
      this.DrawTreesCheck.TabIndex = 21;
      this.DrawTreesCheck.Text = "Trees";
      this.DrawTreesCheck.UseVisualStyleBackColor = true;
      this.DrawTreesCheck.CheckedChanged += new System.EventHandler(this.DrawTreesCheck_CheckedChanged);
      // 
      // DistanceClippingcheck
      // 
      this.DistanceClippingcheck.AutoSize = true;
      this.DistanceClippingcheck.Checked = true;
      this.DistanceClippingcheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.DistanceClippingcheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DistanceClippingcheck.ForeColor = System.Drawing.Color.White;
      this.DistanceClippingcheck.Location = new System.Drawing.Point(3, 163);
      this.DistanceClippingcheck.Name = "DistanceClippingcheck";
      this.DistanceClippingcheck.Size = new System.Drawing.Size(142, 26);
      this.DistanceClippingcheck.TabIndex = 20;
      this.DistanceClippingcheck.Text = "Distance Clipping";
      this.DistanceClippingcheck.UseVisualStyleBackColor = true;
      this.DistanceClippingcheck.CheckedChanged += new System.EventHandler(this.DistanceClippingcheck_CheckedChanged);
      // 
      // FrustrumCullingCheck
      // 
      this.FrustrumCullingCheck.AutoSize = true;
      this.FrustrumCullingCheck.Checked = true;
      this.FrustrumCullingCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.FrustrumCullingCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FrustrumCullingCheck.ForeColor = System.Drawing.Color.White;
      this.FrustrumCullingCheck.Location = new System.Drawing.Point(151, 163);
      this.FrustrumCullingCheck.Name = "FrustrumCullingCheck";
      this.FrustrumCullingCheck.Size = new System.Drawing.Size(142, 26);
      this.FrustrumCullingCheck.TabIndex = 19;
      this.FrustrumCullingCheck.Text = "Frustrum Culling";
      this.FrustrumCullingCheck.UseVisualStyleBackColor = true;
      this.FrustrumCullingCheck.CheckedChanged += new System.EventHandler(this.FrustrumCullingCheck_CheckedChanged);
      // 
      // Tweak4Label
      // 
      this.Tweak4Label.AutoSize = true;
      this.Tweak4Label.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Tweak4Label.Location = new System.Drawing.Point(151, 289);
      this.Tweak4Label.Name = "Tweak4Label";
      this.Tweak4Label.Size = new System.Drawing.Size(142, 33);
      this.Tweak4Label.TabIndex = 17;
      this.Tweak4Label.Text = "0";
      this.Tweak4Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Tweak3Label
      // 
      this.Tweak3Label.AutoSize = true;
      this.Tweak3Label.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Tweak3Label.Location = new System.Drawing.Point(151, 256);
      this.Tweak3Label.Name = "Tweak3Label";
      this.Tweak3Label.Size = new System.Drawing.Size(142, 33);
      this.Tweak3Label.TabIndex = 16;
      this.Tweak3Label.Text = "0";
      this.Tweak3Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Tweak2Label
      // 
      this.Tweak2Label.AutoSize = true;
      this.Tweak2Label.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Tweak2Label.Location = new System.Drawing.Point(151, 223);
      this.Tweak2Label.Name = "Tweak2Label";
      this.Tweak2Label.Size = new System.Drawing.Size(142, 33);
      this.Tweak2Label.TabIndex = 15;
      this.Tweak2Label.Text = "0";
      this.Tweak2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // DebugPicking
      // 
      this.DebugPicking.AutoSize = true;
      this.DebugPicking.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DebugPicking.ForeColor = System.Drawing.Color.White;
      this.DebugPicking.Location = new System.Drawing.Point(151, 131);
      this.DebugPicking.Name = "DebugPicking";
      this.DebugPicking.Size = new System.Drawing.Size(142, 26);
      this.DebugPicking.TabIndex = 13;
      this.DebugPicking.Text = "Debug Picking";
      this.DebugPicking.UseVisualStyleBackColor = true;
      this.DebugPicking.CheckedChanged += new System.EventHandler(this.DebugPicking_CheckedChanged);
      // 
      // DebugDepthCheck
      // 
      this.DebugDepthCheck.AutoSize = true;
      this.DebugDepthCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DebugDepthCheck.ForeColor = System.Drawing.Color.White;
      this.DebugDepthCheck.Location = new System.Drawing.Point(151, 99);
      this.DebugDepthCheck.Name = "DebugDepthCheck";
      this.DebugDepthCheck.Size = new System.Drawing.Size(142, 26);
      this.DebugDepthCheck.TabIndex = 7;
      this.DebugDepthCheck.Text = "Debug Depth";
      this.DebugDepthCheck.UseVisualStyleBackColor = true;
      this.DebugDepthCheck.CheckedChanged += new System.EventHandler(this.DebugDepthCheck_CheckedChanged);
      // 
      // DebugPhysicsCheck
      // 
      this.DebugPhysicsCheck.AutoSize = true;
      this.DebugPhysicsCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DebugPhysicsCheck.ForeColor = System.Drawing.Color.White;
      this.DebugPhysicsCheck.Location = new System.Drawing.Point(151, 67);
      this.DebugPhysicsCheck.Name = "DebugPhysicsCheck";
      this.DebugPhysicsCheck.Size = new System.Drawing.Size(142, 26);
      this.DebugPhysicsCheck.TabIndex = 5;
      this.DebugPhysicsCheck.Text = "Debug Physics";
      this.DebugPhysicsCheck.UseVisualStyleBackColor = true;
      this.DebugPhysicsCheck.CheckedChanged += new System.EventHandler(this.DebugPhysicsCheck_CheckedChanged);
      // 
      // TerrainPhysicsCheck
      // 
      this.TerrainPhysicsCheck.AutoSize = true;
      this.TerrainPhysicsCheck.Checked = true;
      this.TerrainPhysicsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.TerrainPhysicsCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TerrainPhysicsCheck.ForeColor = System.Drawing.Color.White;
      this.TerrainPhysicsCheck.Location = new System.Drawing.Point(151, 35);
      this.TerrainPhysicsCheck.Name = "TerrainPhysicsCheck";
      this.TerrainPhysicsCheck.Size = new System.Drawing.Size(142, 26);
      this.TerrainPhysicsCheck.TabIndex = 3;
      this.TerrainPhysicsCheck.Text = "Terrain Physics";
      this.TerrainPhysicsCheck.UseVisualStyleBackColor = true;
      this.TerrainPhysicsCheck.CheckedChanged += new System.EventHandler(this.TerrainPhysicsCheck_CheckedChanged);
      // 
      // GravityCheck
      // 
      this.GravityCheck.AutoSize = true;
      this.GravityCheck.Checked = true;
      this.GravityCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.GravityCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GravityCheck.ForeColor = System.Drawing.Color.White;
      this.GravityCheck.Location = new System.Drawing.Point(151, 3);
      this.GravityCheck.Name = "GravityCheck";
      this.GravityCheck.Size = new System.Drawing.Size(142, 26);
      this.GravityCheck.TabIndex = 1;
      this.GravityCheck.Text = "Gravity";
      this.GravityCheck.UseVisualStyleBackColor = true;
      this.GravityCheck.CheckedChanged += new System.EventHandler(this.GravityCheck_CheckedChanged);
      // 
      // TweakBar1
      // 
      this.TweakBar1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TweakBar1.LargeChange = 10;
      this.TweakBar1.Location = new System.Drawing.Point(3, 195);
      this.TweakBar1.Maximum = 360;
      this.TweakBar1.Name = "TweakBar1";
      this.TweakBar1.Size = new System.Drawing.Size(142, 25);
      this.TweakBar1.TabIndex = 8;
      this.TweakBar1.TickFrequency = 10;
      this.TweakBar1.Scroll += new System.EventHandler(this.TweakBar1_Scroll);
      // 
      // TweakBar3
      // 
      this.TweakBar3.LargeChange = 10;
      this.TweakBar3.Location = new System.Drawing.Point(3, 259);
      this.TweakBar3.Maximum = 360;
      this.TweakBar3.Name = "TweakBar3";
      this.TweakBar3.Size = new System.Drawing.Size(142, 27);
      this.TweakBar3.TabIndex = 11;
      this.TweakBar3.TickFrequency = 10;
      this.TweakBar3.Scroll += new System.EventHandler(this.TweakBar3_Scroll);
      // 
      // TweakBar4
      // 
      this.TweakBar4.LargeChange = 10;
      this.TweakBar4.Location = new System.Drawing.Point(3, 292);
      this.TweakBar4.Maximum = 360;
      this.TweakBar4.Name = "TweakBar4";
      this.TweakBar4.Size = new System.Drawing.Size(142, 27);
      this.TweakBar4.TabIndex = 10;
      this.TweakBar4.TickFrequency = 10;
      this.TweakBar4.Scroll += new System.EventHandler(this.TweakBar4_Scroll);
      // 
      // TweakBar2
      // 
      this.TweakBar2.LargeChange = 10;
      this.TweakBar2.Location = new System.Drawing.Point(3, 226);
      this.TweakBar2.Maximum = 360;
      this.TweakBar2.Name = "TweakBar2";
      this.TweakBar2.Size = new System.Drawing.Size(142, 27);
      this.TweakBar2.TabIndex = 9;
      this.TweakBar2.TickFrequency = 10;
      this.TweakBar2.Scroll += new System.EventHandler(this.TweakBar2_Scroll);
      // 
      // Tweak1Label
      // 
      this.Tweak1Label.AutoSize = true;
      this.Tweak1Label.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Tweak1Label.Location = new System.Drawing.Point(151, 192);
      this.Tweak1Label.Name = "Tweak1Label";
      this.Tweak1Label.Size = new System.Drawing.Size(142, 31);
      this.Tweak1Label.TabIndex = 14;
      this.Tweak1Label.Text = "0";
      this.Tweak1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // DrawTerrains
      // 
      this.DrawTerrains.AutoSize = true;
      this.DrawTerrains.Checked = true;
      this.DrawTerrains.CheckState = System.Windows.Forms.CheckState.Checked;
      this.DrawTerrains.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DrawTerrains.ForeColor = System.Drawing.Color.White;
      this.DrawTerrains.Location = new System.Drawing.Point(3, 67);
      this.DrawTerrains.Name = "DrawTerrains";
      this.DrawTerrains.Size = new System.Drawing.Size(142, 26);
      this.DrawTerrains.TabIndex = 23;
      this.DrawTerrains.Text = "Terrains";
      this.DrawTerrains.UseVisualStyleBackColor = true;
      this.DrawTerrains.CheckedChanged += new System.EventHandler(this.DrawTerrains_CheckedChanged);
      // 
      // SettingsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(300, 458);
      this.Controls.Add(this.tableLayoutPanel2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "SettingsForm";
      this.Text = "Settings";
      this.Load += new System.EventHandler(this.SettingsForm_Load);
      this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TweakBar2)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.CheckBox GravityCheck;
    private System.Windows.Forms.CheckBox TerrainPhysicsCheck;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.CheckBox DebugPhysicsCheck;
    private System.Windows.Forms.CheckBox DebugDepthCheck;
    private System.Windows.Forms.TrackBar TweakBar1;
    private System.Windows.Forms.TrackBar TweakBar2;
    private System.Windows.Forms.TrackBar TweakBar3;
    private System.Windows.Forms.TrackBar TweakBar4;
    private System.Windows.Forms.CheckBox DebugPicking;
    private System.Windows.Forms.Label Tweak4Label;
    private System.Windows.Forms.Label Tweak3Label;
    private System.Windows.Forms.Label Tweak2Label;
    private System.Windows.Forms.Label Tweak1Label;
    private System.Windows.Forms.CheckBox FrustrumCullingCheck;
    private System.Windows.Forms.CheckBox DistanceClippingcheck;
    private System.Windows.Forms.CheckBox DrawTreesCheck;
    private System.Windows.Forms.CheckBox DrawGrassCheck;
    private System.Windows.Forms.CheckBox DrawTerrains;
  }
}