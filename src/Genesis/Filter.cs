using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genesis
{
    public partial class Filter : Form
    {
        public Filter()
        {
            InitializeComponent();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            startswith.Text = Network.Filters.startsWith;
            endswith.Text = Network.Filters.endsWith;
            contains.Text = Network.Filters.includes;   
        }

        private void startswith_TextChanged(object sender, EventArgs e)
        {
            Network.Filters.startsWith = startswith.Text;
        }

        private void endswith_TextChanged(object sender, EventArgs e)
        {
            Network.Filters.endsWith = endswith.Text;
        }

        private void contains_TextChanged(object sender, EventArgs e)
        {
            Network.Filters.includes = contains.Text;
        }
    }
}
