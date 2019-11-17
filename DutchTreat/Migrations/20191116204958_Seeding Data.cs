using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DutchTreat.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2019, 11, 16, 20, 49, 57, 319, DateTimeKind.Utc).AddTicks(5712));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2019, 11, 16, 17, 15, 28, 977, DateTimeKind.Utc).AddTicks(7477));
        }
    }
}
