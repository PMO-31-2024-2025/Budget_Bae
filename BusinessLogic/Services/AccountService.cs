// <copyright file="AccountService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Runtime.CompilerServices;

    public class AccountService
    {
        private readonly BudgetBaeContext context;

        public AccountService(BudgetBaeContext context)
        {
            this.context = context;
        }

        public void AddAccount()
        {
            //DbHelper.db.Accounts.Add(new Account());
        }

        public static List<int> GetUsersAccountsId()
        {
            return DbHelper.db.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .Select(a => a.Id)
                .ToList();
        }

        public static List<Account> GetUsersAccounts()
        {
            return DbHelper.db.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .ToList();
        }

        public static string? GetAccountName(int accountId)
        {
            return DbHelper.db.Accounts
                .Where(a => a.Id == accountId)
                .Select(a => a.Name)
                .FirstOrDefault();
        }
    }
}
