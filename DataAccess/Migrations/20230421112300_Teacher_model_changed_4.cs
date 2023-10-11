using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Teacher_model_changed_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeachings_Teachers_TeacherId",
                table: "SubjectTeachings");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeachings_TeacherId",
                table: "SubjectTeachings");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SubjectTeachings");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeachings_Teachers_SchoolSubjectsId",
                table: "SubjectTeachings",
                column: "SchoolSubjectsId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeachings_Teachers_SchoolSubjectsId",
                table: "SubjectTeachings");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SubjectTeachings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachings_TeacherId",
                table: "SubjectTeachings",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeachings_Teachers_TeacherId",
                table: "SubjectTeachings",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
