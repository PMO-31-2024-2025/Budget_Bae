﻿namespace Presentation
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
    /// Interaction logic for SavingsWindow.xaml
    /// </summary>
    public partial class SavingsWindow : Window
    {
        private static ILogger logger;

        public SavingsWindow()
        {
            this.InitializeComponent();
            if (SessionManager.CurrentUserId != null)
            {
                this.Savings = SavingService.GetSavings();
            }
            else
            {
                this.Savings = [];
            }

            this.UpdateSavingsGrid();
            this.UpdateSavingsComboBox();
            this.UpdateAccountsComboBox();
            this.SavingsList.SelectionChanged += this.SavingsList_SelectionChanged;
        }

        public List<Saving> Savings { get; set; }

        public static void InitializeLogger(ILogger logger)
        {
            SavingsWindow.logger = logger;
        }

        private void UpdateSavingsGrid()
        {
            this.SavingsStackPannel.Children.Clear();

            for (int i = 0; i < this.Savings.Count; i++)
            {
                Grid savings = new Grid();
                savings.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                savings.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                savings.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(205) });
                savings.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90) });
                savings.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35) });
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
                StackPanel labelsAndButton = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                };
                Label goal = new Label()
                {
                    Content = this.Savings[i].TargetName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5, 5, 10, 5)
                };
                Grid.SetColumn(goal, 0);
                Grid.SetRow(goal, 0);
                savings.Children.Add(goal);
                Label amount = new Label()
                {
                    Content = $"{this.Savings[i].TargetSum} UAH",
                    HorizontalAlignment = HorizontalAlignment.Right,
                    FontSize = 12,
                    Margin = new Thickness(5)
                };
                Grid.SetColumn(amount, 1);
                Grid.SetRow(amount, 0);
                savings.Children.Add(amount);
                Button delete = new Button()
                {
                    Style = (Style)this.FindResource("CloseButton"),
                    Width = 15,
                    Height = 15,
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Tag = this.Savings[i].Id,
                    Margin = new Thickness(10, 5, 10, 10)
                };
                delete.Click += this.Delete_Click;
                Grid.SetRow(delete, 0);
                Grid.SetColumn(delete, 2);
                savings.Children.Add(delete);

                ProgressBar progressBar = new ProgressBar()
                {
                    Minimum = 0,
                    Maximum = this.Savings[i].TargetSum,
                    Value = this.Savings[i].CurrentSum,
                    Height = 15,
                    Margin = new Thickness(15, 5, 10, 15),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0")),
                };

                Grid.SetRow(progressBar, 1);
                savings.Children.Add(progressBar);

                border.Child = savings;
                this.SavingsStackPannel.Children.Add(border);
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            logger?.LogInformation($"Спроба видалити заощадження '{SavingService.GetSavingName(Convert.ToInt32(deleteButton.Tag))}'.");

            if (deleteButton == null || !(deleteButton.Tag is int savingId))
            {
                logger?.LogWarning("Неможливо знайти заощадження для видалення!");
                MessageBox.Show("Неможливо знайти заощадження для видалення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                bool result = await SavingService.DeleteSavingAsync(savingId);
                if (result)
                {
                    var newWindow = new SavingsWindow();
                    this.Close();
                    newWindow.Show();
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning("Помилка!");
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddSavings_Click(object sender, RoutedEventArgs e)
        {
            this.replenishment.Visibility = Visibility.Collapsed;
            this.replenishmentBorder.Visibility = Visibility.Collapsed;
            this.adding.Visibility = Visibility.Visible;
            this.addingBorder.Visibility = Visibility.Visible;
            this.AddSavings.Visibility = Visibility.Collapsed;
        }

        private void CloseAddSavings_Click(object sender, RoutedEventArgs e)
        {
            this.adding.Visibility = Visibility.Collapsed;
            this.addingBorder.Visibility = Visibility.Collapsed;
            this.replenishment.Visibility = Visibility.Visible;
            this.replenishmentBorder.Visibility = Visibility.Visible;
            this.AddSavings.Visibility = Visibility.Visible;
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

        private async void TopUpSavings_Click(object sender, RoutedEventArgs e)
        {
            string topUpAmountInput = this.TopUpAmountSavings.Text;
            var selectedSavings = this.SavingsList.SelectedItem as Saving;
            var selectedAccount = this.AccountsList.SelectedItem as Account;
            int? currentUserId = SessionManager.CurrentUserId;
            var categoryName = "Заощадження";
            logger?.LogInformation($"Спроба поповнити заощадження '{selectedSavings.TargetName}'.");

            if (string.IsNullOrWhiteSpace(topUpAmountInput) || selectedSavings == null || selectedAccount == null)
            {
                logger.LogWarning("Усі поля мають бути заповнені!");
                MessageBox.Show("Усі поля мають бути заповнені!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(topUpAmountInput, out decimal topUpAmount))
            {
                logger.LogWarning("Поле суми поповнення має містити число!");
                MessageBox.Show("Поле суми поповнення має містити число!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (selectedAccount.Balance < (double)topUpAmount)
                {
                    logger.LogWarning("Недостатньо коштів на рахунку!");
                    MessageBox.Show("Недостатньо коштів на рахунку.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedSavings.CurrentSum += (double)topUpAmount;

                var category = DbHelper.dbc.ExpensesCategories
                    .FirstOrDefault(c => c.Name == categoryName && c.UserId == currentUserId);

                if (category == null)
                {
                    bool categoryAdded = await ExpenseCategoryService.AddExpensCategoryAsync(categoryName);
                    if (!categoryAdded)
                    {
                        MessageBox.Show("Не вдалося створити категорію!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    category = DbHelper.dbc.ExpensesCategories
                        .FirstOrDefault(c => c.Name == categoryName && c.UserId == currentUserId);
                }

                bool result = await ExpenseService.AddExpenseAsync(category.Id, (double)topUpAmount, selectedAccount.Id);

                await DbHelper.dbc.SaveChangesAsync();
                this.UpdateSavingsGrid();
                logger?.LogInformation($"Заощадження поповнено на {topUpAmount} UAH.");
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
                logger.LogWarning("Помилка!");
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateSavingsComboBox()
        {
            var savings = DbHelper.dbc.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .ToList();
            this.SavingsList.ItemsSource = savings;
            this.SavingsList.DisplayMemberPath = "TargetName";
        }

        private void UpdateAccountsComboBox()
        {
            var accounts = DbHelper.dbc.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .ToList();
            this.AccountsList.ItemsSource = accounts;
            this.AccountsList.DisplayMemberPath = "Name";
        }

        private async void TopUp_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string dateText = this.Date.Text.Trim();
            string amountText = this.Amount_.Text.Trim();

            logger?.LogInformation($"Спроба створити заощадження '{name}'.");

            if (string.IsNullOrWhiteSpace(name) || name == "Введіть назву")
            {
                logger?.LogWarning("Усі поля повинні бути заповненими!");
                MessageBox.Show("Усі поля повинні бути заповненими!");
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                logger?.LogWarning("Сума повинна бути додатнім числом!");
                MessageBox.Show("Сума повинна бути додатнім числом!");
                return;
            }

            if (!int.TryParse(dateText, out int date) || date < 1)
            {
                logger?.LogWarning("Кількість місяців повинна бути числом, яке рівне 1 чи більше!");
                MessageBox.Show("Кількість місяців повинна бути числом, яке рівне 1 чи більше!");
                return;
            }

            decimal amountPerMonth = amount / date;
            this.AmountPerMonth.Text = amountPerMonth.ToString("F2");

            try
            {
                bool result = await SavingService.AddSavingAsync(name, (int)amount, date);
                if (result)
                {
                    var newWindow = new SavingsWindow();
                    this.Close();
                    newWindow.Show();
                }
            }
            catch (Exception ex)
            {
                logger?.LogWarning("Помилка!");
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void CalculateMonthlyAmount(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(this.Amount_.Text, out decimal amount) &&
                int.TryParse(this.Date.Text, out int months) &&
                months > 0)
            {
                decimal amountPerMonth = amount / months;
                this.AmountPerMonth.Text = amountPerMonth.ToString("F2");
            }
        }

        private void SavingsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SavingsList.SelectedItem is Saving selectedSaving)
            {
                this.TopUpAmountSavings.Text = (selectedSaving.TargetSum / selectedSaving.MonthsNumber).ToString("F2");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SavingsList.Focus();
            this.SavingsList.SelectedIndex = 0;
            this.AccountsList.SelectedIndex = 0;

        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void SavingsList_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.AccountsList.Focus();
            }
        }

        private void AccountsList_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.TopUpAmountSavings.Focus();
            }
        }

        private void TopUpAmountSavings_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.TopUpSavings_Click(sender, e);
            }
        }
    }
}
