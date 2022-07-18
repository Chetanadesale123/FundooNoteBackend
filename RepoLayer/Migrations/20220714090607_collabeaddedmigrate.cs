using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class collabeaddedmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_NotesTable_noteid",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_userid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_noteid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_userid",
                table: "Labels");

            migrationBuilder.AddColumn<long>(
                name: "Note1noteid",
                table: "Labels",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "User1userid",
                table: "Labels",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Note1noteid",
                table: "Labels",
                column: "Note1noteid");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_User1userid",
                table: "Labels",
                column: "User1userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_NotesTable_Note1noteid",
                table: "Labels",
                column: "Note1noteid",
                principalTable: "NotesTable",
                principalColumn: "noteid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_User1userid",
                table: "Labels",
                column: "User1userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_NotesTable_Note1noteid",
                table: "Labels");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_User1userid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_Note1noteid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_User1userid",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "Note1noteid",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "User1userid",
                table: "Labels");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_noteid",
                table: "Labels",
                column: "noteid");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_userid",
                table: "Labels",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_NotesTable_noteid",
                table: "Labels",
                column: "noteid",
                principalTable: "NotesTable",
                principalColumn: "noteid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_userid",
                table: "Labels",
                column: "userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
