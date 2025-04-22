using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemQueueDigitalisation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentToprovider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaymentConfirmed",
                table: "Providers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaymentConfirmed",
                table: "Providers");
        }
    }
}
