using NorthWind;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Paralel
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunParallelTask();
            ParallelLoopIterate();
            //RunLINQ();
            //RunPLINQ();
            //RunContinuationTasks();
            //RunNestedTasks();
            //RunNestedTasksWithChild();
            Console.WriteLine("Presione <enter> para finalizar");
            Console.Read();
            //Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");

        }

        #region ejecutando tareas en paralelo
        static void RunParallelTask()
        {
            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Ejecutar tareas en paralelo");
            Parallel.Invoke(
                () => WriteToConsole("1"),
                () => WriteToConsole("2"),
                () => WriteToConsole("3"),
                () => WriteToConsole("4"),
                () => WriteToConsole("5")
                );
        }

        static void WriteToConsole(string message)
        {
            Console.WriteLine($"{message}. Thread: {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(1000);
            Console.WriteLine($"Fin de la taread. Thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        static void ParallelLoopIterate()
        {
            int[] SquareNumbers = new int[5];

            Parallel.For(0, 5, i =>
             {
                 SquareNumbers[i] = i * i;
                 Console.WriteLine($"Calculando el cuadrado de {i}");
             });

            Parallel.ForEach(SquareNumbers, n => 
            {
                Console.WriteLine($"Cuadrado de {Array.IndexOf(SquareNumbers,n)} {n}");
            });
        }

        static void RunLINQ()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            List<ProductDTO> DTOProducts = Repository.Products.Select(p => new ProductDTO
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            }).ToList();
            s.Stop();
            Console.WriteLine($"Tiempo de eejecucion con LINQ: {s.ElapsedTicks} ticks.");
        }

        static void RunPLINQ()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            List<ProductDTO> DTOProducts = Repository.Products.AsParallel().Select(p => new ProductDTO
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            }).ToList();
            s.Stop();
            Console.WriteLine($"Tiempo de eejecucion con PLINQ: {s.ElapsedTicks} ticks.");
        }
        #endregion

        #region enlazando tareas
        static void RunContinuationTasks()
        {
            //Tarea Antecedente
            Task<List<string>> firstTask = new Task<List<string>>(GetProductNames);
            //Tarea de continuacion
            Task<int> secondTask = firstTask.ContinueWith(antecedentTask =>
            {
                return ProcessData(antecedentTask.Result);
            });

            firstTask.Start();
            Console.WriteLine($"Numero de productos procesados: {secondTask.Result}");
        }

        static int ProcessData(List<string> productNames)
        {
            foreach (string productName in productNames)
            {
                Console.WriteLine($"Nombre del producto {productName}");
            }
            return productNames.Count;
        }

        static List<string> GetProductNames()
        {
            Thread.Sleep(3000);
            return Repository.Products.Select(p => p.ProductName).ToList();
        }

        static void RunNestedTasks()
        {
            Task outerTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Iniciando la tarea externa ...");
                Task innerTask = Task.Factory.StartNew(()=> 
                {
                    Console.WriteLine("Iniciando la tarea interna anidada ...");
                    Thread.Sleep(3000);
                    Console.WriteLine("Finalizadndo la tarea interna anidada ...");
                });
            });
            outerTask.Wait();
            Console.WriteLine("Tarea externa finalizada ...");
        }

        static void RunNestedTasksWithChild()
        {
            Task outerTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Iniciando la tarea externa ...");
                Task innerTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Iniciando la tarea interna anidada ...");
                    Thread.Sleep(3000);
                    Console.WriteLine("Finalizadndo la tarea interna anidada ...");
                }, TaskCreationOptions.AttachedToParent);
            });
            outerTask.Wait();
            Console.WriteLine("Tarea externa finalizada ...");
        }
        #endregion
    }
}
