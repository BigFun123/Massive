namespace OpenWorld.Forms
{
  partial class TeleporterConfigForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeleporterConfigForm));
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.DestinationBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.DescriptionBox = new System.Windows.Forms.TextBox();
      this.UpdatePropertiesButton = new System.Windows.Forms.Button();
      this.ErrorLabel = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.Color.Silver;
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(12, 30);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(260, 90);
      this.textBox1.TabIndex = 1;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // DestinationBox
      // 
      this.DestinationBox.Location = new System.Drawing.Point(12, 142);
      this.DestinationBox.Name = "DestinationBox";
      this.DestinationBox.Size = new System.Drawing.Size(245, 20);
      this.DestinationBox.TabIndex = 2;
      this.DestinationBox.Text = "moon";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 126);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Destination";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 170);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(108, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Description (Optional)";
      // 
      // DescriptionBox
      // 
      this.DescriptionBox.Location = new System.Drawing.Point(12, 186);
      this.DescriptionBox.MaxLength = 1024;
      this.DescriptionBox.Multiline = true;
      this.DescriptionBox.Name = "DescriptionBox";
      this.DescriptionBox.Size = new System.Drawing.Size(245, 64);
      this.DescriptionBox.TabIndex = 5;
      // 
      // UpdatePropertiesButton
      // 
      this.UpdatePropertiesButton.BackColor = System.Drawing.Color.Gray;
      this.UpdatePropertiesButton.FlatAppearance.BorderSize = 0;
      this.UpdatePropertiesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.UpdatePropertiesButton.Location = new System.Drawing.Point(182, 267);
      this.UpdatePropertiesButton.Name = "UpdatePropertiesButton";
      this.UpdatePropertiesButton.Size = new System.Drawing.Size(75, 34);
      this.UpdatePropertiesButton.TabIndex = 6;
      this.UpdatePropertiesButton.Text = "Update";
      this.UpdatePropertiesButton.UseVisualStyleBackColor = false;
      this.UpdatePropertiesButton.Click += new System.EventHandler(this.UpdatePropertiesButton_Click);
      // 
      // ErrorLabel
      // 
      this.ErrorLabel.AutoSize = true;
      this.ErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ErrorLabel.ForeColor = System.Drawing.Color.Maroon;
      this.ErrorLabel.Location = new System.Drawing.Point(19, 267);
      this.ErrorLabel.Name = "ErrorLabel";
      this.ErrorLabel.Size = new System.Drawing.Size(0, 13);
      this.ErrorLabel.TabIndex = 8;
      // 
      // timer1
      // 
      this.timer1.Interval = 500;
      // 
      // TeleporterConfigForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Silver;
      this.ClientSize = new System.Drawing.Size(284, 313);
      this.Controls.Add(this.ErrorLabel);
      this.Controls.Add(this.UpdatePropertiesButton);
      this.Controls.Add(this.DescriptionBox);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.DestinationBox);
      this.Controls.Add(this.textBox1);
      this.Name = "TeleporterConfigForm";
      this.Text = "TeleporterConfigForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeleporterConfigForm_FormClosing);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.DestinationBox, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.DescriptionBox, 0);
      this.Controls.SetChildIndex(this.UpdatePropertiesButton, 0);
      this.Controls.SetChildIndex(this.ErrorLabel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox DestinationBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox DescriptionBox;
    private System.Windows.Forms.Button UpdatePropertiesButton;    
    private System.Windows.Forms.Label ErrorLabel;
    private System.Windows.Forms.Timer timer1;
  }
}