using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renameTableEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_eduaction_tb_m_university_UniversityId",
                table: "tb_t_eduaction");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_t_profiling_tb_t_eduaction_EducationId",
                table: "tb_t_profiling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_t_eduaction",
                table: "tb_t_eduaction");

            migrationBuilder.RenameTable(
                name: "tb_t_eduaction",
                newName: "tb_m_eduaction");

            migrationBuilder.RenameIndex(
                name: "IX_tb_t_eduaction_UniversityId",
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profiling_tb_m_eduaction_EducationId",
                table: "tb_t_profiling",
                column: "EducationId",
                principalTable: "tb_m_eduaction",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "tb_t_eduaction");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_eduaction_UniversityId",
                table: "tb_t_eduaction",
                newName: "IX_tb_t_eduaction_UniversityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_t_eduaction",
                table: "tb_t_eduaction",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_eduaction_tb_m_university_UniversityId",
                table: "tb_t_eduaction",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_t_profiling_tb_t_eduaction_EducationId",
                table: "tb_t_profiling",
                column: "EducationId",
                principalTable: "tb_t_eduaction",
                principalColumn: "EducationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
