using Newtonsoft.Json.Linq;

namespace RussianTasks.src.exercises
{
    public interface IExercise
    {
        string getName();
        void startExercise();
        void load(JObject dict);
        JObject save();
        void finishExercise();
    }
}