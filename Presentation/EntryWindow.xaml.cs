using BusinessLogic.Services;
using System;
using System.Collections.Generic;
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
            var textBox = sender as TextBox;
            if(textBox != null)
            {
                var bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
                bindingExpression?.UpdateSource();
                settingsEntryEmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
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
            else
            {
                try
                {
                    var userService = new UserService(new DAL.Data.BudgetBaeContext());
                    await userService.RegisterUserAsync(emailInput, nameInput, createPasswordInput);
                    MessageBox.Show("Реєстрація успішна!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
    }
}
