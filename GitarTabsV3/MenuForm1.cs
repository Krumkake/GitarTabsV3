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
        public event EventHandler<Callback> callbackEvent;
        public MenuForm1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// En streng blir konstruert basert på tidssignaturen man skriver inn.
        /// strengen passerer som argument til Form1 sin callbackEvent metode, og trigger samtidig denne metoden.
        /// 
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            string s = "";
            int teller = Convert.ToInt32(numericUpDown1.Text);
            int nevner = Convert.ToInt32(numericUpDown2.Text);
            int count = teller * nevner;
            int lengde = Convert.ToInt32(numericUpDown3.Text);
            for (int i = 0; i < count; i++)
            {
                s += "-  ";
            }
            s += "|";
            string temp = s;
            for (int i = 0; i < lengde - 1; i++)
            {
                s = s + temp;
            }
            EventHandler<Callback> handler = callbackEvent;
            // if not null, call handler
            if (handler != null)
            {
                Callback x = new Callback(s);
                // you can make custom EventArgs, pass parameter with it
                handler(this, x);
            }
            this.Close();
        }
    } 
}

/// <summary>
/// Dette er en klasse som håndterer callback funskjonaliteten. 
/// Metiden Callback tar en string som parameter.
/// denne strengen kan passere fra en form til en annen.
/// Du (Rune) hjalp meg med dette.
/// Kilde: https://social.msdn.microsoft.com/Forums/vstudio/en-US/d7e9f80c-5144-4ad9-aa5e-7803363ae0cd/passing-variables-between-forms?forum=csharpgener
/// </summary>
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
