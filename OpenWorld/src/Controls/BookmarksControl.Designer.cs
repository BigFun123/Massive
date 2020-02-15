namespace OpenWorld.src.Controls
{
  partial class BookmarksControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookmarksControl));
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.RenameTextBox = new System.Windows.Forms.ToolStripTextBox();
      this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.setGPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(606, 34);
      this.flowLayoutPanel1.TabIndex = 0;
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "star32.png");
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.setGPSToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
      this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
      this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
      // 
      // renameToolStripMenuItem
      // 
      this.renameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RenameTextBox});
      this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
      this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.renameToolStripMenuItem.Text = "Rename";
      this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
      // 
      // RenameTextBox
      // 
      this.RenameTextBox.BackColor = System.Drawing.Color.White;
      this.RenameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.RenameTextBox.Name = "RenameTextBox";
      this.RenameTextBox.Size = new System.Drawing.Size(100, 23);
      this.RenameTextBox.Leave += new System.EventHandler(this.RenameTextBox_Leave);
      this.RenameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenameTextBox_KeyDown);
      this.RenameTextBox.TextChanged += new System.EventHandler(this.RenameTextBox_TextChanged);
      // 
      // deleteToolStripMenuItem
      // 
      this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
      this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.deleteToolStripMenuItem.Text = "Delete";
      this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
      // 
      // setGPSToolStripMenuItem
      // 
      this.setGPSToolStripMenuItem.Name = "setGPSToolStripMenuItem";
      this.setGPSToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.setGPSToolStripMenuItem.Text = "Set GPS";
      this.setGPSToolStripMenuItem.Click += new System.EventHandler(this.setGPSToolStripMenuItem_Click);
      // 
      // BookmarksControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.flowLayoutPanel1);
      this.Name = "BookmarksControl";
      this.Size = new System.Drawing.Size(606, 34);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    private System.Windows.Forms.ToolStripTextBox RenameTextBox;
    private System.Windows.Forms.ToolStripMenuItem setGPSToolStripMenuItem;
  }
}
