namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using System.Windows;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Interaction logic for IncomeWindow.xaml
    /// </summary>
    public partial class IncomeWindow : Window
    {
        private static ILogger logger;
        private readonly Account selectedAccount;
        private List<string> incomeCategories = new List<string>
        {
            "З/П",
            "Стипендія",
            "Кишенькові",
            "Соц. виплати",
            "Премія",
            "Інше"
        };

        public IncomeWindow(Account account)
        {
            this.InitializeComponent();
            this.selectedAccount = account; // Прив'язуємо рахунок до об'єкта
            this.UpdateIncomeWindowComboBox();
        }

        public static void InitializeLogger(ILogger logger)
        {
            IncomeWindow.logger = logger;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IncomeAddingIncomeSum_LostFocus(object sender, RoutedEventArgs e)
        {
            // Реалізація
        }

        private async void IncomeAddingButton_Click(object sender, RoutedEventArgs e)
        {
            string sum = this.incomeAddingIncomeSumTextBox.Text;
            string selectedCategory = this.incomeAddingCategoryChooseCombobox.SelectedItem?.ToString();
            logger?.LogInformation($"Спроба внести дохід: {selectedCategory} {sum} UAH.");

            if (string.IsNullOrWhiteSpace(sum) || string.IsNullOrWhiteSpace(selectedCategory))
            {
                logger?.LogWarning("Усі поля мають бути заповнені!");
                MessageBox.Show("Усі поля мають бути заповнені!");
                return;
            }

            if (!decimal.TryParse(sum, out decimal incomeSum))
            {
                logger?.LogWarning("Поле суми поповнення має містити число!");
                MessageBox.Show("Поле суми поповнення має містити число!");
                return;
            }

            try
            {
                await IncomeService.AddIncomeAsync(selectedCategory, (double)incomeSum, this.selectedAccount.Id);

                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
                this.Close();
            }
            catch (Exception ex)
            {
                logger?.LogWarning("Помилка!");
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateIncomeWindowComboBox()
        {
            this.incomeAddingCategoryChooseCombobox.ItemsSource = this.incomeCategories;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.incomeAddingIncomeSumTextBox.Focus();
            this.incomeAddingCategoryChooseCombobox.SelectedIndex = 0;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void IncomeAddingIncomeSumTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.incomeAddingCategoryChooseCombobox.Focus();
            }
        }

        private void IncomeAddingCategoryChooseCombobox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.IncomeAddingButton_Click(sender, e);
            }
        }
    }
}
