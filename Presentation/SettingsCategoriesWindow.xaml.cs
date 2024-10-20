﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SettingsCategoriesWindow.xaml
    /// </summary>
    public partial class SettingsCategoriesWindow : Window
    {
        public ObservableCollection<string> Сategories { get; set; }

        public SettingsCategoriesWindow()
        {
            InitializeComponent(); 
        }

        public SettingsCategoriesWindow(ObservableCollection<string> categories)
        {
            InitializeComponent();
            Сategories = categories;
            UpdateCategoryGrid();
        }

        private void UpdateCategoryGrid()
        {
            categoryGrid.RowDefinitions.Clear();
            categoryGrid.Children.Clear();

            for (int i = 0; i < Сategories.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                categoryGrid.RowDefinitions.Add(row);

                Label label = new Label();
                label.Content = Сategories[i];
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
                deleteButton.Click += DeleteButton_Click;

                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                categoryGrid.Children.Add(label);

                Grid.SetRow(deleteButton, i);
                Grid.SetColumn(deleteButton, 1);
                categoryGrid.Children.Add(deleteButton);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int categoryIndex = (int)deleteButton.Tag;
            if (categoryIndex >= 0 && categoryIndex < Сategories.Count)
            {
                Сategories.RemoveAt(categoryIndex);
                UpdateCategoryGrid();
            }
        }
    }
}
