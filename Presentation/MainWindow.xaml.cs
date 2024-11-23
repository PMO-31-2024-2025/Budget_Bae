/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
namespace Presentation
{
    using BusinessLogic.Session;
    using Microsoft.Extensions.Logging;
    using System.Windows;

    public partial class MainWindow : Window
    {
        private readonly ILogger<MainWindow> logger;

        public MainWindow()
        {
            this.InitializeComponent();
            this.MainFrame.Navigate(new MainPage());
            if (SessionManager.CurrentUserId == null)
            {
                this.Loaded += this.MainWindow_Loaded;
            }
            this.Closing += this.MainWindow_Closing;
        }

        public MainWindow(ILogger<MainWindow> logger)
        {
            this.InitializeComponent();
            this.logger = logger;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.ShowDialog();
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
