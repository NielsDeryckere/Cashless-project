using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Register_Employee.Converter
{
    [ValueConversion(typeof(double), typeof(SolidColorBrush))]
    public class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double score = (double)value;
            if (score > 0)
                return new SolidColorBrush(Colors.Green);
            else{return new SolidColorBrush(Colors.Red);}
                

            
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
