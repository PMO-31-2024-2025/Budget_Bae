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
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for PlannedPaymentsWindow.xaml
    /// </summary>
    public partial class PlannedPaymentsWindow : Window
    {
        public ObservableCollection<string> PlannedPayments { get; set; }

        public PlannedPaymentsWindow(ObservableCollection<string> plannedPayments)
        {
            InitializeComponent();
            PlannedPayments = plannedPayments;
            
        }

         

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
