using System;
using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.Model.Interfaces;
using Lab_7.View.Interfaces;
using Lab_7.View.Services;

namespace Lab_7.Controller
{
    public class GameController(IGameEngine engine, ViewRegistry viewRegistry)
    {
        private readonly IGameEngine _engine = engine;
        private readonly ViewRegistry _viewRegistry = viewRegistry;

        private bool _choosingDifficulty;

        public void Run()
        {
            while (true)
            {
                var state = _engine.GetState();

                if (state.Phase == GamePhase.Quit)
                {
                    break;
                }

                Render(state);
                HandleInput(state);
            }
        }

        public void HandleInput(GameState state)
        {
            string input;
            switch (state.Phase)
            {
                case GamePhase.Settings:
                    input = _choosingDifficulty
                        ? UserInput.Read(s => string.IsNullOrEmpty(s) || s == "1" || s == "2" || s == "3", "Please type 1, 2 or 3.")
                        : UserInput.Read(s => string.IsNullOrEmpty(s) || s == "1" || s == "2", "Please type 1 or 2.");
                    HandleSettingsInput(input);
                    break;
                case GamePhase.Menu:
                    input = UserInput.Read(s => s == "1" || s== "2", "Please type 1 or 2.");
                    HandleMenuInput(input);
                    break;
                case GamePhase.Playing:
                    input = UserInput.Read(s => s.All(char.IsLetter) && !string.IsNullOrEmpty(s) && s.Length == 1,"Please type only 1 character.");
                    HandlePlayingInput(input);
                    break;
                case GamePhase.Won:
                case GamePhase.Lost:
                    input = UserInput.Read();
                    HandleGameOverInput(input);
                    break;
            }
        }

        private void HandleSettingsInput(string input)
        {
            if (!_choosingDifficulty)
            {
                ApplyLanguage(input);
                _choosingDifficulty = true;
            }
            else
            {
                ApplyDifficulty(input);
                _choosingDifficulty = false;
                _engine.SetPhase(GamePhase.Menu);
            }
        }

        private void ApplyLanguage(string input)
        {
            switch (input)
            {
                case "1":
                    _engine.SetLanguage(Language.Swedish);
                    break;
                case "2":
                    _engine.SetLanguage(Language.English);
                    break;
            }
        }

        private void ApplyDifficulty(string input)
        {
            switch (input)
            {
                case "1":
                    _engine.SetDifficulty(Difficulty.Easy);
                    break;
                case "2":
                    _engine.SetDifficulty(Difficulty.Medium);
                    break;
                case "3":
                    _engine.SetDifficulty(Difficulty.Hard);
                    break;
            }
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    _engine.StartRound();
                    _engine.SetPhase(GamePhase.Playing);
                    break;
                case "2":
                    _engine.SetPhase(GamePhase.Quit);
                    break;
            }
        }

        private void HandlePlayingInput(string input)
        {
            _engine.GuessWord(input);
        }

        private void HandleGameOverInput(string input)
        {
            _engine.SetPhase(GamePhase.Menu);
        }

        private void Render(GameState state)
        {
            var view = _viewRegistry.Get(state.Phase);
            view.Render(state);
        }
    }
}