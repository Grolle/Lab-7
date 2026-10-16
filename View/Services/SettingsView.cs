using System;
using Lab_7.Model.DataModels;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class SettingsView : IView
    {
        private int _step;

        public void Reset() => _step = 0;

        public void Render(GameState state)
        {
            ViewHelpers.ClearScreen();
            ViewHelpers.WriteCentered("=== Settings ===");
            Console.WriteLine();
            Console.WriteLine($"Current  Language: [{state.Settings.Language}]   Difficulty: [{state.Settings.Difficulty}]");
            Console.WriteLine();

            var prompt = _step == 0
                ? "Language  (1 = Swedish, 2 = English) — Enter to keep:"
                : "Difficulty (1 = Easy, 2 = Medium, 3 = Hard) — Enter to keep:";

            Console.WriteLine(prompt);

            if (_step < 1)
            {
                _step++;
            }
        }
    }
}