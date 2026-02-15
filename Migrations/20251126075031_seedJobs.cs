using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobsApi.Migrations
{
    /// <inheritdoc />
    public partial class seedJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Copies = table.Column<int>(type: "int", nullable: false),
                    Printed = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    IntegratorUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobStatus = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "Copies", "EndTime", "Height", "IntegratorUser", "JobName", "JobStatus", "MachineName", "Printed", "StartTime", "Width" },
                values: new object[,]
                {
                    { 1, 100, new DateTime(2025, 11, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), 70, "alice", "Poster Printing2", 3, "LPT2", 100, new DateTime(2025, 11, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), 50 },
                    { 2, 500, new DateTime(2025, 11, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), 29, "bob", "Flyer Distribution3", 2, "LPT3", 250, new DateTime(2025, 11, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 3, 200, new DateTime(2025, 11, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), 5, "charlie", "Business Cards3", 1, "LPT2", 12, new DateTime(2025, 11, 21, 8, 30, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 4, 150, new DateTime(2025, 11, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), 29, "alice", "Brochure Print3", 1, "LPT3", 70, new DateTime(2025, 11, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 5, 20, new DateTime(2025, 11, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 90, "bob", "Poster Batch4", 3, "LPT1", 20, new DateTime(2025, 11, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), 60 },
                    { 6, 1000, new DateTime(2025, 11, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), 5, "charlie", "Sticker Run4", 1, "LPT1", 700, new DateTime(2025, 11, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
