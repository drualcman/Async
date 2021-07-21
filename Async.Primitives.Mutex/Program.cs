using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Primitives.MutexDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseMutex();
        }

        static int CurrentInvoiceNumber = 0;
        static ConcurrentQueue<int> ordersQueue = new ConcurrentQueue<int>(Enumerable.Range(1, 10));
        static Mutex mutex = new Mutex();

        /// <summary>
        /// Obtener uso exclusivo a un recurso o variable
        /// </summary>
        static void UseMutex()
        {
            Parallel.Invoke(ProcessOrder, ProcessOrder, ProcessOrder);
            Console.WriteLine("Todos los pedidos han sido procesados.");
        }

        static void ProcessOrder()
        {
            int order;
            int processedOrders = 0;

            while (ordersQueue.TryDequeue(out order))
            {
                Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} esta solicitando el mutex.");
                mutex.WaitOne();
                CurrentInvoiceNumber++;
                Thread.Sleep(500);
                Console.WriteLine($"Factura {CurrentInvoiceNumber} asignada a pedido {order}");
                mutex.ReleaseMutex();
                Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} ha liberado el mutex.");
                processedOrders++;
            }
            Console.WriteLine($"El hilo: {Thread.CurrentThread.ManagedThreadId}, pedidos procesados: {processedOrders}");

        }
    }
}
