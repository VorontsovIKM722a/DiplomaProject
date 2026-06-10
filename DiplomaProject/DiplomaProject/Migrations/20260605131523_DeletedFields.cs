using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiplomaProject.Migrations
{
    /// <inheritdoc />
    public partial class DeletedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestStateId",
                table: "TestAttemptEntity");

            migrationBuilder.DropColumn(
                name: "AttemptId",
                table: "TestAnswerEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestStateId",
                table: "TestAttemptEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttemptId",
                table: "TestAnswerEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
