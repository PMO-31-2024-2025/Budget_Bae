using LiveCharts.Wpf;
using LiveCharts;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using DAL.Models;
using DAL.Data;
using BusinessLogic.Session;
using System.Drawing;
using System.Diagnostics;
using WebAssemblyMan;
using BusinessLogic.Services;
using LiveCharts.Definitions.Charts;
using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private List<Account> accounts;
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
            
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePieChart();
            UserExpenses.Text = $"{Math.Round(ExpenseService.CurrentExpense(), 2).ToString()} UAH";
            accounts = AccountService.GetUsersAccounts();
            SetAccounts();
        }

            private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow window = new AddAccountWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.ShowDialog();
        }

        private void AddExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            ExpensesWindow window = new ExpensesWindow();
            window.ShowDialog();
        }

        private void OpenCategories_Click(object sender, RoutedEventArgs e)
        {
            CategoriesWindow window = new CategoriesWindow();
            window.ShowDialog();
        }

        private void AddIncome_Click(object sender, RoutedEventArgs e)
        { 
            IncomeWindow window = new IncomeWindow();
            window.ShowDialog();
        }

        private void OpenAccounts_Click(object sender, RoutedEventArgs e)
        {
            AccountsWindow window = new AccountsWindow();
            window.ShowDialog();
        }

        private void SavingsButton_Click(object sender, RoutedEventArgs e)
        {
            SavingsWindow window = new SavingsWindow();
            window.ShowDialog();
        }

        private void PlannedPaymentsButton_Click(object sender, RoutedEventArgs e)
        {
            PlannedPaymentsWindow window = new PlannedPaymentsWindow();
            window.ShowDialog();
        }

        private void Grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.ContextMenu != null)
            {
                grid.ContextMenu.PlacementTarget = grid;
                grid.ContextMenu.IsOpen = true;
                e.Handled = true;
            }
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow addCategoryWindow = new AddCategoryWindow();
            addCategoryWindow.ShowDialog();
        }

        private void AddAccount_Click_1(object sender, RoutedEventArgs e)
        {
            AddAccountWindow addAccountWindow = new AddAccountWindow();
            addAccountWindow.ShowDialog();
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            ExpensesWindow expensesWindow = new ExpensesWindow();
            expensesWindow.ShowDialog();
        }

        private void AddIncome_Click_1(object sender, RoutedEventArgs e)
        {
            IncomeWindow incomeWindow = new IncomeWindow();
            incomeWindow.ShowDialog();
        }

        private void Savings_Click(object sender, RoutedEventArgs e)
        {
            SavingsWindow savingsWindow = new SavingsWindow();  
            savingsWindow.ShowDialog();
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            PlannedPaymentsWindow plannedPaymentsWindow = new PlannedPaymentsWindow();
            plannedPaymentsWindow.ShowDialog();
        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow != null)
            {
                if (mainWindow.MainFrame != null)
                {
                    mainWindow.MainFrame.Navigate(new AnalyticsPage());
                }
                NavBar navBarControl = mainWindow.NavBar;
                if (navBarControl != null)
                {
                    navBarControl.OpenAnalitics();
                }
            }
        }

        public void SetAccounts()
        {
            Grid accountsGrid = new Grid();
            accountsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160) });
            accountsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(160) });
            accountsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(325) });
            accountsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(325) });

            if (accounts.Count > 0)
            {
                Button accountButton1 = new Button
                {
                    Content = new TextBlock()
                    {
                        Text = $"{accounts[0].Name}\n{accounts[0].Balance} UAH",
                        TextAlignment = TextAlignment.Center
                    },
                    FontFamily = (System.Windows.Media.FontFamily)FindResource("CustomFont"),
                    Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F4F4F4")),
                    FontSize = 20,
                    FontWeight = FontWeights.SemiBold,
                    Style = (Style)FindResource("AccountsButton"),
                    Margin = new Thickness(35, 20, 20, 10)
                };
                accountButton1.Click += AddIncome_Click;
                Grid.SetRow(accountButton1, 0);
                Grid.SetColumn(accountButton1, 0);
                accountsGrid.Children.Add(accountButton1);
            }

            if (accounts.Count > 1)
            {
                Button accountButton2 = new Button
                {
                    Content = new TextBlock()
                    {
                        Text = $"{accounts[1].Name}\n{accounts[1].Balance} UAH",
                        TextAlignment = TextAlignment.Center
                    },
                    FontFamily = (System.Windows.Media.FontFamily)FindResource("CustomFont"),
                    Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F4F4F4")),
                    FontSize = 20,
                    FontWeight = FontWeights.SemiBold,
                    Style = (Style)FindResource("AccountsButton"),
                    Margin = new Thickness(20, 20, 35, 10)
                };
                accountButton2.Click += AddIncome_Click;
                Grid.SetRow(accountButton2, 0);
                Grid.SetColumn(accountButton2, 1);
                accountsGrid.Children.Add(accountButton2);
            }

            if (accounts.Count > 2)
            {
                Button accountButton3 = new Button
                {
                    Content = new TextBlock()
                    {
                        Text = $"{accounts[2].Name}\n{accounts[2].Balance} UAH",
                        TextAlignment = TextAlignment.Center
                    },
                    FontFamily = (System.Windows.Media.FontFamily)FindResource("CustomFont"),
                    Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F4F4F4")),
                    FontSize = 20,
                    FontWeight = FontWeights.SemiBold,
                    Style = (Style)FindResource("AccountsButton"),
                    Margin = new Thickness(35, 5, 20, 15)
                };
                accountButton3.Click += AddIncome_Click;
                Grid.SetRow(accountButton3, 1);
                Grid.SetColumn(accountButton3, 0);
                accountsGrid.Children.Add(accountButton3);
            }

            Button moreButton = new Button
            {
                Content = "БІЛЬШЕ",
                FontFamily = (System.Windows.Media.FontFamily)FindResource("CustomFont"),
                Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#F4F4F4")),
                FontSize = 30,
                FontWeight = FontWeights.SemiBold,
                Style = (Style)FindResource("AccountsButton"),
                Margin = new Thickness(20, 5, 35, 15)
            };
            moreButton.Click += OpenAccounts_Click;
            Grid.SetRow(moreButton, 1);
            Grid.SetColumn(moreButton, 1);
            accountsGrid.Children.Add(moreButton);

            AccountsPanel.Children.Add(accountsGrid);
        }

        public void UpdatePieChart()
        {
            var pieSeriesCollection = new SeriesCollection();
            var legendItems = new List<dynamic>();

            var seriesData = new[]
            {
                new { Title = "витрати цього місяця", Value = ExpenseService.CurrentExpense(), Color = "#9B70C2" },
                new { Title = "заплановані витрати", Value = PlannedExpenseService.GetPaymentsAmount(), Color = "#999B70C2" },
                new { Title = "в заощадження", Value = Math.Round(SavingService.GetTotalSavings(), 2), Color = "#DAB6FC" },
                new { Title = "залишок доходу за минулий місяць", Value = IncomeService.PrevIncome() - ExpenseService.CurrentExpense(), Color = "#99DAB6FC" }
            };

            bool hasData = seriesData.Any(item => item.Value > 0);

            if (!hasData)
            {
                var defaultPieSeries = new PieSeries
                {
                    Title = "Немає даних",
                    Values = new ChartValues<double> { 1 },
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#99DAB6FC"))
                };

                pieSeriesCollection.Add(defaultPieSeries);
                legendItems.Add(new
                {
                    Title = "Немає даних",
                    Color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#99DAB6FC"))
                });
            }
            else
            {
                for (int i = 0; i < seriesData.Length; i++)
                {
                    var item = seriesData[i];

                    var pieSeries = new PieSeries
                    {
                        Title = item.Title,
                        Values = new ChartValues<double> { item.Value },
                        Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(item.Color))
                    };

                    pieSeriesCollection.Add(pieSeries);
                    legendItems.Add(new
                    {
                        Title = item.Title,
                        Color = (SolidColorBrush)(new BrushConverter().ConvertFrom(item.Color))
                    });
                }
            }
            MainPieChart.Series = pieSeriesCollection;
            MainLegend.ItemsSource = legendItems;
        }
    }


}

