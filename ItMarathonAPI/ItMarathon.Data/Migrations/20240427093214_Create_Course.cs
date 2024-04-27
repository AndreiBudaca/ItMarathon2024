using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItMarathon.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfStudy",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", nullable: false),
                    YearOfStudy = table.Column<int>(type: "int", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    OptionalPackage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropColumn(
                name: "YearOfStudy",
                table: "Users");
        }
    }
}
