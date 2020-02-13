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
    public partial class Form3 : Form
    {

        public event EventHandler<Callback> TitleCallbackEvent;

        string filePath = "";
        public Form3(string s, string[] t)
        {
            InitializeComponent();
            filePath = s;
        }
        /// <summary>
        /// Oppdaterer filnavnet til det åpnede dokumentet til et nytt.
        /// passerer filnavnet til TitleCallbackEvent metoden i Form1.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            char c = Convert.ToChar(92); //kan ikke skrive "\" (backslash).
            string newPath = "tabs" + c.ToString() + textBox1.Text + ".txt";
            File.Move(filePath, newPath);
            string[] settings = File.ReadAllLines("settings.txt");
            settings[1] = newPath;
            File.WriteAllLines("settings.txt", settings);
            EventHandler<Callback> handler = TitleCallbackEvent;
            if (handler != null)
            {
                Callback x = new Callback("");
                handler(this, x);
            }
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
