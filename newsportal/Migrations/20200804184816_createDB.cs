using Microsoft.EntityFrameworkCore.Migrations;

namespace newsportal.Migrations
{
    public partial class createDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostDate = table.Column<string>(nullable: false),
                    PostTime = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 400, nullable: false),
                    LikeAmount = table.Column<int>(nullable: true),
                    DislikeAmount = table.Column<int>(nullable: true),
                    VisitedAmount = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
