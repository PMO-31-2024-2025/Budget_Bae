namespace Presentation
{
    using BusinessLogic.Services;
    using DAL.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SettingsAccountsWindow.xaml
    /// </summary>
    public partial class SettingsAccountsWindow : Window
    {
        private List<string> accounts;

        public SettingsAccountsWindow()
        {
            this.InitializeComponent();
            this.accounts = new List<string>();
            List<Account> fetchedAccounts = AccountService.GetCurrentUserAccounts();
            foreach (var account in fetchedAccounts)
            {
                this.accounts.Add(account.Name);
            }
            this.UpdateAccountGrid();
        }

        private void UpdateAccountGrid()
        {
            this.accountGrid.RowDefinitions.Clear();
            this.accountGrid.Children.Clear();
            for (int i = 0; i < this.accounts.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(35);
                this.accountGrid.RowDefinitions.Add(row);
                Label label = new Label();
                label.Content = this.accounts[i];
                label.FontSize = 18;
                label.Margin = new Thickness(10, 5, 5, 0);

                Button deleteButton = new Button();
                deleteButton.Style = (Style)this.FindResource("DeleteCategoryOrAccountButton");
                deleteButton.Tag = i;
                deleteButton.Click += this.DeleteButton_Click;

                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                this.accountGrid.Children.Add(label);

                Grid.SetRow(deleteButton, i);
                Grid.SetColumn(deleteButton, 1);
                this.accountGrid.Children.Add(deleteButton);
            }
            this.Height = (this.accounts.Count * 35) + 20;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int accountIndex = (int)deleteButton.Tag;
            if (accountIndex >= 0 && accountIndex < this.accounts.Count)
            {
                var currAccounts = AccountService.GetCurrentUserAccounts();
                var accountToDeleteId = currAccounts.FirstOrDefault(x => x.Name == this.accounts.ElementAt(accountIndex)).Id;
                this.accounts.RemoveAt(accountIndex);
                _ = AccountService.DeleteAccountAsync(accountToDeleteId);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
                this.UpdateAccountGrid();
            }
        }
    }
}
