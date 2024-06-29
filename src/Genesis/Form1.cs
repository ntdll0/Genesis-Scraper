using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Genesis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class SelectedModules
        {
            public static bool AOL;
            public static bool startPage;
            public static bool QWANT;
            public static bool duckDuckGo;
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 3)
            {
                Utilities.ChangeStatus("Running...", 0, this);
                timer1.Enabled = true;
                foreach (string s in richTextBox1.Text.Split('\n'))
                    if (s != "")
                    {
                        if (SelectedModules.AOL)
                            Task.Run(() => Network.SearchReqAOL(this, s, (int)numericUpDown2.Value, (int)numericUpDown1.Value));
                        if (SelectedModules.startPage)
                            Task.Run(() => Network.SearchReqStartPage(this, s, (int)numericUpDown2.Value, (int)numericUpDown1.Value));
                        if (SelectedModules.QWANT)
                            Task.Run(() => Network.SearchReqQ(this, s, (int)numericUpDown2.Value, (int)numericUpDown1.Value));
                        if (SelectedModules.duckDuckGo)
                            Task.Run(() => Network.SearchReqDuckDuckGo(this, s, (int)numericUpDown2.Value, (int)numericUpDown1.Value));
                    }
            }
            else MessageBox.Show("Please, enter list");
        }

        Filter f = new Filter();
        private void stopButton_Click(object sender, EventArgs e)
        {
            if (f.IsDisposed)
                f = new Filter();
            f.ShowDialog();
            f.BringToFront();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string constructed="";
            foreach (ListViewItem i in aeroListView1.Items)
                constructed += i.SubItems[3].Text + "\n";

            Clipboard.SetText(constructed);
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aeroListView1.Items.Clear();
        }

        private void addPrefixToEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = (string)Interaction.InputBox("Enter prefix", "Enter prefix");
            if (s == "") return;
            string construction = "";
            foreach (string ss in richTextBox1.Text.Split('\n'))
                if (ss != "")
                    construction += $"{s}{ss}\n";
            while (construction.EndsWith("\n") || construction.EndsWith("\r"))
                construction = construction.Substring(0, construction.Length - 1);
            richTextBox1.Text = construction;
        }

        private void addSuffixToEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = (string)Interaction.InputBox("Enter suffix", "Enter suffix");
            if (s == "") return;
            string construction = "";
            foreach (string ss in richTextBox1.Text.Split('\n'))
                if (ss != "")
                    construction += $"{ss}{s}\n";
            while (construction.EndsWith("\n") || construction.EndsWith("\r"))
                construction = construction.Substring(0, construction.Length - 1);
            richTextBox1.Text = construction;
        }

        private void removeBlankLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string construction = "";
            foreach (string ss in richTextBox1.Text.Split('\n'))
                if (ss != "")
                    construction += $"{ss}\n";
            while (construction.EndsWith("\n") || construction.EndsWith("\r"))
                construction = construction.Substring(0, construction.Length - 1);
            richTextBox1.Text = construction;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Utilities.ChangeStatus("Running...", aeroListView1.Items.Count, this);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedModules.AOL = checkedListBox1.GetItemChecked(0);
            SelectedModules.startPage = checkedListBox1.GetItemChecked(1);
            SelectedModules.QWANT = checkedListBox1.GetItemChecked(2);
            SelectedModules.duckDuckGo = checkedListBox1.GetItemChecked(3);
        }
    }
}
