namespace InvestmentConsoleApp.InputReaders.Validators;

internal class DecimalValidator() : BaseValidator<decimal>("single number with '.' as a separator (0.5, 100.2, etc.)")
{
    protected override bool IsValid(string input) => decimal.TryParse(input.Replace(',', '.'), out _);
    protected override decimal ParseInput(string input) => decimal.Parse(input.Replace(',', '.'));
    internal override decimal GetInput(string? input, decimal min = 0m, decimal max = decimal.MaxValue)
    {
        return base.GetInput(input, min, max);
    }
}
