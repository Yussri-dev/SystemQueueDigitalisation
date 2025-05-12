using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemQueueDigitalisation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingRequiredToQueueNumberAndAppointmentDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Clients");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Queues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentTime",
                table: "Queues",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Queues");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
