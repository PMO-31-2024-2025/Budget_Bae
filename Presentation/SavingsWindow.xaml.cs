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
using System.ComponentModel;
using DAL.Models;
using BusinessLogic.Services;
using BusinessLogic.Session;
using Microsoft.VisualBasic;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for SavingsWindow.xaml
    /// </summary>
    public partial class SavingsWindow : Window
    {
        public List<Saving> Savings { get; set; }

        public SavingsWindow()
        {
            InitializeComponent();
            if (SessionManager.CurrentUserId != null)
            {
                Savings = SavingService.GetSavings();
            }
            else
            {
                Savings = [];
            }

            UpdateSavingsGrid();
            UpdateSavingsComboBox();
        }

        private void UpdateSavingsGrid() 
        {
            SavingsStackPannel.Children.Clear();

            for(int i = 0; i < Savings.Count; i++)
            {
                Grid savings = new Grid();
                savings.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                savings.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(35) });
                savings.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(205) });
                savings.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(90) });
                savings.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35) });
                Border border = new Border()
                {
                    BorderThickness = new Thickness(2),
                    BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFromString("#DAB6FC")),
                    Background = (SolidColorBrush)(new BrushConverter().ConvertFromString("#4DDAB6FC")),
                    CornerRadius = new CornerRadius(15),
                    Margin = new Thickness(10, 5, 5, 5),
                    Width = 330,
                    Height = 70,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                StackPanel labelsAndButton = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                };
                Label goal = new Label()
                {
                    Content = Savings[i].TargetName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5, 5, 10, 5)
                };
                Grid.SetColumn(goal, 0);
                Grid.SetRow(goal, 0);
                savings.Children.Add(goal);
                Label amount = new Label()
                {
                    Content = $"{Savings[i].TargetSum} UAH",
                    HorizontalAlignment = HorizontalAlignment.Right,
                    FontSize = 12,
                    Margin = new Thickness(5)
                };
                Grid.SetColumn(amount, 1);
                Grid.SetRow(amount, 0);
                savings.Children.Add(amount);
                Button delete = new Button()
                {
                    Style = (Style)FindResource("CloseButton"),
                    Width = 15,
                    Height = 15,
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Tag = i,
                    Margin = new Thickness(10, 5, 10, 10)
                };
                delete.Click += Delete_Click;
                Grid.SetRow(delete, 0);
                Grid.SetColumn(delete, 2);
                savings.Children.Add(delete);

                ProgressBar progressBar = new ProgressBar()
                {
                    Minimum = 0,
                    Maximum = Savings[i].TargetSum,
                    Value = Savings[i].CurrentSum,
                    Height = 15,
                    Margin = new Thickness(15, 5, 10, 15),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0")),
                };

                Grid.SetRow(progressBar, 1);
                savings.Children.Add(progressBar);

                border.Child = savings;
                SavingsStackPannel.Children.Add(border);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int savingIndex = (int)deleteButton.Tag;
            if (savingIndex >= 0 && savingIndex < Savings.Count)
            {
                Savings.RemoveAt(savingIndex);
                UpdateSavingsGrid();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddSavings_Click(object sender, RoutedEventArgs e)
        {
            replenishment.Visibility = Visibility.Collapsed;
            replenishmentBorder.Visibility = Visibility.Collapsed;
            adding.Visibility = Visibility.Visible;
            addingBorder.Visibility = Visibility.Visible;
            AddSavings.Visibility = Visibility.Collapsed;
        }

        private void closeAddSavings_Click(object sender, RoutedEventArgs e)
        {
            adding.Visibility = Visibility.Collapsed;
            addingBorder.Visibility = Visibility.Collapsed;
            replenishment.Visibility = Visibility.Visible;
            replenishmentBorder.Visibility= Visibility.Visible;
            AddSavings.Visibility = Visibility.Visible;
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

        private async void TopUpSavings_Click(object sender, RoutedEventArgs e)
        {
            string topUpAmountInput = TopUpAmountSavings.Text;
            var selectedSavings = SavingsList.SelectedItem;

            if (string.IsNullOrWhiteSpace(TopUpAmountSavings.Text) || selectedSavings == null )
            {
                MessageBox.Show("Усі поля мають бути заповнені!");
            }
            else if (!decimal.TryParse(topUpAmountInput, out _))
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

        private void UpdateSavingsComboBox()
        {
            List<string> SavingsName = new List<string>();

            foreach (var saving in Savings)
            {
                SavingsName.Add(saving.TargetName); 
            }
            SavingsList.ItemsSource = SavingsName;
        }

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text.Trim();
            string dateText = Date.Text.Trim();
            string amountText = Amount_.Text.Trim();

            if (name == "Введіть назву")
            {
                MessageBox.Show("Усі поля повинні бути заповненими!");
            }
            else if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Сума повинна бути додатнім числом!");
            }
            else if (!int.TryParse(dateText, out int date) || date < 1 )
            {
                MessageBox.Show("Кількість місяців повинна бути 1 чи більше");
            }
            else
            {
                decimal amountPerMonth = amount / date;
                AmountPerMonth.Text = amountPerMonth.ToString("F2"); 
                MessageBox.Show("Дані пройшли перевірку!");
            }
        }



    }
}
