using System;
using System.Threading.Tasks;

namespace Decepcion
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            Console.WriteLine("Comienza la locura de Sergi");
            int vueltas = 0;
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                Console.Write(".");
                vueltas++;
                if (vueltas / 2 == 1)
                {
                    Console.Write("A");
                    Otro("B");
                }
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.ElapsedTicks);
            Console.WriteLine($"Finalizado con valor {vueltas}");
            Console.ReadLine();

            Console.Clear();
            vueltas = 0;
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                Task.Run(()=> 
                {
                    Console.Write(".");
                    vueltas++;
                    if (vueltas / 2 == 1)
                    {
                        Console.Write("A");
                        Task.Run(()=> { Otro("B"); });
                    }
                });
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.ElapsedTicks);
            Console.WriteLine($"Finalizado con valor {vueltas}");
            Console.ReadLine();
            
            Console.Clear();
            vueltas = 0;
            stopwatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                    Console.Write(".");
                    vueltas++;
                    if (vueltas % 2 == 1)
                    {
                        Console.Write("E");
                        Task.Run(() => { Otro("F"); });
                    }
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.ElapsedTicks);
            Console.WriteLine($"Finalizado con valor {vueltas}");

            Console.ReadLine();
            Console.Clear();
            vueltas = 0;
            stopwatch.Start();
            Parallel.For(0, 1000, i =>
            {
                Task.Run(()=> 
                {
                    Console.Write(".");
                    vueltas++;
                    if (vueltas / 2 == 1)
                    {
                        Console.Write("A");
                        Task.Run(() => { Otro("B"); });
                    }
                });
            });
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.ElapsedTicks);
            Console.WriteLine($"Finalizado con valor {vueltas}");

            Console.ReadLine();
            Console.Clear();
            vueltas = 0;
            stopwatch.Start();
            Parallel.For(0, 1000, i=> 
            {
                Console.Write(".");
                vueltas++;
                if (vueltas % 2 == 1)
                {
                    Console.Write("I");
                    Task.Run(() => { Otro("J"); });
                }
            });
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(stopwatch.ElapsedTicks);
            Console.WriteLine($"Finalizado con valor {vueltas}");
        }

        static void Otro(string letra)
        {
            Console.Write(letra);
        }
    }
}
