using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using RussianTasks.src.ui;
using RussianTasks.src.common;
using RussianTasks.src.services;
using RussianTasks.src.vocabulary;
using RussianTasks.src.utils;
using RussianTasks.src.ui.repeating;

namespace RussianTasks.src.exercises.repeating
{
    public class RepeatingExercise : IExercise
    {
        public const string sName = "repeating";
        public const string REPEATING_SECTION_NAME_TEMPLATE = "repeating_section_name_{0}";

        List<RepeatingWordInfo> _staticInfo;
        private Queue<RepeatingWordInfo> _workingQueue = new Queue<RepeatingWordInfo>();
        private Queue<RepeatingWordInfo> _mistakesQueue = new Queue<RepeatingWordInfo>();
        private HashSet<RepeatingWordInfo> _vocabulary = new HashSet<RepeatingWordInfo>();
        private SortedDictionary<uint, string> _sectionsList = new SortedDictionary<uint, string>();
        private RepeatingWordInfo _currentWord;
        private int _completedWordsCount;
        private int _skippedWordsCount;
        private uint _currentSection;
        private RepeatingExerciseWindow _window;

        public RepeatingExercise()
        {
            reset();
            _staticInfo = prepareMaterial();
        }

        public string getName()
        {
            return sName;
        }

        public void startExercise()
        {
            showSectionsWindow();
        }

        public void load(JObject dict)
        {
            JArray vocabularyDict = (JArray)dict[Constants.KEY_SAVE_VOCABULARY];
            if (vocabularyDict != null)
            {
                foreach (JValue value in vocabularyDict)
                {
                    uint wordId = (uint)value;
                    foreach (RepeatingWordInfo wordInfo in _staticInfo)
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
            foreach (RepeatingWordInfo wordInfo in _vocabulary)
            {
                vocabularyDict.Add(wordInfo.id);
            }
            dict.Add(Constants.KEY_SAVE_VOCABULARY, vocabularyDict);

            return dict;
        }

        public void finishExercise()
        {
            reset(true);
            showSectionsWindow();
        }

        public void showNextWord()
        {
            if (_workingQueue.Count > 0)
            {
                _currentWord = _workingQueue.Dequeue();
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
            new ExerciseResultWindow(getLocalizedNameForSection(_currentSection), _completedWordsCount, _mistakesQueue.Count, _skippedWordsCount,
             () =>
             {
                 finishExercise();
             },
             () =>
             {
                 startMistakesCorrection();
             }).Show();
        }

        public void onAnswerGiven(bool hasError)
        {
            if (!hasError)
            {
                _completedWordsCount++;
            }
            else if (!_mistakesQueue.Contains(_currentWord))
            {
                _mistakesQueue.Enqueue(_currentWord);
            }
        }

        public void addWordToVocabulary(RepeatingWordInfo info)
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
            foreach (RepeatingWordInfo info in _vocabulary)
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

        private void startMistakesCorrection()
        {
            reset();
            _workingQueue = new Queue<RepeatingWordInfo>(_mistakesQueue);
            _mistakesQueue.Clear();
            showExerciseWindowAndbeginPractice();
        }

        private void reset(bool fullReset = false)
        {
            _completedWordsCount = 0;
            _skippedWordsCount = 0;
            if (fullReset)
            {
                _workingQueue.Clear();
                _mistakesQueue.Clear();
                _currentSection = 0;
            }
        }

        private void showExerciseWindowAndbeginPractice()
        {
            string windowTitle = Properties.Strings.exercise_name_repeating;
            string sectionTitle = getLocalizedNameForSection(_currentSection);
            _window = new RepeatingExerciseWindow(this, windowTitle, sectionTitle, _workingQueue.Count);
            _window.Show();
            showNextWord();
        }

        private List<RepeatingWordInfo> prepareMaterial()
        {
            JObject config = SharedLocator.getStaticInfo().getRepeatingExerciseConfig();
            JArray wordsArray = (JArray)config[RepeatingWordInfo.KEY_CONTENTS];

            List<RepeatingWordInfo> ret = new List<RepeatingWordInfo>(wordsArray.Count);
            foreach (JObject obj in wordsArray)
            {
                RepeatingWordInfo info = RepeatingWordInfo.createFromDict(obj);
                if (!_sectionsList.ContainsKey(info.section))
                {
                    _sectionsList.Add(info.section, Properties.Strings.ResourceManager.GetString(string.Format(REPEATING_SECTION_NAME_TEMPLATE, info.section)));
                }
                ret.Add(info);
            }

            return ret;
        }

        void showSectionsWindow()
        {
            Action<Form> showVocabularyWindow = (Form parentWindow) =>
            {
                new VocabularyWindow(VocabularyRequests.createVocabularyContentForRepeatingExercise(_vocabulary),
                                     parentWindow,
                                     removeWordFromVocabulary,
                                     beginStudyVocabulary).Show();
            };

            new SelectSectionWindow(_sectionsList,
                                    onSectionSelected,
                                    Properties.Strings.exercise_name_repeating,
                                    null,
                                    showVocabularyWindow).Show();
        }

        private void onSectionSelected(uint section)
        {
            LinkedList<RepeatingWordInfo> workingList = new LinkedList<RepeatingWordInfo>();
            foreach (RepeatingWordInfo info in _staticInfo)
            {
                if (info.section == section) workingList.randomAppend(info);
            }

            if (workingList.Count > 0)
            {
                _currentSection = section;
                _workingQueue = new Queue<RepeatingWordInfo>(workingList);
                showExerciseWindowAndbeginPractice();
            }
        }

        private void beginStudyVocabulary()
        {
            if (_vocabulary.Count > 0)
            {
                _currentSection = common.Constants.EXERCISE_SECTION_VOCABULARY;
                List<RepeatingWordInfo> listToShuffle = new List<RepeatingWordInfo>(_vocabulary);
                listToShuffle.Shuffle();
                _workingQueue = new Queue<RepeatingWordInfo>(listToShuffle);
                showExerciseWindowAndbeginPractice();
            }
            else
            {
                MessageBox.Show(Properties.Strings.error_no_words_in_vocabulary, Properties.Strings.error_text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string getLocalizedNameForSection(uint section)
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
