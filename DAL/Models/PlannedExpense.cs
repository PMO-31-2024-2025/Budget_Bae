using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class PlannedExpense
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double PlannedSum { get; set; }

    public int NotificationDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
