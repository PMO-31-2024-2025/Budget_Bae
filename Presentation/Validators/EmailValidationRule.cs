﻿namespace Presentation.Validators
{
    using System.Windows.Controls;

    public class EmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string input = value as string;

            if (string.IsNullOrWhiteSpace(input))
            {
                return new ValidationResult(false, "Поле не може бути порожнім!");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new ValidationResult(false, "Некоректний формат електронної пошти!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
