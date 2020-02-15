namespace OpenWorld.src.Controls
{
  partial class TextureControl
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
      this.components = new System.ComponentModel.Container();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.UploadTexture = new System.Windows.Forms.Button();
      this.TextureView = new System.Windows.Forms.ListView();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.CopyButton = new System.Windows.Forms.Button();
      this.PasteButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.TextureView, 0, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.72862F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.27138F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(252, 269);
      this.tableLayoutPanel2.TabIndex = 1;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 5;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel1.Controls.Add(this.PasteButton, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.CopyButton, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.UploadTexture, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(252, 44);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // UploadTexture
      // 
      this.UploadTexture.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UploadTexture.Location = new System.Drawing.Point(0, 0);
      this.UploadTexture.Margin = new System.Windows.Forms.Padding(0);
      this.UploadTexture.Name = "UploadTexture";
      this.UploadTexture.Size = new System.Drawing.Size(50, 44);
      this.UploadTexture.TabIndex = 0;
      this.UploadTexture.Text = "Upload";
      this.UploadTexture.UseVisualStyleBackColor = true;
      this.UploadTexture.Click += new System.EventHandler(this.UploadTexture_Click);
      // 
      // TextureView
      // 
      this.TextureView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TextureView.LargeImageList = this.imageList1;
      this.TextureView.Location = new System.Drawing.Point(0, 44);
      this.TextureView.Margin = new System.Windows.Forms.Padding(0);
      this.TextureView.Name = "TextureView";
      this.TextureView.Size = new System.Drawing.Size(252, 225);
      this.TextureView.TabIndex = 2;
      this.TextureView.UseCompatibleStateImageBehavior = false;
      this.TextureView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextureView_MouseClick);
      // 
      // imageList1
      // 
      this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // CopyButton
      // 
      this.CopyButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CopyButton.Location = new System.Drawing.Point(50, 0);
      this.CopyButton.Margin = new System.Windows.Forms.Padding(0);
      this.CopyButton.Name = "CopyButton";
      this.CopyButton.Size = new System.Drawing.Size(50, 44);
      this.CopyButton.TabIndex = 1;
      this.CopyButton.Text = "Copy";
      this.CopyButton.UseVisualStyleBackColor = true;
      this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
      // 
      // PasteButton
      // 
      this.PasteButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PasteButton.Location = new System.Drawing.Point(100, 0);
      this.PasteButton.Margin = new System.Windows.Forms.Padding(0);
      this.PasteButton.Name = "PasteButton";
      this.PasteButton.Size = new System.Drawing.Size(50, 44);
      this.PasteButton.TabIndex = 2;
      this.PasteButton.Text = "Paste";
      this.PasteButton.UseVisualStyleBackColor = true;
      this.PasteButton.Click += new System.EventHandler(this.PasteButton_Click);
      // 
      // TextureControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel2);
      this.Name = "TextureControl";
      this.Size = new System.Drawing.Size(252, 269);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ListView TextureView;
    private System.Windows.Forms.ImageList imageList1;
    private System.Windows.Forms.Button UploadTexture;
    private System.Windows.Forms.Button CopyButton;
    private System.Windows.Forms.Button PasteButton;
  }
}
