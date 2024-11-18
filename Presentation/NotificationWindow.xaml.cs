namespace Presentation
{
    using BusinessLogic.Services;
    using BusinessLogic.Session;
    using DAL.Models;
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

    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private DateTime date = DateTime.Now.Date.AddDays(-1);
        private int temp;

        public NotificationWindow()
        {
            InitializeComponent();
        }

        public void ShowNotification()
        {
            DateTime current = DateTime.Now.Date;
            DateTime lastNotificationDate = Properties.Settings.Default.LastNotificationDate;
            List<PlannedExpense> plannedExpenses;
            bool show = false;

            if (current != lastNotificationDate)
            {
                Properties.Settings.Default.LastNotificationDate = current;
                Properties.Settings.Default.Save();

                show = true;
            }

            if (show)
            {
                if (SessionManager.CurrentUserId != null)
                {
                    plannedExpenses = PlannedExpenseService.GetPlannedExpenses();
                    foreach (var expense in plannedExpenses)
                    {
                        if (expense.NotigicationDate == current.Day)
                        {
                            Label notificationText = new Label
                            {
                                Content = $"Сьогодні треба внести платіж '{expense.Name}'",
                                Margin = new Thickness(0, 5, 0, 5),
                            };
                            this.Notifications.Children.Add(notificationText);

                            this.WindowStartupLocation = WindowStartupLocation.Manual;
                            this.Left = SystemParameters.WorkArea.Width - this.Width;
                            this.Top = SystemParameters.WorkArea.Height - this.Height;
                            this.Show();
                            break;
                        }
                    }
                }
            }
        }


        private void CloseNotificationWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
