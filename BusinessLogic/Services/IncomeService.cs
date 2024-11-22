// <copyright file="IncomeService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.Extensions.Logging;

    public static class IncomeService
    {
        private static ILogger logger;

        public static void InitializeLogger(ILogger logger)
        {
            IncomeService.logger = logger;
        }

        public static double PrevPrevIncome()
        {
            var accountIds = AccountService.GetUsersAccountsId();

            var currentDate = DateTime.Now;
            var dateTwoMonthsAgo = currentDate.AddMonths(-2);
            var yearTwoMonthsAgo = dateTwoMonthsAgo.Year;
            var monthTwoMonthsAgo = dateTwoMonthsAgo.Month;
            double prevPrevMonthIncome = DbHelper.dbc.Incomes
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
            double prevMonthIncome = DbHelper.dbc.Incomes
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
            double currentMonthIncome = DbHelper.dbc.Incomes
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

            return DbHelper.dbc.Incomes
                .Where(i => accountIds.Contains(i.AccountId))
                .ToList();
        }

        public static async Task<bool> AddIncomeAsync(string category, double incomeSum, int accountId)
        {
            logger?.LogInformation($"Спроба внести дохід з сумою {incomeSum}.");
            var income = new Income
            {
                Category = category,
                IncomeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                IncomeSum = incomeSum,
                AccountId = accountId,
            };
            var account = DbHelper.dbc.Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                throw new Exception("Рахунок не знайдено!");
            }

            DbHelper.dbc.Incomes.Add(income);
            account.Balance += incomeSum;
            await DbHelper.dbc.SaveChangesAsync();

            logger?.LogInformation("Дохід внесено успішно.");
            return true;
        }


        public static async Task<bool> DeleteIncomeAsync(int incomeId)
        {
            logger?.LogInformation($"Спроба видалити дохід з ID {incomeId}.");

            var income = GetIncomesByUserId().FirstOrDefault(i => i.Id == incomeId);
            if (income == null)
            {
                logger?.LogWarning("Вказане надходження не знайдено!");
                throw new Exception("Вказане надходження не знайдено!");
            }
            else
            {
                DbHelper.dbc.Incomes.Remove(income);
                await DbHelper.dbc.SaveChangesAsync();
            }
            logger?.LogInformation("Дохід видалено.");
            return true;
        }
    }
}
