// <copyright file="ExpenseCategoryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using Microsoft.Extensions.Logging;
    using System.Xml.Linq;

    public static class ExpenseCategoryService
    {
        private static ILogger logger;

        public static void InitializeLogger(ILogger logger)
        {
            ExpenseCategoryService.logger = logger;
        }
        public static List<ExpenseCategory> GetCategories()
        {
            return DbHelper.dbc.ExpensesCategories
                    .Where(ec => ec.UserId == SessionManager.CurrentUserId)
                    .ToList();
        }

        public static string? GetCategoryName(int categoryId)
        {
            return DbHelper.dbc.ExpensesCategories
                .Where(ec => ec.Id == categoryId)
                .Select(ec => ec.Name)
                .FirstOrDefault();
        }

        public static async Task<bool> AddExpensCategoryAsync(string categoryName)
        {
            logger?.LogInformation($"Спроба додати категорію {categoryName}.");

            int? currUser = SessionManager.CurrentUserId;
            var currUserCategories = ExpenseCategoryService.GetCategories().Where(x => x.UserId == currUser);
            var categoryToAdd = currUserCategories.FirstOrDefault(c => c.Name == categoryName);

            if (categoryToAdd == null && SessionManager.CurrentUserId != null)
            {
                categoryToAdd = new ExpenseCategory
                {
                    Name = categoryName,
                    UserId = SessionManager.CurrentUserId.Value,
                };
                DbHelper.dbc.ExpensesCategories.Add(categoryToAdd);
                await DbHelper.dbc.SaveChangesAsync();
            }
            else
            {
                logger?.LogWarning("Категорія вже існує!");
                throw new Exception("Категорія вже існує!");
            }
            logger?.LogInformation("Категорію додано.");
            return true;
        }

        public static async Task<bool> DeleteExpenseCategory(int categoryId)
        {
            logger?.LogInformation($"Спроба видалити категорію з ID {categoryId}.");

            var category = DbHelper.dbc.ExpensesCategories.Find(categoryId);
            if (category != null && category.UserId == SessionManager.CurrentUserId)
            {
                DbHelper.dbc.ExpensesCategories.Remove(category);
                await DbHelper.dbc.SaveChangesAsync();
            }
            else
            {
                logger?.LogWarning("Такої категорії не існує!");
                throw new Exception("Такої категорії не існує!");
            }
            logger?.LogInformation("Категорію видалено.");
            return true;
        }
    }
}
