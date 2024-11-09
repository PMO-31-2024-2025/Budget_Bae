using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
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
        private NavBar navBar;

        public SettingsWindow()
        {
            InitializeComponent();
            UserNameLabel.Content = DbHelper.db.Users.First(u => u.Id == SessionManager.CurrentUserId).Name;
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
            Close();
        }

        private void PlannedPaymentsButton_Click(Object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                PlannedPaymentsWindow window = new PlannedPaymentsWindow();
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
                categoriesWindow = new SettingsCategoriesWindow();
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
                this.SupportExpanderButton.RenderTransform = rotateTransform;
                this.SupportExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.supportWindow = new SettingsSupportWindow();
                this.supportWindow.Owner = this;
                this.supportWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = this.supportWindow.Width;
                this.supportWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                this.supportWindow.Top = buttonPosition.Y - 80;
                this.supportWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.SupportExpanderButton.RenderTransform = rotateTransform;
                this.SupportExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.supportWindow.Close();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow != null && mainWindow.MainFrame != null)
            {
                this.navBar = mainWindow.NavBar;
                this.navBar.nbAnalyticsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFAF0"));
                this.navBar.nbMainButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#50DAB6FC"));
                mainWindow.MainFrame.Navigate(new MainPage());
            }

            this.navBar.nbEntryButton.Content = "Немає даних";
            SessionManager.ClearCurrentAccount();
            this.Close();
            entryWindow.ShowDialog();
        }
    }
}
