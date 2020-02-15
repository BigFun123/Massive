namespace OpenWorld.src.Controls
{
  partial class StatusControl
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
      this.PositionBox = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.StatusBox = new System.Windows.Forms.TextBox();
      this.FPSCounterLable = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // PositionBox
      // 
      this.PositionBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PositionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PositionBox.Location = new System.Drawing.Point(586, 1);
      this.PositionBox.Margin = new System.Windows.Forms.Padding(1);
      this.PositionBox.Multiline = true;
      this.PositionBox.Name = "PositionBox";
      this.PositionBox.Size = new System.Drawing.Size(313, 39);
      this.PositionBox.TabIndex = 1;
      this.PositionBox.Text = "0,0,0\r\n-18.1, 33.00";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
      this.tableLayoutPanel1.Controls.Add(this.PositionBox, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.StatusBox, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.FPSCounterLable, 2, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(965, 41);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // StatusBox
      // 
      this.StatusBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.StatusBox.Location = new System.Drawing.Point(0, 0);
      this.StatusBox.Margin = new System.Windows.Forms.Padding(0);
      this.StatusBox.Multiline = true;
      this.StatusBox.Name = "StatusBox";
      this.StatusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.StatusBox.Size = new System.Drawing.Size(585, 41);
      this.StatusBox.TabIndex = 2;
      this.StatusBox.Text = "Offline. Connect to Universe to download sites. Click and drag to look around. WA" +
    "SD to move around.";
      // 
      // FPSCounterLable
      // 
      this.FPSCounterLable.AutoSize = true;
      this.FPSCounterLable.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FPSCounterLable.Location = new System.Drawing.Point(903, 0);
      this.FPSCounterLable.Name = "FPSCounterLable";
      this.FPSCounterLable.Size = new System.Drawing.Size(59, 41);
      this.FPSCounterLable.TabIndex = 3;
      this.FPSCounterLable.Text = "FPS";
      this.FPSCounterLable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // StatusControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "StatusControl";
      this.Size = new System.Drawing.Size(965, 41);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.TextBox PositionBox;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.TextBox StatusBox;
    private System.Windows.Forms.Label FPSCounterLable;
  }
}
