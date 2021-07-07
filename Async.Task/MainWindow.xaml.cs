using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async.Tasks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CreateTask();
            //RunTaskGroup();
            ReturnTaskValue();
        }

        #region Task

        void CreateTask()
        {
            Task T1;
            Action code = new Action(ShowMessage);
            T1 = new Task(code);

            Task T2 = new Task(delegate
            {
                MessageBox.Show("Ejecutando una tarea en un metodo anonimo");
            });

            // lambda expresion para hacer esto Task T3 = new Task(delegate { ShowMessage(); });
            // operador => es como va hacia
            // () => expresion
            // (parametros) => expresion
            Task T3 = new Task(() => ShowMessage());
            Task T4 = new Task(() => MessageBox.Show("Ejecutando la tarea 4"));
            // lambda con muchas lineas de codigo como si fuera un metodo
            Task T5 = new Task(() =>
            {
                DateTime currentDate = DateTime.Today;
                DateTime startDate = currentDate.AddDays(30);
                MessageBox.Show($"Tarea 5. Fecha calcula: {startDate}");
            });

            Task T6 = new Task((message) => MessageBox.Show(message.ToString()), "Expresion Lambda con parametros");

            Task T7 = new Task(() => AddMessage("Ejecutando la tarea"));
            T7.Start();
            AddMessage("En el hilo principal");

            var T8 = Task.Factory.StartNew(()=> AddMessage("Tarea inciada con TaskFactory"));
            var T9 = Task.Run(()=> AddMessage("Tarea inciada con Task.Run"));

            var T10 = Task.Run(() => 
            {
                WriteToOutput("Iniciando tarea 10...");
                Thread.Sleep(1000);
                WriteToOutput("Finalizando tarea 10...");
            });

            WriteToOutput("Esperando a la tarea 10");
            T10.Wait();
            WriteToOutput("La tarea 10 finalizo su ejecucion");
        }

        void WriteToOutput(string message)
        {
            Debug.WriteLine(
                $"Mensaje: {message}, " +
                $"Hilo actual: {Thread.CurrentThread.ManagedThreadId}");

        }

        void ShowMessage()
        {
            MessageBox.Show("Ejecutando el metodo ShowMessage");
        }

        void AddMessage(string message)
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            this.Dispatcher.Invoke(() =>
            {
                Messages.Content +=
                    $"Mensaje: {message}. " +
                    $"Hilo actual: {currentThreadId}. " +
                    $"Hilo invocado: { Thread.CurrentThread.ManagedThreadId}. " +
                    $"Procesadores: {Environment.ProcessorCount}. " +
                    $"Dispongo de {System.Diagnostics.Process.GetCurrentProcess().Threads.Count} hilos\n";
            });
        }

        void RunTask(byte taskNumber)
        {
            WriteToOutput($"Iniciando tarea {taskNumber}");
            Thread.Sleep((new Random()).Next(15000));
            WriteToOutput($"Finalizado tarea {taskNumber}");
        }

        void RunTaskGroup()
        {
            Task[] taskGroup = new Task[]
            {
                Task.Run(()=> RunTask(1)),
                Task.Run(()=> RunTask(2)),
                Task.Run(()=> RunTask(3)),
                Task.Run(()=> RunTask(4)),
                Task.Run(()=> RunTask(5))
            };
            WriteToOutput("Esperando a que finalicen todas las tareas...");
            Task.WaitAll(taskGroup);
            WriteToOutput("Todas las tareas han finalizado");
            WriteToOutput("Esperando a que finalice al menos una tarea...");
            taskGroup = new Task[]
            {
                Task.Run(()=> RunTask(6)),
                Task.Run(()=> RunTask(7)),
                Task.Run(()=> RunTask(8)),
                Task.Run(()=> RunTask(9)),
                Task.Run(()=> RunTask(10))
            };
            Task.WaitAny(taskGroup);
            WriteToOutput("Almenos una tarea finalizo");
        }
        #endregion

        #region Task<T>
        void ReturnTaskValue()
        {
            Task<int> T;
            T = Task.Run<int>(() => new Random().Next(1000));
            WriteToOutput($"Valor devuelto por la tarea {T.Result}");

            Task<int> T2 = Task.Run(() =>
            {
                WriteToOutput("Obtener el numero aleatorio");
                Thread.Sleep(10000);
                return new Random().Next(1000);                
            });
            WriteToOutput($"Esperar el resultado de la tarea ...");
            WriteToOutput($"La tarea devolvio el valor {T2.Result}");
            WriteToOutput($"Fin de la ejecucion del metodo ReturnTaskValue");

        }
        #endregion
    }
}
