namespace ShaderPlay
{
  partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.VertexEditor = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.FragmentShaderEditor = new FastColoredTextBoxNS.FastColoredTextBox();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.settingsControl1 = new ShaderPlay.SettingsControl();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.ShowNormalsCheck = new System.Windows.Forms.CheckBox();
      this.RotationCheck = new System.Windows.Forms.CheckBox();
      this.ResultBox = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.TriangleButton = new System.Windows.Forms.RadioButton();
      this.CubeButton = new System.Windows.Forms.RadioButton();
      this.SphereButton = new System.Windows.Forms.RadioButton();
      this.PlaneButton = new System.Windows.Forms.RadioButton();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.VertexEditor)).BeginInit();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.FragmentShaderEditor)).BeginInit();
      this.tabPage5.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.13793F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.86207F));
      this.tableLayoutPanel1.Controls.Add(this.tabControl1, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.ResultBox, 1, 2);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1160, 567);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage5);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(515, 35);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(642, 429);
      this.tabControl1.TabIndex = 6;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.VertexEditor);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(634, 403);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Vertex Shader";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // VertexEditor
      // 
      this.VertexEditor.AutoCompleteBracketsList = new char[] {
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
      this.VertexEditor.AutoScrollMinSize = new System.Drawing.Size(123, 14);
      this.VertexEditor.BackBrush = null;
      this.VertexEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.VertexEditor.CaretColor = System.Drawing.Color.LightSalmon;
      this.VertexEditor.CharHeight = 14;
      this.VertexEditor.CharWidth = 8;
      this.VertexEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.VertexEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.VertexEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.VertexEditor.ForeColor = System.Drawing.Color.White;
      this.VertexEditor.IsReplaceMode = false;
      this.VertexEditor.Location = new System.Drawing.Point(3, 3);
      this.VertexEditor.Name = "VertexEditor";
      this.VertexEditor.Paddings = new System.Windows.Forms.Padding(0);
      this.VertexEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.VertexEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("VertexEditor.ServiceColors")));
      this.VertexEditor.Size = new System.Drawing.Size(628, 397);
      this.VertexEditor.TabIndex = 5;
      this.VertexEditor.Text = "VertexEditor";
      this.VertexEditor.Zoom = 100;
      this.VertexEditor.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.VertexEditor_TextChanged);
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.FragmentShaderEditor);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(634, 403);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Fragment Shader";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // FragmentShaderEditor
      // 
      this.FragmentShaderEditor.AutoCompleteBracketsList = new char[] {
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
      this.FragmentShaderEditor.AutoScrollMinSize = new System.Drawing.Size(98, 14);
      this.FragmentShaderEditor.BackBrush = null;
      this.FragmentShaderEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.FragmentShaderEditor.CaretColor = System.Drawing.Color.LightSalmon;
      this.FragmentShaderEditor.CharHeight = 14;
      this.FragmentShaderEditor.CharWidth = 8;
      this.FragmentShaderEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.FragmentShaderEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.FragmentShaderEditor.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FragmentShaderEditor.ForeColor = System.Drawing.Color.White;
      this.FragmentShaderEditor.IsReplaceMode = false;
      this.FragmentShaderEditor.Location = new System.Drawing.Point(3, 3);
      this.FragmentShaderEditor.Name = "FragmentShaderEditor";
      this.FragmentShaderEditor.Paddings = new System.Windows.Forms.Padding(0);
      this.FragmentShaderEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.FragmentShaderEditor.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("FragmentShaderEditor.ServiceColors")));
      this.FragmentShaderEditor.Size = new System.Drawing.Size(628, 397);
      this.FragmentShaderEditor.TabIndex = 1;
      this.FragmentShaderEditor.Text = "ShaderEditor";
      this.FragmentShaderEditor.Zoom = 100;
      this.FragmentShaderEditor.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.FragmentShaderEditor_TextChanged);
      // 
      // tabPage3
      // 
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(634, 403);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Geometry";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Size = new System.Drawing.Size(634, 403);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Tessellation";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // tabPage5
      // 
      this.tabPage5.Controls.Add(this.settingsControl1);
      this.tabPage5.Location = new System.Drawing.Point(4, 22);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Size = new System.Drawing.Size(634, 403);
      this.tabPage5.TabIndex = 4;
      this.tabPage5.Text = "Setup";
      this.tabPage5.UseVisualStyleBackColor = true;
      // 
      // settingsControl1
      // 
      this.settingsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.settingsControl1.Location = new System.Drawing.Point(0, 0);
      this.settingsControl1.Name = "settingsControl1";
      this.settingsControl1.Size = new System.Drawing.Size(634, 403);
      this.settingsControl1.TabIndex = 0;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.ShowNormalsCheck);
      this.flowLayoutPanel1.Controls.Add(this.RotationCheck);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(506, 26);
      this.flowLayoutPanel1.TabIndex = 2;
      // 
      // ShowNormalsCheck
      // 
      this.ShowNormalsCheck.AutoSize = true;
      this.ShowNormalsCheck.Location = new System.Drawing.Point(3, 3);
      this.ShowNormalsCheck.Name = "ShowNormalsCheck";
      this.ShowNormalsCheck.Size = new System.Drawing.Size(94, 17);
      this.ShowNormalsCheck.TabIndex = 0;
      this.ShowNormalsCheck.Text = "Show Normals";
      this.ShowNormalsCheck.UseVisualStyleBackColor = true;
      this.ShowNormalsCheck.CheckedChanged += new System.EventHandler(this.ShowNormalsCheck_CheckedChanged);
      // 
      // RotationCheck
      // 
      this.RotationCheck.AutoSize = true;
      this.RotationCheck.Checked = true;
      this.RotationCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.RotationCheck.Location = new System.Drawing.Point(103, 3);
      this.RotationCheck.Name = "RotationCheck";
      this.RotationCheck.Size = new System.Drawing.Size(107, 17);
      this.RotationCheck.TabIndex = 1;
      this.RotationCheck.Text = "Animate Rotation";
      this.RotationCheck.UseVisualStyleBackColor = true;
      this.RotationCheck.CheckedChanged += new System.EventHandler(this.RotationCheck_CheckedChanged);
      // 
      // ResultBox
      // 
      this.ResultBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ResultBox.Location = new System.Drawing.Point(515, 470);
      this.ResultBox.Multiline = true;
      this.ResultBox.Name = "ResultBox";
      this.ResultBox.Size = new System.Drawing.Size(642, 94);
      this.ResultBox.TabIndex = 7;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 4;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel2.Controls.Add(this.TriangleButton, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.CubeButton, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this.SphereButton, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.PlaneButton, 3, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 470);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 3;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(506, 94);
      this.tableLayoutPanel2.TabIndex = 8;
      // 
      // TriangleButton
      // 
      this.TriangleButton.AutoSize = true;
      this.TriangleButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TriangleButton.Location = new System.Drawing.Point(3, 3);
      this.TriangleButton.Name = "TriangleButton";
      this.TriangleButton.Size = new System.Drawing.Size(120, 25);
      this.TriangleButton.TabIndex = 0;
      this.TriangleButton.TabStop = true;
      this.TriangleButton.Text = "Triangle";
      this.TriangleButton.UseVisualStyleBackColor = true;
      this.TriangleButton.CheckedChanged += new System.EventHandler(this.TriangleButton_CheckedChanged);
      // 
      // CubeButton
      // 
      this.CubeButton.AutoSize = true;
      this.CubeButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CubeButton.Location = new System.Drawing.Point(129, 3);
      this.CubeButton.Name = "CubeButton";
      this.CubeButton.Size = new System.Drawing.Size(120, 25);
      this.CubeButton.TabIndex = 1;
      this.CubeButton.TabStop = true;
      this.CubeButton.Text = "Cube";
      this.CubeButton.UseVisualStyleBackColor = true;
      this.CubeButton.CheckedChanged += new System.EventHandler(this.CubeButton_CheckedChanged);
      // 
      // SphereButton
      // 
      this.SphereButton.AutoSize = true;
      this.SphereButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SphereButton.Location = new System.Drawing.Point(255, 3);
      this.SphereButton.Name = "SphereButton";
      this.SphereButton.Size = new System.Drawing.Size(120, 25);
      this.SphereButton.TabIndex = 2;
      this.SphereButton.TabStop = true;
      this.SphereButton.Text = "Sphere";
      this.SphereButton.UseVisualStyleBackColor = true;
      this.SphereButton.CheckedChanged += new System.EventHandler(this.SphereButton_CheckedChanged);
      // 
      // PlaneButton
      // 
      this.PlaneButton.AutoSize = true;
      this.PlaneButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PlaneButton.Location = new System.Drawing.Point(381, 3);
      this.PlaneButton.Name = "PlaneButton";
      this.PlaneButton.Size = new System.Drawing.Size(122, 25);
      this.PlaneButton.TabIndex = 3;
      this.PlaneButton.TabStop = true;
      this.PlaneButton.Text = "Plane";
      this.PlaneButton.UseVisualStyleBackColor = true;
      this.PlaneButton.CheckedChanged += new System.EventHandler(this.PlaneButton_CheckedChanged);
      // 
      // timer1
      // 
      this.timer1.Interval = 40;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1160, 567);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "ShaderPlay";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.VertexEditor)).EndInit();
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.FragmentShaderEditor)).EndInit();
      this.tabPage5.ResumeLayout(false);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.CheckBox ShowNormalsCheck;
    private System.Windows.Forms.CheckBox RotationCheck;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private FastColoredTextBoxNS.FastColoredTextBox VertexEditor;
    private System.Windows.Forms.TabPage tabPage2;
    private FastColoredTextBoxNS.FastColoredTextBox FragmentShaderEditor;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TextBox ResultBox;
    private System.Windows.Forms.TabPage tabPage5;
    private SettingsControl settingsControl1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.RadioButton TriangleButton;
    private System.Windows.Forms.RadioButton CubeButton;
    private System.Windows.Forms.RadioButton SphereButton;
    private System.Windows.Forms.RadioButton PlaneButton;
  }
}

