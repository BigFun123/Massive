namespace MassiveGenerator
{
  partial class Form1
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
      this.DownloadButton = new System.Windows.Forms.Button();
      this.RGBBox = new System.Windows.Forms.PictureBox();
      this.DestBox = new System.Windows.Forms.PictureBox();
      this.Zoom = new System.Windows.Forms.TextBox();
      this.xpos = new System.Windows.Forms.TextBox();
      this.ypos = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.Doanload3x3 = new System.Windows.Forms.Button();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.Download6x6 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.Download12x12 = new System.Windows.Forms.Button();
      this.BiomeButton = new System.Windows.Forms.Button();
      this.Continuity = new System.Windows.Forms.Button();
      this.mapTileInspector1 = new MassiveGenerator.Controls.MapTileInspector();
      ((System.ComponentModel.ISupportInitialize)(this.RGBBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DestBox)).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // DownloadButton
      // 
      this.DownloadButton.Location = new System.Drawing.Point(3, 3);
      this.DownloadButton.Name = "DownloadButton";
      this.DownloadButton.Size = new System.Drawing.Size(75, 38);
      this.DownloadButton.TabIndex = 0;
      this.DownloadButton.Text = "Download";
      this.DownloadButton.UseVisualStyleBackColor = true;
      this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
      // 
      // RGBBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.RGBBox, 2);
      this.RGBBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.RGBBox.Location = new System.Drawing.Point(3, 82);
      this.RGBBox.Name = "RGBBox";
      this.RGBBox.Size = new System.Drawing.Size(297, 360);
      this.RGBBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.RGBBox.TabIndex = 1;
      this.RGBBox.TabStop = false;
      // 
      // DestBox
      // 
      this.DestBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DestBox.Location = new System.Drawing.Point(306, 82);
      this.DestBox.Name = "DestBox";
      this.DestBox.Size = new System.Drawing.Size(253, 360);
      this.DestBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.DestBox.TabIndex = 2;
      this.DestBox.TabStop = false;
      // 
      // Zoom
      // 
      this.Zoom.Location = new System.Drawing.Point(3, 50);
      this.Zoom.Name = "Zoom";
      this.Zoom.Size = new System.Drawing.Size(44, 20);
      this.Zoom.TabIndex = 3;
      this.Zoom.Text = "14";
      // 
      // xpos
      // 
      this.xpos.Location = new System.Drawing.Point(306, 50);
      this.xpos.Name = "xpos";
      this.xpos.Size = new System.Drawing.Size(100, 20);
      this.xpos.TabIndex = 4;
      this.xpos.Text = "9029";
      // 
      // ypos
      // 
      this.ypos.Location = new System.Drawing.Point(565, 50);
      this.ypos.Name = "ypos";
      this.ypos.Size = new System.Drawing.Size(100, 20);
      this.ypos.TabIndex = 5;
      this.ypos.Text = "9833";
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(306, 448);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 32);
      this.button1.TabIndex = 6;
      this.button1.Text = "Download";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // Doanload3x3
      // 
      this.Doanload3x3.Location = new System.Drawing.Point(306, 3);
      this.Doanload3x3.Name = "Doanload3x3";
      this.Doanload3x3.Size = new System.Drawing.Size(75, 38);
      this.Doanload3x3.TabIndex = 7;
      this.Doanload3x3.Text = "Download 3x3";
      this.Doanload3x3.UseVisualStyleBackColor = true;
      this.Doanload3x3.Click += new System.EventHandler(this.Doanload3x3_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
      this.statusStrip1.Location = new System.Drawing.Point(0, 483);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(1106, 22);
      this.statusStrip1.TabIndex = 8;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // StatusLabel
      // 
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(15, 17);
      this.StatusLabel.Text = ">";
      // 
      // Download6x6
      // 
      this.Download6x6.Location = new System.Drawing.Point(565, 3);
      this.Download6x6.Name = "Download6x6";
      this.Download6x6.Size = new System.Drawing.Size(75, 38);
      this.Download6x6.TabIndex = 9;
      this.Download6x6.Text = "Download 6x6";
      this.Download6x6.UseVisualStyleBackColor = true;
      this.Download6x6.Click += new System.EventHandler(this.Download6x6_Click);
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(690, 50);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 26);
      this.button3.TabIndex = 10;
      this.button3.Text = "GetMeta";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 7;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 259F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.Download12x12, 4, 0);
      this.tableLayoutPanel1.Controls.Add(this.BiomeButton, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.Continuity, 5, 0);
      this.tableLayoutPanel1.Controls.Add(this.RGBBox, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.button1, 2, 3);
      this.tableLayoutPanel1.Controls.Add(this.DestBox, 2, 2);
      this.tableLayoutPanel1.Controls.Add(this.Download6x6, 3, 0);
      this.tableLayoutPanel1.Controls.Add(this.Doanload3x3, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.ypos, 3, 1);
      this.tableLayoutPanel1.Controls.Add(this.DownloadButton, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.xpos, 2, 1);
      this.tableLayoutPanel1.Controls.Add(this.Zoom, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.mapTileInspector1, 3, 2);
      this.tableLayoutPanel1.Controls.Add(this.button3, 4, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.48325F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.51675F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1106, 483);
      this.tableLayoutPanel1.TabIndex = 11;
      // 
      // Download12x12
      // 
      this.Download12x12.Location = new System.Drawing.Point(690, 3);
      this.Download12x12.Name = "Download12x12";
      this.Download12x12.Size = new System.Drawing.Size(75, 38);
      this.Download12x12.TabIndex = 14;
      this.Download12x12.Text = "Download 12x12";
      this.Download12x12.UseVisualStyleBackColor = true;
      this.Download12x12.Click += new System.EventHandler(this.Download12x12_Click);
      // 
      // BiomeButton
      // 
      this.BiomeButton.Location = new System.Drawing.Point(161, 3);
      this.BiomeButton.Name = "BiomeButton";
      this.BiomeButton.Size = new System.Drawing.Size(75, 38);
      this.BiomeButton.TabIndex = 13;
      this.BiomeButton.Text = "Biome";
      this.BiomeButton.UseVisualStyleBackColor = true;
      this.BiomeButton.Click += new System.EventHandler(this.BiomeButton_Click);
      // 
      // Continuity
      // 
      this.Continuity.Location = new System.Drawing.Point(846, 3);
      this.Continuity.Name = "Continuity";
      this.Continuity.Size = new System.Drawing.Size(75, 38);
      this.Continuity.TabIndex = 12;
      this.Continuity.Text = "Continuity";
      this.Continuity.UseVisualStyleBackColor = true;
      this.Continuity.Click += new System.EventHandler(this.Continuity_Click);
      // 
      // mapTileInspector1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.mapTileInspector1, 2);
      this.mapTileInspector1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mapTileInspector1.Location = new System.Drawing.Point(565, 82);
      this.mapTileInspector1.Name = "mapTileInspector1";
      this.mapTileInspector1.Size = new System.Drawing.Size(275, 360);
      this.mapTileInspector1.TabIndex = 11;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1106, 505);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.statusStrip1);
      this.Name = "Form1";
      this.Text = "_MASSIVE Generatr";
      ((System.ComponentModel.ISupportInitialize)(this.RGBBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DestBox)).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button DownloadButton;
    private System.Windows.Forms.PictureBox RGBBox;
    private System.Windows.Forms.PictureBox DestBox;
    private System.Windows.Forms.TextBox Zoom;
    private System.Windows.Forms.TextBox xpos;
    private System.Windows.Forms.TextBox ypos;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button Doanload3x3;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    private System.Windows.Forms.Button Download6x6;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private Controls.MapTileInspector mapTileInspector1;
    private System.Windows.Forms.Button Continuity;
    private System.Windows.Forms.Button BiomeButton;
    private System.Windows.Forms.Button Download12x12;
  }
}

