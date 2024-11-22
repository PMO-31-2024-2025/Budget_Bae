// <copyright file="SavingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.Extensions.Logging;

    public static class SavingService
    {
        private static ILogger logger;

        public static void InitializeLogger(ILogger logger)
        {
            SavingService.logger = logger;
        }

        public static List<Saving> GetSavings()
        {
            return DbHelper.dbc.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .ToList();
        }

        public static double GetTotalSavings()
        {
            return DbHelper.dbc.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .Sum(s => s.TargetSum / s.MonthsNumber);
        }

        public static async Task<bool> AddSavingAsync(string targetName, int targetSum, int monthsNumber)
        {
            logger?.LogInformation($"Спроба додати заощадження {targetName}.");

            var currUserSavings = DbHelper.dbc.Savings.Where(x => x.UserId == SessionManager.CurrentUserId);
            var saving = currUserSavings.FirstOrDefault(x => x.TargetName == targetName);
            if (saving != null)
            {
                logger?.LogWarning("Заощадження із таким іменем вже існує!");
                throw new Exception("Заощадження із таким іменем вже існує!");
            }
            else
            {
                if (SessionManager.CurrentUserId != null)
                {
                    saving = new Saving
                    {
                        TargetName = targetName,
                        TargetSum = targetSum,
                        MonthsNumber = monthsNumber,
                        UserId = SessionManager.CurrentUserId.Value,
                        CurrentSum = 0
                    };

                    DbHelper.dbc.Savings.Add(saving);
                    await DbHelper.dbc.SaveChangesAsync();
                }
            }
            logger?.LogInformation("Заощадження успішно додано.");
            return true;
        }

        public static async Task<bool> DeleteSavingAsync(int savingId)
        {
            logger?.LogInformation($"Спроба видалити заощадження з ID {savingId}.");

            var currentUserSavings = DbHelper.dbc.Savings.Where(s => s.UserId == SessionManager.CurrentUserId.Value);
            var saving = currentUserSavings.FirstOrDefault(s => s.Id == savingId);
            if (saving == null)
            {
                logger?.LogWarning("Указане заощадження не знайдено!");
                throw new Exception("Указане заощадження не знайдено!");
            }
            else
            {
                DbHelper.dbc.Savings.Remove(saving);
                await DbHelper.dbc.SaveChangesAsync();
            }
            logger?.LogInformation("Заощадження видалено.");
            return true;
        }
    }
}