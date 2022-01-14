using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.DTOs
{
    public class BibleVersionDto
    {
        public int BibleVersionId { get; }
        public string DefaultName { get; set; }
        public string DefaultAbbreviation { get; set; }
        public string LanguageCode { get; }
        public string Name { get; }
        public string Abbreviation { get; }

        public BibleVersionDto
            ((BibleVersion bv, BibleVersionLanguage bvl) version)
        {
            BibleVersionId = version.bv.BibleVersionId;
            DefaultName = version.bv.DefaultName;
            DefaultAbbreviation = version.bv.DefaultAbbreviation;
            LanguageCode = version.bvl.LanguageCode;
            Name = version.bvl.Name;
            Abbreviation = version.bvl.Abbreviation;
        }
    }
}
