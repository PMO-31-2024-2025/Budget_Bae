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
            StackPanel horizontalPanel = null;

            for (int i = 0; i < accounts.Count; i++)
            {
                if (i % 2 == 0)
                {
                    horizontalPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(25, i == 0 ? 5 : 10, 30, 0)
                    };
                    AccountsPanel.Children.Add(horizontalPanel);
                }

                Button accountButton = new Button
                {
                    Content = $"{accounts[i].Name}/n{accounts[i].Balance} UAH",
                    Width = 150,
                    Height = 40,
                    FontSize = 16,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000")),
                    Style = (Style)FindResource("CategoryButton"),
                    Margin = new Thickness(i % 2 == 0 ? 0 : 30, 0, 0, 0),
                    HorizontalAlignment = i % 2 == 0 ? HorizontalAlignment.Left : HorizontalAlignment.Right
                };

                accountButton.Click += AddAccountButton_Click;

                horizontalPanel.Children.Add(accountButton);
            }
        }

    }
}
