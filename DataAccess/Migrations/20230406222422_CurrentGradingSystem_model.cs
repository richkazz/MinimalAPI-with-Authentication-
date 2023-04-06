using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CurrentGradingSystem_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JuniorSchoolSubject_SchoolSubjects_SubjectId",
                table: "JuniorSchoolSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_SeniorSchoolSubject_SchoolSubjects_SubjectId",
                table: "SeniorSchoolSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeniorSchoolSubject",
                table: "SeniorSchoolSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JuniorSchoolSubject",
                table: "JuniorSchoolSubject");

            migrationBuilder.RenameTable(
                name: "SeniorSchoolSubject",
                newName: "SeniorSchoolSubjects");

            migrationBuilder.RenameTable(
                name: "JuniorSchoolSubject",
                newName: "JuniorSchoolSubjects");

            migrationBuilder.RenameIndex(
                name: "IX_SeniorSchoolSubject_SubjectId",
                table: "SeniorSchoolSubjects",
                newName: "IX_SeniorSchoolSubjects_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_JuniorSchoolSubject_SubjectId",
                table: "JuniorSchoolSubjects",
                newName: "IX_JuniorSchoolSubjects_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeniorSchoolSubjects",
                table: "SeniorSchoolSubjects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JuniorSchoolSubjects",
                table: "JuniorSchoolSubjects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CurrentGradingSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradingSystem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentGradingSystems", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_JuniorSchoolSubjects_SchoolSubjects_SubjectId",
                table: "JuniorSchoolSubjects",
                column: "SubjectId",
                principalTable: "SchoolSubjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeniorSchoolSubjects_SchoolSubjects_SubjectId",
                table: "SeniorSchoolSubjects",
                column: "SubjectId",
                principalTable: "SchoolSubjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JuniorSchoolSubjects_SchoolSubjects_SubjectId",
                table: "JuniorSchoolSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_SeniorSchoolSubjects_SchoolSubjects_SubjectId",
                table: "SeniorSchoolSubjects");

            migrationBuilder.DropTable(
                name: "CurrentGradingSystems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeniorSchoolSubjects",
                table: "SeniorSchoolSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JuniorSchoolSubjects",
                table: "JuniorSchoolSubjects");

            migrationBuilder.RenameTable(
                name: "SeniorSchoolSubjects",
                newName: "SeniorSchoolSubject");

            migrationBuilder.RenameTable(
                name: "JuniorSchoolSubjects",
                newName: "JuniorSchoolSubject");

            migrationBuilder.RenameIndex(
                name: "IX_SeniorSchoolSubjects_SubjectId",
                table: "SeniorSchoolSubject",
                newName: "IX_SeniorSchoolSubject_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_JuniorSchoolSubjects_SubjectId",
                table: "JuniorSchoolSubject",
                newName: "IX_JuniorSchoolSubject_SubjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeniorSchoolSubject",
                table: "SeniorSchoolSubject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JuniorSchoolSubject",
                table: "JuniorSchoolSubject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JuniorSchoolSubject_SchoolSubjects_SubjectId",
                table: "JuniorSchoolSubject",
                column: "SubjectId",
                principalTable: "SchoolSubjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeniorSchoolSubject_SchoolSubjects_SubjectId",
                table: "SeniorSchoolSubject",
                column: "SubjectId",
                principalTable: "SchoolSubjects",
                principalColumn: "Id");
        }
    }
}
