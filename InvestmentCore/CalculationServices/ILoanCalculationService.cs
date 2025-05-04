using InvestmentCore.CalculationModels;

namespace InvestmentCore.CalculationServices;

public interface ILoanCalculationService
{
    decimal CalculateFutureInterest(LoanConditions loan, DateTime calculationDate);
}
