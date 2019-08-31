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
using System.Windows.Shapes;

namespace Exam_WPF_EX1
{
    /// <summary>
    /// Interaction logic for ValuesWindow.xaml
    /// </summary>
    public partial class ValuesWindow : Window
    {

        public ValuesWindow(Double Width, Double Height, string TextOnButton)
        {
            InitializeComponent();

            WidthShow.DataContext = Width;
            HeightShow.DataContext = Height;
            ButtonTextShow.DataContext = TextOnButton;
        }

    }
}
