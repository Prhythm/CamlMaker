using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Caml.Maker.Model
{
    public class SPList : System.Windows.Forms.ListViewItem
    {
        #region Members
        public SPField[] Fields { get; set; }

        public DataView FieldView
        {
            get
            {
                DataTable table = new DataTable();

                List<string> columns = new List<string>(new string[] { "ID", "DisplayName", "Name", "StaticName", "Type", "Required", "ReadOnly", "Hidden" });
                Fields.SelectMany(f => f.Attributes.Keys)
                    .OrderBy(s => s)
                    .Each(name => { if (!columns.Contains(name)) columns.Add(name); });

                columns.Each(col => table.Columns.Add(col));

                Fields.Each(f =>
                {
                    DataRow row = table.Rows.Add();
                    f.Attributes.Each(a =>
                    {
                        row[a.Key] = a.Value;
                    });
                });

                return table.AsDataView();
            }
        }

        public string DefaultViewUrl { get; set; }

        public Guid ID { get; set; }

        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        public string Description
        {
            get { return ToolTipText; }
            set { ToolTipText = value; }
        }

        public System.Drawing.Image Image { get; set; }

        public bool Hidden { get; set; }
        #endregion

        public override string ToString()
        {
            return Title;
        }
    }
}
