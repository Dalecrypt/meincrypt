﻿using System;

namespace Meincrypt
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
