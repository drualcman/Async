using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Primitives.SemaphoreSlimDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            UseSemaphoreSlim();
        }

        static void UseSemaphoreSlim()
        {
            SemaphoreSlim S = new SemaphoreSlim(2);

            bool entered = S.Wait(1000);

            Console.WriteLine($"El hilo no tuvo que esperar 1000 milisegundos: {entered}  {S.CurrentCount}");

            entered = S.Wait(1000);
            Console.WriteLine($"Segundo acceso exitoso: {entered}  {S.CurrentCount}");

            Console.WriteLine($"Aqui ya no hay accesos y el hilo tendra que esperar...");

            entered = S.Wait(1000);
            Console.WriteLine($"Tercer acceso exitoso: {entered}  {S.CurrentCount}");
            
            S.Release();            // incrementa el contador en 1 cada vez que es invocado
            entered = S.Wait(1000);
            Console.WriteLine($"Despues del release el acceso exitoso: {entered} {S.CurrentCount}");

            Task T = Task.Run(() =>
            {
                Thread.Sleep(100);
                Console.WriteLine("Tarea liberando el semaforo");
                S.Release();
            });

            T.Wait();
            Console.WriteLine($"Accesos concurrentes disponibles: {S.CurrentCount}");
            S.Dispose();
        }
    }

    class kk : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~kk()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
