﻿namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;

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
            List<ExpenseCategory> fetchedCategories = ExpenseCategoryService.GetCategories();

            foreach (var category in fetchedCategories)
            {
                this.categories.Add(category.Name);
            }

            // if (SessionManager.CurrentUserId != null)
            // {
            //     List<ExpenseCategory> fetchedCategories = ExpenseCategoryService.GetCategories();
            //
            //     foreach (var category in fetchedCategories)
            //     {
            //         this.categories.Add(category.Name);
            //     }
            // }
            // else
            // {
            //     this.categories = new List<string> { "Їжа", "Одяг", "Розваги", "Транспорт", "Здоров'я" };
            // }
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
                var category = DbHelper.dbc.ExpensesCategories
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
            StackPanel horizontalPanel = null;

            for (int i = 0; i < this.categories.Count; i++)
            {
                if (this.categories[i] != "Заплановані платежі" && this.categories[i] != "Заощадження")
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
                        Style = (Style)this.FindResource("AllCaegoriesButton"),
                        Margin = new Thickness(i % 2 == 0 ? 0 : 30, 0, 0, 0),
                        HorizontalAlignment = i % 2 == 0 ? HorizontalAlignment.Left : HorizontalAlignment.Right
                    };

                    categoryButton.Click += this.AddExpense_Click;

                    horizontalPanel.Children.Add(categoryButton);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SearchTextBox.Focus();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                this.Close();
            }
        }

        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.SearchButton_Click(sender, e);
            }
    }
    }
}
