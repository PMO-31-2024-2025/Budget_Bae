namespace Presentation
{
    using BusinessLogic.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            this.ConfigureLogging();
            this.ConfigureServices(serviceCollection);

            this.ServiceProvider = serviceCollection.BuildServiceProvider();

            base.OnStartup(e);
        }

        private void ConfigureLogging()
        {
            // Налаштування Serilog для запису у файл
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Щоденне логування
                .CreateLogger();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Інтеграція Serilog з Microsoft.Extensions.Logging
            services.AddLogging(builder =>
            {
                builder.ClearProviders(); // Видаляємо інші провайдери
                builder.AddSerilog();    // Додаємо Serilog
            });

            var serviceProvider = services.BuildServiceProvider();
            var userLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("UserService");
            UserService.InitializeLogger(userLogger);
            var savingsLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SavingsService");
            SavingService.InitializeLogger(savingsLogger);
            var accountLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("AccountService");
            AccountService.InitializeLogger(accountLogger);
            var addAccountLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("AddAccountService");
            AddAccountWindow.InitializeLogger(addAccountLogger);
            var expenseCategoryLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("ExpenseCategoryService");
            ExpenseCategoryService.InitializeLogger(expenseCategoryLogger);
            var addCategory = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("AddCategory");
            AddCategoryWindow.InitializeLogger(addCategory);
            var expenseLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("ExpenseService");
            ExpenseService.InitializeLogger(expenseLogger);
            var expensesWindowLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("ExpensesWindowService");
            ExpensesWindow.InitializeLogger(expensesWindowLogger);
            var incomeLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("IncomeService");
            IncomeService.InitializeLogger(incomeLogger);
            var incomeWindowLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("IncomeWindowService");
            IncomeWindow.InitializeLogger(incomeWindowLogger);
            var plannedExpenseLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("PlannedExpenseService");
            PlannedExpenseService.InitializeLogger(plannedExpenseLogger);
            var entryLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Entry");
            EntryWindow.InitializeLogger(entryLogger);
            var navBarLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("NavBar");
            NavBar.InitializeLogger(navBarLogger);
            var settingsLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Settings");
            SettingsWindow.InitializeLogger(settingsLogger);
            var savingsWindowLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SavingsWindow");
            SavingsWindow.InitializeLogger(savingsWindowLogger);
            var plannedPaymentsWindowLogger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("PlannedPaymentsWindow");
            PlannedPaymentsWindow.InitializeLogger(plannedPaymentsWindowLogger);
        }
    }
}
