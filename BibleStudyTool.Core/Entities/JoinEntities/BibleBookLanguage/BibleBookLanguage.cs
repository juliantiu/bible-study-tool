﻿using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleBookLanguage : BaseEntity
    {
        public int BibleBookId { get; }
        public BibleBook BibleBook { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string Name { get; }
        public string Style { get; } // todo include in entity type configuration

        public BibleBookLanguage()
        {
        }

        public BibleBookLanguage(int bibleBookId, string languageCode, string name, string style)
        {
            BibleBookId = bibleBookId;
            Name = name;
            LanguageCode = languageCode;
            Style = style;
        }

        public BibleBookLanguage(string languageCode, string style)
        {
            LanguageCode = languageCode;
            Style = style;
        }
    }
}
