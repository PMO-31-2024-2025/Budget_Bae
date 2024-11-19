// <copyright file="ExpenseService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using DAL.Data;
    using DAL.Models;

    public static class ExpenseService
    {
        public static List<Expense> GetExpensesByUserId()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            return DbHelper.dbc.Expenses
                .Where(e => accountIds.Contains(e.AccountId))
                .ToList();
        }

        public static double GetCurrentExpensesByCategoryId(int categoryId)
        {
            var accountIds = AccountService.GetUsersAccountsId();
            var currentDate = DateTime.Now;

            double total = DbHelper.dbc.Expenses
                .AsEnumerable()
                .Where(e => e.CategoryId == categoryId &&
                         accountIds.Contains(e.AccountId) &&
                         DateTime.Parse(e.ExpenseDate).Year == currentDate.Year &&
                         DateTime.Parse(e.ExpenseDate).Month == currentDate.Month)
                .Sum(e => e.ExpenseSum);

            return total;
        }

        public static double PrevPrevExpense()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            var dateTwoMonthsAgo = currentDate.AddMonths(-2);
            var yearTwoMonthsAgo = dateTwoMonthsAgo.Year;
            var monthTwoMonthsAgo = dateTwoMonthsAgo.Month;
            double prevPrevMonthExpense = DbHelper.dbc.Expenses
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
            double prevMonthExpense = DbHelper.dbc.Expenses
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
            double currentMonthExpense = DbHelper.dbc.Expenses
                .AsEnumerable()
                .Where(e => accountIds.Contains(e.AccountId) &&
                         DateTime.Parse(e.ExpenseDate).Year == currentDate.Year &&
                         DateTime.Parse(e.ExpenseDate).Month == currentDate.Month)
                .Sum(e => e.ExpenseSum);

            return currentMonthExpense;
        }

        public static async Task<bool> AddExpenseAsync(int categoryId, double expenseSum, int accountId)
        {
            var expense = new Expense
            {
                CategoryId = categoryId,
                ExpenseSum = expenseSum,
                ExpenseDate = DateTime.Now.ToString(),
                AccountId = accountId,
            };

            DbHelper.dbc.Expenses.Add(expense);
            await DbHelper.dbc.SaveChangesAsync();

            return true;
        }

        public static async Task<bool> DeleteExpenseAsync(int expenseId)
        {
            var expense = DbHelper.dbc.Expenses.Find(expenseId);
            if (expense != null)
            {
                DbHelper.dbc.Remove(expense);
                await DbHelper.dbc.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Не знайдено вказану витрату!");
            }
            return true;
        }
    }
}