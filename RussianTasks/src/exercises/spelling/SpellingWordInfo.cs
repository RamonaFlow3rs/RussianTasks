using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Text;

namespace RussianTasks.src.exercises.spelling
{
    public class SpellingWordPart
    {
        public string letters { get; }
        public bool isHidden { get; }

        public SpellingWordPart(string letters, bool isHidden)
        {
            this.letters = letters;
            this.isHidden = isHidden;
        }
    }

    public class SpellingWordInfo
    {
        public const string KEY_ID = "id";
        public const string KEY_PARTS = "parts";
        public const string KEY_PART = "part";
        public const string KEY_SECTION = "section";
        public const string KEY_HIDDEN = "hidden";
        public const string KEY_CONTENTS = "contents";


        public uint id { get; }
        public List<SpellingWordPart> wordParts { get; }
        public uint section { get; }
        public string wholeWord { get; }

        public SpellingWordInfo(uint id, uint section, List<SpellingWordPart> wordParts)
        {
            this.id = id;
            this.section = section;
            this.wordParts = wordParts;
            StringBuilder builder = new StringBuilder(30);
            foreach (SpellingWordPart part in wordParts)
            {
                builder.Append(part.letters);
            }
            wholeWord = builder.ToString();
        }

        public static SpellingWordInfo createFromDict(JObject dict)
        {
            uint id = (uint)dict[KEY_ID];
            uint section = (uint)dict[KEY_SECTION];
            JArray partsArray = (JArray)dict[KEY_PARTS];
            List<SpellingWordPart> wordParts = new List<SpellingWordPart>(partsArray.Count);
            foreach (JObject obj in partsArray)
            {
                string wordPart = (string)obj[KEY_PART];
                JToken isHidden_token = obj[KEY_HIDDEN];
                bool isHidden = isHidden_token != null && (bool)isHidden_token == true;
                wordParts.Add(new SpellingWordPart(wordPart, isHidden));
            }

            return new SpellingWordInfo(id, section, wordParts);
        }
    }
}
