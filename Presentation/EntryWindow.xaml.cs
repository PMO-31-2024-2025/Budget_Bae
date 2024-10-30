using BusinessLogic.Services;
using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EntryWindow.xaml
    /// </summary>
    public partial class EntryWindow : Window
    {
        NavBar navBar;

        public EntryWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void settingsEntryEmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string emailInput = settingsRegistrationEmailTextBox.Text;

            if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }

        private void settingsRegistrationEmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string emailInput = settingsRegistrationEmailTextBox.Text;
            
            if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Неправильний формат електронної адреси!");
            }
            else
            {
                settingsRegistrationEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Green);
            }
            
        }

        private async void settingsRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = settingsRegistrationEmailTextBox.Text;
            string nameInput = settingsRegistrationNameTextBox.Text;
            string createPasswordInput = settingsRegistrationCreatePasswordTextBox.Text;
            string confirmPasswordInput = settingsRegistrationConfirmPasswordTextBox.Text;

            if (emailInput == "" || nameInput == "" || createPasswordInput == "" || confirmPasswordInput == "")
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
            }
            else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                settingsRegistrationEmailTextBox_LostFocus(sender, e);
            }
            else if (createPasswordInput != confirmPasswordInput)
            {
                MessageBox.Show("Паролі мають бути однаковими!");
            }
            else if (createPasswordInput.Length < 8)
            {
                MessageBox.Show("Пароль має складатися з 8 символів щонайменше.");
            }
            else
            {
                try
                {
                    var userService = new UserService(new DAL.Data.BudgetBaeContext());
                    userService.RegisterUser(emailInput, createPasswordInput, nameInput);
                    MessageBox.Show("Реєстрація успішна!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);

                    SessionManager.SetCurrentUser(DbHelper.db.Users.FirstOrDefault(x => x.Email == emailInput).Id);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void settingsEntryButton_Click(object sender, RoutedEventArgs e)
        {
            string emailInput = settingsEntryEmailTextBox.Text;
            string passwordInput = settingsEntryPasswordTextBox.Text;

            if (emailInput == "" && passwordInput == "")
            {
                MessageBox.Show("Усі поля мають бути заповнені");
            }
            else if (!Regex.IsMatch(emailInput, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                settingsEntryEmailTextBox_LostFocus(sender, e);
            }
            else if (passwordInput.Length < 8)
            {
                MessageBox.Show("Пароль занадто короткий.");
                settingsEntryPasswordTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                try
                {
                    //int userId = DbHelper.db.Users.FirstOrDefault(x => x.Email == emailInput).Id;
                    User user = DbHelper.db.Users.FirstOrDefault(x => x.Email == emailInput);
                    if (user.Id == -1)
                    {
                        //MessageBox.Show("Ая, думав отак влізеш в систему якшо не зареєстрований?");
                        throw new Exception("Ая, думав отак влізеш в систему якшо не зареєстрований?");
                    }
                    if (user.Password != passwordInput)
                    {
                        MessageBox.Show("Нє, пароль не той. Сліпаки роззуй і введи всьо правильно.");
                    }
                    else
                    {
                        MessageBox.Show($"Вітаємо, {DbHelper.db.Users.First(x => x.Id == user.Id).Name}!");
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        // HERE IS THE USERS ID!!!!!!!!!!!!!!!
                        SessionManager.SetCurrentUser(DbHelper.db.Users.First(x => x.Id == user.Id).Id);
                        //settingsEntryEmailTextBox.Text = SessionManager.CurrentUserId.ToString();

                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        if (mainWindow != null && mainWindow.MainFrame != null)
                        {
                            navBar = mainWindow.NavBar;
                            navBar.nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
                            navBar.nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
                            mainWindow.MainFrame.Navigate(new MainPage());
                        }

                        Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
