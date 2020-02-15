namespace OpenWorld.Forms
{
  partial class InfoOverlayForm
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
      this.UserInfo = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // UserInfo
      // 
      this.UserInfo.BackColor = System.Drawing.Color.Black;
      this.UserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UserInfo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.UserInfo.ForeColor = System.Drawing.Color.White;
      this.UserInfo.Location = new System.Drawing.Point(0, 0);
      this.UserInfo.Name = "UserInfo";
      this.UserInfo.Size = new System.Drawing.Size(320, 56);
      this.UserInfo.TabIndex = 0;
      this.UserInfo.Text = ">OPEN WORLD\r\nClick CONNECT Button to open lobby";
      // 
      // timer1
      // 
      this.timer1.Interval = 1000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // InfoOverlayForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
      this.ClientSize = new System.Drawing.Size(320, 56);
      this.Controls.Add(this.UserInfo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "InfoOverlayForm";
      this.Opacity = 0.5D;
      this.ShowInTaskbar = false;
      this.Text = "InfoOverlayForm";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label UserInfo;
    private System.Windows.Forms.Timer timer1;
  }
}