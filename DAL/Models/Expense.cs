using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Expense
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string ExpenseDate { get; set; } = null!;

    public double? ExpenseSum { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ExpensesCategory Category { get; set; } = null!;
}
