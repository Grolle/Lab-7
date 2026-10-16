using System;
using Lab_7.Model.DataModels;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class GameView : IView
    {
        public void Render(GameState state)
        {
            ViewHelpers.ClearScreen();
            ViewHelpers.DrawHangman(state.Settings.Lives);
            Console.WriteLine($"Difficulty: {state.Settings.Difficulty}  |  Lives: {state.LivesLeft}  |  Score: {state.Score}");
            Console.WriteLine();
        }
    }
}