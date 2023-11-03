using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixTagRegressions5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagZone",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagVendor",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagRegion",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagOffer",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagMealType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagMealTiming",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagLanguage",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagFoodType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagCity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5310d489-98d2-416e-bbf4-7badc5197f73",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "98882f28-8948-426d-aa53-c1f438a2d046", new DateTime(2023, 9, 19, 11, 56, 21, 146, DateTimeKind.Utc).AddTicks(1575) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "115a5099-961a-4dfe-8cf6-8ff22c18110e", new DateTime(2023, 9, 19, 11, 56, 21, 146, DateTimeKind.Utc).AddTicks(1551) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dfacd30-c238-49a3-88a3-d0f6037dea21", new DateTime(2023, 9, 19, 11, 56, 21, 146, DateTimeKind.Utc).AddTicks(2753), "AQAAAAIAAYagAAAAEGDtc5fTFoZL9Voq3NM/26hb4tSwvGU5FT+gApfK1PQvAblIlwjViWUyPFe2Anbdgw==", "e4e03570-736c-474d-9efb-33d0a860a237" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagZone",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagVendor",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagRegion",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagOffer",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagMealType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagMealTiming",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagLanguage",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagFoodType",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "TagCity",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

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
        }
    }
}
