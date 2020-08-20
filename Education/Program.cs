using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("numbersImage.txt"))
            {
                Console.WriteLine("Создайте файл \"numbersImage.txt\" для обучения со следующими строками:");
                Console.WriteLine("<цифра>:<название файла>");
                Console.WriteLine("И повторите попытку. Используйте латиницу!");
                Console.ReadKey();
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Эта программа была сделана на скорую руку, в ней почти отсутствуют различные проверки введённых данных и т.д. Идеальная работоспособность не гарантируется!\n");
            Console.ResetColor();
            Console.WriteLine("1) Использовать готовые веса");
            Console.WriteLine("2) Создать новые, чистые");
            Console.Write("Ваш выбор: ");
            int choice = -1;
            choice = Convert.ToInt32(Console.ReadLine());
            string weightsFileName = "";
            if(choice == 1)
            {
                do
                {
                    Console.Write("Введите название файла весов: ");
                    weightsFileName = Console.ReadLine();
                    if (!File.Exists(weightsFileName)) Console.WriteLine("Не найден!");
                } while (!File.Exists(weightsFileName));
            }
            else
            {
                StreamWriter sw = new StreamWriter("weights");
                for (int i = 0; i < 4000; i++) sw.WriteLine("1");
                sw.Close();
                weightsFileName = "weights";
            }
            WorkCode.LoadWeights();
            int Records = 0;
            StreamReader sr = new StreamReader("numbersImage.txt");
            while (sr.ReadLine() != null) Records++;
            sr.Close();
            Checker[] checkers = new Checker[Records];
            sr = new StreamReader("numbersImage.txt");
            string temp = "";
            int count = 0;
            while ((temp = sr.ReadLine()) != null)
            {
                checkers[count] = new Checker();
                checkers[count].num = int.Parse(temp[0].ToString());
                string filename = "";
                for (int i = 2; i < temp.Length; i++) filename += temp[i];
                Bitmap bm = new Bitmap(filename);
                checkers[count].image = bm;
                count++;
            }
            //bool Check(Bitmap, Result)
            int size = checkers.Length;
            bool ISCOOLGLOBAL = true;
            int GLOBALCOUNT = 1;
            if (File.Exists("LOG.txt"))
            {
                File.Delete("LOG.txt");
            }
            
            StreamWriter swLOG = new StreamWriter("LOG.txt");
            do
            {
                Console.WriteLine("........Новая полная итерация.........(" + GLOBALCOUNT.ToString() + ")");
                swLOG.WriteLine(GetDateTime());
                swLOG.WriteLine("........Новая полная итерация.........(" + GLOBALCOUNT.ToString() + ")");
                ISCOOLGLOBAL = true;
                for (int i = 0; i < size; i++)
                {
                repeat:
                    WorkCode.LoadWeights();
                    bool IsCool = WorkCode.Check(checkers[i].image, checkers[i].num);
                    if (IsCool == false) ISCOOLGLOBAL = false;
                    checkers[i].IsGood = IsCool;
                    if (checkers[i].IsGood == false)
                    {
                        Console.WriteLine("Произведено обучение, повтор (цифра " + checkers[i].num.ToString() + " не распозналась)");
                        swLOG.WriteLine("Произведено обучение, повтор (цифра " + checkers[i].num.ToString() + " не распозналась)");
                        goto repeat;
                    }
                    Console.WriteLine("Всё хорошо. Цифра " + checkers[i].num.ToString() + " распозналась!");
                    swLOG.WriteLine("Всё хорошо. Цифра " + checkers[i].num.ToString() + " распозналась!");
                }
                GLOBALCOUNT++;
            } while (!ISCOOLGLOBAL);
            Console.WriteLine("Обучение завершено.");
            swLOG.WriteLine("Обучение завершено.");
            swLOG.WriteLine(GetDateTime());
            swLOG.Close();
            Console.ReadKey();
        }

        private static string GetDateTime()
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            string NowTime = "";
            if (dt.Day.ToString().Length == 1) NowTime += "0";
            NowTime += dt.Day.ToString();
            NowTime += ".";
            if (dt.Month.ToString().Length == 1) NowTime += "0";
            NowTime += dt.Month.ToString();
            NowTime += ".";
            NowTime += dt.Year.ToString();
            NowTime += "     ";
            if (dt.Hour.ToString().Length == 1) NowTime += "0";
            NowTime += dt.Hour.ToString();
            NowTime += ":";
            if (dt.Minute.ToString().Length == 1) NowTime += "0";
            NowTime += dt.Minute.ToString();
            NowTime += ":";
            if (dt.Second.ToString().Length == 1) NowTime += "0";
            NowTime += dt.Second.ToString();
            return NowTime;
        }
    }
}
