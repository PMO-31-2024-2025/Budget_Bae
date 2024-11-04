﻿using BusinessLogic.Services;
using BusinessLogic.Session;
using DAL;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;

namespace ConsoleLevel
{
    class Program
    {
        static void Main(string[] args)
        {

            //DbHelper.db.Add(new Account() {Type="Карта", Name="усьо шо лишилось", CardNumber="1111222233334444", Balance=11678.09, UserId=21 });
            //DbHelper.db.Add(new Account() {Type="Готівіка", Name="усьо шо лишилось але в долярах", Balance=560.11, UserId=21 });
            //DbHelper.db.SaveChanges();

            //DbHelper.db.Add(new ExpenseCategory() {Name="кокальока", UserId=21});
            //DbHelper.db.Add(new ExpenseCategory() {Name="lovalova", UserId=21});
            //DbHelper.db.Add(new ExpenseCategory() {Name="на поїсти", UserId=21});
            //DbHelper.db.Add(new ExpenseCategory() {Name="овочі", UserId=21});
            //DbHelper.db.Add(new ExpenseCategory() {Name="подорож", UserId=21});
            //DbHelper.db.Add(new ExpenseCategory() {Name="котикам", UserId=21});
            //DbHelper.db.SaveChanges();

            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-09-01", ExpenseSum = 150.50, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 2, ExpenseDate = "2024-09-02", ExpenseSum = 200.75, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 3, ExpenseDate = "2024-09-03", ExpenseSum = 350.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 4, ExpenseDate = "2024-09-04", ExpenseSum = 120.25, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 5, ExpenseDate = "2024-09-05", ExpenseSum = 45.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 6, ExpenseDate = "2024-09-06", ExpenseSum = 75.80, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-09-07", ExpenseSum = 180.00, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-09-11", ExpenseSum = 180.00, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 6, ExpenseDate = "2024-09-16", ExpenseSum = 180.00, AccountId = 21 });
            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-09-06", ExpenseSum = 180.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 6, ExpenseDate = "2024-07-07", ExpenseSum = 1800.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-09-07", ExpenseSum = 90.00, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 4, ExpenseDate = "2024-09-07", ExpenseSum = 10.60, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 2, ExpenseDate = "2024-08-08", ExpenseSum = 18.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 6, ExpenseDate = "2024-06-18", ExpenseSum = 37.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-06-27", ExpenseSum = 800.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 2, ExpenseDate = "2024-09-17", ExpenseSum = 90.00, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 4, ExpenseDate = "2024-10-07", ExpenseSum = 340.00, AccountId = 1 });

            //DbHelper.db.Add(new Expense() { CategoryId = 7, ExpenseDate = "2024-11-01 16:30:00", ExpenseSum = 700.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 1, ExpenseDate = "2024-11-02 19:35:00", ExpenseSum = 200.00, AccountId = 2 });
            //DbHelper.db.Add(new Expense() { CategoryId = 2, ExpenseDate = "2024-11-01 13:25:00", ExpenseSum = 90.00, AccountId = 1 });
            //DbHelper.db.Add(new Expense() { CategoryId = 4, ExpenseDate = "2024-11-02 22:10:00", ExpenseSum = 950.00, AccountId = 1 });
            //DbHelper.db.SaveChanges();


            //DbHelper.db.Add(new Income() { Category = "Salary", IncomeDate = "2024-09-01", IncomeSum = 3000.00, AccountId = 1 });
            ////DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-09-10", IncomeSum = 12000.50, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-08-10", IncomeSum = 15000.50, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-08-23", IncomeSum = 7000.00, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-10-05", IncomeSum = 7000.00, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Salary", IncomeDate = "2024-10-23", IncomeSum = 20000.00, AccountId = 1 });

            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-11-02 15:00:00", IncomeSum = 2000.00, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Salary", IncomeDate = "2024-11-01 12:00:00", IncomeSum = 20000.00, AccountId = 1 });
            //DbHelper.db.SaveChanges();

            //DbHelper.db.Add(new Saving()
            //{
            //    TargetName = "На концерт бтс",
            //    TargetSum = 20000.00,
            //    CurrentSum = 2500.00,
            //    EndDate = "2025-10-31",
            //    UserId = 21
            //});
            //DbHelper.db.Add(new Saving()
            //{
            //    TargetName = "На рожевий 16 айфон",
            //    TargetSum = 45000.00,
            //    CurrentSum = 10000.00,
            //    EndDate = "2026-07-30",
            //    UserId = 21
            //});
            //DbHelper.db.SaveChanges();

            //DbHelper.db.Add(new PlannedExpense()
            //{
            //    Name = "спотіфай",
            //    PlannedSum = 120.00,
            //    NotigicationDate = "2024-11-01",
            //    UserId = 21
            //});

            //DbHelper.db.SaveChanges();


            //int accountCount = DbHelper.db.Accounts.Count();
            //Console.WriteLine($"Accounts: {accountCount}");
            //int expenseCount = DbHelper.db.Expenses.Count();
            //Console.WriteLine($"Expenses: {expenseCount}");
            //int savingCount = DbHelper.db.Savings.Count();
            //Console.WriteLine($"Savings: {savingCount}");
            //int plannedExpenseCount = DbHelper.db.PlannedExpenses.Count();
            //Console.WriteLine($"PlannedExpenses: {plannedExpenseCount}");

            //List<Account> accounts = DbHelper.db.Accounts
            //    .Where(a => a.UserId == 21)
            //    .ToList();

            //for (int i = 0; i < accounts.Count; i++)
            //{
            //    Console.WriteLine(accounts[i].Name);
            //    Console.WriteLine(accounts[i].Balance);
            //}

            //List<int> ids = DbHelper.db.Accounts
            //    .Where(a => a.UserId == 21)
            //    .Select(a => a.Id)
            //    .ToList();

            //for (int i = 0; i < ids.Count; i++)
            //{
            //    Console.WriteLine(ids[i]);
            //}

            //DbHelper.db.Add(new Saving()
            //{
            //    TargetName = "На концерт бтс",
            //    TargetSum = 20000.00,
            //    CurrentSum = 2500.00,
            //    MonthsNumber = 7,
            //    UserId = 21
            //});
            //DbHelper.db.Add(new Saving()
            //{
            //    TargetName = "На рожевий 16 айфон",
            //    TargetSum = 45000.00,
            //    CurrentSum = 10000.00,
            //    MonthsNumber = 10,
            //    UserId = 21
            //});
            //DbHelper.db.SaveChanges();

            //DbHelper.db.Add(new PlannedExpense()
            //{
            //    Name = "спотіфай",
            //    PlannedSum = 120.00,
            //    NotigicationDate = 01,
            //    UserId = 21
            //});

            //int plannedExpenseCount = DbHelper.db.PlannedExpenses.Count();
            //Console.WriteLine($"PlannedExpenses: {plannedExpenseCount}");

            //var account1 = DbHelper.db.Accounts.FirstOrDefault(a => a.Id == 1);
            //if (account1 != null)
            //{
            //    account1.Name = "карта моно";
            //}

            //// Отримуємо рахунок з Id = 2
            //var account2 = DbHelper.db.Accounts.FirstOrDefault(a => a.Id == 2);
            //if (account2 != null)
            //{
            //    account2.Name = "готівка";
            //}

            //// Зберігаємо зміни у базі даних
            //DbHelper.db.SaveChanges();

            //Console.WriteLine("Назви рахунків успішно змінено.");

            // Отримуємо перші два об'єкти Saving
            //var firstTwoSavings = DbHelper.db.Savings
            //    .OrderBy(s => s.Id)
            //    .Take(2)
            //    .ToList();

            //// Видаляємо ці об'єкти
            //DbHelper.db.Savings.RemoveRange(firstTwoSavings);

            //// Зберігаємо зміни у базі даних
            //DbHelper.db.SaveChanges();

            //Console.WriteLine("Перші два об'єкти Saving успішно видалено.");

            // Отримуємо всі Id з таблиці Savings
            //List<int> savingIds = DbHelper.db.Savings
            //    .Select(s => s.Id)
            //    .ToList();

            //// Виводимо кожен Id
            //Console.WriteLine("Існуючі Id заощаджень:");
            //foreach (int id in savingIds)
            //{
            //    Console.WriteLine(id);
            //}

            var standardCategories = new List<string> { "Їжа",  "Одяг", "Розваги", "Транспорт", "Здоров'я" };
            var users = DbHelper.db.Users.ToList();

            foreach (var user in users)
            {
                foreach (var categoryName in standardCategories)
                {
                    bool categoryExists = DbHelper.db.ExpensesCategories
                        .Any(c => c.UserId == user.Id && c.Name == categoryName);

                    if (!categoryExists)
                    {
                        var newCategory = new ExpenseCategory
                        {
                            Name = categoryName,
                            UserId = user.Id
                        };
                        DbHelper.db.ExpensesCategories.Add(newCategory);
                    }
                }
            }

            DbHelper.db.SaveChanges();
            Console.WriteLine("Стандартні категорії додані для всіх існуючих користувачів.");

        }
    }
}
