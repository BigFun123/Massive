namespace OpenWorld.Forms
{
  partial class UserStatusForm
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
      this.UpdateButton = new System.Windows.Forms.Button();
      this.UserStatusText = new System.Windows.Forms.TextBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.UploadStatusLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.WhiteButton = new System.Windows.Forms.RadioButton();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.radioButton3 = new System.Windows.Forms.RadioButton();
      this.radioButton4 = new System.Windows.Forms.RadioButton();
      this.radioButton5 = new System.Windows.Forms.RadioButton();
      this.radioButton6 = new System.Windows.Forms.RadioButton();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // UpdateButton
      // 
      this.UpdateButton.Location = new System.Drawing.Point(500, 320);
      this.UpdateButton.Name = "UpdateButton";
      this.UpdateButton.Size = new System.Drawing.Size(75, 32);
      this.UpdateButton.TabIndex = 2;
      this.UpdateButton.Text = "Post";
      this.UpdateButton.UseVisualStyleBackColor = true;
      this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
      // 
      // UserStatusText
      // 
      this.UserStatusText.Location = new System.Drawing.Point(256, 43);
      this.UserStatusText.Multiline = true;
      this.UserStatusText.Name = "UserStatusText";
      this.UserStatusText.Size = new System.Drawing.Size(320, 234);
      this.UserStatusText.TabIndex = 3;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(22, 43);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(228, 234);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 4;
      this.pictureBox1.TabStop = false;
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(22, 320);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(228, 32);
      this.progressBar1.TabIndex = 5;
      // 
      // UploadStatusLabel
      // 
      this.UploadStatusLabel.ForeColor = System.Drawing.Color.White;
      this.UploadStatusLabel.Location = new System.Drawing.Point(256, 320);
      this.UploadStatusLabel.Name = "UploadStatusLabel";
      this.UploadStatusLabel.Size = new System.Drawing.Size(238, 32);
      this.UploadStatusLabel.TabIndex = 6;
      this.UploadStatusLabel.Text = "...";
      this.UploadStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(259, 284);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(68, 13);
      this.label1.TabIndex = 7;
      this.label1.Text = "Background:";
      // 
      // WhiteButton
      // 
      this.WhiteButton.Appearance = System.Windows.Forms.Appearance.Button;
      this.WhiteButton.AutoSize = true;
      this.WhiteButton.BackColor = System.Drawing.Color.White;
      this.WhiteButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.WhiteButton.FlatAppearance.BorderSize = 0;
      this.WhiteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.WhiteButton.ForeColor = System.Drawing.Color.White;
      this.WhiteButton.Location = new System.Drawing.Point(64, 0);
      this.WhiteButton.Margin = new System.Windows.Forms.Padding(0);
      this.WhiteButton.Name = "WhiteButton";
      this.WhiteButton.Size = new System.Drawing.Size(32, 33);
      this.WhiteButton.TabIndex = 8;
      this.WhiteButton.TabStop = true;
      this.WhiteButton.UseVisualStyleBackColor = false;
      this.WhiteButton.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // radioButton1
      // 
      this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
      this.radioButton1.BackColor = System.Drawing.Color.OrangeRed;
      this.radioButton1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radioButton1.FlatAppearance.BorderSize = 0;
      this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.radioButton1.ForeColor = System.Drawing.Color.White;
      this.radioButton1.Location = new System.Drawing.Point(192, 0);
      this.radioButton1.Margin = new System.Windows.Forms.Padding(0);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(32, 33);
      this.radioButton1.TabIndex = 9;
      this.radioButton1.TabStop = true;
      this.radioButton1.UseVisualStyleBackColor = false;
      this.radioButton1.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 7;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.radioButton6, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.radioButton5, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.radioButton4, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.radioButton3, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.radioButton2, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.radioButton1, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this.WhiteButton, 0, 0);
      this.tableLayoutPanel2.Location = new System.Drawing.Point(352, 284);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(224, 33);
      this.tableLayoutPanel2.TabIndex = 10;
      // 
      // radioButton2
      // 
      this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
      this.radioButton2.BackColor = System.Drawing.Color.YellowGreen;
      this.radioButton2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radioButton2.FlatAppearance.BorderSize = 0;
      this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.radioButton2.ForeColor = System.Drawing.Color.White;
      this.radioButton2.Location = new System.Drawing.Point(32, 0);
      this.radioButton2.Margin = new System.Windows.Forms.Padding(0);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(32, 33);
      this.radioButton2.TabIndex = 10;
      this.radioButton2.TabStop = true;
      this.radioButton2.UseVisualStyleBackColor = false;
      this.radioButton2.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // radioButton3
      // 
      this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
      this.radioButton3.BackColor = System.Drawing.Color.SkyBlue;
      this.radioButton3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radioButton3.FlatAppearance.BorderSize = 0;
      this.radioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.radioButton3.ForeColor = System.Drawing.Color.White;
      this.radioButton3.Location = new System.Drawing.Point(0, 0);
      this.radioButton3.Margin = new System.Windows.Forms.Padding(0);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new System.Drawing.Size(32, 33);
      this.radioButton3.TabIndex = 11;
      this.radioButton3.TabStop = true;
      this.radioButton3.UseVisualStyleBackColor = false;
      this.radioButton3.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // radioButton4
      // 
      this.radioButton4.Appearance = System.Windows.Forms.Appearance.Button;
      this.radioButton4.BackColor = System.Drawing.Color.Goldenrod;
      this.radioButton4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radioButton4.FlatAppearance.BorderSize = 0;
      this.radioButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.radioButton4.ForeColor = System.Drawing.Color.White;
      this.radioButton4.Location = new System.Drawing.Point(160, 0);
      this.radioButton4.Margin = new System.Windows.Forms.Padding(0);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new System.Drawing.Size(32, 33);
      this.radioButton4.TabIndex = 12;
      this.radioButton4.TabStop = true;
      this.radioButton4.UseVisualStyleBackColor = false;
      this.radioButton4.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // radioButton5
      // 
      this.radioButton5.Appearance = System.Windows.Forms.Appearance.Button;
      this.radioButton5.BackColor = System.Drawing.Color.MediumOrchid;
      this.radioButton5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radioButton5.FlatAppearance.BorderSize = 0;
      this.radioButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.radioButton5.ForeColor = System.Drawing.Color.White;
      this.radioButton5.Location = new System.Drawing.Point(128, 0);
      this.radioButton5.Margin = new System.Windows.Forms.Padding(0);
      this.radioButton5.Name = "radioButton5";
      this.radioButton5.Size = new System.Drawing.Size(32, 33);
      this.radioButton5.TabIndex = 13;
      this.radioButton5.TabStop = true;
      this.radioButton5.UseVisualStyleBackColor = false;
      this.radioButton5.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // radioButton6
      // 
      this.radioButton6.Appearance = System.Windows.Forms.Appearance.Button;
      this.radioButton6.BackColor = System.Drawing.Color.Silver;
      this.radioButton6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.radioButton6.FlatAppearance.BorderSize = 0;
      this.radioButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.radioButton6.ForeColor = System.Drawing.Color.White;
      this.radioButton6.Location = new System.Drawing.Point(96, 0);
      this.radioButton6.Margin = new System.Windows.Forms.Padding(0);
      this.radioButton6.Name = "radioButton6";
      this.radioButton6.Size = new System.Drawing.Size(32, 33);
      this.radioButton6.TabIndex = 14;
      this.radioButton6.TabStop = true;
      this.radioButton6.UseVisualStyleBackColor = false;
      this.radioButton6.CheckedChanged += new System.EventHandler(this.WhiteButton_CheckedChanged);
      // 
      // UserStatusForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(631, 381);
      this.Controls.Add(this.tableLayoutPanel2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.UploadStatusLabel);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.UserStatusText);
      this.Controls.Add(this.UpdateButton);
      this.Name = "UserStatusForm";
      this.Text = "UserStatusForm";
      this.Controls.SetChildIndex(this.UpdateButton, 0);
      this.Controls.SetChildIndex(this.UserStatusText, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.progressBar1, 0);
      this.Controls.SetChildIndex(this.UploadStatusLabel, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button UpdateButton;
    private System.Windows.Forms.TextBox UserStatusText;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Label UploadStatusLabel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.RadioButton WhiteButton;
    private System.Windows.Forms.RadioButton radioButton1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.RadioButton radioButton2;
    private System.Windows.Forms.RadioButton radioButton3;
    private System.Windows.Forms.RadioButton radioButton4;
    private System.Windows.Forms.RadioButton radioButton5;
    private System.Windows.Forms.RadioButton radioButton6;
  }
}