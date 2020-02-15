namespace OpenWorld.Forms
{
  partial class AssetsForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      this.AssetsView = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.AssetsView)).BeginInit();
      this.SuspendLayout();
      // 
      // AssetsView
      // 
      this.AssetsView.BackgroundColor = System.Drawing.Color.Black;
      this.AssetsView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.AssetsView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.AssetsView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.AssetsView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.AssetsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.AssetsView.DefaultCellStyle = dataGridViewCellStyle2;
      this.AssetsView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.AssetsView.EnableHeadersVisualStyles = false;
      this.AssetsView.GridColor = System.Drawing.Color.Gray;
      this.AssetsView.Location = new System.Drawing.Point(2, 21);
      this.AssetsView.Name = "AssetsView";
      this.AssetsView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.AssetsView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.AssetsView.Size = new System.Drawing.Size(1049, 598);
      this.AssetsView.TabIndex = 2;
      this.AssetsView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AssetsView_CellDoubleClick);
      this.AssetsView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.AssetsView_CellValueChanged);
      this.AssetsView.CurrentCellDirtyStateChanged += new System.EventHandler(this.AssetsView_CurrentCellDirtyStateChanged);
      this.AssetsView.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.AssetsView_RowPrePaint);
      this.AssetsView.SelectionChanged += new System.EventHandler(this.AssetsView_SelectionChanged);
      // 
      // AssetsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.ClientSize = new System.Drawing.Size(1053, 621);
      this.Controls.Add(this.AssetsView);
      this.DoubleBuffered = true;
      this.Name = "AssetsForm";
      this.Opacity = 0.8D;
      this.Text = "AssetsForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssetsForm_FormClosing);
      this.Controls.SetChildIndex(this.AssetsView, 0);
      ((System.ComponentModel.ISupportInitialize)(this.AssetsView)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView AssetsView;
  }
}