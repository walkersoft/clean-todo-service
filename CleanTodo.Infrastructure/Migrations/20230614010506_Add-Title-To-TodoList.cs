using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTodo.Infrastructure.Migrations
{
    public partial class AddTitleToTodoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6828));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivationDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6963));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TodoLists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 13, 20, 5, 6, 605, DateTimeKind.Local).AddTicks(8552),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6123));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "TodoLists");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivationDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6123),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 13, 20, 5, 6, 605, DateTimeKind.Local).AddTicks(8552));
        }
    }
}
