using Caml.Maker.Model;
using Caml.Maker.Model.CM;
using Caml.Maker.Model.WS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Caml.Maker
{
    public partial class MainForm : Form
    {
        string CacheFile
        {
            get
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Caml.Maker.db");
            }
        }

        /// <summary>
        /// Get credentials for sharepoint logon
        /// </summary>
        /// <returns></returns>
        System.Net.ICredentials CurrentCredentials
        {
            get
            {
                if (rbCurrentUser.Checked)
                {
                    return System.Net.CredentialCache.DefaultCredentials;
                }

                if (rbCustomCredential.Checked)
                {
                    return new System.Net.NetworkCredential(
                        tbAccount.Text.Trim(),
                        tbPassword.Text.Trim(),
                        tbDomain.Text.Trim()
                    );
                }

                return null;
            }
        }

        SPConnect Connector;
        SPList CurrentList;

        public MainForm()
        {
            InitializeComponent();

            #region Initial list icons
            lvListLibrary.LargeImageList = new ImageList();
            if (System.IO.Directory.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons")))
            {
                var iconFolder = new System.IO.DirectoryInfo(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icons"));
                iconFolder.GetFiles().Each(f =>
                {
                    try
                    {
                        lvListLibrary.LargeImageList.Images.Add(
                            System.IO.Path.GetFileNameWithoutExtension(f.Name),
                            Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(f.FullName)))
                        );
                    }
                    catch { }
                });
            }
            #endregion

            #region Initial from configuration setting
            switch (ConfigurationManager.AppSettings["connect.method"])
            {
                case "0": rbConnectViaObjectModel.Checked = true; break;
                default:
                case "1": rbConnectViaWebService.Checked = true; break;
            }
            switch (ConfigurationManager.AppSettings["credentials"])
            {
                default:
                case "0": rbCurrentUser.Checked = true; break;
                case "1": rbCustomCredential.Checked = true; break;
            }
            tbAccount.Text = ConfigurationManager.AppSettings["custom.credentials.account"];
            tbPassword.Text = ConfigurationManager.AppSettings["custom.credentials.password"];
            tbDomain.Text = ConfigurationManager.AppSettings["custom.credentials.domain"];
            #endregion

            #region Comparison Operator
            cmbComparisonOperators.Items.Clear();
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Eq", Description = "Equal to" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Neq", Description = "Not equal to" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Gt", Description = "Greater than" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Geq", Description = "Greater than or equal to" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Lt", Description = "Less than" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Leq", Description = "Less than or equal to" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "IsNull", Description = "Is null" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "IsNotNull", Description = "Is not null" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "In", Description = "In" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Includes", Description = "Includes" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "NotIncludes", Description = "Not includes" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "BeginsWith", Description = "Begins with" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "Contains", Description = "Contains" });
            cmbComparisonOperators.Items.Add(new ComparisonOperator() { Name = "DateRangesOverlap", Description = "Date ranges overlap" });
            #endregion

            // Items
            RowItem.AllComparisonOperators = cmbComparisonOperators.Items;
            RowItem.AllFilter = cmbFilter.Items;
            RowItem.AllLogicalJoins = cmbLogicalJoins.Items;

            // Log
            LogHandler.LogArea = tbLog;

            // Load cached url
            if (System.IO.File.Exists(CacheFile))
            {
                var lines = System.IO.File.ReadAllLines(CacheFile);
                lines.Where(l => l.Split('=')[0] == "url").Each(l => cmbUrl.Items.Add(l.Split('=')[1]));
            }

            plRowControls.Adjusted += plRowControls_Adjusted;

            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["log.tab.enabled"])) tcContent.TabPages.Remove(tabLog);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lvListLibrary.Items.Clear();
            // Set list item size
            lvListLibrary.TileSize = new Size(lvListLibrary.ClientSize.Width, 16);
            // Status bar
            SetStatusMessage(string.Empty);

            ClearQueryTabpage();

            #region Centralize window
            int x = Screen.PrimaryScreen.Bounds.Width - this.Width;
            int y = Screen.PrimaryScreen.Bounds.Height - this.Height;
            this.Location = new Point(x / 2, y / 2);
            #endregion
        }

        #region Status Bar
        /// <summary>
        /// Show message on status bar
        /// </summary>
        /// <param name="message"></param>
        /// <param name="param"></param>
        void SetStatusMessage(string message, params object[] param)
        {
            SetStatusMessage(Color.Black, message, param);
        }

        void SetStatusMessage(Color c, string message, params object[] param)
        {
            tsslStatus.Text = string.Format(message, param);
            tsslStatus.ForeColor = c;
            ssStatusBar.Update();
        }

        /// <summary>
        /// Clear list info tab contents
        /// </summary>
        void ClearListInfo()
        {
            // Clear list info
            CurrentList = null;
            tbListID.Text = tbListName.Text = string.Empty;
            dgvColumns.DataSource = null;
            dgvColumns.Refresh();
        }

        void ClearResult()
        {
            dgvResult.DataSource = null;
            dgvResult.Refresh();
        }



        /// <summary>
        /// Set child controls disabled
        /// </summary>
        /// <param name="container"></param>
        /// <param name="enable"></param>
        void SetControlEnable(Control container, bool enable)
        {
            container.Controls.Each<Control>(c =>
            {
                c.Enabled = enable;

                if (c.Controls.Count > 0) SetControlEnable(c, enable);
            });
        }

        void ClearQueryTabpage()
        {
            cmbFields.Items.Clear();
            cmbComparisonOperators.Visible = cmbLogicalJoins.Visible = cmbValues.Visible = cmbFields.Visible = false;
            cmbFilter.Enabled = false;
            btnAddRow.Enabled = btnInsertRow.Enabled = false;
            btnExecuteQuery.Enabled = false;
            plRowControls.Controls.Clear();
            tbCamlXml.Text = string.Empty;
        }
        #endregion

        #region Event handler
        private void lvListLibrary_Resize(object sender, EventArgs e)
        {
            // Adjust list item size
            lvListLibrary.TileSize = new Size(lvListLibrary.ClientSize.Width, 16);
            //lvListLibrary.ResumeLayout();
        }

        /// <summary>
        /// Connect to sharepoint site
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                string text = btnConnect.Text;
                try
                {
                    btnConnect.Enabled = false;
                    switch (btnConnect.Text)
                    {
                        case "Connect":
                            #region
                            {
                                if (rbConnectViaWebService.Checked)
                                    Connector = new WebServiceConnector() { Url = cmbUrl.Text, Credentials = CurrentCredentials };
                                else
                                    Connector = new ClientModelConnector() { Url = cmbUrl.Text, Credentials = CurrentCredentials };

                                btnConnect.Text = "Disconnect";
                                SetStatusMessage("Connecting {0}", Connector.WebUrl);
                                // Disable connection parameters
                                SetControlEnable(gbConnection, false);
                                SetControlEnable(gbCredentials, false);

                                // Remember url
                                if (!string.IsNullOrEmpty(cmbUrl.Text) && !cmbUrl.Items.Contains(cmbUrl.Text))
                                    cmbUrl.Items.Add(cmbUrl.Text);

                                // Clear list
                                lvListLibrary.Items.Clear();
                                // Update UI
                                Update();

                                // Connect to sharepoint site web service
                                var items = Connector.GetListCollection();
                                if (items == null)
                                {
                                    SetStatusMessage(string.Empty);
                                }
                                else
                                {
                                    lvListLibrary.Items.AddRange(items.ToArray());
                                    items.Where(item => item.Image != null).Each(item =>
                                    {
                                        lvListLibrary.LargeImageList.Images.Add(
                                            item.ImageKey,
                                            item.Image
                                        );
                                    });
                                    SetStatusMessage("Connected");
                                }
                            }
                            #endregion
                            break;
                        case "Disconnect":
                            #region
                            {
                                Connector = null;

                                btnConnect.Text = "Connect";
                                // Disable connection parameters
                                SetControlEnable(gbConnection, true);
                                SetControlEnable(gbCredentials, true);

                                // Clear list
                                lvListLibrary.Items.Clear();

                                // Clear list info
                                ClearListInfo();
                                ClearQueryTabpage();
                                ClearResult();

                                SetStatusMessage("Disconnected");
                            }
                            #endregion
                            break;
                    }
                    btnConnect.Enabled = true;

                }
                catch (Exception ex)
                {
                    LogHandler.Log("{0}", ex);
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnConnect.Enabled = true;
                    SetControlEnable(gbConnection, true);
                    SetControlEnable(gbCredentials, true);
                    btnConnect.Text = text;
                }
            }));
        }

        /// <summary>
        /// Select all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\x1')
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Click list and display list column info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvListLibrary_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                try
                {
                    ClearListInfo();
                    ClearQueryTabpage();
                    ClearResult();

                    if (lvListLibrary.SelectedItems.Count > 0)
                    {
                        // Get current selection
                        SPList item = lvListLibrary.SelectedItems[0] as SPList;
                        SetStatusMessage("Request '{0}' info from sharepoint site", item.Title);

                        #region Get list info
                        CurrentList = Connector.GetFields(item);
                        if (CurrentList != null)
                        {
                            tbListID.Text = item.ID.ToString();
                            tbListName.Text = item.Title;

                            // Option
                            DataView view = CurrentList.FieldView;
                            view.RowFilter = cbShowHidden.Checked ? string.Empty : "Hidden is null or Hidden <> 'TRUE'";
                            // Bind data
                            dgvColumns.DataSource = view;

                            // Open list info tab
                            tcContent.SelectedTab = tcContent.TabPages["tabListInfo"];
                            dgvColumns.Focus();

                            // Fill field combobox
                            cmbFields.Items.Clear();
                            item.Fields.Where(f => !f.Hidden).Each(f => cmbFields.Items.Add(f));
                            RowItem.AllField = cmbFields.Items;
                            cmbFilter.Enabled = btnAddRow.Enabled = btnInsertRow.Enabled = true;
                            btnExecuteQuery.Enabled = true;

                            SetStatusMessage("OK");
                            return;
                        }
                        #endregion
                    }
                    SetStatusMessage(string.Empty);
                }
                catch (Exception ex)
                {
                    LogHandler.Log("{0}", ex);
                    MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }

        /// <summary>
        /// Show/hide invisible column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShowHide_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentList != null)
            {
                // Option
                var view = CurrentList.FieldView;
                view.RowFilter = cbShowHidden.Checked ? string.Empty : "Hidden is null or Hidden<>'TRUE'";
                // Bind data
                dgvColumns.DataSource = view;
                dgvColumns.Focus();
            }
        }

        private void tcContent_Resize(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                //lvListLibrary.Refresh();
                //tcContent.Refresh();
            }));
        }

        /// <summary>
        /// Disable/enable input of custom credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Credentials_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == rbCustomCredential)
            {
                tbAccount.Enabled
                    = tbPassword.Enabled
                    = tbDomain.Enabled
                    = true;
            }
            else
            {
                tbAccount.Enabled
                    = tbPassword.Enabled
                    = tbDomain.Enabled
                    = false;
            }
        }

        /// <summary>
        /// Change background color of null grid cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NullBackground_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            var cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

            e.CellStyle.BackColor = cell.Value == DBNull.Value ? Color.LightGoldenrodYellow : Color.FromKnownColor(KnownColor.Window);
        }

        /// <summary>
        /// Change where/groupby/orderby
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFilter_SelectedValueChanged(object sender, EventArgs e)
        {
            // hide all
            cmbFilter.Parent.Controls.OfType<Control>().Each(c => c.Visible = false);
            cmbLogicalJoins.Update();

            switch (cmbFilter.Text)
            {
                case "Where":
                    cmbFilter.Visible = true;
                    cmbLogicalJoins.Visible = true;
                    cmbFields.Visible = true;
                    cmbComparisonOperators.Visible = true;
                    cmbValues.Visible = true;
                    btnAddRow.Visible = true;
                    btnInsertRow.Visible = true;
                    break;
                case "GroupBy":
                case "OrderBy":
                    cmbFilter.Visible = true;
                    cmbLogicalJoins.Visible = false;
                    cmbFields.Visible = true;
                    cmbComparisonOperators.Visible = false;
                    cmbValues.Visible = false;
                    btnAddRow.Visible = true;
                    btnInsertRow.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// Add new query condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                SetStatusMessage(string.Empty);
                var item = new RowItem();
                var required = new List<string>();

                #region Gather input
                switch (cmbFilter.Text)
                {
                    case "Where":
                        if (cmbLogicalJoins.Enabled && cmbLogicalJoins.SelectedItem == null) required.Add("Logical Joins");
                        if (cmbFields.SelectedItem == null) required.Add("FieldRef");
                        if (cmbComparisonOperators.SelectedItem == null) required.Add("Comparison Operators");
                        if (required.Count > 0) { SetStatusMessage(Color.Red, "{0} required", string.Join(", ", required.ToArray())); return; }

                        item.Filter = cmbFilter.Text;
                        item.Field = cmbFields.SelectedItem as SPField;
                        item.LogicalJoins = cmbLogicalJoins.Text;
                        item.ComparisonOperators = cmbComparisonOperators.SelectedItem as ComparisonOperator;
                        item.Value = cmbValues.Value;
                        item.Value = cmbValues.Value;
                        break;
                    case "GroupBy":
                    case "OrderBy":
                        if (cmbFields.SelectedItem == null) required.Add("FieldRef");
                        if (required.Count > 0) { SetStatusMessage(Color.Red, "{0} required", string.Join(", ", required.ToArray())); return; }

                        item.Filter = cmbFilter.Text;
                        item.Field = cmbFields.SelectedItem as SPField;
                        break;
                    default:
                        SetStatusMessage(Color.Red, "Clause requied");
                        return;
                }
                #endregion

                plRowControls.Controls.Add(new RowItemControlPanel(item) { Sort = plConnectMethod.Controls.Count + 1 });
                plRowControls.Refresh();
            }));
        }

        private void btnInsertRow_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                SetStatusMessage(string.Empty);
                var item = new RowItem();
                var required = new List<string>();

                #region Gather input
                switch (cmbFilter.Text)
                {
                    case "Where":
                        if (cmbLogicalJoins.Enabled && cmbLogicalJoins.SelectedItem == null) required.Add("Logical Joins");
                        if (cmbFields.SelectedItem == null) required.Add("FieldRef");
                        if (cmbComparisonOperators.SelectedItem == null) required.Add("Comparison Operators");
                        if (required.Count > 0) { SetStatusMessage(Color.Red, "{0} required", string.Join(", ", required.ToArray())); return; }

                        item.Filter = cmbFilter.Text;
                        item.Field = cmbFields.SelectedItem as SPField;
                        item.LogicalJoins = cmbLogicalJoins.Text;
                        item.ComparisonOperators = cmbComparisonOperators.SelectedItem as ComparisonOperator;
                        item.Value = cmbValues.Value;
                        item.Value = cmbValues.Value;
                        break;
                    case "GroupBy":
                    case "OrderBy":
                        if (cmbFields.SelectedItem == null) required.Add("FieldRef");
                        if (required.Count > 0) { SetStatusMessage(Color.Red, "{0} required", string.Join(", ", required.ToArray())); return; }

                        item.Filter = cmbFilter.Text;
                        item.Field = cmbFields.SelectedItem as SPField;
                        break;
                    default:
                        SetStatusMessage(Color.Red, "Clause requied");
                        return;
                }
                #endregion

                // Get selected row
                var selectedRow = plRowControls.Controls.OfType<RowItemControlPanel>().FirstOrDefault(r => r.IsSelected);

                if (selectedRow == null)
                {
                    plRowControls.Controls.Add(new RowItemControlPanel(item) { Sort = plConnectMethod.Controls.Count + 1 });
                }
                else
                {
                    var selectedRowSort = selectedRow.Sort;
                    // change sort of all after current
                    plRowControls.Controls
                                 .OfType<RowItemControlPanel>()
                                 .Where(r => r.Sort >= selectedRowSort)
                                 .Each(r => r.Sort++);
                    plRowControls.Controls.Add(new RowItemControlPanel(item) { Sort = selectedRowSort });
                }

                plRowControls.Refresh();
            }));
        }

        /// <summary>
        /// Window closing action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (cmbUrl.Items.Count > 0)
                {
                    var sb = new StringBuilder();
                    cmbUrl.Items.Each<object>(item =>
                    {
                        sb.AppendFormat("url={0}", item).AppendLine();
                    });
                    System.IO.File.WriteAllText(CacheFile, sb.ToString());
                }
            }
            catch { }
        }

        /// <summary>
        /// Build CAML action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void plRowControls_Adjusted(object sender, EventArgs e)
        {
            var whereClause = plRowControls.Controls
                                     .OfType<RowItemControlPanel>()
                                     .Where(r => r.ContentItem.Filter == "Where")
                                     .Count();

            if (whereClause > 0)
            {
                cmbLogicalJoins.Enabled = true;
            }
            else
            {
                cmbLogicalJoins.Enabled = false;
                cmbLogicalJoins.SelectedIndex = -1;
            }

            tbCamlXml.Text = plRowControls.ToCaml();
        }

        /// <summary>
        /// Query field chnages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFields_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbValues.Field = cmbFields.SelectedItem as SPField;
        }

        /// <summary>
        /// Execute CAML query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                try
                {
                    SPListItemCollection collection = Connector.GetListItems(
                        tbListName.Text,
                        "",
                        tbCamlXml.Text,
                        null,
                        "",
                        null,
                        ""
                    );
                    dgvResult.DataSource = collection.GenerateView(CurrentList);
                    dgvResult.Refresh();

                    // Open list info tab
                    tcContent.SelectedTab = tcContent.TabPages["tabResult"];
                }
                catch (Exception ex)
                {
                    LogHandler.Log("{0}", ex);
                    MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }));
        }
        #endregion

        private void selectAll_KeyPress(object sender, EventArgs e)
        {

        }
    }
}
