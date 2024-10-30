using System;
using System.Globalization;
using System.Windows.Controls;

namespace lab6
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || !(value is DateTime date))
            {
                return new ValidationResult(false, "Пожалуйста, выберите корректную дату.");
            }

            // Дополнительные проверки можно добавить здесь
            return ValidationResult.ValidResult;
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    internal class DateValidationRule
    {
    }
}
*/