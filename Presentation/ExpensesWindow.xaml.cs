namespace Presentation
{
    using BusinessLogic.Session;
    using DAL.Data;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ExpensesWindow.xaml
    /// </summary>
    public partial class ExpensesWindow : Window
    {
        public ExpensesWindow()
        {
            this.InitializeComponent();
            this.FillAccountsComboboxWithAccounts();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void expenseAddingSumTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private async void expenseAddingAddButton_Click(object sender, RoutedEventArgs e)
        {
            string sum = this.expenseAddingSumTextBox.Text;
            var selectedAccount = this.expenseAddingAccountChooseComboBox.SelectedItem;

            if (sum == null || selectedAccount == null)
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
            }
            else if (!decimal.TryParse(sum, out _))
            {
                MessageBox.Show("Поле суми витрати має містити число!");
            }
            else
            {
                try
                {
                    MessageBox.Show("Поповнення успішно!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                this.expenseAddingAccountChooseComboBox.Items.Clear();
                foreach (var account in DbHelper.db.Accounts.ToList())
                {
                    this.expenseAddingAccountChooseComboBox.Items.Add(account.Name);
                }
            }
        }
    }
}
