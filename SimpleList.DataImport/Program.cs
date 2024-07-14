using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SimpleList.Domain.Concrete;
using SimpleList.WebUI.Services;

namespace SimpleList.DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            // need excel file path for the argument
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the path to the Excel file as a command-line argument.");
                return;
            }

            string filePath = args[0]; // First argument should be the path to the Excel file



            var serviceProvider = SetupServices();

            // Calling the service to import data
            using (var scope = serviceProvider.CreateScope())
            {
                // Adding import and logging services
                var excelDataService = scope.ServiceProvider.GetRequiredService<ExcelDataService>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                try
                {
                    excelDataService.ImportDataFromExcel(filePath);
                    Console.WriteLine("Excel data import completed successfully.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during data import.");
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Optionally log the exception for more detailed diagnostics
                }
            }
        }

        private static IServiceProvider SetupServices()
        {
            var serviceCollection = new ServiceCollection();

            // Register ApplicationDbContext and ExcelDataService 
            serviceCollection.AddTransient<ApplicationDbContext>();
            serviceCollection.AddTransient<ExcelDataService>();

            // Setup logging to console
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            return serviceCollection.BuildServiceProvider();
        }
    }
}
