using Newtonsoft.Json.Linq;
using RussianTasks.src.exercises.accents;
using RussianTasks.src.exercises.spelling;
using RussianTasks.src.exercises.repeating;
using RussianTasks.src.exercises.variant;
using System.Collections.Generic;

namespace RussianTasks.src.exercises
{
    class ExerciseManager
    {
        public const string MODULE_NAME = "exercisesManager";

        private IExercise _currentExercise;
        Dictionary<string, JObject> _exercisesUserData = new Dictionary<string, JObject>();

        public ExerciseManager()
        {
        }


        public JObject save()
        {
            JObject dict = new JObject();

            string currentExerciseName = null;

            if (_currentExercise != null)
            {
                currentExerciseName = _currentExercise.getName();
                dict[currentExerciseName] = _currentExercise.save();
            }

            foreach (KeyValuePair<string, JObject> exerciseData in _exercisesUserData)
            {
                if (exerciseData.Key != currentExerciseName)
                {
                    dict[exerciseData.Key] = exerciseData.Value;
                }
            }

            return dict;
        }


        public void load(JObject dict)
        {
            foreach (JProperty token in dict.Children<JProperty>())
            {
                _exercisesUserData.Add(token.Name, (JObject)token.Value);
            }
        }


        public void startExercise(string name)
        {
            if (_currentExercise == null)
            {
                switch (name)
                {
                    case AccentExercise.sName:
                        _currentExercise = new AccentExercise();
                        break;
                    case SpellingExercise.sName:
                        _currentExercise = new SpellingExercise();
                        break;
                    case RepeatingExercise.sName:
                        _currentExercise = new RepeatingExercise();
                        break;
                    case VariantExercise.sName:
                        _currentExercise = new VariantExercise();
                        break;
                }

                if (_currentExercise != null)
                {
                    if (_exercisesUserData.ContainsKey(name))
                    {
                        _currentExercise.load(_exercisesUserData[name]);
                    }
                    _currentExercise.startExercise();
                }
                else
                {
                    throw new System.Exception(string.Format("No exercise with such name {0}", name));
                }
            }
            else
            {
                throw new System.Exception("current exercise is not null");
            }
        }


        public void finishExercise(IExercise exercise)
        {
            if (_currentExercise.Equals(exercise))
            {
                _exercisesUserData[exercise.getName()] = exercise.save();
                _currentExercise = null;
                MainActivity.getInstance().showTasksWindow();
            }
        }

        public void finishCurrentExercise()
        {
            if (_currentExercise != null)
            {
                finishExercise(_currentExercise);
            }
        }

    }
}