using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SSDAssignment.Migrations
{
    public partial class UpdateAppUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Listing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Listing");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
