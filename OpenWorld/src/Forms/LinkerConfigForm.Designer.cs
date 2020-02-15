namespace OpenWorld.Forms
{
  partial class LinkerConfigForm
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
      this.label1 = new System.Windows.Forms.Label();
      this.LinkBox = new System.Windows.Forms.TextBox();
      this.UpdateButton = new System.Windows.Forms.Button();
      this.StatusLabel = new System.Windows.Forms.Label();
      this.TestButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(21, 50);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(27, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Link";
      // 
      // LinkBox
      // 
      this.LinkBox.Location = new System.Drawing.Point(24, 79);
      this.LinkBox.Name = "LinkBox";
      this.LinkBox.Size = new System.Drawing.Size(450, 20);
      this.LinkBox.TabIndex = 3;
      // 
      // UpdateButton
      // 
      this.UpdateButton.Location = new System.Drawing.Point(399, 144);
      this.UpdateButton.Name = "UpdateButton";
      this.UpdateButton.Size = new System.Drawing.Size(75, 41);
      this.UpdateButton.TabIndex = 4;
      this.UpdateButton.Text = "Update";
      this.UpdateButton.UseVisualStyleBackColor = true;
      this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
      // 
      // StatusLabel
      // 
      this.StatusLabel.AutoSize = true;
      this.StatusLabel.ForeColor = System.Drawing.Color.White;
      this.StatusLabel.Location = new System.Drawing.Point(21, 144);
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new System.Drawing.Size(16, 13);
      this.StatusLabel.TabIndex = 5;
      this.StatusLabel.Text = "...";
      // 
      // TestButton
      // 
      this.TestButton.Location = new System.Drawing.Point(303, 144);
      this.TestButton.Name = "TestButton";
      this.TestButton.Size = new System.Drawing.Size(75, 41);
      this.TestButton.TabIndex = 6;
      this.TestButton.Text = "Test";
      this.TestButton.UseVisualStyleBackColor = true;
      this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
      // 
      // LinkerConfigForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(498, 214);
      this.Controls.Add(this.TestButton);
      this.Controls.Add(this.StatusLabel);
      this.Controls.Add(this.UpdateButton);
      this.Controls.Add(this.LinkBox);
      this.Controls.Add(this.label1);
      this.Name = "LinkerConfigForm";
      this.Text = "LinkerConfigForm";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.LinkBox, 0);
      this.Controls.SetChildIndex(this.UpdateButton, 0);
      this.Controls.SetChildIndex(this.StatusLabel, 0);
      this.Controls.SetChildIndex(this.TestButton, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox LinkBox;
    private System.Windows.Forms.Button UpdateButton;
    private System.Windows.Forms.Label StatusLabel;
    private System.Windows.Forms.Button TestButton;
  }
}