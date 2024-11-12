using BusinessLogic.Services;
using BusinessLogic.Session;
using DAL.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            this.InitializeComponent();
            if (SessionManager.CurrentUserId != null)
            {
                this.Savings = SavingService.GetSavings();
            }
            else
            {
                this.Savings = [];
            }

            this.UpdateSavingsGrid();
            this.UpdateSavingsComboBox();
        }

        private void UpdateSavingsGrid()
        {
            this.SavingsStackPannel.Children.Clear();

            for (int i = 0; i < this.Savings.Count; i++)
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
                    Content = this.Savings[i].TargetName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5, 5, 10, 5)
                };
                Grid.SetColumn(goal, 0);
                Grid.SetRow(goal, 0);
                savings.Children.Add(goal);
                Label amount = new Label()
                {
                    Content = $"{this.Savings[i].TargetSum} UAH",
                    HorizontalAlignment = HorizontalAlignment.Right,
                    FontSize = 12,
                    Margin = new Thickness(5)
                };
                Grid.SetColumn(amount, 1);
                Grid.SetRow(amount, 0);
                savings.Children.Add(amount);
                Button delete = new Button()
                {
                    Style = (Style)this.FindResource("CloseButton"),
                    Width = 15,
                    Height = 15,
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Tag = i,
                    Margin = new Thickness(10, 5, 10, 10)
                };
                delete.Click += this.Delete_Click;
                Grid.SetRow(delete, 0);
                Grid.SetColumn(delete, 2);
                savings.Children.Add(delete);

                ProgressBar progressBar = new ProgressBar()
                {
                    Minimum = 0,
                    Maximum = this.Savings[i].TargetSum,
                    Value = this.Savings[i].CurrentSum,
                    Height = 15,
                    Margin = new Thickness(15, 5, 10, 15),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CCDAB6FC")),
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0")),
                };

                Grid.SetRow(progressBar, 1);
                savings.Children.Add(progressBar);

                border.Child = savings;
                this.SavingsStackPannel.Children.Add(border);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int savingIndex = (int)deleteButton.Tag;
            if (savingIndex >= 0 && savingIndex < this.Savings.Count)
            {
                this.Savings.RemoveAt(savingIndex);
                this.UpdateSavingsGrid();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddSavings_Click(object sender, RoutedEventArgs e)
        {
            this.replenishment.Visibility = Visibility.Collapsed;
            this.replenishmentBorder.Visibility = Visibility.Collapsed;
            this.adding.Visibility = Visibility.Visible;
            this.addingBorder.Visibility = Visibility.Visible;
            this.AddSavings.Visibility = Visibility.Collapsed;
        }

        private void closeAddSavings_Click(object sender, RoutedEventArgs e)
        {
            this.adding.Visibility = Visibility.Collapsed;
            this.addingBorder.Visibility = Visibility.Collapsed;
            this.replenishment.Visibility = Visibility.Visible;
            this.replenishmentBorder.Visibility = Visibility.Visible;
            this.AddSavings.Visibility = Visibility.Visible;
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
            string topUpAmountInput = this.TopUpAmountSavings.Text;
            var selectedSavings = this.SavingsList.SelectedItem;

            if (string.IsNullOrWhiteSpace(this.TopUpAmountSavings.Text) || selectedSavings == null)
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

            foreach (var saving in this.Savings)
            {
                SavingsName.Add(saving.TargetName);
            }
            this.SavingsList.ItemsSource = SavingsName;
        }

        private void TopUp_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string dateText = this.Date.Text.Trim();
            string amountText = this.Amount_.Text.Trim();

            if (name == "Введіть назву")
            {
                MessageBox.Show("Усі поля повинні бути заповненими!");
            }
            else if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Сума повинна бути додатнім числом!");
            }
            else if (!int.TryParse(dateText, out int date) || date < 1)
            {
                MessageBox.Show("Кількість місяців повинна бути 1 чи більше");
            }
            else
            {
                decimal amountPerMonth = amount / date;
                this.AmountPerMonth.Text = amountPerMonth.ToString("F2");
                MessageBox.Show("Дані пройшли перевірку!");
            }
        }



    }
}
