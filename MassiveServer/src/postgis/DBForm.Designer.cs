namespace MassiveServer.src.postgis
{
  partial class DBForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBForm));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.CodeBox = new FastColoredTextBoxNS.FastColoredTextBox();
      this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
      this.ResultBox = new System.Windows.Forms.TextBox();
      this.ExecuteButton = new System.Windows.Forms.Button();
      this.ResultsView = new System.Windows.Forms.DataGridView();
      this.postgresDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
      
      this.objectsBindingSource = new System.Windows.Forms.BindingSource(this.components);      
      this.objectsBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
      this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.instanceidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.owneridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.templateidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.textureidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.staticstorageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.geomDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.radiusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.datecreatedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.datemodifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CodeBox)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ResultsView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.postgresDataSetBindingSource)).BeginInit();      
      ((System.ComponentModel.ISupportInitialize)(this.objectsBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.objectsBindingSource1)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.93687F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.06313F));
      this.tableLayoutPanel1.Controls.Add(this.CodeBox, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.documentMap1, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.ResultBox, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.ExecuteButton, 1, 0);
      this.tableLayoutPanel1.Controls.Add(this.ResultsView, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 255F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 547);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // CodeBox
      // 
      this.CodeBox.AutoCompleteBracketsList = new char[] {
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
      this.CodeBox.AutoIndentCharsPatterns = "";
      this.CodeBox.AutoScrollMinSize = new System.Drawing.Size(0, 14);
      this.CodeBox.BackBrush = null;
      this.CodeBox.CharHeight = 14;
      this.CodeBox.CharWidth = 8;
      this.CodeBox.CommentPrefix = "--";
      this.CodeBox.Cursor = System.Windows.Forms.Cursors.IBeam;
      this.CodeBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
      this.CodeBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.CodeBox.IsReplaceMode = false;
      this.CodeBox.Language = FastColoredTextBoxNS.Language.SQL;
      this.CodeBox.LeftBracket = '(';
      this.CodeBox.Location = new System.Drawing.Point(3, 35);
      this.CodeBox.Name = "CodeBox";
      this.CodeBox.Paddings = new System.Windows.Forms.Padding(0);
      this.CodeBox.RightBracket = ')';
      this.CodeBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
      this.CodeBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("CodeBox.ServiceColors")));
      this.CodeBox.Size = new System.Drawing.Size(880, 190);
      this.CodeBox.TabIndex = 0;
      this.CodeBox.WordWrap = true;
      this.CodeBox.WordWrapIndent = 5;
      this.CodeBox.Zoom = 100;
      this.CodeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeBox_KeyDown);
      // 
      // documentMap1
      // 
      this.documentMap1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
      this.documentMap1.Location = new System.Drawing.Point(889, 35);
      this.documentMap1.Name = "documentMap1";
      this.documentMap1.Size = new System.Drawing.Size(116, 190);
      this.documentMap1.TabIndex = 1;
      this.documentMap1.Target = this.CodeBox;
      this.documentMap1.Text = "documentMap1";
      // 
      // ResultBox
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.ResultBox, 2);
      this.ResultBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ResultBox.Location = new System.Drawing.Point(3, 486);
      this.ResultBox.Multiline = true;
      this.ResultBox.Name = "ResultBox";
      this.ResultBox.Size = new System.Drawing.Size(1002, 58);
      this.ResultBox.TabIndex = 2;
      // 
      // ExecuteButton
      // 
      this.ExecuteButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ExecuteButton.Location = new System.Drawing.Point(889, 3);
      this.ExecuteButton.Name = "ExecuteButton";
      this.ExecuteButton.Size = new System.Drawing.Size(116, 26);
      this.ExecuteButton.TabIndex = 3;
      this.ExecuteButton.Text = "F5 Execute";
      this.ExecuteButton.UseVisualStyleBackColor = true;
      this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
      // 
      // ResultsView
      // 
      this.ResultsView.AutoGenerateColumns = false;
      this.ResultsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.ResultsView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.instanceidDataGridViewTextBoxColumn,
            this.owneridDataGridViewTextBoxColumn,
            this.templateidDataGridViewTextBoxColumn,
            this.textureidDataGridViewTextBoxColumn,
            this.staticstorageDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.tagDataGridViewTextBoxColumn,
            this.geomDataGridViewTextBoxColumn,
            this.radiusDataGridViewTextBoxColumn,
            this.datecreatedDataGridViewTextBoxColumn,
            this.datemodifiedDataGridViewTextBoxColumn});
      this.tableLayoutPanel1.SetColumnSpan(this.ResultsView, 2);
      this.ResultsView.DataMember = "objects";
      this.ResultsView.DataSource = this.postgresDataSetBindingSource;
      this.ResultsView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ResultsView.Location = new System.Drawing.Point(3, 231);
      this.ResultsView.Name = "ResultsView";
      this.ResultsView.Size = new System.Drawing.Size(1002, 249);
      this.ResultsView.TabIndex = 4;      
      // 
      // objectsBindingSource
      // 
      this.objectsBindingSource.DataMember = "objects";
      this.objectsBindingSource.DataSource = this.postgresDataSetBindingSource;
      // 
      // objectsBindingSource1
      // 
      this.objectsBindingSource1.DataMember = "objects";
      this.objectsBindingSource1.DataSource = this.postgresDataSetBindingSource;
      // 
      // idDataGridViewTextBoxColumn
      // 
      this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
      this.idDataGridViewTextBoxColumn.HeaderText = "id";
      this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
      // 
      // instanceidDataGridViewTextBoxColumn
      // 
      this.instanceidDataGridViewTextBoxColumn.DataPropertyName = "instanceid";
      this.instanceidDataGridViewTextBoxColumn.HeaderText = "instanceid";
      this.instanceidDataGridViewTextBoxColumn.Name = "instanceidDataGridViewTextBoxColumn";
      // 
      // owneridDataGridViewTextBoxColumn
      // 
      this.owneridDataGridViewTextBoxColumn.DataPropertyName = "ownerid";
      this.owneridDataGridViewTextBoxColumn.HeaderText = "ownerid";
      this.owneridDataGridViewTextBoxColumn.Name = "owneridDataGridViewTextBoxColumn";
      // 
      // templateidDataGridViewTextBoxColumn
      // 
      this.templateidDataGridViewTextBoxColumn.DataPropertyName = "templateid";
      this.templateidDataGridViewTextBoxColumn.HeaderText = "templateid";
      this.templateidDataGridViewTextBoxColumn.Name = "templateidDataGridViewTextBoxColumn";
      // 
      // textureidDataGridViewTextBoxColumn
      // 
      this.textureidDataGridViewTextBoxColumn.DataPropertyName = "textureid";
      this.textureidDataGridViewTextBoxColumn.HeaderText = "textureid";
      this.textureidDataGridViewTextBoxColumn.Name = "textureidDataGridViewTextBoxColumn";
      // 
      // staticstorageDataGridViewTextBoxColumn
      // 
      this.staticstorageDataGridViewTextBoxColumn.DataPropertyName = "staticstorage";
      this.staticstorageDataGridViewTextBoxColumn.HeaderText = "staticstorage";
      this.staticstorageDataGridViewTextBoxColumn.Name = "staticstorageDataGridViewTextBoxColumn";
      // 
      // nameDataGridViewTextBoxColumn
      // 
      this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
      this.nameDataGridViewTextBoxColumn.HeaderText = "name";
      this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
      // 
      // descriptionDataGridViewTextBoxColumn
      // 
      this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "description";
      this.descriptionDataGridViewTextBoxColumn.HeaderText = "description";
      this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
      // 
      // typeDataGridViewTextBoxColumn
      // 
      this.typeDataGridViewTextBoxColumn.DataPropertyName = "type";
      this.typeDataGridViewTextBoxColumn.HeaderText = "type";
      this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
      // 
      // tagDataGridViewTextBoxColumn
      // 
      this.tagDataGridViewTextBoxColumn.DataPropertyName = "tag";
      this.tagDataGridViewTextBoxColumn.HeaderText = "tag";
      this.tagDataGridViewTextBoxColumn.Name = "tagDataGridViewTextBoxColumn";
      // 
      // geomDataGridViewTextBoxColumn
      // 
      this.geomDataGridViewTextBoxColumn.DataPropertyName = "geom";
      this.geomDataGridViewTextBoxColumn.HeaderText = "geom";
      this.geomDataGridViewTextBoxColumn.Name = "geomDataGridViewTextBoxColumn";
      // 
      // radiusDataGridViewTextBoxColumn
      // 
      this.radiusDataGridViewTextBoxColumn.DataPropertyName = "radius";
      this.radiusDataGridViewTextBoxColumn.HeaderText = "radius";
      this.radiusDataGridViewTextBoxColumn.Name = "radiusDataGridViewTextBoxColumn";
      // 
      // datecreatedDataGridViewTextBoxColumn
      // 
      this.datecreatedDataGridViewTextBoxColumn.DataPropertyName = "date_created";
      this.datecreatedDataGridViewTextBoxColumn.HeaderText = "date_created";
      this.datecreatedDataGridViewTextBoxColumn.Name = "datecreatedDataGridViewTextBoxColumn";
      // 
      // datemodifiedDataGridViewTextBoxColumn
      // 
      this.datemodifiedDataGridViewTextBoxColumn.DataPropertyName = "date_modified";
      this.datemodifiedDataGridViewTextBoxColumn.HeaderText = "date_modified";
      this.datemodifiedDataGridViewTextBoxColumn.Name = "datemodifiedDataGridViewTextBoxColumn";
      // 
      // DBForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1008, 547);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "DBForm";
      this.Text = "DBForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DBForm_FormClosing);
      this.Load += new System.EventHandler(this.DBForm_Load);
      this.Shown += new System.EventHandler(this.DBForm_Shown);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.CodeBox)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ResultsView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.postgresDataSetBindingSource)).EndInit();      
      ((System.ComponentModel.ISupportInitialize)(this.objectsBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.objectsBindingSource1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private FastColoredTextBoxNS.FastColoredTextBox CodeBox;
    private FastColoredTextBoxNS.DocumentMap documentMap1;
    private System.Windows.Forms.TextBox ResultBox;
    private System.Windows.Forms.Button ExecuteButton;
    private System.Windows.Forms.DataGridView ResultsView;
    private System.Windows.Forms.BindingSource postgresDataSetBindingSource;    
    private System.Windows.Forms.BindingSource objectsBindingSource;    
    private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn instanceidDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn owneridDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn templateidDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn textureidDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn staticstorageDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn geomDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn radiusDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn datecreatedDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn datemodifiedDataGridViewTextBoxColumn;
    private System.Windows.Forms.BindingSource objectsBindingSource1;
  }
}