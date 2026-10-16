using System.Collections.Generic;
using Lab_7.Model.Enums;

namespace Lab_7.Model.DataModels
{
    public class GameContext
    {
        #region fields
        private string _secretWord;
        private string _maskedWord;
        private readonly HashSet<char> _guessedLetters;
        private readonly Dictionary<char, int> _remainingLetters;
        private int _livesLeft;
        private readonly int _maxLives;
        private GamePhase _phase;
        private int _currentRound;
        private int _score;
        #endregion

        #region  properties
        public string SecretWord
        {
            get => _secretWord;
            set => _secretWord = value;
        }

        public string MaskedWord
        {
            get => _maskedWord;
            set => _maskedWord = value;
        }

        public HashSet<char> GuessedLetters => _guessedLetters;
        public Dictionary<char, int> RemainingLetters => _remainingLetters;

        public int LivesLeft
        {
            get => _livesLeft;
            set => _livesLeft = Math.Clamp(value, 0, _maxLives);
        }

        public int MaxLives => _maxLives;

        public GamePhase Phase
        {
            get => _phase;
            set => _phase = value;
        }

        public int CurrentRound
        {
            get => _currentRound;
            set => _currentRound = value;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }
        #endregion

        public GameContext(string secretWord, int livesLeft, GamePhase phase, int currentRound)
        {
            _secretWord = secretWord;
            _maskedWord = "";
            _guessedLetters = [];
            _livesLeft = livesLeft;
            _maxLives = livesLeft;
            _phase = phase;
            _currentRound = currentRound;
            _remainingLetters = [];
            _score = 0;

            Init();
        }

        private void Init()
        {
            SetupMaskedWord();
            SetupRemainingWord();
        }

        private void SetupMaskedWord()
        {
            foreach (var c in _secretWord)
            {
                _maskedWord += '_';
            }
        }

        private void SetupRemainingWord()
        {
            foreach (var c in _secretWord)
            {
                if (_remainingLetters.TryGetValue(c, out int reoccurrences))
                {
                    ++reoccurrences;
                    _remainingLetters[c] = reoccurrences;
                }
                else
                {
                    _remainingLetters.Add(c, 1);
                }
            }
        }
    }
}