using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.Model.Interfaces;
using Lab_7.View.Services;

namespace Lab_7.Controller
{
    public class GameController(IGameEngine engine, ViewRegistry viewRegistry)
    {
        private readonly IGameEngine _engine = engine;
        private readonly ViewRegistry _viewRegistry = viewRegistry;

        /// <summary>
        /// Run the gameplay loop from controller. Routes between view and model. If Phase == quit we just break out of loop and let app quit.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
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

        /// <summary>
        /// Will call UserInput for input from user and pass in validator as well as error message.
        /// </summary>
        /// <param name="">GameState state needed to route and validate input based on current game phase</param>
        /// <returns></returns>
        public void HandleInput(GameState state)
        {
            string input;
            switch (state.Phase)
            {
                case GamePhase.Settings:
                    input = state.SettingState == SettingState.Language
                        ? UserInput.Read(s => string.IsNullOrEmpty(s) || s == "1" || s == "2", "Please type 1 or 2.")
                        : UserInput.Read(s => string.IsNullOrEmpty(s) || s == "1" || s == "2" || s == "3", "Please type 1, 2 or 3.");
                    HandleSettingsInput(state.SettingState, input);
                    break;
                case GamePhase.Menu:
                    input = UserInput.Read(s => s == "1" || s == "2" || s == "3", "Please type 1, 2 or 3.");
                    HandleMenuInput(input);
                    break;
                case GamePhase.Playing:
                    input = UserInput.Read(s => s.All(char.IsLetter) && !string.IsNullOrEmpty(s) && s.Length == 1, "Please type only 1 character.");
                    HandlePlayingInput(input);
                    break;
                case GamePhase.Won:
                case GamePhase.Lost:
                    UserInput.Read();//this is just a press any button, we dont need to store actual input
                    HandleGameOverInput();
                    break;
            }
        }
        /// <summary>
        /// Fork input from user between language setting and difficulty setting based on _choosingDifficulty bool. 
        /// </summary>
        /// <param name="">string input comes from user input.</param>
        /// <returns></returns>
        private void HandleSettingsInput(SettingState settingState, string input)
        {
            switch (settingState)
            {
                case SettingState.Language:
                    ApplyLanguage(input);
                    _engine.SetSettingState(SettingState.Difficulty);
                    break;
                case SettingState.Difficulty:
                    ApplyDifficulty(input);
                    _engine.SetPhase(GamePhase.Menu);
                    break;
            }
        }
        /// <summary>
        /// Applies language based on input. If neither options are passed in (input validator allows for empty string) then use default language.
        /// </summary>
        /// <param name="">string input user input.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Applies the difficulty based on user input, if empty string is entered then use default difficulty.
        /// </summary>
        /// <param name="">string input user input</param>
        /// <returns></returns>
        private void ApplyDifficulty(string input)
        {
            switch (input)
            {
                case "1":
                    _engine.SetDifficulty(Difficulty.Easy, StaticSettings.GetLifeScale(Difficulty.Easy));
                    break;
                case "2":
                    _engine.SetDifficulty(Difficulty.Medium, StaticSettings.GetLifeScale(Difficulty.Medium));
                    break;
                case "3":
                    _engine.SetDifficulty(Difficulty.Hard, StaticSettings.GetLifeScale(Difficulty.Hard));
                    break;
            }
        }

        /// <summary>
        /// Takes user input and applies menu action, either start the game or quit app
        /// </summary>
        /// <param name="">string userInput to pick menu option</param>
        /// <returns></returns>
        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    _engine.StartRound();
                    _engine.SetPhase(GamePhase.Playing);
                    break;
                case "2":
                    _engine.SetPhase(GamePhase.Settings);
                    _engine.SetSettingState(StaticSettings.DefaultSettingState);
                    break;
                case "3":
                    _engine.SetPhase(GamePhase.Quit);
                    break;
            }
        }

        /// <summary>
        /// Takes user input string during gameplay loop and send it to engine to guess the word.
        /// </summary>
        /// <param name="">string userInput which contains a character to guess the word</param>
        /// <returns></returns>
        private void HandlePlayingInput(string input)
        {
            _engine.GuessWord(input);
        }

        /// <summary>
        /// Updates the engine with new GamePhase set to menu.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private void HandleGameOverInput()
        {
            _engine.SetPhase(GamePhase.Menu);
        }

        /// <summary>
        /// Fetches a view from registry and render it based on current current Phase.
        /// Pass in the gamestate which is needed to render correct data.
        /// </summary>
        /// <param name="">GameState state contains current phase for correct viewer as well as data to display</param>
        /// <returns></returns>
        private void Render(GameState state)
        {
            var view = _viewRegistry.Get(state.Phase);
            view.Render(state);
        }
    }
}