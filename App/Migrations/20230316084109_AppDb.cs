using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AppDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USERCD = table.Column<string>(name: "USER_CD", type: "nvarchar(450)", nullable: false),
                    USERNM = table.Column<string>(name: "USER_NM", type: "nvarchar(max)", nullable: false),
                    USERDEPT = table.Column<string>(name: "USER_DEPT", type: "nvarchar(max)", nullable: true),
                    USEREMAIL = table.Column<string>(name: "USER_EMAIL", type: "nvarchar(max)", nullable: false),
                    USERCONTACT = table.Column<string>(name: "USER_CONTACT", type: "nvarchar(max)", nullable: true),
                    REMARK = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USERCD);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
