namespace Presentation
{
    using DAL.Data;
    using DAL.Models;
    using System.Windows;

    /// <summary>
    /// Interaction logic for IncomeWindow.xaml
    /// </summary>
    public partial class IncomeWindow : Window
    {
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

            if (string.IsNullOrWhiteSpace(sum) || string.IsNullOrWhiteSpace(selectedCategory))
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
                return;
            }

            if (!decimal.TryParse(sum, out decimal incomeSum))
            {
                MessageBox.Show("Поле суми поповнення має містити число!");
                return;
            }

            try
            {
                // Додати новий прибуток
                var newIncome = new Income
                {
                    IncomeSum = (double)incomeSum,
                    AccountId = this.selectedAccount.Id,
                    IncomeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Category = selectedCategory
                };

                DbHelper.dbс.Add(newIncome);

                // Оновити баланс рахунку
                this.selectedAccount.Balance += (double)incomeSum;
                DbHelper.dbс.Update(this.selectedAccount);

                await DbHelper.dbс.SaveChangesAsync();

                MessageBox.Show("Поповнення успішно додано!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                // Перезавантажити головний інтерфейс для відображення оновленого балансу
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateIncomeWindowComboBox()
        {
            this.incomeAddingCategoryChooseCombobox.ItemsSource = this.incomeCategories;
        }
    }
}
