using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Teacher_model_SubjectTeaching_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    teacherId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextOfKinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextOfkinNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEmployed = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.teacherId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTeachings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolSubjectsId = table.Column<int>(type: "int", nullable: false),
                    SchoolTeacherId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeachings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectTeachings_SchoolSubjects_SchoolSubjectsId",
                        column: x => x.SchoolSubjectsId,
                        principalTable: "SchoolSubjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubjectTeachings_Teachers_SchoolTeacherId",
                        column: x => x.SchoolTeacherId,
                        principalTable: "Teachers",
                        principalColumn: "teacherId");
                    table.ForeignKey(
                        name: "FK_SubjectTeachings_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "teacherId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachings_SchoolSubjectsId",
                table: "SubjectTeachings",
                column: "SchoolSubjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachings_SchoolTeacherId",
                table: "SubjectTeachings",
                column: "SchoolTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeachings_TeacherId",
                table: "SubjectTeachings",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Email",
                table: "Teachers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTeachings");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
