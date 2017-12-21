using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Meincrypt.OrbStuff;
using Meincrypt.Coloring;
using Meincrypt.Seeding;

namespace Meincrypt.Encrypting
{
    public class Encrypt
    {

        static public void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                Console.WriteLine("\r\nChoose file to encrypt:\r");
                inputFile = Console.ReadLine();
                Console.WriteLine("\r\nSave encrypted file as:\r");
                outputFile = Console.ReadLine();
                Console.WriteLine("\r\nEnter a password and don\'t forget it!");
                string prepass = HideMe.ReadPassword();
                Console.WriteLine("\n\rPlease confirm your password");
                string password = HideMe.ReadPassword();
                if (password.Equals(prepass))
                {

                    UnicodeEncoding UE = new UnicodeEncoding();
                    byte[] key = SHA256.Create().ComputeHash(UE.GetBytes(Seeds.keySalt + password));
                    byte[] iv = SHA256.Create().ComputeHash(UE.GetBytes(Seeds.ivSalt + password)).Take(16).ToArray();

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

                    Colors.SuccessText("\r\nSuccessfully encrypted " + "<" + inputFile + ">" + " as " + "[" + outputFile + "]"
                        + "\n\rPress anykey..");
                    ClearTerminal.ClearToMain();
                }
                else
                {
                    Console.Clear();
                    Colors.FailText("PASSWORDS DO NOT MATCH!!! YOUR PASSWORD CAN NOT BE RECOVERED!\n\rIf you encrypt data and lose" +
                        " the password you will lose the data forever.\n\rTo prevent any confusion we will start over. No " +
                        "files have been changed. Press anykey to continue..");
                    ClearTerminal.ClearToMain();
                }
            }

            catch
            {
                Console.Clear();
                Colors.FailText("ERROR! Something went wrong\n\rPress anykey..");
                ClearTerminal.ClearToMain();
            }
        }
    }
}
