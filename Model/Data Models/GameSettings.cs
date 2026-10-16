using Lab_7.Model.Enums;

namespace Lab_7.Model.DataModels
{
    public record GameSettings(Language Language, Difficulty Difficulty, int Lives);
}