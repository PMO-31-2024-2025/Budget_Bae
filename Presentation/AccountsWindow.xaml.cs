/// <summary>
/// Interaction logic for AccountsWindow.xaml
/// </summary>
namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// AccountsWindow.
    /// </summary>
    public partial class AccountsWindow : Window
    {
        private List<Account> accounts = new List<Account>();

        public AccountsWindow()
        {
            this.InitializeComponent();

            if (SessionManager.CurrentUserId != null)
            {
                this.accounts = AccountService.GetCurrentUserAccounts();
            }
            else
            {
                this.accounts = new List<Account>();
            }

            this.SetAccounts();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow window = new AddAccountWindow();
            this.Close();
            window.ShowDialog();
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Account account)
            {
                IncomeWindow window = new IncomeWindow(account);
                this.Close();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Не вдалося знайти рахунок!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SetAccounts()
        {
            foreach (var account in this.accounts)
            {
                Button accountButton = new Button
                {
                    Content = $"{account.Name}",
                    Width = 300,
                    Height = 40,
                    FontSize = 16,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000")),
                    Style = (Style)this.FindResource("CategoryButton"),
                    Margin = new Thickness(15, 10, 15, 10),
                    DataContext = account,
                };

                this.AccountsPanel.Children.Add(accountButton);
                accountButton.Click += this.AddIncome_Click;
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = this.SearchTextBox.Text.ToLower();
            foreach (UIElement element in this.AccountsPanel.Children)
            {
                if (element is Button accountButton)
                {
                    if (accountButton.Content.ToString().ToLower().Contains(searchText))
                    {
                        accountButton.BringIntoView();
                        return;
                    }
                }
            }
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
    }
}