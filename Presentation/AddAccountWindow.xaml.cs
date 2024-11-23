namespace Presentation
{
    using BusinessLogic.Services;
    using System.Windows;

    /// <summary>
    /// Interaction logic for AddAccountWindow.xaml.
    /// </summary>
    public partial class AddAccountWindow : Window
    {
        public AddAccountWindow()
        {
            this.InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            string nameInput = this.AccountNameTextBox.Text.Trim();
            string balanceTextInput = this.AccountBalanceTextBox.Text.Trim();

            if (string.IsNullOrEmpty(nameInput) || string.IsNullOrEmpty(balanceTextInput))
            {
                MessageBox.Show("Усі поля мають бути заповненими.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!double.TryParse(balanceTextInput, out double balance) || balance < 0)
            {
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
