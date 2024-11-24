using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab6.Migrations
{
    /// <inheritdoc />
    public partial class RefProductTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_RefProductType_ProductTypeCode",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_RefProductType_RefProductTypeProductTypeCode",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefProductType",
                table: "RefProductType");

            migrationBuilder.RenameTable(
                name: "RefProductType",
                newName: "RefProductTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefProductTypes",
                table: "RefProductTypes",
                column: "ProductTypeCode");

            migrationBuilder.UpdateData(
                table: "CustomerOrders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 14, 13, 8, 4, 734, DateTimeKind.Utc).AddTicks(3500));

            migrationBuilder.UpdateData(
                table: "CustomerOrders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 11, 19, 13, 8, 4, 734, DateTimeKind.Utc).AddTicks(3510));

            migrationBuilder.UpdateData(
                table: "CustomerOrders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2024, 11, 22, 13, 8, 4, 734, DateTimeKind.Utc).AddTicks(3510));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RefProductTypes_ProductTypeCode",
                table: "Products",
                column: "ProductTypeCode",
                principalTable: "RefProductTypes",
                principalColumn: "ProductTypeCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RefProductTypes_RefProductTypeProductTypeCode",
                table: "Products",
                column: "RefProductTypeProductTypeCode",
                principalTable: "RefProductTypes",
                principalColumn: "ProductTypeCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_RefProductTypes_ProductTypeCode",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_RefProductTypes_RefProductTypeProductTypeCode",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefProductTypes",
                table: "RefProductTypes");

            migrationBuilder.RenameTable(
                name: "RefProductTypes",
                newName: "RefProductType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefProductType",
                table: "RefProductType",
                column: "ProductTypeCode");

            migrationBuilder.UpdateData(
                table: "CustomerOrders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 11, 6, 41, 46, 40, DateTimeKind.Utc).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "CustomerOrders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 11, 16, 6, 41, 46, 40, DateTimeKind.Utc).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "CustomerOrders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "OrderDate",
                value: new DateTime(2024, 11, 19, 6, 41, 46, 40, DateTimeKind.Utc).AddTicks(6270));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RefProductType_ProductTypeCode",
                table: "Products",
                column: "ProductTypeCode",
                principalTable: "RefProductType",
                principalColumn: "ProductTypeCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_RefProductType_RefProductTypeProductTypeCode",
                table: "Products",
                column: "RefProductTypeProductTypeCode",
                principalTable: "RefProductType",
                principalColumn: "ProductTypeCode");
        }
    }
}
