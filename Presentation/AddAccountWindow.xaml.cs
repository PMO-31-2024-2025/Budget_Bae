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

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string balanceText = this.Balance.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(balanceText))
            {
                MessageBox.Show("Усі поля мають бути заповненими.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(balanceText, out decimal balance) || balance < 0)
            {
                MessageBox.Show("Введіть достовірний баланс", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            MessageBox.Show("Акаунт створено успішно!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
