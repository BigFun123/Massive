namespace Massive.Server
{
  partial class MainWindow
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
      this.NetworkTimer = new System.Windows.Forms.Timer(this.components);
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.MaxConnectionsLabel = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.RegisterLobbyButton = new System.Windows.Forms.Button();
      this.PublicIPBox = new System.Windows.Forms.TextBox();
      this.PublicIPLabel = new System.Windows.Forms.Label();
      this.ChatBox = new System.Windows.Forms.TextBox();
      this.FlushButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.StartButton = new System.Windows.Forms.Button();
      this.PortBox = new System.Windows.Forms.TextBox();
      this.StopButton = new System.Windows.Forms.Button();
      this.TestButton = new System.Windows.Forms.Button();
      this.IPAddressBox = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.ShowLogButton = new System.Windows.Forms.Button();
      this.ClearLog = new System.Windows.Forms.Button();
      this.ZoneList = new System.Windows.Forms.ListBox();
      this.label3 = new System.Windows.Forms.Label();
      this.UniverseList = new System.Windows.Forms.ListBox();
      this.ObjectCountLabel = new System.Windows.Forms.Label();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
      this.AliveMeter = new System.Windows.Forms.ProgressBar();
      this.DatabaseButton = new System.Windows.Forms.Button();
      this.ConnectionsList = new System.Windows.Forms.ListView();
      this.ipaddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.userid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.username = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.objects = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.maxobjects = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.state = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.BackupTimer = new System.Windows.Forms.Timer(this.components);
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.postgresDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.mUserAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.CMDTimer = new System.Windows.Forms.Timer(this.components);
      this.ShowDebug = new System.Windows.Forms.CheckBox();
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tableLayoutPanel3.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.postgresDataSetBindingSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.mUserAccountBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // NetworkTimer
      // 
      this.NetworkTimer.Interval = 1000;
      this.NetworkTimer.Tick += new System.EventHandler(this.NetworkTimer_Tick);
      // 
      // timer1
      // 
      this.timer1.Interval = 4000;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 7;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
      this.tableLayoutPanel1.Controls.Add(this.MaxConnectionsLabel, 5, 0);
      this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.ZoneList, 2, 1);
      this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
      this.tableLayoutPanel1.Controls.Add(this.UniverseList, 3, 1);
      this.tableLayoutPanel1.Controls.Add(this.ObjectCountLabel, 2, 0);
      this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.AliveMeter, 6, 0);
      this.tableLayoutPanel1.Controls.Add(this.DatabaseButton, 3, 0);
      this.tableLayoutPanel1.Controls.Add(this.ConnectionsList, 4, 1);
      this.tableLayoutPanel1.Controls.Add(this.ShowDebug, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1128, 445);
      this.tableLayoutPanel1.TabIndex = 9;
      // 
      // MaxConnectionsLabel
      // 
      this.MaxConnectionsLabel.AutoSize = true;
      this.MaxConnectionsLabel.BackColor = System.Drawing.Color.Silver;
      this.MaxConnectionsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MaxConnectionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaxConnectionsLabel.ForeColor = System.Drawing.Color.White;
      this.MaxConnectionsLabel.Location = new System.Drawing.Point(944, 0);
      this.MaxConnectionsLabel.Margin = new System.Windows.Forms.Padding(0);
      this.MaxConnectionsLabel.Name = "MaxConnectionsLabel";
      this.MaxConnectionsLabel.Size = new System.Drawing.Size(120, 32);
      this.MaxConnectionsLabel.TabIndex = 22;
      this.MaxConnectionsLabel.Text = "Max Connections 100";
      this.MaxConnectionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.MaxConnectionsLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaxConnectionsLabel_MouseClick);
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.RegisterLobbyButton);
      this.panel1.Controls.Add(this.PublicIPBox);
      this.panel1.Controls.Add(this.PublicIPLabel);
      this.panel1.Controls.Add(this.ChatBox);
      this.panel1.Controls.Add(this.FlushButton);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.StartButton);
      this.panel1.Controls.Add(this.PortBox);
      this.panel1.Controls.Add(this.StopButton);
      this.panel1.Controls.Add(this.TestButton);
      this.panel1.Controls.Add(this.IPAddressBox);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(3, 35);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(314, 375);
      this.panel1.TabIndex = 20;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.ForeColor = System.Drawing.Color.White;
      this.label4.Location = new System.Drawing.Point(10, 219);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(99, 13);
      this.label4.TabIndex = 17;
      this.label4.Text = "Send Chat Text:";
      // 
      // RegisterLobbyButton
      // 
      this.RegisterLobbyButton.BackColor = System.Drawing.Color.Gold;
      this.RegisterLobbyButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RegisterLobbyButton.Location = new System.Drawing.Point(77, 132);
      this.RegisterLobbyButton.Name = "RegisterLobbyButton";
      this.RegisterLobbyButton.Size = new System.Drawing.Size(111, 39);
      this.RegisterLobbyButton.TabIndex = 16;
      this.RegisterLobbyButton.Text = "Register Lobby";
      this.toolTip1.SetToolTip(this.RegisterLobbyButton, "Register/Update this server with the main lobby");
      this.RegisterLobbyButton.UseVisualStyleBackColor = false;
      this.RegisterLobbyButton.Click += new System.EventHandler(this.RegisterLobbyButton_Click);
      // 
      // PublicIPBox
      // 
      this.PublicIPBox.Location = new System.Drawing.Point(10, 68);
      this.PublicIPBox.Name = "PublicIPBox";
      this.PublicIPBox.ReadOnly = true;
      this.PublicIPBox.Size = new System.Drawing.Size(178, 20);
      this.PublicIPBox.TabIndex = 15;
      // 
      // PublicIPLabel
      // 
      this.PublicIPLabel.AutoSize = true;
      this.PublicIPLabel.ForeColor = System.Drawing.Color.White;
      this.PublicIPLabel.Location = new System.Drawing.Point(9, 51);
      this.PublicIPLabel.Name = "PublicIPLabel";
      this.PublicIPLabel.Size = new System.Drawing.Size(49, 13);
      this.PublicIPLabel.TabIndex = 14;
      this.PublicIPLabel.Text = "PublicIP:";
      // 
      // ChatBox
      // 
      this.ChatBox.Location = new System.Drawing.Point(13, 235);
      this.ChatBox.Multiline = true;
      this.ChatBox.Name = "ChatBox";
      this.ChatBox.Size = new System.Drawing.Size(281, 87);
      this.ChatBox.TabIndex = 13;
      this.ChatBox.Text = "Server is updating. Please close and restart to install updates.";
      // 
      // FlushButton
      // 
      this.FlushButton.BackColor = System.Drawing.Color.Gold;
      this.FlushButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FlushButton.Location = new System.Drawing.Point(233, 177);
      this.FlushButton.Name = "FlushButton";
      this.FlushButton.Size = new System.Drawing.Size(65, 39);
      this.FlushButton.TabIndex = 12;
      this.FlushButton.Text = "Flush!";
      this.toolTip1.SetToolTip(this.FlushButton, "WARNING! DELETES all NON-STATIC Objects including Player AVATARS");
      this.FlushButton.UseVisualStyleBackColor = false;
      this.FlushButton.Click += new System.EventHandler(this.FlushButton_Click_1);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.Color.White;
      this.label1.Location = new System.Drawing.Point(6, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(96, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "LAN IP Address";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.White;
      this.label2.Location = new System.Drawing.Point(6, 90);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(30, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Port";
      // 
      // StartButton
      // 
      this.StartButton.BackColor = System.Drawing.Color.LimeGreen;
      this.StartButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StartButton.Location = new System.Drawing.Point(10, 132);
      this.StartButton.Name = "StartButton";
      this.StartButton.Size = new System.Drawing.Size(64, 39);
      this.StartButton.TabIndex = 0;
      this.StartButton.Text = "Start";
      this.StartButton.UseVisualStyleBackColor = false;
      // 
      // PortBox
      // 
      this.PortBox.Location = new System.Drawing.Point(10, 108);
      this.PortBox.Name = "PortBox";
      this.PortBox.Size = new System.Drawing.Size(178, 20);
      this.PortBox.TabIndex = 6;
      // 
      // StopButton
      // 
      this.StopButton.BackColor = System.Drawing.Color.Crimson;
      this.StopButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StopButton.Location = new System.Drawing.Point(233, 132);
      this.StopButton.Name = "StopButton";
      this.StopButton.Size = new System.Drawing.Size(65, 39);
      this.StopButton.TabIndex = 1;
      this.StopButton.Text = "Stop";
      this.StopButton.UseVisualStyleBackColor = false;
      // 
      // TestButton
      // 
      this.TestButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
      this.TestButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.TestButton.Location = new System.Drawing.Point(229, 328);
      this.TestButton.Name = "TestButton";
      this.TestButton.Size = new System.Drawing.Size(65, 39);
      this.TestButton.TabIndex = 3;
      this.TestButton.Text = "Chat";
      this.toolTip1.SetToolTip(this.TestButton, "Send the text to All users as a chat message");
      this.TestButton.UseVisualStyleBackColor = false;
      this.TestButton.Click += new System.EventHandler(this.TestButton_Click_1);
      // 
      // IPAddressBox
      // 
      this.IPAddressBox.Location = new System.Drawing.Point(10, 24);
      this.IPAddressBox.Name = "IPAddressBox";
      this.IPAddressBox.Size = new System.Drawing.Size(178, 20);
      this.IPAddressBox.TabIndex = 4;
      // 
      // tableLayoutPanel3
      // 
      this.tableLayoutPanel3.ColumnCount = 3;
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
      this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Controls.Add(this.ShowLogButton, 0, 0);
      this.tableLayoutPanel3.Controls.Add(this.ClearLog, 1, 0);
      this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel3.Name = "tableLayoutPanel3";
      this.tableLayoutPanel3.RowCount = 1;
      this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 32);
      this.tableLayoutPanel3.TabIndex = 13;
      // 
      // ShowLogButton
      // 
      this.ShowLogButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ShowLogButton.Location = new System.Drawing.Point(0, 0);
      this.ShowLogButton.Margin = new System.Windows.Forms.Padding(0);
      this.ShowLogButton.Name = "ShowLogButton";
      this.ShowLogButton.Size = new System.Drawing.Size(100, 32);
      this.ShowLogButton.TabIndex = 0;
      this.ShowLogButton.Text = "Show Legend";
      this.ShowLogButton.UseVisualStyleBackColor = true;
      this.ShowLogButton.Click += new System.EventHandler(this.ShowLogButton_Click);
      // 
      // ClearLog
      // 
      this.ClearLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ClearLog.Location = new System.Drawing.Point(100, 0);
      this.ClearLog.Margin = new System.Windows.Forms.Padding(0);
      this.ClearLog.Name = "ClearLog";
      this.ClearLog.Size = new System.Drawing.Size(100, 32);
      this.ClearLog.TabIndex = 1;
      this.ClearLog.Text = "Clear Console";
      this.ClearLog.UseVisualStyleBackColor = true;
      this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
      // 
      // ZoneList
      // 
      this.ZoneList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ZoneList.FormattingEnabled = true;
      this.ZoneList.Location = new System.Drawing.Point(323, 35);
      this.ZoneList.Name = "ZoneList";
      this.ZoneList.Size = new System.Drawing.Size(94, 375);
      this.ZoneList.TabIndex = 12;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.BackColor = System.Drawing.Color.Silver;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.White;
      this.label3.Location = new System.Drawing.Point(523, 0);
      this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
      this.label3.Name = "label3";
      this.label3.Padding = new System.Windows.Forms.Padding(2);
      this.label3.Size = new System.Drawing.Size(421, 32);
      this.label3.TabIndex = 9;
      this.label3.Text = "Connections";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // UniverseList
      // 
      this.UniverseList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.UniverseList.FormattingEnabled = true;
      this.UniverseList.Location = new System.Drawing.Point(423, 35);
      this.UniverseList.Name = "UniverseList";
      this.UniverseList.Size = new System.Drawing.Size(94, 375);
      this.UniverseList.TabIndex = 9;
      this.UniverseList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UniverseList_MouseDoubleClick);
      // 
      // ObjectCountLabel
      // 
      this.ObjectCountLabel.AutoSize = true;
      this.ObjectCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ObjectCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ObjectCountLabel.Location = new System.Drawing.Point(323, 0);
      this.ObjectCountLabel.Name = "ObjectCountLabel";
      this.ObjectCountLabel.Size = new System.Drawing.Size(94, 32);
      this.ObjectCountLabel.TabIndex = 17;
      this.ObjectCountLabel.Text = "0";
      this.ObjectCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // statusStrip1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.statusStrip1, 6);
      this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText});
      this.statusStrip1.Location = new System.Drawing.Point(0, 413);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(1064, 32);
      this.statusStrip1.TabIndex = 23;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // StatusText
      // 
      this.StatusText.Name = "StatusText";
      this.StatusText.Size = new System.Drawing.Size(39, 27);
      this.StatusText.Text = "Ready";
      // 
      // AliveMeter
      // 
      this.AliveMeter.BackColor = System.Drawing.Color.Silver;
      this.AliveMeter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.AliveMeter.Location = new System.Drawing.Point(1064, 0);
      this.AliveMeter.Margin = new System.Windows.Forms.Padding(0);
      this.AliveMeter.Name = "AliveMeter";
      this.AliveMeter.Size = new System.Drawing.Size(64, 32);
      this.AliveMeter.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.AliveMeter.TabIndex = 24;
      // 
      // DatabaseButton
      // 
      this.DatabaseButton.Dock = System.Windows.Forms.DockStyle.Fill;
      this.DatabaseButton.Location = new System.Drawing.Point(420, 0);
      this.DatabaseButton.Margin = new System.Windows.Forms.Padding(0);
      this.DatabaseButton.Name = "DatabaseButton";
      this.DatabaseButton.Size = new System.Drawing.Size(100, 32);
      this.DatabaseButton.TabIndex = 25;
      this.DatabaseButton.Text = "DataBase...";
      this.DatabaseButton.UseVisualStyleBackColor = true;
      this.DatabaseButton.Click += new System.EventHandler(this.DatabaseButton_Click);
      // 
      // ConnectionsList
      // 
      this.ConnectionsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ipaddress,
            this.userid,
            this.email,
            this.username,
            this.objects,
            this.maxobjects,
            this.state});
      this.tableLayoutPanel1.SetColumnSpan(this.ConnectionsList, 3);
      this.ConnectionsList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ConnectionsList.Location = new System.Drawing.Point(523, 35);
      this.ConnectionsList.Name = "ConnectionsList";
      this.ConnectionsList.Size = new System.Drawing.Size(602, 375);
      this.ConnectionsList.TabIndex = 26;
      this.ConnectionsList.UseCompatibleStateImageBehavior = false;
      this.ConnectionsList.View = System.Windows.Forms.View.Details;
      // 
      // ipaddress
      // 
      this.ipaddress.Text = "ipaddress";
      // 
      // userid
      // 
      this.userid.Text = "ID";
      this.userid.Width = 176;
      // 
      // email
      // 
      this.email.Text = "email";
      // 
      // username
      // 
      this.username.Text = "username";
      // 
      // objects
      // 
      this.objects.Text = "objects";
      // 
      // maxobjects
      // 
      this.maxobjects.Text = "max";
      // 
      // state
      // 
      this.state.Text = "state";
      // 
      // BackupTimer
      // 
      this.BackupTimer.Interval = 3600000;
      this.BackupTimer.Tick += new System.EventHandler(this.BackupTimer_Tick);
      // 
      // CMDTimer
      // 
      this.CMDTimer.Interval = 5000;
      this.CMDTimer.Tick += new System.EventHandler(this.CMDTimer_Tick);
      // 
      // ShowDebug
      // 
      this.ShowDebug.AutoSize = true;
      this.ShowDebug.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ShowDebug.Location = new System.Drawing.Point(203, 3);
      this.ShowDebug.Name = "ShowDebug";
      this.ShowDebug.Size = new System.Drawing.Size(114, 26);
      this.ShowDebug.TabIndex = 27;
      this.ShowDebug.Text = "Display Messages";
      this.ShowDebug.UseVisualStyleBackColor = true;
      this.ShowDebug.CheckedChanged += new System.EventHandler(this.ShowDebug_CheckedChanged);
      // 
      // MainWindow
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1128, 445);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainWindow";
      this.Text = "MASSIVE Server";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
      this.Load += new System.EventHandler(this.MainWindow_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.tableLayoutPanel3.ResumeLayout(false);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.postgresDataSetBindingSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.mUserAccountBindingSource)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Timer NetworkTimer;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ListBox UniverseList;
    private System.Windows.Forms.ListBox ZoneList;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    private System.Windows.Forms.Button ShowLogButton;
    private System.Windows.Forms.Button ClearLog;
    private System.Windows.Forms.Timer BackupTimer;
    private System.Windows.Forms.Label ObjectCountLabel;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button FlushButton;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button StartButton;
    private System.Windows.Forms.TextBox PortBox;
    private System.Windows.Forms.Button StopButton;
    private System.Windows.Forms.Button TestButton;
    private System.Windows.Forms.TextBox IPAddressBox;
    private System.Windows.Forms.TextBox ChatBox;
    private System.Windows.Forms.Label MaxConnectionsLabel;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel StatusText;
    private System.Windows.Forms.ProgressBar AliveMeter;
    private System.Windows.Forms.Button DatabaseButton;
    private System.Windows.Forms.BindingSource mUserAccountBindingSource;
    private System.Windows.Forms.BindingSource postgresDataSetBindingSource;
    private System.Windows.Forms.ListView ConnectionsList;
    private System.Windows.Forms.ColumnHeader userid;
    private System.Windows.Forms.ColumnHeader username;
    private System.Windows.Forms.ColumnHeader objects;
    private System.Windows.Forms.ColumnHeader maxobjects;
    private System.Windows.Forms.ColumnHeader state;
    private System.Windows.Forms.ColumnHeader ipaddress;
    private System.Windows.Forms.ColumnHeader email;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Label PublicIPLabel;
    private System.Windows.Forms.TextBox PublicIPBox;
    private System.Windows.Forms.Button RegisterLobbyButton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Timer CMDTimer;
    private System.Windows.Forms.CheckBox ShowDebug;
  }
}

