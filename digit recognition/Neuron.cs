using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digit_recognition
{
    public class Neuron
    {
        private double[] Weights = new double[10];
        private bool IsDraw = false;

        public Neuron()
        {
            for (int i = 0; i < 10; i++) Weights[i] = 1;
        }

        public double[] GetWeights()
        {
            return Weights;
        }

        public double GetWeights(int num)
        {
            return Weights[num];
        }

        public void SetWeights(double[] mas)
        {
            Weights = mas;
        }

        public bool GetIsDraw()
        {
            return IsDraw;
        }

        public bool Scan(Bitmap draw, int x, int y)
        {
            int Red = draw.GetPixel(x, y).R;
            int Green = draw.GetPixel(x, y).G;
            int Blue = draw.GetPixel(x, y).B;
            int temp = Red + Green + Blue;
            if (temp < Form1.LightValue)
            {
                IsDraw = true;
            }
            return IsDraw;
        }

        public void DropIsDraw()
        {
            IsDraw = false;
        }
    }
}
