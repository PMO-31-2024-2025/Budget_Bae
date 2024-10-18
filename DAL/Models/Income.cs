using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Income
{
    public int Id { get; set; }

    public string Category { get; set; } = null!;

    public string IncomeDate { get; set; } = null!;

    public double IncomeSum { get; set; }

    public int? AccountId { get; set; }

    public virtual Account? Account { get; set; }
}
