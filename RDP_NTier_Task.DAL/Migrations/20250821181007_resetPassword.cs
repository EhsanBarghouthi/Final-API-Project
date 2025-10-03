using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDP_NTier_Task.DAL.Migrations
{
    /// <inheritdoc />
    public partial class resetPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeExpiredTime",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordCode",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExpiredTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PasswordCode",
                table: "User");
        }
    }
}
