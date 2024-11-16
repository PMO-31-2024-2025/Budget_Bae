namespace Presentation
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public partial class ExpensesWindow : Window
    {
        private int selectedCategoryId;
        private MainPage mainPage;
        public static event Action ExpenseAdded;

        public ExpensesWindow(int categoryId)
        {
            InitializeComponent();
            this.selectedCategoryId = categoryId;
            this.mainPage = mainPage;
            this.FillAccountsComboboxWithAccounts();
        }

        private void FillAccountsComboboxWithAccounts()
        {
            int? currUserId = SessionManager.CurrentUserId;
            if (currUserId == null)
            {
                MessageBox.Show("Авторизуйтесь для можливості додавання витрат!");
            }
            else if (DbHelper.db.Accounts == null || !DbHelper.db.Accounts.Any())
            {
                MessageBox.Show("Спершу потрібно додати рахунки, а тоді вже витрати!");
            }
            else
            {
                var accounts = DbHelper.db.Accounts.ToList();
                expenseAddingAccountChooseComboBox.ItemsSource = accounts;
                expenseAddingAccountChooseComboBox.DisplayMemberPath = "Name";
            }
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void ExpenseAddingAddButton_Click(object sender, RoutedEventArgs e)
        {
            string sum = expenseAddingSumTextBox.Text;
            var selectedAccount = expenseAddingAccountChooseComboBox.SelectedItem as Account;
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
                var newExpense = new Expense
                {
                    ExpenseSum = expenseSum,
                    AccountId = selectedAccount.Id,
                    CategoryId = selectedCategoryId,
                    ExpenseDate = expenseDate.ToString("yyyy-MM-dd HH:mm:ss")
                };

                DbHelper.db.Add(newExpense);

                selectedAccount.Balance -= expenseSum;
                DbHelper.db.Update(selectedAccount);

                await DbHelper.db.SaveChangesAsync();

                MessageBox.Show("Витрату успішно додано!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
