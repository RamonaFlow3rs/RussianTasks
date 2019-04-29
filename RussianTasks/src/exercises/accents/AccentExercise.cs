using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RussianTasks.src.ui.accents;
using System.Windows.Forms;
using RussianTasks.src.utils;
using RussianTasks.src.services;
using RussianTasks.src.ui;
using RussianTasks.src.vocabulary;
using RussianTasks.src.common;

namespace RussianTasks.src.exercises.accents
{
    public class AccentExercise : IExercise
    {
        public const string sName = "accents";
        public const string ACCENTS_SECTION_NAME_TEMPLATE = "accents_section_name_{0}";

        public const string KEY_CONTENTS = "contents";

        private AccentExerciseWindow _window;
        private Queue<AccentWordInfo> _accentWords = new Queue<AccentWordInfo>();
        private Queue<AccentWordInfo> _mistakes = new Queue<AccentWordInfo>();
        private HashSet<AccentWordInfo> _vocabulary = new HashSet<AccentWordInfo>();
        private SortedDictionary<uint, string> _sectionsList = new SortedDictionary<uint, string>();
        private List<AccentWordInfo> _staticInfo;
        private AccentWordInfo _currentWord;
        private int _completedWordsCount;
        private int _skippedWordsCount;
        private uint _currentSection;


        public string getName()
        {
            return sName;
        }


        public AccentExercise()
        {
            reset();
            _staticInfo = prepareMaterial();
        }


        public void load(JObject dict)
        {
            JArray vocabularyDict = (JArray)dict[Constants.KEY_SAVE_VOCABULARY];
            if (vocabularyDict != null)
            {
                foreach (JValue value in vocabularyDict)
                {
                    uint wordId = (uint)value;
                    foreach (AccentWordInfo wordInfo in _staticInfo)
                    {
                        if (wordInfo.id == wordId)
                        {
                            _vocabulary.Add(wordInfo);
                            break;
                        }
                    }
                }
            }
        }


        public JObject save()
        {
            JObject dict = new JObject();

            JArray vocabularyDict = new JArray();
            foreach (AccentWordInfo wordInfo in _vocabulary)
            {
                vocabularyDict.Add(wordInfo.id);
            }
            dict.Add(Constants.KEY_SAVE_VOCABULARY, vocabularyDict);

            return dict;
        }


        public void showWindowAndBeginPractice()
        {
            _window = new AccentExerciseWindow(this, _currentSection, _accentWords.Count);
            _window.Show();
            showNextWord();
        }


        public void startExercise()
        {
            showSectionsWindow();
        }

        public bool onLetterSelected(int idx)
        {

            bool correct = false;

            if (_currentWord != null)
            {
                correct = _currentWord.accentPosition == (idx + 1);
                if (!correct && !_mistakes.Contains(_currentWord))
                {
                    _mistakes.Enqueue(_currentWord);
                }
                if (correct) _completedWordsCount++;
            }
            return correct;
        }


        public void showNextWord()
        {
            if (_accentWords.Count > 0)
            {
                _currentWord = _accentWords.Dequeue();
                bool haveWordInVocabulary = _vocabulary.Contains(_currentWord);
                _window.showWord(_currentWord, haveWordInVocabulary);
            }
        }


        public void skipCurrentWord()
        {
            _skippedWordsCount++;
            showNextWord();
        }


        public void showExerciseResults()
        {
            _currentWord = null;
            new ui.ExerciseResultWindow(getLocalizedNameForSection(_currentSection), _completedWordsCount, _mistakes.Count, _skippedWordsCount,
             () =>
             {
                 finishExercise();
             },
             () =>
             {
                 startMistakesCorrection();
             }).Show();
        }


        public void startMistakesCorrection()
        {
            reset();
            _accentWords = new Queue<AccentWordInfo>(_mistakes);
            _mistakes.Clear();
            showWindowAndBeginPractice();
        }

        private void showSectionsWindow()
        {
            Action<Form> showVocabularyWindow = (Form parentWindow) =>
            {
                new VocabularyWindow(VocabularyRequests.createVocabularyContentForAccentsExercise(_vocabulary),
                                     parentWindow,
                                     removeWordFromVocabulary,
                                     beginStudyVocabulary).Show();
            };

            new SelectSectionWindow(_sectionsList,
                                    onSectionSelected,
                                    Properties.Strings.exercise_name_accents,
                                    Properties.Strings.select_section_window_title_accents,
                                    showVocabularyWindow).Show();
        }

        private void onSectionSelected(uint section)
        {
            _currentSection = section;

            if (section == Constants.EXERCISE_SECTION_EVERYTHING)
            {
                var shuffledList = new List<AccentWordInfo>(_staticInfo);
                shuffledList.Shuffle();
                _accentWords = new Queue<AccentWordInfo>(shuffledList);
            }
            else
            {
                foreach (AccentWordInfo wordInfo in _staticInfo)
                {
                    if (wordInfo.section == section) { _accentWords.Enqueue(wordInfo); }
                }
            }

            if (_accentWords.Count > 0)
            {
                showWindowAndBeginPractice();
            }
            else
            {
                MessageBox.Show(Properties.Strings.error_text);
                finishExercise();
            }
        }


        public void finishExercise()
        {
            reset(true);
            showSectionsWindow();
        }

        private void reset(bool fullReset = false)
        {
            _window = null;
            _currentWord = null;
            _completedWordsCount = 0;
            _skippedWordsCount = 0;
            if (fullReset)
            {
                _currentSection = 0;
                _accentWords.Clear();
                _mistakes.Clear();
            }
        }

        public void addWordToVocabulary(AccentWordInfo info)
        {
            _vocabulary.Add(info);
        }


        public void addCurrentWordToVocabulary()
        {
            if (_currentWord != null)
            {
                addWordToVocabulary(_currentWord);
            }
        }


        public bool removeWordFromVocabulary(uint wordId)
        {
            foreach (AccentWordInfo info in _vocabulary)
            {
                if (info.id == wordId)
                {
                    _vocabulary.Remove(info);
                    return true;
                }
            }
            return false;
        }


        public void removeCurrentWordFromVocabulary()
        {
            if (_currentWord != null)
            {
                removeWordFromVocabulary(_currentWord.id);
            }
        }


        public bool isWordInVocabulary(AccentWordInfo info)
        {
            return _vocabulary.Contains(info);
        }


        public List<AccentWordInfo> prepareMaterial()
        {
            List<AccentWordInfo> ret = new List<AccentWordInfo>();

            JObject config = SharedLocator.getStaticInfo().getAccentsExerciseConfig();
            JArray wordsArray = (JArray)config[KEY_CONTENTS];

            ret.Capacity = wordsArray.Count;
            foreach (JObject obj in wordsArray)
            {
                AccentWordInfo wordInfo = new AccentWordInfo(obj);
                ret.Add(wordInfo);
                if (!_sectionsList.ContainsKey(wordInfo.section))
                {
                    _sectionsList.Add(wordInfo.section, Properties.Strings.ResourceManager.GetString(string.Format(ACCENTS_SECTION_NAME_TEMPLATE, wordInfo.section)));
                }
            }
            _sectionsList.Add(Constants.EXERCISE_SECTION_EVERYTHING, Properties.Strings.section_name_shuffle);

            return ret;
        }

        public void beginStudyVocabulary()
        {
            if (_vocabulary.Count > 0)
            {
                _currentSection = common.Constants.EXERCISE_SECTION_VOCABULARY;
                List<AccentWordInfo> listToShuffle = new List<AccentWordInfo>(_vocabulary);
                listToShuffle.Shuffle();
                _accentWords = new Queue<AccentWordInfo>(listToShuffle);
                showWindowAndBeginPractice();
            }
            else
            {
                MessageBox.Show(Properties.Strings.error_no_words_in_vocabulary, Properties.Strings.error_text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public string getLocalizedNameForSection(uint section)
        {
            if (section == Constants.EXERCISE_SECTION_VOCABULARY)
            {
                return Properties.Strings.section_name_vocabulary;
            }
            else
            {
                return _sectionsList[section];
            }
        }
    }
}