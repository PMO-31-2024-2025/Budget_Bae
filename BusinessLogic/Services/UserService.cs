﻿// <copyright file="UserService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BusinessLogic.Services
{
    using BusinessLogic.Session;
    using DAL.Data;
    using DAL.Models;
    using System.Data.Entity;

    public class UserService
    {
        public UserService(BudgetBaeContext context) { }

        public void RegisterUser(string email, string password, string name)
        {
            if (DbHelper.dbс.Users.Any(u => u.Email == email))
            {
                throw new Exception("Користувач з такою електронною поштою вже існує!");
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Name = name,
            };

            DbHelper.dbс.Users.Add(user);
            DbHelper.dbс.SaveChangesAsync();
        }

        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            var user = await DbHelper.dbс.Users
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
            var existingUser = await DbHelper.dbс.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;

                await DbHelper.dbс.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await DbHelper.dbс.Users.FindAsync(userId);
            if (user != null)
            {
                DbHelper.dbс.Users.Remove(user);
                await DbHelper.dbс.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Користувача не знайдено.");
            }
        }

        public async Task<int?> AuthorizeUserAsync(string email, string password)
        {
            var user = await DbHelper.dbс.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user?.Id;
        }
    }
}
