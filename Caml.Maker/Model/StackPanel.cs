using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Caml.Maker.Model
{
    public class StackPanel : Panel
    {
        public event EventHandler Adjusted;

        public StackPanel()
            : base()
        {
            this.AutoScroll = true;
        }

        public override void Refresh()
        {
            base.Refresh();
            AdjustChildControls();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            AdjustChildControls();
        }

        void AdjustChildControls()
        {
            Point p = new Point(1, 1);

            Controls.OfType<RowItemControlPanel>()
                    .OrderBy<RowItemControlPanel, RowItemControlPanel>(e => e, new RowItemSorter())
                    .Each((item, i) =>
                    {
                        item.Location = new Point(p.X, p.Y);

                        item.Sort = i;
                        // Set width to fit container
                        item.Width = Width - 5 - (this.VScroll ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0);
                        item.Refresh();

                        // next location
                        p = new Point(p.X, p.Y + item.Height + 1);
                    });

            if (Adjusted != null) Adjusted.Invoke(this, EventArgs.Empty);
        }

        public string ToCaml()
        {
            var sb = new StringBuilder("<Query>");

            #region Where clause
            var cs = Controls.OfType<RowItemControlPanel>()
                             .Where(r => "Where".Equals(r.ContentItem.Filter))
                             .OrderBy(r => r.Sort);
            if (cs.Any())
            {
                sb.Append("<Where>");
                int end;
                sb.Append(BuildWhereClause(cs.ToArray(), 0, out end));
                sb.Append("</Where>");
            }
            #endregion

            #region GroupBy clause
            cs = Controls.OfType<RowItemControlPanel>()
                    .Where(r => "GroupBy".Equals(r.ContentItem.Filter))
                    .OrderBy(r => r.Sort);
            if (cs.Any())
            {
                sb.Append("<GroupBy>");
                cs.Each(c =>
                {
                    var item = c.ContentItem;

                    sb.AppendFormat(
                        @"<FieldRef Name=""{0}""/>",
                        item.Field.InternalName
                    );
                });
                sb.Append("</GroupBy>");
            }
            #endregion

            #region OrderBy clause
            cs = Controls.OfType<RowItemControlPanel>()
                 .Where(r => "OrderBy".Equals(r.ContentItem.Filter))
                 .OrderBy(r => r.Sort);
            if (cs.Any())
            {
                sb.Append("<OrderBy>");
                cs.Each(c =>
                {
                    var item = c.ContentItem;

                    sb.AppendFormat(
                        @"<FieldRef Name=""{0}""/>",
                        item.Field.InternalName
                    );
                });
                sb.Append("</OrderBy>");
            }
            #endregion

            return FormatXml(sb.Append("</Query>").ToString());
            //return sb.Append("</Query>").ToString();
        }

        string BuildWhereClause(RowItemControlPanel[] csa, int start, out int end)
        {
            var buffer = new List<string>();
            end = start;

            for (int i = start; i < csa.Length; i++)
            {
                end = i;
                var current = csa[i];
                var currentItem = current.ContentItem;

                #region Special value
                switch (currentItem.Field.Type)
                {
                    case "User":
                        if ("UserID".Equals(currentItem.Value)) currentItem.Value = "<UserID/>";
                        break;
                    case "DateTime":
                        switch (currentItem.Value)
                        {
                            case "Today":
                            case "Now":
                                currentItem.Value = string.Format("<{0}/>", currentItem.Value);
                                break;
                        }
                        break;
                }
                #endregion

                var term = string.Format(
                    @"<{0}><FieldRef Name=""{1}""/><Value Type=""{2}"">{3}</Value></{0}>",
                    currentItem.ComparisonOperators.Name,
                    currentItem.Field.InternalName,
                    currentItem.Field.Type,
                    currentItem.Value
                );

                // Current
                buffer.Add(term);

                var next = i < csa.Length - 1 ? csa[i + 1] : null;
                var prev = i > 0 ? csa[i - 1] : null;

                if (next == null)
                {
                    // last one
                    if (!string.IsNullOrEmpty(currentItem.LogicalJoins))
                    {
                        buffer.Insert(0, string.Format("<{0}>", currentItem.LogicalJoins));
                        buffer.Add(string.Format("</{0}>", currentItem.LogicalJoins));
                    }
                }
                else if (next.Indent > current.Indent)
                {
                    if (!string.IsNullOrEmpty(currentItem.LogicalJoins))
                    {
                        buffer.Insert(0, string.Format("<{0}>", currentItem.LogicalJoins));
                        buffer.Add(string.Format("</{0}>", currentItem.LogicalJoins));
                    }

                    var result = BuildWhereClause(csa, i + 1, out end);
                    buffer.Insert(0, string.Format("<{0}>", next.ContentItem.LogicalJoins));
                    buffer.Add(result);
                    buffer.Add(string.Format("</{0}>", next.ContentItem.LogicalJoins));
                    i = end;
                }
                else if (next.Indent < current.Indent)
                {
                    return string.Join(string.Empty, buffer.ToArray());
                }
                else
                {
                    if (prev == null)
                    {
                        // first one
                    }
                    else if (prev.Indent < current.Indent)
                    {
                        // ignore
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(currentItem.LogicalJoins))
                        {
                            buffer.Insert(0, string.Format("<{0}>", currentItem.LogicalJoins));
                            buffer.Add(string.Format("</{0}>", currentItem.LogicalJoins));
                        }
                    }
                }
            }

            return string.Join(string.Empty, buffer.ToArray());
        }

        string FormatXml(string inputXml)
        {
            var document = new XmlDocument();
            document.Load(new StringReader(inputXml));

            var builder = new StringBuilder();
            using (var writer = new XmlTextWriter(new StringWriter(builder)))
            {
                writer.Formatting = Formatting.Indented;
                //writer.IndentChar = '\t';
                //writer.Indentation = 1;
                document.Save(writer);
            }

            return string.Join(
                Environment.NewLine,
                builder.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray()
            );
        }
    }

    class RowItemSorter : IComparer<RowItemControlPanel>
    {
        public int Compare(RowItemControlPanel x, RowItemControlPanel y)
        {
            int xf = 0, yf = 0;

            RowItem.AllFilter.Each<object>((r, i) =>
            {
                if (r.Equals(x.Filter.SelectedItem)) xf = i;
                if (r.Equals(y.Filter.SelectedItem)) yf = i;
            });

            if (xf - yf != 0) return xf - yf;
            return x.Sort - y.Sort;
        }
    }
}
