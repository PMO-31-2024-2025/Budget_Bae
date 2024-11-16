namespace Presentation
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;

    /// <summary>
    /// Interaction logic for CategoriesWindow.xaml.
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        private List<string> categories;

        public CategoriesWindow()
        {
            this.InitializeComponent();

            this.categories = new List<string>();
            if (SessionManager.CurrentUserId != null)
            {
                List<ExpenseCategory> fetchedCategories = ExpenseCategoryService.GetCategories();

                foreach (var category in fetchedCategories)
                {
                    this.categories.Add(category.Name);
                }
            }
            else
            {
                this.categories = new List<string> { "Їжа", "Одяг", "Розваги", "Транспорт", "Здоров'я" };
            }

            this.SetCategories();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow window = new AddCategoryWindow();
            this.Close();
            window.ShowDialog();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = this.SearchTextBox.Text.ToLower();
            foreach (UIElement element in this.CategoriesPanel.Children)
            {
                if (element is StackPanel innerPanel)
                {
                    foreach (UIElement innerElement in innerPanel.Children)
                    {
                        if (innerElement is Button categoryButton)
                        {
                            if (categoryButton.Content.ToString().ToLower().Contains(searchText))
                            {
                                categoryButton.BringIntoView();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button categoryButton)
            {
                var categoryName = categoryButton.Content.ToString();
                var category = DbHelper.db.ExpensesCategories
                    .FirstOrDefault(c => c.Name == categoryName && c.UserId == SessionManager.CurrentUserId);

                if (category != null)
                {
                    ExpensesWindow window = new ExpensesWindow(category.Id);
                    this.Close(); // Закриваємо CategoriesWindow
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Категорія не знайдена!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = textBox.Tag.ToString();
                }
            }
        }

        private void SetCategories()
        {
            // Очищуємо панель перед додаванням категорій
            CategoriesPanel.Children.Clear();

            var categories = DbHelper.db.ExpensesCategories
                .Where(c => c.UserId == SessionManager.CurrentUserId)
                .ToList();

            foreach (var category in categories)
            {
                Button categoryButton = new Button
                {
                    Content = category.Name, // Відображаємо назву категорії
                    Width = 150,
                    Height = 40,
                    FontSize = 16,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000")),
                    Margin = new Thickness(5),
                };

                // Прив'язка події
                categoryButton.Click += AddExpense_Click;

                CategoriesPanel.Children.Add(categoryButton);
            }
        }


    }
}
