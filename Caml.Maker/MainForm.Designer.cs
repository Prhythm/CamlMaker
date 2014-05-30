namespace Caml.Maker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.cmbUrl = new System.Windows.Forms.ComboBox();
            this.plConnectMethod = new System.Windows.Forms.Panel();
            this.rbConnectViaObjectModel = new System.Windows.Forms.RadioButton();
            this.rbConnectViaWebService = new System.Windows.Forms.RadioButton();
            this.lbUrl = new System.Windows.Forms.Label();
            this.rbCustomCredential = new System.Windows.Forms.RadioButton();
            this.rbCurrentUser = new System.Windows.Forms.RadioButton();
            this.gbCredentials = new System.Windows.Forms.GroupBox();
            this.tbDomain = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.lbDomain = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbAccount = new System.Windows.Forms.Label();
            this.plCredentials = new System.Windows.Forms.Panel();
            this.btnConnect = new System.Windows.Forms.Button();
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.lvListLibrary = new System.Windows.Forms.ListView();
            this.gbContent = new System.Windows.Forms.GroupBox();
            this.tcContent = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tabListInfo = new System.Windows.Forms.TabPage();
            this.cbShowHidden = new System.Windows.Forms.CheckBox();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.tbListID = new System.Windows.Forms.TextBox();
            this.tbListName = new System.Windows.Forms.TextBox();
            this.lbListID = new System.Windows.Forms.Label();
            this.lbListName = new System.Windows.Forms.Label();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.spQueryOuterContainer = new System.Windows.Forms.SplitContainer();
            this.spQueryBuilder = new System.Windows.Forms.SplitContainer();
            this.plRowControls = new Caml.Maker.Model.StackPanel();
            this.flplRowBuilder = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.cmbLogicalJoins = new System.Windows.Forms.ComboBox();
            this.cmbFields = new System.Windows.Forms.ComboBox();
            this.cmbComparisonOperators = new System.Windows.Forms.ComboBox();
            this.cmbValues = new Caml.Maker.Model.FieldValue();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnInsertRow = new System.Windows.Forms.Button();
            this.tbCamlXml = new System.Windows.Forms.TextBox();
            this.flplQueryCommand = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.plProperties = new System.Windows.Forms.Panel();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbConnection.SuspendLayout();
            this.plConnectMethod.SuspendLayout();
            this.gbCredentials.SuspendLayout();
            this.plCredentials.SuspendLayout();
            this.scContent.Panel1.SuspendLayout();
            this.scContent.Panel2.SuspendLayout();
            this.scContent.SuspendLayout();
            this.gbList.SuspendLayout();
            this.gbContent.SuspendLayout();
            this.tcContent.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabListInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.tabQuery.SuspendLayout();
            this.spQueryOuterContainer.Panel1.SuspendLayout();
            this.spQueryOuterContainer.Panel2.SuspendLayout();
            this.spQueryOuterContainer.SuspendLayout();
            this.spQueryBuilder.Panel1.SuspendLayout();
            this.spQueryBuilder.Panel2.SuspendLayout();
            this.spQueryBuilder.SuspendLayout();
            this.flplRowBuilder.SuspendLayout();
            this.flplQueryCommand.SuspendLayout();
            this.tabResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.ssStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConnection
            // 
            this.gbConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConnection.Controls.Add(this.cmbUrl);
            this.gbConnection.Controls.Add(this.plConnectMethod);
            this.gbConnection.Controls.Add(this.lbUrl);
            this.gbConnection.Location = new System.Drawing.Point(12, 7);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(703, 40);
            this.gbConnection.TabIndex = 3;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Connection";
            // 
            // cmbUrl
            // 
            this.cmbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUrl.FormattingEnabled = true;
            this.cmbUrl.Location = new System.Drawing.Point(127, 13);
            this.cmbUrl.Name = "cmbUrl";
            this.cmbUrl.Size = new System.Drawing.Size(260, 21);
            this.cmbUrl.TabIndex = 5;
            // 
            // plConnectMethod
            // 
            this.plConnectMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.plConnectMethod.AutoSize = true;
            this.plConnectMethod.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.plConnectMethod.Controls.Add(this.rbConnectViaObjectModel);
            this.plConnectMethod.Controls.Add(this.rbConnectViaWebService);
            this.plConnectMethod.Location = new System.Drawing.Point(393, 11);
            this.plConnectMethod.Name = "plConnectMethod";
            this.plConnectMethod.Size = new System.Drawing.Size(304, 23);
            this.plConnectMethod.TabIndex = 4;
            // 
            // rbConnectViaObjectModel
            // 
            this.rbConnectViaObjectModel.AutoSize = true;
            this.rbConnectViaObjectModel.Enabled = false;
            this.rbConnectViaObjectModel.Location = new System.Drawing.Point(0, 3);
            this.rbConnectViaObjectModel.Name = "rbConnectViaObjectModel";
            this.rbConnectViaObjectModel.Size = new System.Drawing.Size(148, 17);
            this.rbConnectViaObjectModel.TabIndex = 2;
            this.rbConnectViaObjectModel.TabStop = true;
            this.rbConnectViaObjectModel.Text = "Connect via Object Model";
            this.rbConnectViaObjectModel.UseVisualStyleBackColor = true;
            // 
            // rbConnectViaWebService
            // 
            this.rbConnectViaWebService.AutoSize = true;
            this.rbConnectViaWebService.Location = new System.Drawing.Point(154, 3);
            this.rbConnectViaWebService.Name = "rbConnectViaWebService";
            this.rbConnectViaWebService.Size = new System.Drawing.Size(147, 17);
            this.rbConnectViaWebService.TabIndex = 3;
            this.rbConnectViaWebService.TabStop = true;
            this.rbConnectViaWebService.Text = "Connect via Web Service";
            this.rbConnectViaWebService.UseVisualStyleBackColor = true;
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Location = new System.Drawing.Point(6, 16);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(115, 13);
            this.lbUrl.TabIndex = 0;
            this.lbUrl.Text = "URL of SharePoint site";
            // 
            // rbCustomCredential
            // 
            this.rbCustomCredential.AutoSize = true;
            this.rbCustomCredential.Checked = true;
            this.rbCustomCredential.Location = new System.Drawing.Point(89, 3);
            this.rbCustomCredential.Name = "rbCustomCredential";
            this.rbCustomCredential.Size = new System.Drawing.Size(114, 17);
            this.rbCustomCredential.TabIndex = 0;
            this.rbCustomCredential.TabStop = true;
            this.rbCustomCredential.Text = "Custom credentials";
            this.rbCustomCredential.UseVisualStyleBackColor = true;
            this.rbCustomCredential.CheckedChanged += new System.EventHandler(this.Credentials_CheckedChanged);
            // 
            // rbCurrentUser
            // 
            this.rbCurrentUser.AutoSize = true;
            this.rbCurrentUser.Location = new System.Drawing.Point(1, 3);
            this.rbCurrentUser.Name = "rbCurrentUser";
            this.rbCurrentUser.Size = new System.Drawing.Size(82, 17);
            this.rbCurrentUser.TabIndex = 0;
            this.rbCurrentUser.Text = "Current user";
            this.rbCurrentUser.UseVisualStyleBackColor = true;
            this.rbCurrentUser.CheckedChanged += new System.EventHandler(this.Credentials_CheckedChanged);
            // 
            // gbCredentials
            // 
            this.gbCredentials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCredentials.Controls.Add(this.tbDomain);
            this.gbCredentials.Controls.Add(this.tbPassword);
            this.gbCredentials.Controls.Add(this.tbAccount);
            this.gbCredentials.Controls.Add(this.lbDomain);
            this.gbCredentials.Controls.Add(this.lbPassword);
            this.gbCredentials.Controls.Add(this.lbAccount);
            this.gbCredentials.Controls.Add(this.plCredentials);
            this.gbCredentials.Location = new System.Drawing.Point(12, 49);
            this.gbCredentials.Name = "gbCredentials";
            this.gbCredentials.Size = new System.Drawing.Size(703, 42);
            this.gbCredentials.TabIndex = 1;
            this.gbCredentials.TabStop = false;
            this.gbCredentials.Text = "Credentials";
            // 
            // tbDomain
            // 
            this.tbDomain.Location = new System.Drawing.Point(594, 14);
            this.tbDomain.Name = "tbDomain";
            this.tbDomain.Size = new System.Drawing.Size(100, 20);
            this.tbDomain.TabIndex = 8;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(439, 14);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(100, 20);
            this.tbPassword.TabIndex = 8;
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(274, 14);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(100, 20);
            this.tbAccount.TabIndex = 8;
            // 
            // lbDomain
            // 
            this.lbDomain.AutoSize = true;
            this.lbDomain.Location = new System.Drawing.Point(545, 18);
            this.lbDomain.Name = "lbDomain";
            this.lbDomain.Size = new System.Drawing.Size(43, 13);
            this.lbDomain.TabIndex = 7;
            this.lbDomain.Text = "Domain";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(380, 18);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(53, 13);
            this.lbPassword.TabIndex = 6;
            this.lbPassword.Text = "Password";
            // 
            // lbAccount
            // 
            this.lbAccount.AutoSize = true;
            this.lbAccount.Location = new System.Drawing.Point(221, 18);
            this.lbAccount.Name = "lbAccount";
            this.lbAccount.Size = new System.Drawing.Size(47, 13);
            this.lbAccount.TabIndex = 5;
            this.lbAccount.Text = "Account";
            // 
            // plCredentials
            // 
            this.plCredentials.AutoSize = true;
            this.plCredentials.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.plCredentials.Controls.Add(this.rbCustomCredential);
            this.plCredentials.Controls.Add(this.rbCurrentUser);
            this.plCredentials.Location = new System.Drawing.Point(9, 14);
            this.plCredentials.Name = "plCredentials";
            this.plCredentials.Size = new System.Drawing.Size(206, 23);
            this.plCredentials.TabIndex = 4;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(721, 7);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(77, 85);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // scContent
            // 
            this.scContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scContent.Location = new System.Drawing.Point(12, 92);
            this.scContent.Name = "scContent";
            // 
            // scContent.Panel1
            // 
            this.scContent.Panel1.Controls.Add(this.gbList);
            this.scContent.Panel1MinSize = 200;
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.Controls.Add(this.gbContent);
            this.scContent.Size = new System.Drawing.Size(786, 300);
            this.scContent.SplitterDistance = 204;
            this.scContent.TabIndex = 6;
            // 
            // gbList
            // 
            this.gbList.Controls.Add(this.lvListLibrary);
            this.gbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbList.Location = new System.Drawing.Point(0, 0);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(204, 300);
            this.gbList.TabIndex = 0;
            this.gbList.TabStop = false;
            this.gbList.Text = "List";
            // 
            // lvListLibrary
            // 
            this.lvListLibrary.AllowColumnReorder = true;
            this.lvListLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvListLibrary.FullRowSelect = true;
            this.lvListLibrary.GridLines = true;
            this.lvListLibrary.LabelWrap = false;
            this.lvListLibrary.Location = new System.Drawing.Point(3, 16);
            this.lvListLibrary.MultiSelect = false;
            this.lvListLibrary.Name = "lvListLibrary";
            this.lvListLibrary.Size = new System.Drawing.Size(198, 281);
            this.lvListLibrary.TabIndex = 0;
            this.lvListLibrary.TileSize = new System.Drawing.Size(168, 16);
            this.lvListLibrary.UseCompatibleStateImageBehavior = false;
            this.lvListLibrary.View = System.Windows.Forms.View.Tile;
            this.lvListLibrary.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvListLibrary_MouseDoubleClick);
            this.lvListLibrary.Resize += new System.EventHandler(this.lvListLibrary_Resize);
            // 
            // gbContent
            // 
            this.gbContent.Controls.Add(this.tcContent);
            this.gbContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContent.Location = new System.Drawing.Point(0, 0);
            this.gbContent.Name = "gbContent";
            this.gbContent.Size = new System.Drawing.Size(578, 300);
            this.gbContent.TabIndex = 0;
            this.gbContent.TabStop = false;
            this.gbContent.Text = "Content";
            // 
            // tcContent
            // 
            this.tcContent.Controls.Add(this.tabLog);
            this.tcContent.Controls.Add(this.tabListInfo);
            this.tcContent.Controls.Add(this.tabQuery);
            this.tcContent.Controls.Add(this.tabResult);
            this.tcContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcContent.Location = new System.Drawing.Point(3, 16);
            this.tcContent.Name = "tcContent";
            this.tcContent.SelectedIndex = 0;
            this.tcContent.Size = new System.Drawing.Size(572, 281);
            this.tcContent.TabIndex = 1;
            this.tcContent.Resize += new System.EventHandler(this.tcContent_Resize);
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.tbLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(564, 255);
            this.tabLog.TabIndex = 0;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(3, 3);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLog.Size = new System.Drawing.Size(558, 249);
            this.tbLog.TabIndex = 0;
            this.tbLog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.selectAll_KeyPress);
            // 
            // tabListInfo
            // 
            this.tabListInfo.Controls.Add(this.cbShowHidden);
            this.tabListInfo.Controls.Add(this.dgvColumns);
            this.tabListInfo.Controls.Add(this.tbListID);
            this.tabListInfo.Controls.Add(this.tbListName);
            this.tabListInfo.Controls.Add(this.lbListID);
            this.tabListInfo.Controls.Add(this.lbListName);
            this.tabListInfo.Location = new System.Drawing.Point(4, 22);
            this.tabListInfo.Name = "tabListInfo";
            this.tabListInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabListInfo.Size = new System.Drawing.Size(564, 255);
            this.tabListInfo.TabIndex = 1;
            this.tabListInfo.Text = "List Info";
            this.tabListInfo.UseVisualStyleBackColor = true;
            // 
            // cbShowHidden
            // 
            this.cbShowHidden.AutoSize = true;
            this.cbShowHidden.Location = new System.Drawing.Point(66, 59);
            this.cbShowHidden.Name = "cbShowHidden";
            this.cbShowHidden.Size = new System.Drawing.Size(130, 17);
            this.cbShowHidden.TabIndex = 4;
            this.cbShowHidden.Text = "Show hidden columns";
            this.cbShowHidden.UseVisualStyleBackColor = true;
            this.cbShowHidden.CheckedChanged += new System.EventHandler(this.cbShowHide_CheckedChanged);
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AllowUserToOrderColumns = true;
            this.dgvColumns.AllowUserToResizeRows = false;
            this.dgvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvColumns.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Location = new System.Drawing.Point(3, 81);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.ReadOnly = true;
            this.dgvColumns.RowHeadersVisible = false;
            this.dgvColumns.Size = new System.Drawing.Size(555, 167);
            this.dgvColumns.TabIndex = 3;
            this.dgvColumns.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.NullBackground_CellFormatting);
            // 
            // tbListID
            // 
            this.tbListID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbListID.Location = new System.Drawing.Point(66, 33);
            this.tbListID.Name = "tbListID";
            this.tbListID.ReadOnly = true;
            this.tbListID.Size = new System.Drawing.Size(492, 20);
            this.tbListID.TabIndex = 2;
            // 
            // tbListName
            // 
            this.tbListName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbListName.Location = new System.Drawing.Point(66, 7);
            this.tbListName.Name = "tbListName";
            this.tbListName.ReadOnly = true;
            this.tbListName.Size = new System.Drawing.Size(492, 20);
            this.tbListName.TabIndex = 2;
            // 
            // lbListID
            // 
            this.lbListID.AutoSize = true;
            this.lbListID.Location = new System.Drawing.Point(23, 35);
            this.lbListID.Name = "lbListID";
            this.lbListID.Size = new System.Drawing.Size(37, 13);
            this.lbListID.TabIndex = 1;
            this.lbListID.Text = "List ID";
            // 
            // lbListName
            // 
            this.lbListName.AutoSize = true;
            this.lbListName.Location = new System.Drawing.Point(6, 9);
            this.lbListName.Name = "lbListName";
            this.lbListName.Size = new System.Drawing.Size(54, 13);
            this.lbListName.TabIndex = 0;
            this.lbListName.Text = "List Name";
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.spQueryOuterContainer);
            this.tabQuery.Location = new System.Drawing.Point(4, 22);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Size = new System.Drawing.Size(564, 255);
            this.tabQuery.TabIndex = 2;
            this.tabQuery.Text = "Query";
            this.tabQuery.UseVisualStyleBackColor = true;
            // 
            // spQueryOuterContainer
            // 
            this.spQueryOuterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spQueryOuterContainer.Location = new System.Drawing.Point(0, 0);
            this.spQueryOuterContainer.Name = "spQueryOuterContainer";
            // 
            // spQueryOuterContainer.Panel1
            // 
            this.spQueryOuterContainer.Panel1.Controls.Add(this.spQueryBuilder);
            // 
            // spQueryOuterContainer.Panel2
            // 
            this.spQueryOuterContainer.Panel2.Controls.Add(this.plProperties);
            this.spQueryOuterContainer.Size = new System.Drawing.Size(564, 255);
            this.spQueryOuterContainer.SplitterDistance = 486;
            this.spQueryOuterContainer.TabIndex = 2;
            // 
            // spQueryBuilder
            // 
            this.spQueryBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spQueryBuilder.Location = new System.Drawing.Point(0, 0);
            this.spQueryBuilder.Name = "spQueryBuilder";
            this.spQueryBuilder.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spQueryBuilder.Panel1
            // 
            this.spQueryBuilder.Panel1.Controls.Add(this.plRowControls);
            this.spQueryBuilder.Panel1.Controls.Add(this.flplRowBuilder);
            // 
            // spQueryBuilder.Panel2
            // 
            this.spQueryBuilder.Panel2.Controls.Add(this.tbCamlXml);
            this.spQueryBuilder.Panel2.Controls.Add(this.flplQueryCommand);
            this.spQueryBuilder.Size = new System.Drawing.Size(486, 255);
            this.spQueryBuilder.SplitterDistance = 127;
            this.spQueryBuilder.TabIndex = 0;
            // 
            // plRowControls
            // 
            this.plRowControls.AutoScroll = true;
            this.plRowControls.BackColor = System.Drawing.SystemColors.ControlLight;
            this.plRowControls.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plRowControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plRowControls.Location = new System.Drawing.Point(0, 58);
            this.plRowControls.Name = "plRowControls";
            this.plRowControls.Size = new System.Drawing.Size(486, 69);
            this.plRowControls.TabIndex = 1;
            // 
            // flplRowBuilder
            // 
            this.flplRowBuilder.AutoSize = true;
            this.flplRowBuilder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flplRowBuilder.Controls.Add(this.cmbFilter);
            this.flplRowBuilder.Controls.Add(this.cmbLogicalJoins);
            this.flplRowBuilder.Controls.Add(this.cmbFields);
            this.flplRowBuilder.Controls.Add(this.cmbComparisonOperators);
            this.flplRowBuilder.Controls.Add(this.cmbValues);
            this.flplRowBuilder.Controls.Add(this.btnAddRow);
            this.flplRowBuilder.Controls.Add(this.btnInsertRow);
            this.flplRowBuilder.Dock = System.Windows.Forms.DockStyle.Top;
            this.flplRowBuilder.Location = new System.Drawing.Point(0, 0);
            this.flplRowBuilder.Name = "flplRowBuilder";
            this.flplRowBuilder.Size = new System.Drawing.Size(486, 58);
            this.flplRowBuilder.TabIndex = 0;
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "Where",
            "OrderBy",
            "GroupBy"});
            this.cmbFilter.Location = new System.Drawing.Point(3, 3);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(71, 21);
            this.cmbFilter.TabIndex = 0;
            this.cmbFilter.SelectedValueChanged += new System.EventHandler(this.cmbFilter_SelectedValueChanged);
            // 
            // cmbLogicalJoins
            // 
            this.cmbLogicalJoins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogicalJoins.FormattingEnabled = true;
            this.cmbLogicalJoins.Items.AddRange(new object[] {
            "And",
            "Or"});
            this.cmbLogicalJoins.Location = new System.Drawing.Point(80, 3);
            this.cmbLogicalJoins.Name = "cmbLogicalJoins";
            this.cmbLogicalJoins.Size = new System.Drawing.Size(50, 21);
            this.cmbLogicalJoins.TabIndex = 1;
            // 
            // cmbFields
            // 
            this.cmbFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFields.FormattingEnabled = true;
            this.cmbFields.Location = new System.Drawing.Point(136, 3);
            this.cmbFields.Name = "cmbFields";
            this.cmbFields.Size = new System.Drawing.Size(121, 21);
            this.cmbFields.TabIndex = 2;
            this.cmbFields.SelectedValueChanged += new System.EventHandler(this.cmbFields_SelectedValueChanged);
            // 
            // cmbComparisonOperators
            // 
            this.cmbComparisonOperators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComparisonOperators.FormattingEnabled = true;
            this.cmbComparisonOperators.Items.AddRange(new object[] {
            "Equal to (Eq)",
            "Not equal to (Neq)",
            "Greater than (Gt)",
            "Greater than or equal to (Geq)",
            "Less than (Lt)",
            "Less than or equal to (Leq)",
            "Is null (IsNull)",
            "Is not null (IsNotNull)",
            "In",
            "Includes",
            "NotIncludes",
            "BeginsWith",
            "Contains",
            "DateRangesOverlap"});
            this.cmbComparisonOperators.Location = new System.Drawing.Point(263, 3);
            this.cmbComparisonOperators.Name = "cmbComparisonOperators";
            this.cmbComparisonOperators.Size = new System.Drawing.Size(121, 21);
            this.cmbComparisonOperators.TabIndex = 3;
            // 
            // cmbValues
            // 
            this.cmbValues.AutoSize = true;
            this.cmbValues.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmbValues.Field = null;
            this.cmbValues.Location = new System.Drawing.Point(387, 0);
            this.cmbValues.Margin = new System.Windows.Forms.Padding(0);
            this.cmbValues.Name = "cmbValues";
            this.cmbValues.Size = new System.Drawing.Size(0, 0);
            this.cmbValues.TabIndex = 4;
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(390, 3);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(75, 23);
            this.btnAddRow.TabIndex = 0;
            this.btnAddRow.Text = "Add";
            this.btnAddRow.UseVisualStyleBackColor = true;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnInsertRow
            // 
            this.btnInsertRow.Location = new System.Drawing.Point(3, 32);
            this.btnInsertRow.Name = "btnInsertRow";
            this.btnInsertRow.Size = new System.Drawing.Size(75, 23);
            this.btnInsertRow.TabIndex = 0;
            this.btnInsertRow.Text = "Insert";
            this.btnInsertRow.UseVisualStyleBackColor = true;
            this.btnInsertRow.Click += new System.EventHandler(this.btnInsertRow_Click);
            // 
            // tbCamlXml
            // 
            this.tbCamlXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCamlXml.Location = new System.Drawing.Point(0, 29);
            this.tbCamlXml.Multiline = true;
            this.tbCamlXml.Name = "tbCamlXml";
            this.tbCamlXml.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCamlXml.Size = new System.Drawing.Size(486, 95);
            this.tbCamlXml.TabIndex = 2;
            this.tbCamlXml.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.selectAll_KeyPress);
            // 
            // flplQueryCommand
            // 
            this.flplQueryCommand.AutoSize = true;
            this.flplQueryCommand.BackColor = System.Drawing.SystemColors.ControlLight;
            this.flplQueryCommand.Controls.Add(this.btnExecuteQuery);
            this.flplQueryCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.flplQueryCommand.Location = new System.Drawing.Point(0, 0);
            this.flplQueryCommand.Name = "flplQueryCommand";
            this.flplQueryCommand.Size = new System.Drawing.Size(486, 29);
            this.flplQueryCommand.TabIndex = 1;
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Location = new System.Drawing.Point(3, 3);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(96, 23);
            this.btnExecuteQuery.TabIndex = 0;
            this.btnExecuteQuery.Text = "Execute Query";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // plProperties
            // 
            this.plProperties.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.plProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plProperties.Location = new System.Drawing.Point(0, 0);
            this.plProperties.Name = "plProperties";
            this.plProperties.Size = new System.Drawing.Size(74, 255);
            this.plProperties.TabIndex = 1;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.dgvResult);
            this.tabResult.Location = new System.Drawing.Point(4, 22);
            this.tabResult.Name = "tabResult";
            this.tabResult.Size = new System.Drawing.Size(564, 255);
            this.tabResult.TabIndex = 3;
            this.tabResult.Text = "Result";
            this.tabResult.UseVisualStyleBackColor = true;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.Size = new System.Drawing.Size(564, 255);
            this.dgvResult.TabIndex = 0;
            this.dgvResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.NullBackground_CellFormatting);
            // 
            // ssStatusBar
            // 
            this.ssStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.ssStatusBar.Location = new System.Drawing.Point(0, 395);
            this.ssStatusBar.Name = "ssStatusBar";
            this.ssStatusBar.Size = new System.Drawing.Size(810, 22);
            this.ssStatusBar.TabIndex = 7;
            this.ssStatusBar.Text = "statusStrip1";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(118, 17);
            this.tsslStatus.Text = "toolStripStatusLabel1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 417);
            this.Controls.Add(this.ssStatusBar);
            this.Controls.Add(this.scContent);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.gbCredentials);
            this.Controls.Add(this.gbConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(807, 249);
            this.Name = "MainForm";
            this.Text = "Caml Maker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.plConnectMethod.ResumeLayout(false);
            this.plConnectMethod.PerformLayout();
            this.gbCredentials.ResumeLayout(false);
            this.gbCredentials.PerformLayout();
            this.plCredentials.ResumeLayout(false);
            this.plCredentials.PerformLayout();
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.Panel2.ResumeLayout(false);
            this.scContent.ResumeLayout(false);
            this.gbList.ResumeLayout(false);
            this.gbContent.ResumeLayout(false);
            this.tcContent.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabListInfo.ResumeLayout(false);
            this.tabListInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.tabQuery.ResumeLayout(false);
            this.spQueryOuterContainer.Panel1.ResumeLayout(false);
            this.spQueryOuterContainer.Panel2.ResumeLayout(false);
            this.spQueryOuterContainer.ResumeLayout(false);
            this.spQueryBuilder.Panel1.ResumeLayout(false);
            this.spQueryBuilder.Panel1.PerformLayout();
            this.spQueryBuilder.Panel2.ResumeLayout(false);
            this.spQueryBuilder.Panel2.PerformLayout();
            this.spQueryBuilder.ResumeLayout(false);
            this.flplRowBuilder.ResumeLayout(false);
            this.flplRowBuilder.PerformLayout();
            this.flplQueryCommand.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.RadioButton rbConnectViaObjectModel;
        private System.Windows.Forms.RadioButton rbConnectViaWebService;
        private System.Windows.Forms.RadioButton rbCurrentUser;
        private System.Windows.Forms.RadioButton rbCustomCredential;
        private System.Windows.Forms.GroupBox gbCredentials;
        private System.Windows.Forms.Panel plConnectMethod;
        private System.Windows.Forms.Panel plCredentials;
        private System.Windows.Forms.TextBox tbDomain;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label lbDomain;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbAccount;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.SplitContainer scContent;
        private System.Windows.Forms.StatusStrip ssStatusBar;
        private System.Windows.Forms.GroupBox gbList;
        private System.Windows.Forms.GroupBox gbContent;
        private System.Windows.Forms.ListView lvListLibrary;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TabControl tcContent;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabListInfo;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.TextBox tbListID;
        private System.Windows.Forms.TextBox tbListName;
        private System.Windows.Forms.Label lbListID;
        private System.Windows.Forms.Label lbListName;
        private System.Windows.Forms.CheckBox cbShowHidden;
        private System.Windows.Forms.TabPage tabQuery;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.SplitContainer spQueryBuilder;
        private System.Windows.Forms.SplitContainer spQueryOuterContainer;
        private System.Windows.Forms.FlowLayoutPanel flplRowBuilder;
        private System.Windows.Forms.Panel plProperties;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnInsertRow;
        private System.Windows.Forms.ComboBox cmbLogicalJoins;
        private System.Windows.Forms.ComboBox cmbComparisonOperators;
        private System.Windows.Forms.ComboBox cmbFields;
        private Caml.Maker.Model.FieldValue cmbValues;
        private Model.StackPanel plRowControls;
        private System.Windows.Forms.ComboBox cmbUrl;
        private System.Windows.Forms.FlowLayoutPanel flplQueryCommand;
        private System.Windows.Forms.Button btnExecuteQuery;
        private System.Windows.Forms.TextBox tbCamlXml;
    }
}

