using BusinessLogic.Services;
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
            //DbHelper.db.SaveChanges();


            //DbHelper.db.Add(new Income() { Category = "Salary", IncomeDate = "2024-09-01", IncomeSum = 3000.00, AccountId = 1 });
            ////DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-09-10", IncomeSum = 12000.50, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-08-10", IncomeSum = 15000.50, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-08-23", IncomeSum = 7000.00, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Freelance Work", IncomeDate = "2024-10-05", IncomeSum = 7000.00, AccountId = 1 });
            //DbHelper.db.Add(new Income() { Category = "Salary", IncomeDate = "2024-10-23", IncomeSum = 20000.00, AccountId = 1 });
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


        }
    }
}
