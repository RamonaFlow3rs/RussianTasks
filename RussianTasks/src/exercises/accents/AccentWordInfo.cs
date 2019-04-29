using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RussianTasks.src.exercises.accents
{
    public class Addition
    {
        public const string KEY_ADDITION_TEXT = "text";
        public const string KEY_ADDITION_POSITION = "position";

        public enum Position
        {
            TOP = 1,
            RIGHT = 2,
            BOTTOM = 3,
            LEFT = 4
        };

        public Addition(JObject obj)
        {
            text = (string)obj[KEY_ADDITION_TEXT];
            uint pos = (uint)obj[KEY_ADDITION_POSITION];
            position = (Position)pos;
        }

        public string text;
        public Position position;
    }

    public class AccentWordInfo
    {
        public const string KEY_ID = "id";
        public const string KEY_SECTION = "section";
        public const string KEY_WORD = "word";
        public const string KEY_ACCENT_POSITION = "accentPosition";
        public const string KEY_ADDITIONS = "additions";

        public uint id { get; }
        public string word { get; set; }
        public uint accentPosition { get; set; }
        public uint section { get; }
        public Addition addition { get; }

        public AccentWordInfo(uint id, uint section, string word, uint accentPosition)
        {
            this.id = id;
            this.section = section;
            this.word = word;
            this.accentPosition = accentPosition;
        }

        public AccentWordInfo(JObject dict)
        {
            id = (uint)dict[KEY_ID];
            section = (uint)dict[KEY_SECTION];
            word = (string)dict[KEY_WORD];
            accentPosition = (uint)dict[KEY_ACCENT_POSITION];
            JToken additions = dict[KEY_ADDITIONS];
            if (additions != null)
            {
                JToken additionDict = ((JArray)additions)[0];
                addition = new Addition(additionDict as JObject);
            }
        }

        public AccentWordInfo(AccentWordInfo otherInfo)
        {
            id = otherInfo.id;
            section = otherInfo.section;
            word = otherInfo.word;
            accentPosition = otherInfo.accentPosition;
        }

        public static AccentWordInfo createFromDict(JObject dict)
        {
            uint id = (uint)dict[KEY_ID];
            uint section = (uint)dict[KEY_SECTION];
            string word = (string)dict[KEY_WORD];
            uint accentPosition = (uint)dict[KEY_ACCENT_POSITION];

            return new AccentWordInfo(id, section, word, accentPosition);
        }
    }
}