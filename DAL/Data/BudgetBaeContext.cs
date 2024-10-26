using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public partial class BudgetBaeContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpensesCategories { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<PlannedExpense> PlannedExpenses { get; set; }
        public DbSet<Saving> Savings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\BudgetBae\DAL\DataBase\BudgetBaeDB.db;");
        }
    }
}
