using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradebookApi.Migrations
{
    /// <inheritdoc />
    public partial class EmailVerificationAttemptsCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "EmailVerificationCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "EmailVerificationCodes");
        }
    }
}
