// <copyright file="SavingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;

    public class SavingService
    {
        public static List<Saving> GetSavings()
        {
            return DbHelper.dbс.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .ToList();
        }

        public static double GetTotalSavings()
        {
            return DbHelper.dbс.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .Sum(s => s.TargetSum / s.MonthsNumber);
        }

        public static void AddSaving(string name, int sum, int monthsNumber)
        {
            // реалізація
        }
    }
}
