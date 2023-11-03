using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddQueryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Query",
                columns: table => new
                {
                    IdQuery = table.Column<Guid>(type: "uuid", nullable: false),
                    IdAggregator = table.Column<Guid>(type: "uuid", nullable: false),
                    IdDynamicIntegration = table.Column<Guid>(type: "uuid", nullable: false),
                    Method = table.Column<int>(type: "integer", nullable: false),
                    Api = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Query", x => x.IdQuery);
                });

            migrationBuilder.UpdateData(
                table: "IntegrationParameter",
                keyColumn: "IntegrationParameterId",
                keyValue: new Guid("0127afbd-ade2-49ee-ae67-f038866c100d"),
                column: "MatchWithKey",
                value: 20);

            migrationBuilder.UpdateData(
                table: "IntegrationParameter",
                keyColumn: "IntegrationParameterId",
                keyValue: new Guid("0127afbd-ade2-49ee-ae67-f038866c200d"),
                column: "MatchWithKey",
                value: 20);

            migrationBuilder.UpdateData(
                table: "IntegrationParameter",
                keyColumn: "IntegrationParameterId",
                keyValue: new Guid("0127afbd-ade2-54ee-ae67-f034566c692d"),
                column: "MatchWithKey",
                value: 20);

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "5310d489-98d2-416e-bbf4-7badc5197f73",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "30bbc472-0af1-4582-8284-5b5b996481b5", new DateTime(2023, 11, 1, 11, 59, 0, 41, DateTimeKind.Utc).AddTicks(4635) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "Role",
                keyColumn: "Id",
                keyValue: "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684",
                columns: new[] { "ConcurrencyStamp", "CreationTime" },
                values: new object[] { "289b920b-9fe2-496e-b29f-16d1251d1687", new DateTime(2023, 11, 1, 11, 59, 0, 41, DateTimeKind.Utc).AddTicks(4609) });

            migrationBuilder.UpdateData(
                schema: "Security",
                table: "User",
                keyColumn: "Id",
                keyValue: "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f",
                columns: new[] { "ConcurrencyStamp", "CreationTime", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d6a136c-22e8-417a-a5ce-f15e8182a9dd", new DateTime(2023, 11, 1, 11, 59, 0, 41, DateTimeKind.Utc).AddTicks(5477), "AQAAAAIAAYagAAAAED2p2xeBkcaG821ZdXObAmP3AdfyCuM3yrZnb6oIUz95c2V4MAEovOefdp3F4RhFHw==", "3806744b-187a-457e-9846-c4b06511e735" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Query");

            migrationBuilder.UpdateData(
                table: "IntegrationParameter",
                keyColumn: "IntegrationParameterId",
                keyValue: new Guid("0127afbd-ade2-49ee-ae67-f038866c100d"),
                column: "MatchWithKey",
                value: 11);

            migrationBuilder.UpdateData(
                table: "IntegrationParameter",
                keyColumn: "IntegrationParameterId",
                keyValue: new Guid("0127afbd-ade2-49ee-ae67-f038866c200d"),
                column: "MatchWithKey",
                value: 11);

            migrationBuilder.UpdateData(
                table: "IntegrationParameter",
                keyColumn: "IntegrationParameterId",
                keyValue: new Guid("0127afbd-ade2-54ee-ae67-f034566c692d"),
                column: "MatchWithKey",
                value: 11);

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
    }
}
