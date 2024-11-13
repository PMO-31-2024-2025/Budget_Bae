namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for PlannedPaymentsWindow.xaml
    /// </summary>
    public partial class PlannedPaymentsWindow : Window
    {
        public PlannedPaymentsWindow()
        {
            this.InitializeComponent();
            if (SessionManager.CurrentUserId != null)
            {
                this.PlannedPayments = PlannedExpenseService.GetPlannedExpenses();
            }
            else
            {
                this.PlannedPayments = [];
            }
            this.UpdatePaymentsGrid();

        }

        public List<PlannedExpense> PlannedPayments { get; set; }

        private void UpdatePaymentsGrid()
        {
            this.PaymentsStackPanel.Children.Clear();

            for (int i = 0; i < this.PlannedPayments.Count; i++)
            {
                Grid payment = new Grid();
                payment.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
                payment.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(45) });
                payment.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(235) });
                payment.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
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

                Label name = new Label()
                {
                    Width = 225,
                    Content = new TextBlock
                    {
                        Text = this.PlannedPayments[i].Name,
                        TextWrapping = TextWrapping.Wrap
                    },
                    FontSize = 18,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetRow(name, 0);
                Grid.SetColumn(name, 0);
                Grid.SetRowSpan(name, 2);
                payment.Children.Add(name);

                Button delete = new Button()
                {
                    Style = (Style)this.FindResource("CloseButton"),
                    Width = 15,
                    Height = 15,
                    FontSize = 12,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(0, 5, 15, 0),
                    Tag = i
                };
                delete.Click += this.Delete_Click;
                Grid.SetRow(delete, 0);
                Grid.SetColumn(delete, 1);
                payment.Children.Add(delete);

                Label date = new Label()
                {
                    Content = new TextBlock
                    {
                        Text = $"Внесок {this.PlannedPayments[i].NotigicationDate} числа\n{this.PlannedPayments[i].PlannedSum} UAH",
                        TextAlignment = TextAlignment.Right
                    },
                    VerticalAlignment = VerticalAlignment.Bottom,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    FontSize = 10,
                    Margin = new Thickness(0, 0, 10, 5)
                };
                Grid.SetRow(date, 1);
                Grid.SetColumn(date, 1);
                payment.Children.Add(date);

                border.Child = payment;
                this.PaymentsStackPanel.Children.Add(border);

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            int paymentIndex = (int)deleteButton.Tag;
            if (paymentIndex >= 0 && paymentIndex < this.PlannedPayments.Count)
            {
                this.PlannedPayments.RemoveAt(paymentIndex);
                this.UpdatePaymentsGrid();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void CreatePayment_Click(object sender, RoutedEventArgs e)
        {
            string name = this.Name.Text.Trim();
            string amountText = this.Amount.Text.Trim();
            string dateText = this.Date.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(amountText) || string.IsNullOrEmpty(dateText))
            {
                MessageBox.Show("Усі поля мають бути заповненими.");
                return;
            }
            else if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Сума поповнення повинна бути числом.");
                return;
            }
            else if (!int.TryParse(dateText, out int date) || date < 1 || date > 28)
            {
                MessageBox.Show("Число дати повинно бути від 1 до 28.");
                return;
            }
            else
            {
                int notificationDate = int.Parse(dateText);
                double expenseAmount = double.Parse(amountText);
                PlannedExpenseService.AddPlannedExpense(name, notificationDate, expenseAmount);
                MessageBox.Show("Платіж створено успішно!");
            }


        }
    }
}
