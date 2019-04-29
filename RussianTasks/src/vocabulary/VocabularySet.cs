using System.Collections.Generic;

namespace RussianTasks.src.vocabulary
{
    public class VocabularySet
    {
        public uint section { get; }
        public string title { get; }
        public List<VocabularyItem> words { get; }

        public VocabularySet(uint section, string title, List<VocabularyItem> words)
        {
            this.section = section;
            this.title = title;
            this.words = words;
        }
    }
}
