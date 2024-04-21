using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EdiplanDotnetAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RateUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStaff = table.Column<bool>(type: "bit", nullable: true),
                    ProductionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Asset_Productions_ProductionId",
                        column: x => x.ProductionId,
                        principalTable: "Productions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingBookingGroup",
                columns: table => new
                {
                    BookingGroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingBookingGroup", x => new { x.BookingGroupsId, x.BookingsId });
                    table.ForeignKey(
                        name: "FK_BookingBookingGroup_BookingGroups_BookingGroupsId",
                        column: x => x.BookingGroupsId,
                        principalTable: "BookingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingBookingGroup_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetAssetGroup",
                columns: table => new
                {
                    AssetGroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAssetGroup", x => new { x.AssetGroupsId, x.AssetsId });
                    table.ForeignKey(
                        name: "FK_AssetAssetGroup_AssetGroup_AssetGroupsId",
                        column: x => x.AssetGroupsId,
                        principalTable: "AssetGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetAssetGroup_Asset_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookingGroups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("146023a2-4255-4cfd-893a-d04b0839e616"), "Dub - In House" },
                    { new Guid("1cd16d2e-a35b-48ef-ab93-debd26d445f0"), "Grade" },
                    { new Guid("4658345f-4454-4128-a2f8-c673e73fa846"), "Online" },
                    { new Guid("b5b7a148-0452-4e0e-8298-7c37fd4caa64"), "Offline" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "IsConfirmed", "LastModifiedBy", "LastModifiedDate", "LocationId", "Name", "Notes", "ProductionId", "StartDate" },
                values: new object[] { new Guid("ac2e516a-90d9-4a96-9004-1f39863c9a51"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 28, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9744), false, null, null, null, "", "Need access to soundproof dubbing studio.", new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), new DateTime(2024, 5, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9737) });

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
                table: "Productions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), "Top Gear Special" },
                    { new Guid("4050a623-5308-4640-8c36-493729f6f884"), "Teen Mom UK Series 10" },
                    { new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), "The Great British Bake Off!" },
                    { new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), "The Crown Season 5" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EndDate", "IsConfirmed", "LastModifiedBy", "LastModifiedDate", "LocationId", "Name", "Notes", "ProductionId", "StartDate" },
                values: new object[,]
                {
                    { new Guid("0c69b80a-c5e5-416b-8a68-cb81dc5d9ba6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 11, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9849), true, null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Final editing phase.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 4, 6, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9846) },
                    { new Guid("5a5458ef-8cf3-4c91-89c7-d7a13d93d2a3"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 16, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9883), true, null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Location scouting.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 2, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9880) },
                    { new Guid("b2f4ed23-5099-495e-9310-87338caaae77"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 26, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9970), false, null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Pre-production meetings.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 6, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9967) },
                    { new Guid("c1d42095-7bf9-4f29-9f35-c47065efe1ff"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 14, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9696), false, null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "High-speed internet required for remote editing.", new Guid("4050a623-5308-4640-8c36-493729f6f884"), new DateTime(2024, 4, 23, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9623) },
                    { new Guid("e005a01c-114c-4b8e-a654-68635714d9a6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 11, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9916), true, null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Principal photography.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 1, 21, 16, 11, 2, 179, DateTimeKind.Local).AddTicks(9913) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset_BookingId",
                table: "Asset",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ProductionId",
                table: "Asset",
                column: "ProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetAssetGroup_AssetsId",
                table: "AssetAssetGroup",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingBookingGroup_BookingsId",
                table: "BookingBookingGroup",
                column: "BookingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ProductionId",
                table: "Bookings",
                column: "ProductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAssetGroup");

            migrationBuilder.DropTable(
                name: "BookingBookingGroup");

            migrationBuilder.DropTable(
                name: "AssetGroup");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "BookingGroups");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Productions");
        }
    }
}
