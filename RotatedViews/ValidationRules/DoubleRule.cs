using System.Globalization;
using System.Windows.Controls;

using RotatedViews.Resources;

namespace RotatedViews.ValidationRules
{
    public class DoubleRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var number = value as string;

            if (double.TryParse(number, out double doubleValue))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, UIStrings.DoubleRuleLabel);
            }
        }
    }
}
