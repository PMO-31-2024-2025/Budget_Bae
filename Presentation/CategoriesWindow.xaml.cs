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
        public CategoriesWindow()
        {
            InitializeComponent();
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
    }
}
