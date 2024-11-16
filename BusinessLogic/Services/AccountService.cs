// <copyright file="AccountService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Runtime.CompilerServices;

    public static class AccountService
    {
        public static List<int> GetUsersAccountsId()
        {
            return DbHelper.dbс.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .Select(a => a.Id)
                .ToList();
        }

        public static List<Account> GetUsersAccounts()
        {
            return DbHelper.dbс.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .ToList();
        }

        public static string? GetAccountName(int accountId)
        {
            return DbHelper.dbс.Accounts
                .Where(a => a.Id == accountId)
                .Select(a => a.Name)
                .FirstOrDefault();
        }

        public static async Task AddAccount(string name, double balance)
        {
            var currentUserAccounts = DbHelper.dbс.Accounts.Where(x => x.UserId == SessionManager.CurrentUserId).ToList();
            if (currentUserAccounts.FirstOrDefault(x => x.Name == name) == null)
            {
#pragma warning disable CS8629 // Nullable value type may be null.
                var account = new Account
                {
                    Name = name,
                    Balance = balance,
                    UserId = SessionManager.CurrentUserId.Value
                };
#pragma warning restore CS8629 // Nullable value type may be null.
                await DbHelper.dbс.Accounts.AddAsync(account);
                await DbHelper.dbс.SaveChangesAsync();
            }
        }
    }
}
