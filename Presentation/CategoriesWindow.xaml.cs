using BusinessLogic.Services;
using BusinessLogic.Session;
using DAL.Models;
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
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        private List<string> categories;

        public CategoriesWindow()
        {
            InitializeComponent();

            categories = new List<string>();
            if (SessionManager.CurrentUserId != null)
            {
                List<ExpenseCategory> fetchedCategories = ExpenseCategoryService.GetCategories();

                foreach (var category in fetchedCategories)
                {
                    categories.Add(category.Name);
                }
            }
            else
            {
                categories = new List<string> { "Їжа", "Одяг", "Розваги", "Транспорт", "Здоров'я" };
            }

            SetCategories();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow window = new AddCategoryWindow();
            this.Close();
            window.ShowDialog();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            foreach (UIElement element in CategoriesPanel.Children)
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

            for (int i = 0; i < categories.Count; i++)
            {
                if (i % 2 == 0)
                {
                    horizontalPanel = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Margin = new Thickness(25, i == 0 ? 5 : 10, 30, 0)
                    };
                    CategoriesPanel.Children.Add(horizontalPanel);
                }

                Button categoryButton = new Button
                {
                    Content = categories[i],
                    Width = 150,
                    Height = 40,
                    FontSize = 16,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CC000000")),
                    Style = (Style)FindResource("CategoryButton"),
                    Margin = new Thickness(i % 2 == 0 ? 0 : 30, 0, 0, 0),
                    HorizontalAlignment = i % 2 == 0 ? HorizontalAlignment.Left : HorizontalAlignment.Right
                };

                categoryButton.Click += AddExpense_Click;

                horizontalPanel.Children.Add(categoryButton);
            }
        }
        
    }
}
