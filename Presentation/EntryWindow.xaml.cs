/// <summary>
/// Interaction logic for EntryWindow.xaml
/// </summary>
namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public partial class EntryWindow : Window
    {
        private NavBar navBar;

        public EntryWindow()
        {
            this.InitializeComponent();
        }

        private void SettingsEntryEmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // string emailInput = settingsEntryEmailTextBox.Text.ToLower();
            // if (emailInput == "")
            // {
            //     MessageBox.Show("Поле з адресою має бути заповненим!");
            // }
            // else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            // {
            //     settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            //     MessageBox.Show("Неправильний формат електронної адреси!");
            // }
            // else
            // {
            //     settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Green);
            // }
        }

        private void SettingsRegistrationEmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // string emailInput = settingsRegistrationEmailTextBox.Text.ToLower();
            // if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            // {
            //     settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            //     MessageBox.Show("Неправильний формат електронної адреси!");
            // }
            // else
            // {
            //     settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Green);
            // }
        }

        private async void SettingsRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = this.settingsRegistrationEmailTextBox.Text.ToLower();
            string nameInput = this.settingsRegistrationNameTextBox.Text;
            string createPasswordInput = this.settingsRegistrationCreatePasswordTextBox.Text;
            string confirmPasswordInput = this.settingsRegistrationConfirmPasswordTextBox.Text;

            if (emailInput == string.Empty || nameInput == string.Empty || createPasswordInput == string.Empty || confirmPasswordInput == string.Empty)
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
            }
            else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Неправильний формат електронної адреси!");
            }
            else if (createPasswordInput != confirmPasswordInput)
            {
                MessageBox.Show("Паролі мають бути однаковими!");
            }
            else if (createPasswordInput.Length < 8)
            {
                MessageBox.Show("Пароль має складатися з 8 символів щонайменше.");
            }
            else if (createPasswordInput.Contains(" "))
            {
                MessageBox.Show("Пароль не може містити пробіли!");
            }
            else
            {
                try
                {
                    await UserService.RegisterUserAsync(emailInput, createPasswordInput, nameInput);
                    MessageBox.Show("Реєстрація успішна!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);
                    SessionManager.SetCurrentUser(UserService.GetUserIdByEmail(emailInput));

                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    if (mainWindow != null && mainWindow.MainFrame != null)
                    {
                        this.navBar = mainWindow.NavBar;
                        this.navBar.nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
                        this.navBar.nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
                        mainWindow.MainFrame.Navigate(new MainPage());
                    }
                    this.navBar.nbEntryButton.Content = DbHelper.dbc.Users.FirstOrDefault(x => x.Email == emailInput).Name;
                    await ExpenseCategoryService.AddExpenseAsync("Їжа");
                    await ExpenseCategoryService.AddExpenseAsync("Одяг");
                    await ExpenseCategoryService.AddExpenseAsync("Розваги");
                    await ExpenseCategoryService.AddExpenseAsync("Транспорт");
                    await ExpenseCategoryService.AddExpenseAsync("Здоров'я");

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void SettingsEntryButton_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = this.settingsEntryEmailTextBox.Text.ToLower();
            string passwordInput = this.settingsEntryPasswordTextBox.Text;

            if (emailInput == string.Empty || passwordInput == string.Empty)
            {
                MessageBox.Show("Усі поля мають бути заповнені");
            }
            else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Неправильний формат електронної адреси!");
            }
            else if (passwordInput.Length < 8)
            {
                MessageBox.Show("Пароль занадто короткий.");
                this.settingsEntryPasswordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                try
                {
                    // int userId = DbHelper.db.Users.FirstOrDefault(x => x.Email == emailInput).Id;
                    User user = DbHelper.dbc.Users.FirstOrDefault(x => x.Email == emailInput);
                    if (user == null)
                    {
                        MessageBox.Show("Некоректна пошта");
                    }
                    else if (user.Password != passwordInput)
                    {
                        MessageBox.Show("Некоректний пароль");
                    }
                    else
                    {
                        // MessageBox.Show($"Вітаємо, {DbHelper.db.Users.First(x => x.Id == user.Id).Name}!");
                        // SessionManager.SetCurrentUser(DbHelper.db.Users.First(x => x.Id == user.Id).Id);
                        // settingsEntryEmailTextBox.Text = SessionManager.CurrentUserId.ToString();
                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        if (mainWindow != null && mainWindow.MainFrame != null)
                        {
                            this.navBar = mainWindow.NavBar;
                            this.navBar.nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
                            this.navBar.nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
                            mainWindow.MainFrame.Navigate(new MainPage());
                        }

                        this.navBar.nbEntryButton.Content = DbHelper.dbc.Users.First(x => x.Id == user.Id).Name;
                        SessionManager.SetCurrentUser(DbHelper.dbc.Users.First(x => x.Id == user.Id).Id);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.settingsEntryEmailTextBox.Focus();
        }

        private void SettingsEntryEmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.settingsEntryPasswordTextBox.Focus();
            }
        }

        private void SettingsEntryPasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SettingsEntryButton_Click(sender, e);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsRegistrationEmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.settingsRegistrationNameTextBox.Focus();
            }
        }

        private void SettingsRegistrationNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.settingsRegistrationCreatePasswordTextBox.Focus();
            }
        }

        private void SettingsRegistrationCreatePasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.settingsRegistrationConfirmPasswordTextBox.Focus();
            }
        }

        private void SettingsRegistrationConfirmPasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.SettingsRegistrationButton_Click(sender, e);
            }
        }
    }
}
