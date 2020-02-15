namespace OpenWorld.Forms
{
  partial class ProductInfoForm
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
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.InfoBox = new System.Windows.Forms.TextBox();
      this.UpdateButton = new System.Windows.Forms.Button();
      this.StatusText = new System.Windows.Forms.Label();
      this.MetaData = new System.Windows.Forms.TextBox();
      this.CloseButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
      this.tableLayoutPanel1.ColumnCount = 5;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.41447F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.58553F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.InfoBox, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.UpdateButton, 3, 1);
      this.tableLayoutPanel1.Controls.Add(this.StatusText, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.MetaData, 3, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(851, 530);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.Location = new System.Drawing.Point(3, 3);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(489, 452);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // InfoBox
      // 
      this.InfoBox.BackColor = System.Drawing.Color.Silver;
      this.InfoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.tableLayoutPanel1.SetColumnSpan(this.InfoBox, 2);
      this.InfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.InfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.InfoBox.Location = new System.Drawing.Point(495, 0);
      this.InfoBox.Margin = new System.Windows.Forms.Padding(0);
      this.InfoBox.Multiline = true;
      this.InfoBox.Name = "InfoBox";
      this.InfoBox.Size = new System.Drawing.Size(220, 458);
      this.InfoBox.TabIndex = 1;
      // 
      // UpdateButton
      // 
      this.UpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
      this.UpdateButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UpdateButton.FlatAppearance.BorderSize = 0;
      this.UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.UpdateButton.Location = new System.Drawing.Point(715, 458);
      this.UpdateButton.Margin = new System.Windows.Forms.Padding(0);
      this.UpdateButton.Name = "UpdateButton";
      this.UpdateButton.Size = new System.Drawing.Size(100, 40);
      this.UpdateButton.TabIndex = 2;
      this.UpdateButton.Text = "Update";
      this.UpdateButton.UseVisualStyleBackColor = false;
      this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
      // 
      // StatusText
      // 
      this.StatusText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(69)))), ((int)(((byte)(69)))));
      this.tableLayoutPanel1.SetColumnSpan(this.StatusText, 3);
      this.StatusText.Dock = System.Windows.Forms.DockStyle.Fill;
      this.StatusText.ForeColor = System.Drawing.Color.White;
      this.StatusText.Location = new System.Drawing.Point(3, 458);
      this.StatusText.Name = "StatusText";
      this.StatusText.Size = new System.Drawing.Size(709, 40);
      this.StatusText.TabIndex = 3;
      this.StatusText.Text = ">";
      this.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // MetaData
      // 
      this.MetaData.BackColor = System.Drawing.Color.LightGray;
      this.MetaData.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.MetaData.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MetaData.Location = new System.Drawing.Point(715, 0);
      this.MetaData.Margin = new System.Windows.Forms.Padding(0);
      this.MetaData.Multiline = true;
      this.MetaData.Name = "MetaData";
      this.MetaData.Size = new System.Drawing.Size(100, 458);
      this.MetaData.TabIndex = 4;
      // 
      // CloseButton
      // 
      this.CloseButton.BackColor = System.Drawing.Color.Silver;
      this.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CloseButton.FlatAppearance.BorderSize = 0;
      this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CloseButton.Location = new System.Drawing.Point(780, 0);
      this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(32, 26);
      this.CloseButton.TabIndex = 9;
      this.CloseButton.Text = "x";
      this.CloseButton.UseVisualStyleBackColor = false;
      // 
      // ProductInfoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.ClientSize = new System.Drawing.Size(855, 553);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ProductInfoForm";
      this.Text = "Information";
      this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button CloseButton;
    private System.Windows.Forms.TextBox InfoBox;
    private System.Windows.Forms.Button UpdateButton;
    private System.Windows.Forms.Label StatusText;
    private System.Windows.Forms.TextBox MetaData;
  }
}