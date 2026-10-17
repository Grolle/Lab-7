using System;

namespace Lab_7.View.Services
{
    public static class UserInput
    {
        /// <summary>
        /// Reads input from the user and keeps asking until the validator accepts it, writing the error message on invalid input.
        /// </summary>
        /// <param name="">
        /// Func<string,bool> validator validates the input.
        /// string errorMessage is shown when the input is invalid.</param>
        /// <returns></returns>
        public static string Read(Func<string,bool> validator, string errorMessage)
        {
            bool correctInput = false;
            string input = "";
            while(!correctInput)
            {
                input = System.Console.ReadLine() ?? string.Empty;
                correctInput = validator(input);

                if(!correctInput)
                {
                    Console.WriteLine(errorMessage);
                }
            }

            return input;
        }

        /// <summary>
        /// Reads a single line of input from the user with no validation.
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string Read()=> Console.ReadLine() ?? string.Empty;
    }
}