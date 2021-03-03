using System;

namespace sb_task_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 50;
            int k = (int) Math.Log2(N);
            Console.WriteLine($"Считано число N = {N}");
            Console.WriteLine($"Количество групп: {k+1}");
            for (int j = 1; j <= k; j++)
            {
                int nextK = (int) Math.Pow(2, j);
                int lim = nextK * 2;
                for (int i = nextK; i < lim && i <= N; i++)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine();
            }

        }
    }
}


