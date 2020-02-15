using OpenWorld.Controls;
using OpenWorld.Forms;

namespace OpenWorld.Forms
{
  partial class ChatForm
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
      this.cChatControl1 = new OpenWorld.Controls.CChatControl();
      this.CloseButton = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.cChatControl1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 527);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // cChatControl1
      // 
      this.cChatControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.cChatControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cChatControl1.Location = new System.Drawing.Point(0, 0);
      this.cChatControl1.Margin = new System.Windows.Forms.Padding(0);
      this.cChatControl1.Name = "cChatControl1";
      this.cChatControl1.Padding = new System.Windows.Forms.Padding(1);
      this.cChatControl1.Size = new System.Drawing.Size(363, 527);
      this.cChatControl1.TabIndex = 0;
      this.cChatControl1.Load += new System.EventHandler(this.cChatControl1_Load);
      // 
      // CloseButton
      // 
      this.CloseButton.BackColor = System.Drawing.Color.Gray;
      this.CloseButton.FlatAppearance.BorderSize = 0;
      this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CloseButton.ForeColor = System.Drawing.Color.White;
      this.CloseButton.Location = new System.Drawing.Point(0, 0);
      this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
      this.CloseButton.Name = "CloseButton";
      this.CloseButton.Size = new System.Drawing.Size(32, 28);
      this.CloseButton.TabIndex = 1;
      this.CloseButton.Text = ">>";
      this.CloseButton.UseVisualStyleBackColor = false;
      this.CloseButton.Click += new System.EventHandler(this.button1_Click);
      // 
      // ChatForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Black;
      this.ClientSize = new System.Drawing.Size(367, 550);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ChatForm";
      this.Opacity = 0.9D;
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Chat";
      this.TopMost = false;
      this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(2)))));
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
      this.Shown += new System.EventHandler(this.ChatForm_Shown);
      this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private CChatControl cChatControl1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button CloseButton;
  }
}