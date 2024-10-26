﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(BudgetBaeContext))]
    [Migration("20241026200004_DatabaseInit")]
    partial class DatabaseInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("DAL.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL")
                        .HasColumnName("balance");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("card_number");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("DAL.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("account_id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("category_id");

                    b.Property<string>("ExpenseDate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("expense_date");

                    b.Property<double>("ExpenseSum")
                        .HasColumnType("REAL")
                        .HasColumnName("expense_sum");

                    b.HasKey("Id");

                    b.ToTable("expenses");
                });

            modelBuilder.Entity("DAL.Models.ExpenseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("expenses_categories");
                });

            modelBuilder.Entity("DAL.Models.Income", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("account_id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("category");

                    b.Property<string>("IncomeDate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("income_date");

                    b.Property<double>("IncomeSum")
                        .HasColumnType("REAL")
                        .HasColumnName("income_sum");

                    b.HasKey("Id");

                    b.ToTable("incomes");
                });

            modelBuilder.Entity("DAL.Models.PlannedExpense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("NotigicationDate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("notification_date");

                    b.Property<double>("PlannedSum")
                        .HasColumnType("REAL")
                        .HasColumnName("planned_sum");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("planned_expenses");
                });

            modelBuilder.Entity("DAL.Models.Saving", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<double>("CurrentSum")
                        .HasColumnType("REAL")
                        .HasColumnName("current_sum");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("end_date");

                    b.Property<string>("TargetName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("target_name");

                    b.Property<double>("TargetSum")
                        .HasColumnType("REAL")
                        .HasColumnName("target_sum");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.ToTable("savings");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.HasOne("DAL.Models.Account", null)
                        .WithMany("Users")
                        .HasForeignKey("UserId");

                    b.HasOne("DAL.Models.ExpenseCategory", null)
                        .WithMany("User")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DAL.Models.Account", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DAL.Models.ExpenseCategory", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}