using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digit_recognition
{
    public class ReadWriteWeights
    {
        public static void CreateOrLoadWeights(Neuron[,] neurons)
        {
            if (File.Exists("weights"))
            {
                StreamReader sr = new StreamReader("weights");
                for(int i = 0; i<20;i++)
                    for(int j = 0; j<20; j++)
                    {
                        double[] temp = new double[10];
                        for (int g = 0; g < 10; g++)
                        {
                            temp[g] = Convert.ToDouble(sr.ReadLine());
                        }
                        neurons[i, j].SetWeights(temp);
                    }
                sr.Close();
                        
            }
            else
            {
                StreamWriter sw = new StreamWriter("weights");
                for (int i = 0; i < 4000; i++) sw.WriteLine("1");
                sw.Close();
                for(int i = 0; i<20; i++)
                    for(int j = 0; j<20; j++)
                    {
                        double[] temp = new double[10];
                        for (int g = 0; g < 10; g++)
                        {
                            temp[g] = 1;
                        }
                        neurons[i, j].SetWeights(temp);
                    }
            }
        }

        public static void SaveWeights(Neuron[,] neurons)
        {
            StreamWriter sw = new StreamWriter("weights");
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    for (int g = 0; g < 10; g++)
                    {
                        sw.WriteLine(neurons[i, j].GetWeights(g));
                    }
                }
            sw.Close();
        }
    }
}
