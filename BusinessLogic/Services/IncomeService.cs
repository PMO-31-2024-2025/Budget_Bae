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
    public class IncomeService
    {

        public static double PrevPrevIncome()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            var dateTwoMonthsAgo = currentDate.AddMonths(-2);
            var yearTwoMonthsAgo = dateTwoMonthsAgo.Year;
            var monthTwoMonthsAgo = dateTwoMonthsAgo.Month;
            double prevPrevMonthIncome = DbHelper.db.Incomes
                .AsEnumerable() 
                .Where(i => accountIds.Contains(i.AccountId) &&
                         DateTime.Parse(i.IncomeDate).Year == yearTwoMonthsAgo &&
                         DateTime.Parse(i.IncomeDate).Month == monthTwoMonthsAgo)
                .Sum(i => i.IncomeSum);

            return prevPrevMonthIncome;
        }

        public static double PrevIncome()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            var dateMonthAgo = currentDate.AddMonths(-1);
            var yearMonthAgo = dateMonthAgo.Year;
            var monthAgo = dateMonthAgo.Month;
            double prevMonthIncome = DbHelper.db.Incomes
                .AsEnumerable()
                .Where(i => accountIds.Contains(i.AccountId) &&
                         DateTime.Parse(i.IncomeDate).Year == yearMonthAgo &&
                         DateTime.Parse(i.IncomeDate).Month == monthAgo)
                .Sum(i => i.IncomeSum);

            return prevMonthIncome;
        }

        public static double CurrentIncome()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            double currentMonthIncome = DbHelper.db.Incomes
                .AsEnumerable()
                .Where(i => accountIds.Contains(i.AccountId) &&
                         DateTime.Parse(i.IncomeDate).Year == currentDate.Year &&
                         DateTime.Parse(i.IncomeDate).Month == currentDate.Month)
                .Sum(i => i.IncomeSum);

            return currentMonthIncome;
        }

        public static List<Income> GetIncomesByUserId()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            return DbHelper.db.Incomes
                .Where(i => accountIds.Contains(i.AccountId))
                .ToList();
        }

        //public static void AddIncome(string category, int sum)
        //{
        //    int? currUser = SessionManager.CurrentUserId;
        //    if(currUser == null)
        //    {
        //        throw new Exception("Для додавання доходів необхідно авторизуватися.");
        //    }
        //    else
        //    {
        //        var income = new Income
        //        {
        //            Category = category,
        //            IncomeDate = DateTime.Now.ToString(),
        //            IncomeSum = sum,
        //        };
        //    }
        //}

        //public static void AddExpense(string categoryName)
        //{
        //    int? currUser = SessionManager.CurrentUserId;
        //    if (currUser == null)
        //    {
        //        throw new Exception("Авторизуйтесь для додавання категорії.");
        //    }
        //    else
        //    {
        //        var category = DbHelper.db.ExpensesCategories.FirstOrDefault(c => c.Name == categoryName);
        //        if (category == null)
        //        {
        //            category = new ExpenseCategory
        //            {
        //                Name = categoryName,
        //                UserId = SessionManager.CurrentUserId.Value
        //            };
        //            DbHelper.db.ExpensesCategories.Add(category);
        //            DbHelper.db.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            throw new Exception("Категорія вже існує!");
        //        }
        //    }
        //}

        //public static void DeleteExpense(int categoryId)
        //{
        //    var category = DbHelper.db.ExpensesCategories.Find(categoryId);
        //    if (category.UserId == SessionManager.CurrentUserId)
        //    {
        //        DbHelper.db.ExpensesCategories.Remove(category);
        //        DbHelper.db.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        throw new Exception("Такої категорії не існує!");
        //    }
        //}

    }
}
