using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixTagRegressions1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5310d489-98d2-416e-bbf4-7badc5197f73",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "5ab70136-8160-4611-afab-a47845f2cf7d", new DateTime(2023, 9, 19, 10, 44, 52, 182, DateTimeKind.Utc).AddTicks(7659) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "5ab7155b-c3a4-44a1-a692-f9f79882d729", new DateTime(2023, 9, 19, 10, 44, 52, 182, DateTimeKind.Utc).AddTicks(7639) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c92f94f-3347-477f-9255-66486a50faa1", new DateTime(2023, 9, 19, 10, 44, 52, 182, DateTimeKind.Utc).AddTicks(8874), "AQAAAAIAAYagAAAAENUQI0+8hklrZphQ23S9UIoLW4u0Vl2p8KmHUEEPlcETjw2nBxKwoEu8tj2L7bSOOg==", "45858135-923d-4d12-8f55-2f6155dc0102" });
        }
    }
}
