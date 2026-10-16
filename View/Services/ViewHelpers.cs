using System;
using Lab_7.Model.DataModels;

namespace Lab_7.View.Services
{
    public static class ViewHelpers
    {
        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void DrawHangman(int lives)
        {
            // TODO: draw hangman based on remaining lives
            Console.WriteLine($"[Hangman drawing — {lives} lives remaining]");
        }

        public static void WriteCentered(string text)
        {
            var left = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
            Console.WriteLine(new string(' ', left) + text);
        }
    }
}