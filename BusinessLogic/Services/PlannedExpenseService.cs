using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class PlannedExpenseService
    {
        public static List<PlannedExpense> GetPlannedExpenses()
        {
            return DbHelper.db.PlannedExpenses
                .Where(p => p.UserId == SessionManager.CurrentUserId)
                .ToList();
        }

        public static double GetPaymentsAmount()
        {
            return DbHelper.db.PlannedExpenses
                .Where(p => p.UserId == SessionManager.CurrentUserId)
                .Sum(p => p.PlannedSum);

        }

        public static void AddPlannedExpense(string expenseName, int notification_date, double plannedSum)
        {
            int? currUser = SessionManager.CurrentUserId;
            List<PlannedExpense> currUserData = DbHelper.db.PlannedExpenses.Where(p => p.UserId == currUser).ToList();

            if (currUser != null)
            {
                var plannedExpense = DbHelper.db.PlannedExpenses.FirstOrDefault(c => c.Name == expenseName);
                if (plannedExpense == null)
                {
                    plannedExpense = new PlannedExpense
                    {
                        Name = expenseName, 
                        NotigicationDate = notification_date,
                        PlannedSum = plannedSum,
                        UserId = currUser.Value
                    };
                    DbHelper.db.PlannedExpenses.Add(plannedExpense);
                    DbHelper.db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Задана запланована витрата вже існує!");
                }
            }
        }

        public static void DeletePlannedExpense(string expenseName)
        {
            int? currUser = SessionManager.CurrentUserId;
            if (currUser != null)
            {
                throw new Exception("Вам нічого видаляти.");
            }
            else
            {
                List<PlannedExpense> currUserData = DbHelper.db.PlannedExpenses.Where(p => p.UserId == currUser).ToList();

                var plannedExpenseToDelete = currUserData.FirstOrDefault(x => x.Name == expenseName);
                if (plannedExpenseToDelete.UserId == SessionManager.CurrentUserId)
                {
                    DbHelper.db.PlannedExpenses.Remove(plannedExpenseToDelete);
                    DbHelper.db.SaveChangesAsync();
                }
            }
            
        }
    }
}
