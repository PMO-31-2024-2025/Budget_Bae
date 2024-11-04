using BusinessLogic.Services;
using BusinessLogic.Session;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for AccountsWindow.xaml
    /// </summary>
    public partial class AccountsWindow : Window
    {
        private List<Account> accounts;
        public AccountsWindow()
        {
            InitializeComponent();

            if (SessionManager.CurrentUserId != null)
            {
                accounts = AccountService.GetUsersAccounts();
            }
            else
            {
                accounts = new List<Account>();
            }
            SetAccounts();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow window = new AddAccountWindow();
            this.Close();
            window.ShowDialog();
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        {
            IncomeWindow window = new IncomeWindow();
            this.Close();
            window.ShowDialog();
        }

        private void SetAccounts()
        {
            foreach (var account in accounts)
            {
                Button accountButton = new Button
                {
                    Content = $"{account.Name}",
                    Width = 300,
                    Height = 40,
                    FontSize = 16,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000")),
                    Style = (Style)FindResource("CategoryButton"),
                    Margin = new Thickness(15, 10, 15, 10)
                };
                AccountsPanel.Children.Add(accountButton);
                accountButton.Click += AddIncome_Click;
            }
               
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            foreach (UIElement element in AccountsPanel.Children)
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