namespace DAL.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("planned_expenses")]
    public class PlannedExpense
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("planned_sum")]
        public double PlannedSum { get; set; }
        [Column("notification_date")]
        public int NotigicationDate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        DbSet<User> Users { get; set; }

    }
}


//CREATE TABLE planned_expenses (
//	id INTEGER PRIMARY KEY,
//    name TEXT NOT NULL,
//    planned_sum REAL NOT NULL,
//    notification_date INTEGER NOT NULL,
//    user_id INTEGER,
//    FOREIGN KEY (user_id) REFERENCES users(id)
//);