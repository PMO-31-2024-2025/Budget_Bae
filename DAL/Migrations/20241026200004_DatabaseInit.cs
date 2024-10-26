using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    type = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    card_number = table.Column<string>(type: "TEXT", nullable: true),
                    balance = table.Column<double>(type: "REAL", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                    table.UniqueConstraint("UC_card_number", x => x.card_number);
                });

            migrationBuilder.CreateTable(
                name: "expenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    category_id = table.Column<int>(type: "INTEGER", nullable: false),
                    expense_date = table.Column<string>(type: "TEXT", nullable: false),
                    expense_sum = table.Column<double>(type: "REAL", nullable: false),
                    account_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expenses_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenses_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "incomes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    category = table.Column<string>(type: "TEXT", nullable: false),
                    income_date = table.Column<string>(type: "TEXT", nullable: false),
                    income_sum = table.Column<double>(type: "REAL", nullable: false),
                    account_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_incomes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planned_expenses",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    planned_sum = table.Column<double>(type: "REAL", nullable: false),
                    notification_date = table.Column<string>(type: "TEXT", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planned_expenses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "savings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    target_name = table.Column<string>(type: "TEXT", nullable: false),
                    target_sum = table.Column<double>(type: "REAL", nullable: false),
                    current_sum = table.Column<double>(type: "REAL", nullable: false),
                    end_date = table.Column<string>(type: "TEXT", nullable: false),
                    user_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_savings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.UniqueConstraint("UC_email", x => x.email);
                    table.ForeignKey(
                        name: "FK_users_accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "accounts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_expenses_categories_UserId",
                        column: x => x.UserId,
                        principalTable: "expenses_categories",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_UserId",
                table: "users",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expenses");

            migrationBuilder.DropTable(
                name: "incomes");

            migrationBuilder.DropTable(
                name: "planned_expenses");

            migrationBuilder.DropTable(
                name: "savings");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "expenses_categories");
        }
    }
}
