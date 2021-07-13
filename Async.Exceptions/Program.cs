using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            HandleTaskException();
            Console.WriteLine("Presiona <ENTER> para finalizar");
            Console.ReadLine();
        }

        static void RunLongTask(CancellationToken token)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(2000);
                token.ThrowIfCancellationRequested();
            }
        }

        static void HandleTaskException()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;
            Task longRunningTask = Task.Run(()=> RunLongTask(ct), ct);
            cts.Cancel();
            try
            {
                longRunningTask.Wait();
            }
            catch (AggregateException ext)
            {
                foreach (var item in ext.InnerExceptions)
                {
                    if (item is TaskCanceledException)
                    {
                        Console.WriteLine("La tarea fue cancelada");
                    }
                    else
                    {
                        Console.WriteLine(item.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
