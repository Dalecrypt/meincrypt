using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
