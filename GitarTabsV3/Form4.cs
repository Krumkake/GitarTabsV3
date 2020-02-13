using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitarTabsV3
{
    public partial class Form4 : Form
    {
        string message;
        public Form4(string m)
        {
            InitializeComponent();
            message = m;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }
        public void showMessage()
        {
            label1.Text = message;
        }
    }
}
