using Lab_7.Model.Enums;

namespace Lab_7.Model.DataModels
{
    /// <summary>
    /// Record that holds the runtime settings specific game.
    /// </summary>
    /// <param name="">Language language is the language words are picked in.
    /// Difficulty difficulty is the difficulty current game runs on.
    /// int Lives are the max lives the user has when playing this round.
    /// float LifeScale is the scale we use as pity system for player across difficulties.
    /// </param>
    /// <returns></returns>
    public record GameSettings(Language Language, Difficulty Difficulty, int Lives, float LifeScale);
}