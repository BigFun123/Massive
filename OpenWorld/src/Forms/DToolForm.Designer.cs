namespace OpenWorld.Forms
{
  partial class DToolForm
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.TitleBar = new System.Windows.Forms.Label();
      this.CloseButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
      this.tableLayoutPanel1.Controls.Add(this.TitleBar, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.CloseButton, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 21);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // TitleBar
      // 
      this.TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
      this.tableLayoutPanel1.SetColumnSpan(this.TitleBar, 2);
      this.TitleBar.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TitleBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TitleBar.ForeColor = System.Drawing.Color.White;
      this.TitleBar.Location = new System.Drawing.Point(0, 0);
      this.TitleBar.Margin = new System.Windows.Forms.Padding(0);
      this.TitleBar.Name = "TitleBar";
      this.TitleBar.Padding = new System.Windows.Forms.Padding(2);
      this.TitleBar.Size = new System.Drawing.Size(234, 21);
      this.TitleBar.TabIndex = 0;
      this.TitleBar.Text = "TITLE";
      this.TitleBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
      this.TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseMove);
      this.TitleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseUp);
      // 
      // CloseButton
      // 
      this.CloseButton.BackColor = System.Drawing.Color.Transparent;
      this.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CloseButton.FlatAppearance.BorderSize = 0;
      this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CloseButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.CloseButton.Location = new System.Drawing.Point(234, 0);
      this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(22, 21);
      this.CloseButton.TabIndex = 1;
      this.CloseButton.Text = "x";
      this.CloseButton.UseVisualStyleBackColor = false;
      this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
      // 
      // DToolForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(260, 513);
      this.Controls.Add(this.tableLayoutPanel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "DToolForm";
      this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
      this.ShowInTaskbar = false;
      this.Text = "DToolForm";
      this.TopMost = true;
      this.Shown += new System.EventHandler(this.DToolForm_Shown);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DToolForm_MouseDown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label TitleBar;
    private System.Windows.Forms.Button CloseButton;
  }
}