/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
namespace Presentation
{
    using BusinessLogic.Session;
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.MainFrame.Navigate(new MainPage());
            if (SessionManager.CurrentUserId == null)
            {
                Loaded += this.MainWindow_Loaded;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            EntryWindow entryWindow = new EntryWindow();
            entryWindow.ShowDialog();
        }
    }
}
