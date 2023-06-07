using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanTodo.Infrastructure.Migrations
{
    public partial class ApplyConfigsFromAssembly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TodoTags",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TodoLists",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivationDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6123),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TodoItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TodoTags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6828));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TodoLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivationDate",
                table: "TodoLists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6963));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 6, 20, 0, 21, 273, DateTimeKind.Local).AddTicks(6123));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TodoItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
