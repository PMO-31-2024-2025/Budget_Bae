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
    /// Interaction logic for SavingsWindow.xaml
    /// </summary>
    public partial class SavingsWindow : Window
    {
        public SavingsWindow()
        {
            InitializeComponent();
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
        }

        private void closeAddSavings_Click(object sender, RoutedEventArgs e)
        {
            adding.Visibility = Visibility.Collapsed;
            addingBorder.Visibility = Visibility.Collapsed;
            replenishment.Visibility = Visibility.Visible;
            replenishmentBorder.Visibility= Visibility.Visible;
        }
    }
}
