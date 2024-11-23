namespace Presentation
{
    using BusinessLogic.Session;
    using DAL.Data;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Microsoft.Extensions.Logging;
    using BusinessLogic.Services;

    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    ///
    public partial class SettingsWindow : Window
    {
        private static ILogger logger;
        private SettingsCategoriesWindow categoriesWindow;
        private SettingsAccountsWindow accountsWindow;
        private SettingsNotificationWindow notificationWindow;
        private SettingsSupportWindow supportWindow;
        private NavBar navBar;

        public SettingsWindow()
        {
            this.InitializeComponent();
            this.UserNameLabel.Content = DbHelper.dbc.Users.First(u => u.Id == SessionManager.CurrentUserId).Name;
        }

        public static void InitializeLogger(ILogger logger)
        {
            SettingsWindow.logger = logger;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            this.Close();
        }

        private void PlannedPaymentsButton_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                PlannedPaymentsWindow window = new PlannedPaymentsWindow();
                window.ShowDialog();
            }

        }

        private void PrivacyPolicyButton_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.MainFrame != null)
            {
                PrivacyPolicyWindow window = new PrivacyPolicyWindow();
                window.ShowDialog();
            }
        }

        private void TermsOfUseButton_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
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

            if (this.categoriesWindow == null || !this.categoriesWindow.IsVisible)
            {
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.CategoryExpanderButton.RenderTransform = rotateTransform;
                this.CategoryExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.categoriesWindow = new SettingsCategoriesWindow();
                this.categoriesWindow.Owner = this;
                this.categoriesWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = this.categoriesWindow.Width;
                this.categoriesWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                this.categoriesWindow.Top = buttonPosition.Y + 13;
                this.categoriesWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.CategoryExpanderButton.RenderTransform = rotateTransform;
                this.CategoryExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.categoriesWindow.Close();
            }
        }

        private void AccountExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            double аngle = -90;

            if (this.accountsWindow == null || !this.accountsWindow.IsVisible)
            {
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.AccountExpanderButton.RenderTransform = rotateTransform;
                this.AccountExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.accountsWindow = new SettingsAccountsWindow();
                this.accountsWindow.Owner = this;
                this.accountsWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = this.accountsWindow.Width;
                this.accountsWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                this.accountsWindow.Top = buttonPosition.Y;
                this.accountsWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.AccountExpanderButton.RenderTransform = rotateTransform;
                this.AccountExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.accountsWindow.Close();
            }
        }

        private void NotificationExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            double аngle = -90;

            if (this.notificationWindow == null || !this.notificationWindow.IsVisible)
            {
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.NotificationExpanderButton.RenderTransform = rotateTransform;
                this.NotificationExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.notificationWindow = new SettingsNotificationWindow();
                this.notificationWindow.Owner = this;
                this.notificationWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var windowWidth = this.notificationWindow.Width;
                this.notificationWindow.Left = screenWidth - windowWidth;
                var button = sender as Button;
                var buttonPosition = button.PointToScreen(new Point(0, 0));
                var buttonHeight = button.ActualHeight;
                this.notificationWindow.Top = buttonPosition.Y - 40;
                this.notificationWindow.Show();
            }
            else
            {
                аngle = 0;
                RotateTransform rotateTransform = new RotateTransform(аngle);
                this.NotificationExpanderButton.RenderTransform = rotateTransform;
                this.NotificationExpanderButton.RenderTransformOrigin = new Point(0.5, 0.5);
                this.notificationWindow.Close();
            }
        }

        private void SupportExpanderButton_Click(object sender, RoutedEventArgs e)
        {
            double аngle = -90;

            if (this.supportWindow == null || !this.supportWindow.IsVisible)
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
                this.supportWindow.Top = buttonPosition.Y - 85;
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
            logger?.LogInformation($"Користувач {UserService.GetUserNameById(SessionManager.CurrentUserId)} вийшов з акаунта.");
            SessionManager.ClearCurrentAccount();
            this.Close();
            entryWindow.ShowDialog();
        }
    }
}
