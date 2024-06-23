using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EdiplanDotnetAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
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
                    IsLostOrBroken = table.Column<bool>(type: "boolean", nullable: false),
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
                columns: new[] { "Id", "AssetNumber", "CreatedBy", "CreatedDate", "Description", "IsLostOrBroken", "LastModifiedBy", "LastModifiedDate", "Make", "Model", "Name", "Rate", "RateUnit", "Type", "Value" },
                values: new object[,]
                {
                    { -20, "12467", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "External backup drive.", false, null, null, "Seagate", "Expansion 10TB", "Backup Drive 1", null, null, "equipment", 300m },
                    { -19, "12466", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Online machine.", false, null, null, "Hewlett-Packard", "Z8 G4", "Edit07", null, null, "equipment", 2000m },
                    { -18, "12465", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Camera slider for smooth shots.", false, null, null, "Rhino", "RŌV Pro", "Slider 1", null, null, "equipment", 500m },
                    { -17, "12464", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fluid head tripod.", false, null, null, "Manfrotto", "MVH500AH", "Tripod 1", null, null, "equipment", 250m },
                    { -16, "12463", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-inch on-camera monitor.", false, null, null, "Atomos", "Ninja V", "Field Monitor", null, null, "equipment", 400m },
                    { -15, "12462", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "High sensitivity camera.", false, null, null, "Sony", "A7S III", "Sony A7S III", null, null, "equipment", 1800m },
                    { -14, "12461", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine.", false, null, null, "Hewlett-Packard", "Z4 G4", "Edit06", null, null, "equipment", 1000m },
                    { -13, "12460", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Continuous LED light.", false, null, null, "Godox", "SL200W", "Lighting Kit B", null, null, "equipment", 1000m },
                    { -12, "12459", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Camera stabilizer.", false, null, null, "Zhiyun", "Crane 3S", "Gimbal Stabilizer", null, null, "equipment", 700m },
                    { -11, "12458", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shotgun microphone.", false, null, null, "Sennheiser", "MKH 416", "Microphone 1", null, null, "equipment", 300m },
                    { -10, "12457", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Editing workstation.", false, null, null, "Apple", "iMac Pro", "Edit05", null, null, "equipment", 1500m },
                    { -9, "12456", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aerial shots drone.", false, null, null, "DJI", "Phantom 4 Pro", "Drone 1", null, null, "equipment", 2000m },
                    { -8, "12455", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portable audio recorder.", false, null, null, "Zoom", "H6", "Sound Recorder 1", null, null, "equipment", 500m },
                    { -7, "12454", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Color tunable LED soft light.", false, null, null, "Arri", "SKYPANEL S60-C", "Lighting Kit A", null, null, "equipment", 800m },
                    { -6, "12453", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New sensor installed.", false, null, null, "Canon", "C300 Mark III", "Canon C300", null, null, "equipment", 1500m },
                    { -5, "23452", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Online machine", false, null, null, "Hewlett-Packard", "Z8 G4", "Edit04", null, null, "equipment", 2000m },
                    { -4, "13352", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine", false, null, null, "Hewlett-Packard", "Z4 G4", "Edit03", null, null, "equipment", 1000m },
                    { -3, "12432", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine", false, null, null, "Hewlett-Packard", "Z4 G4", "Edit02", null, null, "equipment", 1000m },
                    { -2, "12452", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Offline machine", false, null, null, "Hewlett-Packard", "Z4 G4", "Edit01", null, null, "equipment", 1000m },
                    { -1, "12442", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faulty lense.", false, null, null, "Sony", "FX-6", "Sony FX6", null, null, "equipment", 1000m }
                });

            migrationBuilder.InsertData(
                table: "person",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "Email", "IsStaff", "LastModifiedBy", "LastModifiedDate", "Name", "PhoneNumber", "ProductionId", "Rate", "RateUnit", "Role", "Type" },
                values: new object[,]
                {
                    { -40, "19 Poplar Close, Norwich, NR1 4WX", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "olivia.costume@costumecrafts.co.uk", false, null, null, "Olivia Costume", "07719 012345", null, null, null, "Costume Designer", "person" },
                    { -39, "11 Cedar Grove, Leicester, LE2 7UV", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "nate.propman@propscreative.com", false, null, null, "Nate Propman", "07718 901234", null, null, null, "Props Master", "person" },
                    { -38, "48 Elm Street, York, YO10 3ST", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "mia.soundcheck@soundstudio.org", false, null, null, "Mia Soundcheck", "07717 890123", null, null, null, "Sound Engineer", "person" },
                    { -37, "61 Maple Terrace, Aberdeen, AB10 1QR", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "leo.cameraman@cameraaction.com", false, null, null, "Leo Cameraman", "07716 789012", null, null, null, "Camera Operator", "person" },
                    { -36, "73 Sycamore Lane, Belfast, BT7 8OP", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "karen.lights@lightsup.com", false, null, null, "Karen Lights", "07715 678901", null, null, null, "Lighting Technician", "person" },
                    { -35, "40 Beech Hill, Reading, RG1 4MN", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jack.animator@animationhub.com", false, null, null, "Jack Animator", "07714 567890", null, null, null, "Animator", "person" },
                    { -34, "9 Redwood Close, Brighton, BN1 3KL", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "isla.graphics@designcreations.co.uk", false, null, null, "Isla Graphics", "07713 456789", null, null, null, "Graphic Designer", "person" },
                    { -33, "18 Chestnut Boulevard, Cambridge, CB1 2IJ", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hank.foley@foleyartistry.com", false, null, null, "Hank Foley", "07712 345678", null, null, null, "Foley Artist", "person" },
                    { -32, "25 Alder Lane, Nottingham, NG3 2GH", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.storyteller@writingscripts.com", false, null, null, "Grace Storyteller", "07711 234567", null, null, null, "Screenwriter", "person" },
                    { -31, "33 Fir Road, Cardiff, CF10 1AB", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.musicman@musicscores.co.uk", false, null, null, "Frank Musicman", "07710 123456", null, null, null, "Music Composer", "person" },
                    { -30, "67 Willow Drive, Oxford, OX4 5EF", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eve.cuttingly@filmeditors.com", false, null, null, "Eve Cuttingly", "07709 012345", null, null, null, "Film Editor", "person" },
                    { -29, "21 Spruce Court, Bristol, BS1 3YZ", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dennis.filmor@cinemagic.com", false, null, null, "Dennis Filmor", "07708 901234", null, null, null, "Cinematographer", "person" },
                    { -28, "14 Cedar Avenue, Glasgow, G12 8XY", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "claire.soundmix@soundgenius.org", false, null, null, "Claire Soundmix", "07707 890123", null, null, null, "Sound Mixer", "person" },
                    { -27, "89 Pine Street, Edinburgh, EH8 9AB", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.directson@moviemagic.co.uk", false, null, null, "Bob Directson", "07706 789012", null, null, null, "Director", "person" },
                    { -26, "54 Ash Road, Leeds, LS6 7JK", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.scriptwriter@writescripts.com", false, null, null, "Alice Scriptwriter", "07705 678901", null, null, null, "Scriptwriter", "person" },
                    { -25, "36 Elm Close, Liverpool, L5 6GH", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dave.programly@tvgeekery.co.uk", false, null, null, "Dave Programly", "07704 567890", null, null, null, "Offline editor", "person" },
                    { -24, "8 Birch Lane, Newcastle, NE4 5FG", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "percival.ts@televisingtonsmythe.net", false, null, null, "Percival Televisington-Smythe", "07703 456789", null, null, null, "Offline editor", "person" },
                    { -23, "12 Willow Crescent, Birmingham, B3 4EF", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "daphne.showmaker@tvantics.org", false, null, null, "Daphne Showmaker", "07702 345678", null, null, null, "Offline editor", "person" },
                    { -22, "45 Oak Avenue, Manchester, M2 3CD", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "melanie.editswel@postprolol.com", false, null, null, "Melanie Editswel", "07701 234567", null, null, null, "Offline editor", "person" },
                    { -21, "23 Maple Street, Sheffield, S1 2AB", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jeremy.cutnice@tvfunmail.com", false, null, null, "Jeremy Cutnice", "07700 123456", null, null, null, "Offline editor", "person" }
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
                    { -20, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 13, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(481), null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "Complete sound design.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 5, 8, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(480), "completed" },
                    { -19, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 21, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(466), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "New scene additions.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 5, 18, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(465), "confirmed" },
                    { -18, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 6, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(452), null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Client requested changes.", new Guid("4050a623-5308-4640-8c36-493729f6f884"), new DateTime(2024, 6, 4, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(451), "cancelled" },
                    { -17, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 28, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(437), null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Final quality check.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 4, 23, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(436), "completed" },
                    { -16, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 20, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(423), null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "Title sequence design.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 6, 17, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(422), "pending" },
                    { -15, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 8, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(409), null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Visual effects review.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 5, 3, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(408), "confirmed" },
                    { -14, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 18, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(396), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "ADR sessions completed.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 5, 13, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(395), "completed" },
                    { -13, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 8, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(381), null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Waiting for client feedback.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 6, 7, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(380), "pending" },
                    { -12, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 25, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(363), null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Special effects integration.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 5, 23, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(362), "confirmed" },
                    { -11, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 5, 30, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(350), null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "Initial editing phase.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 5, 28, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(349), "completed" },
                    { -10, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 2, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(336), null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Project on hold.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 5, 31, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(335), "cancelled" },
                    { -9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 11, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(319), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Reshoots scheduled.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 6, 10, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(319), "confirmed" },
                    { -8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 4, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(305), null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Sound mixing completed.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 6, 2, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(304), "completed" },
                    { -7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 17, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(291), null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "Color correction phase.", new Guid("4050a623-5308-4640-8c36-493729f6f884"), new DateTime(2024, 6, 14, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(290), "pending" },
                    { -6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 27, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(277), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Pre-production meetings.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 8, 22, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(276), "confirmed" },
                    { -5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 12, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(259), null, null, new Guid("5e10152d-dd1b-49a2-bc95-79246ee8ca8a"), "", "Principal photography.", new Guid("d7af2c8c-525e-41ad-b379-edad3de1defe"), new DateTime(2024, 3, 22, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(258), "confirmed" },
                    { -4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 17, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(244), null, null, new Guid("189d7685-bdf0-4a39-9750-7720ec6044c9"), "", "Location scouting.", new Guid("3cbedfd3-a8b1-43b2-9ccb-67ec980118a6"), new DateTime(2024, 4, 22, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(243), "confirmed" },
                    { -3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 12, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(230), null, null, new Guid("71e40a55-2430-4a68-8adc-f78a1ef2c8c2"), "", "Final editing phase.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 6, 7, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(229), "confirmed" },
                    { -2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 29, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(216), null, null, null, "", "Need access to soundproof dubbing studio.", new Guid("709bf680-7cc8-406c-bb8d-13ace00d4fe7"), new DateTime(2024, 7, 22, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(210), "provisional" },
                    { -1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 15, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(193), null, null, new Guid("e19d79c7-58d6-4906-ba7a-3507a2e90f09"), "", "High-speed internet required for remote editing.", new Guid("4050a623-5308-4640-8c36-493729f6f884"), new DateTime(2024, 6, 24, 14, 53, 46, 827, DateTimeKind.Utc).AddTicks(184), "provisional" }
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
