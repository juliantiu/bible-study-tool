namespace BibleStudyTool.Public.DTOs
{
    public class NoteVerseReferenceDto
    {
        public int Id { get; private set; }
        public int NoteId { get; private set; }
        public string BibleBook { get; private set; }
        public int BookChapter { get; private set; }
        public int ChapterVerseNumber { get; private set; }
    }
}
