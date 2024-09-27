using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class convertEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VacType",
                table: "Types",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StatusType",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusType",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "StatusType",
                value: "Approved");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "StatusType",
                value: "Rejected");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "StatusType",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1,
                column: "VacType",
                value: "Casual");

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2,
                column: "VacType",
                value: "Annual");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VacType",
                table: "Types",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "StatusType",
                table: "Statuses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "StatusType",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "StatusType",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "StatusType",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 1,
                column: "VacType",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Types",
                keyColumn: "Id",
                keyValue: 2,
                column: "VacType",
                value: 2);
        }
    }
}
