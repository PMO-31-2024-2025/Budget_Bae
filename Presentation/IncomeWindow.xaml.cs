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
            string sumInput = incomeAddingIncomeSumTextBox.Text;

            if (sumInput == "")
            {
                MessageBox.Show("Вкажіть суму витрати!");
            }
            else if (sumInput.Contains("-"))
            {
                MessageBox.Show("Сума витрати повинна бути додатньою!");
            }
            else if (!int.TryParse(sumInput, out int result))
            {
                MessageBox.Show("Сума витрати повинна бути числом!");
            }
            else
            {
                MessageBox.Show("Всьо ґуд, дані пройшли перевірку!");
            }
        }

        private void incomeAddingButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(incomeAddingIncomeSumTextBox.Text))
            {
                MessageBox.Show("Вкажіть суму витрати!");
            }
            else if (!decimal.TryParse(incomeAddingIncomeSumTextBox.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Сума витрати повинна бути додатньою та числом!");
            }
            else if (incomeAddingCategoryChooseCombobox.SelectedValue == null)
            {
                MessageBox.Show("Виберіть категорію витрати!");
            }
            else
            {
                MessageBox.Show("Всьо ґуд, дані пройшли перевірку!");
            }
        }


        private void UpdateIncomeWindowComboBox()
        {
            incomeAddingCategoryChooseCombobox.ItemsSource = incomeCategories;
        }
    }
}
