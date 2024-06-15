using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EdiplanDotnetAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "AssetIds");

            migrationBuilder.CreateTable(
                name: "asset_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentGroupId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_asset_group_asset_group_ParentGroupId1",
                        column: x => x.ParentGroupId1,
                        principalTable: "asset_group",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "booking_group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"AssetIds\"')"),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: true),
                    RateUnit = table.Column<decimal>(type: "numeric", nullable: true),
                    AssetNumber = table.Column<string>(type: "text", nullable: true),
                    Make = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "production",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"AssetIds\"')"),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: true),
                    RateUnit = table.Column<decimal>(type: "numeric", nullable: true),
                    UsedFor = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "asset_group_map",
                columns: table => new
                {
                    AssetGroupsId = table.Column<int>(type: "integer", nullable: false),
                    AssetsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_group_map", x => new { x.AssetGroupsId, x.AssetsId });
                    table.ForeignKey(
                        name: "FK_asset_group_map_asset_group_AssetGroupsId",
                        column: x => x.AssetGroupsId,
                        principalTable: "asset_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ProductionId = table.Column<Guid>(type: "uuid", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_booking_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_booking_production_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "production",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"AssetIds\"')"),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: true),
                    RateUnit = table.Column<decimal>(type: "numeric", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    IsStaff = table.Column<bool>(type: "boolean", nullable: false),
                    ProductionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_production_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "production",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AssetBooking",
                columns: table => new
                {
                    AssetsId = table.Column<int>(type: "integer", nullable: false),
                    BookingsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetBooking", x => new { x.AssetsId, x.BookingsId });
                    table.ForeignKey(
                        name: "FK_AssetBooking_booking_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "booking_group_map",
                columns: table => new
                {
                    BookingGroupsId = table.Column<int>(type: "integer", nullable: false),
                    BookingsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_booking_group_map", x => new { x.BookingGroupsId, x.BookingsId });
                    table.ForeignKey(
                        name: "FK_booking_group_map_booking_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_booking_group_map_booking_group_BookingGroupsId",
                        column: x => x.BookingGroupsId,
                        principalTable: "booking_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "Notes" },
                values: new object[,]
                {
                    { new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "Top Gear Production Office, London", "" },
                    { new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "The Crown Production Office, London", "" },
                    { new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "Picture Shop, Manchester", "" },
                    { new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "True North Productions, Leeds", "" }
                });

            migrationBuilder.InsertData(
                table: "booking_group",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Offline" },
                    { 2, "Online" },
                    { 3, "Dub - In House" },
                    { 4, "Grade" }
                });

            migrationBuilder.InsertData(
                table: "equipment",
                columns: new[] { "Id", "AssetNumber", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "Make", "Model", "Name", "Rate", "RateUnit", "Type", "Value" },
                values: new object[,]
                {
                    { -5, "23452", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Online machine", null, null, "Hewlett-Packard", "Z8 G4", "Edit04", null, null, "equipment", 2000m },
                    { -4, "13352", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine", null, null, "Hewlett-Packard", "Z4 G4", "Edit03", null, null, "equipment", 1000m },
                    { -3, "12432", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine", null, null, "Hewlett-Packard", "Z4 G4", "Edit02", null, null, "equipment", 1000m },
                    { -2, "12452", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine", null, null, "Hewlett-Packard", "Z4 G4", "Edit01", null, null, "equipment", 1000m },
                    { -1, "12442", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faulty lense.", null, null, "Sony", "FX-6", "Sony FX6", null, null, "equipment", 1000m }
                });

            migrationBuilder.InsertData(
                table: "person",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "Email", "IsStaff", "LastModifiedBy", "LastModifiedDate", "Name", "PhoneNumber", "ProductionId", "Rate", "RateUnit", "Role", "Type" },
                values: new object[,]
                {
                    { -10, "36 Elm Close, Liverpool, L5 6GH", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.programly@tvgeekery.co.uk", false, null, null, "Dave Programly", "07704 567890", null, null, null, "Offline editor", "person" },
                    { -9, "8 Birch Lane, Newcastle, NE4 5FG", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "percival.ts@televisingtonsmythe.net", false, null, null, "Percival Televisington-Smythe", "07703 456789", null, null, null, "Offline editor", "person" },
                    { -8, "12 Willow Crescent, Birmingham, B3 4EF", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "daphne.showmaker@tvantics.org", false, null, null, "Daphne Showmaker", "07702 345678", null, null, null, "Offline editor", "person" },
                    { -7, "45 Oak Avenue, Manchester, M2 3CD", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "melanie.editswel@postprolol.com", false, null, null, "Melanie Editswel", "07701 234567", null, null, null, "Offline editor", "person" },
                    { -6, "23 Maple Street, Sheffield, S1 2AB", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeremy.cutnice@tvfunmail.com", false, null, null, "Jeremy Cutnice", "07700 123456", null, null, null, "Offline editor", "person" }
                });

            migrationBuilder.InsertData(
                table: "production",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), "Top Gear Special" },
                    { new Guid("4050a623-5308-4640-8c36-493729f6f884"), "Teen Mom UK Series 10" },
                    { new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), "The Great British Bake Off!" },
                    { new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), "The Crown Season 5" }
                });

            migrationBuilder.InsertData(
                table: "booking",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "LastModifiedBy", "LastModifiedDate", "LocationId", "Name", "Notes", "ProductionId", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 6, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(858), null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "High-speed internet required for remote editing.", new Guid("4050a623-5308-4640-8c36-493729f6f884"), new DateTime(2024, 6, 15, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(847), "provisional" },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 20, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(906), null, null, null, "", "Need access to soundproof dubbing studio.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 7, 13, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(898), "provisional" },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 3, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(930), null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Final editing phase.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 5, 29, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(929), "confirmed" },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(964), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Location scouting.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 4, 13, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(952), "confirmed" },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 3, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(990), null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Principal photography.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 3, 13, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(989), "confirmed" },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 18, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(1022), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Pre-production meetings.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 8, 13, 19, 17, 22, 757, DateTimeKind.Utc).AddTicks(1021), "confirmed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_asset_group_ParentGroupId1",
                table: "asset_group",
                column: "ParentGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_asset_group_map_AssetsId",
                table: "asset_group_map",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetBooking_BookingsId",
                table: "AssetBooking",
                column: "BookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_booking_LocationId",
                table: "booking",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_booking_ProductionId",
                table: "booking",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_booking_group_map_BookingsId",
                table: "booking_group_map",
                column: "BookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_person_ProductionId",
                table: "person",
                column: "ProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asset_group_map");

            migrationBuilder.DropTable(
                name: "AssetBooking");

            migrationBuilder.DropTable(
                name: "booking_group_map");

            migrationBuilder.DropTable(
                name: "equipment");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "asset_group");

            migrationBuilder.DropTable(
                name: "booking");

            migrationBuilder.DropTable(
                name: "booking_group");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "production");

            migrationBuilder.DropSequence(
                name: "AssetIds");
        }
    }
}
