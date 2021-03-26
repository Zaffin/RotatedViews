using System.Globalization;
using System.Windows.Controls;

using RotatedViews.Resources;

namespace RotatedViews.ValidationRules
{
    public class PositiveIntegerRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var number = value as string;

            if (int.TryParse(number, out int intValue))
            {
                if (intValue > 0)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, UIStrings.PositiveIntegerRuleLabel);
                }
            }
            else
            {
                return new ValidationResult(false, UIStrings.PositiveIntegerRuleLabel);
            }
        }
    }
}
