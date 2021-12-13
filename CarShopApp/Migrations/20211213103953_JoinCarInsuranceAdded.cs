using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShopApp.Migrations
{
    public partial class JoinCarInsuranceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarInsurances",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false),
                    InsuranceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInsurances", x => new { x.CarId, x.InsuranceId });
                    table.ForeignKey(
                        name: "FK_CarInsurances_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarInsurances_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarInsurances_InsuranceId",
                table: "CarInsurances",
                column: "InsuranceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarInsurances");
        }
    }
}
