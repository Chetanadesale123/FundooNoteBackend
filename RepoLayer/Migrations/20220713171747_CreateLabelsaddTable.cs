using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class CreateLabelsaddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_Userid",
                table: "Labels");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Labels",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "Noteid",
                table: "Labels",
                newName: "noteid");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_Userid",
                table: "Labels",
                newName: "IX_Labels_userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_userid",
                table: "Labels",
                column: "userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labels_Users_userid",
                table: "Labels");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "Labels",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "noteid",
                table: "Labels",
                newName: "Noteid");

            migrationBuilder.RenameIndex(
                name: "IX_Labels_userid",
                table: "Labels",
                newName: "IX_Labels_Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Users_Userid",
                table: "Labels",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
