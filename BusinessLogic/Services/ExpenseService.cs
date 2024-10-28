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
    public class ExpenseService
    {
        public List<Expense> GetExpensesByUserId()
        {
            var accountIds = DbHelper.db.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .Select(a => a.Id)
                .ToList();

            var expenses = DbHelper.db.Expenses
                .Where(e => accountIds.Contains(e.AccountId))
                .ToList();

            return expenses;
        }

        public double GetTotalExpensesByCategoryId(int categoryId)
        {
            double totalAmount = DbHelper.db.Expenses
                .Where(e => e.CategoryId == categoryId)
                .Sum(e => e.ExpenseSum);

            return totalAmount;
        }
    }
}
