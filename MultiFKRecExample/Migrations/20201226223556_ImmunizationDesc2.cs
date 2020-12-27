using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiFKRecExample.Migrations
{
    public partial class ImmunizationDesc2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords",
                column: "ImmunizationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords");
        }
    }
}
