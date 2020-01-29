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
        public event EventHandler<Callback> AdviseParent;
        public MenuForm1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "";
            int teller = Convert.ToInt32(textBox3.Text);
            int nevner = Convert.ToInt32(textBox1.Text);
            int count = teller * nevner;
            int lengde = Convert.ToInt32(LengthTextBox.Text);
            for (int i = 0; i < count; i++)
            {
                s += "-";
            }
            s += "|";
            string temp = s;
            for (int i = 0; i < lengde - 1; i++)
            {
                s = s + temp;
            }
            EventHandler<Callback> handler = AdviseParent;
            // if not null, call handler
            if (handler != null)
            {
                Callback x = new Callback(s);
                // you can make custom EventArgs, pass parameter with it
                handler(this, x);
            }
            this.Close();
        }

        private void MenuForm1_Load(object sender, EventArgs e)
        {

        }
    } 
}
public class Callback : EventArgs
{
    private readonly string adviseText;
    public Callback(string adviseText)
    {
        this.adviseText = adviseText;
    }
    public string AdviseText
    {
        get { return adviseText; }
    }
}
