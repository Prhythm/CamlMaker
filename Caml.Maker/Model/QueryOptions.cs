using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class QueryOptions
    {
        public bool? DateInUtc { get; set; }
        public string Folder { get; set; }
        public string Paging { get; set; }
        public bool? IncludeMandatoryColumns { get; set; }
        public MeetingInstanceID? MeetingInstanceID { get; set; }
        public string ViewAttributes { get; set; }

        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder("<QueryOptions>");
            StringBuilder sb = new StringBuilder();

            if (DateInUtc != null) sb.AppendFormat("<DateInUtc>{1}</DateInUtc>", DateInUtc.ToString().ToUpper());
            if (Folder != null) sb.AppendFormat("<Folder>{0}</Folder>", Folder);
            if (Paging != null) sb.AppendFormat("<Paging>{0}</Paging>", Paging);
            if (IncludeMandatoryColumns != null) sb.AppendFormat("<IncludeMandatoryColumns>{0}</IncludeMandatoryColumns>", IncludeMandatoryColumns.ToString().ToUpper());
            if (MeetingInstanceID != null) sb.AppendFormat("<MeetingInstanceID>{0}</MeetingInstanceID>", (int)MeetingInstanceID);
            if (ViewAttributes != null) sb.AppendFormat("<ViewAttributes>{0}</ViewAttributes>", ViewAttributes);

            //return sb.Append("</QueryOptions>").ToString();
            return sb.ToString();
        }
    }

    public enum MeetingInstanceID : int
    {
        UnSpecified = -3, AllWithSeries = -2, AllButSeries = -1, Series = 0
    }
}
