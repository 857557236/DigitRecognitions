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
        private static Neuron[,] neurons = new Neuron[20,20];
        public static int LightValue = 650;
        private static Bitmap Original20x20SizeImage;
        public static Bitmap OriginalImage;
        public static Queue<string> TextToConsole = new Queue<string>();

        private void WriteToConsoleEverytime()
        {
            while (TextToConsole.Count == 0)
            {

            }
        }


        public static void WriteToConsole(String text)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string hours = dt.Hour.ToString(),
                minutes = dt.Minute.ToString(),
                seconds = dt.Second.ToString();
            if (hours.Length == 1) hours = "0" + hours;
            if (minutes.Length == 1) minutes = "0" + minutes;
            if (seconds.Length == 1) seconds = "0" + seconds;
            if (!text.Equals("Загрузка весов из файла..."))
            {
                TextToConsole.Enqueue("\n\r\n\r" + ("[" + hours + ":" + minutes + ":" + seconds + "] ") + text);
            }
            else{
                TextToConsole.Enqueue(("[" + hours + ":" + minutes + ":" + seconds + "] ") + text);
            }
            
        }
        public Form1()
        {
            InitializeComponent();
            RunConsole();
            for (int i = 0; i < 20; i++)
                for(int j = 0; j<20; j++)
                    neurons[i,j] = new Neuron();
            label3.Visible = false;
            button4.Visible = false;
            label3.Location = new Point(34, 221);
            WriteToConsole("Загрузка весов из файла...");
            ReadWriteWeights.CreateOrLoadWeights(neurons);
            WriteToConsole("Загрузка прошла успешно!");
            WriteToConsole("Программа запущена успешно. Для начала выберите графический файл с рукописной цифрой на белом фоне...");
        }

        private async void RunConsole()
        {
            while (true)
            {
                await Task.Run(() => WriteToConsoleEverytime());
                textBox1.AppendText(TextToConsole.Dequeue());
                textBox1.Refresh();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WriteToConsole("Начало работы...");
            FormChoice fc = new FormChoice();
            fc.ShowDialog();
            if(OriginalImage== null)
            {
                WriteToConsole("Повторите попытку!");
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
            WriteToConsole("Начинается подготовка изображения...");
            WriteToConsole("Обрезка изображения...");
            Bitmap newImage = new Bitmap(OriginalImage);
            newImage = CropImage(newImage);
            WriteToConsole("Сжатие изображения...");
            newImage = new Bitmap(newImage, 20, 20);
            WriteToConsole("Обновление в блоке изображения слева...");
            pictureBox1.Image = newImage;
            WriteToConsole("Сканирование изображения...");
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
                    //(255 + 255 + 255)/2 = 382.5
                    if (temp < LightValue)
                    {
                        b.SetPixel(i, j, Color.Red);
                    }
                }
            pictureBox1.Image = b;
        }

        private Bitmap CropImage(Bitmap image)
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            for (int x = 0; x<image.Width; x++)
            {
                for(int y = 0; y<image.Height; y++)
                {
                    if(image.GetPixel(x, y).Name == "ffffffff")
                    {
                        continue;
                    }
                    else
                    {
                        if (x < minX) minX = x;
                        if (x > maxX) maxX = x;
                        if (y < minY) minY = y;
                        if (y > maxY) maxY = y;
                    }
                }
            }

            Bitmap newImage = new Bitmap(maxX - minX + 1, maxY - minY + 1);
            for (int x = 0; x < maxX - minX + 1; x++)
            {
                for (int y = 0; y < maxY - minY + 1; y++)
                {
                    newImage.SetPixel(x, y, image.GetPixel(x + minX, y + minY));
                }
            }

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
                    //(255 + 255 + 255)/2 = 382.5
                    if (temp < LightValue)
                    {
                        b.SetPixel(i, j, Color.Red);
                    }
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
            for(int i = 0; i<20;i++)
                for(int j = 0; j < 20; j++)
                {
                    neurons[i, j].DropIsDraw();
                    if (neurons[i, j].Scan(draw, i, j)) drawNeuronList.AddLast(neurons[i,j]);
                }

            double[] SumWeights = new double[10];
            for (int i = 0; i < SumWeights.Length; i++) SumWeights[i] = 0;

            for(int i = 0;i < drawNeuronList.Count; i++)
            {
                Neuron temp = drawNeuronList.ElementAt(i);
                for (int j = 0; j < 10; j++) SumWeights[j] += temp.GetWeights(j);
            }
            int tempRES = -1;

            for (int i = 0; i < 10; i++) if (SumWeights.Max() == SumWeights[i]) tempRES = i;
            WriteToConsole("Сканирование завершено!");

            if(tempRES == -1)
            {
                WriteToConsole("Цифра не была опознана!");
                label3.Text = "?";
                label3.Visible = true;
                button4.Visible = true;
                WriteToConsole("====================================");
                return;
            }
                WriteToConsole("Опознана цифра " + tempRES + " .");
                label3.Text = tempRES.ToString();
                label3.Visible = true;
            button4.Visible = true;
            WriteToConsole("====================================");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("ErrorsImages\\")) Directory.CreateDirectory("ErrorsImages\\");
            int count = 0;
            while(File.Exists("ErrorsImages\\" + count.ToString() + ".png"))
            {
                count++;
            }
            string dir = "ErrorsImages\\" + count.ToString() + ".png";
            WriteToConsole("Ошибочное распознавание сохранено: " + dir);
            OriginalImage.Save(dir);
            MessageBox.Show("Оригинальное изображение было сохранено в папке ErrorsImages, находящейся рядом с запускаемым файлом программы. Его можно использовать для более точного обучения сети, чтобы таких ошибок более не возникало!","Сохранено");
        }
    }
}
