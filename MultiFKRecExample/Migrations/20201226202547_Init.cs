using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiFKRecExample.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Immunizations",
                columns: table => new
                {
                    ImmunizationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImmunizationDesc = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immunizations", x => x.ImmunizationID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "ImmunizationRecords",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ImmunizationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImmunizationRecords", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_ImmunizationRecord_Immunization",
                        column: x => x.ImmunizationID,
                        principalTable: "Immunizations",
                        principalColumn: "ImmunizationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImmunizationRecord_Patient",
                        column: x => x.PatientID,
                        principalTable: "Patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords",
                column: "ImmunizationID");

            migrationBuilder.CreateIndex(
                name: "FK_ImmunizationRecord_Patient",
                table: "ImmunizationRecords",
                column: "PatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImmunizationRecords");

            migrationBuilder.DropTable(
                name: "Immunizations");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
