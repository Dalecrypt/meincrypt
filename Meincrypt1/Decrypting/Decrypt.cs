using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Meincrypt.Coloring;
using Meincrypt.OrbStuff;
using Meincrypt.Seeding;

namespace Meincrypt.Decrypting
{
    public class Decrypt
    {
        static public void DecryptFile(string inputFile, string outputFile)
        {
            inputFile = ("");
            System.Console.WriteLine("\r\nFilename to decrypt:\r");
            inputFile = Console.ReadLine();
            Console.WriteLine("\r\nSave decrypted file as:\r");
            outputFile = Console.ReadLine();

            Console.WriteLine("\r\nDid you remember your password? \n\r");
            string password = HideMe.ReadPassword();

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] key = SHA256.Create().ComputeHash(UE.GetBytes(Seeds.keySalt + password));
            byte[] iv = SHA256.Create().ComputeHash(UE.GetBytes(Seeds.ivSalt + password)).Take(16).ToArray();

            try
            {
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;

                try
                {
                    while ((data = cs.ReadByte()) != -1) fsOut.WriteByte((byte)data);
                    fsOut.Close();
                    cs.Close();
                    fsCrypt.Close();
                    Colors.SuccessText("\r\nSuccessfully decrypted " + "<" + inputFile + ">" + " as " + "[" + outputFile + "]" + "\n\rPress anykey..");
                    ClearToMain();
                }

                catch
                {
                    Colors.FailText("Did you enter the correct password?\n\r Press anykey..");
                    ClearToMain();
                }
            }
            catch
            {
                Colors.FailText("Does the file exist?\n\rPress anykey..");
                ClearToMain();
            }
        }

        private static void ClearToMain()
        {
            Console.ReadKey();
            Console.Clear();
            Program.Main();
        }
    }
}
