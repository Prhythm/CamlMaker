using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caml.Maker.Model
{
    public class RowItem
    {
        public static ComboBox.ObjectCollection AllFilter;
        public static ComboBox.ObjectCollection AllLogicalJoins;
        public static ComboBox.ObjectCollection AllField;
        public static ComboBox.ObjectCollection AllComparisonOperators;
        public static ComboBox.ObjectCollection AllValue;

        public int Index { get; set; }
        public int Indent { get; set; }

        public string Filter { get; set; }
        public string LogicalJoins { get; set; }
        public SharePointField Field { get; set; }
        public ComparisonOperator ComparisonOperators { get; set; }
        public string Value { get; set; }
    }
}
