using InvestmentCore.CalculationModels;

namespace InvestmentConsoleApp.InputReaders.InputModels;

internal record FutureInterestInput(LoanConditions LoanConditions, DateTime CalculationDate);
