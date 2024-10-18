using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Saving
{
    public int Id { get; set; }

    public string TargetName { get; set; } = null!;

    public double TargetSum { get; set; }

    public double? CurrentSum { get; set; }

    public string EndDate { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
