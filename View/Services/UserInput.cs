using System;

namespace Lab_7.View.Services
{
    public static class UserInput
    {
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

        public static string Read()=> Console.ReadLine() ?? string.Empty;
    }
}