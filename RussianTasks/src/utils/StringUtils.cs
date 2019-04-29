using System;
using System.Collections.Generic;

namespace RussianTasks.src.utils
{
    public enum VersionsCompareResult
    {
        NEW_VERSION_IS_BIGGER,
        OLD_VERSION_IS_BIGGER,
        VERSIONS_ARE_EQUAL
    };

    public class StringUtils
    {
        public static string CHAR_ACCENT = System.Text.RegularExpressions.Regex.Unescape("\\u0301");
        public static string CHAR_NEW_LINE = System.Text.RegularExpressions.Regex.Unescape("\\r\\n");

        private static HashSet<char> VOWEL_LETTERS = new HashSet<char> { 'а', 'А', 'о', 'О','и', 'И', 'у', 'У', 'е', 'Е', 'ё', 'Ё', 'э', 'Э', 'ы', 'Ы', 'я', 'Я', 'ю', 'Ю' };

        public static string replaceMootLetters(string word)
        {
            string result = word.Replace('ё', 'е').Replace('Ё', 'Е');

            return result;
        }

        public static bool isVowel(char letter)
        {
            return VOWEL_LETTERS.Contains(letter);
        }

        public static VersionsCompareResult compareApplicationVersions(string currentVersion, string newVersion)
        {
            for (int i = 0; i < currentVersion.Length && i < newVersion.Length; i++)
            {
                int currentDigit = Convert.ToInt32(currentVersion[i]);
                int newDigit = Convert.ToInt32(newVersion[i]);
                if (newDigit > currentDigit)
                {
                    return VersionsCompareResult.NEW_VERSION_IS_BIGGER;
                }
                else if (currentDigit > newDigit)
                {
                    return VersionsCompareResult.OLD_VERSION_IS_BIGGER;
                }
            }
            return VersionsCompareResult.VERSIONS_ARE_EQUAL;
        }

        public static string getStringByKey(string key)
        {
            string result = Properties.Resources.ResourceManager.GetString(key);
            return result.Equals(string.Empty) ? key : result;
        }
    }
}
