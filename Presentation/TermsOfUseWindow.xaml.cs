/// <summary>
/// Interaction logic for TermsOfUseWindow.xaml
/// </summary>
namespace Presentation
{
    using System.Windows;

    public partial class TermsOfUseWindow : Window
    {
        public TermsOfUseWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
