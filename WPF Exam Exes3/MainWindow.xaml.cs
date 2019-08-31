using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Windows.Shapes;

namespace WPF_Exam_Exes3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            viewModel.GoLoadingTime.Restart();
            string text = "";

            string newUrl = InsertURL.Text;

            Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    text = client.DownloadString(newUrl);
                }

                int size = ASCIIEncoding.ASCII.GetByteCount(text);

                Action uiwork = () =>
                {
                    ShowMeSize.Text = $"The Size Of Current Site Is: {size} Bytes.";
                };

                SafeInvoke(uiwork);
                viewModel.GoLoadingTime.Stop();
            });
        }

        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess())
            {
                work.Invoke();
                return;
            }
            Dispatcher.Invoke(work);
        }

        private void Counter()
        {
            while (viewModel.GoLoadingTime.IsRunning)
            {
                SafeInvoke(new Action(() => { ShowMeTime.Text = viewModel.loadingTime.ToString(); }));
                Thread.Sleep(5);
            }
            ShowMeTime.Text = $"This site was loaded during: {viewModel.loadingTime.ToString()} mseconds";
        }

        
    }
}
