using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace TimeRecordingApp.Validation
{
    class TimeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string time = value.ToString();
            TimeSpan result;
            if (TimeSpan.TryParse(time, out result))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Falsche Uhrzeit!");
        }
    }
}
