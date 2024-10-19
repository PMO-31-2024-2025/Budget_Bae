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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    ///
    public partial class SettingsWindow : Window
    {

        private SettingsCategoriesWindow categoriesWindow;
        private SettingsNotificationWindow notificationWindow;
        private SettingsSupportWindow supportWindow;
        public ObservableCollection<string> categories = new ObservableCollection<string> { "Категорія 1", "Категорія 2",
        "Категорія 3", "Категорія 4", "Категорія 5", "Категорія 6", "Категорія 7", "Категорія 8" };
        public ObservableCollection<string> plannedPayments = new ObservableCollection<string> { "Комунальні послуги", "Спортзал" };


        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SettingsEntryButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        
        private void HistoryArrowButton_Click(Object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                mainWindow.MainFrame.Navigate(new AnalyticsPage());
            }
            Close();
        }

        private void PlannedPaymentsButton_Click(Object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                PlannedPaymentsWindow window = new PlannedPaymentsWindow(plannedPayments);
                window.ShowDialog();
            }
            
        }

        private void PrivacyPolicyButton_Click(Object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                PrivacyPolicyWindow window = new PrivacyPolicyWindow();
                window.ShowDialog();
            }
        }

        private void TermsOfUseButton_Click(Object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                TermsOfUseWindow window = new TermsOfUseWindow();
                window.ShowDialog();
            }
        }

        private void CategoryExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            double аngle = -90;

            if (categoriesWindow == null || !categoriesWindow.IsVisible)
            {
                RotateTransform rotateTransform = new RotateTransform(аngle);
                CategoryExpanderButton.RenderTransform = rotateTransform;
                CategoryExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                categoriesWindow = new SettingsCategoriesWindow(categories);
                categoriesWindow.Owner = this;
                categoriesWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = categoriesWindow.Width;
                categoriesWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                categoriesWindow.Top = buttonPosition.Y;
                categoriesWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                CategoryExpanderButton.RenderTransform = rotateTransform;
                CategoryExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                categoriesWindow.Close();
            }
        }

        private void NotificationExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            double аngle = -90;

            if (notificationWindow == null || !notificationWindow.IsVisible)
            {
                RotateTransform rotateTransform = new RotateTransform(аngle);
                NotificationExpanderButton.RenderTransform = rotateTransform;
                NotificationExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                notificationWindow = new SettingsNotificationWindow();
                notificationWindow.Owner = this;
                notificationWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = notificationWindow.Width;
                notificationWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                notificationWindow.Top = buttonPosition.Y - 40;
                notificationWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                NotificationExpanderButton.RenderTransform = rotateTransform;
                NotificationExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                notificationWindow.Close();
            }
        }

        private void SupportExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            double аngle = -90;

            if (supportWindow == null || !supportWindow.IsVisible)
            {
                RotateTransform rotateTransform = new RotateTransform(аngle);
                SupportExpanderButton.RenderTransform = rotateTransform;
                SupportExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                supportWindow = new SettingsSupportWindow();
                supportWindow.Owner = this;
                supportWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = supportWindow.Width;
                supportWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                supportWindow.Top = buttonPosition.Y - 80;
                supportWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                SupportExpanderButton.RenderTransform = rotateTransform;
                SupportExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                supportWindow.Close();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
