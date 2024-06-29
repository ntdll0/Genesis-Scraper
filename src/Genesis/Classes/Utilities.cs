using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genesis
{
    class Utilities
    {
        public static void ChangeStatus(string status, int founds, Form form)
        {
            form.Text = $"Genesis Alpha v0.1 ~ Status: {status}  |  Found: {founds} URL";
        }
    }
}
