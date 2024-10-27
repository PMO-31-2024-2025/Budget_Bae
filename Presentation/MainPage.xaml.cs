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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<string> plannedPayments = new ObservableCollection<string> { "Комунальні послуги", "Спортзал" };
        public ObservableCollection<string> savings = new ObservableCollection<string> { "Кокальока обсешн", "На церкву", "Lip tint Rhode" };

        public MainPage()
        {
            InitializeComponent();
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

        private void AddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddAccountWindow window = new AddAccountWindow();
            window.ShowDialog();
        }

        private void SavingsButton_Click(object sender, RoutedEventArgs e)
        {
            SavingsWindow window = new SavingsWindow(savings);
            window.ShowDialog();
        }

        private void PlannedPaymentsButton_Click(object sender, RoutedEventArgs e)
        {
            PlannedPaymentsWindow window = new PlannedPaymentsWindow(plannedPayments);
            window.ShowDialog();
        }

        private void Grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Grid grid && grid.ContextMenu != null)
            {
                // Переконаємось, що контекстне меню прив’язане до Grid і відкрити його на місці кліку
                grid.ContextMenu.PlacementTarget = grid;
                grid.ContextMenu.IsOpen = true;
                e.Handled = true; // Зупиняємо подальшу обробку події
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
            SavingsWindow savingsWindow = new SavingsWindow(savings);  
            savingsWindow.ShowDialog();
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            PlannedPaymentsWindow plannedPaymentsWindow = new PlannedPaymentsWindow(plannedPayments);
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

        
    }
}

