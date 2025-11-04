using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do_list.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableEnumsAndConversions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Items",
                newName: "PriorityLevel");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "Items",
                newName: "Kind");

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatus",
                table: "Items",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "PriorityLevel",
                table: "Items",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Kind",
                table: "Items",
                newName: "Priority");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
