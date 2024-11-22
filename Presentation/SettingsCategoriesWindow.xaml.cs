namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;

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
            int count = 0;

            for (int i = 0; i < this.categories.Count; i++)
            {
                if (this.categories[i] != "Заплановані платежі" && this.categories[i] != "Заощадження")
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(35);
                    this.categoryGrid.RowDefinitions.Add(row);

                    Label label = new Label();
                    label.Content = this.categories[i];
                    label.FontSize = 18;
                    label.Margin = new Thickness(10, 5, 5, 0);

                    Button deleteButton = new Button();
                    deleteButton.Style = (Style)this.FindResource("DeleteCategoryOrAccountButton");
                    deleteButton.Tag = i; // збереження індекса категорії
                    deleteButton.Click += this.DeleteButton_Click;

                    Grid.SetRow(label, i);
                    Grid.SetColumn(label, 0);
                    this.categoryGrid.Children.Add(label);

                    Grid.SetRow(deleteButton, i);
                    Grid.SetColumn(deleteButton, 1);
                    this.categoryGrid.Children.Add(deleteButton);
                    count++;
                }
            }
            this.Height = (count * 35) + 20;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int categoryIndex = (int)deleteButton.Tag;
            if (categoryIndex >= 0 && categoryIndex < this.categories.Count)
            {
                var currCategories = ExpenseCategoryService.GetCategories();
                var categoryToDeleteId = currCategories.FirstOrDefault(x => x.Name == this.categories.ElementAt(categoryIndex)).Id;
                this.categories.RemoveAt(categoryIndex);
                _ = ExpenseCategoryService.DeleteExpenseCategory(categoryToDeleteId);
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
