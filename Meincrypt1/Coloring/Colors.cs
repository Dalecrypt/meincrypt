using System;

namespace Meincrypt.Coloring
{
    public class Colors
    {
        public static void SuccessText(string greenWord)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(greenWord);
            Console.ResetColor();
        }

        public static void FailText(string redWord)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(redWord);
            Console.ResetColor();
        }
    }
}
