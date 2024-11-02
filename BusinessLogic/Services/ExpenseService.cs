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
        public static List<Expense> GetExpensesByUserId()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            return DbHelper.db.Expenses
                .Where(e => accountIds.Contains(e.AccountId))
                .ToList();
        }

        public static double GetTotalExpensesByCategoryId(int categoryId)
        {
            return DbHelper.db.Expenses
                .Where(e => e.CategoryId == categoryId)
                .Sum(e => e.ExpenseSum);
        }

        public static double PrevPrevExpense()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            var dateTwoMonthsAgo = currentDate.AddMonths(-2);
            var yearTwoMonthsAgo = dateTwoMonthsAgo.Year;
            var monthTwoMonthsAgo = dateTwoMonthsAgo.Month;
            double prevPrevMonthExpense = DbHelper.db.Expenses
                .AsEnumerable()
                .Where(e => accountIds.Contains(e.AccountId) &&
                         DateTime.Parse(e.ExpenseDate).Year == yearTwoMonthsAgo &&
                         DateTime.Parse(e.ExpenseDate).Month == monthTwoMonthsAgo)
                .Sum(e => e.ExpenseSum);

            return prevPrevMonthExpense;
        }

        public static double PrevExpense()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            var dateMonthAgo = currentDate.AddMonths(-1);
            var yearMonthAgo = dateMonthAgo.Year;
            var monthAgo = dateMonthAgo.Month;
            double prevMonthExpense = DbHelper.db.Expenses
                .AsEnumerable()
                .Where(e => accountIds.Contains(e.AccountId) &&
                         DateTime.Parse(e.ExpenseDate).Year == yearMonthAgo &&
                         DateTime.Parse(e.ExpenseDate).Month == monthAgo)
                .Sum(e => e.ExpenseSum);

            return prevMonthExpense;
        }

        public static double CurrentExpense()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            double currentMonthExpense = DbHelper.db.Expenses
                .AsEnumerable()
                .Where(e => accountIds.Contains(e.AccountId) &&
                         DateTime.Parse(e.ExpenseDate).Year == currentDate.Year &&
                         DateTime.Parse(e.ExpenseDate).Month == currentDate.Month)
                .Sum(e => e.ExpenseSum);

            return currentMonthExpense;
        }

        public static void AddExpense(int categoryId, double expenseSum, int accountId)
        {
            //if (DbHelper.db.Users.Any(u => u.Email == email))
            //{
            //    throw new Exception("Користувач з такою електронною поштою вже існує!");
            //}

            var expense = new Expense
            {
                CategoryId = categoryId,
                ExpenseSum = expenseSum,
                ExpenseDate = DateTime.Now.ToString(),
                AccountId = accountId
            };

            DbHelper.db.Expenses.Add(expense);
            DbHelper.db.SaveChangesAsync();
        }

        public static void DeleteExpense(int expenseId)
        {
            var expense = DbHelper.db.Expenses.Find(expenseId);
            if (expense != null)
            {
                DbHelper.db.Remove(expense);
                DbHelper.db.SaveChangesAsync();
            }
        }
    }
}