using System;

namespace lab4_2
{
    public class Validator
    {
        private readonly System.Collections.Generic.List<string> _errors;

        public Validator()
        {
            _errors = new System.Collections.Generic.List<string>();
        }

        public System.Collections.Generic.List<string> Errors => _errors;
        public bool IsValid => !_errors.Any();

        public void ClearErrors()
        {
            _errors.Clear();
        }

        public void AddError(string errorMessage)
        {
            _errors.Add(errorMessage);
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

        public Validator ValidateByte(byte value, string fieldName, 
            byte? minValue = null, byte? maxValue = null, bool required = false)
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
        public static byte GetValidatedByte(string fieldName, byte minValue = 0, byte maxValue = 23)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Введите {fieldName} ({minValue}-{maxValue}): ");
                    string input = Console.ReadLine();
                    
                    if (byte.TryParse(input, out byte value))
                    {
                        if (value >= minValue && value <= maxValue)
                        {
                            return value;
                        }
                        else
                        {
                            Console.WriteLine($"Ошибка: {fieldName} должен быть от {minValue} до {maxValue}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: введите число от {minValue} до {maxValue}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        public static Time GetValidatedTime(string timeName)
        {
            Console.WriteLine($"\nВвод времени {timeName}:");
            byte hours = GetValidatedByte("часы", 0, 23);
            byte minutes = GetValidatedByte("минуты", 0, 59);
            
            return new Time(hours, minutes);
        }

        public static uint GetValidatedUint(string fieldName, uint minValue = 0, uint maxValue = 1440)
        {
            while (true)
            {
                try
                {
                    Console.Write($"Введите {fieldName}: ");
                    string input = Console.ReadLine();
                    
                    Validator.Validate(v => v.ValidateInt(int.Parse(input), fieldName, (int)minValue, (int)maxValue, true));
                    return uint.Parse(input);
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
    }

    public class ValidationResult
    {
        public bool IsValid { get; }
        public System.Collections.Generic.List<string> Errors { get; }

        public ValidationResult(bool isValid, System.Collections.Generic.List<string> errors)
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