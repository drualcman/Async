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

namespace Async.Await
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate void ShowDelegate(string message);

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGetResult_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "Calculando un numero aleatorio... ";
            Debug.WriteLine($"Hilo que lanza la tarea: {Thread.CurrentThread.ManagedThreadId}");
            Task<int> T = Task.Run(() => 
            {
                Debug.WriteLine($"Hilo que ejecuta la tarea: {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(10000);
                return new Random().Next(5000);
            });
            Debug.WriteLine($"Hilo antes del await: {Thread.CurrentThread.ManagedThreadId}");
            lblResult.Content += $"Numero obtenido: {await T}. ";
            Debug.WriteLine($"Hilo despues del await: {Thread.CurrentThread.ManagedThreadId}");
            lblResult.Content += "Continua con otras instrucciones. ";
        }

        void ShowMessage(string message)
        {
            lblResult.Content += message;
        }
    }
}
