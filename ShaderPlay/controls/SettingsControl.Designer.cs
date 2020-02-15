namespace ShaderPlay
{
  partial class SettingsControl
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.LoadImage1 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.ImageBox1 = new System.Windows.Forms.PictureBox();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.LoadImage2 = new System.Windows.Forms.Button();
      this.ImageBox2 = new System.Windows.Forms.PictureBox();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ImageBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ImageBox2)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 4;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.ImageBox2, 2, 1);
      this.tableLayoutPanel1.Controls.Add(this.LoadImage2, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.LoadImage1, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
      this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
      this.tableLayoutPanel1.Controls.Add(this.ImageBox1, 2, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 8;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(511, 528);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // LoadImage1
      // 
      this.LoadImage1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LoadImage1.Location = new System.Drawing.Point(63, 3);
      this.LoadImage1.Name = "LoadImage1";
      this.LoadImage1.Size = new System.Drawing.Size(194, 26);
      this.LoadImage1.TabIndex = 0;
      this.LoadImage1.Text = "Load...";
      this.LoadImage1.UseVisualStyleBackColor = true;
      this.LoadImage1.Click += new System.EventHandler(this.LoadImage1_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 32);
      this.label1.TabIndex = 1;
      this.label1.Text = "Image 1";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(3, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(54, 32);
      this.label2.TabIndex = 2;
      this.label2.Text = "Image 2";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Location = new System.Drawing.Point(3, 64);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(54, 32);
      this.label3.TabIndex = 3;
      this.label3.Text = "label3";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(3, 96);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(54, 32);
      this.label4.TabIndex = 4;
      this.label4.Text = "label4";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Location = new System.Drawing.Point(3, 128);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(54, 32);
      this.label5.TabIndex = 5;
      this.label5.Text = "label5";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Location = new System.Drawing.Point(3, 160);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(54, 32);
      this.label6.TabIndex = 6;
      this.label6.Text = "label6";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label7.Location = new System.Drawing.Point(3, 192);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(54, 32);
      this.label7.TabIndex = 7;
      this.label7.Text = "label7";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // ImageBox1
      // 
      this.ImageBox1.Location = new System.Drawing.Point(263, 3);
      this.ImageBox1.Name = "ImageBox1";
      this.ImageBox1.Size = new System.Drawing.Size(74, 26);
      this.ImageBox1.TabIndex = 8;
      this.ImageBox1.TabStop = false;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // LoadImage2
      // 
      this.LoadImage2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LoadImage2.Location = new System.Drawing.Point(63, 35);
      this.LoadImage2.Name = "LoadImage2";
      this.LoadImage2.Size = new System.Drawing.Size(194, 26);
      this.LoadImage2.TabIndex = 9;
      this.LoadImage2.Text = "Load...";
      this.LoadImage2.UseVisualStyleBackColor = true;
      this.LoadImage2.Click += new System.EventHandler(this.LoadImage2_Click);
      // 
      // ImageBox2
      // 
      this.ImageBox2.Location = new System.Drawing.Point(263, 35);
      this.ImageBox2.Name = "ImageBox2";
      this.ImageBox2.Size = new System.Drawing.Size(74, 26);
      this.ImageBox2.TabIndex = 10;
      this.ImageBox2.TabStop = false;
      // 
      // SettingsControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "SettingsControl";
      this.Size = new System.Drawing.Size(511, 528);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ImageBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ImageBox2)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button LoadImage1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.PictureBox ImageBox1;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.PictureBox ImageBox2;
    private System.Windows.Forms.Button LoadImage2;
  }
}
