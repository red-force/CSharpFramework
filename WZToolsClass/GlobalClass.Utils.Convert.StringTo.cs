extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    public partial class GlobalClass
    {
        public partial class Utils
        {
            public partial class Convert
            {
                /// <summary>
                /// Convert String To
                /// </summary>
                public partial class StringTo
                {
                    /// <summary>
                    /// convert string to
                    /// </summary>
                    /// <param name="inStr"></param>
                    /// <param name="fromEncoding"></param>
                    /// <param name="toEncoding"></param>
                    /// <param name="usage"></param>
                    /// <returns></returns>
                    public static string Any(string inStr, string fromEncoding=null, string toEncoding = "utf-8", string usage = "MIME")
                    {
                        string outStr = inStr;
                        try
                        {
                            Encoding encForStream = rf.href.Utils.EncodingTools.GetMostEfficientEncodingForStream(inStr);
                            Encoding encForMime = rf.href.Utils.EncodingTools.GetMostEfficientEncoding(inStr);
                            Encoding encoding = Encoding.Default;
                            switch (usage)
                            {
                                case "Stream":
                                    encoding = encForStream;
                                    break;
                                case "MIME":
                                default:
                                    encoding = encForMime;
                                    break;
                            }
                            fromEncoding = fromEncoding ?? encoding.BodyName;
                            outStr = System.Text.Encoding.GetEncoding(toEncoding).GetString(System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding(fromEncoding), System.Text.Encoding.GetEncoding(toEncoding), System.Text.Encoding.GetEncoding(fromEncoding).GetBytes(inStr)));
                        }
                        catch (Exception ex) {
                        }
                        return outStr;
                    }
                    /// <summary>
                    /// convert utf8 encoding string to unicode encoding.
                    /// </summary>
                    /// <param name="utf8Str"></param>
                    /// <returns></returns>
                    public static string ConvertUTF8ToUnicode(string utf8Str)
                    {
                        string outStr = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.UTF8.GetBytes(utf8Str));
                        return outStr;
                    }

                    /// <summary>
                    /// convert unicode encoding string to utf8encoding.
                    /// </summary>
                    /// <param name="unicodeStr"></param>
                    /// <returns></returns>
                    public static string ConvertUnicodeToUTF8(string unicodeStr)
                    {
                        string outStr = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Unicode.GetBytes(unicodeStr));
                        return outStr;
                    }

                    /// <summary>
                    /// to UTF8
                    /// </summary>
                    /// <param name="intStr"></param>
                    /// <param name="usage">"MIME" or "Stream"</param>
                    /// <returns></returns>
                    public static string UTF8(string inStr, string usage = "MIME")
                    {
                        string outStr = string.Empty;
                        outStr = Any(inStr: inStr, toEncoding: Encoding.UTF8.BodyName, usage: usage);
                        return outStr;
                    }
  
                    /// <summary>
                    /// UNICODE字符转为中文   
                    /// 对这个方法做一点改进 使他支持中英混排  
                    /// </summary>
                    /// <param name="unicodeString"></param>
                    /// <returns></returns>
                    public static string ConvertUnicodeStringToChinese(string unicodeString)
                    {
                        if (string.IsNullOrEmpty(unicodeString))
                            return string.Empty;

                        string outStr = unicodeString;

                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("\\\\u[0123456789abcdef]{4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        System.Text.RegularExpressions.MatchCollection mc = re.Matches(unicodeString);
                        foreach (System.Text.RegularExpressions.Match ma in mc)
                        {
                            outStr = outStr.Replace(ma.Value, ConverUnicodeStringToChar(ma.Value).ToString());
                        }
                        return outStr;
                    }

                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="str"></param>
                    /// <returns></returns>
                    private static char ConverUnicodeStringToChar(string str)
                    {
                        char outStr = Char.MinValue;
                        outStr = (char)int.Parse(str.Remove(0, 2), System.Globalization.NumberStyles.HexNumber);
                        return outStr;
                    }

                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="asciiString"></param>
                    /// <returns></returns>
                    public string ConvertStringToHex(string asciiString)
                    {
                        string hex = "";
                        foreach (char c in asciiString)
                        {
                            int tmp = c;
                            hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                        }
                        return hex;
                    }

                    /// <summary>
                    /// 
                    /// </summary>
                    /// <param name="HexValue"></param>
                    /// <returns></returns>
                    public string ConvertHexToString(string HexValue)
                    {
                        string StrValue = "";
                        while (HexValue.Length > 0)
                        {
                            StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                            HexValue = HexValue.Substring(2, HexValue.Length - 2);
                        }
                        return StrValue;
                    }
                }
            }
        }
    }
}
