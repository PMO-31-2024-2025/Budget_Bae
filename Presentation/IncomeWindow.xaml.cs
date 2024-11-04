using DAL.Data;
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
    /// Interaction logic for IncomeWindow.xaml
    /// </summary>
    public partial class IncomeWindow : Window
    {
        List<string> incomeCategories = new List<string>
        {
            "З/П",
            "Стипендія",
            "Кишенькові",
            "Соц. виплати",
            "Премія",
            "Інше"
        };
        public IncomeWindow()
        {
            InitializeComponent();
            UpdateIncomeWindowComboBox();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void incomeAddingIncomeSum_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }
        private async void incomeAddingButton_Click(object sender, RoutedEventArgs e)
        {
            string sum = incomeAddingIncomeSumTextBox.Text;
            var selectedCategory = incomeAddingCategoryChooseCombobox.SelectedItem;

            if (sum == null || selectedCategory == null)
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
            }
            else if (!decimal.TryParse(sum, out _))
            {
                MessageBox.Show("Поле суми поповнення має містити число!");
            }
            else
            {
                try
                {
                    MessageBox.Show("Поповнення успішно!", "Успіх!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void UpdateIncomeWindowComboBox()
        {
            incomeAddingCategoryChooseCombobox.ItemsSource = incomeCategories;
        }
    }
}
