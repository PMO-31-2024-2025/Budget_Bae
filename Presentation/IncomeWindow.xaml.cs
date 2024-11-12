namespace Presentation
{
    using System.Windows;

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
            this.InitializeComponent();
            this.UpdateIncomeWindowComboBox();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void incomeAddingIncomeSum_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        private async void incomeAddingButton_Click(object sender, RoutedEventArgs e)
        {
            string sum = this.incomeAddingIncomeSumTextBox.Text;
            var selectedCategory = this.incomeAddingCategoryChooseCombobox.SelectedItem;

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
            this.incomeAddingCategoryChooseCombobox.ItemsSource = this.incomeCategories;
        }
    }
}
