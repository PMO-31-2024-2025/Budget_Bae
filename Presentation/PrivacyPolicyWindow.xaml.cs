namespace Presentation
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for PrivacyPolicyWindow.xaml
    /// </summary>
    public partial class PrivacyPolicyWindow : Window
    {
        public PrivacyPolicyWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
