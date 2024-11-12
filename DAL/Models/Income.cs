namespace DAL.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("incomes")]
    public class Income
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("category")]
        public string Category { get; set; } = null!;
        [Column("income_date")]
        public string IncomeDate { get; set; } = null!;
        [Column("income_sum")]
        public double IncomeSum { get; set; }
        [Column("account_id")]
        public int AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]

        DbSet<Account> Accounts { get; set; }

    }
}

//CREATE TABLE incomes (
//	id INTEGER PRIMARY KEY AUTOINCREMENT,
//    category TEXT NOT NULL,
//    income_date TEXT NOT NULL,
//    income_sum REAL NOT NULL,
//    account_id INTEGER,
//    FOREIGN KEY (account_id) REFERENCES accounts(id)
//);