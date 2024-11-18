namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for SettingsCategoriesWindow.xaml
    /// </summary>
    public partial class SettingsCategoriesWindow : Window
    {
        private List<string> categories;

        public SettingsCategoriesWindow()
        {
            this.InitializeComponent();
            if (SessionManager.CurrentUserId != null)
            {
                this.categories = new List<string>();
                List<ExpenseCategory> fetchedCategories = ExpenseCategoryService.GetCategories();

                foreach (var category in fetchedCategories)
                {
                    this.categories.Add(category.Name);
                }
            }
            else
            {
                this.categories = ["Їжа", "Одяг", "Розваги", "Транспорт", "Здоров'я"];
            }

            this.UpdateCategoryGrid();
        }


        private void UpdateCategoryGrid()
        {
            this.categoryGrid.RowDefinitions.Clear();
            this.categoryGrid.Children.Clear();

            for (int i = 0; i < this.categories.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                this.categoryGrid.RowDefinitions.Add(row);

                if (this.categories[i] != "Заплановані платежі" && this.categories[i] != "Заощадження")
                {
                    Label label = new Label();
                    label.Content = this.categories[i];
                    label.FontSize = 18;
                    label.Margin = new Thickness(10, 5, 5, 0);

                    Button deleteButton = new Button();
                    deleteButton.Content = "X";
                    deleteButton.Height = 15;
                    deleteButton.Width = 15;
                    deleteButton.Background = Brushes.Transparent;
                    deleteButton.BorderThickness = new Thickness(0);
                    deleteButton.FontSize = 15;
                    deleteButton.Tag = i; // збереження індекса категорії
                    deleteButton.Click += this.DeleteButton_Click;

                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, 0);
                    this.categoryGrid.Children.Add(label);

                    Grid.SetRow(deleteButton, i);
                    Grid.SetColumn(deleteButton, 1);
                    this.categoryGrid.Children.Add(deleteButton);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int categoryIndex = (int)deleteButton.Tag;
            if (categoryIndex >= 0 && categoryIndex < this.categories.Count)
            {
                var currCategories = ExpenseCategoryService.GetCategories();
                var categoryToDeleteId = currCategories.FirstOrDefault(x => x.Name == categories.ElementAt(categoryIndex)).Id;
                this.categories.RemoveAt(categoryIndex);
                ExpenseCategoryService.DeleteExpenseCategory(categoryToDeleteId);
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new MainPage());
                }
                this.UpdateCategoryGrid();
            }
        }
    }
}
