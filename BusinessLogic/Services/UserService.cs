// <copyright file="UserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Data.Entity;
    using Microsoft.Extensions.Logging;

    public static class UserService
    {
        private static ILogger logger;

        public static void InitializeLogger(ILogger logger)
        {
            UserService.logger = logger;
        }

        public static async Task<bool> RegisterUserAsync(string email, string password, string name)
        {
            logger?.LogInformation("Спроба реєстрації користувача з поштою: {Email}.", email);

            if (DbHelper.dbc.Users.Any(u => u.Email == email))
            {
                logger?.LogWarning($"Користувач з такою поштою вже існує.\n");
                throw new Exception("Користувач з такою електронною поштою вже існує!");
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Name = name,
            };
            DbHelper.dbc.Users.Add(user);
            await DbHelper.dbc.SaveChangesAsync();

            logger?.LogInformation($"Користувача успішно додано.\n");
            return true;
        }

        public static async Task<bool> DeleteUserAsync(int userId)
        {
            logger?.LogInformation("Спроба видалити користувача з ID {id}.", userId);

            var user = await DbHelper.dbc.Users.FindAsync(userId);
            if (user != null)
            {
                DbHelper.dbc.Users.Remove(user);
                await DbHelper.dbc.SaveChangesAsync();
            }
            else
            {
                logger?.LogWarning($"Користувача не знайдено.\n");
                throw new Exception("Користувача не знайдено.");
            }
            logger?.LogInformation($"Користувача видалено.\n");
            return true;
        }

        public static async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await DbHelper.dbc.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                throw new Exception("Неправильна пошта, або пароль");
            }

            SessionManager.SetCurrentUser(user.Id);

            return user;
        }

        public static async Task UpdateUserAsync(User user)
        {
            var existingUser = await DbHelper.dbc.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;

                await DbHelper.dbc.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public static async Task<int?> AuthorizeUserAsync(string email, string password)
        {
            var user = await DbHelper.dbc.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user?.Id;
        }

        public static int GetUserIdByEmail(string email)
        {
            var user = DbHelper.dbc.Users.First(u => u.Email == email);
            return user.Id;
        }
    }
}
