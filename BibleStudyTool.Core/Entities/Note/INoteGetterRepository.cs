using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public interface INoteGetterRepository
    {
        public EntityGetterRawSql
            GetAllUserNotesForChapterGenerateSql(string userId,
                                                 string[] bibleVerseIds);
    }
}
