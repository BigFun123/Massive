namespace MassiveServer.src.forms
{
  partial class SQLForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLForm));
      this.QueryBox = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.SchemaBox = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.PortBox = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.PasswordBox = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.UsernameBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.Execute = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.HostnameBox = new System.Windows.Forms.TextBox();
      this.ResultBox = new System.Windows.Forms.TextBox();
      this.UpdateButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      this.SuspendLayout();
      // 
      // QueryBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.QueryBox, 11);
      this.QueryBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.QueryBox.Location = new System.Drawing.Point(3, 36);
      this.QueryBox.Multiline = true;
      this.QueryBox.Name = "QueryBox";
      this.QueryBox.Size = new System.Drawing.Size(1102, 215);
      this.QueryBox.TabIndex = 0;
      this.QueryBox.Text = "Select * from objects LIMIT 10;";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 11;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.SchemaBox, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.PortBox, 9, 0);
      this.tableLayoutPanel1.Controls.Add(this.label4, 8, 0);
      this.tableLayoutPanel1.Controls.Add(this.PasswordBox, 7, 0);
      this.tableLayoutPanel1.Controls.Add(this.label3, 6, 0);
      this.tableLayoutPanel1.Controls.Add(this.UsernameBox, 5, 0);
      this.tableLayoutPanel1.Controls.Add(this.label2, 4, 0);
      this.tableLayoutPanel1.Controls.Add(this.QueryBox, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.Execute, 10, 0);
      this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.HostnameBox, 3, 0);
      this.tableLayoutPanel1.Controls.Add(this.ResultBox, 2, 3);
      this.tableLayoutPanel1.Controls.Add(this.UpdateButton, 10, 3);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1108, 547);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // SchemaBox
      // 
      this.SchemaBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SchemaBox.Location = new System.Drawing.Point(66, 3);
      this.SchemaBox.Name = "SchemaBox";
      this.SchemaBox.Size = new System.Drawing.Size(94, 20);
      this.SchemaBox.TabIndex = 14;
      // 
      // label5
      // 
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Location = new System.Drawing.Point(3, 0);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(57, 33);
      this.label5.TabIndex = 13;
      this.label5.Text = "Schema";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // PortBox
      // 
      this.PortBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PortBox.Location = new System.Drawing.Point(755, 3);
      this.PortBox.Name = "PortBox";
      this.PortBox.Size = new System.Drawing.Size(59, 20);
      this.PortBox.TabIndex = 10;
      // 
      // label4
      // 
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(706, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(43, 33);
      this.label4.TabIndex = 9;
      this.label4.Text = "Port";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // PasswordBox
      // 
      this.PasswordBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PasswordBox.Location = new System.Drawing.Point(612, 3);
      this.PasswordBox.Name = "PasswordBox";
      this.PasswordBox.Size = new System.Drawing.Size(88, 20);
      this.PasswordBox.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Location = new System.Drawing.Point(529, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(77, 33);
      this.label3.TabIndex = 7;
      this.label3.Text = "Password";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // UsernameBox
      // 
      this.UsernameBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UsernameBox.Location = new System.Drawing.Point(425, 3);
      this.UsernameBox.Name = "UsernameBox";
      this.UsernameBox.Size = new System.Drawing.Size(98, 20);
      this.UsernameBox.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(329, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(90, 33);
      this.label2.TabIndex = 4;
      this.label2.Text = "Username";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // dataGridView1
      // 
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 11);
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(3, 257);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new System.Drawing.Size(1102, 215);
      this.dataGridView1.TabIndex = 1;
      // 
      // Execute
      // 
      this.Execute.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Execute.Location = new System.Drawing.Point(820, 3);
      this.Execute.Name = "Execute";
      this.Execute.Size = new System.Drawing.Size(285, 27);
      this.Execute.TabIndex = 2;
      this.Execute.Text = "F5 Execute && Save";
      this.Execute.UseVisualStyleBackColor = true;
      this.Execute.Click += new System.EventHandler(this.Execute_Click);
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(166, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 33);
      this.label1.TabIndex = 3;
      this.label1.Text = "Host";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // HostnameBox
      // 
      this.HostnameBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.HostnameBox.Location = new System.Drawing.Point(229, 3);
      this.HostnameBox.Name = "HostnameBox";
      this.HostnameBox.Size = new System.Drawing.Size(94, 20);
      this.HostnameBox.TabIndex = 5;
      // 
      // ResultBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.ResultBox, 8);
      this.ResultBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ResultBox.Location = new System.Drawing.Point(166, 478);
      this.ResultBox.Multiline = true;
      this.ResultBox.Name = "ResultBox";
      this.ResultBox.Size = new System.Drawing.Size(648, 66);
      this.ResultBox.TabIndex = 12;
      // 
      // UpdateButton
      // 
      this.UpdateButton.Dock = System.Windows.Forms.DockStyle.Right;
      this.UpdateButton.Location = new System.Drawing.Point(1015, 478);
      this.UpdateButton.Name = "UpdateButton";
      this.UpdateButton.Size = new System.Drawing.Size(90, 66);
      this.UpdateButton.TabIndex = 11;
      this.UpdateButton.Text = "Update";
      this.UpdateButton.UseVisualStyleBackColor = true;
      // 
      // SQLForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1108, 547);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Name = "SQLForm";
      this.Text = "Database";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SQLForm_KeyUp);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox QueryBox;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.Button Execute;
    private System.Windows.Forms.TextBox PortBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox PasswordBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox UsernameBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox HostnameBox;
    private System.Windows.Forms.Button UpdateButton;
    private System.Windows.Forms.TextBox ResultBox;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox SchemaBox;
  }
}