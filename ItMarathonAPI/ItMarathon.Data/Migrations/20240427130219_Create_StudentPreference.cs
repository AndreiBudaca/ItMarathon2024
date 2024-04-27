using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItMarathon.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_StudentPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGrades_Courses_StudentId",
                table: "StudentGrades");

            migrationBuilder.CreateTable(
                name: "StudentOptionalPreferences",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    OptionalId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentOptionalPreferences", x => new { x.StudentId, x.OptionalId });
                    table.ForeignKey(
                        name: "FK_StudentOptionalPreferences_Courses_OptionalId",
                        column: x => x.OptionalId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentOptionalPreferences_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGrades_CourseId",
                table: "StudentGrades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentOptionalPreferences_OptionalId",
                table: "StudentOptionalPreferences",
                column: "OptionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGrades_Courses_CourseId",
                table: "StudentGrades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGrades_Courses_CourseId",
                table: "StudentGrades");

            migrationBuilder.DropTable(
                name: "StudentOptionalPreferences");

            migrationBuilder.DropIndex(
                name: "IX_StudentGrades_CourseId",
                table: "StudentGrades");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGrades_Courses_StudentId",
                table: "StudentGrades",
                column: "StudentId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
