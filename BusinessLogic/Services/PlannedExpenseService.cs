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
    }
}
