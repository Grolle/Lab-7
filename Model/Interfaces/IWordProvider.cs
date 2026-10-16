using System.Collections.Generic;
using Lab_7.Model.Enums;

namespace Lab_7.Model.Interfaces
{
    public interface IWordProvider
    {
        IEnumerable<string> GetWords(Language language, Difficulty difficulty);
        string GetRandomWord(Language language, Difficulty difficulty);
    }
}