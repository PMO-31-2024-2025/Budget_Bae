using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNotigicationDateType : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "months_number",
            //    table: "savings",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "notification_date",
                table: "planned_expenses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "notification_date",
                table: "planned_expenses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
