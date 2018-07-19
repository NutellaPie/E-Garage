using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SSD_Assignment.Data.Migrations
{
    public partial class inherit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePics",
                table: "ProfilePics");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ProfilePics",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ProfilePics",
                nullable: true,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ProfilePics",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "ProfilePics",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "ProfilePics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "ProfilePics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "ProfilePics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "ProfilePics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProfilePics",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePics",
                table: "ProfilePics",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfilePics",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "ProfilePics");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProfilePics");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProfilePics",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "ProfilePics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfilePics",
                table: "ProfilePics",
                column: "ID");
        }
    }
}
