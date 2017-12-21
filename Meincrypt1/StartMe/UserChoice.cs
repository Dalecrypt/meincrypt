using System;
using Meincrypt.Encrypting;
using Meincrypt.Terminal;

namespace Meincrypt.StartMe
{
    public class UserChoice
    {
        public static void Init(string x = "1", string y = "2", string z = "3", string inputFile = "", string outputFile = "")
        {
            string selec = Console.ReadLine();
            if (selec.Equals(x))
                Encrypt.EncryptFile(inputFile, outputFile);
            else if (selec.Equals(y))
                Decrypt.DecryptFile(inputFile, outputFile);
            else if (selec.Equals(z))
                Environment.Exit(0);
            else
                Console.WriteLine("You can\'t read?\nPress anykey..");
                ClearTerminal.ClearToMain();
        }
    }
}
