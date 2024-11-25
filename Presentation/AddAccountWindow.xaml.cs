namespace Presentation
{
    using BusinessLogic.Services;
    using System.Windows;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Interaction logic for AddAccountWindow.xaml.
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        private static ILogger logger;

        public AddAccountWindow()
        {
            this.InitializeComponent();
        }

        public static void InitializeLogger(ILogger logger)
        {
            AddAccountWindow.logger = logger;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            string nameInput = this.AccountNameTextBox.Text.Trim();
            string balanceTextInput = this.AccountBalanceTextBox.Text.Trim();
            logger?.LogInformation($"Спроба додати рахунок '{nameInput}'.");

            if (string.IsNullOrEmpty(nameInput) || string.IsNullOrEmpty(balanceTextInput))
            {
                logger.LogWarning("Не всі поля заповнені!");
                MessageBox.Show("Усі поля мають бути заповненими.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(balanceTextInput, out double balance) || balance < 0)
            {
                logger.LogWarning("Неправильний формат балансу!");
                MessageBox.Show("Введіть достовірний баланс", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            await AccountService.AddAccountAsync(nameInput, balance);

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow != null && mainWindow.MainFrame != null)
            {
                mainWindow.MainFrame.Navigate(new MainPage());
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.AccountNameTextBox.Focus();
        }

        private void AccountNameTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.AccountBalanceTextBox.Focus();
            }
        }

        private void AccountBalanceTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.AddAccount_Click(sender, e);
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}
