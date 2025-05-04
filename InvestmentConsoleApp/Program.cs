using InvestmentConsoleApp.InputReaders;
using InvestmentCore.CalculationServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<ILoanCalculationService, LoanCalculationService>();
    })
    .Build();

var reader = new FutureInterestInputReader();
var calculationService = host.Services.GetRequiredService<ILoanCalculationService>();
bool exit = false;

while (!exit)
{
    Console.Clear();
    Console.WriteLine("------- Greetings from your investment helper -------");
    Console.WriteLine("This program calculates the sum of all future interest payments for a loan.");
    Console.WriteLine("-----------------------------------------------------");
    var input = reader.ReadInput();

    var futureInterest = calculationService.CalculateFutureInterest(input.LoanConditions, input.CalculationDate);

    Console.WriteLine($"Sum of all future interest payments: {futureInterest:F2}");
    Console.WriteLine($"So from {input.CalculationDate} until {input.LoanConditions.AgreementEndDate} it's the interest amount that will be payed.");
    Console.WriteLine("Press 1 to calculate again, any other key to exit...");
    exit = Console.ReadKey().KeyChar != '1';
}
