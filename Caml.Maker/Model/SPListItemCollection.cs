using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class SPListItemCollection
    {
        public string ListItemCollectionPositionNext;

        public SPListItem[] Items { get; set; }

        public DataView GenerateView(SPList list)
        {
            DataTable table = new DataTable();

            string[] columns = Items.SelectMany(f => f.Attributes.Keys)
                .Distinct()
                .OrderBy(s => s)
                .ToArray();

            columns.Each(col =>
            {
                string name = col.Replace("ows_", "");
                SPField field = list.Fields.FirstOrDefault(f => f.StaticName == name);
                table.Columns.Add(new DataColumn()
                {
                    Caption = field == null ? col : field.DisplayName,
                    ColumnName = col
                });
            });

            Items.Each(f =>
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
}
