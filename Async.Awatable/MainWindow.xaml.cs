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

namespace Async.Awatable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGetResult_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "Obtener el nombre dle producto...";
            string productName = await GetProductName(1);
            lblResult.Content += Environment.NewLine + productName;
            await ShowName(2);
            lblResult.Content += $"{1}";
        }

        async Task<string> GetProductName(int id)
        {
            string Result = await Task.Run<string>(async () => 
            {
                await TimeSpan.FromSeconds(1000);
                return $"{id}: Chai";
            });
            return Result;
        }

        async Task ShowName(int id)
        {
            string productName = await Task.Run<string>(() =>
            {
                Thread.Sleep(1000);
                return $"{id}: Chang";
            });
            lblResult.Content += Environment.NewLine + productName;
        }
    }
}
