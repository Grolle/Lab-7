using System;
using Lab_7.Model.Enums;

namespace Lab_7.Model.DataModels;

    /// <summary>
    /// Single source of truth for values that are not user-changeable at runtime,
    /// such as default language/difficulty, starting lives and life scales per difficulty.
    /// </summary>
    public class StaticSettings
    {
    public static int Lives { get; private set; } = 6;

    public static Language DefaultLanguage { get; private set; } = Language.Swedish;
    public static Difficulty DefaultDifficulty { get; private set; } = Difficulty.Medium;

    public static float LifeScaleEasy { get; private set; } = 0.25f;
    public static float LifeScaleMedium { get; private set; } = 0.5f;
    public static float LifeScaleHard { get; private set; } = 1.0f;

    /// <summary>
    /// Maps scale to difficulty
    /// </summary>
    /// <param name="">Difficulty difficulty used to determine lifeScale</param>
    /// <returns></returns>
    public static float GetLifeScale(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Easy => LifeScaleEasy,
        Difficulty.Medium => LifeScaleMedium,
        Difficulty.Hard => LifeScaleHard,
        _ => LifeScaleMedium
    };
}

