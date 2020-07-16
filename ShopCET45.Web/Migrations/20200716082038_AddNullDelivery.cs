using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopCET45.Web.Migrations
{
    public partial class AddNullDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Orderdate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "Deliverydate",
                table: "Orders",
                newName: "DeliveryDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "Orderdate");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "Orders",
                newName: "Deliverydate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deliverydate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
