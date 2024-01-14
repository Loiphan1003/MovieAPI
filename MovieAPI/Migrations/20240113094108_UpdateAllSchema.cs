using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharacterName",
                table: "Casts",
                newName: "Character");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Born",
                table: "Persons",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Character",
                table: "Casts",
                newName: "CharacterName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Born",
                table: "Persons",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
