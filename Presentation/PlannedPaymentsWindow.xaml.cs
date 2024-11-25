namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Interaction logic for PlannedPaymentsWindow.xaml
    /// </summary>
    public partial class PlannedPaymentsWindow : Window
    {
        private static ILogger logger;

        public PlannedPaymentsWindow()
        {
            InitializeComponent();

            if (SessionManager.CurrentUserId != null)
            {
                this.PlannedPayments = PlannedExpenseService.GetPlannedExpenses();
            }
            else
            {
                this.PlannedPayments = new List<PlannedExpense>();
            }

            this.UpdateComboBoxes();
            this.UpdatePaymentsGrid();
            this.PaymentsList.SelectionChanged += this.PaymentsList_SelectionChanged;
        }


        public List<PlannedExpense> PlannedPayments { get; set; }

        public static void InitializeLogger(ILogger logger)
        {
            PlannedPaymentsWindow.logger = logger;
        }

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

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            logger?.LogInformation($"Спроба видалити заощадження {PlannedExpenseService.GetPlannedExpenseName(Convert.ToInt32(deleteButton.Tag))}.");

            if (deleteButton == null || !(deleteButton.Tag is int paymentId))
            {
                MessageBox.Show("Неможливо знайти платіж для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                bool result = await PlannedExpenseService.DeletePlannedExpense(paymentId);
                if (result)
                {
                    var newWindow = new PlannedPaymentsWindow();
                    this.Close();
                    newWindow.Show();
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning("Помилка!");
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void CreatePayment_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string amountText = this.Amount.Text.Trim();
            string dateText = this.Date.Text.Trim();
            logger?.LogInformation($"Спроба додати запланований платіж {name}.");

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(amountText) || string.IsNullOrEmpty(dateText))
            {
                logger.LogWarning("Усі поля мають бути заповненими!");
                MessageBox.Show("Усі поля мають бути заповненими.");
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                logger.LogWarning("Сума повинна бути числом!");
                MessageBox.Show("Сума повинна бути числом.");
                return;
            }

            if (!int.TryParse(dateText, out int date) || date < 1 || date > 28)
            {
                logger.LogWarning("Число дати повинно бути від 1 до 28!");
                MessageBox.Show("Число дати повинно бути від 1 до 28.");
                return;
            }

            try
            {
                bool result = await PlannedExpenseService.AddPlannedExpense(name, date, (double)amount);
                if (result)
                {
                    var newWindow = new PlannedPaymentsWindow();
                    this.Close();
                    newWindow.Show();
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning("Помилка!");
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
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
            var selectedPayment = this.PaymentsList.SelectedItem as PlannedExpense;
            var selectedAccount = this.AccountsList.SelectedItem as Account;
            string plannedCategoryName = "Заплановані платежі";
            int? currentUserId = SessionManager.CurrentUserId;
            logger?.LogInformation($"Спроба поповнити запланований платіж {selectedPayment.Name}.");

            if (string.IsNullOrWhiteSpace(sum) || selectedAccount == null || currentUserId == null)
            {
                logger.LogWarning("Усі поля мають бути заповнені!");
                MessageBox.Show("Усі поля мають бути заповнені!");
                return;
            }

            if (!double.TryParse(sum, out double paymentSum))
            {
                logger.LogWarning("Поле суми поповнення має містити число!");
                MessageBox.Show("Поле суми поповнення має містити число!");
                return;
            }

            try
            {
                if (selectedAccount.Balance < paymentSum)
                {
                    logger.LogWarning("Недостатньо коштів на рахунку!");
                    MessageBox.Show("Недостатньо коштів на рахунку.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var category = DbHelper.dbc.ExpensesCategories
                    .FirstOrDefault(c => c.Name == plannedCategoryName && c.UserId == currentUserId);

                if (category == null)
                {
                    bool categoryAdded = await ExpenseCategoryService.AddExpensCategoryAsync(plannedCategoryName);
                    if (!categoryAdded)
                    {
                        MessageBox.Show("Не вдалося створити категорію!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    category = DbHelper.dbc.ExpensesCategories
                        .FirstOrDefault(c => c.Name == plannedCategoryName && c.UserId == currentUserId);
                }

                bool result = await ExpenseService.AddExpenseAsync(category.Id, paymentSum, selectedAccount.Id);
                logger?.LogInformation($"Запланований платіж {selectedPayment.Name} поповнено на {sum}.");

                if (result)
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    if (mainWindow != null && mainWindow.MainFrame != null)
                    {
                        mainWindow.MainFrame.Navigate(new MainPage());
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning("Помилка!");
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void CloseAddPayment_Click(object sender, RoutedEventArgs e)
        {
            this.adding.Visibility = Visibility.Collapsed;
            this.addingBorder.Visibility = Visibility.Collapsed;
            this.replenishment.Visibility = Visibility.Visible;
            this.replenishmentBorder.Visibility = Visibility.Visible;
            this.AddPayment.Visibility = Visibility.Visible;
        }

        private void UpdateComboBoxes()
        {
            this.PaymentsList.ItemsSource = this.PlannedPayments;
            this.PaymentsList.DisplayMemberPath = "Name";

            var accounts = DbHelper.dbc.Accounts.Where(a => a.UserId == SessionManager.CurrentUserId).ToList();

            this.AccountsList.ItemsSource = accounts;
            this.AccountsList.DisplayMemberPath = "Name";
        }

        private void PaymentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.PaymentsList.SelectedItem is PlannedExpense selectedPayment)
            {
                this.TopUpAmountPayment.Text = selectedPayment.PlannedSum.ToString("F2");
            }
        }

    }
}
