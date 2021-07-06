using System;
using System.Collections.Generic;
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
            CreateTask();
        }

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
    }
}
