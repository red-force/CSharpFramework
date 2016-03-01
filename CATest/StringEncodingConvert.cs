using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CATest
{
    class StringEncodingConvert
    {
        public static void main()
        {
            Console.WriteLine("Please in put the uft8 string");
            string read = Console.ReadLine();
            Console.WriteLine("the Unicode string convert from UTF8 is:");
            string unicodeStr = ConvertUTF8ToUnicode(read);
            Console.WriteLine(unicodeStr);
            Console.WriteLine("the UTF8 string convert from Unicode is:");
            string utf8Str = ConvertUnicodeToUTF8(unicodeStr);
            Console.WriteLine(utf8Str);
            Console.Write("Press Enter to Exit.");
            Console.ReadLine();
        }

        public static string ConvertUTF8ToUnicode(string str)
        {
            string outStr = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.UTF8.GetBytes(str));
            return outStr;
        }

        public static string ConvertUnicodeToUTF8(string str)
        {
            string outStr = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Unicode.GetBytes(str));
            return outStr;
        }

    }
}
