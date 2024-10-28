using BusinessLogic.Session;
using DAL.Data;
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
    }
}
