namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for CategoriesWindow.xaml
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
            ExpensesWindow window = new ExpensesWindow();
            this.Close();
            window.ShowDialog();
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
            StackPanel horizontalPanel = null;

            for (int i = 0; i < this.categories.Count; i++)
            {
                if (i % 2 == 0)
                {
                    horizontalPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(25, i == 0 ? 5 : 10, 30, 0)
                    };
                    this.CategoriesPanel.Children.Add(horizontalPanel);
                }

                Button categoryButton = new Button
                {
                    Content = this.categories[i],
                    Width = 150,
                    Height = 40,
                    FontSize = 16,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000")),
                    Style = (Style)this.FindResource("CategoryButton"),
                    Margin = new Thickness(i % 2 == 0 ? 0 : 30, 0, 0, 0),
                    HorizontalAlignment = i % 2 == 0 ? HorizontalAlignment.Left : HorizontalAlignment.Right
                };

                categoryButton.Click += this.AddExpense_Click;

                horizontalPanel.Children.Add(categoryButton);
            }
        }

    }
}
