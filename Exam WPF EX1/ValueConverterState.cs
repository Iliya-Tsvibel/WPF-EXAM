using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Exam_WPF_EX1
{
    class ValueConverterState : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sliderValue = (double)value;
            var slidervalue = ((sliderValue - 40) / 60) * 100;
            if (slidervalue < 25)
                return "SMALL";
            else if (slidervalue < 50)
                return "MEDIUM";
            else if (slidervalue < 75)
                return "LARGE";
            else
                return "EXTRA LARGE";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
