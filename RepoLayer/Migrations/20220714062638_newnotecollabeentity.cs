using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class newnotecollabeentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollabeTable_NotesTable_notesnoteid",
                table: "CollabeTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_NotesTable_notesnoteid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_notesnoteid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_CollabeTable_notesnoteid",
                table: "CollabeTable");

            migrationBuilder.DropColumn(
                name: "notesnoteid",
                table: "Labels");

            migrationBuilder.DropColumn(
                name: "notesnoteid",
                table: "CollabeTable");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_noteid",
                table: "Labels",
                column: "noteid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_NotesTable_noteid",
                table: "Labels",
                column: "noteid",
                principalTable: "NotesTable",
                principalColumn: "noteid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollabeTable_NotesTable_noteid",
                table: "CollabeTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Labels_NotesTable_noteid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_Labels_noteid",
                table: "Labels");

            migrationBuilder.DropIndex(
                name: "IX_CollabeTable_noteid",
                table: "CollabeTable");

            migrationBuilder.AddColumn<long>(
                name: "notesnoteid",
                table: "Labels",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "notesnoteid",
                table: "CollabeTable",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Labels_notesnoteid",
                table: "Labels",
                column: "notesnoteid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_NotesTable_notesnoteid",
                table: "Labels",
                column: "notesnoteid",
                principalTable: "NotesTable",
                principalColumn: "noteid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
