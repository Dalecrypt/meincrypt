using System;
using Meincrypt.Encrypting;
using Meincrypt.Decrypting;
using Meincrypt.Welcome;

namespace Meincrypt
{

    public class Program
    {

        public static void Main()
        {
            Splash.RunIntro();
            string one = "1";
            string two = "2";
            string three = "3";
            string selec = Console.ReadLine();

            string inputFile = "";
            string outputFile = "";

            if (selec.Equals(one))
                Encrypt.EncryptFile(inputFile, outputFile);
            else if (selec.Equals(two))
                Decrypt.DecryptFile(inputFile, outputFile);
            else if (selec.Equals(three))
                Environment.Exit(0);
            else
                Console.WriteLine("You can\'t read?\nPress anykey..");
            Console.ReadKey();
            Console.Clear();
            Main();
        }
    }
}
