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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();

        }
        private void nbMainButton_Click(object sender, RoutedEventArgs e)
        {
            nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
            nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
            ((MainWindow)Window.GetWindow(this)).MainFrame.Navigate(new MainPage());

        }

        private void nbAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
            nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
            ((MainWindow)Window.GetWindow(this)).MainFrame.Navigate(new AnalyticsPage());

        }

        private void nbExitButton_Click(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.Owner = Window.GetWindow(this);
            entryWindow.ShowDialog();
        }

        private void nbSettingsButton_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}
