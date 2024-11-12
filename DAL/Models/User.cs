using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
        [Column("email")]
        public string Email { get; set; } = null!;
        [Column("password")]
        public string Password { get; set; } = null!;

    }
}

//CREATE TABLE users (
//	id INTEGER PRIMARY KEY AUTOINCREMENT,
//    name TEXT NOT NULL,
//    email TEXT NOT NULL,
//    password TEXT NOT NULL
//);