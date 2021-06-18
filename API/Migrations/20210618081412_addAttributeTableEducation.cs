using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addAttributeTableEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_eduaction_tb_m_university_UniversityId",
                table: "tb_m_eduaction");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "tb_m_eduaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_eduaction_tb_m_university_UniversityId",
                table: "tb_m_eduaction",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_eduaction_tb_m_university_UniversityId",
                table: "tb_m_eduaction");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                table: "tb_m_eduaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_eduaction_tb_m_university_UniversityId",
                table: "tb_m_eduaction",
                column: "UniversityId",
                principalTable: "tb_m_university",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
