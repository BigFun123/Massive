namespace MassiveGenerator.Controls
{
  partial class MapTileInspector
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
      this.treeView1 = new System.Windows.Forms.TreeView();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.GetJSON = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // treeView1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.treeView1, 3);
      this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView1.Location = new System.Drawing.Point(3, 38);
      this.treeView1.Name = "treeView1";
      this.treeView1.Size = new System.Drawing.Size(389, 503);
      this.treeView1.TabIndex = 0;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 3;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.GetJSON, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.433824F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.56618F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(395, 544);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // GetJSON
      // 
      this.GetJSON.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GetJSON.Location = new System.Drawing.Point(3, 3);
      this.GetJSON.Name = "GetJSON";
      this.GetJSON.Size = new System.Drawing.Size(94, 29);
      this.GetJSON.TabIndex = 1;
      this.GetJSON.Text = "JSON";
      this.GetJSON.UseVisualStyleBackColor = true;
      this.GetJSON.Click += new System.EventHandler(this.GetJSON_Click);
      // 
      // MapTileInspector
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "MapTileInspector";
      this.Size = new System.Drawing.Size(395, 544);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TreeView treeView1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button GetJSON;
  }
}
