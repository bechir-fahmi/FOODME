using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixTagRegressions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagCity_Tag_TagId",
                table: "TagCity");

            migrationBuilder.DropForeignKey(
                name: "FK_TagFoodType_Tag_TagId",
                table: "TagFoodType");

            migrationBuilder.DropForeignKey(
                name: "FK_TagKitchenType_Tag_TagId",
                table: "TagKitchenType");

            migrationBuilder.DropForeignKey(
                name: "FK_TagLanguage_Tag_TagId",
                table: "TagLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMealTiming_Tag_TagId",
                table: "TagMealTiming");

            migrationBuilder.DropForeignKey(
                name: "FK_TagMealType_Tag_TagId",
                table: "TagMealType");

            migrationBuilder.DropForeignKey(
                name: "FK_TagOffer_Tag_TagId",
                table: "TagOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_TagRegion_Tag_TagId",
                table: "TagRegion");

            migrationBuilder.DropForeignKey(
                name: "FK_TagVendor_Tag_TagId",
                table: "TagVendor");

            migrationBuilder.DropForeignKey(
                name: "FK_TagZone_Tag_TagId",
                table: "TagZone");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagZone",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagVendor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagRegion",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagOffer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagMealType",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagMealTiming",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagLanguage",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagKitchenType",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagFoodType",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "TagCity",
                type: "text",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "TagZone");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagVendor");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagRegion");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagOffer");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagMealType");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagMealTiming");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagLanguage");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagKitchenType");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagFoodType");

            migrationBuilder.DropColumn(
                name: "value",
                table: "TagCity");

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5310d489-98d2-416e-bbf4-7badc5197f73",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "6759e4da-5107-4242-8866-302946db944c", new DateTime(2023, 9, 18, 22, 23, 19, 936, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "5362f477-c37d-4092-a1fc-fe754e3df3b1", new DateTime(2023, 9, 18, 22, 23, 19, 936, DateTimeKind.Utc).AddTicks(3408) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba7e50ad-3144-4630-89c6-18becc86da37", new DateTime(2023, 9, 18, 22, 23, 19, 936, DateTimeKind.Utc).AddTicks(4388), "AQAAAAIAAYagAAAAENLdM/EkqB39cBIAo3ES1ETwBwrWbW2+xTeq99kzyq9I6Q3DRSWkGxWD0DZpnXxxEQ==", "1e9cdb01-7614-4ea4-9979-c4cdc0495955" });

            migrationBuilder.AddForeignKey(
                name: "FK_TagCity_Tag_TagId",
                table: "TagCity",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagFoodType_Tag_TagId",
                table: "TagFoodType",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagKitchenType_Tag_TagId",
                table: "TagKitchenType",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagLanguage_Tag_TagId",
                table: "TagLanguage",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMealTiming_Tag_TagId",
                table: "TagMealTiming",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagMealType_Tag_TagId",
                table: "TagMealType",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagOffer_Tag_TagId",
                table: "TagOffer",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagRegion_Tag_TagId",
                table: "TagRegion",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagVendor_Tag_TagId",
                table: "TagVendor",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagZone_Tag_TagId",
                table: "TagZone",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
