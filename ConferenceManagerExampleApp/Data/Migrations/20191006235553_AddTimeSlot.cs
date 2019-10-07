using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConferenceManagerExampleApp.Data.Migrations
{
    public partial class AddTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    RoomModelId = table.Column<int>(nullable: false),
                    SessionModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => new { x.StartTime, x.EndTime });
                    table.ForeignKey(
                        name: "FK_TimeSlot_Room_RoomModelId",
                        column: x => x.RoomModelId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSlot_Session_SessionModelId",
                        column: x => x.SessionModelId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_RoomModelId",
                table: "TimeSlot",
                column: "RoomModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_SessionModelId",
                table: "TimeSlot",
                column: "SessionModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSlot");
        }
    }
}
