using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Platform.ReferentialData.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "AgeRange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MaxAge = table.Column<int>(type: "integer", nullable: false),
                    MinAge = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandMatching",
                columns: table => new
                {
                    AggregatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocalBrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    DistantBrandId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandMatching", x => new { x.AggregatorId, x.LocalBrandId });
                });

            migrationBuilder.CreateTable(
                name: "ClenderWorkingTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClenderWorkingTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    CountryKey = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryMode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationAuthentication",
                columns: table => new
                {
                    AuthenticationId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthenticationType = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    APIkey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationAuthentication", x => x.AuthenticationId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<int>(type: "integer", nullable: false),
                    isDefault = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageResourceSet",
                columns: table => new
                {
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageResourceSet", x => x.LanguageResourceSetId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatorUserId = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleterUserId = table.Column<string>(type: "text", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuportCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HelpSupportType = table.Column<int>(type: "integer", nullable: false),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuportCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TermsService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermsService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ThemeUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Picture = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<string>(type: "text", nullable: true),
                    UserType = table.Column<string>(type: "text", nullable: true),
                    AssignedTo = table.Column<string>(type: "text", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    NumberOfOperation = table.Column<string>(type: "text", nullable: true),
                    lastActiveDate = table.Column<string>(type: "text", nullable: true),
                    RequestCode = table.Column<string>(type: "text", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RejectTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuthentificationSource = table.Column<int>(type: "integer", nullable: false),
                    MacAddress = table.Column<string>(type: "text", nullable: true),
                    FCMToken = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    ResetCode = table.Column<string>(type: "text", nullable: true),
                    ResetCodeExpireTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorUserId = table.Column<string>(type: "text", nullable: true),
                    DeleterUserId = table.Column<string>(type: "text", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "text", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: false),
                    AndLink = table.Column<string>(type: "text", nullable: true),
                    IOSLink = table.Column<string>(type: "text", nullable: true),
                    WebLink = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    AdminScore = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    OtherDescription = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "DayWorkingTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    MorningStartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MorningCloseTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AfterNoonStartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AfterNoonCloseTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    ClenderWorkingTimeEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayWorkingTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayWorkingTime_ClenderWorkingTime_ClenderWorkingTimeEntityId",
                        column: x => x.ClenderWorkingTimeEntityId,
                        principalTable: "ClenderWorkingTime",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExceptionWeekWorkingTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeActivated = table.Column<bool>(type: "boolean", nullable: false),
                    ClenderWorkingTimeEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionWeekWorkingTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExceptionWeekWorkingTime_ClenderWorkingTime_ClenderWorkingT~",
                        column: x => x.ClenderWorkingTimeEntityId,
                        principalTable: "ClenderWorkingTime",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodType_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitchenType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitchenType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitchenType_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageKey = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageResource_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageResource_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealTiming",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTiming", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealTiming_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealType_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    VendorType = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<string>(type: "text", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BrandOfferLink = table.Column<string>(type: "text", nullable: true),
                    FacebookOfferLink = table.Column<string>(type: "text", nullable: true),
                    TwitterOfferLink = table.Column<string>(type: "text", nullable: true),
                    InstagramOfferLink = table.Column<string>(type: "text", nullable: true),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId");
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Region_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddressType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddressType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddressType_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    QuestionLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    SuportCategoryId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_SuportCategory_SuportCategoryId",
                        column: x => x.SuportCategoryId,
                        principalTable: "SuportCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagLanguage",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagLanguage", x => new { x.TagId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_TagLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagLanguage_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "Security",
                columns: table => new
                {
                    UserEntityId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Token = table.Column<string>(type: "text", nullable: true),
                    ExpiresOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.UserEntityId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserEntityId",
                        column: x => x.UserEntityId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddresse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    UserAddressTypeId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AddressType = table.Column<int>(type: "integer", nullable: false),
                    FullAddress = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AddressLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddresse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAddresse_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserOTPVerification",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    OTPVerificationCode = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOTPVerification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOTPVerification_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicIntegration",
                columns: table => new
                {
                    DynamicIntegrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    URi = table.Column<string>(type: "text", nullable: false),
                    Port = table.Column<string>(type: "text", nullable: true),
                    http = table.Column<string>(type: "text", nullable: true),
                    AuthenticationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicIntegration", x => x.DynamicIntegrationId);
                    table.ForeignKey(
                        name: "FK_DynamicIntegration_IntegrationAuthentication_Authentication~",
                        column: x => x.AuthenticationId,
                        principalTable: "IntegrationAuthentication",
                        principalColumn: "AuthenticationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DynamicIntegration_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagVendor",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagVendor", x => new { x.TagId, x.VendorId });
                    table.ForeignKey(
                        name: "FK_TagVendor_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagVendor_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorDeliveryMode",
                columns: table => new
                {
                    DeliveryModeId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDeliveryMode", x => new { x.VendorId, x.DeliveryModeId });
                    table.ForeignKey(
                        name: "FK_VendorDeliveryMode_DeliveryMode_DeliveryModeId",
                        column: x => x.DeliveryModeId,
                        principalTable: "DeliveryMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorDeliveryMode_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionDayWorkingTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Day = table.Column<int>(type: "integer", nullable: true),
                    MorningStartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MorningCloseTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AfterNoonStartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AfterNoonCloseTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    ExceptionWeekWorkingTimeEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionDayWorkingTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExceptionDayWorkingTime_ExceptionWeekWorkingTime_ExceptionW~",
                        column: x => x.ExceptionWeekWorkingTimeEntityId,
                        principalTable: "ExceptionWeekWorkingTime",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TagFoodType",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    FoodTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagFoodType", x => new { x.TagId, x.FoodTypeId });
                    table.ForeignKey(
                        name: "FK_TagFoodType_FoodType_FoodTypeId",
                        column: x => x.FoodTypeId,
                        principalTable: "FoodType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagFoodType_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorFoodType",
                columns: table => new
                {
                    FoodTypeId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorFoodType", x => new { x.VendorId, x.FoodTypeId });
                    table.ForeignKey(
                        name: "FK_VendorFoodType_FoodType_FoodTypeId",
                        column: x => x.FoodTypeId,
                        principalTable: "FoodType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorFoodType_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagKitchenType",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    KitchenTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagKitchenType", x => new { x.TagId, x.KitchenTypeId });
                    table.ForeignKey(
                        name: "FK_TagKitchenType_KitchenType_KitchenTypeId",
                        column: x => x.KitchenTypeId,
                        principalTable: "KitchenType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagKitchenType_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorKitchenType",
                columns: table => new
                {
                    KitchenTypeId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorKitchenType", x => new { x.VendorId, x.KitchenTypeId });
                    table.ForeignKey(
                        name: "FK_VendorKitchenType_KitchenType_KitchenTypeId",
                        column: x => x.KitchenTypeId,
                        principalTable: "KitchenType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorKitchenType_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagMealTiming",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    MealTimingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMealTiming", x => new { x.TagId, x.MealTimingId });
                    table.ForeignKey(
                        name: "FK_TagMealTiming_MealTiming_MealTimingId",
                        column: x => x.MealTimingId,
                        principalTable: "MealTiming",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagMealTiming_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorMealTiming",
                columns: table => new
                {
                    MealTimingId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorMealTiming", x => new { x.VendorId, x.MealTimingId });
                    table.ForeignKey(
                        name: "FK_VendorMealTiming_MealTiming_MealTimingId",
                        column: x => x.MealTimingId,
                        principalTable: "MealTiming",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorMealTiming_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagMealType",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    MealTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagMealType", x => new { x.TagId, x.MealTypeId });
                    table.ForeignKey(
                        name: "FK_TagMealType_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagMealType_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorMealType",
                columns: table => new
                {
                    MealTypeId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorMealType", x => new { x.VendorId, x.MealTypeId });
                    table.ForeignKey(
                        name: "FK_VendorMealType_MealType_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorMealType_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagOffer",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagOffer", x => new { x.TagId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_TagOffer_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagOffer_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    CityCode = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_City_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagRegion",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagRegion", x => new { x.TagId, x.RegionId });
                    table.ForeignKey(
                        name: "FK_TagRegion_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagRegion_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationMethod",
                columns: table => new
                {
                    IntegrationMethodId = table.Column<Guid>(type: "uuid", nullable: false),
                    DynamicIntegrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UseDefaultAuth = table.Column<bool>(type: "boolean", nullable: false),
                    IntegrationType = table.Column<int>(type: "integer", nullable: false),
                    EndPoint = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<int>(type: "integer", nullable: false),
                    MethodType = table.Column<int>(type: "integer", nullable: false),
                    AuthenticationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationMethod", x => x.IntegrationMethodId);
                    table.ForeignKey(
                        name: "FK_IntegrationMethod_DynamicIntegration_DynamicIntegrationId",
                        column: x => x.DynamicIntegrationId,
                        principalTable: "DynamicIntegration",
                        principalColumn: "DynamicIntegrationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegrationMethod_IntegrationAuthentication_AuthenticationId",
                        column: x => x.AuthenticationId,
                        principalTable: "IntegrationAuthentication",
                        principalColumn: "AuthenticationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    NameLabelCode = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    AreaName = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Area_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagCity",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCity", x => new { x.TagId, x.CityId });
                    table.ForeignKey(
                        name: "FK_TagCity_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagCity_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationParameter",
                columns: table => new
                {
                    IntegrationParameterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    MatchWithKey = table.Column<int>(type: "integer", nullable: false),
                    QueryOrBody = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    MatchWithValue = table.Column<int>(type: "integer", nullable: false),
                    MethodId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationParameter", x => x.IntegrationParameterId);
                    table.ForeignKey(
                        name: "FK_IntegrationParameter_IntegrationMethod_MethodId",
                        column: x => x.MethodId,
                        principalTable: "IntegrationMethod",
                        principalColumn: "IntegrationMethodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    AreaId = table.Column<int>(type: "integer", nullable: false),
                    LanguageResourceSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zones_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zones_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zones_LanguageResourceSet_LanguageResourceSetId",
                        column: x => x.LanguageResourceSetId,
                        principalTable: "LanguageResourceSet",
                        principalColumn: "LanguageResourceSetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zones_Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StaticIntegration",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ZoneId = table.Column<int>(type: "integer", nullable: false),
                    StaticIntegrationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Fees = table.Column<double>(type: "double precision", nullable: false),
                    TimeEstimation = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatorUserId = table.Column<int>(type: "integer", nullable: true),
                    DeleterUserId = table.Column<int>(type: "integer", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserId = table.Column<int>(type: "integer", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticIntegration", x => new { x.VendorId, x.ZoneId });
                    table.ForeignKey(
                        name: "FK_StaticIntegration_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaticIntegration_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagZone",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    ZoneId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagZone", x => new { x.TagId, x.ZoneId });
                    table.ForeignKey(
                        name: "FK_TagZone_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagZone_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorDeliveryZones",
                columns: table => new
                {
                    ZoneId = table.Column<int>(type: "integer", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorDeliveryZones", x => new { x.VendorId, x.ZoneId });
                    table.ForeignKey(
                        name: "FK_VendorDeliveryZones_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorDeliveryZones_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Code", "CountryKey", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LastModificationTime", "LastModifierUserId", "NameLabelCode", "Status" },
                values: new object[] { 1, "sao", "sao", null, null, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 });

            migrationBuilder.InsertData(
                table: "DeliveryMode",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "ImageLabelCode", "LastModificationTime", "LastModifierUserId", "Name", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), null, null, "Delivery", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1be9"), null, null, "Carhop", new Guid("ce646e7d-e307-4a12-a795-33004abb1be9"), 5 }
                });

            migrationBuilder.InsertData(
                table: "IntegrationAuthentication",
                columns: new[] { "AuthenticationId", "APIkey", "AuthenticationType", "Login", "Password", "Token" },
                values: new object[,]
                {
                    { new Guid("0efd66e7-e4ad-42a5-928c-241c4cb71ae4"), "", 0, "", "", "" },
                    { new Guid("0efd66e7-f5ad-42a5-928c-241c4cb71ae4"), "", 0, "", "", "" },
                    { new Guid("16ce9df4-a1a3-4e3e-80cd-49be1d3f6913"), "", 1, "admin", "password", "" }
                });

            migrationBuilder.InsertData(
                table: "LanguageResourceSet",
                columns: new[] { "LanguageResourceSetId", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LastModificationTime", "LastModifierUserId", "Status" },
                values: new object[,]
                {
                    { new Guid("088ae5f8-6bd2-4ee2-777e-a9d44449f4ca"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae5f8-6bd2-4ee2-667e-a9d44449f4ca"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ef"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae6f8-6bd2-4ee2-887e-a9d44449f414"), null, null, null, null, null, null, 5 },
                    { new Guid("099ae7f8-6bd2-4ee2-887e-a9d44449f413"), null, null, null, null, null, null, 5 },
                    { new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e"), null, null, null, null, null, null, 5 },
                    { new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"), null, null, null, null, null, null, 5 },
                    { new Guid("3ee5766a-af6a-436b-b588-b9ef645867f0"), null, null, null, null, null, null, 5 },
                    { new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, null, null, null, null, 5 },
                    { new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4"), null, null, null, null, null, null, 5 },
                    { new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff7"), null, null, null, null, null, null, 5 },
                    { new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff8"), null, null, null, null, null, null, 5 },
                    { new Guid("e5a05881-856c-4453-99b2-74222677ceb7"), null, null, null, null, null, null, 5 },
                    { new Guid("ef192566-67d7-4f85-b724-f8163019723a"), null, null, null, null, null, null, 5 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LastModificationTime", "LastModifierUserId", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { "5310d489-98d2-416e-bbf4-7badc5197f73", "6759e4da-5107-4242-8866-302946db944c", new DateTime(2023, 9, 18, 22, 23, 19, 936, DateTimeKind.Utc).AddTicks(3420), "No User", null, null, null, null, "CLIENT", "CLIENT", 0 },
                    { "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684", "5362f477-c37d-4092-a1fc-fe754e3df3b1", new DateTime(2023, 9, 18, 22, 23, 19, 936, DateTimeKind.Utc).AddTicks(3408), "No User", null, null, null, null, "ADMINISTRATOR", "ADMINISTRATOR", 0 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "User",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ApprovalTime", "AssignedTo", "AuthentificationSource", "ConcurrencyStamp", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "Email", "EmailConfirmed", "FCMToken", "FullName", "Gender", "LastModificationTime", "LastModifierUserId", "Location", "LockoutEnabled", "LockoutEnd", "MacAddress", "NormalizedEmail", "NormalizedUserName", "NumberOfOperation", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "RejectTime", "RequestCode", "RequestTime", "ResetCode", "ResetCodeExpireTime", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName", "UserType", "lastActiveDate" },
                values: new object[] { "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f", 0, null, null, null, 0, "ba7e50ad-3144-4630-89c6-18becc86da37", new DateTime(2023, 9, 18, 22, 23, 19, 936, DateTimeKind.Utc).AddTicks(4388), "No User", null, null, "mail.foodme@gmail.com", true, null, null, null, null, null, null, false, null, null, "MAIL.FOODME@GMAIL.COM", "ADMINISTRATOR", null, "AQAAAAIAAYagAAAAENLdM/EkqB39cBIAo3ES1ETwBwrWbW2+xTeq99kzyq9I6Q3DRSWkGxWD0DZpnXxxEQ==", "515553891", false, "", null, null, null, null, null, "1e9cdb01-7614-4ea4-9979-c4cdc0495955", 0, false, "Administrator", null, null });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "VendorId", "AdminScore", "AndLink", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "Description", "IOSLink", "LastModificationTime", "LastModifierUserId", "Logo", "Name", "OtherDescription", "Status", "Type", "WebLink" },
                values: new object[,]
                {
                    { new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"), 0, "", null, null, null, null, "MAGMA is proud to offer you a refined cuisine that combines the excellence of French gastronomy with the Japanese culinary techniques of its chef, Ryuya ONO. ", "", null, null, "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", "Magma", "At MAGMA, we work closely with carefully selected premium French producers. Our menu evolves daily based on the day's arrivals, where surprise and discovery are integral parts of your visit to MAGMA. ", 0, 0, "https://restaurantmagma.com/en/" },
                    { new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 0, "", null, null, null, null, "Looking for Chick-N-Dip delivery? Take a look at the menus and lists", "", null, null, "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", "ChikNDip", "order what you need and get it delivered to you. Ready, steady", 0, 0, "https://www.facebook.com/chickndipksa/" },
                    { new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"), 0, "", null, null, null, null, "kfc saudi from saudi.kfc.me\r\nOrder great tasting fried chicken, sandwiches & family meals online with KFC Delivery. ", "", null, null, "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", "KFC", "Attractive combos & deals available from our menu for a 'so good' ", 0, 0, "https://saudi.kfc.me/en/home" }
                });

            migrationBuilder.InsertData(
                table: "DynamicIntegration",
                columns: new[] { "DynamicIntegrationId", "AuthenticationId", "Port", "URi", "VendorId", "http" },
                values: new object[,]
                {
                    { new Guid("ce646e7d-e154-4a12-a795-33004abb1bd8"), new Guid("16ce9df4-a1a3-4e3e-80cd-49be1d3f6913"), "3004", "localhost", new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"), "http" },
                    { new Guid("ce646e7d-e154-4a17-a000-33004abb1bd8"), new Guid("0efd66e7-e4ad-42a5-928c-241c4cb71ae4"), "3004", "localhost", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), "http" },
                    { new Guid("ce646e7d-e154-4a17-a795-33004acc1bd8"), new Guid("0efd66e7-f5ad-42a5-928c-241c4cb71ae4"), "3004", "localhost", new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"), "http" }
                });

            migrationBuilder.InsertData(
                table: "FoodType",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "ImageLabelCode", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Name", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce"), null, null, "Fish", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e"), null, null, "Pizza", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 5 },
                    { 3, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"), new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"), null, null, "Meat", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"), 5 }
                });

            migrationBuilder.InsertData(
                table: "KitchenType",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "ImageLabelCode", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Name", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), new Guid("099ae5f8-6bd2-4ee2-667e-a9d44449f4ca"), null, null, "American", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), new Guid("088ae5f8-6bd2-4ee2-777e-a9d44449f4ca"), null, null, "Italian", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 5 },
                    { 3, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"), new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ef"), null, null, "Indian", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"), 5 }
                });

            migrationBuilder.InsertData(
                table: "LanguageResource",
                columns: new[] { "Id", "Code", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "Description", "Image", "LanguageId", "LanguageKey", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Status", "Value" },
                values: new object[,]
                {
                    { 1, new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce"), null, null, null, null, "", null, null, 1, new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ce"), null, null, 5, "Fish" },
                    { 2, new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e"), null, null, null, null, "", null, null, 1, new Guid("13488271-3653-41a0-9287-dc65ab6d2d7e"), null, null, 5, "Pizza" },
                    { 3, new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"), null, null, null, null, "", null, null, 1, new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"), null, null, 5, "Meat" },
                    { 4, new Guid("310363c2-3044-47c7-81ea-fababb60d3dc"), null, null, null, null, "", null, null, 1, new Guid("3ee5766a-af6a-436b-b588-b9ef645867f0"), null, null, 5, "Family Meal" },
                    { 5, new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, null, null, "", null, null, 1, new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, 5, "Happy Meal" },
                    { 6, new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4"), null, null, null, null, "", null, null, 1, new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4"), null, null, 5, "BreakFast" },
                    { 7, new Guid("e5a05881-856c-4453-99b2-74222677ceb7"), null, null, null, null, "", null, null, 1, new Guid("e5a05881-856c-4453-99b2-74222677ceb7"), null, null, 5, "Lunch" },
                    { 8, new Guid("ef192566-67d7-4f85-b724-f8163019723a"), null, null, null, null, "", null, null, 1, new Guid("ef192566-67d7-4f85-b724-f8163019723a"), null, null, 5, "Dinner" },
                    { 9, new Guid("074be5f8-6bd2-4ee2-667e-a9d44449f4ca"), null, null, null, null, "", null, null, 1, new Guid("099ae5f8-6bd2-4ee2-667e-a9d44449f4ca"), null, null, 5, "American" },
                    { 10, new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ea"), null, null, null, null, "", null, null, 1, new Guid("088ae5f8-6bd2-4ee2-777e-a9d44449f4ca"), null, null, 5, "Italian" },
                    { 11, new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4ca"), null, null, null, null, "", null, null, 1, new Guid("099ae5f8-6bd2-4ee2-777e-a9d44449f4ef"), null, null, 5, "Indian" },
                    { 12, new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe"), null, null, null, null, "", null, null, 1, new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe"), null, null, 5, "Jeddah" },
                    { 13, new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0"), null, null, null, null, "", null, null, 1, new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0"), null, null, 5, "Riadh" },
                    { 14, new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, null, null, "", null, null, 1, new Guid("099ae7f8-6bd2-4ee2-887e-a9d44449f413"), null, null, 5, "Jeddah saudi arabia" },
                    { 15, new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, null, null, "", null, null, 1, new Guid("099ae6f8-6bd2-4ee2-887e-a9d44449f414"), null, null, 5, "Riadh saudi arabia" },
                    { 16, new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, null, null, "", null, null, 1, new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff7"), null, null, 5, "Jeddah saudi arabia" },
                    { 17, new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, null, null, "", null, null, 1, new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff8"), null, null, 5, "Riadh saudi arabia" }
                });

            migrationBuilder.InsertData(
                table: "MealTiming",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "ImageLabelCode", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Name", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), new Guid("d2ab81c5-b9b3-4b14-827e-d24da57d6ff4"), null, null, "BreakFast", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), new Guid("e5a05881-856c-4453-99b2-74222677ceb7"), null, null, "Lunch", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 5 },
                    { 3, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"), new Guid("ef192566-67d7-4f85-b724-f8163019723a"), null, null, "Dinner", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd7"), 5 }
                });

            migrationBuilder.InsertData(
                table: "MealType",
                columns: new[] { "Id", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "ImageLabelCode", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Name", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), new Guid("3ee5766a-af6a-436b-b588-b9ef645867f0"), null, null, "Family Meal", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, null, null, null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), new Guid("48178984-0325-469a-a611-6c29c5610f13"), null, null, "Happy Meal", new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 5 }
                });

            migrationBuilder.InsertData(
                table: "Region",
                columns: new[] { "Id", "CountryId", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, null, null, null, new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff7"), null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, 1, null, null, null, null, new Guid("d2ab81c5-b9b3-6b14-827e-d24da57d6ff8"), null, null, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 5 }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "RoleClaim",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "Permission.Brand.ViewAll", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 2, "Permission", "Permission.Brand.View", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 3, "Permission", "Permission.Aggregator.ViewAll", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 4, "Permission", "Permission.Aggregator.View", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 5, "Permission", "Permission.UserManagement.View", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 6, "Permission", "Permission.UserManagement.Update", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 7, "Permission", "Permission.UserManagement.Delete", "5310d489-98d2-416e-bbf4-7badc5197f73" },
                    { 8, "Permission", "Permission.Offers.ViewAll", "5310d489-98d2-416e-bbf4-7badc5197f73" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8f1e9ce9-ab8c-4aaf-958b-596b88dc6684", "6bf1ea57-1d07-4e8f-97d4-2b3c050a979f" });

            migrationBuilder.InsertData(
                table: "VendorDeliveryMode",
                columns: new[] { "DeliveryModeId", "VendorId" },
                values: new object[,]
                {
                    { 1, new Guid("3a7b3888-9b89-459a-a108-e06aefec5500") },
                    { 1, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8") },
                    { 1, new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8") }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CityCode", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Latitude", "Longitude", "NameLabelCode", "RegionId", "Status" },
                values: new object[,]
                {
                    { 1, "Jeddah saudi arabia", null, null, null, null, new Guid("099ae5f8-6bd2-4ee2-887e-a9d44449f4fe"), null, null, 856.25400000000002, 104.256, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 1, 5 },
                    { 2, "Riadh saudi arabia", null, null, null, null, new Guid("099ae5f7-6bd2-4ee2-887e-a9d44449f4f0"), null, null, 745.25599999999997, 201.589, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "IntegrationMethod",
                columns: new[] { "IntegrationMethodId", "AuthenticationId", "Content", "DynamicIntegrationId", "EndPoint", "IntegrationType", "MethodType", "UseDefaultAuth" },
                values: new object[,]
                {
                    { new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), new Guid("0efd66e7-f5ad-42a5-928c-241c4cb71ae4"), 0, new Guid("ce646e7d-e154-4a17-a795-33004acc1bd8"), "NotsecureApi/Magma/deliveryInfo", 0, 1, true },
                    { new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), new Guid("16ce9df4-a1a3-4e3e-80cd-49be1d3f6913"), 0, new Guid("ce646e7d-e154-4a12-a795-33004abb1bd8"), "SecureApi/KFC/deliveryInfo", 0, 1, true },
                    { new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), new Guid("0efd66e7-e4ad-42a5-928c-241c4cb71ae4"), 0, new Guid("ce646e7d-e154-4a17-a000-33004abb1bd8"), "NotsecureApi/chikeNDip/deliveryInfo", 0, 1, true }
                });

            migrationBuilder.InsertData(
                table: "VendorFoodType",
                columns: new[] { "FoodTypeId", "VendorId" },
                values: new object[,]
                {
                    { 2, new Guid("3a7b3888-9b89-459a-a108-e06aefec5500") },
                    { 3, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8") },
                    { 1, new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8") }
                });

            migrationBuilder.InsertData(
                table: "VendorKitchenType",
                columns: new[] { "KitchenTypeId", "VendorId" },
                values: new object[,]
                {
                    { 2, new Guid("3a7b3888-9b89-459a-a108-e06aefec5500") },
                    { 3, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8") },
                    { 1, new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8") }
                });

            migrationBuilder.InsertData(
                table: "VendorMealTiming",
                columns: new[] { "MealTimingId", "VendorId" },
                values: new object[,]
                {
                    { 2, new Guid("3a7b3888-9b89-459a-a108-e06aefec5500") },
                    { 3, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8") },
                    { 1, new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8") }
                });

            migrationBuilder.InsertData(
                table: "VendorMealType",
                columns: new[] { "MealTypeId", "VendorId" },
                values: new object[,]
                {
                    { 2, new Guid("3a7b3888-9b89-459a-a108-e06aefec5500") },
                    { 2, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8") },
                    { 1, new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8") }
                });

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "AreaName", "CityId", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LastModificationTime", "LastModifierUserId", "Latitude", "Longitude", "NameLabelCode", "Status" },
                values: new object[,]
                {
                    { 1, "", 1, null, null, null, null, null, null, 856.25400000000002, 104.258, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 5 },
                    { 2, "", 2, null, null, null, null, null, null, 745.25599999999997, 201.589, new Guid("ce646e7d-e307-4a12-a795-33004abb1bd9"), 5 }
                });

            migrationBuilder.InsertData(
                table: "IntegrationParameter",
                columns: new[] { "IntegrationParameterId", "Key", "MatchWithKey", "MatchWithValue", "MethodId", "QueryOrBody", "Type" },
                values: new object[,]
                {
                    { new Guid("0127afbd-ade2-49ee-ae67-f034566c692d"), "Latitude", 5, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 0 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f034566c693c"), "Longitude", 6, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 0 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c0285"), "TimeEstimation", 10, 0, new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c100d"), "Aggregator", 11, 0, new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c1012"), "Rating", 8, 0, new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c1103"), "Distance", 7, 0, new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c166d"), "Fees", 9, 0, new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c1745"), "TimeEstimation", 10, 0, new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c198d"), "DeliveryZone", 0, 0, new Guid("b5204c9b-cfd7-431b-9dfa-3c373e92fa89"), 1, 0 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c200d"), "Aggregator", 11, 0, new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c4782"), "Rating", 8, 0, new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c654d"), "Fees", 9, 0, new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), 1, 1 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c692d"), "DeliveryZone", 0, 0, new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), 1, 0 },
                    { new Guid("0127afbd-ade2-49ee-ae67-f038866c9633"), "Distance", 7, 0, new Guid("2e635561-5ebb-4774-b9dd-8a6603153990"), 1, 1 },
                    { new Guid("0127afbd-ade2-50ee-ae67-f034566c692d"), "TimeEstimation", 10, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 1 },
                    { new Guid("0127afbd-ade2-51ee-ae67-f034566c692d"), "Fees", 9, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 1 },
                    { new Guid("0127afbd-ade2-52ee-ae67-f034566c692d"), "Rating", 8, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 1 },
                    { new Guid("0127afbd-ade2-53ee-ae67-f034566c692d"), "Distance", 7, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 1 },
                    { new Guid("0127afbd-ade2-54ee-ae67-f034566c692d"), "Aggregator", 11, 0, new Guid("623320f2-e01d-42ad-a5fb-d56cb3448a38"), 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "Id", "AreaId", "CityId", "CountryId", "CreationTime", "CreatorUserId", "DeleterUserId", "DeletionTime", "LanguageResourceSetId", "LastModificationTime", "LastModifierUserId", "Name", "RegionId", "Status" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, null, null, null, null, new Guid("099ae7f8-6bd2-4ee2-887e-a9d44449f413"), null, null, "Jeddah Saudi Arabia", 1, 5 },
                    { 2, 2, 2, 1, null, null, null, null, new Guid("099ae6f8-6bd2-4ee2-887e-a9d44449f414"), null, null, "Riadh Saudi Arabia", 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "VendorDeliveryZones",
                columns: new[] { "VendorId", "ZoneId" },
                values: new object[,]
                {
                    { new Guid("3a7b3888-9b89-459a-a108-e06aefec5500"), 1 },
                    { new Guid("ce646e7d-e307-4a12-a795-33004abb1bd8"), 1 },
                    { new Guid("ce748e7d-e307-4a12-a795-33004abb1bd8"), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_CityId",
                table: "Area",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_City_LanguageResourceSetId",
                table: "City",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_City_RegionId",
                table: "City",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_DayWorkingTime_ClenderWorkingTimeEntityId",
                table: "DayWorkingTime",
                column: "ClenderWorkingTimeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicIntegration_AuthenticationId",
                table: "DynamicIntegration",
                column: "AuthenticationId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicIntegration_VendorId",
                table: "DynamicIntegration",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionDayWorkingTime_ExceptionWeekWorkingTimeEntityId",
                table: "ExceptionDayWorkingTime",
                column: "ExceptionWeekWorkingTimeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionWeekWorkingTime_ClenderWorkingTimeEntityId",
                table: "ExceptionWeekWorkingTime",
                column: "ClenderWorkingTimeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodType_LanguageResourceSetId",
                table: "FoodType",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationMethod_AuthenticationId",
                table: "IntegrationMethod",
                column: "AuthenticationId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationMethod_DynamicIntegrationId",
                table: "IntegrationMethod",
                column: "DynamicIntegrationId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationParameter_MethodId",
                table: "IntegrationParameter",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_KitchenType_LanguageResourceSetId",
                table: "KitchenType",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageResource_LanguageId",
                table: "LanguageResource",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageResource_LanguageResourceSetId",
                table: "LanguageResource",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_MealTiming_LanguageResourceSetId",
                table: "MealTiming",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_MealType_LanguageResourceSetId",
                table: "MealType",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_LanguageResourceSetId",
                table: "Offer",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_SuportCategoryId",
                table: "QuestionAnswer",
                column: "SuportCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_CountryId",
                table: "Region",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Region_LanguageResourceSetId",
                table: "Region",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "Security",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticIntegration_ZoneId",
                table: "StaticIntegration",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_TagCity_CityId",
                table: "TagCity",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TagFoodType_FoodTypeId",
                table: "TagFoodType",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TagKitchenType_KitchenTypeId",
                table: "TagKitchenType",
                column: "KitchenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TagLanguage_LanguageId",
                table: "TagLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMealTiming_MealTimingId",
                table: "TagMealTiming",
                column: "MealTimingId");

            migrationBuilder.CreateIndex(
                name: "IX_TagMealType_MealTypeId",
                table: "TagMealType",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TagOffer_OfferId",
                table: "TagOffer",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TagRegion_RegionId",
                table: "TagRegion",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TagVendor_VendorId",
                table: "TagVendor",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_TagZone_ZoneId",
                table: "TagZone",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresse_UserId",
                table: "UserAddresse",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddressType_LanguageResourceSetId",
                table: "UserAddressType",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "Security",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "Security",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOTPVerification_UserId",
                schema: "Security",
                table: "UserOTPVerification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Security",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDeliveryMode_DeliveryModeId",
                table: "VendorDeliveryMode",
                column: "DeliveryModeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorDeliveryZones_ZoneId",
                table: "VendorDeliveryZones",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorFoodType_FoodTypeId",
                table: "VendorFoodType",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorKitchenType_KitchenTypeId",
                table: "VendorKitchenType",
                column: "KitchenTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorMealTiming_MealTimingId",
                table: "VendorMealTiming",
                column: "MealTimingId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorMealType_MealTypeId",
                table: "VendorMealType",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_AreaId",
                table: "Zones",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_CityId",
                table: "Zones",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_CountryId",
                table: "Zones",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_LanguageResourceSetId",
                table: "Zones",
                column: "LanguageResourceSetId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_RegionId",
                table: "Zones",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgeRange");

            migrationBuilder.DropTable(
                name: "BrandMatching");

            migrationBuilder.DropTable(
                name: "DayWorkingTime");

            migrationBuilder.DropTable(
                name: "ExceptionDayWorkingTime");

            migrationBuilder.DropTable(
                name: "IntegrationParameter");

            migrationBuilder.DropTable(
                name: "LanguageResource");

            migrationBuilder.DropTable(
                name: "QuestionAnswer");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "StaticIntegration");

            migrationBuilder.DropTable(
                name: "TagCity");

            migrationBuilder.DropTable(
                name: "TagFoodType");

            migrationBuilder.DropTable(
                name: "TagKitchenType");

            migrationBuilder.DropTable(
                name: "TagLanguage");

            migrationBuilder.DropTable(
                name: "TagMealTiming");

            migrationBuilder.DropTable(
                name: "TagMealType");

            migrationBuilder.DropTable(
                name: "TagOffer");

            migrationBuilder.DropTable(
                name: "TagRegion");

            migrationBuilder.DropTable(
                name: "TagVendor");

            migrationBuilder.DropTable(
                name: "TagZone");

            migrationBuilder.DropTable(
                name: "TermsService");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "UserAddresse");

            migrationBuilder.DropTable(
                name: "UserAddressType");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserOTPVerification",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "VendorDeliveryMode");

            migrationBuilder.DropTable(
                name: "VendorDeliveryZones");

            migrationBuilder.DropTable(
                name: "VendorFoodType");

            migrationBuilder.DropTable(
                name: "VendorKitchenType");

            migrationBuilder.DropTable(
                name: "VendorMealTiming");

            migrationBuilder.DropTable(
                name: "VendorMealType");

            migrationBuilder.DropTable(
                name: "ExceptionWeekWorkingTime");

            migrationBuilder.DropTable(
                name: "IntegrationMethod");

            migrationBuilder.DropTable(
                name: "SuportCategory");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "DeliveryMode");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "FoodType");

            migrationBuilder.DropTable(
                name: "KitchenType");

            migrationBuilder.DropTable(
                name: "MealTiming");

            migrationBuilder.DropTable(
                name: "MealType");

            migrationBuilder.DropTable(
                name: "ClenderWorkingTime");

            migrationBuilder.DropTable(
                name: "DynamicIntegration");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "IntegrationAuthentication");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "LanguageResourceSet");
        }
    }
}
