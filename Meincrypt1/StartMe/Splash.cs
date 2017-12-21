using System;

namespace Meincrypt.StartMe
{
    class Splash
    {
        public static void RunIntro()
        {
            Console.Title = "Meincrypt -v0.0.3b | File Encryption";
            var arr = new[]
            {
                @"                   8 8          ,o.",
                @"___  ___     _    d8o8azzzzzzzzd   b       _",
                @"|  \/  |    (_)      —v0.0.3b—  `o'       | |",
                @"| .  . | ___ _ _ __   ___ _ __ _   _ _ __ | |_",
                @"| |\/| |/ _ \ | '_ \ / __| '__| | | | '_ \| __|",
                @"| |  | |  __/ | | | | (__| |  | |_| | |_) | |_",
                @"\_|  |_/\___|_|_| |_|\___|_|   \__, | .__/ \__|",
                @"                                __/ | |",
                @"                         —37—  |___/|_|"
            };                        
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("~MEINCRYPT-v0.0.3beta~ | Encrypt it yourself\n\r\n\r\n\rPress anykey..");
            Console.WriteLine("\r\n1 - Encrypt\r\n2 - Decrypt\n\r3 - Exit Meincrypt\r\n\r\nUser selection:");
        }
    }
}
