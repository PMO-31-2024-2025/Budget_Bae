// <copyright file="AccountService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;

    public static class AccountService
    {
        public static List<int> GetUsersAccountsId()
        {
            return DbHelper.dbc.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId)
                .Select(a => a.Id)
                .ToList();
        }

        public static List<Account> GetCurrentUserAccounts()
        {
            return DbHelper.dbc.Accounts
                .Where(a => a.UserId == SessionManager.CurrentUserId.Value)
                .ToList();
        }

        public static string? GetAccountName(int accountId)
        {
            return DbHelper.dbc.Accounts
                .Where(a => a.Id == accountId)
                .Select(a => a.Name)
                .FirstOrDefault();
        }

        public static async Task<bool> AddAccountAsync(string name, double balance)
        {
            var currentUserAccounts = DbHelper.dbc.Accounts.Where(x => x.UserId == SessionManager.CurrentUserId);
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
                DbHelper.dbc.Accounts.Add(account);
                await DbHelper.dbc.SaveChangesAsync();
            }
            return true;
        }

        public static async Task<bool> DeleteAccountAsync(int accountId)
        {
            var account = GetCurrentUserAccounts().FirstOrDefault(a => a.Id == accountId);
            if (account == null)
            {
                throw new Exception("Такого рахунку не існує!");
            }
            else
            {
                DbHelper.dbc.Accounts.Remove(account);
                await DbHelper.dbc.SaveChangesAsync();
            }
            return true;
        }

        public static Account? GetAccountById(int accountId)
        {
            return DbHelper.dbc.Accounts.FirstOrDefault(x => x.Id == accountId);
        }
    }
}
