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
        public void nbMainButton_Click(object sender, RoutedEventArgs e)
        {
            nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
            nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
            ((MainWindow)Window.GetWindow(this)).MainFrame.Navigate(new MainPage());

        }
        public void OpenAnalitics()
        {
            nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
            nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
            ((MainWindow)Window.GetWindow(this)).MainFrame.Navigate(new AnalyticsPage());
        }
        private void nbAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
           OpenAnalitics();
        }

        private void nbEntryButton_Click(object sender, RoutedEventArgs e)
        {
            // Показуємо діалогове вікно з повідомленням та двома кнопками
            MessageBoxResult result = MessageBox.Show("Бажаєте вийти з акаунту?", "Застереження", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Обробка результату
            if (result == MessageBoxResult.Yes && nbEntryButton.Content != "Вхід")
            {
                EntryWindow entryWindow = new EntryWindow();
                entryWindow.ShowDialog();
            }
            // ТУТ Я НЕ ХОТІВ ВИДАЛЯТИ ЧИЙСЬ КОД ТОМУ ЗАКОМЕНТУВАВ ЙОГО
            //if (nbEntryButton.Content != "Вхід")
            //{
            //    EntryWindow entryWindow = new EntryWindow();
            //    entryWindow.ShowDialog();
            //}
        }

        private void nbSettingsButton_Click(Object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            var mainWindow = Application.Current.MainWindow;
            settingsWindow.Height = mainWindow.ActualHeight;
            settingsWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var windowWidth = settingsWindow.Width;
            settingsWindow.Left = screenWidth - windowWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            var windowHeight = settingsWindow.Height;
            settingsWindow.Top = (screenHeight - windowHeight) / 2;
            settingsWindow.ShowDialog();
            
        }
    }
}
