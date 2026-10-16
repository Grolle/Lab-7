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

        public static void ClearScreen()
        {
            Console.Clear();
        }

        public static void DrawHangman(int lives, int maxLives)
        {
            int wrongGuesses = maxLives - lives;
            int frame = Math.Clamp(wrongGuesses, 0, HangmanFrames.Length - 1);
            Console.WriteLine(HangmanFrames[frame]);
        }

        public static void WriteCentered(string text)
        {
            var left = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
            Console.WriteLine(new string(' ', left) + text);
        }
    }
}