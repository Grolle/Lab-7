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

        /// <summary>
        /// Creates a new game context for a round with the selected word, starting lives,
        /// start phase and round number. Runs Init to set up masked word and remaining letters.
        /// </summary>
        /// <param name="">
        /// string secretWord is the word to guess this round.
        /// int livesLeft is the number of lives the player starts with.
        /// GamePhase phase is the phase the game starts in.
        /// int currentRound is the current round number.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Init function to gather the calls that are needed for context to work
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void Init()
        {
            SetupMaskedWord();
            SetupRemainingWord();
        }
        /// <summary>
        /// Setup masked word string with correct amount of '_'.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void SetupMaskedWord()
        {
            foreach (var c in _secretWord)
            {
                _maskedWord += '_';
            }
        }
        /// <summary>
        /// Setup the dictionary where we track remainder of characters not guessed in the selected word.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
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
