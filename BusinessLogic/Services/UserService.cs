using BusinessLogic.Session;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly BudgetBaeContext _context;

        public UserService(BudgetBaeContext context)
        {
            _context = context;
        }

        public void RegisterUser(string email, string password, string name)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                throw new Exception("Користувач з такою електронною поштою вже існує!");
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Name = name
            };

            _context.Users.Add(user);
            _context.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                throw new Exception("Неправильна пошта, або пароль");
            }

            SessionManager.SetCurrentUser(user.Id);

            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public async Task<int?> AuthorizeUserAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user?.Id;
        }
    }
}
