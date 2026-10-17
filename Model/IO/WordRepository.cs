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

        /// <summary>
        /// Read and cache all words, language and difficulty enums are used to construct full path to .txt files.
        /// </summary>
        /// <param name="">string basePath root folder that contains the language word folders</param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns all loaded words for the given language and difficulty.
        /// </summary>
        /// <param name="">Language language the language to fetch words for</param>
        /// <param name="">Difficulty difficulty the difficulty tier to fetch words for</param>
        /// <returns></returns>
        public IEnumerable<string> GetWords(Language language, Difficulty difficulty)
        {
            return _words[language][difficulty];
        }

        /// <summary>
        /// Picks a random word from the given language and difficulty, returns empty string if that list has no words.
        /// </summary>
        /// <param name="">Language language the language to pick a word from</param>
        /// <param name="">Difficulty difficulty the difficulty tier to pick a word from</param>
        /// <returns></returns>
        public string GetRandomWord(Language language, Difficulty difficulty)
        {
            var list = _words[language][difficulty];
            if (list.Count == 0)
            {
                return string.Empty;
            }
            return list[_random.Next(list.Count)];
        }

        /// <summary>
        /// Read file from contructed path based on param, taken locally from =>Words/sv(or en)/easy(medium,hard).txt
        /// If no file exist at path returns empty list otherwise fetch all lines as strings and return as list. Trim and remove white spaces from word so they are able to be used in hangmang game.
        /// </summary>
        /// <param name="">string basePath is path to and including the word folder
        /// Language language used to specify which folder used as path2.
        /// Difficulty difficulty used to fetch correct .txt file.
        /// </param>
        /// <returns></returns>
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
