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
        public IncomeWindow()
        {
            InitializeComponent();
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
            string sumInput = incomeAddingIncomeSumTextBox.Text;
            string accountSelection = incomeAddingCategoryChooseCombobox.SelectedValue.ToString();
            foreach (var item in DbHelper.db.Accounts.ToList())
            {
                incomeAddingCategoryChooseCombobox.Items.Add(item);
            }

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
    }
}
