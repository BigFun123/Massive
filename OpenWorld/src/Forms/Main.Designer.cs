namespace OpenWorld
{
  partial class Main
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.connectControl1 = new OpenWorld.src.Controls.ConnectControl();
      this.navBarControl1 = new OpenWorld.Controls.NavBarControl();
      this.positionControl1 = new OpenWorld.src.Controls.StatusControl();
      this.toolbarControl1 = new OpenWorld.Controls.ToolbarControl();
      this.bookmarksControl1 = new OpenWorld.src.Controls.BookmarksControl();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
      this.tableLayoutPanel1.Controls.Add(this.connectControl1, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.navBarControl1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.positionControl1, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.toolbarControl1, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.bookmarksControl1, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1000, 654);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // timer1
      // 
      this.timer1.Interval = 20;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // connectControl1
      // 
      this.connectControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.connectControl1.Location = new System.Drawing.Point(958, 0);
      this.connectControl1.Margin = new System.Windows.Forms.Padding(0);
      this.connectControl1.Name = "connectControl1";
      this.connectControl1.Size = new System.Drawing.Size(42, 32);
      this.connectControl1.TabIndex = 0;
      // 
      // navBarControl1
      // 
      this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.navBarControl1.Location = new System.Drawing.Point(0, 0);
      this.navBarControl1.Margin = new System.Windows.Forms.Padding(0);
      this.navBarControl1.Name = "navBarControl1";
      this.navBarControl1.Size = new System.Drawing.Size(958, 32);
      this.navBarControl1.TabIndex = 1;
      // 
      // positionControl1
      // 
      this.positionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.positionControl1.Location = new System.Drawing.Point(1, 607);
      this.positionControl1.Margin = new System.Windows.Forms.Padding(1);
      this.positionControl1.Name = "positionControl1";
      this.positionControl1.Size = new System.Drawing.Size(956, 46);
      this.positionControl1.TabIndex = 2;
      // 
      // toolbarControl1
      // 
      this.toolbarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolbarControl1.Location = new System.Drawing.Point(961, 67);
      this.toolbarControl1.Name = "toolbarControl1";
      this.toolbarControl1.Size = new System.Drawing.Size(36, 536);
      this.toolbarControl1.TabIndex = 3;
      // 
      // bookmarksControl1
      // 
      this.bookmarksControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.bookmarksControl1.Location = new System.Drawing.Point(3, 35);
      this.bookmarksControl1.Name = "bookmarksControl1";
      this.bookmarksControl1.Size = new System.Drawing.Size(952, 26);
      this.bookmarksControl1.TabIndex = 4;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1000, 654);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "Main";
      this.Text = "Open World | bigfun.co.za";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.Shown += new System.EventHandler(this.Main_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
      this.Move += new System.EventHandler(this.Main_Move);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private src.Controls.ConnectControl connectControl1;
    private System.Windows.Forms.Timer timer1;
    private Controls.NavBarControl navBarControl1;
    private src.Controls.StatusControl positionControl1;
    private Controls.ToolbarControl toolbarControl1;
    private src.Controls.BookmarksControl bookmarksControl1;
  }
}

