using BusinessLogic.Services;
using DAL.Data;
using DAL.Models;
using System.Reflection.Metadata;

namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void RegisterUserTest()
        {
            var result = UserService.RegisterUserAsync("testestest@mail.com", "11111111", "test");
            Assert.True(await result);
            User user = DbHelper.dbñ.Users.FirstOrDefault(x => x.Email == "testestest@mail.com");
            Assert.NotNull(user);
            int id = user.Id;
            await UserService.DeleteUserAsync(id);
            var deletedUser = DbHelper.dbñ.Users.FirstOrDefault(u => u.Id == id);
            Assert.Null(deletedUser);
        }
    }
}