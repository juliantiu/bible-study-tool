﻿using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleBookAbbreviationLanguage : BaseEntity
    {
        public int BibleBookId { get; }
        public BibleBook BibleBook { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string Abbreviation { get; }
        public string Style { get; } // todo include in entity type configuration

        public BibleBookAbbreviationLanguage()
        {
        }

        public BibleBookAbbreviationLanguage(int bibleBookId, string languageCode, string abbreviation, string style)
        {
            BibleBookId = bibleBookId;
            LanguageCode = languageCode;
            Abbreviation = abbreviation;
            Style = style;
        }

        public BibleBookAbbreviationLanguage(string languageCode, string style)
        {
            LanguageCode = languageCode;
            Style = style;
        }
    }
}
