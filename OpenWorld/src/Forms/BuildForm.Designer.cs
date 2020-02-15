namespace OpenWorld.Forms
{
  partial class BuildForm
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
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.PartsView = new System.Windows.Forms.ListView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.objectMoveControl1 = new OpenWorld.src.Controls.ObjectMoveControl();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.CanIBuildHere = new System.Windows.Forms.Label();
      this.Duplicate = new System.Windows.Forms.Button();
      this.DeleteButton = new System.Windows.Forms.Button();
      this.SelectedLabel = new System.Windows.Forms.Label();
      this.textureControl1 = new OpenWorld.src.Controls.TextureControl();
      this.ZoneCheckTimer = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.PartsView, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.objectMoveControl1, 0, 3);
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.textureControl1, 0, 2);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 4;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.25F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.125F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.625F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(333, 622);
      this.tableLayoutPanel2.TabIndex = 2;
      // 
      // PartsView
      // 
      this.PartsView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PartsView.LabelWrap = false;
      this.PartsView.LargeImageList = this.imageList1;
      this.PartsView.Location = new System.Drawing.Point(3, 31);
      this.PartsView.Name = "PartsView";
      this.PartsView.Size = new System.Drawing.Size(327, 328);
      this.PartsView.TabIndex = 1;
      this.PartsView.UseCompatibleStateImageBehavior = false;
      this.PartsView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PartsView_MouseDoubleClick);
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // objectMoveControl1
      // 
      this.objectMoveControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.objectMoveControl1.Location = new System.Drawing.Point(3, 532);
      this.objectMoveControl1.Name = "objectMoveControl1";
      this.objectMoveControl1.Size = new System.Drawing.Size(327, 87);
      this.objectMoveControl1.TabIndex = 2;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.ColumnCount = 4;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel3.Controls.Add(this.CanIBuildHere, 3, 0);
      this.tableLayoutPanel3.Controls.Add(this.Duplicate, 1, 0);
      this.tableLayoutPanel3.Controls.Add(this.DeleteButton, 1, 0);
      this.tableLayoutPanel3.Controls.Add(this.SelectedLabel, 0, 0);
      this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(333, 28);
      this.tableLayoutPanel3.TabIndex = 3;
      // 
      // CanIBuildHere
      // 
      this.CanIBuildHere.AutoSize = true;
      this.CanIBuildHere.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CanIBuildHere.ForeColor = System.Drawing.Color.White;
      this.CanIBuildHere.Location = new System.Drawing.Point(252, 0);
      this.CanIBuildHere.Name = "CanIBuildHere";
      this.CanIBuildHere.Size = new System.Drawing.Size(78, 28);
      this.CanIBuildHere.TabIndex = 4;
      this.CanIBuildHere.Text = "BUILD";
      this.CanIBuildHere.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // Duplicate
      // 
      this.Duplicate.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Duplicate.Location = new System.Drawing.Point(166, 0);
      this.Duplicate.Margin = new System.Windows.Forms.Padding(0);
      this.Duplicate.Name = "Duplicate";
      this.Duplicate.Size = new System.Drawing.Size(83, 28);
      this.Duplicate.TabIndex = 2;
      this.Duplicate.Text = "Duplicate";
      this.Duplicate.UseVisualStyleBackColor = true;
      this.Duplicate.Click += new System.EventHandler(this.Duplicate_Click);
      // 
      // DeleteButton
      // 
      this.DeleteButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DeleteButton.Location = new System.Drawing.Point(83, 0);
      this.DeleteButton.Margin = new System.Windows.Forms.Padding(0);
      this.DeleteButton.Name = "DeleteButton";
      this.DeleteButton.Size = new System.Drawing.Size(83, 28);
      this.DeleteButton.TabIndex = 1;
      this.DeleteButton.Text = "Delete";
      this.DeleteButton.UseVisualStyleBackColor = true;
      this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
      // 
      // SelectedLabel
      // 
      this.SelectedLabel.AutoSize = true;
      this.SelectedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SelectedLabel.ForeColor = System.Drawing.Color.White;
      this.SelectedLabel.Location = new System.Drawing.Point(3, 0);
      this.SelectedLabel.Name = "SelectedLabel";
      this.SelectedLabel.Size = new System.Drawing.Size(77, 28);
      this.SelectedLabel.TabIndex = 3;
      this.SelectedLabel.Text = "...";
      this.SelectedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // textureControl1
      // 
      this.textureControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textureControl1.Location = new System.Drawing.Point(3, 365);
      this.textureControl1.Name = "textureControl1";
      this.textureControl1.Size = new System.Drawing.Size(327, 161);
      this.textureControl1.TabIndex = 4;
      // 
      // ZoneCheckTimer
      // 
      this.ZoneCheckTimer.Interval = 1000;
      this.ZoneCheckTimer.Tick += new System.EventHandler(this.ZoneCheckTimer_Tick);
      // 
      // BuildForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(337, 645);
      this.Controls.Add(this.tableLayoutPanel2);
      this.Name = "BuildForm";
      this.Text = "BuildForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BuildForm_FormClosing);
      this.Load += new System.EventHandler(this.BuildForm_Load);
      this.Shown += new System.EventHandler(this.BuildForm_Shown);
      this.Controls.SetChildIndex(this.tableLayoutPanel2, 0);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel3.ResumeLayout(false);
      this.tableLayoutPanel3.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.ImageList imageList1;
    private src.Controls.ObjectMoveControl objectMoveControl1;
    private System.Windows.Forms.ListView PartsView;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.Windows.Forms.Button DeleteButton;
    private System.Windows.Forms.Button Duplicate;
    private src.Controls.TextureControl textureControl1;
    private System.Windows.Forms.Label SelectedLabel;
    private System.Windows.Forms.Timer ZoneCheckTimer;
    private System.Windows.Forms.Label CanIBuildHere;
  }
}