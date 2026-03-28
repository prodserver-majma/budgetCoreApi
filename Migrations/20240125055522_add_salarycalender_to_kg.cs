using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_salarycalender_to_kg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.AddColumn<string>(
            //    name: "salaryCalender",
            //    table: "khidmat_guzaar",
            //    type: "varchar(200)",
            //    maxLength: 200,
            //    nullable: true,
            //    collation: "utf8_general_ci")
            //    .Annotation("MySql:CharSet", "utf8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "salaryCalender",
                table: "khidmat_guzaar");

        }
    }
}
