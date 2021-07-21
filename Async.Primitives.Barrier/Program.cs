using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Primitives.BarrierDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseBarrier();
        }

        /// <summary>
        /// Hacer muchas tareas y esperar a que otras tareas esperan hasta que se hayan finalizado las anteriores
        /// </summary>
        static void UseBarrier()
        {
            int counter = 0;
            Barrier barrier = new Barrier(4, (barrierObject) => 
            {
                //se ejecuta cada vezs que termine una fase
                Console.WriteLine("Fase {0} finalizada. {1} notificaciones.", barrierObject.CurrentPhaseNumber, counter);
            });

            Action install = () =>
            {
                // fase 1
                Interlocked.Increment(ref counter);             //incrementa el valor de una variable de froma segura
                Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} Fase {barrier.CurrentPhaseNumber}");
                barrier.SignalAndWait();            //se espera hasta tener 4 SignalAndWait()
                // fase 2
                Interlocked.Increment(ref counter);             //incrementa el valor de una variable de froma segura
                Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} Fase {barrier.CurrentPhaseNumber}");
                barrier.SignalAndWait();            //se espera hasta tener 4 SignalAndWait()
                // fase 3
                Interlocked.Increment(ref counter);             //incrementa el valor de una variable de froma segura
                Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} Fase {barrier.CurrentPhaseNumber}");
                barrier.SignalAndWait();            //se espera hasta tener 4 SignalAndWait()
                // fase 4
                Interlocked.Increment(ref counter);             //incrementa el valor de una variable de froma segura
                Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId} Fase {barrier.CurrentPhaseNumber}");
                barrier.SignalAndWait();            //se espera hasta tener 4 SignalAndWait()
            };

            Parallel.Invoke(install, install, install, install);
            Console.WriteLine("Todas las tareas finalizadas.");
            barrier.Dispose();
        }
    }
}
