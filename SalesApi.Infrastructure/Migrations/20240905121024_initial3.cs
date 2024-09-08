using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleLinesProducts_Products_ProductsId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleLinesProducts_SaleLines_SaleLinesId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleLinesProducts_ProductsId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleLinesProducts_SaleLinesId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropColumn(
                name: "SaleLinesId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.CreateIndex(
                name: "IX_SaleLinesProducts_ProductId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleLinesProducts_SaleLineId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "SaleLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLinesProducts_Products_ProductId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "ProductId",
                principalSchema: "sales",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLinesProducts_SaleLines_SaleLineId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "SaleLineId",
                principalSchema: "sales",
                principalTable: "SaleLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleLinesProducts_Products_ProductId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleLinesProducts_SaleLines_SaleLineId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleLinesProducts_ProductId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.DropIndex(
                name: "IX_SaleLinesProducts_SaleLineId",
                schema: "sales",
                table: "SaleLinesProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductsId",
                schema: "sales",
                table: "SaleLinesProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SaleLinesId",
                schema: "sales",
                table: "SaleLinesProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SaleLinesProducts_ProductsId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleLinesProducts_SaleLinesId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "SaleLinesId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLinesProducts_Products_ProductsId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "ProductsId",
                principalSchema: "sales",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleLinesProducts_SaleLines_SaleLinesId",
                schema: "sales",
                table: "SaleLinesProducts",
                column: "SaleLinesId",
                principalSchema: "sales",
                principalTable: "SaleLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
