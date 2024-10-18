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
    /// Interaction logic for SettingsNotificationWindow.xaml
    /// </summary>
    public partial class SettingsNotificationWindow : Window
    {
        public SettingsNotificationWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.IsCheckedNotification = NotificationCheckBox.IsChecked ?? false;
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NotificationCheckBox.IsChecked = Properties.Settings.Default.IsCheckedNotification;
        }
    }
}
