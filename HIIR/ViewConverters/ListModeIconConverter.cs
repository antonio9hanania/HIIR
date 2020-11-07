using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace HIIR
{
    public class ListModeIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            string res = String.Empty;
            if (val == "Running Duration")
                res = "ico_running_interval_white.png";
            else if (val == "Walking Duration")
                res = "ico_walking_interval_white.png";

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
