namespace OpenWorld.Forms
{
  partial class LobbyForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LobbyForm));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.DomainBox = new System.Windows.Forms.TextBox();
      this.JoinButton = new System.Windows.Forms.Button();
      this.ServerIPBox = new System.Windows.Forms.TextBox();
      this.NameLabel = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333333F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.66666F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 486);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(3, 43);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(752, 440);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
      this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 4;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.34503F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.32164F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
      this.tableLayoutPanel2.Controls.Add(this.DomainBox, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.JoinButton, 3, 0);
      this.tableLayoutPanel2.Controls.Add(this.ServerIPBox, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.NameLabel, 0, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(752, 34);
      this.tableLayoutPanel2.TabIndex = 1;
      // 
      // DomainBox
      // 
      this.DomainBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DomainBox.Location = new System.Drawing.Point(134, 3);
      this.DomainBox.Name = "DomainBox";
      this.DomainBox.Size = new System.Drawing.Size(199, 20);
      this.DomainBox.TabIndex = 3;
      this.DomainBox.Text = "bigfun.co.za";
      // 
      // JoinButton
      // 
      this.JoinButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.JoinButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.JoinButton.ForeColor = System.Drawing.Color.White;
      this.JoinButton.Location = new System.Drawing.Point(618, 3);
      this.JoinButton.Name = "JoinButton";
      this.JoinButton.Size = new System.Drawing.Size(131, 28);
      this.JoinButton.TabIndex = 0;
      this.JoinButton.Text = "Join";
      this.JoinButton.UseVisualStyleBackColor = true;
      this.JoinButton.Click += new System.EventHandler(this.JoinButton_Click);
      // 
      // ServerIPBox
      // 
      this.ServerIPBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ServerIPBox.Location = new System.Drawing.Point(339, 3);
      this.ServerIPBox.Name = "ServerIPBox";
      this.ServerIPBox.Size = new System.Drawing.Size(273, 20);
      this.ServerIPBox.TabIndex = 1;
      // 
      // NameLabel
      // 
      this.NameLabel.AutoSize = true;
      this.NameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.NameLabel.ForeColor = System.Drawing.Color.White;
      this.NameLabel.Location = new System.Drawing.Point(3, 0);
      this.NameLabel.Name = "NameLabel";
      this.NameLabel.Size = new System.Drawing.Size(125, 34);
      this.NameLabel.TabIndex = 2;
      this.NameLabel.Text = "Servers";
      this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // LobbyForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(762, 509);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "LobbyForm";
      this.Text = "Lobby";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LobbyForm_FormClosing);
      this.Load += new System.EventHandler(this.LobbyForm_Load);
      this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
      this.tableLayoutPanel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Button JoinButton;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TextBox ServerIPBox;
    private System.Windows.Forms.Label NameLabel;
    private System.Windows.Forms.TextBox DomainBox;
  }
}