using System;
using System.Threading;
using System.Threading.Tasks;

namespace Answers
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.GetMaxThreads(out int worker, out int port);
            Console.WriteLine($"Numero maximo de threads: {worker}");
            Console.WriteLine($"Numero maximo de threads para operasiones asincronas de entrada y salida: {port}");

            ThreadPool.GetAvailableThreads(out worker, out port);
            Console.WriteLine($"Numero maximo de threads disponibles: {worker}");
            Console.WriteLine($"Numero maximo de threads disponibles para operasiones asincronas de entrada y salida: {port}");

            Task k = new Task(() => Console.WriteLine("Hola Mundo!"));
            k.Start();

            ThreadPool.GetAvailableThreads(out worker, out port);
            Console.WriteLine($"Numero maximo de threads disponibles: {worker}");

            Console.WriteLine("Presiona <enter> para finalizar ...");
            Console.ReadLine();
        }
    }
}
