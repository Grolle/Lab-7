using System;
using System.Linq;
using System.Text;
using Lab_7.Model.DataModels;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class GameView : IView
    {
        public void Render(GameState state)
        {
            ViewHelpers.ClearScreen();

            ViewHelpers.DrawHangman(state.LivesLeft, state.Settings.Lives);
            Console.WriteLine();

            ViewHelpers.WriteCentered(FormatMaskedWord(state.MaskedWord));
            Console.WriteLine();

            ViewHelpers.WriteCentered($"Difficulty: {state.Settings.Difficulty}   Lives: {state.LivesLeft}/{state.Settings.Lives}   Score: {state.Score}");
            Console.WriteLine();
            ViewHelpers.WriteCentered($"Guessed Letters : {FormatGuessedLetters(state.GuessedLetters)}");
            Console.WriteLine();
            Console.WriteLine("Guess a letter and press Enter:");
        }

        private static string FormatMaskedWord(string maskedWord)
        {
            var sb = new StringBuilder();
            foreach (var c in maskedWord)
            {
                sb.Append(c);
                sb.Append(' ');
            }
            return sb.ToString().TrimEnd();
        }

        private static string FormatGuessedLetters(string guessedLetters)
        {
            return string.Join(' ', guessedLetters.OrderBy(c => c));
        }
    }
}