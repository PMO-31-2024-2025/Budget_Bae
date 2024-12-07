namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System;
    using System.Linq;
    using System.Windows;
    using Microsoft.Extensions.Logging;

    public partial class ExpensesWindow : Window
    {
        private static ILogger logger;
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

        public static void InitializeLogger(ILogger logger)
        {
            ExpensesWindow.logger = logger;
        }

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
            logger?.LogInformation($"Спроба додати витрату: {ExpenseCategoryService.GetCategoryName(this.selectedCategoryId)} {sum} UAH.");

            if (string.IsNullOrWhiteSpace(sum) || selectedAccount == null)
            {
                logger?.LogWarning("Усі поля мають бути заповнені!");
                MessageBox.Show("Усі поля мають бути заповнені!");
                return;
            }

            if (!double.TryParse(sum, out double expenseSum))
            {
                logger?.LogWarning("Поле суми витрати має містити число!");
                MessageBox.Show("Поле суми витрати має містити число!");
                return;
            }

            if (double.Parse(sum) > selectedAccount.Balance)
            {
                logger?.LogWarning("На обраному рахунку недостатньо коштів!");
                MessageBox.Show("На обраному рахунку недостатньо коштів!");
                return;
            }

            try
            {
                await ExpenseService.AddExpenseAsync(this.selectedCategoryId, expenseSum, selectedAccount.Id);
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
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.expenseAddingSumTextBox.Focus();
            this.expenseAddingAccountChooseComboBox.SelectedIndex = 0;
        }

        private void ExpenseAddingSumTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.expenseAddingAccountChooseComboBox.Focus();
            }
        }

        private void ExpenseAddingAccountChooseComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.ExpenseAddingAddButton_Click(sender, e);
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
