namespace Presentation
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for SettingsNotificationWindow.xaml
    /// </summary>
    public partial class SettingsNotificationWindow : Window
    {
        public SettingsNotificationWindow()
        {
            this.InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.IsCheckedNotification = this.NotificationCheckBox.IsChecked ?? false;
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.NotificationCheckBox.IsChecked = Properties.Settings.Default.IsCheckedNotification;
        }
    }
}
