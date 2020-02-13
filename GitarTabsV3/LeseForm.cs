using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;

namespace GitarTabsV3
{
    public partial class LeseForm : Form
    {
        string[] text = { };
        public LeseForm(string[] a)
        {
            InitializeComponent();
            text = a;
        }
        /// <summary>
        /// laster inn hele teksten i det åpnede dokumentet inn i en richtextbox.
        /// </summary>
        private void LeseForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            richTextBox1.Size = new Size(this.Width - 60, this.Height - 50);
            string[] settings = File.ReadAllLines("settings.txt");
            for (int i = 0; i < text.Length; i++)
            {
                richTextBox1.AppendText(text[i] + Environment.NewLine);
                int k = i + 1;
                //lager mellomrom for hver sjette linje.
                if ((k%6 == 0)&&(i>1))
                    richTextBox1.AppendText(Environment.NewLine);
            }

        }
    }
}
