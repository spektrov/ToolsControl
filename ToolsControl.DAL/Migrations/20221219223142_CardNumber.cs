using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolsControl.DAL.Migrations
{
    public partial class CardNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Workers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7510e33a-099f-48d9-8d01-f229db155641"),
                column: "ConcurrencyStamp",
                value: "fbc7873b-69a7-4ddf-b9b6-c517421ae61a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d7d70154-a342-4ba3-95ec-d20079240b72"),
                column: "ConcurrencyStamp",
                value: "ca118346-6ba5-4600-8f27-660ee1c248e3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Workers");

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
    }
}
