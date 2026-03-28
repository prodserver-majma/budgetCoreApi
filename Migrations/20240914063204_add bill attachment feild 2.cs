using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mahadalzahrawebapi.Migrations
{
    /// <inheritdoc />
    public partial class addbillattachmentfeild2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "panCardAttachmentFileName",
                table: "mz_expense_bill_master",
                newName: "billAttachment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "billAttachment",
                table: "mz_expense_bill_master",
                newName: "panCardAttachmentFileName");
        }
    }
}
