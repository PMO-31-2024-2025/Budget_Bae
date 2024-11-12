using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("expenses")]
    public class Expense
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [Column("expense_date")]
        public string ExpenseDate { get; set; } = null!;
        [Column("expense_sum")]
        public double ExpenseSum { get; set; }
        [Column("account_id")]
        public int AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]

        DbSet<ExpenseCategory> ExpensesCategories { get; set; }
        DbSet<Account> Accounts { get; set; }

    }
}

//CREATE TABLE expenses (
//	id INTEGER PRIMARY KEY AUTOINCREMENT,
//    category_id INTEGER NOT NULL,
//    expense_date TEXT NOT NULL,
//    expense_sum REAL,
//    account_id INTEGER,
//    FOREIGN KEY (account_id) REFERENCES accounts(id),
//    FOREIGN KEY (category_id) REFERENCES expenses_categories(id)
//);