using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

namespace Async.AwaitableExceptions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var unobserbedExceptions = e.Exception.InnerExceptions;
            foreach (var item in unobserbedExceptions)
            {
                Webcontent.Dispatcher.Invoke(() => Webcontent.Content += $"{item.Message}{Environment.NewLine}");
            }
            e.SetObserved();
        }

        private void GetContent_Click(object sender, RoutedEventArgs e)
        {
            Task T = Task.Run(async ()=> 
            {
                using WebClient client = new WebClient();
                string content = await client.DownloadStringTaskAsync("https://ticapacitacion.com2");
                Webcontent.Dispatcher.Invoke(() => Webcontent.Content = content);
            });
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Webcontent.Content += "Tarea ejecutada";
        }

        //private async void GetContent_Click(object sender, RoutedEventArgs e)
        //{
        //    using WebClient client = new WebClient();
        //    try
        //    {
        //        Webcontent.Content = await client.DownloadStringTaskAsync("https://ticapacitacion.com");
        //    }
        //    catch (WebException er)
        //    {
        //        Webcontent.Content = er.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        Webcontent.Content = ex.Message;
        //    }
        //}
    }
}
