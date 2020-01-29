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
/// <summary>
/// TODO:
/// - fixe undo
/// - bli ferdig med adding
/// - få til markering og sletting av deler
/// - kopiering/pasting
/// - markere hele BARS og kopiere/slette/klipp ut
/// - redo funksjonalitet
/// - få til ctrl + c/v/x/z/y
/// </summary>

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
        /// <summary>
        /// ReadFiles() laster inn text i settings.txt til et array settings. 
        /// På grunn av at filbanen ikke er relativ skal programmet leveres til 
        /// en annen datamaskin uten settings.txt slik at programmet ikke krasjer.
        /// </summary>
        
        private void ReadFiles()
        {
            if (!File.Exists("settings.txt") || settings.Length == 1)
            {
                Console.WriteLine("No file path");
                return;
            }
            settings = File.ReadAllLines("settings.txt");
            try
            {
                a = File.ReadAllLines(settings[1]);
                LoadStrings();
                titleLabelUpdate(settings[1]);
            }
            catch
            {
                Form2 errorForm = new Form2();
                string message = "Last opened file does not exist";
                errorForm.ShowError(message);
                errorForm.Show();
            }
            
            
        }
        /// <summary>
        /// LoadStrings() metoden oppdaterer alle labels i Form1 til å vise alle strings i a[].
        /// Texten deles inn i 3 substrings per linje, totalt 18.
        /// Labels lengst til venstre lyttes til en ny posisjon som avhenger av lengden til strengen i labelen.
        /// Alle vil da flyttes like mye fordi de har alle samme antall char. 
        /// </summary>
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
        /// <summary>
        /// Selectlabel funksjonen skifter farge på den valgte labelen.
        /// </summary>
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
        /// <summary>
        /// KeyDowsn har sitt nanf fordi det ikke er mulig å kalle den KeyDown. 
        /// Funksjonen registrerer alle knappetrykk, men håndterer ikke char verdiene.
        /// Brukes kun til registrering av piltastetrykk for å endre valgt index.
        /// </summary>
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
        /// <summary>
        /// KeyPresss har sitt navn fordi det ikke er mulig å kalle den KeyPress.
        /// Funskjonen registrerer alle tastetrykk og tar kun input fra charverdier til tall 
        /// og "-".
        /// </summary>
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
        /// <summary>
        /// SaveBtnLabel_Click registrerer musetrukk på en label "Save".
        /// Funksjonen skriver alt som er i displayet (a[]) til dens riktige 
        /// filbane som den får fra settings[1].
        /// Hvis det er en helt ny fil, har dokumentet ikke en filbane. 
        /// En SaveFileDialog vil da åpnes hvor bruker 
        /// kan velge tittel og hvor dokumentet skal lagres.
        /// </summary>
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
        /// <summary>
        /// Ikke Ferdig
        /// 
        /// </summary>
        /// 
        private void UndoBtnLabel_Click(object sender, EventArgs e)
        {
            if (history.Count > 1)
            {

                a = history[history.Count - 2];
                history.RemoveAt(history.Count - 1);

                LoadStrings();
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
            foreach (string[] item in history)
            {
                Console.WriteLine(item[0]);

            }
            Console.WriteLine("glont");

        }
        /// <summary>
        /// histrorySave lagrer elle endringer av a[] til en liste. Brukes i Undo funskjonen.
        /// </summary>
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
            foreach (string[] item in history)
            {
                Console.WriteLine(item[0]);
            }
            Console.WriteLine("glont");
        }
        /// <summary>
        /// OpenBtnLabel_Click registrerer museklikk på label med text "Open".
        /// Den åpner en OpenFileDialog hvor bruker velger hvilken .txt fil som programmet skal laste inn.
        /// </summary>
        private void OpenBtnLabel_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Open tab";
            fileDialog.Filter = "Text File|*.txt";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] previousOpenedFile = { "Siste tab: ", fileDialog.FileName };
                if (!File.Exists("settings.txt"))
                {
                    using (FileStream fs = File.Create("settings.txt"))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                        fs.Write(title, 0, title.Length);
                    }
                }
                File.WriteAllLines("settings.txt", previousOpenedFile);
                titleLabelUpdate(fileDialog.FileName);
            }
            ReadFiles();
        }
        /// <summary>
        /// NeyBtnLabel_Click registrerer museklikk på label med text "New".
        /// Den setter teksten i displayet til en blank fil. newFile bool settes 
        /// til true slik at SaveBtnLabel_Click vet at det er en helt ny fil som 
        /// ikke har en eksisterende filbane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// titleLabelUpdate setter text i TitleLabel til innlastet fil sitt filnavn.
        /// Filnavnet filtreres ut ved å splitte filbanen med char "\" med ascii verdi 92.
        /// siste index av string[] p splittes med char "." slik at ".txt ikke blir med.
        /// </summary>
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
            f.AdviseParent += new EventHandler<Callback>(adviseParent);
            f.Show();
        }
        /// <summary>
        /// tar imot callback
        /// </summary>
        private void adviseParent(object sender, Callback e)
        {
            string[] temp = a;
            int i = 0;
            foreach (string item in a)
            {
                temp[i] += e.AdviseText;
                i++;
            }
            a = temp;
            LoadStrings();
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