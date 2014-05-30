using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class SharePointField
    {
        public string DisplayName { get; set; }
        public string InternalName { get; set; }
        public string Type { get; set; }
        public object[] Values { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}