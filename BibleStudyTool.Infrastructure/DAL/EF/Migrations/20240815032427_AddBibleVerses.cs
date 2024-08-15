using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibleStudyTool.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddBibleVerses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BibleVerse",
                table: "BibleVerse");

            migrationBuilder.RenameTable(
                name: "BibleVerse",
                newName: "bible_verses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bible_verses",
                table: "bible_verses",
                columns: new[] { "language", "version_abbreviation", "book_id", "chapter_number", "verse_number" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_bible_verses",
                table: "bible_verses");

            migrationBuilder.RenameTable(
                name: "bible_verses",
                newName: "BibleVerse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BibleVerse",
                table: "BibleVerse",
                columns: new[] { "language", "version_abbreviation", "book_id", "chapter_number", "verse_number" });
        }
    }
}
