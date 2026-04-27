using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Members",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "(SYSUTCDATETIME())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF_Members_ModifiedAt");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Members",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Members",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(0)",
                oldDefaultValueSql: "(SYSUTCDATETIME())")
                .OldAnnotation("Relational:DefaultConstraintName", "DF_Members_CreatedAt");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AspNetUsers_Id",
                table: "Members",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AspNetUsers_Id",
                table: "Members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                table: "Members",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "(SYSUTCDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:DefaultConstraintName", "DF_Members_ModifiedAt");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Members",
                type: "datetime2(0)",
                nullable: false,
                defaultValueSql: "(SYSUTCDATETIME())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:DefaultConstraintName", "DF_Members_CreatedAt");
        }
    }
}
