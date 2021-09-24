using System;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class NoteGetterRepository : EntityGetterRepository, INoteGetterRepository
    {
        public EntityGetterRawSql
            GetAllUserNotesForChapterGenerateSql(string userId,  
                                                 string[] bibleVerseIds)
        {
            throw new NotImplementedException();
        }
    }
}
