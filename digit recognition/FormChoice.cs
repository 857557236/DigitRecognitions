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
        private void updateLanguageOnForm(string s)
        {
            Langs.updateLang(s);
            Text = Langs.titleFormChoice;
            button1.Text = Langs.imageFromComputer;
            button2.Text = Langs.drawImage;
        }

        public FormChoice()
        {
            InitializeComponent();
            updateLanguageOnForm(Langs.lang);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string temp = openFileDialog1.FileName;
            Form1.WriteToConsole(Langs.userDoChoice);
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Equals(temp) && temp.Equals(""))
            {
                Form1.WriteToConsole(Langs.imageNotChoice);
                return;
            }
            else if (openFileDialog1.FileName.Equals(temp))
            {
                Form1.WriteToConsole(Langs.imageNotChange);
                Form1.OriginalImage = new Bitmap(openFileDialog1.FileName);
                Close();
            }
            Form1.WriteToConsole(Langs.niceChoice);
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
