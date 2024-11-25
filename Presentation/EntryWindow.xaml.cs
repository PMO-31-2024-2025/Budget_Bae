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
    using Microsoft.Extensions.Logging;

    public partial class EntryWindow : Window
    {
        private static ILogger logger;
        private NavBar navBar;

        public EntryWindow()
        {
            this.InitializeComponent();
        }

        public static void InitializeLogger(ILogger logger)
        {
            EntryWindow.logger = logger;
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
            logger?.LogInformation($"Спроба реєстрації користувача з поштою '{emailInput}'.");


            if (emailInput == string.Empty || nameInput == string.Empty || createPasswordInput == string.Empty || confirmPasswordInput == string.Empty)
            {
                logger?.LogWarning("Усі поля мають бути заповнені!");
                MessageBox.Show("Усі поля мають бути заповнені!");
            }
            else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                logger?.LogWarning("Неправильний формат електронної адреси!");
                MessageBox.Show("Неправильний формат електронної адреси!");
            }
            else if (createPasswordInput != confirmPasswordInput)
            {
                logger?.LogWarning("Паролі мають бути однаковими!");
                MessageBox.Show("Паролі мають бути однаковими!");
            }
            else if (createPasswordInput.Length < 8)
            {
                logger?.LogWarning("Пароль має складатися з 8 символів щонайменше!");
                MessageBox.Show("Пароль має складатися з 8 символів щонайменше.");
            }
            else if (createPasswordInput.Contains(" "))
            {
                logger?.LogWarning("Пароль не може містити пробіли!");
                MessageBox.Show("Пароль не може містити пробіли!");
            }
            else
            {
                try
                {
                    await UserService.RegisterUserAsync(emailInput, createPasswordInput, nameInput);
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
                    await ExpenseCategoryService.AddExpensCategoryAsync("Їжа");
                    await ExpenseCategoryService.AddExpensCategoryAsync("Одяг");
                    await ExpenseCategoryService.AddExpensCategoryAsync("Розваги");
                    await ExpenseCategoryService.AddExpensCategoryAsync("Транспорт");
                    await ExpenseCategoryService.AddExpensCategoryAsync("Здоров'я");

                    this.Close();
                }
                catch (Exception ex)
                {
                    logger?.LogWarning("Помилка!");
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void SettingsEntryButton_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = this.settingsEntryEmailTextBox.Text.ToLower();
            string passwordInput = this.settingsEntryPasswordTextBox.Text;
            logger?.LogInformation($"Спроба увійти в акаунт користувача з поштою '{emailInput}'.");

            if (emailInput == string.Empty || passwordInput == string.Empty)
            {
                logger?.LogWarning("Усі поля мають бути заповнені!");
                MessageBox.Show("Усі поля мають бути заповнені");
            }
            else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                logger?.LogWarning("Неправильний формат електронної адреси!");
                MessageBox.Show("Неправильний формат електронної адреси!");
            }
            else if (passwordInput.Length < 8)
            {
                logger?.LogWarning("Пароль занадто короткий!");
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
                        logger?.LogWarning("Некоректна пошта!");
                        MessageBox.Show("Некоректна пошта");
                    }
                    else if (user.Password != passwordInput)
                    {
                        logger?.LogWarning("Некоректний пароль!");
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
                        logger?.LogInformation($"Користувач {UserService.GetUserNameById(SessionManager.CurrentUserId)} увійшов.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogWarning("Помилка!");
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
