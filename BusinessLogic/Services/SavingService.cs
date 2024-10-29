﻿using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SavingService
    {
        public static List<Saving> GetSavings()
        {
            return DbHelper.db.Savings
                .Where(s => s.UserId == SessionManager.CurrentUserId)
                .ToList();
        }
    }
}
