using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace lab4_1
{
    public class Validator
    {
        private readonly List<string> _errors;

        public Validator()
        {
            _errors = new List<string>();
        }

        public List<string> Errors => _errors;
        public bool IsValid => !_errors.Any();

        public void ClearErrors()
        {
            _errors.Clear();
        }

        public void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
        }

        public Validator ValidateString(string value, string fieldName, bool required = false, 
            int? minLength = null, int? maxLength = null, string pattern = null)
        {
            if (required && string.IsNullOrWhiteSpace(value))
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (!required && string.IsNullOrWhiteSpace(value))
            {
                return this;
            }

            if (minLength.HasValue && value.Length < minLength.Value)
                _errors.Add($"{fieldName} должен содержать минимум {minLength} символов");

            if (maxLength.HasValue && value.Length > maxLength.Value)
                _errors.Add($"{fieldName} должен содержать максимум {maxLength} символов");

            if (!string.IsNullOrEmpty(pattern) && !Regex.IsMatch(value, pattern))
                _errors.Add($"{fieldName} имеет неверный формат");

            return this;
        }

        public Validator ValidateInt(int value, string fieldName, 
            int? minValue = null, int? maxValue = null, bool required = false)
        {
            if (required && value == 0)
            {
                _errors.Add($"{fieldName} является обязательным полем");
                return this;
            }

            if (minValue.HasValue && value < minValue.Value)
                _errors.Add($"{fieldName} должен быть не меньше {minValue}");

            if (maxValue.HasValue && value > maxValue.Value)
                _errors.Add($"{fieldName} должен быть не больше {maxValue}");

            return this;
        }

        public Validator ValidateFileExists(string filePath, string fieldName = "Файл")
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                _errors.Add($"{fieldName} не может быть пустым");
                return this;
            }

            if (!File.Exists(filePath))
            {
                _errors.Add($"{fieldName} '{filePath}' не существует");
            }

            return this;
        }

        public void ThrowIfInvalid()
        {
            if (!IsValid)
            {
                throw new ValidationException(string.Join("; ", _errors));
            }
        }

        public static void Validate(Action<Validator> validationAction)
        {
            var validator = new Validator();
            validationAction(validator);
            validator.ThrowIfInvalid();
        }

        // Статические методы для ввода данных
        public static int GetValidatedInt(string fieldName, int minValue = 1, int maxValue = 100)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Введите {fieldName}: ");
                    string input = Console.ReadLine();
                    
                    Validator.Validate(v => v.ValidateInt(int.Parse(input), fieldName, minValue, maxValue, true));
                    return int.Parse(input);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Ошибка: введите целое число для {fieldName}");
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        public static string GetValidatedString(string fieldName, int minLength = 1, int maxLength = 100)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Введите {fieldName}: ");
                    string input = Console.ReadLine();
                    Validator.Validate(v => v.ValidateString(input, fieldName, true, minLength, maxLength));
                    return input;
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        public static string GetValidatedText(string fieldName)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"Введите {fieldName} (для завершения введите пустую строку):");
                    string text = "";
                    string line;
                    while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                    {
                        text += line + " ";
                    }
                    
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Console.WriteLine("Текст не может быть пустым");
                        continue;
                    }
                    
                    return text.Trim();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; }
        public List<string> Errors { get; }

        public ValidationResult(bool isValid, List<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}