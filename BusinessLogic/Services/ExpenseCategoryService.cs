// <copyright file="ExpenseCategoryService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;

    public static class ExpenseCategoryService
    {
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

        public static async Task<bool> AddExpenseAsync(string categoryName)
        {
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
                throw new Exception("Категорія вже існує!");
            }

            return true;
        }

        public static void DeleteExpenseCategory(int categoryId)
        {
            var category = DbHelper.dbc.ExpensesCategories.Find(categoryId);
            if (category != null && category.UserId == SessionManager.CurrentUserId)
            {
                DbHelper.dbc.ExpensesCategories.Remove(category);
                DbHelper.dbc.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Такої категорії не існує!");
            }
        }
    }
}
