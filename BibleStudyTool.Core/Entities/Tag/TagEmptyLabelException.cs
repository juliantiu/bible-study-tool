using System;
namespace BibleStudyTool.Core.Entities.Exceptions
{
    public class TagEmptyLabelException : Exception
    {
        public TagEmptyLabelException()
        {
        }

        public TagEmptyLabelException(string message)
            : base(message)
        {
        }

        public TagEmptyLabelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
