using System;
using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.Model.Interfaces;

namespace Lab_7.Model
{
    public class GameEngine(GameSettings settings, IWordProvider wordProvider) : IGameEngine
    {
        private GameSettings _settings = settings;
        private readonly IWordProvider _wordProvider = wordProvider;
        private GameContext _context = new(string.Empty, settings.Lives, GamePhase.Settings, 1, SettingState.Language);
        private int roundsSinceLastPointDeduction = 0;

        public GameSettings Settings => _settings;

        /// <summary>
        /// Called at start of game. Fetches new word from provider and calls SetupGame
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public void StartRound()
        {
            var word = _wordProvider.GetRandomWord(_settings.Language, _settings.Difficulty);
            SetupGame(word);
        }
        /// <summary>
        /// Creates a new GameContext and set it up with generated word, full lives, correct GamePhase as well as round.
        /// </summary>
        /// <param name="">
        /// string word is the selected word for this game.</param>
        /// <returns></returns>
        public void SetupGame(string word)
        {
            _context = new(word, _settings.Lives, GamePhase.Playing, 1, SettingState.Language);
        }
        /// <summary>
        /// Get updated GamePhase and updates the current gamecontext.
        /// </summary>
        /// <param name="">
        /// GamePhase phase is the current phase the game is in.</param>
        /// <returns></returns>
        public void SetPhase(GamePhase phase)
        {
            _context.Phase = phase;
        }
        /// <summary>
        /// Creates a new instance of setting with updated Language 
        /// </summary>
        /// <param name="">
        /// Language language used in new runtime setting.</param>
        /// <returns></returns>
        public void SetLanguage(Language language)
        {
            _settings = _settings with { Language = language };
        }
        /// <summary>
        /// Creates new instance of setting with updated difficulty and life scalar
        /// </summary>
        /// <param name="">
        /// Difficulty difficulty used in new runtime settning
        /// float lifeScalar used in new runtime settning
        /// </param>
        /// <returns></returns>
        public void SetDifficulty(Difficulty difficulty, float lifeScalar)
        {
            _settings = _settings with { Difficulty = difficulty, LifeScale = lifeScalar };
        }
        /// <summary>
        /// Get user input and calls to process it and after re-evaluate the current gamecontext
        /// </summary>
        /// <param name="">
        /// string input user input for guessing word
        /// </param>
        /// <returns></returns>

        public void SetSettingState(SettingState settingState)
        {
            _context.SettingState = settingState;
        }

        public void GuessWord(string input)
        {
            ProcessInput(input);
            ReevaluateGameContext();
        }
        /// <summary>
        /// Returns new gamestate struct containing current data about game. Creates a string based on HashSet with guessed letters in GameContext.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public GameState GetState()
        {
            string guessedLetters = string.Concat(_context.GuessedLetters);
            return new GameState(_context.Phase, _context.MaskedWord, _context.LivesLeft, _settings, _context.Score, guessedLetters, _context.SecretWord, _context.SettingState);
        }

        #region Services
        /// <summary>
        /// Determines current gameplay context. If no lives left game is over, update GamePhase.
        /// If user has lives left update the masked word for view to display and check if there are
        /// any remaining letters not guessed. If not, set GamePhase to won.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void ReevaluateGameContext()
        {
            if (_context.LivesLeft > 0)
            {
                string updatedMaskedWord = "";

                for (int i = 0; i < _context.SecretWord.Length; i++)
                {
                    updatedMaskedWord += _context.RemainingLetters[_context.SecretWord[i]] > 0 ? '_' : _context.SecretWord[i];
                }

                _context.MaskedWord = updatedMaskedWord;
                bool success = true;
                foreach (var kvp in _context.RemainingLetters)
                {
                    if (kvp.Value > 0)
                    {
                        success = false;
                        break;
                    }
                }
                if (success)
                {
                    SetPhase(GamePhase.Won);
                }
            }
            else
            {
                SetPhase(GamePhase.Lost);
            }
        }
        /// <summary>
        /// Process user input. Checks if user has already guessed character, if so return because we 
        /// don't deduct point on same character twice.
        /// If the user guess correct character that has not been guessed then add points
        /// corresponding to number of times character is used in word and reset 
        /// rounds since last point deduction.
        /// If character does not exist, call DeductLife function.
        /// </summary>
        /// <param name="">
        /// string input input guess from user.
        /// </param>
        /// <returns></returns>
        private void ProcessInput(string input)
        {
            char c = input[0];

            if (_context.GuessedLetters.Contains(c)) //prevent from losing points for same guess
                return;

            if (_context.RemainingLetters.TryGetValue(c, out int reoccurrences))
            {
                _context.Score += reoccurrences;
                _context.RemainingLetters[c] = 0;
                ResetRoundsSinceLastDeduction();
            }
            else
            {
                DeductLife(_settings.LifeScale);
            }

            _context.GuessedLetters.Add(c);
        }

        /// <summary>
        /// Check wither threshold for deducting point is met, if so deduct a point
        /// and reset rounds since last deduction.
        /// </summary>
        /// <param name="">
        /// float scalarValue used to determine threshold for when to deduct point.
        /// </param>
        /// <returns></returns>
        private void DeductLife(float scalarValue)
        {
            ++roundsSinceLastPointDeduction;

            int threshold = (int)Math.Ceiling(1f / scalarValue);
            if (roundsSinceLastPointDeduction >= threshold)
            {
                ResetRoundsSinceLastDeduction();
                _context.LivesLeft--;
            }
        }

        /// <summary>
        /// Helper function to reset the rounds since last deduction variable.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void ResetRoundsSinceLastDeduction() => roundsSinceLastPointDeduction = 0;
        #endregion
    }
}

