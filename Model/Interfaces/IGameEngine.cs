using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;

namespace Lab_7.Model.Interfaces
{
    public interface IGameEngine
    {
        void SetPhase(GamePhase phase);
        void GuessWord(string word);
        GameState GetState();
        GameSettings Settings { get; }
        void SetLanguage(Language language);
        void SetDifficulty(Difficulty difficulty, float lifeScalar);
        void StartRound();
    }
}