using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addTableAccountProfilingEducationUniversity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_university",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_university", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_account_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_eduaction",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_eduaction", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_tb_t_eduaction_tb_m_university_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "tb_m_university",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_profiling",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_profiling", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_t_profiling_tb_t_account_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_t_account",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_profiling_tb_t_eduaction_EducationId",
                        column: x => x.EducationId,
                        principalTable: "tb_t_eduaction",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_eduaction_UniversityId",
                table: "tb_t_eduaction",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_profiling_EducationId",
                table: "tb_t_profiling",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_t_profiling");

            migrationBuilder.DropTable(
                name: "tb_t_account");

            migrationBuilder.DropTable(
                name: "tb_t_eduaction");

            migrationBuilder.DropTable(
                name: "tb_m_university");
        }
    }
}
