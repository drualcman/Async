using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Primitives.ReadWriteLockSlinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseReaderWriterLockSlimDemo();
        }

        static int CurrentInvoiceNumber = 0;

        /// <summary>
        /// Incrementar el valor de una variable solo desde un hilo a la vez
        /// </summary>
        static void UseReaderWriterLockSlimDemo() 
        {
            ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
            
            ConcurrentQueue<int> ordersQueue = new ConcurrentQueue<int>(Enumerable.Range(1, 10));

            CountdownEvent countdownEvent = new CountdownEvent(ordersQueue.Count);
            
            Action processOrders = () =>
            {
                int order;
                int processedOrders = 0;

                while (ordersQueue.TryDequeue(out order))
                {
                    Console.WriteLine($"Hilo: {Thread.CurrentThread.ManagedThreadId}, pedido {order}");
                    
                    readerWriterLockSlim.EnterReadLock();
                    int NewInvoiceNumber = CurrentInvoiceNumber + 1;                    
                    Console.WriteLine($"Asignando factura {NewInvoiceNumber} a pedido {order}");
                    Thread.Sleep(500);
                    readerWriterLockSlim.ExitReadLock();

                    readerWriterLockSlim.EnterWriteLock();
                    CurrentInvoiceNumber++;
                    Thread.Sleep(500);
                    Console.WriteLine($"Factura {CurrentInvoiceNumber} asignada a pedido {order}");
                    readerWriterLockSlim.ExitWriteLock();


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
            readerWriterLockSlim.Dispose();

            Console.WriteLine($"Todos los pedidos han sido procesados. Total {CurrentInvoiceNumber}");
        }
    }
}
