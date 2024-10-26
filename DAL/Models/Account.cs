﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("accounts")]
    public class Account
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("type")]
        public string Type { get; set; } = null!;
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("card_number")]
        public string CardNumber { get; set; }
        [Column("balance")]
        public double Balance { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public DbSet<User> Users { get; set; }
    }
}


//CREATE TABLE accounts (
//	id INTEGER PRIMARY KEY AUTOINCREMENT,
//    type TEXT NOT NULL,
//    name TEXT NOT NULL,
//    card_number TEXT,
//    balance REAL DEFAULT (0.0),
//    user_id INTEGER,
//    FOREIGN KEY (user_id) REFERENCES users(id)
//);