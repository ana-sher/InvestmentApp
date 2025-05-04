namespace InvestmentConsoleApp.InputReaders.Validators;

internal class DateTimeValidator() : BaseValidator<DateTime>("dd/MM/yyyy")
{
    protected override bool IsValid(string input) => DateTime.TryParse(input, out _);
    protected override DateTime ParseInput(string input) => DateTime.Parse(input);
}
