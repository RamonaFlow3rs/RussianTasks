using Newtonsoft.Json.Linq;

namespace RussianTasks.src.exercises.repeating
{
    public class RepeatingWordInfo
    {
        public const string KEY_ID = "id";
        public const string KEY_SECTION = "section";
        public const string KEY_WORD = "word";
        public const string KEY_ANSWER = "answer";
        public const string KEY_CONTENTS = "contents";

        public uint id { get; }
        public uint section { get; }
        public string word { get; }
        public string answer { get; }

        public RepeatingWordInfo(uint id, uint section, string word, string answer)
        {
            this.id = id;
            this.section = section;
            this.word = word;
            this.answer = answer;
        }

        public static RepeatingWordInfo createFromDict(JObject dict)
        {
            uint id = (uint)dict[KEY_ID];
            uint section = (uint)dict[KEY_SECTION];
            string word = (string)dict[KEY_WORD];
            string answer = (string)dict[KEY_ANSWER];

            return new RepeatingWordInfo(id, section, word, answer);
        }
    }
}
