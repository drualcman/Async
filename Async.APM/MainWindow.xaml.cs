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

/// <summary>
/// APM => Asyn Programming Model
/// </summary>
namespace Async.APM
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

        private async void ValidateUrl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UrlToValidate.Text);

                HttpWebResponse response = await Task<WebResponse>.Factory.FromAsync(
                        request.BeginGetResponse, 
                        request.EndGetResponse, 
                        request
                    ) as HttpWebResponse;
                Result.Content = $"Estatus devuelto: {response.StatusCode} {response.StatusDescription}. ";
                
                IAsyncResult result = request.BeginGetResponse(GetResponse, request);
            }
            catch (Exception ex)
            {
                Result.Content = ex.Message;
            }
        }

        private void GetResponse(IAsyncResult ar)
        {
            try
            {
                HttpWebRequest request = ar.AsyncState as HttpWebRequest;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
                Result.Dispatcher.Invoke(() =>
                    Result.Content = $"Estatus devuelto: {response.StatusCode} {response.StatusDescription}. ");
            }
            catch (Exception ex)
            {
                Result.Dispatcher.Invoke(new Action(() =>
                {
                    Result.Content = ex.Message;
                }));
            }
        }
    }
}
