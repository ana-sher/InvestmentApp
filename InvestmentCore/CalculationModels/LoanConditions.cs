namespace InvestmentCore.CalculationModels;

public record LoanConditions
{
    public DateTime AgreementDate { get; init; }
    public DateTime AgreementEndDate { get; init; }
    public decimal Principal { get; init; }
    public decimal AnnualRate { get; init; }
    public int Years { get; init; }
    public LoanConditions(DateTime agreementDate, decimal principal, decimal annualRate, int years)
    {
        Validate(agreementDate, principal, annualRate, years);
        AgreementDate = agreementDate;
        AgreementEndDate = agreementDate.AddYears(years);
        Principal = principal;
        AnnualRate = annualRate;
        Years = years;
    }
    private void Validate(DateTime agreementDate, decimal principal, decimal annualRate, int years)
    {
        if (agreementDate == default)
            throw new ArgumentException("Agreement date is not valid.");
        if (principal <= 0)
            throw new ArgumentException("Principal amount must be greater than zero.");
        if (annualRate <= 0 || annualRate >= 1)
            throw new ArgumentException("Annual rate must be between 0 and 1.");
        if (years <= 0)
            throw new ArgumentException("Years must be greater than zero.");
    }
}
