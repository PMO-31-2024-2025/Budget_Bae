namespace Presentation
{
    using BusinessLogic.Services;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml.
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            this.InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text == textBox.Tag.ToString())
                {
                    textBox.Text = string.Empty;
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // if (sender is TextBox textBox)
            // {
            //     if (string.IsNullOrWhiteSpace(textBox.Text))
            //     {
            //         textBox.Text = textBox.Tag.ToString();
            //     }
            // }
        }

        private async void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string categoryInput = this.AddCategoryName.Text;
                if (categoryInput.ToLower() == "Введіть назву категорії")
                {
                    MessageBox.Show("Заповніть поле з назвою!");
                }
                else
                {
                    await ExpenseCategoryService.AddExpensCategoryAsync(categoryInput);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.AddCategoryName.Focus();
        }

        private void AddCategoryName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.AddCategoryButton_Click(sender, e);
            }
            else if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }
    }
}
