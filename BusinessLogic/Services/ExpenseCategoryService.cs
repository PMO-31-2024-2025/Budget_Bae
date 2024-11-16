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
            return DbHelper.dbс.ExpensesCategories
                    .Where(ec => ec.UserId == SessionManager.CurrentUserId)
                    .ToList();
        }

        public static string? GetCategoryName(int categoryId)
        {
            return DbHelper.dbс.ExpensesCategories
                .Where(ec => ec.Id == categoryId)
                .Select(ec => ec.Name)
                .FirstOrDefault();
        }

        public static void AddExpense(string categoryName)
        {
            int? currUser = SessionManager.CurrentUserId;
            if (currUser == null)
            {
                throw new Exception("Авторизуйтесь для додавання категорії.");
            }
            else
            {
                var category = DbHelper.dbс.ExpensesCategories.FirstOrDefault(c => c.Name == categoryName);
                if (category == null && SessionManager.CurrentUserId != null)
                {
                    category = new ExpenseCategory
                    {
                        Name = categoryName,
                        UserId = SessionManager.CurrentUserId.Value,
                    };
                    DbHelper.dbс.ExpensesCategories.Add(category);
                    DbHelper.dbс.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Категорія вже існує!");
                }
            }
        }

        public static void DeleteExpense(int categoryId)
        {
            var category = DbHelper.dbс.ExpensesCategories.Find(categoryId);
            if (category != null && category.UserId == SessionManager.CurrentUserId)
            {
                DbHelper.dbс.ExpensesCategories.Remove(category);
                DbHelper.dbс.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Такої категорії не існує!");
            }
        }
    }
}
