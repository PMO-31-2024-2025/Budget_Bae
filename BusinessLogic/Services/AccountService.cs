using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountService
    {
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

        public static string GetAccountName(int accountId)
        {
            return DbHelper.db.Accounts
                .Where(a => a.Id == accountId)
                .Select(a => a.Name)
                .FirstOrDefault();
        }
    }
}
