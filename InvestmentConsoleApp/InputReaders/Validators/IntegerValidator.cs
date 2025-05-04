namespace InvestmentConsoleApp.InputReaders.Validators;

internal class IntegerValidator() : BaseValidator<int>("single number")
{
    protected override bool IsValid(string input) => int.TryParse(input, out _);
    protected override int ParseInput(string input) => int.Parse(input);
    internal override int GetInput(string? input, int min = 1, int max = int.MaxValue)
    {
        return base.GetInput(input, min, max);
    }
}
