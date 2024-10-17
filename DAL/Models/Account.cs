using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? CardNumber { get; set; }

    public double? Balance { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual User? User { get; set; }
}
