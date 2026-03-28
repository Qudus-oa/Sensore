using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sensore.Migrations
{
    /// <inheritdoc />
    public partial class InitComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicianReply",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReplyAt",
                table: "Comments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicianReply",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ReplyAt",
                table: "Comments");
        }
    }
}
