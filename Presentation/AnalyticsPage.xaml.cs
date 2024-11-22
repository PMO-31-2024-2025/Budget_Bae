namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
    using LiveCharts;
    using LiveCharts.Wpf;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;

    /// <summary>
    /// Interaction logic for AnalyticsPage.xaml.
    /// </summary>
    public partial class AnalyticsPage : Page
    {
        public AnalyticsPage()
        {
            this.InitializeComponent();
            this.UpdatePieChart();
            this.UpdateBarChart();
            this.UpdateHistory();
            ExpensesWindow.ExpenseAdded += this.UpdateHistory;
            this.AnimateVisibleElements();
        }

        public void UpdateBarChart()
        {
            double incomeTwoMonthsAgo = IncomeService.PrevPrevIncome();
            double expenseTwoMonthsAgo = ExpenseService.PrevPrevExpense();

            double incomePreviousMonth = IncomeService.PrevIncome();
            double expensePreviousMonth = ExpenseService.PrevExpense();

            double incomeCurrentMonth = IncomeService.CurrentIncome();
            double expenseCurrentMonth = ExpenseService.CurrentExpense();

            var currentDate = DateTime.Now;

            var monthLabels = new List<string>
            {
                currentDate.AddMonths(-2).ToString("MMM"),
                currentDate.AddMonths(-1).ToString("MMM"),
                currentDate.ToString("MMM"),
            };

            bool hasData = expenseTwoMonthsAgo > 0 || expensePreviousMonth > 0 || expenseCurrentMonth > 0 ||
                   incomeTwoMonthsAgo > 0 || incomePreviousMonth > 0 || incomeCurrentMonth > 0;

            if (!hasData)
            {
                this.AnalyticsBarChart.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Немає даних",
                        Values = new ChartValues<double> { 0 },
                        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#E0E0E0"),
                    },
                };
                this.AnalyticsBarChart.AxisX.Clear();
                this.AnalyticsBarChart.AxisX.Add(new Axis
                {
                    Labels = new List<string> { string.Empty },
                });

                this.AnalyticsBarChart.AxisY[0].LabelFormatter = value => string.Empty;
            }
            else
            {
                var seriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Витрати",
                        Values = new ChartValues<double> { expenseTwoMonthsAgo, expensePreviousMonth, expenseCurrentMonth },
                        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#CCDAB6FC"),
                    },
                    new ColumnSeries
                    {
                        Title = "Доходи",
                        Values = new ChartValues<double> { incomeTwoMonthsAgo, incomePreviousMonth, incomeCurrentMonth },
                        Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#CC9B70C2"),
                    },
                };
                this.AnalyticsBarChart.Series = seriesCollection;
                this.AnalyticsBarChart.AxisX.Clear();
                this.AnalyticsBarChart.AxisX.Add(new Axis
                {
                    Labels = monthLabels,
                });
                this.AnalyticsBarChart.AxisY[0].LabelFormatter = value => value.ToString("C");
            }
        }

        private void UpdatePieChart()
        {
            var categories = ExpenseCategoryService.GetCategories();
            var pieSeriesCollection = new SeriesCollection();
            var legendItems = new List<dynamic>();
            var colors = new List<SolidColorBrush>
            {
                (SolidColorBrush)new BrushConverter().ConvertFrom("#2D4E6C"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#3C6890"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#3274B6"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#4A82B4"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#5995D2"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#8DB6E0"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#6E9BC3"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#86ACCD"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#AECCE9"),
                (SolidColorBrush)new BrushConverter().ConvertFrom("#BDD5ED"),
            };

            ScrollViewer scrollLegend = new ScrollViewer();

            if (SessionManager.CurrentUserId == null || ExpenseService.CurrentExpense() == 0)
            {
                pieSeriesCollection.Add(new PieSeries
                {
                    Title = "Немає даних",
                    Values = new ChartValues<double> { 1 },
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#BDD5ED"),
                });

                legendItems.Add(new
                {
                    Title = "Немає даних",
                    Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#BDD5ED"),
                });
            }
            else
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    var category = categories[i];
                    var color = colors[i % colors.Count];

                    if (ExpenseService.GetCurrentExpensesByCategoryId(category.Id) != 0)
                    {
                        pieSeriesCollection.Add(new PieSeries
                        {
                            Title = category.Name,
                            Values = new ChartValues<double> { ExpenseService.GetCurrentExpensesByCategoryId(category.Id) },
                            Fill = color,
                        });

                        legendItems.Add(new
                        {
                            Title = category.Name,
                            Color = color,
                        });
                    }
                }
            }

            this.AnalyticsPieChart.Series = pieSeriesCollection;
            this.AnalyticsLegend.ItemsSource = legendItems;
        }

        private Border AddTransaction(int id, string category, string sum, string account, char type)
        {
            Border elem = new Border()
            {
                Style = (Style)this.FindResource("HistoryElement"),
                Opacity = 0,
            };
            Grid elemGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            elemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            elemGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            elemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            elemGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label categoryLabel = new Label()
            {
                Content = category,
                Margin = new Thickness(15, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18,
            };
            Grid.SetRow(categoryLabel, 0);
            Grid.SetColumn(categoryLabel, 0);
            Grid.SetRowSpan(categoryLabel, 2);

            Label sumLabel = new Label()
            {
                Content = sum,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 10, 0),
            };
            Grid.SetRow(sumLabel, 0);
            Grid.SetColumn(sumLabel, 1);

            Label accountLabel = new Label()
            {
                Content = account,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 0, 10, 0),
            };
            Grid.SetRow(accountLabel, 1);
            Grid.SetColumn(accountLabel, 1);

            Button deleteButton = new Button()
            {
                Content = "×",
                Style = (Style)this.FindResource("DeleteHistoryElement"),
            };
            deleteButton.Click += (s, e) =>
            {
                if (type == 'e')
                {
                    _ = ExpenseService.DeleteExpenseAsync(id);
                }
                else
                {
                    _ = IncomeService.DeleteIncomeAsync(id);
                }
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                if (mainWindow != null && mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new AnalyticsPage());
                }
            };
            Grid.SetRow(deleteButton, 0);
            Grid.SetColumn(deleteButton, 1);
            Grid.SetRowSpan(deleteButton, 2);

            elem.MouseEnter += (s, e) =>
            {
                sumLabel.Visibility = Visibility.Hidden;
                accountLabel.Visibility = Visibility.Hidden;
                deleteButton.Visibility = Visibility.Visible;
                elem.Width = 515;
                elem.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#EEC8FF");
                elem.Effect = new DropShadowEffect()
                {
                    Color = (Color)ColorConverter.ConvertFromString("#9B70C2"),
                    BlurRadius = 20,
                    ShadowDepth = 7,
                };
            };

            elem.MouseLeave += (s, e) =>
            {
                sumLabel.Visibility = Visibility.Visible;
                accountLabel.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Hidden;
                elem.Width = 500;
                elem.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#DAB6FC");
                elem.Effect = new DropShadowEffect()
                {
                    Color = (Color)ColorConverter.ConvertFromString("#9B70C2"),
                    BlurRadius = 10,
                    ShadowDepth = 5,
                };
            };

            deleteButton.MouseEnter += (s, e) =>
            {
                deleteButton.FontWeight = FontWeights.Bold;
                deleteButton.FontSize = 30;
            };

            deleteButton.MouseLeave += (s, e) =>
            {
                deleteButton.FontWeight = FontWeights.SemiBold;
                deleteButton.FontSize = 25;
            };

            if (type == 'e')
            {
                elem.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                elem.HorizontalAlignment = HorizontalAlignment.Right;
            }

            elemGrid.Children.Add(categoryLabel);
            elemGrid.Children.Add(sumLabel);
            elemGrid.Children.Add(accountLabel);
            elemGrid.Children.Add(deleteButton);

            elem.Child = elemGrid;
            return elem;
        }

        private void UpdateHistory()
        {
            List<Expense> expenses = ExpenseService.GetExpensesByUserId();
            List<Income> incomes = IncomeService.GetIncomesByUserId();
            List<object> transactions = new List<object>();
            transactions.AddRange(expenses);
            transactions.AddRange(incomes);
            List<object> sortedTransactions = transactions.OrderBy(t => t is Expense e ? e.ExpenseDate : (t as Income)?.IncomeDate).ToList();

            if (sortedTransactions.Count != 0)
            {
                DateTime prevDate = sortedTransactions.First() switch
                {
                    Expense e => DateTime.Parse(e.ExpenseDate).Date,
                    Income i => DateTime.Parse(i.IncomeDate).Date,
                    _ => throw new NotImplementedException(),
                };
                DateTime date = prevDate;
                foreach (var transaction in sortedTransactions)
                {
                    if (transaction is Expense expense)
                    {
                        date = DateTime.Parse(expense.ExpenseDate).Date;
                        string category = ExpenseCategoryService.GetCategoryName(expense.CategoryId);
                        string sum = expense.ExpenseSum.ToString();
                        string account = AccountService.GetAccountName(expense.AccountId);
                        Border elem = this.AddTransaction(expense.Id, category, $"- {sum}", account, 'e');
                        if (date > prevDate)
                        {
                            this.HistoryElements.Children.Insert(0, new Label() { Content = prevDate.ToString("dddd, d MMMM") });
                            prevDate = date;
                        }

                        this.HistoryElements.Children.Insert(0, elem);
                    }
                    else if (transaction is Income income)
                    {
                        date = DateTime.Parse(income.IncomeDate).Date;
                        string category = income.Category;
                        string sum = income.IncomeSum.ToString();
                        string account = AccountService.GetAccountName(income.AccountId);
                        Border elem = this.AddTransaction(income.Id, category, $"+ {sum}", account, 'i');
                        if (date > prevDate)
                        {
                            this.HistoryElements.Children.Insert(0, new Label() { Content = prevDate.ToString("dddd, d MMMM") });
                            prevDate = date;
                        }

                        this.HistoryElements.Children.Insert(0, elem);
                    }
                }

                this.HistoryElements.Children.Insert(0, new Label() { Content = date.ToString("dddd, d MMMM") });
            }
        }


        private bool IsElementVisible(UIElement element)
        {
            var scrollViewer = this.HistoryElements.Parent as ScrollViewer;
            if (scrollViewer == null)
            {
                return false;
            }

            try
            {
                GeneralTransform transform = element.TransformToAncestor(scrollViewer);
                Rect elementBounds = transform.TransformBounds(new Rect(new Point(0, 0), element.RenderSize));
                Rect viewportBounds = new Rect(
                    0,
                    0,
                    scrollViewer.ViewportWidth,
                    scrollViewer.ViewportHeight);

                return viewportBounds.IntersectsWith(elementBounds);
            }
            catch
            {
                return false;
            }
        }


        private void AnimateElement(UIElement element)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
            };

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }


        private async void AnimateVisibleElements()
        {
            await Task.Delay(300);

            foreach (UIElement child in this.HistoryElements.Children)
            {
                if (child.Opacity == 0 && this.IsElementVisible(child))
                {
                    this.AnimateElement(child);
                    await Task.Delay(200);
                }
                else
                {
                    child.Opacity = 1;
                }
            }
        }
    }
}