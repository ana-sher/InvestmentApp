using InvestmentCore.CalculationModels;

namespace InvestmentCore.CalculationServices;

public class LoanCalculationService: ILoanCalculationService
{
    // Amortization formula for total monthly payment (TMP = x * (i(1 + i)^t) / ((1 + i)^t - 1))
    // Source: https://www.investopedia.com/terms/a/amortization.asp
    private decimal CalculateMonthlyPayment(decimal principal, decimal monthlyRate, int totalPayments)
    {
        return principal * (monthlyRate * (decimal)Math.Pow((double)(1 + monthlyRate), totalPayments)) /
                                ((decimal)Math.Pow((double)(1 + monthlyRate), totalPayments) - 1);
    }

    public decimal CalculateFutureInterest(LoanConditions loan, DateTime calculationDate)
    {
        if (calculationDate < loan.AgreementDate)
            throw new ArgumentException("Calculation date must be after the agreement date.");

        decimal monthlyRate = loan.AnnualRate / 12; // i = r/12
        int totalPayments = loan.Years * 12; // t = n*12

        decimal monthlyPayment = CalculateMonthlyPayment(loan.Principal, monthlyRate, totalPayments);

        decimal futureInterest = 0;
        decimal remainingPrincipal = loan.Principal;

        int currentPaymentIndex = 0;
        DateTime currentPaymentDate = loan.AgreementDate;

        // Generate payments until we reach the calculation date
        while (currentPaymentDate < calculationDate && currentPaymentIndex < totalPayments)
        {
            // Calculate interest for this period
            decimal interestForPeriod = remainingPrincipal * monthlyRate;
            decimal principalForPeriod = monthlyPayment - interestForPeriod;

            // Update remaining principal
            remainingPrincipal -= principalForPeriod;

            // Move to next payment
            currentPaymentDate = currentPaymentDate.AddMonths(1);
            currentPaymentIndex++;
        }

        // Calculate future interest from calculation date to end
        for (int i = currentPaymentIndex; i < totalPayments; i++)
        {
            decimal interestForPeriod = remainingPrincipal * monthlyRate;
            decimal principalForPeriod = monthlyPayment - interestForPeriod;

            futureInterest += interestForPeriod;
            remainingPrincipal -= principalForPeriod;
        }

        return futureInterest;
    }
}
