using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SSDAssignment.Migrations
{
    public partial class ListingUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Listing");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Listing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Listing");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Listing",
                nullable: false,
                defaultValue: 0);
        }
    }
}
