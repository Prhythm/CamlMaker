using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class SPField
    {
        public Dictionary<string, string> Attributes { get; private set; }

        public string DisplayName
        {
            get
            {
                if (Attributes.ContainsKey("DisplayName"))
                    return Attributes["DisplayName"];
                else
                    return null;
            }
        }
        public string Type
        {
            get
            {
                if (Attributes.ContainsKey("Type"))
                    return Attributes["Type"];
                else
                    return null;
            }
        }
        public string StaticName
        {
            get
            {
                if (Attributes.ContainsKey("StaticName"))
                    return Attributes["StaticName"];
                else
                    return null;
            }
        }
        public Guid ID
        {
            get
            {
                if (Attributes.ContainsKey("ID"))
                    return new Guid(Attributes["ID"]);
                else
                    return Guid.Empty;
            }
        }
        public bool Hidden
        {
            get
            {
                if (Attributes.ContainsKey("Hidden"))
                    return Convert.ToBoolean(Attributes["Hidden"]);
                else
                    return false;
            }
        }

        public object[] Values { get { return new object[0]; } }

        public SPField()
        {
            Attributes = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}