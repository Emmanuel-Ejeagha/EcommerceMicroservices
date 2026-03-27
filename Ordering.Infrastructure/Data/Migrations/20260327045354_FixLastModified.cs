using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixLastModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LasteModified",
                table: "Products",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "LasteModified",
                table: "Orders",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "LasteModified",
                table: "OrderItems",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "LasteModified",
                table: "Customers",
                newName: "LastModified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Products",
                newName: "LasteModified");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Orders",
                newName: "LasteModified");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "OrderItems",
                newName: "LasteModified");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Customers",
                newName: "LasteModified");
        }
    }
}
