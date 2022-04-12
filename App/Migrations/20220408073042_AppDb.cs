using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations.AppDb
{
    public partial class AppDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    USER_CD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USER_NM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_DEPT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_CONTACT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    REMARK = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.USER_CD);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
