using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Primitives.CountdownEventDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseCountdownEvent();
            Console.ReadLine();
        }
        /// <summary>
        /// Liberar el hilo cuando todas las tareas and marcado al contador como hecho. El contador llegara a cero.
        /// </summary>
        static void UseCountdownEvent()
        {
            ConcurrentQueue<int> ordersQueue = new ConcurrentQueue<int>(Enumerable.Range(1, 1000));
            
            CountdownEvent countdownEvent = new CountdownEvent(ordersQueue.Count);
            int counter = 0;
            Action processOrders = () =>
            {
                int order;
                int processedOrders = 0;

                while (ordersQueue.TryDequeue(out order))
                {
                    counter++;
                    Console.WriteLine($"Hilo: {Thread.CurrentThread.ManagedThreadId}, pedido {order}");
                    Thread.Sleep(10);
                    processedOrders++;
                    countdownEvent.Signal();
                }
                Console.WriteLine($"Hilo: {Thread.CurrentThread.ManagedThreadId}, pedidos procesados: {processedOrders}");
            };

            Task.Run(processOrders);
            Task.Run(processOrders);
            Task.Run(processOrders);

            countdownEvent.Wait();
            countdownEvent.Dispose();

            Console.WriteLine($"Todos los pedidos han sido procesados. Total {counter}");
        }
    }
}
