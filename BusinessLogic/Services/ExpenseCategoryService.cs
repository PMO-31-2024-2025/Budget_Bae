using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ExpenseCategoryService
    {
        public List<ExpenseCategory> GetCategories()
        {
            return DbHelper.db.ExpensesCategories
                    .Where(ec => ec.UserId == SessionManager.CurrentUserId)
                    .ToList();
        }
    }
}
