using System;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public partial class BudgetBaeContext : DbContext
{
    public BudgetBaeContext()
    {
    }

    public BudgetBaeContext(DbContextOptions<BudgetBaeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<ExpensesCategory> ExpensesCategories { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<PlannedExpense> PlannedExpenses { get; set; }

    public virtual DbSet<Saving> Savings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\BudgetBae\\DAL\\DataBase\\BudgetBaeDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0.0)
                .HasColumnName("balance");
            entity.Property(e => e.CardNumber).HasColumnName("card_number");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Accounts).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("expenses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ExpenseDate).HasColumnName("expense_date");
            entity.Property(e => e.ExpenseSum).HasColumnName("expense_sum");

            entity.HasOne(d => d.Account).WithMany(p => p.Expenses).HasForeignKey(d => d.AccountId);

            entity.HasOne(d => d.Category).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ExpensesCategory>(entity =>
        {
            entity.ToTable("expenses_categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ExpensesCategories).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.ToTable("incomes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.IncomeDate).HasColumnName("income_date");
            entity.Property(e => e.IncomeSum).HasColumnName("income_sum");

            entity.HasOne(d => d.Account).WithMany(p => p.Incomes).HasForeignKey(d => d.AccountId);
        });

        modelBuilder.Entity<PlannedExpense>(entity =>
        {
            entity.ToTable("planned_expenses");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NotificationDate).HasColumnName("notification_date");
            entity.Property(e => e.PlannedSum).HasColumnName("planned_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PlannedExpenses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Saving>(entity =>
        {
            entity.ToTable("savings");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CurrentSum)
                .HasDefaultValue(0.0)
                .HasColumnName("current_sum");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.TargetName).HasColumnName("target_name");
            entity.Property(e => e.TargetSum).HasColumnName("target_sum");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Savings).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
