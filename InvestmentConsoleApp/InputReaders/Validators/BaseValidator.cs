namespace InvestmentConsoleApp.InputReaders.Validators;

internal abstract class BaseValidator<T>
{
    internal readonly string InputFormat;
    protected BaseValidator(string inputFormat) => InputFormat = inputFormat;

    protected abstract bool IsValid(string input);
    protected abstract T ParseInput(string input);
    internal virtual T GetInput(string? input, T min, T max)
    {
        while (input == null || !IsValid(input))
        {
            WriteErrorMessage(GetErrorMessage());
            input = Console.ReadLine();
        }
        var value = ParseInput(input);
        if(!CheckRange(value, min, max))
        {
            return GetInput(Console.ReadLine(), min, max);
        }
        return value;
    }
    private bool CheckRange(T value, T min, T max)
    {
        if (value is IComparable<T> comparable)
        {
            if (comparable.CompareTo(min) < 0)
            {
                WriteErrorMessage($"Input is less than minimal {min?.ToString()}. Try again.");
                return false;
            }
            if (comparable.CompareTo(max) > 0) 
            {
                WriteErrorMessage($"Input is greater than maximal {max?.ToString()}. Try again.");
                return false;
            }
        }
        return true;
    }

    private void WriteErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    protected virtual string GetErrorMessage() => $"Input is not valid, correct format: {InputFormat}. Please, change input and try again.";
}
