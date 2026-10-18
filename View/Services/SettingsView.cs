using System;
using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class SettingsView : IView
    {
        /// <summary>
        /// Renders the settings screen, stepping through language then difficulty selection.
        /// </summary>
        /// <param name="">GameState state contains the current settings to display</param>
        /// <returns></returns>
        public void Render(GameState state)
        {
            ViewHelpers.ClearScreen();
            ViewHelpers.WriteCentered("=== Settings ===");
            Console.WriteLine();
            Console.WriteLine($"Current  Language: [{state.Settings.Language}]   Difficulty: [{state.Settings.Difficulty}]");
            Console.WriteLine();

            var prompt = state.SettingState == SettingState.Language
                ? "Language  (1 = Swedish, 2 = English) — Enter to keep:"
                : "Difficulty (1 = Easy, 2 = Medium, 3 = Hard) — Enter to keep:";

            Console.WriteLine(prompt);
        }
    }
}