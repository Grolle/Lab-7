using System;
using System.Linq;

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
            WriteCenteredBlock(HangmanFrames[frame]);
        }

        /// <summary>
        /// Writes a multi-line block of text horizontally centered in the console window,
        /// padding every line by the same amount so the block's internal alignment is preserved.
        /// </summary>
        /// <param name="">
        /// string block is the multi-line text to center and write.</param>
        /// <returns></returns>
        public static void WriteCenteredBlock(string block)
        {
            string[] lines = block.Split('\n');
            int widest = lines.Max(l => l.Length);
            int padLeft = Math.Max(0, (Console.WindowWidth - widest) / 2);
            string padding = new string(' ', padLeft);
            foreach (string line in lines)
            {
                Console.WriteLine(padding + line);
            }
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
