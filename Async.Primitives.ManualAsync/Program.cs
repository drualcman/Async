using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Primitives.ManualResetEventSlimDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DoManualResetEventSlim();
        }

        static void DoManualResetEventSlim()
        {
            // No señalizado (NonSignaled)
            ManualResetEventSlim M1 = new ManualResetEventSlim();

            // No señalizado (NonSignaled) explicito
            ManualResetEventSlim M2 = new ManualResetEventSlim(false);

            // Señalizado (Signaled)
            ManualResetEventSlim M3 = new ManualResetEventSlim(true);

            Task T1 = Task.Run(() =>
            {
                M1.Wait();
                Console.WriteLine("M1 esta en estado señalizado.");
                Console.WriteLine("Estable estado NonSignaled al objeto M3....");
                M3.Reset();         //reset = set NonSignaled
                
                Console.WriteLine("Estable estado Signaled al objeto M2....");
                M2.Set();           //set = set Signaled
            });

            Console.WriteLine("En el hilo principal");
            Console.WriteLine($"Estado de M3 es Signaled? {M3.IsSet}");
            Console.WriteLine("El hilo principal señalizara a M1");
            M1.Set();
            
            Console.WriteLine("El hilo principal espera a que M2 sea señalizado");
            M2.Wait();
            Console.WriteLine("En el hilo principal M2 ha sido señalizado");
            Console.WriteLine($"Estado de M3 es Signaled? {M3.IsSet}");

            T1.Wait();
            // es buena practica siempre hacer el dispose the los objectos ManualResetEventSlim
            M1.Dispose();
            M2.Dispose();
            M3.Dispose();
        }
    }
}
