extern alias mc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CARFSecurer
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (null != args && 0 == args.Length)
            {
                Console.WriteLine("Hi this is securer, is there anything that can I do for you?");
                startTheGuider();
            }else if(1 == args.Length){
                Console.WriteLine("Help:");
            }else {
                int argsLength = args.Length;
                string argName = String.Empty;
                string argValue = String.Empty;
                String encryptionName = String.Empty;
                String stringToEncryting = String.Empty;
                while (argsLength-- > 1)
                {
                    argValue = args[argsLength];
                    argName = args[--argsLength];
                    switch (argName)
                    {
                        case "-n":
                            encryptionName = argValue;
                            break;
                        case "-m":
                            stringToEncryting = argValue;
                            break;
                        default: break;
                    }
                }
                Console.WriteLine(GetEncrytedMessage(encrytionName: encryptionName, originMessage: stringToEncryting));
            }
            // Console.ReadLine();
        }

        private static void startTheGuider()
        {
            string toDoCMD = Console.ReadLine();
            switch (toDoCMD)
            {
                case "encrypt":
                    Console.Write("Encryption Name:");
                    string encryptionName = Console.ReadLine();
                    Console.WriteLine("String to encrypt");
                    string stringToEncryting = Console.ReadLine();
                    Console.WriteLine(GetEncrytedMessage(encrytionName: encryptionName, originMessage: stringToEncryting));
                    Console.ReadKey();
                    break;
                case "QRP$L@^":
                    Console.Write(":");
                    string decryptionName = Console.ReadLine();
                    Console.Write(":");
                    string stringToDecrypting = Console.ReadLine();
                    Console.Write(":");
                    string keyForDecryption = Console.ReadLine();
                    if ("" == keyForDecryption)
                    {
                        Console.WriteLine(GetDecryptedMessage(decryptionName: decryptionName, stringToDecrypting: stringToDecrypting));
                    }
                    else
                    {
                        Console.WriteLine(GetDecryptedMessage(decryptionName: decryptionName, stringToDecrypting: stringToDecrypting, keyForDecryption: keyForDecryption));
                    }
                    Console.ReadKey();
                    break;
                case "q": 
                    break;
                default:
                    Console.WriteLine("Pardon ? (q to quit)");
                    restartTheGuider();
                    break;
            }
        }

        private static void restartTheGuider()
        {
            startTheGuider();
            return;
        }

        public static string GetEncrytedMessage(string encrytionName, string originMessage, string encryptionKey = "WangZhi")
        {
            string result = String.Empty;


            switch (encrytionName)
            {
                case "16_3_a":
                    result = RF.GlobalClass.Securer.securerEncryption16_3_a(originMessage);
                    break;
                case "16_3_b":
                    result = RF.GlobalClass.Securer.securerEncryption16_3_b(originMessage);
                    break;
                case "t":
                    result = RF.GlobalClass.Securer.securerEncryption_t("");
                    break;
                case "16_3":
                    result = RF.GlobalClass.Securer.securerEncryption16_3(originMessage);
                    break;
                case "DES":
                case "des":
                    result = RF.GlobalClass.Securer.securerEncryption_des(originMessage, encryptionKey);
                    break;
                default:
                    if (Enum.GetNames(typeof(RF.GlobalClass.Securer.Names)).Contains(encrytionName))
                    {
                        result = (String)mc.RF.GlobalClass.Utils.Do.dynamicallyCallClassMethod(typeofClass: typeof(RF.GlobalClass.Securer), methodName: RF.GlobalClass.Securer.EncryptionNamePrefix + encrytionName, args: new object[] { originMessage });
                    }
                    else { }
                    break;
            }
            return result;
        }

        public static string GetDecryptedMessage(string decryptionName, string stringToDecrypting, string keyForDecryption = "WangZhi")
        {
            string result = String.Empty;

            try
            {
                switch (decryptionName)
                {
                    case "des":
                        result = CARFSecurer.Decryption.DES(text: stringToDecrypting, key: keyForDecryption);
                        break;
                    default:
                        /* do not public all
                        if (Enum.GetNames(typeof(CARFSecurer.Decryption.Names)).Contains(decryptionName))
                        {
                            result = (String)mc.RF.GlobalClass.Utils.Do.dynamicallyCallClassMethod(typeofClass: typeof(CARFSecurer.Decryption), methodName: CARFSecurer.Decryption.DecryptionNamePrefix + decryptionName, args: new object[] { stringToDecrypting, keyForDecryption });
                        }
                        else { }
                         * */
                        break;
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        private static string _GetDecryptedMessage(string decryptionName, string stringToDecrypting, string keyForDecryption = "WangZhi")
        {
            string result = String.Empty;

            try
            {
                switch (decryptionName)
                {
                    case "des":
                        result = CARFSecurer.Decryption.DES(text: stringToDecrypting, key: keyForDecryption);
                        break;
                    default:
                        if (Enum.GetNames(typeof(CARFSecurer.Decryption.Names)).Contains(decryptionName))
                        {
                            result = (String)mc.RF.GlobalClass.Utils.Do.dynamicallyCallClassMethod(typeofClass: typeof(CARFSecurer.Decryption), methodName: CARFSecurer.Decryption.DecryptionNamePrefix + decryptionName, args: new object[] { stringToDecrypting, keyForDecryption });
                        }
                        else { }
                        break;
                }
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}
