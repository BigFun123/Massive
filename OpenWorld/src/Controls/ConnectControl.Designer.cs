namespace OpenWorld.src.Controls
{
  partial class ConnectControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectControl));
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.ConnectedCheck = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "connected.png");
      this.imageList1.Images.SetKeyName(1, "disconnected.png");
      this.imageList1.Images.SetKeyName(2, "disconnectederror.png");
      // 
      // ConnectedCheck
      // 
      this.ConnectedCheck.Appearance = System.Windows.Forms.Appearance.Button;
      this.ConnectedCheck.AutoSize = true;
      this.ConnectedCheck.BackColor = System.Drawing.Color.Transparent;
      this.ConnectedCheck.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ConnectedCheck.ImageIndex = 1;
      this.ConnectedCheck.ImageList = this.imageList1;
      this.ConnectedCheck.Location = new System.Drawing.Point(0, 0);
      this.ConnectedCheck.Name = "ConnectedCheck";
      this.ConnectedCheck.Size = new System.Drawing.Size(42, 39);
      this.ConnectedCheck.TabIndex = 0;
      this.ConnectedCheck.UseVisualStyleBackColor = false;
      this.ConnectedCheck.Click += new System.EventHandler(this.ConnectedCheck_Click);
      // 
      // ConnectControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.ConnectedCheck);
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "ConnectControl";
      this.Size = new System.Drawing.Size(42, 39);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.CheckBox ConnectedCheck;
  }
}
