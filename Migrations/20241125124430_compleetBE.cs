using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentPortfolioAPI.Migrations
{
    public partial class compleetBE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPrice",
                table: "PortfolioItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "PortfolioItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "PortfolioItems");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "PortfolioItems");
        }
    }
}
