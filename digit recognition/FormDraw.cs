using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace digit_recognition
{
    public partial class FormDraw : Form
    {
        private bool IsPressed = false;
        private static Bitmap b;
        private static Graphics g;
        private static Pen p;

        public FormDraw()
        {
            InitializeComponent();
            updateLanguageOnForm(Langs.lang);
            Clean();
        }

        private void updateLanguageOnForm(string s)
        {
            Langs.updateLang(s);
            Text = Langs.titleFormDraw;
            button1.Text = Langs.recog;
            button2.Text = Langs.clean;
        }

        private void Clean()
        {
            Bitmap b = new Bitmap(500, 500);
            for (int i = 0; i < 500; i++)
                for (int j = 0; j < 500; j++)
                    b.SetPixel(i, j, Color.White);
            pictureBox1.Image = b;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1.OriginalImage = new Bitmap(pictureBox1.Image);
            Form1.WriteToConsole(Langs.goodDraw);
            Close();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsPressed = true;
            b = new Bitmap(pictureBox1.Image);
            g = Graphics.FromImage(b);
            p = new Pen(Color.Black, 10);
        }
        
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsPressed = false;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPressed)
            {
                g.DrawEllipse(p, new RectangleF(e.X, e.Y, 10, 10));
                pictureBox1.Image = b;
            }
        }
    }
}
