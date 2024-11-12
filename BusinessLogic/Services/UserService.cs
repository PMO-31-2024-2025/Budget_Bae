// <copyright file="UserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using System.Data.Entity;
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;

    public class UserService
    {
        private readonly BudgetBaeContext context;

        public UserService(BudgetBaeContext context)
        {
            this.context = context;
        }

        public void RegisterUser(string email, string password, string name)
        {
            if (this.context.Users.Any(u => u.Email == email))
            {
                throw new Exception("Користувач з такою електронною поштою вже існує!");
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Name = name,
            };

            this.context.Users.Add(user);
            this.context.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await this.context.Users
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
            var existingUser = await this.context.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;

                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await this.context.Users.FindAsync(userId);
            if (user != null)
            {
                this.context.Users.Remove(user);
                await this.context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public async Task<int?> AuthorizeUserAsync(string email, string password)
        {
            var user = await this.context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user?.Id;
        }
    }
}
