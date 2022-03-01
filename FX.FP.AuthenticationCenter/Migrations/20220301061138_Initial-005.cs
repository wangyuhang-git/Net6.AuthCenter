using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FX.FP.AuthenticationCenter.Migrations
{
    public partial class Initial005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BackReason",
                table: "T_APIKeyInfo",
                type: "nvarchar(220)",
                maxLength: 220,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppID",
                table: "T_APIKeyInfo",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppID",
                table: "T_APIKeyInfo");

            migrationBuilder.AlterColumn<string>(
                name: "BackReason",
                table: "T_APIKeyInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(220)",
                oldMaxLength: 220,
                oldNullable: true);
        }
    }
}
