using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RussianTasks.src.exercises.variant
{
    public class VariantWordInfo
    {
        public class Variant
        {
            public Variant(string text, bool isCorrect)
            {
                this.text = text;
                this.isCorrect = isCorrect;
            }

            public string text { get; }
            public bool isCorrect { get; }
        }

        public const string KEY_ID = "id";
        public const string KEY_SECTION = "section";
        public const string KEY_QUESTION = "question";
        public const string KEY_ANSWER = "answer";
        public const string KEY_VARIANTS = "variants";
        public const string KEY_CONTENTS = "contents";

        public uint id { get; }
        public uint section { get; }
        public string question { get; }
        public List<Variant> variants { get; }


        public VariantWordInfo(uint id, uint section, string question, List<Variant> variants)
        {
            this.id = id;
            this.section = section;
            this.question = question;
            this.variants = variants;
        }
   

        public static VariantWordInfo createFromDict(JObject dict)
        {
            JArray variantsArray = (JArray)dict[KEY_VARIANTS];
            List<Variant> variants = new List<Variant>(variantsArray.Count);
            int correctVariant = (int)dict[KEY_ANSWER];

            for (int i = 0; i < variantsArray.Count; i++)
            {
                variants.Add(new Variant((string)variantsArray[i], i == correctVariant));
            }

            uint id = (uint)dict[KEY_ID];
            uint section = (uint)dict[KEY_SECTION];
            string question = (string)dict[KEY_QUESTION];

            return new VariantWordInfo(id, section, question, variants);
        }

        public Variant getCorrectVariant()
        {
            foreach (Variant variant in variants) {
                if (variant.isCorrect)
                {
                    return variant;
                }
            }
            return null;
        }
    }
}
