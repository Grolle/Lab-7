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

                System.Console.WriteLine($"Current Score : {state}");


                Render(state);

                var input = Console.ReadLine();
                if (input == null)
                {
                    break;
                }

                HandleInput(input.Trim());
            }
        }

        public void HandleInput(string input)
        {
            var state = _engine.GetState();

            switch (state.Phase)
            {
                case GamePhase.Settings:
                    HandleSettingsInput(input);
                    break;
                case GamePhase.Menu:
                    HandleMenuInput(input);
                    break;
                case GamePhase.Playing:
                    HandlePlayingInput(input);
                    break;
                case GamePhase.Won:
                case GamePhase.Lost:
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