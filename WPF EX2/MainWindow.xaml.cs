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

namespace WPF_EX2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static bool ButtonClicked = false;
        int i = 30;
        public MainWindow()
        {

            InitializeComponent();
            if (ButtonClicked == false)
                CountAsync();

        }
        private void Answer1_Click(object sender, RoutedEventArgs e)
        {

            ButtonClicked = true;
            Answer1.Foreground = new SolidColorBrush(Colors.DarkGreen);
            Action action = InCaseCorrectAnswer;
            SafeInvoke(action);
        }
        private void Answer2_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked = true;
            InCaseWrongAnswer();
        }
        private void Answer3_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked = true;
            InCaseWrongAnswer();
        }

        //Option 4 - is the correct one.
        private void Answer4_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked = true;
            InCaseWrongAnswer();
        }
       
      
        void InCaseWrongAnswer()
        {
            Background = new SolidColorBrush(Colors.OrangeRed);
            Answer1.IsEnabled = false;
            Answer2.IsEnabled = false;
            Answer3.IsEnabled = false;
            Answer4.IsEnabled = false;

        }
        void InCaseCorrectAnswer()
        {


            Background = new SolidColorBrush(Colors.DarkGreen);
            Answer1.IsEnabled = false;
            Answer2.IsEnabled = false;
            Answer3.IsEnabled = false;
            Answer4.IsEnabled = false;
        }
        public async void CountAsync()
        {
            if (ButtonClicked)
                return;
            for (i = 30; i >= 0; i--)
            {
                if (ButtonClicked)
                {

                    break;
                }
                Timer.Content = i.ToString();
                await Task.Run(() =>
                {

                    Thread.Sleep(500);
                    if (i == 11 && ButtonClicked == false)
                    {
                        Action action = On10SecLeft;
                        SafeInvoke(action);
                    }
                    if (i == 0)
                    {
                        Action action = OnTimesUp;
                        SafeInvoke(action);
                    }

                });

            }

        }


        private void OnTimesUp()
        {

            InCaseWrongAnswer();

        }

        private void On10SecLeft()
        {
            Timer.Foreground = new SolidColorBrush(Colors.Red);
        }
        private void SafeInvoke(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action.Invoke();
                return;
            }
            this.Dispatcher.Invoke(action);
        }
    }
}
