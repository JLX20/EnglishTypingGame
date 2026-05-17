using System.Collections.Generic;

namespace EnglishTypingGame
{
    public class GrammarLesson
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GrammarRule> Rules { get; set; }

        public GrammarLesson()
        {
            Title = "";
            Description = "";
            Rules = new List<GrammarRule>();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}