using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class addbillattachmentfeild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "panCardAttachmentFileName",
                table: "mz_expense_bill_master",
                type: "text",
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "panCardAttachmentFileName",
                table: "mz_expense_bill_master");
        }
    }
}
