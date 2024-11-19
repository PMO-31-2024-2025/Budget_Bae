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
        public async void DeleteUser_ShouldThrowExceptionWhenUserDoesntExist()
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
        
        [Fact]
        public async Task AddAccountIncomeExpenseToUser38_ShouldAddAccountIncomeExpenseToUser38()
        {
            SessionManager.ClearCurrentUser();
            SessionManager.SetCurrentUser(38);
            var user = DbHelper.dbc.Users.FirstOrDefault(x => x.Id == 38);
            Assert.NotNull(user);
            Assert.Equal(SessionManager.CurrentUserId, 38);
            string accName = "MyWallet";
            await AccountService.AddAccountAsync(accName, 350);
            var account = DbHelper.dbc.Accounts.FirstOrDefault(x => x.Name == accName && x.UserId == SessionManager.CurrentUserId);
            Assert.NotNull(account);
            Assert.Equal(350, account.Balance);

            SessionManager.SetCurrentAccount(account.Id);
            
            var newIncome = new Income
            {
                IncomeSum = 250,
                AccountId = account.Id,
                IncomeDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Category = "Їжа"
            };

            await IncomeService.AddIncomeAsync(newIncome);
            var income = DbHelper.dbc.Incomes.
                FirstOrDefault(i => i.AccountId == SessionManager.CurrentAccountId && i.Category == "Їжа");
            Assert.NotNull(income);
            SessionManager.ClearCurrentAccount();
            Assert.Equal(-1, SessionManager.CurrentAccountId);

            await AccountService.DeleteAccountAsync(accName);
            var deleted_account = DbHelper.dbc.Accounts.FirstOrDefault(x => x.Name == accName && x.UserId == SessionManager.CurrentUserId);
            Assert.Null(deleted_account);
        }
    }
}