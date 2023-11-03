using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixTagRegressions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagKitchenType",
                table: "TagKitchenType");

            migrationBuilder.DropIndex(
                name: "IX_TagKitchenType_KitchenTypeId",
                table: "TagKitchenType");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagKitchenType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagKitchenType",
                table: "TagKitchenType",
                column: "KitchenTypeId");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5310d489-98d2-416e-bbf4-7badc5197f73",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "7571c510-af7e-4927-adf6-a60ee3153793", new DateTime(2023, 9, 19, 11, 36, 46, 708, DateTimeKind.Utc).AddTicks(816) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "03541842-dd41-420e-950e-fc8cbe864df9", new DateTime(2023, 9, 19, 11, 36, 46, 708, DateTimeKind.Utc).AddTicks(799) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7551632f-e04b-43e7-a661-18ec63239697", new DateTime(2023, 9, 19, 11, 36, 46, 708, DateTimeKind.Utc).AddTicks(2753), "AQAAAAIAAYagAAAAEMqi7K76r6+ARiGJF3/OT/1oXHvGlWf8DTcRZX+V3XaadXj/oTli18QLMgDXY0rTtw==", "a4bed26e-b50a-403c-a29d-45fffa5e1c4f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagKitchenType",
                table: "TagKitchenType");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagKitchenType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagKitchenType",
                table: "TagKitchenType",
                columns: new[] { "TagId", "KitchenTypeId" });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5310d489-98d2-416e-bbf4-7badc5197f73",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "f6b86777-3478-45c4-b3fe-aaa38c0d7f45", new DateTime(2023, 9, 19, 11, 21, 21, 107, DateTimeKind.Utc).AddTicks(6346) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "89ffacbc-7718-438c-9fc8-79153dc335ff", new DateTime(2023, 9, 19, 11, 21, 21, 107, DateTimeKind.Utc).AddTicks(6333) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2cd229e2-1f66-4746-95c3-54f142333e88", new DateTime(2023, 9, 19, 11, 21, 21, 107, DateTimeKind.Utc).AddTicks(7183), "AQAAAAIAAYagAAAAEOuUYri28N3jmp1AWzUPa53Hgx7pQZf87teHKEp8Bc632Fn9h52jxNTwAFZgyCZZaA==", "9b59e207-4bce-44c8-8093-5283c14cb73a" });

            migrationBuilder.CreateIndex(
                name: "IX_TagKitchenType_KitchenTypeId",
                table: "TagKitchenType",
                column: "KitchenTypeId");
        }
    }
}
