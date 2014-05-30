using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class ComparisonOperator
    {
        public string Description { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Description, Name);
        }
    }
}
