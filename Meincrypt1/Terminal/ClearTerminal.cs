using System;

namespace Meincrypt.Terminal
{
    public class ClearTerminal
    {
        public static void ClearToMain()
        {
            Console.ReadKey();
            Console.Clear();
            Program.Main();
        }
    }
}
