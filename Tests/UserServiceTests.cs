using Accessibility;
using BusinessLogic.Services;
using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System.Reflection.Metadata;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void DeletUser_ShouldThrowExceptionWhenUserDoesntExist()
        {
            await Assert.ThrowsAsync<Exception>(() => UserService.DeleteUserAsync(1000000));
        }

        [Fact]
        public async void RegisterUserTest_ShouldThrowException_WhenEmailExists()
        {
            var existingUser = new User { Email = "testmail@mail.com", Password = "Password123", Name = "Test User" };
            DbHelper.dbc.Users.Add(existingUser);
            await DbHelper.dbc.SaveChangesAsync();

            var exception = await Assert.ThrowsAsync<Exception>(() => UserService.RegisterUserAsync("testmail@mail.com", "Password123", "New User"));
            Assert.Equal("Користувач з такою електронною поштою вже існує!", exception.Message);
            await UserService.DeleteUserAsync(UserService.GetUserIdByEmail("testmail@mail.com"));
        }

        [Fact]
        public async void RegisterUserAsync_ShouldAddUserWhenEmailDoesNotExist()
        {
            string email = "testusermail@mail.com";
            string password = "Password123";
            string name = "New User";

            await UserService.RegisterUserAsync(email, password, name);
            var user = DbHelper.dbc.Users.FirstOrDefault(u => u.Email == email);

            Assert.NotNull(user);
            Assert.Equal(name, user.Name);
            await UserService.DeleteUserAsync(UserService.GetUserIdByEmail(email));
        }
        //[Fact]
        //public async Task AddDataToCurrentUser()
        //{
        //    var email = "big.smoke@gta.sa";
        //    var password = "password";
        //    var name = "Smoke";

        //    var result1 = UserService.RegisterUserAsync(email, password, name);

        //    var categoryName = "Oceanic Fuel";
        //    var accountName = "Gangsta case";
        //    var initialBalance = 500000;
        //    var userId = UserService.GetUserIdByEmail(email);
        //    SessionManager.SetCurrentUser(userId);
        //    Assert.Equal(SessionManager.CurrentUserId, userId);

        //    var result2 = ExpenseCategoryService.AddExpenseAsync(categoryName);
        //    Assert.True(result2.Result);
        //    var category = ExpenseCategoryService.GetCategories().First();

        //    var result3 = AccountService.AddAccountAsync(accountName, initialBalance);
        //    Assert.True(result3.Result);
        //    var account = AccountService.GetCurrentUserAccounts().First();

        //    await Assert.ThrowsAsync<Exception>(() => ExpenseCategoryService.AddExpenseAsync(categoryName));
        //    await Assert.ThrowsAsync<Exception>(() => AccountService.AddAccountAsync(accountName, 23));

        //    DbHelper.dbc.Accounts.Remove(account);
        //    DbHelper.dbc.ExpensesCategories.Remove(category);
        //    await DbHelper.dbc.SaveChangesAsync();

        //    await UserService.DeleteUserAsync(userId);
        //}

        [Fact]
        public async Task AddAccountToUser38_ShouldAddAccount()
        {
            SessionManager.ClearCurrentUser();
            SessionManager.SetCurrentAccount(38);
            var user = DbHelper.dbc.Users.FirstOrDefault(x => x.Id == 38);
            Assert.NotNull(user);
            Assert.Equal(SessionManager.CurrentAccountId, 38);
            string accName = "TheLeatherWallet";
            await AccountService.AddAccountAsync(accName, 350);
            var account = DbHelper.dbc.Accounts.FirstOrDefault(x => x.Name == accName && x.UserId == SessionManager.CurrentUserId);
            Assert.NotNull(account);
            Assert.Equal(350, account.Balance);

            IncomeService.


            await AccountService.DeleteAccountAsync(accName);
            var deleted_account = DbHelper.dbc.Accounts.FirstOrDefault(x => x.Name == accName && x.UserId == SessionManager.CurrentUserId);
            Assert.Null(deleted_account);
        }
    }
}