// <copyright file="PlannedExpenseService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.Extensions.Logging;

    public static class PlannedExpenseService
    {
        private static ILogger logger;

        public static void InitializeLogger(ILogger logger)
        {
            PlannedExpenseService.logger = logger;
        }

        public static List<PlannedExpense> GetPlannedExpenses()
        {
            return DbHelper.dbc.PlannedExpenses
                .Where(p => p.UserId == SessionManager.CurrentUserId)
                .ToList();
        }

        public static double GetPaymentsAmount()
        {
            return DbHelper.dbc.PlannedExpenses
                .Where(p => p.UserId == SessionManager.CurrentUserId)
                .Sum(p => p.PlannedSum);
        }

        public static string? GetPlannedExpenseName(int plannedExpenseId)
        {
            return DbHelper.dbc.PlannedExpenses
                .Where(p => p.Id == plannedExpenseId)
                .Select(p => p.Name)
                .FirstOrDefault();
        }

        public static async Task<bool> AddPlannedExpense(string expenseName, int notification_date, double plannedSum)
        {
            int? currUser = SessionManager.CurrentUserId;
            List<PlannedExpense> currUserData = DbHelper.dbc.PlannedExpenses.Where(p => p.UserId == currUser).ToList();

            if (currUser != null)
            {
                var plannedExpense = DbHelper.dbc.PlannedExpenses.FirstOrDefault(c => c.Name == expenseName);
                if (plannedExpense == null)
                {
                    plannedExpense = new PlannedExpense
                    {
                        Name = expenseName,
                        NotigicationDate = notification_date,
                        PlannedSum = plannedSum,
                        UserId = currUser.Value,
                    };
                    DbHelper.dbc.PlannedExpenses.Add(plannedExpense);
                    await DbHelper.dbc.SaveChangesAsync();
                }
                else
                {
                    logger?.LogWarning("Заданий запланований платіж вже існує!");
                    throw new Exception("Заданий запланований платіж вже існує!");
                }
            }
            logger?.LogInformation("Запланований платіж додано.");
            return true;
        }

        public static async Task<bool> DeletePlannedExpense(int expenseId)
        {
            var currUserPlannedExpenses = PlannedExpenseService.GetPlannedExpenses();
            var expenseToDelete = currUserPlannedExpenses.FirstOrDefault(pe => pe.Id == expenseId);
            if (expenseToDelete == null)
            {
                logger?.LogWarning("Заданий запланований платіж не знайдено!");
                throw new Exception("Заданий запланований платіж не знайдено!");
            }
            else
            {
                List<PlannedExpense> currUserData = DbHelper.dbc.PlannedExpenses.
                    Where(p => p.UserId == SessionManager.CurrentUserId).ToList();

                var plannedExpenseToDelete = currUserData.FirstOrDefault(x => x.Id == expenseId);
                if (plannedExpenseToDelete != null && plannedExpenseToDelete.UserId == SessionManager.CurrentUserId)
                {
                    DbHelper.dbc.PlannedExpenses.Remove(plannedExpenseToDelete);
                    DbHelper.dbc.SaveChanges();
                }
            }
            logger?.LogInformation("Запланований платіж видалено.");
            return true;
        }
    }
}
