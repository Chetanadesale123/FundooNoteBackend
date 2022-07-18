using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class CreateCollabeTablr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CollabeTable",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabeEmail = table.Column<string>(nullable: true),
                    userid = table.Column<long>(nullable: false),
                    noteid = table.Column<long>(nullable: false),
                    notesnoteid = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabeTable", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_CollabeTable_NotesTable_notesnoteid",
                        column: x => x.notesnoteid,
                        principalTable: "NotesTable",
                        principalColumn: "noteid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollabeTable_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabeTable_notesnoteid",
                table: "CollabeTable",
                column: "notesnoteid");

            migrationBuilder.CreateIndex(
                name: "IX_CollabeTable_userid",
                table: "CollabeTable",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_Users_userid",
                table: "NotesTable",
                column: "userid",
                principalTable: "Users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_Users_userid",
                table: "NotesTable");

            migrationBuilder.DropTable(
                name: "CollabeTable");

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
    }
}
