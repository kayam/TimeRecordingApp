using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace TimeRecordingApp.Converter
{

    [ValueConversion(typeof(DateTime), typeof(string))]
    class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //CultureInfo culture = new CultureInfo("de-DE");

            DateTime dateTime = (DateTime)value;
            return dateTime.ToString("dd.MM.yyyy", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateTime = value.ToString();
            DateTime result;
            if (DateTime.TryParse(dateTime, out result))
            {
                return result;
            }
            return value;
        }
    }
}
