using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcAuthNBlog.Migrations
{
    public partial class AuthorIdtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Author_AuthorID1",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_AuthorID1",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "AuthorID1",
                table: "Blog");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Blog",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_AuthorID",
                table: "Blog",
                column: "AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Author_AuthorID",
                table: "Blog",
                column: "AuthorID",
                principalTable: "Author",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Author_AuthorID",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_AuthorID",
                table: "Blog");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorID",
                table: "Blog",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AuthorID1",
                table: "Blog",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_AuthorID1",
                table: "Blog",
                column: "AuthorID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Author_AuthorID1",
                table: "Blog",
                column: "AuthorID1",
                principalTable: "Author",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
