using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class SPListItem
    {
        public Dictionary<string, string> Attributes { get; private set; }

        public SPListItem()
        {
            Attributes = new Dictionary<string, string>();
        }
    }
}
