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
        //Globale Variabler:
        int selectedY = 1;
        int selectedRow = 0;
        string[] settings = new string[2];
        string[] a = new string[6];
        int posx1;
        int forskyvning = 11;
        int selectedX = 3;
        List<string[]> history = new List<string[]>();
        bool newFile = false;
        bool newNote = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            posx1 = label1.Location.X + forskyvning;
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
            if (!File.Exists("settings.txt"))
            {
                Console.WriteLine("No file path");
                File.Create("settings.txt");
                NyFil();
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
                //sender en errormelding som vises i Form2 instansen
                Form2 errorForm = new Form2();
                string message = "Last opened file does not exist";
                errorForm.ShowError(message);
                errorForm.Show();
            }


        }
        /// <summary>
        /// LoadStrings() metoden oppdaterer alle labels i Form1 til å vise texten i a[].
        /// I den midterste hovedraden deles texten inn i 3 substrings per linje, totalt 18.
        /// Labels lengst til venstre flyttes til en ny posisjon som avhenger av lengden til strengen i labelen.
        /// Alle vil da flyttes like mye fordi de har alle samme antall char. 
        /// Radene øverst og nederst består av 6 labeler hver.
        /// </summary>
        private void LoadStrings()
        {
            int TopRowFilled = 0;
            int TopRowOnTop = 0;
            if ((a.Length > 6) && (selectedRow > 0))
            {
                if (selectedRow > 1)
                    TopRowOnTop = 1;
                int x = TopRowOnTop * (selectedRow - 1) * 6;
                label1R2.Text = a[0 + x];
                label2R2.Text = a[1 + x];
                label3R2.Text = a[2 + x];
                label4R2.Text = a[3 + x];
                label5R2.Text = a[4 + x];
                label6R2.Text = a[5 + x];
                TopRowFilled = 1;
            }
            if (selectedRow == 0)
            {
                label1R2.Text = "";
                label2R2.Text = "";
                label3R2.Text = "";
                label4R2.Text = "";
                label5R2.Text = "";
                label6R2.Text = "";
            }
            int k = TopRowFilled * selectedRow * 6;

                label1.Text = a[0 + k].Substring(0, selectedX);
                labelM1.Text = a[0 + k].Substring(selectedX, 1);
                label3.Text = a[0 + k].Substring(selectedX + 1);

                label4.Text = a[1 + k].Substring(0, selectedX);
                labelM2.Text = a[1 + k].Substring(selectedX, 1);
                label6.Text = a[1 + k].Substring(selectedX + 1);

                label7.Text = a[2 + k].Substring(0, selectedX);
                labelM3.Text = a[2 + k].Substring(selectedX, 1);
                label9.Text = a[2 + k].Substring(selectedX + 1);

                label10.Text = a[3 + k].Substring(0, selectedX);
                labelM4.Text = a[3 + k].Substring(selectedX, 1);
                label12.Text = a[3 + k].Substring(selectedX + 1);

                label13.Text = a[4 + k].Substring(0, selectedX);
                labelM5.Text = a[4 + k].Substring(selectedX, 1);
                label15.Text = a[4 + k].Substring(selectedX + 1);

                label16.Text = a[5 + k].Substring(0, selectedX);
                labelM6.Text = a[5 + k].Substring(selectedX, 1);
                label18.Text = a[5 + k].Substring(selectedX + 1);
            

            label1.Location = new Point(posx1 - forskyvning * label1.Text.Length, label1.Location.Y);
            label4.Location = new Point(posx1 - forskyvning * label1.Text.Length, label4.Location.Y);
            label7.Location = new Point(posx1 - forskyvning * label1.Text.Length, label7.Location.Y);
            label10.Location = new Point(posx1 - forskyvning * label1.Text.Length, label10.Location.Y);
            label13.Location = new Point(posx1 - forskyvning * label1.Text.Length, label13.Location.Y);
            label16.Location = new Point(posx1 - forskyvning * label1.Text.Length, label16.Location.Y);

            label1R2.Location = new Point(posx1 - forskyvning * label1.Text.Length, label1R2.Location.Y);
            label2R2.Location = new Point(posx1 - forskyvning * label1.Text.Length, label2R2.Location.Y);
            label3R2.Location = new Point(posx1 - forskyvning * label1.Text.Length, label3R2.Location.Y);
            label4R2.Location = new Point(posx1 - forskyvning * label1.Text.Length, label4R2.Location.Y);
            label5R2.Location = new Point(posx1 - forskyvning * label1.Text.Length, label5R2.Location.Y);
            label6R2.Location = new Point(posx1 - forskyvning * label1.Text.Length, label6R2.Location.Y);

            label1R3.Location = new Point(posx1 - forskyvning * label1.Text.Length, label1R3.Location.Y);
            label2R3.Location = new Point(posx1 - forskyvning * label1.Text.Length, label2R3.Location.Y);
            label3R3.Location = new Point(posx1 - forskyvning * label1.Text.Length, label3R3.Location.Y);
            label4R3.Location = new Point(posx1 - forskyvning * label1.Text.Length, label4R3.Location.Y);
            label5R3.Location = new Point(posx1 - forskyvning * label1.Text.Length, label5R3.Location.Y);
            label6R3.Location = new Point(posx1 - forskyvning * label1.Text.Length, label6R3.Location.Y);

            if (a.Length > 6 + 6 * selectedRow)
            {
                label1R3.Text = a[6 + selectedRow * 6];
                label2R3.Text = a[7 + selectedRow * 6];
                label3R3.Text = a[8 + selectedRow * 6];
                label4R3.Text = a[9 + selectedRow * 6];
                label5R3.Text = a[10 + selectedRow * 6];
                label6R3.Text = a[11 + selectedRow * 6];
            }
            if (selectedRow == a.Length / 6 - 1)
            {
                label1R3.Text = "";
                label2R3.Text = "";
                label3R3.Text = "";
                label4R3.Text = "";
                label5R3.Text = "";
                label6R3.Text = "";
            }
            labelRows.Text = selectedRow.ToString();

        }
        /// <summary>
        /// Selectlabel funksjonen skifter farge på den valgte labelen.
        /// </summary>
        private void SelectLabel()
        {
            labelM6.BackColor = Color.Transparent;
            labelM5.BackColor = Color.Transparent;
            labelM4.BackColor = Color.Transparent;
            labelM3.BackColor = Color.Transparent;
            labelM2.BackColor = Color.Transparent;
            labelM1.BackColor = Color.Transparent;
            switch (selectedY)
            {
                case 1:
                    labelM6.BackColor = Color.DeepSkyBlue;
                    break;
                case 2:
                    labelM5.BackColor = Color.DeepSkyBlue;
                    break;
                case 3:
                    labelM4.BackColor = Color.DeepSkyBlue;
                    break;
                case 4:
                    labelM3.BackColor = Color.DeepSkyBlue;
                    break;
                case 5:
                    labelM2.BackColor = Color.DeepSkyBlue;
                    break;
                case 6:
                    labelM1.BackColor = Color.DeepSkyBlue;
                    break;
            }
        }
        /// <summary>
        /// KeyDowsn har sitt navn fordi det ikke er mulig å kalle den KeyDown. 
        /// Funksjonen registrerer alle knappetrykk, men håndterer ikke char verdiene.
        /// Brukes kun til registrering av piltastetrykk og backspace.
        /// </summary>
        private void KeyDowsn(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (selectedY < 6)
                    selectedY++;
                else if ((selectedRow > 0)&&(selectedX < a[5 + 6*selectedRow].Length))
                {
                    selectedY = 1;
                    selectedRow--;
                }
                newNote = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                if (selectedY > 1)
                    selectedY--;
                else if (selectedRow * 6 + 6 < a.Length)
                {
                    selectedY = 6;
                    selectedRow++;
                }
                newNote = true;
            }
            if ((e.KeyCode == Keys.Right) && (selectedX < a[selectedRow*6].Length - 2))
            {
                FlyttHøyreVenstre(1);
                newNote = true;
            }
            if ((e.KeyCode == Keys.Left) && (selectedX > 0))
            {
                FlyttHøyreVenstre(-1);
                newNote = true;
            }
            // sletteknapp på BACKSPACE
            if ((e.KeyCode == Keys.Back))
            {
                a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Remove(selectedX, 2);
                a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Insert(selectedX, "- ");
                newNote = true;
            }
            LoadStrings();
            SelectLabel();
        }

        /// <summary>
        /// Flytter selectedX til neste verdi når man trykker høyre og venstre piltast.
        /// Funksjonen spoler gjennom alle mellomrom og '|' i texten.
        /// Passer også på at man ikke kan spole lengre enn mulig til siden.
        /// </summary>
        private void FlyttHøyreVenstre(int f)
        {
            if ((label3.Text.Length < 4) && (f == 1))
                f = 0;
            selectedX = selectedX + f;
            LoadStrings();
            while ((labelM1.Text == " ") || (labelM1.Text == "|"))
            {
                if ((label3.Text.Length < 4) && (f == 1))
                    break;
                selectedX = selectedX + f;
                LoadStrings();
            }
        }
        /// <summary>
        /// KeyPresss har sitt navn fordi det ikke er mulig å kalle den KeyPress.
        /// Funskjonen registrerer alle tastetrykk. Kun charverdier til tall og '-' blir satt inn på plass i texten.
        /// Et unntak er når valgt plass er lengst mulig til venstre, da kan man velge hvilken stemming gitaren har
        /// ved å bruke bokstaver.
        /// </summary>
        private void KeyPresss(object sender, KeyPressEventArgs e)
        {
            char temp = e.KeyChar;
            char c = '-'; //må deklares her. har ikke noe å si hva den er deklarert som, men den må være noe.
            if ((temp > 47 && temp < 58) || (temp == '-') || (label1.Text.Length == 0))
            {
                c = temp;
                if (newNote == true)
                {
                    a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Remove(selectedX, 1);
                    a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Insert(selectedX, c.ToString());
                    a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Remove(selectedX + 1, 1);
                    a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Insert(selectedX + 1, " ");
                    newNote = false;
                }
                else
                {
                    a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Remove(selectedX + 1, 1);
                    a[6 + 6 * selectedRow - selectedY] = a[6 + 6 * selectedRow - selectedY].Insert(selectedX + 1, c.ToString());
                }
            }
            LoadStrings();
            historySave();
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
            save();
            ButtonAnimasjon(sender);
        }
        /// <summary>
        /// lagrer dokumentet til en .txt fil.
        /// hvis filen er ny, åpnes en ny fildialog hvor man velger tittel og hvor filen skal lagres.
        /// </summary>
        private void save()
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

                    if (!File.Exists("settings.txt"))
                    {
                        using (FileStream fs = File.Create("settings.txt"))
                        {
                            Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                            fs.Write(title, 0, title.Length);
                        }


                        using (Stream s = File.Open(savefile.FileName, FileMode.CreateNew))
                        using (StreamWriter sw = new StreamWriter(s))
                            sw.Write(a);
                        newFile = false;
                        string[] previousOpenedFile = { "Siste tab: ", savefile.FileName };
                        File.WriteAllLines("settings.txt", settings);
                    }
                }
                File.WriteAllLines(settings[1], a);
                ReadFiles();
            }

        }
        /// <summary>
        /// Ved trykk på UndoBtnLabel, oppdateres texten til en tidligere lagret versjon fra listen history.
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
            if (history.Count == 1)
            {
                string[] n =
            {
                label1.Text + labelM1.Text + label3.Text,
                label4.Text + labelM2.Text + label6.Text,
                label7.Text + labelM3.Text + label9.Text,
                label10.Text + labelM4.Text + label12.Text,
                label13.Text + labelM5.Text + label15.Text,
                label16.Text + labelM6.Text + label18.Text
            };
                history = new List<string[]>();
                history.Add(n);
            }
            titleLabel.Text = history.Count.ToString();
            ButtonAnimasjon(sender);
            foreach (string[] item in history)
                Console.WriteLine(item[0]);
        }
        /// <summary>
        /// histrorySave lagrer elle endringer av texten a[] til en liste. Brukes i Undo funskjonen.
        /// </summary>
        private void historySave()
        {
            string[] n =
            {
                label1.Text + labelM1.Text + label3.Text,
                label4.Text + labelM2.Text + label6.Text,
                label7.Text + labelM3.Text + label9.Text,
                label10.Text + labelM4.Text + label12.Text,
                label13.Text + labelM5.Text + label15.Text,
                label16.Text + labelM6.Text + label18.Text
            };
            history.Add(n);
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
                        Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                        fs.Write(title, 0, title.Length);
                    }
                }
                File.WriteAllLines("settings.txt", previousOpenedFile);
                titleLabelUpdate(fileDialog.FileName);
            }
            ReadFiles();
            ButtonAnimasjon(sender);
        }
        /// <summary>
        /// newBtnLabel_Click registrerer museklikk på label med text "New".
        /// Den setter teksten i displayet til en blank fil. newFile bool settes 
        /// til true slik at SaveBtnLabel_Click vet at det er en helt ny fil som 
        /// ikke har en eksisterende filbane.
        /// </summary>
        private void newBtnLabel_Click(object sender, EventArgs e)
        {
            NyFil();
            ButtonAnimasjon(sender);
        }
        private void NyFil()
        {
            string[] temp = {
                "e |-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  |",
                "B |-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  |",
                "G |-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  |",
                "D |-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  |",
                "A |-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  |",
                "E |-  -  -  -  -  -  -  -  -  -  -  -  -  -  -  -  |"};
            a = temp;
            selectedRow = 0;
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

        /// <summary>
        /// Ved klikk på EditBtnLabel, åpnes en ny instans av MenuForm1. 
        /// En public event callbackevent i menuForm1 klassen får en ny "oppgave".
        /// Det fungerer ved at denne klassens "private void callbackEvent(object sender, Callback e)"
        /// blir trigget når en klikker på "legg til" knappen i MenuForm1.
        /// Mer informasjon om dette kommentarene i MenuForm1 
        /// </summary>
        private void EditBtnLabel_Click(object sender, EventArgs e)
        {
            MenuForm1 f = new MenuForm1();
            f.callbackEvent += new EventHandler<Callback>(callbackEvent);
            f.Show();
            ButtonAnimasjon(sender);
        }
        /// <summary>
        /// Tar imot callback fra MenuForm1 ved addering av flere linjer
        /// Dersom lengden på texten i valgt rad er mer enn 101 char, vil
        /// en ny rad med text bli oprettet.
        /// </summary>
        private void callbackEvent(object sender, Callback e)
        {
            historySave();
            //hvis siste rad er valgt
            if (selectedRow + 1== a.Length / 6)
            {
                string[] temp =
                            {
                label1.Text + labelM1.Text + label3.Text,
                label4.Text + labelM2.Text + label6.Text,
                label7.Text + labelM3.Text + label9.Text,
                label10.Text + labelM4.Text + label12.Text,
                label13.Text + labelM5.Text + label15.Text,
                label16.Text + labelM6.Text + label18.Text
                };
                int i = 0;
                foreach (string item in temp)
                {
                    temp[i] += e.AdviseText;
                    if (temp[0].Length <= 101)
                        a[i + selectedRow * 6] = temp[i];
                    i++;
                }
                string[] extended = new string[a.Length + 6];
                if (temp[0].Length > 101)
                {
                    int count = 0;
                    for (int j = 0; j < extended.Length; j++)
                    {
                        if (j < a.Length)
                            extended[j] = a[j];
                        else
                        {
                            extended[j] = a[count].Substring(0,3) + e.AdviseText;
                            count++;
                        }
                    }
                    a = extended;
                }
                LoadStrings();
            }
        }
        /// <summary>
        /// Tar en label som parameter. Her endres fargen på labelen i parameteret til blå.
        /// </summary>
        /// <param name="sender"></param>
        private void ButtonAnimasjon(object sender)
        {
            Label b = sender as Label;
            b.BackColor = Color.DeepSkyBlue;
            timerBtnAnimasjon.Start();
        }
        /// <summary>
        /// Her endres alle labeler farge tilbake til gjennomsiktig.
        /// Kilden hvor jeg fant hvordan man kan liste opp alle labeler i formen:
        /// https://stackoverflow.com/questions/24706297/making-a-foreach-loop-for-all-of-my-labels-in-c-sharp
        /// </summary>
        private void timerBtnAnimasjon_Tick(object sender, EventArgs e)
        {
            foreach (Label item in this.Controls.OfType<Label>())
            {
                item.BackColor = Color.Transparent;
            }
            timerBtnAnimasjon.Stop();
        }

        /// <summary>
        /// Ved klikk, åpnes en ny instans av Form3. her kan en endre tittelen på dokumentet.
        /// En public event TitleCallbackevent i menuForm1 klassen får en ny "oppgave".
        /// Det fungerer ved at denne klassens "private void TitleCallbackEvent(object sender, Callback e)"
        /// blir trigget når en klikker knappen i Form3.
        /// </summary>
        private void titleLabel_Click(object sender, EventArgs e)
        {
            if (!newFile)
            {
                save();
                Form3 endretittelForm = new Form3(settings[1], a);
                endretittelForm.Show();
                ButtonAnimasjon(sender);
                endretittelForm.TitleCallbackEvent += new EventHandler<Callback>(TitleCallbackEvent);
            }
            else
            {
                //åpner en Form hvor denne meldingen blir vist.
                Form4 f = new Form4("Lagre dokumentet før ny navngivelse");
                f.showMessage();
                f.Show();
            }
        }
        /// <summary>
        /// tar imot callback fra Form3
        /// Kjører funksjonen titleLabelUpdate slik at tittelen blir endret i 
        /// titlelabel samtidig som man trykker inn knappen i Form3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitleCallbackEvent(object sender, Callback e)
        {
            ReadFiles();
            titleLabelUpdate(settings[1]);
        }
        /// <summary>
        /// Åpner en ny instans av LeseForm. Her kan en lese hele dokumentet på en 
        /// oversiktlig måte i en rich textbox
        /// </summary>
        private void LesemodusLabelBtn_Click(object sender, EventArgs e)
        {
            LeseForm f = new LeseForm(a);
            f.Show();
        }
        /*
        private void label1R2_Click(object sender, EventArgs e)
        {

        }

        private void label5R2_Click(object sender, EventArgs e)
        {

        }

        private void label2R2_Click(object sender, EventArgs e)
        {

        }

        private void label4R2_Click(object sender, EventArgs e)
        {

        }

        private void label3R2_Click(object sender, EventArgs e)
        {

        }

        private void labelM4_Click(object sender, EventArgs e)
        {

        }

        private void labelM6_Click(object sender, EventArgs e)
        {

        }

        private void labelM1_Click(object sender, EventArgs e)
        {

        }

        private void label6R2_Click(object sender, EventArgs e)
        {

        }

        private void labelM2_Click(object sender, EventArgs e)
        {

        }

        private void labelM3_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void labelM5_Click(object sender, EventArgs e)
        {

        }

        private void label6R3_Click(object sender, EventArgs e)
        {

        }

        private void label1R3_Click(object sender, EventArgs e)
        {

        }

        private void label5R3_Click(object sender, EventArgs e)
        {

        }

        private void label2R3_Click(object sender, EventArgs e)
        {

        }

        private void label4R3_Click(object sender, EventArgs e)
        {

        }

        private void label3R3_Click(object sender, EventArgs e)
        {

        }*/
    }
}