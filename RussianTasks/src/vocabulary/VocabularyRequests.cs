using RussianTasks.src.exercises.accents;
using RussianTasks.src.utils;
using System.Collections.Generic;
using RussianTasks.src.exercises.spelling;
using RussianTasks.src.exercises.repeating;
using RussianTasks.src.exercises.variant;

namespace RussianTasks.src.vocabulary
{
    class VocabularyRequests
    {
        public static List<VocabularySet> createVocabularyContentForAccentsExercise(HashSet<AccentWordInfo> accentsVocabulary)
        {
            List<VocabularySet> ret = new List<VocabularySet>();

            foreach (AccentWordInfo info in accentsVocabulary)
            {
                uint wordSection = info.section;
                bool wordAdded = false;
                foreach (VocabularySet set in ret)
                {
                    if (set.section == wordSection)
                    {
                        string wordStr = info.word.Insert((int)info.accentPosition, StringUtils.CHAR_ACCENT);
                        set.words.Add(new VocabularyItem(info.id, wordSection, wordStr));
                        wordAdded = true;
                        break;
                    }
                }

                if (!wordAdded)
                {
                    List<VocabularyItem> words = new List<VocabularyItem>();
                    string wordStr = info.word.Insert((int)info.accentPosition, StringUtils.CHAR_ACCENT);
                    words.Add(new VocabularyItem(info.id, wordSection, wordStr));
                    string title = Properties.Strings.ResourceManager.GetString(string.Format(AccentExercise.ACCENTS_SECTION_NAME_TEMPLATE, wordSection));
                    ret.Add(new VocabularySet(wordSection, title, words));
                }
            }

            ret.Sort((x, y) =>
            {
                return (int)x.section - (int)y.section;
            });

            return ret;
        }

        public static List<VocabularySet> createVocabularyContentForSpellingExercise(HashSet<SpellingWordInfo> spellingVocabulary)
        {
            List<VocabularySet> ret = new List<VocabularySet>();

            foreach (SpellingWordInfo info in spellingVocabulary)
            {
                uint wordSection = info.section;
                bool wordAdded = false;
                foreach (VocabularySet set in ret)
                {
                    if (set.section == wordSection)
                    {
                        set.words.Add(new VocabularyItem(info.id, wordSection, info.wholeWord));
                        wordAdded = true;
                        break;
                    }
                }

                if (!wordAdded)
                {
                    List<VocabularyItem> words = new List<VocabularyItem>();
                    words.Add(new VocabularyItem(info.id, wordSection, info.wholeWord));
                    string title = Properties.Strings.ResourceManager.GetString(string.Format(SpellingExercise.SPELLING_SECTION_NAME_TEMPLATE, wordSection));
                    ret.Add(new VocabularySet(wordSection, title, words));
                }
            }

            ret.Sort((x, y) =>
            {
                return (int)x.section - (int)y.section;
            });

            return ret;
        }

        public static List<VocabularySet> createVocabularyContentForRepeatingExercise(HashSet<RepeatingWordInfo> repeatingVocabulary)
        {
            List<VocabularySet> ret = new List<VocabularySet>();

            foreach (RepeatingWordInfo info in repeatingVocabulary)
            {
                uint wordSection = info.section;
                bool wordAdded = false;
                foreach (VocabularySet set in ret)
                {
                    if (set.section == wordSection)
                    {
                        set.words.Add(new VocabularyItem(info.id, wordSection, info.answer));
                        wordAdded = true;
                        break;
                    }
                }

                if (!wordAdded)
                {
                    List<VocabularyItem> words = new List<VocabularyItem>();
                    words.Add(new VocabularyItem(info.id, wordSection, info.answer));
                    string title = Properties.Strings.ResourceManager.GetString(string.Format(RepeatingExercise.REPEATING_SECTION_NAME_TEMPLATE, wordSection));
                    ret.Add(new VocabularySet(wordSection, title, words));
                }
            }

            ret.Sort((x, y) =>
            {
                return (int)x.section - (int)y.section;
            });

            return ret;
        }

        public static List<VocabularySet> createVocabularyContentForVariantExercise(HashSet<VariantWordInfo> variantVocabulary)
        {
            List<VocabularySet> ret = new List<VocabularySet>();

            foreach (VariantWordInfo info in variantVocabulary)
            {
                uint wordSection = info.section;
                bool wordAdded = false;
                foreach (VocabularySet set in ret)
                {
                    if (set.section == wordSection)
                    {
                        set.words.Add(new VocabularyItem(info.id, wordSection, info.getCorrectVariant().text));
                        wordAdded = true;
                        break;
                    }
                }

                if (!wordAdded)
                {
                    List<VocabularyItem> words = new List<VocabularyItem>();
                    words.Add(new VocabularyItem(info.id, wordSection, info.getCorrectVariant().text));
                    string title = Properties.Strings.ResourceManager.GetString(string.Format(VariantExercise.VARIANT_SECTION_NAME_TEMPLATE, wordSection));
                    ret.Add(new VocabularySet(wordSection, title, words));
                }
            }

            ret.Sort((x, y) =>
            {
                return (int)x.section - (int)y.section;
            });

            return ret;
        }

    }
}
