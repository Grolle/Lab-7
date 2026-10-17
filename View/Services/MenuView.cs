using System;
using Lab_7.Model.DataModels;
using Lab_7.View.Interfaces;

namespace Lab_7.View.Services
{
    public class MenuView : IView
    {
        /// <summary>
        /// Renders the main menu with the play and quit options.
        /// </summary>
        /// <param name="">GameState state contains the current data, unused by this view</param>
        /// <returns></returns>
        public void Render(GameState state)
        {
            ViewHelpers.ClearScreen();
            ViewHelpers.WriteCentered("=== Hangman ===");
            ViewHelpers.WriteCentered("1. Play");
            ViewHelpers.WriteCentered("2. Quit");
        }
    }
}