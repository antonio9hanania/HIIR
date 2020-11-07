using System;
using System.Globalization;
using Xamarin.Forms;

namespace HIIR
{
    public class IsRunningConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            bool res = false;
            if (val == "Running Duration")
                res = true;

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
