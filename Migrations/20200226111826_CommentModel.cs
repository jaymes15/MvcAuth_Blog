using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcAuthNBlog.Migrations
{
    public partial class CommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentAuthorID = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CommentPost = table.Column<string>(type: "varchar(500)", nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    BlogCommentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comment_Blog_BlogCommentID",
                        column: x => x.BlogCommentID,
                        principalTable: "Blog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BlogCommentID",
                table: "Comment",
                column: "BlogCommentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
