namespace Presentation
{
    using System.Windows;
    using BusinessLogic.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using Serilog.Extensions.Logging;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureLogging();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            base.OnStartup(e);

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
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

            // Реєстрація інших сервісів
            services.AddTransient<MainWindow>();
            //services.AddTransient<IUserService, UserService>();
        }
    }
}
