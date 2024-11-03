using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("savings")]
    public class Saving
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("target_name")]
        public string TargetName { get; set; } = null!;
        [Column("target_sum")]
        public double TargetSum { get; set; }
        [Column("current_sum")]
        public double CurrentSum { get; set; }
        [Column("months_number")]
        public int MonthsNumber { get; set; } 
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        DbSet<User> Users { get; set; }

    }


}



//CREATE TABLE savings (
//	id INTEGER PRIMARY KEY,
//    target_name TEXT NOT NULL,
//    target_sum REAL NOT NULL,
//    current_sum REAL DEFAULT(0.0),
//    end_date TEXT NOT NULL,
//    user_id INTEGER,
//    FOREIGN KEY (user_id) REFERENCES users(id)
//);