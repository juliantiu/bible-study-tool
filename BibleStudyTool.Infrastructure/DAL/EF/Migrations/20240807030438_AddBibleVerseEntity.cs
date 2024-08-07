using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibleStudyTool.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddBibleVerseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BibleVerse",
                table: "BibleVerse");

            migrationBuilder.RenameTable(
                name: "BibleVerse",
                newName: "BibleVerses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BibleVerses",
                table: "BibleVerses",
                columns: new[] { "Language", "VersionAbbr", "BookId", "ChapterNumber", "VerseNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BibleVerses",
                table: "BibleVerses");

            migrationBuilder.RenameTable(
                name: "BibleVerses",
                newName: "BibleVerse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BibleVerse",
                table: "BibleVerse",
                columns: new[] { "Language", "VersionAbbr", "BookId", "ChapterNumber", "VerseNumber" });
        }
    }
}
