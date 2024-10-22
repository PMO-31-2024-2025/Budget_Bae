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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<string> plannedPayments = new ObservableCollection<string> { "Комунальні послуги", "Спортзал" };

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
            SavingsWindow window = new SavingsWindow();
            window.ShowDialog();
        }

        private void PlannedPaymentsButton_Click(object sender, RoutedEventArgs e)
        {
            PlannedPaymentsWindow window = new PlannedPaymentsWindow(plannedPayments);
            window.ShowDialog();
        }
    }
}
