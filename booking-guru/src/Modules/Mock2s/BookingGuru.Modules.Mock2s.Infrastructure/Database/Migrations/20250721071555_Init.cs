using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingGuru.Modules.Mock2s.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mock2s");

            migrationBuilder.CreateTable(
                name: "InboxMessage",
                schema: "mock2s",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    occurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    processedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_InboxMessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessageConsumer",
                schema: "mock2s",
                columns: table => new
                {
                    inboxMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_InboxMessageConsumer", x => new { x.inboxMessageId, x.name });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                schema: "mock2s",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    occurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    processedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_OutboxMessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessageConsumer",
                schema: "mock2s",
                columns: table => new
                {
                    outboxMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_OutboxMessageConsumer", x => new { x.outboxMessageId, x.name });
                });

            migrationBuilder.CreateTable(
                name: "Publish",
                schema: "mock2s",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    publishDateUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_Publish", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboxMessage",
                schema: "mock2s");

            migrationBuilder.DropTable(
                name: "InboxMessageConsumer",
                schema: "mock2s");

            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "mock2s");

            migrationBuilder.DropTable(
                name: "OutboxMessageConsumer",
                schema: "mock2s");

            migrationBuilder.DropTable(
                name: "Publish",
                schema: "mock2s");
        }
    }
}
