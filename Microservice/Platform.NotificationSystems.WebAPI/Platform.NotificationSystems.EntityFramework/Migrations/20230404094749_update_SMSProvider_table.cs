using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.NotificationSystems.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class updateSMSProvidertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlTemplate",
                table: "SMSProviderEndPoint",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "SMSProvider",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sender",
                table: "SMSProvider",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppKey",
                table: "SMSProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppSecret",
                table: "SMSProvider",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "SMSProvider",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppKey",
                table: "SMSProvider");

            migrationBuilder.DropColumn(
                name: "AppSecret",
                table: "SMSProvider");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "SMSProvider");

            migrationBuilder.AlterColumn<string>(
                name: "UrlTemplate",
                table: "SMSProviderEndPoint",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "SMSProvider",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Sender",
                table: "SMSProvider",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
