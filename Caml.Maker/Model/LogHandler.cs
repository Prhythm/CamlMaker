using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Caml.Maker.Model
{
    public class LogHandler
    {
        public static System.Windows.Forms.TextBox LogArea;

        public static void Log(string message, params object[] param)
        {
            if (LogArea != null)
            {
                LogArea.Invoke(new MethodInvoker(() =>
                {
                    if (param == null || param.Length == 0)
                        LogArea.AppendText(message);
                    else
                        LogArea.AppendText(string.Format(message, param));
                }));
            }
        }

        public static void Clear()
        {
            if (LogArea != null)
            {
                LogArea.Invoke(new MethodInvoker(() =>
                {
                    LogArea.Text = string.Empty;
                }));
            }
        }
    }
}
