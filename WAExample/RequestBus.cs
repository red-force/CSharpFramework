using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WAExample
{
    public partial class RequestBus
    {

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="Url">路径</param>
        /// <param name="postDataStr">get参数</param>
        /// <param name="cookie"></param>
        /// <returns>源码</returns>
        public string SendDataByGET(string Url, string postDataStr, ref CookieContainer cookie)
        {
            string resultStr = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
                request.Headers.Add("Cookie", HttpContext.Current.Request.Headers["Cookie"]);
                request.Method = "GET";
                request.ContentType = "text/plain;charset=utf-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                resultStr = retString;
            }
            catch (System.Net.WebException we)
            {
                resultStr = we.Message;
            }
            return resultStr;
        }

        /// <summary>
        /// 获取asp页面返回的数据并存入session
        /// </summary>
        public void SetSession()
        {
            //try
            //{
            //    HttpContext.Current.Session["user"].ToString();
            //}
            //catch
            if (HttpContext.Current.Session["user"] == null)
            {
                string url = System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeGetSessionURL") ?? System.Configuration.ConfigurationManager.AppSettings.Get("rfoa1.sessions") ?? Properties.Settings.Default.GetSessionURL ?? @"http://localhost/rfoanew/getsessions.asp";
                string postDataStr = "";
                CookieContainer mycook = new CookieContainer();
                string str = string.Empty;
                // GetSession getsession = new GetSession();
                str = SendDataByGET(url, postDataStr, ref mycook).ToString();
                string str1 = "{}";
                if (str.IndexOf("{") != -1)
                {
                    //根据返回源文件获取json串
                    str1 = str.Substring(str.IndexOf("{"), str.LastIndexOf("}") - str.IndexOf("{") + 1);
                }
                else
                {
                    // str1 = "{}";
                }
#if ! DEBUG
                if (str1 == "{}" /*|| str1.IndexOf("userid") == -1*/)
                {
                    //登陆超时,返回登陆界面

                    HttpContext.Current.Response.SetCookie(new HttpCookie("WebAppOfficeLoginPageURL", (System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeLoginPageURL") ?? System.Configuration.ConfigurationManager.AppSettings.Get("rfoa1.login") ?? System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeLoginPageURL") ?? Properties.Settings.Default.LoginPageURL ?? "http://localhost/rfoanew/login.asp")));
                    HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeGotoLoginPageURL") ?? System.Configuration.ConfigurationManager.AppSettings.Get("rfoa1.login") ?? Properties.Settings.Default.LoginPageURL ?? "http://localhost/rfoanew/login.asp", true); // WALogin.aspx
                    //HttpContext.Current.Response.Write("<script>alert('" + url + "')</script>");
                    //HttpContext.Current.Response.Write("<script>alert('" + postDataStr + "')</script>");
                    //HttpContext.Current.Response.Write("<script>alert('" + str + "')</script>");
                    //HttpContext.Current.Response.Write("<script>alert('" + str1 + "')</script>");
                    // HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeLoginPageURL") ?? Properties.Settings.Default.LoginPageURL ?? "http://localhost/rfoanew/login.asp", true);


                }
                else
                {
                    //登陆成功
                    Encoding utf8 = Encoding.GetEncoding(65001);
                    Encoding gb2312 = Encoding.GetEncoding("gb2312");
                    byte[] temp = utf8.GetBytes(str1);
                    byte[] temp1 = Encoding.Convert(utf8, gb2312, temp);
                    string result = gb2312.GetString(temp1);
                    HttpContext.Current.Session["user"] = result;

                }
#else
                HttpContext.Current.Session["user"] = @"{""GetCode"":""4187"",""pwd"":""RedForce47"",""dptdes"":""ResearchCenter"",""depid"":""system"",""userid"":""47"",""empid"":""47"",""dptid"":""system"",""empdes"":""will"",""login_name"":""system"",""lasttime"":""201603141259  "",""cont"":""131"",""isstp"":""n"",""idno"":""510******"",""log"":""rfzb610041""}";
                //{"GetCode":"4317","pwd":"RedForce47","dptdes":"ResearchCenter","depid":"system","userid":"47","empid":"47","dptid":"will","empdes":"will","login_name":"system","lasttime":"201504241022  ","cont":"191","isstp":"n","idno":"510108198706091833","log":"rfzb610041"}
#endif
            }
            else { }
        }


        #region for Get SingName of each interface.


        #endregion

        /// <summary>
        /// 获取日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间对象</param>
        /// <returns>日期时间字符串：2014-10-21 08:33</returns>
        public static string GetDateTimeString(DateTime dateTime)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("zh-CN");
            System.Globalization.DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.DateSeparator = "-";
            string resultStr = dateTime.ToString("G", dtfi) ?? dateTime.ToShortDateString() + " " + dateTime.ToLongTimeString();
            return resultStr;
        }

        /// <summary>
        /// Get Date String
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetDateString(string p, string format = "D")
        {
            p = (p ?? "").Trim();
            DateTime dateTime = new DateTime(); int y = 0;
            int m = 0;
            int d = 0;
            if (p.Length == 8)
            {
                if (int.TryParse(p.Substring(0, 4), out y) && int.TryParse(p.Substring(4, 2), out m) && int.TryParse(p.Substring(6, 2), out d))
                {
                    dateTime = new DateTime(y, m, d);
                }
                else { }
            }
            else if (10 == p.Length)
            {
                if (int.TryParse(p.Substring(0, 4), out y) && int.TryParse(p.Substring(5, 2), out m) && int.TryParse(p.Substring(8, 2), out d))
                {
                    dateTime = new DateTime(y, m, d);
                }
                else { }
            }
            else { }
            string resultStr = RF.GlobalClass.Utils.DateTime.GetDateString(dateTime, format: format);
            return resultStr;
        }

        /// <summary>
        /// JsonConvert DeserializeObject
        /// </summary>
        /// <param name="p">jsonText</param>
        /// <param name="type">Type</param>
        /// <param name="mode">object/array/List<Dictionary<string,string>>/Dictionary<string,string></param>
        /// <returns></returns>
        internal static object JsonConvertDeserializeObject(string p, Type type = null, String mode = "object")
        {
            Object result = null;
            switch (mode.Replace(" ", ""))
            {
                case "List<Dictionary<string,string>>":

                    //result = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(p);
                    result = RF.GlobalClass.Utils.Convert.JSONToObject(p, type: typeof(List<Dictionary<string, string>>), mode: "array");
                    break;
                case "Dictionary<string,string>":
                    //result = JsonConvert.DeserializeObject<Dictionary<string, string>>(p);
                    result = RF.GlobalClass.Utils.Convert.JSONToObject(p, type: typeof(Dictionary<string, string>));
                    break;
                case "object":
                case "array":
                    if (null != type)
                    {
                        if ("object" == mode)
                        {
                            p = Regex.Replace(p, "^\\[|\\]$", "");
#if DEBUG
                            p = Regex.Replace(p, @"\""", @"'");
#endif
                        }
                        // result = JsonConvert.DeserializeObject(p, type);
                        result = RF.GlobalClass.Utils.Convert.JSONToObject(p, type: type);
                    }
                    break;
                default:
                    break;
            }

            return result;

        }

        internal static double getInternetExplorerVersion(HttpRequest Request)
        {
            // Returns the version of Internet Explorer or a -1
            // (indicating the use of another browser).
            float rv = -1;
            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            if (browser.Browser == "IE")
                rv = (float)(browser.MajorVersion + browser.MinorVersion);
            return rv;
        }

        /// <summary>
        /// get data passing by post
        /// </summary>
        /// <param name="Request">HttpContext.Current.Request</param>
        /// <returns></returns>
        /// <example>
        /// Dictionary<string, string> dss = RequestBus.GetPostData(Request: HttpContext.Current.Request);
        /// </example>
        public static Dictionary<string, string> GetPostData(HttpRequest Request)
        {
            Dictionary<string, string> dss = new Dictionary<string, string>();
            try
            {
                string data = RF.GlobalClass.WebForm.getPostData(Request);
                dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<string, string>));
                if (null != dss && dss.ContainsKey("data"))
                {
                    data = System.Web.HttpUtility.UrlDecode(dss["data"], System.Text.Encoding.UTF8);
                    dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<string, string>));
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            return dss;
        }

        /// <summary>
        /// get data passing by post
        /// </summary>
        /// <param name="Request">HttpContext.Current.Request</param>
        /// <param name="type">typeof(Dictionary&lt;string, string&gt;)</param>
        /// <returns></returns>
        public static object GetPostData(HttpRequest Request, Type type)
        {
            type = null == type ? typeof(Dictionary<string, string>) : type;
            Object obj = new Object();
            try
            {
                string data = RF.GlobalClass.WebForm.getPostData(Request);
                Dictionary<string, string> dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<string, string>));
                if (null != dss && dss.TryGetValue("data", out data))
                {
                    data = System.Web.HttpUtility.UrlDecode(data, System.Text.Encoding.UTF8);
                    obj = RF.GlobalClass.Utils.Convert.JSONToObject(data, type: type);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            return obj;
        }

        /// <summary>
        /// get data passing by post
        /// </summary>
        /// <typeparam name="T">typeof the data</typeparam>
        /// <param name="Request">HttpContext.Current.Request</param>
        /// <returns></returns>
        public static T GetPostData<T>(HttpRequest Request)
        {
            T obj = default(T);
            try
            {
                string data = RF.GlobalClass.WebForm.getPostData(Request);
                Dictionary<string, string> dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<string, string>));
                if (null != dss && dss.TryGetValue("data", out data))
                {
                    data = System.Web.HttpUtility.UrlDecode(data, System.Text.Encoding.UTF8);
                    obj = (T)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(T));
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }
            return obj;
        }

        /// <summary>
        /// Get data passed by get.
        /// </summary>
        /// <param name="Request">HttpContext.Current.Request</param>
        /// <returns></returns>
        /// <example>
        /// Dictionary<string, string> dss = RequestBus.GetPostData(Request: HttpContext.Current.Request);
        /// </example>
        public static Dictionary<string, string> GetPutData(HttpRequest Request)
        {
            Dictionary<string, string> dss = new Dictionary<string, string>();
            try
            {

                System.Collections.Specialized.NameValueCollection nvc = HttpUtility.ParseQueryString(Request.Url.Query.ToString());
                nvc.AllKeys.Select(delegate(string name, int idx)
                {
                    dss[name] = nvc[idx];
                    return name;
                }).ToArray();
            }
            catch (Exception ex)
            {
            }
            return dss;
        }

        public static Dictionary<string, string> GetPutData(string data)
        {
            Dictionary<string, string> dss = new Dictionary<string, string>();
            try
            {
                data = HttpUtility.UrlDecode(data);
                dss = RF.GlobalClass.Utils.Convert.JSONToObject(data, type: (typeof(Dictionary<string, string>))) as Dictionary<string, string>;
            }
            catch (Exception ex)
            {
            }
            return dss;
        }

        /// <summary>
        /// Convert resultMsg[propertyName] to user defined object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T ConvertResultMsgProperty<T>(object obj)
        {
            string objStr = String.Empty;
            T t = default(T);
            if (null != obj)
            {
                Type ty = obj.GetType();
                try
                {
                    if (ty != typeof(String))
                    {
                        objStr = RF.GlobalClass.Utils.Convert.ObjectToJSON(obj);
                    }
                    else
                    {
                        objStr = (String)obj;
                    }
                    t = RF.GlobalClass.Utils.Convert.JSONToObject<T>(objStr);
                }
                catch (Exception ex)
                {

                }
            }
            else { }
            return t;
        }


        /// <summary>
        /// Convert resultMsg[propertyName] to List&ltDictionary&ltstring, string&gt&gt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> ConvertResultMsgProperty(object obj, string propertyName = "Obj")
        {
            string objStr = String.Empty;
            List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
            if (null != obj)
            {
                try
                {
                    if (obj.GetType() != typeof(String))
                    {
                        objStr = RF.GlobalClass.Utils.Convert.ObjectToJSON(obj);
                    }
                    else
                    {
                        objStr = (String)obj;
                    }
                    ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: (typeof(List<Dictionary<string, string>>)), mode: "array");
                }
                catch (Exception ex)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    try
                    {
                        dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: typeof(Dictionary<string, string>));
                        if (dss.TryGetValue(propertyName, out objStr))
                        {
                            try
                            {
                                ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: (typeof(List<Dictionary<string, string>>)), mode: "array");
                            }
                            catch (Exception ex2) { }
                        }
                        else { }
                    }
                    catch (Exception ex3)
                    {
                        try
                        {
                            ((Dictionary<string, List<Dictionary<string, string>>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: typeof(Dictionary<string, List<Dictionary<string, string>>>))).TryGetValue(propertyName, out ldss);
                        }
                        catch (Exception ex4) {
                            ldss = new List<Dictionary<string, string>>() { new Dictionary<String, String>() { { "value", objStr } } };
                        }
                    }
                }
            }
            else { }
            return ldss;
        }

        /// <summary>
        /// Convert resultMsg\.Obj to List&ltDictionary&ltstring, string&gt&gt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> ConvertResultMsgObj(object obj)
        {
            return ConvertResultMsgProperty(obj, propertyName: "Obj");
            /*
            string objStr = String.Empty;
            List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
            if (null != obj)
            {
                try
                {
                    if (obj.GetType() != typeof(String))
                    {
                        objStr = RF.GlobalClass.Utils.Convert.ObjectToJSON(obj);
                    }
                    else {
                        objStr = (String)obj;
                    }
                    ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: (typeof(List<Dictionary<string, string>>)), mode: "array");
                }
                catch (Exception ex)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    try
                    {
                        dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: typeof(Dictionary<string, string>));
                        if (dss.TryGetValue("Obj", out objStr))
                        {
                            try
                            {
                                ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: (typeof(List<Dictionary<string, string>>)), mode: "array");
                            }
                            catch (Exception ex2) { }
                        }
                        else { }
                    }
                    catch (Exception ex3)
                    {
                        ((Dictionary<string, List<Dictionary<string, string>>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: typeof(Dictionary<string, List<Dictionary<string, string>>>))).TryGetValue("Obj", out ldss);
                    }
                }
            }
            else { }
            return ldss;
             * */
        }
        /// <summary>
        /// Convert resultMsg\.Obj to List&ltDictionary&ltstring, string&gt&gt
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> ConvertResultMsgObjj(object obj)
        {
            return ConvertResultMsgProperty(obj, propertyName: "Objj");
            /*
             * string objStr = String.Empty;
            List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
            if (null != obj)
            {
                try
                {
                    if (obj.GetType() != typeof(String))
                    {
                        objStr = RF.GlobalClass.Utils.Convert.ObjectToJSON(obj);
                    }
                    else
                    {
                        objStr = (String)obj;
                    }
                    ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: (typeof(List<Dictionary<string, string>>)), mode: "array");
                }
                catch (Exception ex)
                {
                    Dictionary<string, string> dss = new Dictionary<string, string>();
                    try
                    {
                        dss = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: typeof(Dictionary<string, string>));
                        if (dss.TryGetValue("Objj", out objStr))
                        {
                            try
                            {
                                ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: (typeof(List<Dictionary<string, string>>)), mode: "array");
                            }
                            catch (Exception ex2) { }
                        }
                        else { }
                    }
                    catch (Exception ex3)
                    {
                        ((Dictionary<string, List<Dictionary<string, string>>>)RF.GlobalClass.Utils.Convert.JSONToObject(objStr, type: typeof(Dictionary<string, List<Dictionary<string, string>>>))).TryGetValue("Objj",out ldss);
                    }
                }
            }
            else { }
            return ldss;
             * */
        }

        /// <summary>
        /// Get Page Num according to the param and post data
        /// </summary>
        /// <param name="currPageNumStr">[0-9+-]|First|Last|Prev|Next</param>
        /// <param name="dss">the data from post data</param>
        /// <param name="currPageNumKeyName">the key name of currPageNum in dss</param>
        /// <param name="pageTotalCountKeyName">the key name of pageTotalCount in dss</param>
        /// <returns></returns> 
        public static string GetPageNum(string currPageNumStr, Dictionary<string, string> dss, string currPageNumKeyName = "currPageNum", string pageTotalCountKeyName = "pageTotalCount")
        {
            string result = "1";
            try
            {
                if (null != dss && typeof(Dictionary<string, string>) == dss.GetType())
                {
                    #region get Page Num
                    string tmpNowPage = (dss.TryGetValue(currPageNumKeyName, out tmpNowPage) ? tmpNowPage : "1");
                    int tmpNowPageNum = int.TryParse((tmpNowPage ?? String.Empty).Trim(), out tmpNowPageNum) ? tmpNowPageNum : 1;
                    tmpNowPageNum = (tmpNowPageNum < 1 ? 1 : tmpNowPageNum);
                    int nowPageNum = int.TryParse((currPageNumStr ?? String.Empty).Trim(), out nowPageNum) ? nowPageNum : tmpNowPageNum;
                    nowPageNum = (nowPageNum < 1 ? 1 : nowPageNum);
                    string lastPage = dss.TryGetValue(pageTotalCountKeyName, out lastPage) ? lastPage : currPageNumStr;
                    int lastPageNum = int.TryParse((lastPage ?? String.Empty).Trim(), out lastPageNum) ? lastPageNum : nowPageNum;
                    switch (currPageNumStr)
                    {
                        case "Next":
                        case "+":
                            currPageNumStr = (((tmpNowPageNum + 1) > lastPageNum) ? lastPageNum : (tmpNowPageNum + 1)).ToString(); break;
                        case "Prev":
                        case "-":
                            currPageNumStr = (--tmpNowPageNum > 0 ? tmpNowPageNum : 1).ToString(); break;
                        case "First":
                            currPageNumStr = "1"; break;
                        case "Last":
                            currPageNumStr = lastPage; break;
                        default:
                            currPageNumStr = (nowPageNum > lastPageNum ? lastPageNum : nowPageNum).ToString();
                            break;
                    }
                    result = currPageNumStr;
                    #endregion
                }
                else { }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// get query param with inner key name.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Param(string key, string value = null)
        {
            string result = String.Empty;
            string prefix = "rf";
            if (null == value)
            {
                result = prefix + key;
            }
            else
            {
                result = prefix + key + "=" + value;
            }
            return result;
        }

        /// <summary>
        /// Get URL according to request and search string
        /// </summary>
        /// <param name="Request">HttpRequest</param>
        /// <param name="search">the search string of window.location</param>
        /// <returns></returns>
        public static string GetURL(HttpRequest Request, string search)
        {
            string result = String.Empty;
            try
            {
                result = Request.Url.AbsolutePath + "?" + HttpUtility.UrlEncode(search.TrimStart('?'));
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static void ChangeKeyNameOfDSS(string p, string p_2, Dictionary<string, string> dictionary)
        {
            string ltrclsdes = dictionary[p_2] = dictionary.TryGetValue(p, out ltrclsdes) ? ltrclsdes : String.Empty;
        }

        public static void ChangeKeyNameOfLDSS(string p, string p_2, List<Dictionary<string, string>> ldss)
        {
            ldss.ForEach(delegate(Dictionary<string, string> dss)
            {
                ChangeKeyNameOfDSS(p, p_2, dss);
            });
        }

        #region jumpingData

        /// <summary>
        /// Get Jumping Data From post/put result which is encoded.
        /// </summary>
        /// <param name="dss"></param>
        /// <param name="jumpingData"></param>
        /// <returns></returns>
        /// <example>
        /// <code language="C#" description="">
        /// resultData[RequestBus.Param("jumpdata")] = RequestBus.GetJumpingData(dss, jumpingData:"jumpdata");
        ///        ajaxResponse.data = resultData;
        /// </code>
        /// </example>
        public static List<Dictionary<string, string>> GetDecodedJumpingData(Dictionary<string, string> dss, string jumpingData = "jumpdata")
        {
            List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
            string jumpData = dss.TryGetValue(RequestBus.Param(jumpingData), out jumpData) ? jumpData : String.Empty;
            try
            {
                ldss = (List<Dictionary<string, string>>)RF.GlobalClass.Utils.Convert.JSONToObject(System.Web.HttpUtility.HtmlDecode(jumpData), type: typeof(List<Dictionary<string, string>>), mode: "array");
            }
            catch (Exception ex)
            {

            }
            return ldss?? new List<Dictionary<String, String>>();
        }

        /// <summary>
        /// Get JumpingData from post/put which is not encoded.
        /// </summary>
        /// <param name="ldss"></param>
        /// <returns></returns>
        /// <example>
        /// <code language="C#" description="encode the jumpingData and set it to page element to store.">
        /// vbsm_rfjumpdata.Text = RequestBus.GetJumpingData(ldss);
        /// </code>
        /// </example>
        public static string GetEncodedJumpingData(List<Dictionary<string, string>> ldss)
        {
            string result = String.Empty;
            try
            {
                result = System.Web.HttpUtility.HtmlEncode(RF.GlobalClass.Utils.Convert.ObjectToJSON(ldss));
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// Combine put data and post data together into Dictionary&lt;string string&gt;
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetPostPutData(HttpRequest Request)
        {
            Dictionary<string, string> dssPut = RequestBus.GetPutData(Request) ?? new Dictionary<string, string>();
            Dictionary<String, List<Dictionary<string, string>>> dsldssPost = null;
            Dictionary<string, string> dssPost =  new Dictionary<string, string>();
            #region get post data
            try
            {
                try
                {
                    string data = RF.GlobalClass.WebForm.getPostData(Request);
                    dssPost = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<string, string>));
                    if (null != dssPost && dssPost.ContainsKey("data"))
                    {
                        data = System.Web.HttpUtility.UrlDecode(dssPost["data"], System.Text.Encoding.UTF8);
                        try
                        {
                            dssPost = (Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<string, string>));
                        }catch(Exception ea){
                            try
                            {
                                dsldssPost = (Dictionary<String, List<Dictionary<string, string>>>)RF.GlobalClass.Utils.Convert.JSONToObject(data, type: typeof(Dictionary<String, List<Dictionary<string, string>>>));
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        dssPost = dssPost ?? new Dictionary<String, String>();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
            #endregion
            //AjaxResponse ar = new AjaxResponse();
            //ar.data = dsldssPost;
            string jumpdata = RequestBus.Param("jumpdata");
            List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
            //Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dssPost));
            //Response.Write("##");
            //Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dssPut));
            //Response.Write("##");
            //Response.Write("##");
            //Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsldssPost));
            //Response.Write("##");
            //Response.Write("##");
            //Response.Write("##");
            //Response.Write((RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<String, String>>(Request.Params[jumpdata]) as Dictionary<string, string>)["BusinessClass"]);
            //{jumpdata:new List<Dictionary<String,String>>(){(RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<string, string>>(dssPut[jumpdata]) as Dictionary<string, string> )} })
            string tmpText = String.Empty;
            if (null == dsldssPost && dssPut.TryGetValue(jumpdata, out tmpText))
            {
                try
                {
                    dsldssPost = (new Dictionary<String, List<Dictionary<String, String>>>());
                    dsldssPost.Add(jumpdata, new List<Dictionary<String, String>>() { (RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<string, string>>(tmpText) as Dictionary<string, string>) });
                }
                catch (Exception ex)
                {
                }
            }else{}

            if (null != dsldssPost && dsldssPost.TryGetValue(jumpdata, out ldss))
            {
                try
                {
                    foreach (KeyValuePair<string, string> _kvp in ldss.FirstOrDefault<Dictionary<String, String>>())
                    {
                        dssPost[_kvp.Key] = dssPost.ContainsKey(_kvp.Key)? (dssPost[_kvp.Key] ?? _kvp.Value): _kvp.Value;
                    }
                    //tmpText = System.Text.RegularExpressions.Regex.Replace(tmpText, ",\"TableQueryItemListPerPageRowCount.*$", "}");
                    //dsldssPost = (new Dictionary<String, List<Dictionary<String, String>>>());
                    //dsldssPost.Add(jumpdata, new List<Dictionary<String, String>>() { (RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<string, string>>(tmpText) as Dictionary<string, string>) });
                }
                catch (Exception ex)
                {
                }
            }
            else { }
            dssPut.ToList().ForEach(x => dssPost[x.Key] = dssPost.ContainsKey(x.Key) ? dssPost[x.Key] : x.Value);
            return dssPost;
        }
        #endregion

        #region Call Download Page
        public static void download(String filename = "", String dir = "DEFAULT", String page = "DEFAULT")
        {
            try
            {
                if ("" == filename)
                {
                    filename = System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOffice-IE浏览器升级程序-IE8-WindowsXP-x86-CHS.2728888507.exe") ?? ("../" + Properties.Settings.Default.ResourceDirectory) ?? "IE浏览器升级程序-IE8-WindowsXP-x86-CHS.2728888507.exe";
                }
                else { }
                if ("DEFAULT" == dir)
                {
                    dir = System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeResourceDirectory") ?? ("../" + Properties.Settings.Default.ResourceDirectory) ?? "../Resources/Installations/";
                }
                else { }
                if ("DEFAULT" == page)
                {
                    page = System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeDownloadPageURL") ?? ("../" + Properties.Settings.Default.DownloadPageURL) ?? "../WSPublic/download.aspx";
                }
                else { }
                HttpContext.Current.Response.Redirect(page
                   + "?file=" + HttpUtility.UrlEncode(filename)
                   + "&dir=" + HttpUtility.UrlEncode(dir), true);
                // HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeGotoLoginPageURL") ?? Properties.Settings.Default.DownloadPageURL ?? "http://localhost/rfoanew/login.asp", true); // WALogin.aspx
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region redirect post

        /// <summary>
        /// POST data and Redirect to the specified url using the specified page.
        /// </summary>
        /// <param name="page">The page which will be the referrer page.</param>
        /// <param name="destinationUrl">The destination Url to which
        /// the post and redirection is occuring.</param>
        /// <param name="data">The data should be posted.</param>
        /// <Author>Samer Abu Rabie</Author>
        public static void RedirectAndPOST(System.Web.UI.Page page, string destinationUrl,
                                           System.Collections.Specialized.NameValueCollection data)
        {
            //Prepare the Posting form
            string strForm = PreparePOSTForm(destinationUrl, data);
            //Add a literal control the specified page holding 
            //the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new System.Web.UI.LiteralControl(strForm));
        }

        /// <summary>
        /// POST data and Redirect to the specified url using the specified page.
        /// </summary>
        /// <param name="page">The page which will be the referrer page.</param>
        /// <param name="destinationUrl">The destination Url to which
        /// the post and redirection is occuring.</param>
        /// <param name="data">The data should be posted.</param>
        /// <Author>Samer Abu Rabie</Author>
        public static void RedirectAndPOST(System.Web.UI.Control page, string destinationUrl,
                                           System.Collections.Specialized.NameValueCollection data)
        {
            //Prepare the Posting form
            string strForm = PreparePOSTForm(destinationUrl, data);
            //Add a literal control the specified page holding 
            //the Post Form, this is to submit the Posting form with the request.
            page.Controls.Add(new System.Web.UI.LiteralControl(strForm));
        }

        /// <summary>
        /// This method prepares an Html form which holds all data
        /// in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection
        /// will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that
        /// will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        /// <Author>Samer Abu Rabie</Author>
        private static String PreparePOSTForm(string url, System.Collections.Specialized.NameValueCollection data)
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");
            
            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key +
                               "\" value=\"" + data[key] + "\">");
            }

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append(@"<script language=""javascript"">");
            strScript.Append("var " + formID + " = document." +
                             formID + ";");
            strScript.Append("" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        #endregion
    }
}
