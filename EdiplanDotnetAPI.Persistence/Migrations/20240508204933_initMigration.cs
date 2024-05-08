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
                    Value = table.Column<decimal>(type: "numeric", nullable: true),
                    AssetNumber = table.Column<string>(type: "text", nullable: true),
                    Make = table.Column<string>(type: "text", nullable: true),
                    Model = table.Column<string>(type: "text", nullable: true),
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
                    Value = table.Column<decimal>(type: "numeric", nullable: true),
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
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
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
                    Value = table.Column<decimal>(type: "numeric", nullable: true),
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
                values: new object[] { 1, "12442", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faulty lense.", null, null, "Sony", "FX-6", "Sony FX6", null, null, "Equipment", 1000m });

            migrationBuilder.InsertData(
                table: "person",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "Email", "IsStaff", "LastModifiedBy", "LastModifiedDate", "Name", "PhoneNumber", "ProductionId", "Rate", "RateUnit", "Role", "Type", "Value" },
                values: new object[] { 2, "5 Nincompoop Close", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeff@goldie.com", false, null, null, "Jeff Goldblum", "1234567890", null, null, null, "Editor", "Person", null });

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
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "IsConfirmed", "LastModifiedBy", "LastModifiedDate", "LocationId", "Name", "Notes", "ProductionId", "StartDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 31, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8292), false, null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "High-speed internet required for remote editing.", new Guid("4050a623-5308-4640-8c36-493729f6f884"), new DateTime(2024, 5, 10, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8284) },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 15, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8315), false, null, null, null, "", "Need access to soundproof dubbing studio.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 6, 8, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8311) },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 28, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8332), true, null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Final editing phase.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 4, 23, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8331) },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 3, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8348), true, null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Location scouting.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 3, 8, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8348) },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 27, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8364), true, null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Principal photography.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 2, 8, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8364) },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 13, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8381), false, null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Pre-production meetings.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 7, 8, 20, 49, 33, 519, DateTimeKind.Utc).AddTicks(8381) }
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
