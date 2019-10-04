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
    public partial class FormChoice : Form
    {
        public FormChoice()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string temp = openFileDialog1.FileName;
            Form1.WriteToConsole("Пользователь выбирает изображение...");
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Equals(temp) && temp.Equals(""))
            {
                Form1.WriteToConsole("Изображение не выбрано! Ожидание нового выбора...");
                return;
            }
            else
            {
                if (openFileDialog1.FileName.Equals(temp))
                {
                    Form1.WriteToConsole("Выбранное изображение не было изменено!");
                    Form1.OriginalImage = new Bitmap(openFileDialog1.FileName);
                    Close();
                }
            }
            Form1.WriteToConsole("Пользователь выбрал изображение с рукописной цифрой. Ожидание нажатия кнопки распознавания...");
            Form1.OriginalImage = new Bitmap(openFileDialog1.FileName);
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FormDraw fd = new FormDraw();
            fd.ShowDialog();
            Close();
        }
    }
}
