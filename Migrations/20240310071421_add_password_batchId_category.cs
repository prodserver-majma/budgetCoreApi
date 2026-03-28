using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class add_password_batchId_category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "batchId",
                table: "khidmat_guzaar",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "khidmat_guzaar",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "batchId",
                table: "khidmat_guzaar");

            migrationBuilder.DropColumn(
                name: "category",
                table: "khidmat_guzaar");

            migrationBuilder.DropColumn(
                name: "password",
                table: "khidmat_guzaar");
        }
    }
}
