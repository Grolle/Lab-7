using Lab_7.Model.Enums;

namespace Lab_7.Model.DataModels
{
    public readonly record struct GameState(GamePhase Phase, string MaskedWord, int LivesLeft, GameSettings Settings, int Score, string GuessedLetters);

}