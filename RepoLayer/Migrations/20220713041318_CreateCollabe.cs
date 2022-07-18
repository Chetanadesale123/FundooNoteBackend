using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class CreateCollabe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_Users_userid",
                table: "NotesTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesTable",
                table: "NotesTable");

            migrationBuilder.RenameTable(
                name: "NotesTable",
                newName: "NoteEntity");

            migrationBuilder.RenameIndex(
                name: "IX_NotesTable_userid",
                table: "NoteEntity",
                newName: "IX_NoteEntity_userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteEntity",
                table: "NoteEntity",
                column: "noteid");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteEntity_Users_userid",
                table: "NoteEntity",
                column: "userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteEntity_Users_userid",
                table: "NoteEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteEntity",
                table: "NoteEntity");

            migrationBuilder.RenameTable(
                name: "NoteEntity",
                newName: "NotesTable");

            migrationBuilder.RenameIndex(
                name: "IX_NoteEntity_userid",
                table: "NotesTable",
                newName: "IX_NotesTable_userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesTable",
                table: "NotesTable",
                column: "noteid");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_Users_userid",
                table: "NotesTable",
                column: "userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
