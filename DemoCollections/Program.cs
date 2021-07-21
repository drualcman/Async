using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DemoCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Parallel.Invoke(Enqueue, Denqueue, Enqueue, Denqueue);
            Console.WriteLine($"{queue.Count} elementos");
            foreach (int item in queue)
            {
                Console.WriteLine(item);
            }
        }

        static ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

        static void Enqueue()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(random.Next(5000));
            }
        }


        static void Denqueue()
        {
            int fuera = 0;
            for (int i = 0; i < 10; i++)
            {
                queue.TryDequeue(out fuera);
                Console.WriteLine($"saco {fuera}");
            }
        }
    }
}
