// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World github!");

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static int N = 5000;
        static double[,] a, a0;
        static bool IsEqual(double[,] a, double[,] b) 
        { 
            int N = a.GetLength(0); 
            for (int i = 0; i < N; i++) 
                for (int j = 0; j < N; j++) 
                    if (a[i, j] != b[i, j]) 
                        return false; 
            return true; 
        }

        static void FillAll()
        {
            for (int i = 0; i < N; i++)
            {
                for(int j = 0; j < N; j++)
                {
                    a[i,j] = Math.Pow(i+1, 1.768*(j+2));
                }
            }
        }
        static void FillLines_(object o)
        {
            int[] ii = (int[])(o);
            for (int i = ii[0]; i < ii[1]; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    a[i, j] = Math.Pow(i + 1, 1.768 * (j + 2));
                }
            }
        }

        static void Main()
        {
            a = new double[N, N];
            a0 = new double[N, N];
            DateTime t0;
            TimeSpan dt;
            for (int s = 0; s < 3; s++)
            {
                t0 = DateTime.Now;
                FillAll();
                dt = DateTime.Now - t0;
                Console.Write("t(посл)={0,-8:f3}    ", dt.TotalMilliseconds / 1000);

                Array.Copy(a, a0, N * N); Array.Clear(a, 0, N * N);


                t0 = DateTime.Now;
                Thread th1 = new Thread(FillLines_);
                Thread th2 = new Thread(FillLines_);
                th1.Start(new int[2] { 0,N/2});
                th2.Start(new int[2] { N/2, N});
                th1.Join();
                th2.Join();
                dt = DateTime.Now - t0;
                Console.Write("t(4)={0,-8:f3} {1}", dt.TotalMilliseconds / 1000, IsEqual(a, a0));

                Console.WriteLine();
            }
        }


    }
}
