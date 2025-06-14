using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TimeRecordingApp.Converter
{

    [ValueConversion(typeof(TimeSpan), typeof(string))]
    class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //CultureInfo culture = new CultureInfo("de-DE");

            TimeSpan time = (TimeSpan)value;
            return time.ToString("hh':'mm", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string time = value.ToString();
            TimeSpan result;
            if (TimeSpan.TryParse(time, out result))
            {
                return result;
            }
            return value;
        }
    }
}
