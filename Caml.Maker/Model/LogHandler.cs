using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caml.Maker.Model
{
    public class LogHandler
    {
        public static System.Windows.Forms.TextBox LogArea;

        public static void Log(string message, params object[] param)
        {
            if (LogArea != null) LogArea.AppendText(string.Format(message, param));
        }

        public static void Clear()
        {
            if (LogArea != null) LogArea.Text = string.Empty;
        }
    }
}
