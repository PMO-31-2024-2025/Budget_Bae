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
            DateTime current = DateTime.Now.Date;  // Поточна дата без часу
            DateTime lastNotificationDate = Properties.Settings.Default.LastNotificationDate; // Остання дата показу сповіщення
            List<PlannedExpense> plannedExpenses;
            bool show = false;

            // Перевіряємо, чи змінився день (якщо так, потрібно показати сповіщення)
            if (current != lastNotificationDate)
            {
                // Оновлюємо останню дату показу сповіщення
                Properties.Settings.Default.LastNotificationDate = current;
                Properties.Settings.Default.Save();  // Зберігаємо зміни

                show = true;
            }

            if (show)
            {
                if (SessionManager.CurrentUserId != null)
                {
                    // Сповіщення на основі запланованих витрат
                    plannedExpenses = PlannedExpenseService.GetPlannedExpenses();
                    foreach (var expense in plannedExpenses)
                    {
                        if (expense.NotigicationDate == current.Day)  // Перевірка, чи треба показати сповіщення
                        {
                            Label notificationText = new Label
                            {
                                Content = $"Сьогодні треба внести платіж '{expense.Name}'",
                                Margin = new Thickness(0, 5, 0, 5),
                            };
                            this.Notifications.Children.Add(notificationText);

                            // Встановлюємо позицію вікна сповіщення в правий нижній кут
                            this.WindowStartupLocation = WindowStartupLocation.Manual;
                            this.Left = SystemParameters.WorkArea.Width - this.Width;
                            this.Top = SystemParameters.WorkArea.Height - this.Height;
                            this.Show();
                            break; // Якщо знайдено хоча б одне сповіщення, припиняємо цикл
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
