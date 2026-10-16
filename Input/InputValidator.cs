namespace Lab_7.Input
{
    public static class InputValidator
    {
        public static bool IsValidMenuChoice(string input)
        {
            return input == "1" || input == "2";
        }

        public static bool IsValidLetter(string input)
        {
            return input.Length == 1 && char.IsLetter(input[0]);
        }

        public static bool IsValidWord(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }
    }
}