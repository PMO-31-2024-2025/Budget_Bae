using BusinessLogic.Session;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
            if (SessionManager.CurrentUserId == null)
            {
                Loaded += MainWindow_Loaded;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.ShowDialog();
        }
    }
}
