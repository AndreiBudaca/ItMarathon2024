using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItMarathon.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Add_YearOfStudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfStudy",
                table: "StudentOptionalPreferences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyYear",
                table: "StudentGrades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StudentOptional",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    OptionalId = table.Column<int>(type: "int", nullable: false),
                    StudyYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOptional", x => new { x.StudentId, x.OptionalId });
                    table.ForeignKey(
                        name: "FK_StudentOptional_Courses_OptionalId",
                        column: x => x.OptionalId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentOptional_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentOptional_OptionalId",
                table: "StudentOptional",
                column: "OptionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentOptional");

            migrationBuilder.DropColumn(
                name: "YearOfStudy",
                table: "StudentOptionalPreferences");

            migrationBuilder.DropColumn(
                name: "StudyYear",
                table: "StudentGrades");
        }
    }
}
