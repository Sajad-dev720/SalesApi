using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "SalesPeople");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "SalesPeople");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "SaleLinesSalesPeople");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "SaleLinesSalesPeople");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "SaleLines");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "SaleLines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "FactorHeaders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "FactorHeaders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "FactorDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "FactorDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "sales",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "sales",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "SalesPeople",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "SalesPeople",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "SaleLinesSalesPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "SaleLinesSalesPeople",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "SaleLinesProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "SaleLinesProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "SaleLines",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "SaleLines",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "Products",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "FactorHeaders",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "FactorHeaders",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "FactorDetails",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "FactorDetails",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "Discounts",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "Discounts",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "sales",
                table: "Customers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "sales",
                table: "Customers",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }
    }
}
