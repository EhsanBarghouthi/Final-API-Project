using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDP_NTier_Task.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Products");
        }
    }
}
