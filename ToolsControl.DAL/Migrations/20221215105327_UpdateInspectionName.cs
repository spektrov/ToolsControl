using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolsControl.DAL.Migrations
{
    public partial class UpdateInspectionName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InspectionPeriodInDays",
                table: "EquipmentTypes",
                newName: "InspectionPeriod");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7510e33a-099f-48d9-8d01-f229db155641"),
                column: "ConcurrencyStamp",
                value: "83bed49d-8550-47f5-927b-0c12f2d49e84");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d7d70154-a342-4ba3-95ec-d20079240b72"),
                column: "ConcurrencyStamp",
                value: "f9f236cc-8c87-4ef0-a48d-2a59d251bf46");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InspectionPeriod",
                table: "EquipmentTypes",
                newName: "InspectionPeriodInDays");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7510e33a-099f-48d9-8d01-f229db155641"),
                column: "ConcurrencyStamp",
                value: "10b89ff9-84e6-41ad-a7c9-ea47e0e2c83d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d7d70154-a342-4ba3-95ec-d20079240b72"),
                column: "ConcurrencyStamp",
                value: "ef0e7a86-5dc7-4d7a-9008-841a2a632e5c");
        }
    }
}
