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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int selectedY = 1;
        string[] settings = new string[2];
        string[] a = new string[6];
        int posx1;
        int selectedX = 3;
        List<string[]> history = new List<string[]>();
        bool newFile = false;



        private void Form1_Load(object sender, EventArgs e)
        {
            posx1 = label1.Location.X + 12;
            ReadFiles();
            SelectLabel();
            historySave();
            
        }

        private void ReadFiles()
        {
            settings = File.ReadAllLines("settings.txt");
            a = File.ReadAllLines(settings[1]);
            LoadStrings();
            titleLabelUpdate(settings[1]);
        }

        private void LoadStrings()
        {
            label1.Text = a[0].Substring(0, selectedX);
            label2.Text = a[0].Substring(selectedX, 1);
            label3.Text = a[0].Substring(selectedX + 1);

            label4.Text = a[1].Substring(0, selectedX);
            label5.Text = a[1].Substring(selectedX, 1);
            label6.Text = a[1].Substring(selectedX + 1);

            label7.Text = a[2].Substring(0, selectedX);
            label8.Text = a[2].Substring(selectedX, 1);
            label9.Text = a[2].Substring(selectedX + 1);

            label10.Text = a[3].Substring(0, selectedX);
            label11.Text = a[3].Substring(selectedX, 1);
            label12.Text = a[3].Substring(selectedX + 1);

            label13.Text = a[4].Substring(0, selectedX);
            label14.Text = a[4].Substring(selectedX, 1);
            label15.Text = a[4].Substring(selectedX + 1);

            label16.Text = a[5].Substring(0, selectedX);
            label17.Text = a[5].Substring(selectedX, 1);
            label18.Text = a[5].Substring(selectedX + 1);

            label1.Location = new Point(posx1 - 11 * label1.Text.Length, label1.Location.Y);
            label4.Location = new Point(posx1 - 11 * label1.Text.Length, label4.Location.Y);
            label7.Location = new Point(posx1 - 11 * label1.Text.Length, label7.Location.Y);
            label10.Location = new Point(posx1 - 11 * label1.Text.Length, label10.Location.Y);
            label13.Location = new Point(posx1 - 11 * label1.Text.Length, label13.Location.Y);
            label16.Location = new Point(posx1 - 11 * label1.Text.Length, label16.Location.Y);

        }
        private void SelectLabel()
        {
            label17.BackColor = Color.Transparent;
            label14.BackColor = Color.Transparent;
            label11.BackColor = Color.Transparent;
            label8.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            switch (selectedY)
            {
                case 1:
                    label17.BackColor = Color.DeepSkyBlue;
                    break;
                case 2:
                    label14.BackColor = Color.DeepSkyBlue;
                    break;
                case 3:
                    label11.BackColor = Color.DeepSkyBlue;
                    break;
                case 4:
                    label8.BackColor = Color.DeepSkyBlue;
                    break;
                case 5:
                    label5.BackColor = Color.DeepSkyBlue;
                    break;
                case 6:
                    label2.BackColor = Color.DeepSkyBlue;
                    break;
            }
        }


        private void KeyDowsn(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
                if (selectedY < 6)
                    selectedY++;
            if (e.KeyCode == Keys.Down)
                if (selectedY > 1)
                    selectedY--;
            if ((e.KeyCode == Keys.Right) && (selectedX < a[0].Length - 2))
            {
                selectedX++;
                LoadStrings();
                if (label2.Text == "|")
                {
                    selectedX++;
                }
            }

            if ((e.KeyCode == Keys.Left) && (selectedX > 0))
            {
                selectedX--;
                LoadStrings();
                if (label2.Text == "|")
                {
                    selectedX--;
                }
            }
            LoadStrings();
            SelectLabel();
        }

        private void KeyPresss(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if ((c > 48 && c < 58) || (c == 45))
            {
                a[6 - selectedY] = a[6 - selectedY].Remove(selectedX, 1);
                a[6 - selectedY] = a[6 - selectedY].Insert(selectedX, c.ToString());
                LoadStrings();
                historySave();
            }

        }

        private void SaveBtnLabel_Click(object sender, EventArgs e)
        {
            if (newFile)
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.RestoreDirectory = true;
                savefile.FileName = String.Format("{0}.txt", titleLabel.Text);
                savefile.DefaultExt = "*.txt*";
                savefile.Filter = "Text Files|*.txt";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(savefile.FileName, FileMode.CreateNew))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(a);
                    }
                    newFile = false;
                    settings[1] = savefile.FileName;
                    File.WriteAllLines("settings.txt", settings);
                }
            }
                File.WriteAllLines(settings[1], a);
            ReadFiles();
        }

        private void UndoBtnLabel_Click(object sender, EventArgs e)
        {
            if (history.Count > 1)
            {
                history.RemoveAt(history.Count - 1);
                a = history[history.Count - 1];

                LoadStrings();
            }
            if (history.Count == 1)
            {
                history.RemoveAt(history.Count - 1);
                history.Add(a);

            }
            /*
            try
            {
                a = history[history.Count - 2];
                history.RemoveAt(history.Count - 1);
                LoadStrings();
            }
            catch (ArgumentOutOfRangeException) { }*/
            titleLabel.Text = history.Count.ToString();

            ButtonAnimasjon(sender);
        }

        private void historySave()
        {
            string[] n =
            {
                label1.Text + label2.Text + label3.Text,
                label4.Text + label5.Text + label6.Text,
                label7.Text + label8.Text + label9.Text,
                label10.Text + label11.Text + label12.Text,
                label13.Text + label14.Text + label15.Text,
                label16.Text + label17.Text + label18.Text
            };
            history.Add(n);

        }

        private void OpenBtnLabel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Open tab";
            fileDialog.Filter = "Text File|*.txt";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                settings[1] = fileDialog.FileName;
                File.WriteAllLines("settings.txt", settings);
                titleLabelUpdate(fileDialog.FileName);
            }
            ReadFiles();
        }

        private void newBtnLabel_Click(object sender, EventArgs e)
        {
            string[] temp = {
                "e |----------------|",
                "B |----------------|",
                "G |----------------|",
                "D |----------------|",
                "A |----------------|",
                "E |----------------|"};
            a = temp;
            newFile = true;
            titleLabel.Text = "New File";
            LoadStrings();
        }
        private void titleLabelUpdate(string e)
        {
            string[] p = e.Split(Convert.ToChar(92)); //går ikke å bruke '\'
            string t = p[p.Length - 1];
            string[] s = t.Split('.');
            titleLabel.Text = s[0];


        }

        private void EditBtnLabel_Click(object sender, EventArgs e)
        {
            MenuForm1 f = new MenuForm1();
            f.AdviseParent += new EventHandler<AdviseParentEventArgs>();
            f.Show();

        }

        private void ButtonAnimasjon(object sender)
        {
            Label b = sender as Label;
            b.BackColor = Color.DeepSkyBlue;
            timerBtnAnimasjon.Start();
        }

        private void timerBtnAnimasjon_Tick(object sender, EventArgs e)
        {
            undoBtnLabel.BackColor = Color.White;
            timerBtnAnimasjon.Stop();
        }
    }

}
