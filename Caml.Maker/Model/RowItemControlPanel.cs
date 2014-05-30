using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caml.Maker.Model
{
    public class RowItemControlPanel : Panel
    {
        public bool IsSelected { get; set; }
        public int Sort { get; set; }
        public int Indent { get; set; }

        #region Controls
        private PictureBox DeleteButton;
        private PictureBox IncreaseIndent;
        private PictureBox DecreaseIndent;

        public ComboBox Filter;

        private FlowLayoutPanel layout;

        private Label IndentLabel;
        private Label IndentSeperator;

        private ComboBox LogicalJoin;
        private ComboBox Field;
        private ComboBox ComparisonOperators;
        private FieldValue Value;
        #endregion

        #region Constructor
        public RowItemControlPanel()
        {
            DeleteButton = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Cursor = Cursors.Hand,
                Image = Caml.Maker.Properties.Resources.IconDelete,
                Location = new Point(1, 1)
            };
            DeleteButton.Click += DeleteButton_Click;

            IncreaseIndent = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Cursor = Cursors.Hand,
                Image = Caml.Maker.Properties.Resources.IndentIncrease
            };
            IncreaseIndent.Click += IncreaseIndent_Click;

            DecreaseIndent = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.AutoSize,
                Cursor = Cursors.Hand,
                Image = Caml.Maker.Properties.Resources.IndentDecrease
            };
            DecreaseIndent.Click += DecreaseIndent_Click;


            Filter = new ComboBox() { Width = 100, Enabled = false };
            Filter.Items.AddRange(RowItem.AllFilter.OfType<object>().ToArray());
            Filter.Location = new Point(DeleteButton.Location.X + (DeleteButton.Width + 3) * 3, DeleteButton.Location.Y);

            LogicalJoin = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 50 };
            LogicalJoin.Items.AddRange(RowItem.AllLogicalJoins.OfType<object>().ToArray());

            Field = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            if (RowItem.AllField != null) Field.Items.AddRange(RowItem.AllField.OfType<object>().ToArray());
            Field.SelectedValueChanged += Field_SelectedValueChanged;

            ComparisonOperators = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            ComparisonOperators.Items.AddRange(RowItem.AllComparisonOperators.OfType<object>().ToArray());

            layout = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0, 0, 0, 0)
            };
            layout.Location = new Point(Filter.Location.X + Filter.Width + 3, Filter.Location.Y - 3);

            layout.Controls.Add(IndentLabel = new Label() { Width = 0 });
            layout.Controls.Add(LogicalJoin);
            layout.Controls.Add(IndentSeperator = new Label() { Width = 0 });
            layout.Controls.Add(Field);
            layout.Controls.Add(ComparisonOperators);
            layout.Controls.Add(Value = new FieldValue());

            Controls.Add(DeleteButton);
            Controls.Add(IncreaseIndent);
            Controls.Add(DecreaseIndent);
            Controls.Add(Filter);
            Controls.Add(layout);

            Height = layout.Height + 1;
            MinimumSize = new System.Drawing.Size(DeleteButton.Width + Filter.Width + layout.Width + 3 * 5, Height);

            DeleteButton.Location = new Point(
                1,
                layout.Location.Y + ((layout.Height - DeleteButton.Height) / 2)
            );
            IncreaseIndent.Location = new Point(DeleteButton.Location.X + DeleteButton.Width + 3, DeleteButton.Location.Y);
            DecreaseIndent.Location = new Point(IncreaseIndent.Location.X + IncreaseIndent.Width + 3, IncreaseIndent.Location.Y);

            Click += RowItemControlPanel_Click;
        }

        public RowItemControlPanel(RowItem item)
            : this()
        {
            ContentItem = item;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public RowItem ContentItem
        {
            get
            {
                var item = new RowItem() { Filter = Filter.Text };
                switch (Filter.Text)
                {
                    case "Where":
                        item.LogicalJoins = LogicalJoin.Text;
                        item.Field = Field.SelectedItem as SharePointField;
                        item.ComparisonOperators = ComparisonOperators.SelectedItem as ComparisonOperator;
                        item.Value = Value.Value;
                        break;
                    case "GroupBy":
                    case "OrderBy":
                        item.Field = Field.SelectedItem as SharePointField;
                        break;
                }
                return item;
            }
            set
            {
                Filter.SelectedItem = value.Filter;

                switch (value.Filter)
                {
                    case "Where":
                        LogicalJoin.Visible = true;
                        Field.Visible = true;
                        ComparisonOperators.Visible = true;
                        Value.Visible = true;

                        LogicalJoin.SelectedItem = value.LogicalJoins;
                        Field.SelectedItem = value.Field;
                        ComparisonOperators.SelectedItem = value.ComparisonOperators;
                        Value.Value = value.Value;

                        break;
                    case "GroupBy":
                    case "OrderBy":
                        LogicalJoin.Visible = false;
                        Field.Visible = true;
                        ComparisonOperators.Visible = false;
                        Value.Visible = false;
                        IncreaseIndent.Visible = false;
                        DecreaseIndent.Visible = false;

                        Field.SelectedItem = value.Field;
                        break;
                }
            }
        }

        /// <summary>
        /// Previous row item panel
        /// </summary>
        public RowItemControlPanel Previous
        {
            get
            {
                return Parent.Controls
                             .OfType<RowItemControlPanel>()
                             .Where(e => e.Sort < Sort)
                             .OrderByDescending(e => e.Sort)
                             .FirstOrDefault();
            }
        }

        /// <summary>
        /// Next row item panel
        /// </summary>
        public RowItemControlPanel Next
        {
            get
            {
                return Parent.Controls
                             .OfType<RowItemControlPanel>()
                             .Where(e => e.Sort > Sort)
                             .OrderBy(e => e.Sort)
                             .FirstOrDefault();
            }
        }

        public override void Refresh()
        {
            #region where/groupby/orderby grouping
            var prevItem = Previous;

            if (prevItem != null)
            {
                if (prevItem.Filter.Text.Equals(Filter.Text))
                {
                    // Hide where/goupby/orderby combobox when previous item has same value
                    Filter.Visible = false;
                    IncreaseIndent.Visible = DecreaseIndent.Visible = "Where".Equals(Filter.Text);
                }
                else
                {
                    Filter.Visible = true;
                    IncreaseIndent.Visible = DecreaseIndent.Visible = false;

                    // if current is where of head, disable and/or combobox
                    LogicalJoin.Enabled = !"Where".Equals(Filter.Text);
                    if (!LogicalJoin.Enabled) LogicalJoin.SelectedIndex = -1;
                }
            }
            else
            {
                Filter.Visible = true;
                IncreaseIndent.Visible = DecreaseIndent.Visible = false;

                // if current is where of head, disable and/or combobox
                LogicalJoin.Enabled = !"Where".Equals(Filter.Text);
                if (!LogicalJoin.Enabled) LogicalJoin.SelectedIndex = -1;
            }
            #endregion

            #region indent
            if ("Where".Equals(Filter.Text))
            {
                IndentLabel.Width = IndentSeperator.Width = 0;

                var prev = prevItem;

                if (prev != null && prev.Indent < Indent)
                {
                    IndentSeperator.Width = LogicalJoin.Width;
                    IndentLabel.Width = (Indent - 1) * LogicalJoin.Width;
                }
                else
                {
                    IndentLabel.Width = Indent * LogicalJoin.Width;
                }
                DecreaseIndent.Visible = Indent > 0;
                if (prev != null) IncreaseIndent.Visible = Indent <= prevItem.Indent;
            }
            #endregion

            base.Refresh();
        }

        void DeleteButton_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                var p = Parent;
                p.Controls.Remove(this);
                p.Refresh();
            }));
        }

        void DecreaseIndent_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                // Decrease current indent
                if ("Where".Equals(Filter.Text)
                    && !Filter.Visible
                    && (Indent > Previous.Indent || Indent > 0))
                {
                    // Decrease next all indent
                    Parent.Controls
                          .OfType<RowItemControlPanel>()
                          .Where(r => (r.Sort == Sort) || (r.Sort > Sort && r.Indent > Indent))
                          .Each(r => { r.Indent--; });
                }
                Parent.Refresh();
            }));
        }

        void IncreaseIndent_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                if ("Where".Equals(Filter.Text)
                    && !Filter.Visible
                    && Indent <= Previous.Indent)
                {
                    Indent++;
                }
                Parent.Refresh();
            }));
        }

        void RowItemControlPanel_Click(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                // Clear others
                Parent.Controls
                      .OfType<RowItemControlPanel>()
                      .Where(r => r.Sort != Sort)
                      .Each(r =>
                      {
                          r.IsSelected = false;
                          r.BackColor = Color.Transparent;
                      });

                if (IsSelected)
                {
                    IsSelected = false;
                    BackColor = Color.Transparent;
                }
                else
                {
                    IsSelected = true;
                    BackColor = Color.LightGoldenrodYellow;
                }
            }));
        }

        void Field_SelectedValueChanged(object sender, EventArgs e)
        {
            Value.Field = Field.SelectedItem as SharePointField;
            var c = new Control();
        }

        void Element_Seleted(object sender, EventArgs e)
        {
            ClearElementSelection();
            var c = sender as Control;
            c.BackColor = Color.LightSkyBlue;
            c.Tag = true;
        }

        void ClearElementSelection()
        {
            //Parent.Controls
            //      .OfType<RowItemControlPanel>()
            //      .
        }
    }
}
