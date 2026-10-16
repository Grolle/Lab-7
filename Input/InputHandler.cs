using System;
using Lab_7.Controller;

namespace Lab_7.Input
{
    public class InputHandler
    {
        private readonly GameController _controller;

        public InputHandler(GameController controller)
        {
            _controller = controller;
        }

        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == null)
                {
                    break;
                }

                _controller.HandleInput(input.Trim());
            }
        }
    }
}