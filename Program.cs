using System.Collections.Generic;
using System.IO;
using Lab_7.Controller;
using Lab_7.Model;
using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.Model.IO;
using Lab_7.View.Interfaces;
using Lab_7.View.Services;

namespace Lab_7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameSettings settings = new(Language.Swedish, Difficulty.Medium, 6);

            string wordsPath = Path.Combine(AppContext.BaseDirectory, "Words");
            WordRepository wordRepository = new(wordsPath);
            GameEngine engine = new(settings, wordRepository);
            SettingsView settingsView = new();
            MenuView menuView = new();
            GameView gameView = new();
            GameOverView gameOverView = new();

            Dictionary<GamePhase, IView> views = [];
            views.Add(GamePhase.Settings, settingsView);
            views.Add(GamePhase.Menu, menuView);
            views.Add(GamePhase.Playing, gameView);
            views.Add(GamePhase.Won, gameOverView);
            views.Add(GamePhase.Lost, gameOverView);

            ViewRegistry viewRegistry = new(views);
            GameController controller = new(engine, viewRegistry);
            controller.Run();
        }
    }
}

