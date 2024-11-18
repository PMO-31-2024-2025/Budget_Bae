﻿namespace Presentation
{
    using BusinessLogic.Session;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for NavBar.xaml
    /// </summary>
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            this.InitializeComponent();

        }
        public void NbMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
            this.nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
            ((MainWindow)Window.GetWindow(this)).MainFrame.Navigate(new MainPage());

        }
        public void OpenAnalitics()
        {
            this.nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
            this.nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
            ((MainWindow)Window.GetWindow(this)).MainFrame.Navigate(new AnalyticsPage());
        }
        private void NbAnalyticsButton_Click(object sender, RoutedEventArgs e)
        {
            this.OpenAnalitics();
        }

        private void NbEntryButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult closeTheWindow = MessageBox.Show("Бажаєте вийти з акаунту?", "Застереження!", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (closeTheWindow == MessageBoxResult.Yes)
            {
                this.nbEntryButton.Content = "Немає даних";
                EntryWindow entryWindow = new EntryWindow();
                SessionManager.ClearCurrentUser();
                entryWindow.ShowDialog();
            }
        }

        private void NbSettingsButton_Click(Object sender, RoutedEventArgs e)
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
