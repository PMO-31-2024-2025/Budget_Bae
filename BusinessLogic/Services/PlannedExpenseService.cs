// <copyright file="PlannedExpenseService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;

    public static class PlannedExpenseService
    {
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
                    throw new Exception("Задана запланована витрата вже існує!");
                }
            }

            return true;
        }

        public static async Task<bool> DeletePlannedExpense(int expenseId)
        {
            int? currUser = SessionManager.CurrentUserId;
            if (currUser != null)
            {
                throw new Exception("Вам нічого видаляти.");
            }
            else
            {
                List<PlannedExpense> currUserData = DbHelper.dbc.PlannedExpenses.Where(p => p.UserId == currUser).ToList();

                var plannedExpenseToDelete = currUserData.FirstOrDefault(x => x.Id == expenseId);
                if (plannedExpenseToDelete != null && plannedExpenseToDelete.UserId == SessionManager.CurrentUserId)
                {
                    DbHelper.dbc.PlannedExpenses.Remove(plannedExpenseToDelete);
                    await DbHelper.dbc.SaveChangesAsync();
                }
            }

            return true;
        }
    }
}
