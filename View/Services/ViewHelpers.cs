using System;

namespace Lab_7.View.Services
{
    public static class ViewHelpers
    {
        private static readonly string[] HangmanFrames =
        {
            // 0 wrong guesses — empty gallows
            "  +---+\n" + "  |   |\n" + "      |\n" + "      |\n" + "      |\n" + "      |\n" + "=========",
            // 1 — head
            "  +---+\n" + "  |   |\n" + "  O   |\n" + "      |\n" + "      |\n" + "      |\n" + "=========",
            // 2 — body
            "  +---+\n" + "  |   |\n" + "  O   |\n" + "  |   |\n" + "      |\n" + "      |\n" + "=========",
            // 3 — left arm
            "  +---+\n" + "  |   |\n" + "  O   |\n" + " /|   |\n" + "      |\n" + "      |\n" + "=========",
            // 4 — right arm
            "  +---+\n" + "  |   |\n" + "  O   |\n" + " /|\\  |\n" + "      |\n" + "      |\n" + "=========",
            // 5 — left leg
            "  +---+\n" + "  |   |\n" + "  O   |\n" + " /|\\  |\n" + " /    |\n" + "      |\n" + "=========",
            // 6 — right leg (dead)
            "  +---+\n" + "  |   |\n" + "  O   |\n" + " /|\\  |\n" + " / \\  |\n" + "      |\n" + "========="
        };

        /// <summary>
        /// Clears the console screen before rendering a new view.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Draws the hangman frame based on how many lives are left compared to max lives.
        /// </summary>
        /// <param name="">
        /// int lives is the player's current lives left.
        /// int maxLives is the number of lives the round started with.</param>
        /// <returns></returns>
        public static void DrawHangman(int lives, int maxLives)
        {
            int wrongGuesses = maxLives - lives;
            int frame = Math.Clamp(wrongGuesses, 0, HangmanFrames.Length - 1);
            Console.WriteLine(HangmanFrames[frame]);
        }

        /// <summary>
        /// Writes a line of text centered horizontally in the console window.
        /// </summary>
        /// <param name="">
        /// string text is the text to center and write.</param>
        /// <returns></returns>
        public static void WriteCentered(string text)
        {
            var left = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
            Console.WriteLine(new string(' ', left) + text);
        }
    }
}