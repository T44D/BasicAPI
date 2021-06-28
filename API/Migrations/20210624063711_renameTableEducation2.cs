using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renameTableEducation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_eduaction_tb_m_university_UniversityId",
                table: "tb_m_eduaction");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profiling_tb_m_eduaction_EducationId",
                table: "tb_t_profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_eduaction",
                table: "tb_m_eduaction");

            migrationBuilder.RenameTable(
                name: "tb_m_eduaction",
                newName: "tb_t_education");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_eduaction_UniversityId",
                table: "tb_t_education",
                newName: "IX_tb_t_education_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_education",
                table: "tb_t_education",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_education_tb_m_university_UniversityId",
                table: "tb_t_education",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profiling_tb_t_education_EducationId",
                table: "tb_t_profiling",
                column: "EducationId",
                principalTable: "tb_t_education",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_education_tb_m_university_UniversityId",
                table: "tb_t_education");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profiling_tb_t_education_EducationId",
                table: "tb_t_profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_education",
                table: "tb_t_education");

            migrationBuilder.RenameTable(
                name: "tb_t_education",
                newName: "tb_m_eduaction");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_education_UniversityId",
                table: "tb_m_eduaction",
                newName: "IX_tb_m_eduaction_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_eduaction",
                table: "tb_m_eduaction",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_eduaction_tb_m_university_UniversityId",
                table: "tb_m_eduaction",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profiling_tb_m_eduaction_EducationId",
                table: "tb_t_profiling",
                column: "EducationId",
                principalTable: "tb_m_eduaction",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
