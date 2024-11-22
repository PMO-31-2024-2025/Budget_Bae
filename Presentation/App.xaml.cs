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

            var mainWindow = this.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
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
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("UserService");
            UserService.InitializeLogger(logger);

            // Реєструємо MainWindow у DI
            services.AddTransient<MainWindow>();
        }
    }
}
