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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void ShowError(string s)
        {
            label1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete("settings.txt");
        }
    }
}
