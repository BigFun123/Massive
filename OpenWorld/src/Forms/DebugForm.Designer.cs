namespace OpenWorld.Forms
{
  partial class DebugForm
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
      this.DebugTable = new System.Windows.Forms.DataGridView();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.GraphBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.DebugTable)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.GraphBox)).BeginInit();
      this.SuspendLayout();
      // 
      // DebugTable
      // 
      this.DebugTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DebugTable.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DebugTable.Location = new System.Drawing.Point(3, 3);
      this.DebugTable.Name = "DebugTable";
      this.DebugTable.Size = new System.Drawing.Size(727, 372);
      this.DebugTable.TabIndex = 0;
      // 
      // timer1
      // 
      this.timer1.Interval = 50;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.DebugTable, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.GraphBox, 0, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.2619F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.73809F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(733, 563);
      this.tableLayoutPanel2.TabIndex = 2;
      // 
      // GraphBox
      // 
      this.GraphBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GraphBox.Location = new System.Drawing.Point(3, 381);
      this.GraphBox.Name = "GraphBox";
      this.GraphBox.Size = new System.Drawing.Size(727, 179);
      this.GraphBox.TabIndex = 1;
      this.GraphBox.TabStop = false;
      this.GraphBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphBox_Paint);
      // 
      // DebugForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(737, 586);
      this.Controls.Add(this.tableLayoutPanel2);
      this.Name = "DebugForm";
      this.Text = "DebugForm";
      this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
      ((System.ComponentModel.ISupportInitialize)(this.DebugTable)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.GraphBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView DebugTable;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.PictureBox GraphBox;
  }
}