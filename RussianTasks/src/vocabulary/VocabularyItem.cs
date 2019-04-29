namespace RussianTasks.src.vocabulary
{
    public class VocabularyItem
    {
        public uint id { get; }
        public uint section { get; }
        public string word { get; }

        public VocabularyItem(uint id, uint section, string word)
        {
            this.id = id;
            this.section = section;
            this.word = word;
        }
    }
}
