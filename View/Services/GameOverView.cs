using System;
using Lab_7.Model.DataModels;
using Lab_7.Model.Enums;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class GameOverView : IView
    {
        public void Render(GameState state)
        {
            ViewHelpers.ClearScreen();

            var message = state.Phase == GamePhase.Won ? "You won!" : "You lost.";
            ViewHelpers.WriteCentered(message);
            ViewHelpers.WriteCentered($"Final score: {state.Score}");
            Console.WriteLine();
            ViewHelpers.WriteCentered("Press any key to continue...");
        }
    }
}