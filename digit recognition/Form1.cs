using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace digit_recognition
{
    public partial class Form1 : Form
    {
        private static Neuron[,] neurons = new Neuron[20, 20];
        public static int LightValue = 650;
        private static Bitmap Original20x20SizeImage;
        public static Bitmap OriginalImage;
        public static Queue<string> TextToConsole = new Queue<string>();

        public Form1()
        {
            InitializeComponent();
            LoadLang();
            RunConsole();
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    neurons[i, j] = new Neuron();
            label3.Visible = false;
            button4.Visible = false;
            label3.Location = new Point(34, 221);
            WriteToConsole(Langs.load);
            ReadWriteWeights.CreateOrLoadWeights(neurons);
            WriteToConsole(Langs.loadSuccessful);
            WriteToConsole(Langs.startSuccessful);
        }

        public static void WriteToConsole(String text)
        {
            DateTime dt = DateTime.Now;
            if (!text.Equals(Langs.load))
                TextToConsole.Enqueue("\n\r\n\r" + "[" + dt.ToString("HH:mm:ss") + "] " + text);
            else
                TextToConsole.Enqueue("[" + dt.ToString("HH:mm:ss") + "] " + text);
        }

        private void LoadLang()
        {
            if (File.Exists("lang.txt"))
            {
                StreamReader sr = new StreamReader("lang.txt");
                string lang = sr.ReadToEnd();
                sr.Close();
                radioButton1.Checked = lang.Equals("Russian") ? true : false;
                radioButton2.Checked = lang.Equals("English") ? true : false;
                updateLanguageOnForm(lang);
            }
            else
            {
                StreamWriter sw = new StreamWriter("lang.txt");
                sw.Write("Russian");
                updateLanguageOnForm("Russian");
                sw.Close();
            }
        }

        private void updateLanguageOnForm(string s)
        {
            Langs.updateLang(s);
            Text = Langs.titleForm1;
            button1.Text = Langs.letsGo;
            button2.Text = Langs.recogDigit;
            groupBox2.Text = Langs.settingSelection;
            label2.Text = Langs.selectionText;
            button3.Text = Langs.goContinue;
            button4.Text = Langs.error;
            groupBox1.Text = Langs.textOutput;
        }

        private async void RunConsole()
        {
            while (true)
            {
                await Task.Run(() => { while (TextToConsole.Count == 0) { } });
                textBox1.AppendText(TextToConsole.Dequeue());
                textBox1.Refresh();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WriteToConsole(Langs.startWork);
            FormChoice fc = new FormChoice();
            fc.ShowDialog();
            if (OriginalImage == null)
            {
                WriteToConsole(Langs.repeat);
                return;
            }
            button2.Enabled = true;
            button2.Visible = true;
            groupBox2.Visible = false;
            label3.Visible = false;
            button4.Visible = false;
            pictureBox1.Image = OriginalImage;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            WriteToConsole(Langs.prepareImageStart);
            WriteToConsole(Langs.cropImage);
            Bitmap newImage = new Bitmap(OriginalImage);
            newImage = CropImage(newImage);
            WriteToConsole(Langs.compressionImage);
            newImage = new Bitmap(newImage, 20, 20);
            WriteToConsole(Langs.updateImage);
            pictureBox1.Image = newImage;
            WriteToConsole(Langs.scanImage);
            Original20x20SizeImage = new Bitmap(20, 20);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++) Original20x20SizeImage.SetPixel(i, j, newImage.GetPixel(i, j));
            hScrollBar1.Value = 650;
            groupBox2.Visible = true;
            button2.Visible = false;
            label3.Visible = false;
            button4.Visible = false;

            LightValue = hScrollBar1.Value;
            Bitmap b = new Bitmap(20, 20);

            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    b.SetPixel(i, j, Original20x20SizeImage.GetPixel(i, j));
                    int Red = b.GetPixel(i, j).R;
                    int Green = b.GetPixel(i, j).G;
                    int Blue = b.GetPixel(i, j).B;
                    int temp = Red + Green + Blue;
                    if (temp < LightValue) b.SetPixel(i, j, Color.Red);
                }
            pictureBox1.Image = b;
        }

        private Bitmap CropImage(Bitmap image)
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                {
                    if (image.GetPixel(x, y).Name == "ffffffff") continue;
                    else
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }

            Bitmap newImage = new Bitmap(maxX - minX + 1, maxY - minY + 1);
            for (int x = 0; x < maxX - minX + 1; x++)
                for (int y = 0; y < maxY - minY + 1; y++)
                    newImage.SetPixel(x, y, image.GetPixel(x + minX, y + minY));

            return newImage;
        }

        private void HScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            LightValue = hScrollBar1.Value;
            Bitmap b = new Bitmap(20, 20);

            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    b.SetPixel(i, j, Original20x20SizeImage.GetPixel(i, j));
                    int Red = b.GetPixel(i, j).R;
                    int Green = b.GetPixel(i, j).G;
                    int Blue = b.GetPixel(i, j).B;
                    int temp = Red + Green + Blue;
                    if (temp < LightValue) b.SetPixel(i, j, Color.Red);
                }
            pictureBox1.Image = b;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            button2.Visible = true;
            button2.Enabled = true;
            pictureBox1.Image = OriginalImage;
            Bitmap draw = new Bitmap(Original20x20SizeImage);
            LinkedList<Neuron> drawNeuronList = new LinkedList<Neuron>();
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    neurons[i, j].DropIsDraw();
                    if (neurons[i, j].Scan(draw, i, j)) drawNeuronList.AddLast(neurons[i, j]);
                }

            double[] SumWeights = new double[10];
            for (int i = 0; i < SumWeights.Length; i++) SumWeights[i] = 0;

            for (int i = 0; i < drawNeuronList.Count; i++)
            {
                Neuron temp = drawNeuronList.ElementAt(i);
                for (int j = 0; j < 10; j++) SumWeights[j] += temp.GetWeights(j);
            }
            int tempRES = -1;

            for (int i = 0; i < 10; i++) if (SumWeights.Max() == SumWeights[i]) tempRES = i;
            WriteToConsole(Langs.scanEnded);

            WriteToConsole(Langs.foundDigit + " " + tempRES + " .");
            label3.Text = tempRES.ToString();
            label3.Visible = true;
            button4.Visible = true;
            WriteToConsole("====================================");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("ErrorsImages\\")) Directory.CreateDirectory("ErrorsImages\\");
            int count = 0;
            while (File.Exists("ErrorsImages\\" + count.ToString() + ".png")) count++;
            string dir = "ErrorsImages\\" + count.ToString() + ".png";
            WriteToConsole(Langs.errorRecogSaved + " " + dir);
            OriginalImage.Save(dir);
            MessageBox.Show(Langs.errorRecogSaveInfo, Langs.saved);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                StreamWriter sw = new StreamWriter("lang.txt");
                sw.Write("Russian");
                sw.Close();
                updateLanguageOnForm("Russian");
            }
            else
            {
                StreamWriter sw = new StreamWriter("lang.txt");
                sw.Write("English");
                sw.Close();
                updateLanguageOnForm("English");
            }
        }
    }
}
