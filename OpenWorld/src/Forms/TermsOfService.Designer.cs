namespace OpenWorld.Forms
{
  partial class TermsOfService
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TermsOfService));
      this.TermsTextBox = new System.Windows.Forms.TextBox();
      this.AcceptButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // TermsTextBox
      // 
      this.TermsTextBox.Location = new System.Drawing.Point(12, 12);
      this.TermsTextBox.Multiline = true;
      this.TermsTextBox.Name = "TermsTextBox";
      this.TermsTextBox.ReadOnly = true;
      this.TermsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TermsTextBox.Size = new System.Drawing.Size(685, 394);
      this.TermsTextBox.TabIndex = 0;
      this.TermsTextBox.TabStop = false;
      this.TermsTextBox.Text = resources.GetString("TermsTextBox.Text");
      // 
      // AcceptButton
      // 
      this.AcceptButton.Location = new System.Drawing.Point(309, 422);
      this.AcceptButton.Name = "AcceptButton";
      this.AcceptButton.Size = new System.Drawing.Size(75, 44);
      this.AcceptButton.TabIndex = 1;
      this.AcceptButton.Text = "I Accept";
      this.AcceptButton.UseVisualStyleBackColor = true;
      this.AcceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
      // 
      // TermsOfService
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(709, 492);
      this.Controls.Add(this.AcceptButton);
      this.Controls.Add(this.TermsTextBox);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "TermsOfService";
      this.Text = "Terms and Conditions";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox TermsTextBox;
    private System.Windows.Forms.Button AcceptButton;
  }
}