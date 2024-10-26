using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class DbHelper
    {
        public static BudgetBaeContext db = new BudgetBaeContext();
    }
}
