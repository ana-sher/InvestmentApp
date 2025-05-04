using InvestmentCore.CalculationModels;
using InvestmentCore.CalculationServices;

namespace InvestmentCore.Tests.CalculationServicesTests;

public class LoanCalculationServiceTests
{
    private readonly LoanCalculationService _service;

    public LoanCalculationServiceTests()
    {
        _service = new LoanCalculationService();
    }

    [Fact]
    public void CalculateFutureInterest_WhenGivenValidInput_ReturnsFutureInterest()
    {
        // Arrange
        var loan = new LoanConditions
        (
            agreementDate: new DateTime(2025, 1, 1),
            principal: 10000m,
            annualRate: 0.05m,
            years: 5
        );
        var calculationDate = new DateTime(2026, 1, 1);

        // Act
        var result = _service.CalculateFutureInterest(loan, calculationDate);

        // Assert
        Assert.True(result > 0, "Future interest should be greater than zero.");
    }

    [Theory]
    [InlineData(10000, 0.05, 5, "01/01/2025", "01/01/2025", 1322.74)]
    [InlineData(10000, 0.05, 5, "01/01/2025", "01/02/2025", 1281.07)]
    [InlineData(10000, 0.05, 5, "01/01/2025", "01/01/2026", 863.74)]
    [InlineData(5000, 0.03, 3, "01/01/2025", "01/07/2025", 164.62)]
    public void CalculateFutureInterest_WhenGivenInput_ReturnsCorrectFutureInterest(
        decimal principal, decimal annualRate, int years, string agreementDate, string calculationDate, decimal expectedFutureInterest)
    {
        // Arrange
        var loan = new LoanConditions
        (
            agreementDate: DateTime.Parse(agreementDate),
            principal: principal,
            annualRate: annualRate,
            years: years
        );
        var calcDate = DateTime.Parse(calculationDate);

        // Act
        var result = _service.CalculateFutureInterest(loan, calcDate);

        // Assert
        Assert.Equal(expectedFutureInterest, result, precision: 2);
    }

    [Fact]
    public void CalculateFutureInterest_WhenGivenWrongCalculateDate_ThrowsException()
    {
        // Arrange
        var loan = new LoanConditions
        (
            agreementDate: new DateTime(2025, 1, 1),
            principal: 10000m,
            annualRate: 0.05m,
            years: 5
        );
        var calculationDate = new DateTime(2024, 12, 31);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.CalculateFutureInterest(loan, calculationDate));
    }
}
