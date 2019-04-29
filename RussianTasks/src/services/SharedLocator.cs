using RussianTasks.src.exercises;

namespace RussianTasks.src.services
{
    class SharedLocator
    {
        private static StaticInfo _staticInfo;
        private static ExerciseManager _exercisesManager;

        public static StaticInfo getStaticInfo() { return _staticInfo; }
        public static void setStaticInfo(StaticInfo staticInfo) { _staticInfo = staticInfo; }

        public static ExerciseManager getExercisesManager() { return _exercisesManager; }
        public static void setExercisesManager(ExerciseManager exercisesManager) { _exercisesManager = exercisesManager; }
    }
}