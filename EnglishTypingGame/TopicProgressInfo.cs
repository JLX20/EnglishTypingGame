namespace EnglishTypingGame
{
    public class TopicProgressInfo
    {
        public string TopicName { get; set; }
        public int TotalWords { get; set; }
        public int LearnedWords { get; set; }
        public int MistakeWords { get; set; }
        public double LearnedPercent { get; set; }
        public int Stars { get; set; }

        public string StarsText
        {
            get
            {
                if (Stars <= 0)
                    return "☆☆☆";

                if (Stars == 1)
                    return "★☆☆";

                if (Stars == 2)
                    return "★★☆";

                return "★★★";
            }
        }
    }
}