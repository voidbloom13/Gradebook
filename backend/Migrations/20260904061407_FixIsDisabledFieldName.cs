using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradebookApi.Migrations
{
    /// <inheritdoc />
    public partial class FixIsDisabledFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisable",
                table: "Users",
                newName: "IsDisabled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "Users",
                newName: "IsDisable");
        }
    }
}
