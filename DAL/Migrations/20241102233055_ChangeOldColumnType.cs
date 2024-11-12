#nullable disable

namespace DAL.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class ChangeOldColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "end_date",
            //    table: "savings");

            //migrationBuilder.AddColumn<int>(
            //    name: "months_number",
            //    table: "savings",
            //    type: "INTEGER",
            //    nullable: false,
            //    defaultValue: 0);
            migrationBuilder.AlterColumn<int>(
                name: "months_number",
                table: "savings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "months_number",
            //    table: "savings");

            //migrationBuilder.AddColumn<string>(
            //    name: "end_date",
            //    table: "savings",
            //    type: "TEXT",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "months_number",
                table: "savings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
