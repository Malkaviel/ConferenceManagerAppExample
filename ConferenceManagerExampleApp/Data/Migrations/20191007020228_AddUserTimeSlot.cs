using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConferenceManagerExampleApp.Data.Migrations
{
    public partial class AddUserTimeSlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTimeSlot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdentityUserId = table.Column<string>(nullable: false),
                    TimeSlotModelStartTime = table.Column<DateTime>(nullable: false),
                    TimeSlotModelEndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTimeSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTimeSlot_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTimeSlot_TimeSlot_TimeSlotModelStartTime_TimeSlotModelEndTime",
                        columns: x => new { x.TimeSlotModelStartTime, x.TimeSlotModelEndTime },
                        principalTable: "TimeSlot",
                        principalColumns: new[] { "StartTime", "EndTime" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeSlot_IdentityUserId",
                table: "UserTimeSlot",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTimeSlot_TimeSlotModelStartTime_TimeSlotModelEndTime",
                table: "UserTimeSlot",
                columns: new[] { "TimeSlotModelStartTime", "TimeSlotModelEndTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTimeSlot");
        }
    }
}
