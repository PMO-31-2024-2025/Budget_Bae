﻿namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for SavingsWindow.xaml
    /// </summary>
    public partial class SavingsWindow : Window
    {
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
        }

        public List<Saving> Savings { get; set; }

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
                    Tag = i,
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int savingIndex = (int)deleteButton.Tag;
            if (savingIndex >= 0 && savingIndex < this.Savings.Count)
            {
                this.Savings.RemoveAt(savingIndex);
                this.UpdateSavingsGrid();
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
            var selectedSavings = SavingsList.SelectedItem as Saving;
            var selectedAccount = AccountsList.SelectedItem as Account;
            int? currentUserId = SessionManager.CurrentUserId;

            if (string.IsNullOrWhiteSpace(topUpAmountInput) || selectedSavings == null || selectedAccount == null)
            {
                MessageBox.Show("Усі поля мають бути заповнені!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(topUpAmountInput, out decimal topUpAmount))
            {
                MessageBox.Show("Поле суми поповнення має містити число!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (selectedAccount.Balance < (double)topUpAmount)
                {
                    MessageBox.Show("Недостатньо коштів на рахунку.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedAccount.Balance -= (double)topUpAmount;
                selectedSavings.CurrentSum += (double)topUpAmount;

                var savingsCategory = DbHelper.dbс.ExpensesCategories
                    .FirstOrDefault(c => c.Name == "Заощадження" && c.UserId == SessionManager.CurrentUserId);

                if (savingsCategory == null)
                {
                    savingsCategory = new ExpenseCategory
                    {
                        Name = "Заощадження",
                        UserId = currentUserId.Value
                    };
                    DbHelper.dbс.ExpensesCategories.Add(savingsCategory);
                    await DbHelper.dbс.SaveChangesAsync();
                }

                var newExpense = new Expense
                {
                    ExpenseSum = (double)topUpAmount,
                    AccountId = selectedAccount.Id,
                    CategoryId = savingsCategory.Id,
                    ExpenseDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                DbHelper.dbс.Expenses.Add(newExpense);

                DbHelper.dbс.Update(selectedAccount);
                DbHelper.dbс.Update(selectedSavings);
                await DbHelper.dbс.SaveChangesAsync();

                this.Savings = SavingService.GetSavings(); 
                UpdateSavingsGrid();

                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
                MessageBox.Show("Поповнення заощадження успішно виконано!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void UpdateSavingsComboBox()
        {
            var savings = DbHelper.dbс.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .ToList();
            SavingsList.ItemsSource = savings;
            SavingsList.DisplayMemberPath = "TargetName"; 
        }

        private void UpdateAccountsComboBox()
        {
            var accounts = DbHelper.dbс.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .ToList();
            AccountsList.ItemsSource = accounts;
            AccountsList.DisplayMemberPath = "Name"; 
        }

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string dateText = this.Date.Text.Trim();
            string amountText = this.Amount_.Text.Trim();

            if (name == "Введіть назву")
            {
                MessageBox.Show("Усі поля повинні бути заповненими!");
            }
            else if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Сума повинна бути додатнім числом!");
            }
            else if (!int.TryParse(dateText, out int date) || date < 1)
            {
                MessageBox.Show("Кількість місяців повинна бути 1 чи більше");
            }
            else
            {
                decimal amountPerMonth = amount / date;
                this.AmountPerMonth.Text = amountPerMonth.ToString("F2");
                MessageBox.Show("Дані пройшли перевірку!");

                var newSaving = new Saving
                {
                    TargetName = name,
                    TargetSum = (double)amount,
                    CurrentSum = 0, 
                    MonthsNumber = date,
                    UserId = SessionManager.CurrentUserId.Value 
                };

                DbHelper.dbс.Savings.Add(newSaving);
                DbHelper.dbс.SaveChanges();

                MessageBox.Show("Заощадження успішно створено!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);

                var newWindow = new SavingsWindow();
                this.Close();
                newWindow.Show();

            }
        }



    }
}
