using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education
{
    public class WorkCode
    {
        private static Neuron[,] neurons = new Neuron[20, 20];
        public static void LoadWeights()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    neurons[i, j] = new Neuron();
            ReadWriteWeights.CreateOrLoadWeights(neurons);
        }
        public static bool Check(Bitmap bm, int Result)
        {
            Bitmap bTEST = new Bitmap(bm);
            bTEST = CropImage(bTEST);
            bTEST = new Bitmap(bTEST, 20, 20);
            Bitmap testBitMap = new Bitmap(20, 20);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++) testBitMap.SetPixel(i, j, bTEST.GetPixel(i, j));
            bTEST = testBitMap;


            LinkedList<Neuron> drawNeuronList = new LinkedList<Neuron>();
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    if (neurons[i, j].Scan(bTEST, i, j)) drawNeuronList.AddLast(neurons[i, j]);
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
            //////////////////обучение
            ///
            if((tempRES != Result) && (tempRES != -1))
            {
                for(int i = 0; i<drawNeuronList.Count; i++)
                {
                    double[] mas = drawNeuronList.ElementAt(i).GetWeights();
                    mas[Result] = mas[Result] + 2;
                    mas[tempRES] = mas[tempRES] - 1;
                    drawNeuronList.ElementAt(i).SetWeights(mas);
                }
                ReadWriteWeights.SaveWeights(neurons);
                return false;
            }



            ///
            if (tempRES == -1)
            {
                return false;
            }
            if (tempRES != Result) return false;
            return true;
        }

        private static Bitmap CropImage(Bitmap image)
        {
            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (image.GetPixel(x, y).Name == "ffffffff")
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
    }
}
