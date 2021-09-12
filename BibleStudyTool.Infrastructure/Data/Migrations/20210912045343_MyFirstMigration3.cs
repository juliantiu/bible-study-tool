using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BibleStudyTool.Infrastructure.Data.Migrations
{
    public partial class MyFirstMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    LanguageName = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Endonym = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NoteUid = table.Column<int>(type: "integer", nullable: false),
                    Summary = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    NoteText = table.Column<string>(type: "text", nullable: true),
                    NoteReferenceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Notes_NoteReferenceId",
                        column: x => x.NoteReferenceId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagGroups",
                columns: table => new
                {
                    TagGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagGroupUid = table.Column<int>(type: "integer", nullable: false),
                    TagGroupLabel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroups", x => x.TagGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagUid = table.Column<int>(type: "integer", nullable: false),
                    TagLabel = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "BibleVersions",
                columns: table => new
                {
                    BibleVersionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BibleVersionName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    BibleVersionAbbreviation = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVersions", x => x.BibleVersionId);
                    table.ForeignKey(
                        name: "FK_BibleVersions_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagGroupNotes",
                columns: table => new
                {
                    TagGroupNoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagGroupId = table.Column<int>(type: "integer", nullable: false),
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroupNotes", x => x.TagGroupNoteId);
                    table.ForeignKey(
                        name: "FK_TagGroupNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagGroupNotes_TagGroups_TagGroupId",
                        column: x => x.TagGroupId,
                        principalTable: "TagGroups",
                        principalColumn: "TagGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagGroupTags",
                columns: table => new
                {
                    TagGroupTagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagGroupId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagGroupTags", x => x.TagGroupTagId);
                    table.ForeignKey(
                        name: "FK_TagGroupTags_TagGroups_TagGroupId",
                        column: x => x.TagGroupId,
                        principalTable: "TagGroups",
                        principalColumn: "TagGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagGroupTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagNotes",
                columns: table => new
                {
                    TagNoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagNotes", x => x.TagNoteId);
                    table.ForeignKey(
                        name: "FK_TagNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagNotes_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BibleBooks",
                columns: table => new
                {
                    BibleBookId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BibleVersionId = table.Column<int>(type: "integer", nullable: false),
                    BibleBookName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleBooks", x => x.BibleBookId);
                    table.ForeignKey(
                        name: "FK_BibleBooks_BibleVersions_BibleVersionId",
                        column: x => x.BibleVersionId,
                        principalTable: "BibleVersions",
                        principalColumn: "BibleVersionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BibleBookAbbreviations",
                columns: table => new
                {
                    BibleBookAbbreviationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BibleBookId = table.Column<int>(type: "integer", nullable: false),
                    BibleBookAbbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleBookAbbreviations", x => x.BibleBookAbbreviationId);
                    table.ForeignKey(
                        name: "FK_BibleBookAbbreviations_BibleBooks_BibleBookId",
                        column: x => x.BibleBookId,
                        principalTable: "BibleBooks",
                        principalColumn: "BibleBookId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BibleVerses",
                columns: table => new
                {
                    BibleVerseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BibleBookId = table.Column<int>(type: "integer", nullable: false),
                    ChapterNumber = table.Column<int>(type: "integer", nullable: false),
                    VerseNumber = table.Column<int>(type: "integer", nullable: false),
                    BibleVerseText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVerses", x => x.BibleVerseId);
                    table.ForeignKey(
                        name: "FK_BibleVerses_BibleBooks_BibleBookId",
                        column: x => x.BibleBookId,
                        principalTable: "BibleBooks",
                        principalColumn: "BibleBookId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BibleVerseNotes",
                columns: table => new
                {
                    BibleVerseNoteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BibleVerseId = table.Column<int>(type: "integer", nullable: false),
                    NoteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibleVerseNotes", x => x.BibleVerseNoteId);
                    table.ForeignKey(
                        name: "FK_BibleVerseNotes_BibleVerses_BibleVerseId",
                        column: x => x.BibleVerseId,
                        principalTable: "BibleVerses",
                        principalColumn: "BibleVerseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BibleVerseNotes_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BibleBookAbbreviations_BibleBookId",
                table: "BibleBookAbbreviations",
                column: "BibleBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BibleBooks_BibleVersionId",
                table: "BibleBooks",
                column: "BibleVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_BibleVerseNotes_BibleVerseId",
                table: "BibleVerseNotes",
                column: "BibleVerseId");

            migrationBuilder.CreateIndex(
                name: "IX_BibleVerseNotes_NoteId",
                table: "BibleVerseNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_BibleVerses_BibleBookId",
                table: "BibleVerses",
                column: "BibleBookId");

            migrationBuilder.CreateIndex(
                name: "IX_BibleVersions_LanguageId",
                table: "BibleVersions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteReferenceId",
                table: "Notes",
                column: "NoteReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupNotes_NoteId",
                table: "TagGroupNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupNotes_TagGroupId",
                table: "TagGroupNotes",
                column: "TagGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupTags_TagGroupId",
                table: "TagGroupTags",
                column: "TagGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TagGroupTags_TagId",
                table: "TagGroupTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagNotes_NoteId",
                table: "TagNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TagNotes_TagId",
                table: "TagNotes",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BibleBookAbbreviations");

            migrationBuilder.DropTable(
                name: "BibleVerseNotes");

            migrationBuilder.DropTable(
                name: "TagGroupNotes");

            migrationBuilder.DropTable(
                name: "TagGroupTags");

            migrationBuilder.DropTable(
                name: "TagNotes");

            migrationBuilder.DropTable(
                name: "BibleVerses");

            migrationBuilder.DropTable(
                name: "TagGroups");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "BibleBooks");

            migrationBuilder.DropTable(
                name: "BibleVersions");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
