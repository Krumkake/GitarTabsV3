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
    public partial class MenuForm1 : Form
    {
        public event EventHandler<AdviseParentEventArgs> AdviseParent;
        public MenuForm1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*EventHandler<EventArgs> handler = this.CallBack;
            // if not null, call handler
            if (handler != null)
            {
                // you can make custom EventArgs, pass parameter with it
                handler(this, new EventArgs());
            }*/
            string[] a = new string[6];
            int count = Convert.ToInt32(textBox3.Text) * Convert.ToInt32(textBox1.Text);
            int length = Convert.ToInt32(LengthTextBox.Text);
            for (int i = 0; i < length; i++)
            {
                for (int l = 0; l < 6; l++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        a[i].Append<char>('-');
                    }
                }
                for (int k = 0; k < 6; k++)
                {
                    a[k].Append<char>('|');
                }
            }

        }
        protected virtual void glont()
        {

        }
    }
    public class AdviseParentEventArgs : EventArgs
    {
        private readonly string adviseText;
        public AdviseParentEventArgs(string adviseText)
        {
            this.adviseText = adviseText;
        }
        public string AdviseText
        {
            get { return adviseText; }
        }
    }




}
