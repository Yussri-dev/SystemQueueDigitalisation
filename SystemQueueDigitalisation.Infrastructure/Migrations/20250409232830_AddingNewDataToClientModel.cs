using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemQueueDigitalisation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewDataToClientModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Clients",
                newName: "LastName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Clients",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Clients",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
