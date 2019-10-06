using Microsoft.EntityFrameworkCore.Migrations;

namespace ConferenceManagerExampleApp.Data.Migrations
{
    public partial class AddSpeaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    SubjectModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speaker_Subject_SubjectModelId",
                        column: x => x.SubjectModelId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_SubjectModelId",
                table: "Speaker",
                column: "SubjectModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Speaker");
        }
    }
}
