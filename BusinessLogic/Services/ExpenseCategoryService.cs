using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Services
{
    public class ExpenseCategoryService
    {
        public static List<ExpenseCategory> GetCategories()
        {
            return DbHelper.db.ExpensesCategories
                    .Where(ec => ec.UserId == SessionManager.CurrentUserId)
                    .ToList();
        }

        public static string GetCategoryName(int categoryId)
        {
            return DbHelper.db.ExpensesCategories
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
                var category = DbHelper.db.ExpensesCategories.FirstOrDefault(c => c.Name == categoryName);
                if (category == null)
                {
                    category = new ExpenseCategory
                    {
                        Name = categoryName,
                        UserId = SessionManager.CurrentUserId.Value
                    };
                    DbHelper.db.ExpensesCategories.Add(category);
                    DbHelper.db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Категорія вже існує!");
                }   
            }
        }

        public static void DeleteExpense(int categoryId)
        {
            var category = DbHelper.db.ExpensesCategories.Find(categoryId);
            if (category.UserId == SessionManager.CurrentUserId)
            {
                DbHelper.db.ExpensesCategories.Remove(category);
                DbHelper.db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Такої категорії не існує!");
            }
        }
    }
}
