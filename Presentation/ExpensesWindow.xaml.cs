namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System;
    using System.Linq;
    using System.Windows;

    public partial class ExpensesWindow : Window
    {
        private int selectedCategoryId;
        private MainPage mainPage;

        public ExpensesWindow(int categoryId)
        {
            this.InitializeComponent();
            this.selectedCategoryId = categoryId;

            //this.mainPage = mainPage;
            this.FillAccountsComboboxWithAccounts();
        }

        public static event Action ExpenseAdded;

        private void FillAccountsComboboxWithAccounts()
        {
            int? currUserId = SessionManager.CurrentUserId;
            var currUserAccounts = AccountService.GetCurrentUserAccounts();
            if (currUserAccounts == null)
            {
                MessageBox.Show("Спершу потрібно додати рахунки, а тоді вже витрати!");
            }
            else
            {
                this.expenseAddingAccountChooseComboBox.ItemsSource = currUserAccounts.ToList();
                this.expenseAddingAccountChooseComboBox.DisplayMemberPath = "Name";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ExpenseAddingAddButton_Click(object sender, RoutedEventArgs e)
        {
            string sum = this.expenseAddingSumTextBox.Text;
            var selectedAccount = this.expenseAddingAccountChooseComboBox.SelectedItem as Account;
            DateTime expenseDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(sum) || selectedAccount == null)
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
                return;
            }

            if (!double.TryParse(sum, out double expenseSum))
            {
                MessageBox.Show("Поле суми витрати має містити число!");
                return;
            }

            try
            {
                await ExpenseService.AddExpenseAsync(this.selectedCategoryId, expenseSum, selectedAccount.Id);
                MessageBox.Show("Витрату успішно додано!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
