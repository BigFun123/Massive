namespace OpenWorld.src.Forms
{
  partial class ShaderEditorForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShaderEditorForm));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.ShaderEditor = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.FragmentFunctions = new System.Windows.Forms.RadioButton();
      this.RevertButton = new System.Windows.Forms.Button();
      this.FragmentMode = new System.Windows.Forms.RadioButton();
      this.EvalMode = new System.Windows.Forms.RadioButton();
      this.ControlMode = new System.Windows.Forms.RadioButton();
      this.ShaderCombo = new System.Windows.Forms.ComboBox();
      this.UpdateButton = new System.Windows.Forms.Button();
      this.VertexMode = new System.Windows.Forms.RadioButton();
      this.ResultBox = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.TimeOfDay = new System.Windows.Forms.TrackBar();
      this.TimeOfDayLabel = new System.Windows.Forms.Label();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ShaderEditor)).BeginInit();
      this.tableLayoutPanel2.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TimeOfDay)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.ShaderEditor, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.ResultBox, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 21);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(788, 645);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // ShaderEditor
      // 
      this.ShaderEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
      this.ShaderEditor.AutoScrollMinSize = new System.Drawing.Size(27, 14);
      this.ShaderEditor.BackBrush = null;
      this.ShaderEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
      this.ShaderEditor.CaretColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
      this.ShaderEditor.CharHeight = 14;
      this.ShaderEditor.CharWidth = 8;
      this.ShaderEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.ShaderEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.ShaderEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ShaderEditor.ForeColor = System.Drawing.Color.White;
      this.ShaderEditor.IsReplaceMode = false;
      this.ShaderEditor.Location = new System.Drawing.Point(3, 67);
      this.ShaderEditor.Name = "ShaderEditor";
      this.ShaderEditor.Paddings = new System.Windows.Forms.Padding(0);
      this.ShaderEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(215)))), ((int)(((byte)(214)))), ((int)(((byte)(255)))));
      this.ShaderEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ShaderEditor.ServiceColors")));
      this.ShaderEditor.Size = new System.Drawing.Size(782, 475);
      this.ShaderEditor.TabIndex = 0;
      this.ShaderEditor.TextAreaBorder = FastColoredTextBoxNS.TextAreaBorderType.Single;
      this.ShaderEditor.Zoom = 100;
      this.ShaderEditor.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ShaderEditor_TextChangedDelayed);
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 9;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.FragmentFunctions, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this.RevertButton, 7, 0);
      this.tableLayoutPanel2.Controls.Add(this.FragmentMode, 3, 0);
      this.tableLayoutPanel2.Controls.Add(this.EvalMode, 5, 0);
      this.tableLayoutPanel2.Controls.Add(this.ControlMode, 4, 0);
      this.tableLayoutPanel2.Controls.Add(this.ShaderCombo, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.UpdateButton, 6, 0);
      this.tableLayoutPanel2.Controls.Add(this.VertexMode, 2, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(782, 26);
      this.tableLayoutPanel2.TabIndex = 1;
      // 
      // FragmentFunctions
      // 
      this.FragmentFunctions.AutoSize = true;
      this.FragmentFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FragmentFunctions.ForeColor = System.Drawing.Color.White;
      this.FragmentFunctions.Location = new System.Drawing.Point(203, 3);
      this.FragmentFunctions.Name = "FragmentFunctions";
      this.FragmentFunctions.Size = new System.Drawing.Size(76, 20);
      this.FragmentFunctions.TabIndex = 7;
      this.FragmentFunctions.Text = "Functions";
      this.FragmentFunctions.UseVisualStyleBackColor = true;
      this.FragmentFunctions.CheckedChanged += new System.EventHandler(this.FragmentFunctions_CheckedChanged);
      // 
      // RevertButton
      // 
      this.RevertButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.RevertButton.Location = new System.Drawing.Point(692, 0);
      this.RevertButton.Margin = new System.Windows.Forms.Padding(0);
      this.RevertButton.Name = "RevertButton";
      this.RevertButton.Size = new System.Drawing.Size(82, 26);
      this.RevertButton.TabIndex = 6;
      this.RevertButton.Text = "Revert";
      this.RevertButton.UseVisualStyleBackColor = true;
      this.RevertButton.Click += new System.EventHandler(this.RevertButton_Click);
      // 
      // FragmentMode
      // 
      this.FragmentMode.AutoSize = true;
      this.FragmentMode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FragmentMode.ForeColor = System.Drawing.Color.White;
      this.FragmentMode.Location = new System.Drawing.Point(367, 3);
      this.FragmentMode.Name = "FragmentMode";
      this.FragmentMode.Size = new System.Drawing.Size(76, 20);
      this.FragmentMode.TabIndex = 5;
      this.FragmentMode.Text = "Fragment";
      this.FragmentMode.UseVisualStyleBackColor = true;
      this.FragmentMode.CheckedChanged += new System.EventHandler(this.FragmentMode_CheckedChanged);
      // 
      // EvalMode
      // 
      this.EvalMode.AutoSize = true;
      this.EvalMode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.EvalMode.ForeColor = System.Drawing.Color.White;
      this.EvalMode.Location = new System.Drawing.Point(531, 3);
      this.EvalMode.Name = "EvalMode";
      this.EvalMode.Size = new System.Drawing.Size(76, 20);
      this.EvalMode.TabIndex = 4;
      this.EvalMode.TabStop = true;
      this.EvalMode.Text = "Eval";
      this.EvalMode.UseVisualStyleBackColor = true;
      this.EvalMode.CheckedChanged += new System.EventHandler(this.EvalMode_CheckedChanged);
      // 
      // ControlMode
      // 
      this.ControlMode.AutoSize = true;
      this.ControlMode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ControlMode.ForeColor = System.Drawing.Color.White;
      this.ControlMode.Location = new System.Drawing.Point(449, 3);
      this.ControlMode.Name = "ControlMode";
      this.ControlMode.Size = new System.Drawing.Size(76, 20);
      this.ControlMode.TabIndex = 3;
      this.ControlMode.TabStop = true;
      this.ControlMode.Text = "Control";
      this.ControlMode.UseVisualStyleBackColor = true;
      this.ControlMode.CheckedChanged += new System.EventHandler(this.ControlMode_CheckedChanged);
      // 
      // ShaderCombo
      // 
      this.ShaderCombo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ShaderCombo.FormattingEnabled = true;
      this.ShaderCombo.Location = new System.Drawing.Point(3, 3);
      this.ShaderCombo.Name = "ShaderCombo";
      this.ShaderCombo.Size = new System.Drawing.Size(194, 21);
      this.ShaderCombo.TabIndex = 0;
      this.ShaderCombo.Text = "Select Shader";
      this.ShaderCombo.SelectedIndexChanged += new System.EventHandler(this.ShaderCombo_SelectedIndexChanged);
      // 
      // UpdateButton
      // 
      this.UpdateButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UpdateButton.Location = new System.Drawing.Point(610, 0);
      this.UpdateButton.Margin = new System.Windows.Forms.Padding(0);
      this.UpdateButton.Name = "UpdateButton";
      this.UpdateButton.Size = new System.Drawing.Size(82, 26);
      this.UpdateButton.TabIndex = 1;
      this.UpdateButton.Text = "Update";
      this.UpdateButton.UseVisualStyleBackColor = true;
      this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
      // 
      // VertexMode
      // 
      this.VertexMode.AutoSize = true;
      this.VertexMode.Checked = true;
      this.VertexMode.Dock = System.Windows.Forms.DockStyle.Fill;
      this.VertexMode.ForeColor = System.Drawing.Color.White;
      this.VertexMode.Location = new System.Drawing.Point(285, 3);
      this.VertexMode.Name = "VertexMode";
      this.VertexMode.Size = new System.Drawing.Size(76, 20);
      this.VertexMode.TabIndex = 2;
      this.VertexMode.TabStop = true;
      this.VertexMode.Text = "Vertex";
      this.VertexMode.UseVisualStyleBackColor = true;
      this.VertexMode.CheckedChanged += new System.EventHandler(this.VertexMode_CheckedChanged);
      // 
      // ResultBox
      // 
      this.ResultBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ResultBox.Location = new System.Drawing.Point(3, 548);
      this.ResultBox.Multiline = true;
      this.ResultBox.Name = "ResultBox";
      this.ResultBox.Size = new System.Drawing.Size(782, 94);
      this.ResultBox.TabIndex = 2;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.ColumnCount = 3;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Controls.Add(this.TimeOfDay, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.TimeOfDayLabel, 1, 0);
      this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 35);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(782, 26);
      this.tableLayoutPanel3.TabIndex = 3;
      // 
      // TimeOfDay
      // 
      this.TimeOfDay.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TimeOfDay.Location = new System.Drawing.Point(3, 3);
      this.TimeOfDay.Maximum = 24;
      this.TimeOfDay.Name = "TimeOfDay";
      this.TimeOfDay.Size = new System.Drawing.Size(194, 20);
      this.TimeOfDay.TabIndex = 0;
      this.TimeOfDay.Value = 10;
      this.TimeOfDay.Scroll += new System.EventHandler(this.TimeOfDay_Scroll);
      // 
      // TimeOfDayLabel
      // 
      this.TimeOfDayLabel.AutoSize = true;
      this.TimeOfDayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TimeOfDayLabel.ForeColor = System.Drawing.Color.White;
      this.TimeOfDayLabel.Location = new System.Drawing.Point(203, 0);
      this.TimeOfDayLabel.Name = "TimeOfDayLabel";
      this.TimeOfDayLabel.Size = new System.Drawing.Size(94, 26);
      this.TimeOfDayLabel.TabIndex = 1;
      this.TimeOfDayLabel.Text = "Time Of Day";
      this.TimeOfDayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ShaderEditorForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 668);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ShaderEditorForm";
      this.Text = "Shader Editor";
      this.Load += new System.EventHandler(this.ShaderEditorForm_Load);
      this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ShaderEditor)).EndInit();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.tableLayoutPanel3.ResumeLayout(false);
      this.tableLayoutPanel3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.TimeOfDay)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private FastColoredTextBoxNS.FastColoredTextBox ShaderEditor;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.ComboBox ShaderCombo;
    private System.Windows.Forms.Button UpdateButton;
    private System.Windows.Forms.TextBox ResultBox;
    private System.Windows.Forms.RadioButton VertexMode;
    private System.Windows.Forms.RadioButton EvalMode;
    private System.Windows.Forms.RadioButton ControlMode;
    private System.Windows.Forms.RadioButton FragmentMode;
    private System.Windows.Forms.Button RevertButton;
    private System.Windows.Forms.RadioButton FragmentFunctions;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.Windows.Forms.TrackBar TimeOfDay;
    private System.Windows.Forms.Label TimeOfDayLabel;
  }
}