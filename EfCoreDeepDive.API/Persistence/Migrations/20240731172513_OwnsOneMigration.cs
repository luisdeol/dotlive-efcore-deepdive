using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreDeepDive.API.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OwnsOneMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manufacturer_Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer_ProductFullAddress",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Manufacturer_ProductionDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manufacturer_Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Manufacturer_ProductFullAddress",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Manufacturer_ProductionDate",
                table: "Products");
        }
    }
}
