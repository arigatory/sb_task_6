using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace sb_task_6
{
    class Program
    {
        static int N = 50;
        static int k;
        static string path = @"TextFile1.txt";
        static string fileName = "groups";

        static void Main(string[] args)
        {
            N = GetNumber();

            //number of groups
            k = (int)Math.Log2(N);

            Console.WriteLine($"Считано число N = {N}");

            PrintHelp();

            //main loop
            while (true)
            {
                string input = Console.ReadLine().Trim();

                if (input == "1")
                {
                    ShowNumberOfGroups();
                }
                else if (input == "2")
                {
                    WriteToFile();
                    Console.WriteLine("Архивировать файл? \n1 - да \n0 - нет");
                    bool archive = Console.ReadLine().Trim() == "1";
                    if (archive)
                    {
                        ZipFile();
                    }
                }
                else if (input == "0")
                {
                    return;
                } else
                {
                    PrintHelp();
                }
            }
        }


        private static void ShowNumberOfGroups()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            Console.WriteLine($"Количество групп: {k + 1}");
            stopwatch.Stop();
            long time = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Затрачено времени { time / 1000: 0.000} c");
        }


        private static void ZipFile()
        {
            string compressed = $"{fileName}.txt.zip";
            using (FileStream ss = new FileStream($"{fileName}.txt", FileMode.OpenOrCreate))
            {
                using (FileStream ts = File.Create(compressed))
                {
                    using (GZipStream cs = new GZipStream(ts, CompressionMode.Compress))
                    {
                        ss.CopyTo(cs);
                        Console.WriteLine("Сжатие файла {0} завершено. Было: {1}  стало: {2}.",
                                          fileName,
                                          ss.Length,
                                          ts.Length);
                    }
                }
            }
        }


        private static void WriteToFile()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            using (StreamWriter sw = new StreamWriter($"{fileName}.txt"))
            {
                sw.WriteLine("Группа 1: 1");
                for (int j = 1; j <= k; j++)
                {
                    int nextK = (int)Math.Pow(2, j);
                    int lim = nextK * 2;
                    sw.Write($"Группа {j+1}: ");
                    for (int i = nextK; i < lim && i <= N; i++)
                    {
                        sw.Write(i + " ");
                        //Console.WriteLine($"{(double)i/N*100.0: 0.000} %");
                        //drawTextProgressBar(i, N);
                    }
                    sw.WriteLine();
                }
            }
            stopwatch.Stop();
            long time = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Затрачено времени { time / 1000 : 0.000} c");
        }



        /// <summary>
        /// All available commands
        /// </summary>
        private static void PrintHelp()
        {

            Console.WriteLine("1: Показать количество групп");
            Console.WriteLine("2: Записать группы в файл");
            Console.WriteLine("0: Выйти");
        }


        /// <summary>
        /// Read number from the file
        /// </summary>
        /// <returns></returns>
        static int GetNumber()
        {
            string text = File.ReadAllText(path);
            return int.Parse(text);
        }

        private static void drawTextProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}


