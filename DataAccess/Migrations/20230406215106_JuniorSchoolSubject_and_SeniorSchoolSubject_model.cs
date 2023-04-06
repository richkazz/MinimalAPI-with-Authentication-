using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class JuniorSchoolSubject_and_SeniorSchoolSubject_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JuniorSchoolSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JuniorSchoolSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JuniorSchoolSubject_SchoolSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SchoolSubjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SeniorSchoolSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeniorSchoolSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeniorSchoolSubject_SchoolSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "SchoolSubjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JuniorSchoolSubject_SubjectId",
                table: "JuniorSchoolSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SeniorSchoolSubject_SubjectId",
                table: "SeniorSchoolSubject",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JuniorSchoolSubject");

            migrationBuilder.DropTable(
                name: "SeniorSchoolSubject");
        }
    }
}
