using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixTagRegressions3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagKitchenType",
                table: "TagKitchenType");

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
                values: new object[] { "92c15cab-3fe9-45a9-933f-614ad203906b", new DateTime(2023, 9, 19, 11, 46, 58, 488, DateTimeKind.Utc).AddTicks(1558) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "9adfc448-0c6d-40bd-8123-aab94723808a", new DateTime(2023, 9, 19, 11, 46, 58, 488, DateTimeKind.Utc).AddTicks(1544) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25e8ec2d-14f5-4396-a724-8ae0c5a1dd15", new DateTime(2023, 9, 19, 11, 46, 58, 488, DateTimeKind.Utc).AddTicks(2251), "AQAAAAIAAYagAAAAEP/wGrkyacTkXhiGVwBArri5u8xAxJEDYeigie6RfvsmE7i8NCKwZOKM+a3caKZVSQ==", "3103021d-4979-4c88-8b94-b87914e16852" });

            migrationBuilder.CreateIndex(
                name: "IX_TagKitchenType_KitchenTypeId",
                table: "TagKitchenType",
                column: "KitchenTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TagKitchenType",
                table: "TagKitchenType");

            migrationBuilder.DropIndex(
                name: "IX_TagKitchenType_KitchenTypeId",
                table: "TagKitchenType");

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
    }
}
