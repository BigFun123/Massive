namespace OpenWorld.Forms
{
  partial class MapForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.DistanceVal = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.AstroBodies = new System.Windows.Forms.ComboBox();
      this.LonLatBox = new System.Windows.Forms.TextBox();
      this.GoLatLon = new System.Windows.Forms.Button();
      this.WorldCoords = new System.Windows.Forms.TextBox();
      this.GotoMapPointButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.MapBox = new System.Windows.Forms.Panel();
      this.Navigate = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DistanceVal)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 8;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
      this.tableLayoutPanel1.Controls.Add(this.DistanceVal, 5, 2);
      this.tableLayoutPanel1.Controls.Add(this.label1, 4, 2);
      this.tableLayoutPanel1.Controls.Add(this.AstroBodies, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.LonLatBox, 4, 0);
      this.tableLayoutPanel1.Controls.Add(this.GoLatLon, 7, 0);
      this.tableLayoutPanel1.Controls.Add(this.WorldCoords, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.GotoMapPointButton, 8, 2);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.MapBox, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.Navigate, 6, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(906, 597);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // DistanceVal
      // 
      this.DistanceVal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.DistanceVal.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.DistanceVal.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DistanceVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DistanceVal.ForeColor = System.Drawing.Color.White;
      this.DistanceVal.Location = new System.Drawing.Point(494, 551);
      this.DistanceVal.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
      this.DistanceVal.Name = "DistanceVal";
      this.DistanceVal.Size = new System.Drawing.Size(74, 20);
      this.DistanceVal.TabIndex = 12;
      this.DistanceVal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(344, 548);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(144, 49);
      this.label1.TabIndex = 11;
      this.label1.Text = "Distance from surface";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // AstroBodies
      // 
      this.AstroBodies.BackColor = System.Drawing.Color.Silver;
      this.tableLayoutPanel1.SetColumnSpan(this.AstroBodies, 2);
      this.AstroBodies.Dock = System.Windows.Forms.DockStyle.Fill;
      this.AstroBodies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.AstroBodies.FormattingEnabled = true;
      this.AstroBodies.Location = new System.Drawing.Point(74, 3);
      this.AstroBodies.Name = "AstroBodies";
      this.AstroBodies.Size = new System.Drawing.Size(264, 21);
      this.AstroBodies.TabIndex = 8;
      this.AstroBodies.SelectedIndexChanged += new System.EventHandler(this.AstroBodies_SelectedIndexChanged);
      // 
      // LonLatBox
      // 
      this.LonLatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tableLayoutPanel1.SetColumnSpan(this.LonLatBox, 2);
      this.LonLatBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LonLatBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.LonLatBox.Location = new System.Drawing.Point(341, 0);
      this.LonLatBox.Margin = new System.Windows.Forms.Padding(0);
      this.LonLatBox.Multiline = true;
      this.LonLatBox.Name = "LonLatBox";
      this.LonLatBox.Size = new System.Drawing.Size(230, 32);
      this.LonLatBox.TabIndex = 6;
      this.LonLatBox.Text = "18.4241,-33.9249";
      this.toolTip1.SetToolTip(this.LonLatBox, "Longitude,Latitude");
      this.LonLatBox.Visible = false;
      // 
      // GoLatLon
      // 
      this.GoLatLon.BackColor = System.Drawing.Color.Silver;
      this.GoLatLon.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GoLatLon.FlatAppearance.BorderSize = 0;
      this.GoLatLon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.GoLatLon.Location = new System.Drawing.Point(691, 0);
      this.GoLatLon.Margin = new System.Windows.Forms.Padding(0);
      this.GoLatLon.Name = "GoLatLon";
      this.GoLatLon.Size = new System.Drawing.Size(215, 32);
      this.GoLatLon.TabIndex = 5;
      this.GoLatLon.Text = "Teleport";
      this.GoLatLon.UseVisualStyleBackColor = false;
      this.GoLatLon.Visible = false;
      this.GoLatLon.Click += new System.EventHandler(this.GoLatLon_Click);
      // 
      // WorldCoords
      // 
      this.WorldCoords.BackColor = System.Drawing.Color.Silver;
      this.WorldCoords.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tableLayoutPanel1.SetColumnSpan(this.WorldCoords, 4);
      this.WorldCoords.Dock = System.Windows.Forms.DockStyle.Fill;
      this.WorldCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.WorldCoords.Location = new System.Drawing.Point(0, 548);
      this.WorldCoords.Margin = new System.Windows.Forms.Padding(0);
      this.WorldCoords.Multiline = true;
      this.WorldCoords.Name = "WorldCoords";
      this.WorldCoords.Size = new System.Drawing.Size(341, 49);
      this.WorldCoords.TabIndex = 4;
      this.WorldCoords.Text = "1\r\n2";
      // 
      // GotoMapPointButton
      // 
      this.GotoMapPointButton.BackColor = System.Drawing.Color.LightCoral;
      this.GotoMapPointButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GotoMapPointButton.FlatAppearance.BorderSize = 0;
      this.GotoMapPointButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.GotoMapPointButton.Location = new System.Drawing.Point(691, 548);
      this.GotoMapPointButton.Margin = new System.Windows.Forms.Padding(0);
      this.GotoMapPointButton.Name = "GotoMapPointButton";
      this.GotoMapPointButton.Size = new System.Drawing.Size(215, 49);
      this.GotoMapPointButton.TabIndex = 3;
      this.GotoMapPointButton.Text = "Go To Map Point";
      this.toolTip1.SetToolTip(this.GotoMapPointButton, "Teleport to the Selected map point");
      this.GotoMapPointButton.UseVisualStyleBackColor = false;
      this.GotoMapPointButton.Click += new System.EventHandler(this.GotoMapPointButton_Click);
      // 
      // label2
      // 
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.ImageIndex = 0;
      this.label2.ImageList = this.imageList1;
      this.label2.Location = new System.Drawing.Point(3, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(33, 32);
      this.label2.TabIndex = 13;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "icons8-globe-80.png");
      // 
      // MapBox
      // 
      this.MapBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.tableLayoutPanel1.SetColumnSpan(this.MapBox, 8);
      this.MapBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MapBox.Location = new System.Drawing.Point(0, 32);
      this.MapBox.Margin = new System.Windows.Forms.Padding(0);
      this.MapBox.Name = "MapBox";
      this.MapBox.Size = new System.Drawing.Size(906, 516);
      this.MapBox.TabIndex = 14;
      this.MapBox.Paint += new System.Windows.Forms.PaintEventHandler(this.MapBox_Paint);
      this.MapBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MapBox_MouseClick);
      this.MapBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapBox_MouseMove);
      // 
      // Navigate
      // 
      this.Navigate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.Navigate.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Navigate.FlatAppearance.BorderSize = 0;
      this.Navigate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.Navigate.Location = new System.Drawing.Point(571, 0);
      this.Navigate.Margin = new System.Windows.Forms.Padding(0);
      this.Navigate.Name = "Navigate";
      this.Navigate.Size = new System.Drawing.Size(120, 32);
      this.Navigate.TabIndex = 15;
      this.Navigate.Text = "Set Navigator";
      this.toolTip1.SetToolTip(this.Navigate, "Set the 3D Nagiator to point to the Longitude,Latitude");
      this.Navigate.UseVisualStyleBackColor = false;
      this.Navigate.Visible = false;
      this.Navigate.Click += new System.EventHandler(this.Navigate_Click);
      // 
      // MapForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Gray;
      this.ClientSize = new System.Drawing.Size(906, 597);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MapForm";
      this.ShowIcon = false;
      this.Text = "Map";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DistanceVal)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TextBox WorldCoords;
    private System.Windows.Forms.Button GotoMapPointButton;
    private System.Windows.Forms.TextBox LonLatBox;
    private System.Windows.Forms.Button GoLatLon;
    private System.Windows.Forms.ComboBox AstroBodies;
    private System.Windows.Forms.NumericUpDown DistanceVal;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Panel MapBox;
    private System.Windows.Forms.Button Navigate;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}