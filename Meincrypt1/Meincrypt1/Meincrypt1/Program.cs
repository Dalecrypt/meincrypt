using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Meincrypt
{

    public class Program
    {

        static void Main()
        {

            Console.Title = "Meincrypt -v0.0.2b | File Encryption";
            var arr = new[]
            {
                @"                   8 8          ,o.",
                @"___  ___     _    d8o8azzzzzzzzd   b       _",
                @"|  \/  |    (_)      —v0.0.1b—  `o'       | |",
                @"| .  . | ___ _ _ __   ___ _ __ _   _ _ __ | |_",
                @"| |\/| |/ _ \ | '_ \ / __| '__| | | | '_ \| __|",
                @"| |  | |  __/ | | | | (__| |  | |_| | |_) | |_",
                @"\_|  |_/\___|_|_| |_|\___|_|   \__, | .__/ \__|",
                @"                                __/ | |",
                @"                         —37—  |___/|_|"
            };
            string one = "1";
            string two = "2";
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("~MEINCRYPT-v0.0.2beta~ | Encrypt it yourself\n\r\n\r\n\rPress anykey..");
            Console.ReadKey();
            Console.WriteLine("\r\n1 - Encrypt\r\n2 - Decrypt\n\rUser selection:");
            string selec = Console.ReadLine();

            string inputFile = "";
            string outputFile = "";

            if (selec.Equals(one))
                EncryptFile(inputFile, outputFile);
            else if (selec.Equals(two))
                DecryptFile(inputFile, outputFile);
            else
                Console.WriteLine("You can\'t read?\nPress anykey..");
            Console.ReadKey();
            Console.Clear();
            Main();
        }

        public static readonly RandomNumberGenerator doRngStuff = RandomNumberGenerator.Create();
        public static readonly string keySalt = doRngStuff.ToString();
        public static readonly string ivSalt = doRngStuff.ToString();

        static private void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                inputFile = ("");
                Console.WriteLine("\r\nChoose file to encrypt:\r");
                inputFile = Console.ReadLine();
                Console.WriteLine("\r\nSave encrypted file as:\r");
                outputFile = Console.ReadLine();
                Console.WriteLine("\r\nEnter a password and don\'t forget it!");
                string prepass = Orb.App.Console.ReadPassword();
                Console.WriteLine("\n\rPlease confirm your password");
                string password = Orb.App.Console.ReadPassword();
                if (password.Equals(prepass))
                {                    
                    UnicodeEncoding UE = new UnicodeEncoding();
                    byte[] key = SHA256.Create().ComputeHash(UE.GetBytes(keySalt + password));
                    byte[] iv = SHA256.Create().ComputeHash(UE.GetBytes(ivSalt + password)).Take(16).ToArray();

                    string cryptFile = outputFile;
                    FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                    RijndaelManaged RMCrypto = new RijndaelManaged();

                    CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                    FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                    int data;
                    while ((data = fsIn.ReadByte()) != -1) cs.WriteByte((byte)data);

                    fsIn.Close();
                    cs.Close();
                    fsCrypt.Close();

                    SuccessText("\r\nSuccessfully encrypted " + "<" + inputFile + ">" + " as " + "[" + outputFile + "]" 
                        + "\n\rPress anykey to exit..");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    Console.Clear();
                    FailText("PASSWORDS DO NOT MATCH!!! YOUR PASSWORD CAN NOT BE RECOVERED!\n\rIf you encrypt data and lose" + 
                        " the password you will lose the data forever.\n\rTo prevent any confusion we will start over. No " + 
                        "files have been changed. Press anykey to continue..");
                    Console.ReadKey();
                    Console.Clear();
                    Main();
                }
            }

            catch
            {
                Console.Clear();
                FailText("FUCKING REKT! How the fuck did you get an ERROR?\n\rYou\'re bad at this!\rAnd you should feel bad \n\rPress anykey..");
                Console.ReadKey();
                Main();
            }
        }
              
        static private void DecryptFile(string inputFile, string outputFile)
        {
            inputFile = ("");
            Console.WriteLine("\r\nFilename to decrypt:\r");
            inputFile = Console.ReadLine();
            Console.WriteLine("\r\nSave decrypted file as:\r");
            outputFile = Console.ReadLine();

            Console.WriteLine("\r\nDid you remember your password? \n\r");
            string password = Orb.App.Console.ReadPassword();

            ////store in global variable
            //string keySalt = "X9CV786SDAFKIUSDYFGOIUY"; //enter random value
            //string ivSalt = "KJHbikjhgc976tcvxz8976rhdBdgs6"; //random value also

            UnicodeEncoding UE = new UnicodeEncoding();
            //byte[] key = UE.GetBytes(password);
            byte[] key = SHA256.Create().ComputeHash(UE.GetBytes(keySalt + password));
            byte[] iv = SHA256.Create().ComputeHash(UE.GetBytes(ivSalt + password)).Take(16).ToArray();

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

            RijndaelManaged RMCrypto = new RijndaelManaged();

            CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int data;
            while ((data = cs.ReadByte()) != -1) fsOut.WriteByte((byte)data);

            fsOut.Close();
            cs.Close();
            fsCrypt.Close();
            SuccessText("\r\nSuccessfully decrypted " + "<" + inputFile + ">" + " as " + "[" + outputFile + "]" + "\n\rPress anykey to exit..");
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void SuccessText(string greenWord)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(greenWord);
            Console.ResetColor();
        }

        static void FailText(string redWord)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(redWord);
            Console.ResetColor();
        }
    }
}

namespace Orb.App
{
    /// <summary>
    /// Method to hide typed password on console
    /// 
    /// </summary>
    static public class Console
    {
       
        public static string ReadPassword(char mask)
        {
            const int ENTER = 13, BACKSP = 8, CTRLBACKSP = 127;
            int[] FILTERED = { 0, 27, 9, 10 /*, 32 space, if you care */ }; // const

            var pass = new Stack<char>();
            char chr = (char)0;

            while ((chr = System.Console.ReadKey(true).KeyChar) != ENTER)
            {
                if (chr == BACKSP)
                {
                    if (pass.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        pass.Pop();
                    }
                }
                else if (chr == CTRLBACKSP)
                {
                    while (pass.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        pass.Pop();
                    }
                }
                else if (FILTERED.Count(x => chr == x) > 0) { }
                else
                {
                    pass.Push((char)chr);
                    System.Console.Write(mask);
                }
            }

            System.Console.WriteLine();

            return new string(pass.Reverse().ToArray());
        }

        /// <summary>
        /// Like System.Console.ReadLine(), only with a mask.
        /// </summary>
        /// <returns>the string the user typed in </returns>
        public static string ReadPassword()
        {
            return Orb.App.Console.ReadPassword('*');
        }
    }
}