namespace OpenWorld.Controls
{
  partial class ToolbarControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolbarControl));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.ModeButton = new System.Windows.Forms.Button();
      this.DebugButton = new System.Windows.Forms.Button();
      this.MapButton = new System.Windows.Forms.Button();
      this.HelpButton = new System.Windows.Forms.Button();
      this.AssetsButton = new System.Windows.Forms.Button();
      this.SettingsButton = new System.Windows.Forms.Button();
      this.BuildButton = new System.Windows.Forms.Button();
      this.ChatButton = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.ShaderEditorButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.ShaderEditorButton, 0, 6);
      this.tableLayoutPanel1.Controls.Add(this.ModeButton, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.DebugButton, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.MapButton, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.HelpButton, 0, 10);
      this.tableLayoutPanel1.Controls.Add(this.AssetsButton, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.SettingsButton, 0, 9);
      this.tableLayoutPanel1.Controls.Add(this.BuildButton, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.ChatButton, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 11;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(38, 388);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // ModeButton
      // 
      this.ModeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ModeButton.BackgroundImage")));
      this.ModeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.ModeButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ModeButton.Location = new System.Drawing.Point(0, 128);
      this.ModeButton.Margin = new System.Windows.Forms.Padding(0);
      this.ModeButton.Name = "ModeButton";
      this.ModeButton.Size = new System.Drawing.Size(38, 32);
      this.ModeButton.TabIndex = 7;
      this.toolTip1.SetToolTip(this.ModeButton, "Chat with others");
      this.ModeButton.UseVisualStyleBackColor = true;
      this.ModeButton.Click += new System.EventHandler(this.ModeButton_Click);
      // 
      // DebugButton
      // 
      this.DebugButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DebugButton.BackgroundImage")));
      this.DebugButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.DebugButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DebugButton.Location = new System.Drawing.Point(0, 160);
      this.DebugButton.Margin = new System.Windows.Forms.Padding(0);
      this.DebugButton.Name = "DebugButton";
      this.DebugButton.Size = new System.Drawing.Size(38, 32);
      this.DebugButton.TabIndex = 6;
      this.toolTip1.SetToolTip(this.DebugButton, "Chat with others");
      this.DebugButton.UseVisualStyleBackColor = true;
      this.DebugButton.Click += new System.EventHandler(this.DebugButton_Click);
      // 
      // MapButton
      // 
      this.MapButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MapButton.BackgroundImage")));
      this.MapButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.MapButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MapButton.Location = new System.Drawing.Point(0, 96);
      this.MapButton.Margin = new System.Windows.Forms.Padding(0);
      this.MapButton.Name = "MapButton";
      this.MapButton.Size = new System.Drawing.Size(38, 32);
      this.MapButton.TabIndex = 5;
      this.toolTip1.SetToolTip(this.MapButton, "Chat with others");
      this.MapButton.UseVisualStyleBackColor = true;
      this.MapButton.Click += new System.EventHandler(this.MapButton_Click);
      // 
      // HelpButton
      // 
      this.HelpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HelpButton.BackgroundImage")));
      this.HelpButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.HelpButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.HelpButton.Location = new System.Drawing.Point(0, 356);
      this.HelpButton.Margin = new System.Windows.Forms.Padding(0);
      this.HelpButton.Name = "HelpButton";
      this.HelpButton.Size = new System.Drawing.Size(38, 32);
      this.HelpButton.TabIndex = 4;
      this.toolTip1.SetToolTip(this.HelpButton, "Chat with others");
      this.HelpButton.UseVisualStyleBackColor = true;
      this.HelpButton.Click += new System.EventHandler(this.HelpButton_Click);
      // 
      // AssetsButton
      // 
      this.AssetsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AssetsButton.BackgroundImage")));
      this.AssetsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.AssetsButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.AssetsButton.Location = new System.Drawing.Point(0, 64);
      this.AssetsButton.Margin = new System.Windows.Forms.Padding(0);
      this.AssetsButton.Name = "AssetsButton";
      this.AssetsButton.Size = new System.Drawing.Size(38, 32);
      this.AssetsButton.TabIndex = 3;
      this.toolTip1.SetToolTip(this.AssetsButton, "Chat with others");
      this.AssetsButton.UseVisualStyleBackColor = true;
      this.AssetsButton.Click += new System.EventHandler(this.AssetsButton_Click);
      // 
      // SettingsButton
      // 
      this.SettingsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SettingsButton.BackgroundImage")));
      this.SettingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.SettingsButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SettingsButton.Location = new System.Drawing.Point(0, 324);
      this.SettingsButton.Margin = new System.Windows.Forms.Padding(0);
      this.SettingsButton.Name = "SettingsButton";
      this.SettingsButton.Size = new System.Drawing.Size(38, 32);
      this.SettingsButton.TabIndex = 2;
      this.toolTip1.SetToolTip(this.SettingsButton, "Chat with others");
      this.SettingsButton.UseVisualStyleBackColor = true;
      this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
      // 
      // BuildButton
      // 
      this.BuildButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BuildButton.BackgroundImage")));
      this.BuildButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.BuildButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.BuildButton.Location = new System.Drawing.Point(0, 32);
      this.BuildButton.Margin = new System.Windows.Forms.Padding(0);
      this.BuildButton.Name = "BuildButton";
      this.BuildButton.Size = new System.Drawing.Size(38, 32);
      this.BuildButton.TabIndex = 1;
      this.toolTip1.SetToolTip(this.BuildButton, "Chat with others");
      this.BuildButton.UseVisualStyleBackColor = true;
      this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
      // 
      // ChatButton
      // 
      this.ChatButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ChatButton.BackgroundImage")));
      this.ChatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.ChatButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ChatButton.Location = new System.Drawing.Point(0, 0);
      this.ChatButton.Margin = new System.Windows.Forms.Padding(0);
      this.ChatButton.Name = "ChatButton";
      this.ChatButton.Size = new System.Drawing.Size(38, 32);
      this.ChatButton.TabIndex = 0;
      this.toolTip1.SetToolTip(this.ChatButton, "Chat with others");
      this.ChatButton.UseVisualStyleBackColor = true;
      this.ChatButton.Click += new System.EventHandler(this.ChatButton_Click);
      // 
      // ShaderEditorButton
      // 
      this.ShaderEditorButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ShaderEditorButton.BackgroundImage")));
      this.ShaderEditorButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.ShaderEditorButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ShaderEditorButton.Location = new System.Drawing.Point(0, 192);
      this.ShaderEditorButton.Margin = new System.Windows.Forms.Padding(0);
      this.ShaderEditorButton.Name = "ShaderEditorButton";
      this.ShaderEditorButton.Size = new System.Drawing.Size(38, 32);
      this.ShaderEditorButton.TabIndex = 8;
      this.toolTip1.SetToolTip(this.ShaderEditorButton, "Chat with others");
      this.ShaderEditorButton.UseVisualStyleBackColor = true;
      this.ShaderEditorButton.Click += new System.EventHandler(this.ShaderEditorButton_Click);
      // 
      // ToolbarControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ToolbarControl";
      this.Size = new System.Drawing.Size(38, 388);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button ChatButton;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Button BuildButton;
    private System.Windows.Forms.Button SettingsButton;
    private System.Windows.Forms.Button AssetsButton;
    private System.Windows.Forms.Button HelpButton;
    private System.Windows.Forms.Button MapButton;
    private System.Windows.Forms.Button DebugButton;
    private System.Windows.Forms.Button ModeButton;
    private System.Windows.Forms.Button ShaderEditorButton;
  }
}
