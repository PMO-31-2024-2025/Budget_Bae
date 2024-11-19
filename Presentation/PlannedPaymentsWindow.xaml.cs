namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for PlannedPaymentsWindow.xaml
    /// </summary>
    public partial class PlannedPaymentsWindow : Window
    {
        public PlannedPaymentsWindow()
        {
            this.InitializeComponent();
            if (SessionManager.CurrentUserId != null)
            {
                this.PlannedPayments = PlannedExpenseService.GetPlannedExpenses();
            }
            else
            {
                this.PlannedPayments = [];
            }


            UpdateComboBoxes();
            this.UpdatePaymentsGrid();

        }

        public List<PlannedExpense> PlannedPayments { get; set; }

        private void UpdatePaymentsGrid()
        {
            this.PaymentsStackPanel.Children.Clear();

            for (int i = 0; i < this.PlannedPayments.Count; i++)
            {
                Grid payment = new Grid();
                payment.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                payment.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(45) });
                payment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(235) });
                payment.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                Border border = new Border()
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFromString("#DAB6FC")),
                    Background = (SolidColorBrush)(new BrushConverter().ConvertFromString("#4DDAB6FC")),
                    CornerRadius = new CornerRadius(15),
                    Margin = new Thickness(10, 5, 5, 5),
                    Width = 330,
                    Height = 70,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                Label name = new Label()
                {
                    Width = 225,
                    Content = new TextBlock
                    {
                        Text = this.PlannedPayments[i].Name,
                        TextWrapping = TextWrapping.Wrap
                    },
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(name, 0);
                Grid.SetColumn(name, 0);
                Grid.SetRowSpan(name, 2);
                payment.Children.Add(name);

                Button delete = new Button()
                {
                    Style = (Style)this.FindResource("CloseButton"),
                    Width = 15,
                    Height = 15,
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 5, 15, 0),
                    Tag = this.PlannedPayments[i].Id
                };
                delete.Click += this.Delete_Click;
                Grid.SetRow(delete, 0);
                Grid.SetColumn(delete, 1);
                payment.Children.Add(delete);

                Label date = new Label()
                {
                    Content = new TextBlock
                    {
                        Text = $"Внесок {this.PlannedPayments[i].NotigicationDate} числа\n{this.PlannedPayments[i].PlannedSum} UAH",
                        TextAlignment = TextAlignment.Right
                    },
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    FontSize = 10,
                    Margin = new Thickness(0, 0, 10, 5)
                };
                Grid.SetRow(date, 1);
                Grid.SetColumn(date, 1);
                payment.Children.Add(date);

                border.Child = payment;
                this.PaymentsStackPanel.Children.Add(border);

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;

            if (deleteButton == null || !(deleteButton.Tag is int paymentId))
            {
                MessageBox.Show("Неможливо знайти платіж для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var paymentToDelete = DbHelper.dbc.PlannedExpenses.FirstOrDefault(p => p.Id == paymentId);

                if (paymentToDelete != null)
                {
                    DbHelper.dbc.PlannedExpenses.Remove(paymentToDelete);
                    DbHelper.dbc.SaveChanges();

                    MessageBox.Show("Платіж успішно видалено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                    var newWindow = new PlannedPaymentsWindow();
                    this.Close();
                    newWindow.Show();
                }
                else
                {
                    MessageBox.Show("Платіж не знайдено.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text == textBox.Tag.ToString())
                {
                    textBox.Text = string.Empty;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = textBox.Tag.ToString();
                }
            }
        }

        private void CreatePayment_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string amountText = this.Amount.Text.Trim();
            string dateText = this.Date.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(amountText) || string.IsNullOrEmpty(dateText))
            {
                MessageBox.Show("Усі поля мають бути заповненими.");
                return;
            }
            else if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Сума поповнення повинна бути числом.");
                return;
            }
            else if (!int.TryParse(dateText, out int date) || date < 1 || date > 28)
            {
                MessageBox.Show("Число дати повинно бути від 1 до 28.");
                return;
            }
            else
            {
                int notificationDate = int.Parse(dateText);
                double expenseAmount = double.Parse(amountText);
                PlannedExpenseService.AddPlannedExpense(name, notificationDate, expenseAmount);
                MessageBox.Show("Платіж створено успішно!"); 
                var newWindow = new PlannedPaymentsWindow();
                this.Close();
                newWindow.Show();
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
            }
        }

        private void AddPayment_Click(object sender, RoutedEventArgs e)
        {
            this.replenishment.Visibility = Visibility.Collapsed;
            this.replenishmentBorder.Visibility = Visibility.Collapsed;
            this.adding.Visibility = Visibility.Visible;
            this.addingBorder.Visibility = Visibility.Visible;
            this.AddPayment.Visibility = Visibility.Collapsed;
        }

        private async void TopUpPayment_Button_Click(object sender, RoutedEventArgs e)
        {
            string sum = this.TopUpAmountPayment.Text;
            var selectedAccount = this.AccountsList.SelectedItem as Account;
            string plannedCategoryName = "Заплановані платежі";
            int? currentUserId = SessionManager.CurrentUserId;

            if (string.IsNullOrWhiteSpace(sum) || selectedAccount == null || currentUserId == null)
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
                return;
            }

            if (!double.TryParse(sum, out double paymentSum))
            {
                MessageBox.Show("Поле суми поповнення має містити число!");
                return;
            }

            try
            {
                var category = DbHelper.dbc.ExpensesCategories
                    .FirstOrDefault(c => c.Name == plannedCategoryName && c.UserId == currentUserId);

                if (category == null)
                {
                    category = new ExpenseCategory
                    {
                        Name = plannedCategoryName,
                        UserId = currentUserId.Value
                    };
                    DbHelper.dbc.ExpensesCategories.Add(category);
                    await DbHelper.dbc.SaveChangesAsync();
                }

                var newExpense = new Expense
                {
                    ExpenseSum = paymentSum,
                    AccountId = selectedAccount.Id,
                    CategoryId = category.Id,
                    ExpenseDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                DbHelper.dbc.Expenses.Add(newExpense);

                selectedAccount.Balance -= paymentSum;
                DbHelper.dbc.Update(selectedAccount);
                await DbHelper.dbc.SaveChangesAsync();

                MessageBox.Show("Поповнення успішно внесено як витрату!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void closeAddPayment_Click(object sender, RoutedEventArgs e)
        {
            this.adding.Visibility = Visibility.Collapsed;
            this.addingBorder.Visibility = Visibility.Collapsed;
            this.replenishment.Visibility = Visibility.Visible;
            this.replenishmentBorder.Visibility = Visibility.Visible;
            this.AddPayment.Visibility = Visibility.Visible;
        }

        private void UpdateComboBoxes()
        {
            PaymentsList.ItemsSource = this.PlannedPayments;
            PaymentsList.DisplayMemberPath = "Name";

            var accounts = DbHelper.dbc.Accounts.Where(a => a.UserId == SessionManager.CurrentUserId).ToList();

            AccountsList.ItemsSource = accounts;
            AccountsList.DisplayMemberPath = "Name";
        }


    }
}
