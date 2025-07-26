using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingGuru.Modules.Mocks.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mocks");

            migrationBuilder.CreateTable(
                name: "FirstEntity",
                schema: "mocks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    field1Nullable = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    field2Utc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_FirstEntity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessage",
                schema: "mocks",
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
                schema: "mocks",
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
                schema: "mocks",
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
                schema: "mocks",
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
                name: "PublishClone",
                schema: "mocks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    publishDateUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_PublishClone", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "mocks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SecondEntity",
                schema: "mocks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    field1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    creationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    creatorUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    lastModificationTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    lastModifierUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    deleterUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    deletionTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_SecondEntity", x => x.id);
                    table.ForeignKey(
                        name: "fK_SecondEntity_User_creatorUserId",
                        column: x => x.creatorUserId,
                        principalSchema: "mocks",
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fK_SecondEntity_User_deleterUserId",
                        column: x => x.deleterUserId,
                        principalSchema: "mocks",
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fK_SecondEntity_User_lastModifierUserId",
                        column: x => x.lastModifierUserId,
                        principalSchema: "mocks",
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "iX_SecondEntity_creatorUserId",
                schema: "mocks",
                table: "SecondEntity",
                column: "creatorUserId");

            migrationBuilder.CreateIndex(
                name: "iX_SecondEntity_deleterUserId",
                schema: "mocks",
                table: "SecondEntity",
                column: "deleterUserId");

            migrationBuilder.CreateIndex(
                name: "iX_SecondEntity_lastModifierUserId",
                schema: "mocks",
                table: "SecondEntity",
                column: "lastModifierUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstEntity",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "InboxMessage",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "InboxMessageConsumer",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "OutboxMessage",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "OutboxMessageConsumer",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "PublishClone",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "SecondEntity",
                schema: "mocks");

            migrationBuilder.DropTable(
                name: "User",
                schema: "mocks");
        }
    }
}
