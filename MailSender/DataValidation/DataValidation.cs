using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MailSenderNameSpace.DataValidation
{
    /// <summary>
    /// Класс для специальной проверки на корректность введенного значения id письма.
    /// </summary>
    class DataValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int intValue;
            //ValidationResult result = null;
            try
            {
                intValue = Convert.ToInt16(value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Введите число");
            }
            if (intValue < 0) return new ValidationResult(false, "Введите число больше 0" );

            return new ValidationResult(true, null);
        }
    }
}
