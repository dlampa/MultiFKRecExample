using Microsoft.EntityFrameworkCore.Migrations;

namespace MultiFKRecExample.Migrations
{
    public partial class ImmunizationDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords");

            migrationBuilder.DropIndex(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords");

            migrationBuilder.AddColumn<string>(
                name: "ImmunizationDesc",
                table: "ImmunizationRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImmunizationDesc",
                table: "ImmunizationRecords");

            migrationBuilder.CreateIndex(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords",
                column: "ImmunizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_ImmunizationRecord_Immunization",
                table: "ImmunizationRecords",
                column: "ImmunizationID",
                principalTable: "Immunizations",
                principalColumn: "ImmunizationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
