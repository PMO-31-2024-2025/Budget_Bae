﻿namespace DAL.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("expenses_categories")]
    public class ExpenseCategory
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public DbSet<User> User { get; set; }

    }
}

//CREATE TABLE expenses_categories (
//	id INTEGER PRIMARY KEY AUTOINCREMENT,
//    name TEXT NOT NULL,
//    user_id INTEGER,
//    FOREIGN KEY (user_id) REFERENCES users(id)
//);
