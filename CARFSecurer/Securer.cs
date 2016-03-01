extern alias mc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace RF
{
    public partial class GlobalClass
    {
        /// <summary>
        /// the securer
        /// </summary>
        public partial class Securer
        {
            /// <summary>
            /// DefaultSecureEncryption
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string securerEncryption16_3(string text)
            {
                /*
		            efbbbf20202020202020202020202020202020202a203020312032203320
		            34203520362037203820392041204220432044204520460d0a2020202020
		            2020202020202020202020202a2061206220632064206520662067206820
		            69206a206b206c206d0d0a20202020202020202020202020202020202a20
		            6e206f2070207120722073207420752076207720782079207a0d0a
                 * */
                String result = String.Empty;
                result = String.Join("", text.Select(delegate(char c)
                {
                    char _r = ' ';
                    #region Dictionary<char, char>
                    Dictionary<char, char> _dcc = new Dictionary<char, char>(){
                        {'0','A'},
                        {'1','B'},
                        {'2','C'},
                        {'3','D'},
                        {'4','E'},
                        {'5','F'},
                        {'6','G'},
                        {'7','H'},
                        {'8','I'},
                        {'9','J'},
                        {'a','N'},
                        {'b','O'},
                        {'c','P'},
                        {'d','Q'},
                        {'e','R'},
                        {'f','S'},
                        {'g','T'},
                        {'h','U'},
                        {'i','V'},
                        {'j','W'},
                        {'k','X'},
                        {'l','Y'},
                        {'m','Z'},
                        {'n',')'},
                        {'o','!'},
                        {'p','@'},
                        {'q','#'},
                        {'r','$'},
                        {'s','%'},
                        {'t','^'},
                        {'u','&'},
                        {'v','*'},
                        {'w','('},
                        {'x','K'},
                        {'y','L'},
                        {'z','M'},
                        {'A','0'},
                        {'B','1'},
                        {'C','2'},
                        {'D','3'},
                        {'E','4'},
                        {'F','5'},
                        {'G','6'},
                        {'H','7'},
                        {'I','8'},
                        {'J','9'},
                        {'K','x'},
                        {'L','y'},
                        {'M','z'},
                        {'N','a'},
                        {'O','b'},
                        {'P','c'},
                        {'Q','d'},
                        {'R','e'},
                        {'S','f'},
                        {'T','g'},
                        {'U','h'},
                        {'V','i'},
                        {'W','j'},
                        {'X','k'},
                        {'Y','l'},
                        {'Z','m'},
                        {')','n'},
                        {'!','o'},
                        {'@','p'},
                        {'#','q'},
                        {'$','r'},
                        {'%','s'},
                        {'^','t'},
                        {'&','u'},
                        {'*','v'},
                        {'(','w'},
                    };
                    #endregion
                    _r = _dcc.TryGetValue(c, out _r) ? _r : _r;
                    return _r.ToString();
                }).ToArray());
                return result;
            }

            public static string securerEncryption16_3_a(string text)
            {/*
		            efbbbf20202020202020202020202020202020202a203020312032203320
		            34203520362037203820392041204220432044204520460d0a2020202020
		            2020202020202020202020202a2061206220632064206520662067206820
		            69206a206b206c206d0d0a20202020202020202020202020202020202a20
		            6e206f2070207120722073207420752076207720782079207a0d0a
                 * */
                String result = String.Empty;
                result = String.Join("", text.Select(delegate(char c, int idx)
                {
                    char _r = ' ';

                    #region Dictionary<char, char>
                    Dictionary<char, char> _dcc = new Dictionary<char, char>(){
                        {'0','A'},
                        {'1','B'},
                        {'2','C'},
                        {'3','D'},
                        {'4','E'},
                        {'5','F'},
                        {'6','G'},
                        {'7','H'},
                        {'8','I'},
                        {'9','J'},
                        {'a','N'},
                        {'b','O'},
                        {'c','P'},
                        {'d','Q'},
                        {'e','R'},
                        {'f','S'},
                        {'g','T'},
                        {'h','U'},
                        {'i','V'},
                        {'j','W'},
                        {'k','X'},
                        {'l','Y'},
                        {'m','Z'},
                        {'n',')'},
                        {'o','!'},
                        {'p','@'},
                        {'q','#'},
                        {'r','$'},
                        {'s','%'},
                        {'t','^'},
                        {'u','&'},
                        {'v','*'},
                        {'w','('},
                        {'x','K'},
                        {'y','L'},
                        {'z','M'},
                        {'A','0'},
                        {'B','1'},
                        {'C','2'},
                        {'D','3'},
                        {'E','4'},
                        {'F','5'},
                        {'G','6'},
                        {'H','7'},
                        {'I','8'},
                        {'J','9'},
                        {'K','x'},
                        {'L','y'},
                        {'M','z'},
                        {'N','a'},
                        {'O','b'},
                        {'P','c'},
                        {'Q','d'},
                        {'R','e'},
                        {'S','f'},
                        {'T','g'},
                        {'U','h'},
                        {'V','i'},
                        {'W','j'},
                        {'X','k'},
                        {'Y','l'},
                        {'Z','m'},
                        {')','n'},
                        {'!','o'},
                        {'@','p'},
                        {'#','q'},
                        {'$','r'},
                        {'%','s'},
                        {'^','t'},
                        {'&','u'},
                        {'*','v'},
                        {'(','w'},
                    };
                    #endregion

                    #region Dictionary<char, char> version 2
                    Dictionary<char, char> _dcc2 = new Dictionary<char, char>(){
                        {'0','N'},
                        {'1','O'},
                        {'2','P'},
                        {'3','Q'},
                        {'4','R'},
                        {'5','S'},
                        {'6','T'},
                        {'7','U'},
                        {'8','V'},
                        {'9','W'},
                        {'a','A'},
                        {'b','B'},
                        {'c','C'},
                        {'d','D'},
                        {'e','E'},
                        {'f','F'},
                        {'g','G'},
                        {'h','H'},
                        {'i','I'},
                        {'j','J'},
                        {'k','Z'},
                        {'l','X'},
                        {'m','Y'},
                        {'n','K'},
                        {'o','L'},
                        {'p','M'},
                        {'q','x'},
                        {'r','y'},
                        {'s','z'},
                        {'t','a'},
                        {'u','b'},
                        {'v','c'},
                        {'w','d'},
                        {'x',')'},
                        {'y','!'},
                        {'z','@'},
                        {'A','#'},
                        {'B','$'},
                        {'C','%'},
                        {'D','^'},
                        {'E','&'},
                        {'F','*'},
                        {'G','('},
                        {'H','e'},
                        {'I','f'},
                        {'J','g'},
                        {'K','0'},
                        {'L','1'},
                        {'M','2'},
                        {'N','3'},
                        {'O','4'},
                        {'P','5'},
                        {'Q','n'},
                        {'R','o'},
                        {'S','p'},
                        {'T','q'},
                        {'U','r'},
                        {'V','s'},
                        {'W','t'},
                        {'X','u'},
                        {'Y','v'},
                        {'Z','w'},
                        {')','6'},
                        {'!','7'},
                        {'@','8'},
                        {'#','9'},
                        {'$','h'},
                        {'%','i'},
                        {'^','j'},
                        {'&','k'},
                        {'*','l'},
                        {'(','m'},
                    };
                    #endregion

                    if (idx % 2 == 1)
                    {
                        _r = _dcc.TryGetValue(c, out _r) ? _r : _r;
                    }
                    else
                    {
                        _r = _dcc2.TryGetValue(c, out _r) ? _r : _r;
                    }

                    return _r.ToString();
                }).ToArray());
                return result;
            }

            public static string securerEncryption16_3_b(string text)
            {
                text = mc.RF.GlobalClass.Utils.DB.MD5(strPwd: text);
                return securerEncryption16_3_a(text: text);
                //text.ToCharArray().Select(delegate(char _c)
                //{
                //    return mc.RF.GlobalClass.Utils.Convert.StringToHexadecimal(_c.ToString(), prefix: "");
                //}).ToArray();
            }

            public static string securerEncryption_t(string text)
            {
                string result = text;
                int i = 0;
                while(i++<300){
                    try
                    {
                        int value = Int32.TryParse(String.Format("{0:X}", i),System.Globalization.NumberStyles.HexNumber, null, out value)? value: value;
                        Console.Write(" " + Char.ConvertToUtf32(Char.ConvertFromUtf32(i).ToString(), 0) 
                            + ":" + Char.ConvertFromUtf32(i) 
                            + ":" + String.Format("0x{0:X}", i) 
                            + ":" + value);
                        if (0 == i % 20)
                        {
                            Console.WriteLine();
                        }
                        else { }
                    }
                    catch (Exception ex) { }
                }

                return result;
            }
            
            //默认密钥向量
            private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            
            public static string securerEncryption_des(string text, string key = "WangZhi")
            {
                string result = text;
                try
                {
                    byte[] rgbkey = Encoding.UTF8.GetBytes(key.PadRight(8,'v').Substring(0,8));
                    byte[] rgbIV = Keys;
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(text);
                    System.Security.Cryptography.DESCryptoServiceProvider dCSP = new System.Security.Cryptography.DESCryptoServiceProvider();

                    using (MemoryStream mStream = new MemoryStream())
                    {
                        CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey: rgbkey, rgbIV: rgbIV), CryptoStreamMode.Write);
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();
                        result = Convert.ToBase64String(mStream.ToArray());
                    }
                }
                catch (Exception ex)
                {
                }
                return result;
            }

            public static string EncryptionOG_D_N(string text)
            {
                return securerEncryption16_3_a(text: text);
            }

            public static string EncryptionQADHOAEBPEUISDTAPJOPNGOERINRVF(string text)
            {
                return securerEncryption16_3_b(text: text);
            }

            public static string EncryptionMD5(string text)
            {
                return mc.RF.GlobalClass.Utils.DB.MD5(strPwd: text);
                //text.ToCharArray().Select(delegate(char _c)
                //{
                //    return mc.RF.GlobalClass.Utils.Convert.StringToHexadecimal(_c.ToString(), prefix: "");
                //}).ToArray();
            }

            public static string EncryptionDES(string text, string key = "WangZhi")
            {
                return securerEncryption_des(text: text, key: key);
            }

            public const string EncryptionNamePrefix = "Encryption";

            public enum Names
            {
                OG_D_N = 0,
                QADHOAEBPEUISDTAPJOPNGOERINRVF = 1,
                MD5 = 2,
                DES = 3
            };
        }
    }
}
