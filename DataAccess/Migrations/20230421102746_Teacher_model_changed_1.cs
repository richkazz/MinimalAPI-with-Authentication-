using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Teacher_model_changed_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTeachings_Teachers_SchoolTeacherId",
                table: "SubjectTeachings");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTeachings_SchoolTeacherId",
                table: "SubjectTeachings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachings_SchoolTeacherId",
                table: "SubjectTeachings",
                column: "SchoolTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTeachings_Teachers_SchoolTeacherId",
                table: "SubjectTeachings",
                column: "SchoolTeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
