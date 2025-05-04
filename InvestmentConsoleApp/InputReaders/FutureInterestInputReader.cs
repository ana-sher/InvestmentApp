using InvestmentConsoleApp.InputReaders.InputModels;
using InvestmentConsoleApp.InputReaders.Validators;
using InvestmentCore.CalculationModels;

namespace InvestmentConsoleApp.InputReaders;

internal class FutureInterestInputReader
{
    private readonly IntegerValidator _integerValidator = new();
    private readonly DecimalValidator _decimalValidator = new();
    private readonly DateTimeValidator _dateTimeValidator = new();
    internal FutureInterestInput ReadInput()
    {
        Console.WriteLine($"Enter agreement date ({_dateTimeValidator.InputFormat}): ");
        DateTime agreementDate = _dateTimeValidator.GetInput(Console.ReadLine(), DateTime.MinValue, DateTime.Now);
        WriteSuccessMessage(string.Format("Agreement date: {0}", agreementDate.ToString(_dateTimeValidator.InputFormat)));

        Console.WriteLine($"Enter investment duration in years ({_integerValidator.InputFormat}): ");
        int durationYears = _integerValidator.GetInput(Console.ReadLine());
        DateTime agreementEndDate = agreementDate.AddYears(durationYears);
        WriteSuccessMessage(string.Format("Duration in years: {0}", durationYears.ToString()));

        Console.WriteLine($"Enter calculation date ({_dateTimeValidator.InputFormat}): ");
        DateTime calculationDate = _dateTimeValidator.GetInput(Console.ReadLine(), agreementDate, agreementEndDate);
        WriteSuccessMessage(string.Format("Calculation date: {0}", calculationDate.ToString(_dateTimeValidator.InputFormat)));

        Console.WriteLine($"Enter investment amount ({_decimalValidator.InputFormat}): ");
        decimal investmentAmount = _decimalValidator.GetInput(Console.ReadLine());
        WriteSuccessMessage(string.Format("Investment amount: {0}", investmentAmount.ToString()));

        Console.WriteLine($"Enter annual interest rate ({_decimalValidator.InputFormat} %): ");
        decimal annualInterestRate = _decimalValidator.GetInput(Console.ReadLine()) / 100;
        WriteSuccessMessage(string.Format("Annual interest rate: {0}", annualInterestRate.ToString()));

        return new FutureInterestInput(
            new LoanConditions(agreementDate, investmentAmount, annualInterestRate, durationYears),
            calculationDate);
    }

    private void WriteSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
