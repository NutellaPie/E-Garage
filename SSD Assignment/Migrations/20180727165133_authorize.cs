using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SSDAssignment.Migrations
{
    public partial class authorize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Listing");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetRoles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Listing",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Listing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
