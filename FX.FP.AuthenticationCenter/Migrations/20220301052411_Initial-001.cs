using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX.FP.AuthenticationCenter.Migrations
{
    public partial class Initial001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_APIKeyInfo",
                columns: table => new
                {
                    GUID = table.Column<string>(type: "varchar(36)", nullable: false),
                    AppSecret = table.Column<string>(type: "varchar(36)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    PlatformName = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    LinkMan = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LinkPhone = table.Column<string>(type: "varchar(50)", nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    BackReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    AllowIps = table.Column<string>(type: "varchar(100)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_APIKeyInfo", x => x.GUID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_APIKeyInfo");
        }
    }
}
