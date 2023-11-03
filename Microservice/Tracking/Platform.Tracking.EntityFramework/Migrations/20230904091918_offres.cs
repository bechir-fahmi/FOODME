using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Platform.Tracking.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class offres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeOfAction = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TypeOfAction = table.Column<int>(type: "integer", nullable: false),
                    BrandName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandActionSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrandModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GoToAppCount = table.Column<long>(type: "bigint", nullable: false),
                    ViewDetailsCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandActionSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DealAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeOfAction = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    OffreID = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeOfAction = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SocialMedia = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSearch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SearchText = table.Column<string>(type: "text", nullable: true),
                    SearchTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HasResults = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSearch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateLogin = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateLogout = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AggregatorItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AggregatorItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DealActionEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AggregatorItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AggregatorItem_DealAction_DealActionEntityId",
                        column: x => x.DealActionEntityId,
                        principalTable: "DealAction",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AggregatorItem_DealActionEntityId",
                table: "AggregatorItem",
                column: "DealActionEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AggregatorItem");

            migrationBuilder.DropTable(
                name: "BrandAction");

            migrationBuilder.DropTable(
                name: "BrandActionSummary");

            migrationBuilder.DropTable(
                name: "OfferAction");

            migrationBuilder.DropTable(
                name: "UserSearch");

            migrationBuilder.DropTable(
                name: "UserStatus");

            migrationBuilder.DropTable(
                name: "DealAction");
        }
    }
}
