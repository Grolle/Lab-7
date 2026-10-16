using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_7.Model.Enums;
using Lab_7.Model.Interfaces;

namespace Lab_7.Model.IO
{
    public class WordRepository : IWordProvider
    {
        private static readonly Dictionary<Language, string> LanguageFolders = new()
        {
            [Language.English] = "en",
            [Language.Swedish] = "sv"
        };

        private readonly Dictionary<Language, Dictionary<Difficulty, List<string>>> _words = new();
        private readonly Random _random = new();

        public WordRepository(string basePath)
        {
            foreach (var language in Enum.GetValues<Language>())
            {
                var byDifficulty = new Dictionary<Difficulty, List<string>>();

                foreach (var difficulty in Enum.GetValues<Difficulty>())
                {
                    byDifficulty[difficulty] = LoadWords(basePath, language, difficulty);
                }

                _words[language] = byDifficulty;
            }
        }

        public IEnumerable<string> GetWords(Language language, Difficulty difficulty)
        {
            return _words[language][difficulty];
        }

        public string GetRandomWord(Language language, Difficulty difficulty)
        {
            var list = _words[language][difficulty];
            if (list.Count == 0)
            {
                return string.Empty;
            }
            return list[_random.Next(list.Count)];
        }

        private static List<string> LoadWords(string basePath, Language language, Difficulty difficulty)
        {
            var filePath = Path.Combine(
                basePath,
                LanguageFolders[language],
                $"{difficulty.ToString().ToLowerInvariant()}.txt");

            if (!File.Exists(filePath))
            {
                return new List<string>();
            }

            return File.ReadAllLines(filePath)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();
        }
    }
}