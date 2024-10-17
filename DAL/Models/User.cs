using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<ExpensesCategory> ExpensesCategories { get; set; } = new List<ExpensesCategory>();

    public virtual ICollection<PlannedExpense> PlannedExpenses { get; set; } = new List<PlannedExpense>();

    public virtual ICollection<Saving> Savings { get; set; } = new List<Saving>();
}
