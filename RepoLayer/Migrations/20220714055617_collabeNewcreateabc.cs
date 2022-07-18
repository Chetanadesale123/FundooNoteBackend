using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class collabeNewcreateabc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollabeTable_NotesTable_noteid",
                table: "CollabeTable");

            migrationBuilder.DropIndex(
                name: "IX_CollabeTable_noteid",
                table: "CollabeTable");

            migrationBuilder.AddColumn<long>(
                name: "notesnoteid",
                table: "CollabeTable",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollabeTable_notesnoteid",
                table: "CollabeTable",
                column: "notesnoteid");

            migrationBuilder.AddForeignKey(
                name: "FK_CollabeTable_NotesTable_notesnoteid",
                table: "CollabeTable",
                column: "notesnoteid",
                principalTable: "NotesTable",
                principalColumn: "noteid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollabeTable_NotesTable_notesnoteid",
                table: "CollabeTable");

            migrationBuilder.DropIndex(
                name: "IX_CollabeTable_notesnoteid",
                table: "CollabeTable");

            migrationBuilder.DropColumn(
                name: "notesnoteid",
                table: "CollabeTable");

            migrationBuilder.CreateIndex(
                name: "IX_CollabeTable_noteid",
                table: "CollabeTable",
                column: "noteid");

            migrationBuilder.AddForeignKey(
                name: "FK_CollabeTable_NotesTable_noteid",
                table: "CollabeTable",
                column: "noteid",
                principalTable: "NotesTable",
                principalColumn: "noteid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
