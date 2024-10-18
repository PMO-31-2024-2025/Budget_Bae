using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ExpensesCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual User? User { get; set; }
}
