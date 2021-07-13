using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Async.Dispacher
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

        private void btnGetResult_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(()=> 
            {
                string result = "Resultado obtenido.";
                lblResult.Dispatcher.BeginInvoke(new Action(() => ShowMessage(result)));
                lblResult.Dispatcher.BeginInvoke(new ShowDelegate(ShowMessage), result);                
                lblResult.Dispatcher.InvokeAsync(() => ShowMessage(result));
            });
        }

        void ShowMessage(string message)
        {
            lblResult.Content += message;
        }
    }
}
