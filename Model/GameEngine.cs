using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.Model.Interfaces;

namespace Lab_7.Model
{
    public class GameEngine(GameSettings settings, IWordProvider wordProvider) : IGameEngine
    {
        private GameSettings _settings = settings;
        private readonly IWordProvider _wordProvider = wordProvider;
        private GameContext _context = new(string.Empty, settings.Lives, GamePhase.Settings, 1);


        public GameSettings Settings => _settings;

        public void StartRound()
        {
            var word = _wordProvider.GetRandomWord(_settings.Language, _settings.Difficulty);
            SetupGame(word);
        }

        public void SetupGame(string word)
        {
            _context = new(word, _settings.Lives, GamePhase.Playing, 1);
        }

        public void SetPhase(GamePhase phase)
        {
            _context.Phase = phase;
        }

        public void SetLanguage(Language language)
        {
            _settings = _settings with { Language = language };
        }

        public void SetDifficulty(Difficulty difficulty)
        {
            _settings = _settings with { Difficulty = difficulty };
        }

        public void GuessWord(string word)
        {
            ProcessInput(word);
            ReevaluateGameContext();
        }

        public GameState GetState()
        {
            return new GameState(_context.Phase, _context.MaskedWord, _context.LivesLeft, _settings, _context.Score);
        }

        #region Services
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

        private void ProcessInput(string word)
        {
            string scrubbedWord = "";

            foreach (var c in word)
            {
                if (_context.GuessedLetters.Contains(c))
                {
                    _context.LivesLeft--;
                    continue;
                }
                scrubbedWord += c;
            }

            foreach (var c in scrubbedWord)
            {
                if (_context.RemainingLetters.TryGetValue(c, out int reoccurrences))
                {
                    if (reoccurrences > 0)
                    {
                        _context.RemainingLetters[c] = 0;
                        _context.Score++;
                    }
                    else
                    {
                        _context.LivesLeft--;
                    }
                }
                else
                {
                    _context.LivesLeft--;
                }

                _context.GuessedLetters.Add(c);
            }
        }
        #endregion
    }
}

