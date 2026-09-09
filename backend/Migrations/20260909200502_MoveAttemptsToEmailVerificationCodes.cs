using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradebookApi.Migrations
{
    /// <inheritdoc />
    public partial class MoveAttemptsToEmailVerificationCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Attempts",
                table: "EmailVerificationCodes",
                newName: "FailedAttempts");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedAt",
                table: "EmailVerificationCodes",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifiedAt",
                table: "EmailVerificationCodes");

            migrationBuilder.RenameColumn(
                name: "FailedAttempts",
                table: "EmailVerificationCodes",
                newName: "Attempts");
        }
    }
}
