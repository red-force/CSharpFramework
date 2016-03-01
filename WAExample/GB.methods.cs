extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace WAExample
{
    public partial class GB
    {

        public partial class Authorization
        {
            private string _moduleID;

            /// <summary>
            /// 模块编号
            /// </summary>
            public string ModuleID
            {
                get { return _moduleID; }
                set { _moduleID = value; }
            }

            private string _signName = "{signName}";
            /// <summary>
            /// 签名模板值
            /// </summary>
            public string SignName
            {
                get { return _signName; }
                set { _signName = value; }
            }

            private string _powerID;
            /// <summary>
            /// 权限编号
            /// </summary>
            public string PowerID
            {
                get { return _powerID; }
                set { _powerID = value; }
            }

            private string _openName;
            /// <summary>
            /// 明号
            /// </summary>
            /// <example>
            /// <value>rfOaService</value>
            /// </example>
            public string OpenName
            {
                get { return _openName; }
                set { _openName = value; }
            }

            private string _methodName;
            /// <summary>
            /// 方法名称
            /// </summary>
            /// <example>
            /// <value>facebook</value>
            /// </example>
            public string MethodName
            {
                get { return _methodName; }
                set { _methodName = value; }
            }

            public Authorization()
            {
            }


            public Authorization(HttpResponse response = null)
            {
                Response = response;
                //RequestUserSession();
                //RequestPowerID();
            }

            #region for authorization

            /// <summary>
            /// 获取Session
            /// </summary>
            public void RequestUserSession(string sessionName = "user")
            {
                
                RequestBus rb = new RequestBus();
                rb.SetSession();
                UserSession = (UserSession)RF.GlobalClass.Utils.Convert
                    .JSONToObject((HttpContext.Current.Session[sessionName] ?? "")
                    .ToString(), type: typeof(UserSession));
                //UserSession = (UserSession)JsonConvert
                //    .DeserializeObject((HttpContext.Current.Session["user"] ?? "")
                //    .ToString(), typeof(UserSession));

            }

            /// <summary>
            /// 获取签名
            /// </summary>
            /// <param name="p">functionName：GetPwr/GetInfoLst/</param>
            /// <param name="jsonText"></param>
            /// <returns></returns>
            public virtual string GetSignName(string p, string jsonText = "")
            {
                string resultStr = "";
                // string jsonText = @"{""empid"":'502',""mdlid"":'0018',""clsid"":'getPWR',""TimeStamp"":'2013-11-13 10:42:35',""OpenName"":'rfOaService',""Sign"":'9D91A0B958DDA93C9F848046E481CB53',""MethodName"":'redforce'}";
                jsonText = jsonText ?? @"{""empid"":'" + UserSession.empid
                    + @"',""mdlid"":'" + ModuleID + @"',""clsid"":'" + UserSession.log
                    + @"',""TimeStamp"":'" + RequestBus.GetDateTimeString(DateTime.Now)
                    + @"',""OpenName"":'" + OpenName
                    + @"',""Sign"":'9D91A0B958DDA93C9F848046E481CB53'"
                    + @",""MethodName"":'" + MethodName + @"'}";
                switch (p)
                {
                    case "GetPwr":
                        //resultStr = RequestBus.DirGetRequestStr(jsonText);
                        break;
                    case "GetInfoLst":
                        //resultStr = RequestBus.NoteDirGetRequest(jsonText);
                        break;
                    case "GetReplyFlg":
                        //resultStr = RequestBus.GetReadNoteFlgSig(jsonText);
                        break;
                    case "SetReplyFlg":
                        //resultStr = RequestBus.GetReadNoteFlgSig(jsonText);
                        break;
                    case "ReadSingNote":
                        //resultStr = RequestBus.SingNoteDirSig(jsonText);
                        break;
                    default: break;
                }
#if DEBUG && PRTRESULTMSG
            Response.Write(resultStr);
#endif
                return resultStr;
            }

            /// <summary>
            /// 获取权限
            /// </summary>
            /// <param name="signName"></param>
            /// 
            public string RequestPowerID(string moduleID = "")
            {
                string resultStr = "";
                moduleID = "" == moduleID ? ModuleID : moduleID;
                //string jsonText = @"{""empid"":'"+userSession.empid+@"',""mdlid"":'0018',""clsid"":'getPWR',""TimeStamp"":'2013-11-13 10:42:35',""OpenName"":'rfOaService',""Sign"":'" + signName + @"',""MethodName"":'redforce'}";
                string pwrid = (UserSession.empid == "47" ? "01" : "");
                string jsonText = GetJsonTextWithSignName("GetPwr"
                    , @"{""empid"":'" + UserSession.empid
                    + @"',""mdlid"":'" + moduleID
                    + @"',""clsid"":'getPWR'"
                    + @",""TimeStamp"":'" + RequestBus.GetDateTimeString(DateTime.Now)
                    + @"',""OpenName"":'" + OpenName + @"',""Sign"":'" + SignName
                    + @"',""MethodName"":'" + MethodName + @"'}");
                //Response.Write(nm.GetPwr(jsonText).Obj);
                //SRExample.ResultMsg resultMsg = (new GB.WebReference(response: Response))
                //    .GetSRExampleResultMsg("ExampleData", jsonText);
                string interfaceName = "GetPowerInfo";
                jsonText = GetJsonTextWithSignName(methodName: interfaceName, jsonText: "");
                SRExample.ResultMsg resultMsg = (new GB.WebReference(response: Response))
                    .GetResultMsg<SRExample.ServiceSoapClient, Func<SRExample.ServiceSoapClient,String,SRExample.ResultMsg>, SRExample.ResultMsg>(serviceSoapClient: new SRExample.ServiceSoapClient(),
                    interfaceName: interfaceName,
                    jsonText: jsonText);
                if (null != resultMsg && null != resultMsg.Obj)
                {
                    if ("00" == resultMsg.RetCode)
                    {
                        RequestBus.ConvertResultMsgObj(resultMsg.Obj);
                        List<Dictionary<string, string>> power = RequestBus.ConvertResultMsgObj(resultMsg.Obj);
                        //Power power = (Power)RequestBus.JsonConvertDeserializeObject(resultMsg.Obj.ToString(), type:typeof(Power)) ?? new Power();
                        resultStr = 0 == power.Count ? pwrid
                            : (power[0]["powerID"] ?? pwrid).ToString().Trim() ?? pwrid;
                    }
                    else
                    {
                        resultStr = "";
                    }
                }
                else
                {
                }
                PowerID = resultStr;
                return resultStr;
            }

            /// <summary>
            /// 获取有合法签名的json参数字符串
            /// </summary>
            /// <param name="serviceName">like SRExample</param>
            /// <param name="methodName">like GetPowerInfo</param>
            /// <param name="jsonText"></param>
            /// <param name="methodParamObjectArray">new Object[] {}</param>
            /// <returns></returns>
            public string GetJsonTextWithSignName(string serviceName = "", string methodName="", string jsonText="", object[] methodParamObjectArray = null)
            {
                string result = "";
                string preffix = String.Empty;
                string suffix = "GetRequest";
                try
                {
                    result = (RF.GlobalClass.Utils.Do.MagicMethod<RequestBus, Func<object, object[], object>>(typeof(RequestBus).GetMethod(preffix + serviceName + methodName + suffix)) as Func<object, object[], object>)(null, methodParamObjectArray) as string;
                }
                catch (Exception ex)
                {
                    try
                    {
                        var func = RF.GlobalClass.Utils.Do.MagicMethod<RequestBus, Func<object[], string>>(typeof(RequestBus).GetMethod(preffix + serviceName + suffix));
                        Dictionary<string, string> dss = new Dictionary<string, string>() { {"params","paramA"}};
                        Dictionary<string, object[]> dssA = new Dictionary<string, object[]>() { { "params", methodParamObjectArray } };
                        result = func(methodParamObjectArray) as string;
                    }
                    catch (Exception exa)
                    {
                        result = "{}";
                        exa.Message.ToArray();
                    }
                }
#if DEBUG && PRTRESULTMSG
            Response.Write(resultStr);
#endif
                return result;
            }

            #endregion

            #region extend for authorization

            /// <summary>
            /// Used to turn to authorizable page, will check before response the page url.
            /// </summary>
            /// <param name="viewID">the target view</param>
            /// <param name="moduleID">authorization module id</param>
            /// <param name="invalidMessage">message for failing the authorization checking </param>
            /// <param name="dssParam">param from the client can be in post or put</param>
            /// <param name="result">the result</param>
            public void TurnToAuthorizablePage(String viewID, String moduleID, String invalidMessage = "", Dictionary<string, string> dssParam = null, AjaxResponse result = null, String successPowerID="01")
            {
                Dictionary<string, AjaxResponse> dsar = new Dictionary<string, AjaxResponse>();
                try
                {

                    // Dictionary<string, string> dss = RequestBus.GetPutData(Request:Request);
                    dssParam = dssParam ?? new Dictionary<string, string>();
                    ModuleID = moduleID;
#if !DEBUG
                    if (successPowerID == RequestPowerID() && null == result)
                    {
#endif
                        result = new WAExample.AjaxResponse();
                        //result = "{status:'failure',message:'查询失败',data:{a:'A'}}";
                        result.status = "success";
                        // result.message = "查询成功";
                        Location location = new Location();
                        //location.protocol = "http:";
                        //location.hostname = "localhost";
                        //location.port = "1352";
                        //location.pathname = "";
                        location.search = "?view=" + viewID; //"?ChangeModificationListPerPageRowCountTo=";
                        //location.hash = "";
                        result.location = location;
                        Dictionary<string, List<Dictionary<string, string>>> resultData = new Dictionary<string, List<Dictionary<string, string>>>();
                        resultData["a"] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { { "a", "A" } } });
                        resultData[RequestBus.Param("jumpdata")] = (new List<Dictionary<string, string>> { dssParam });
                        result.data = resultData;
#if !DEBUG
                    }
                    else
                    {
                        result = new WAExample.AjaxResponse();
                        result.status = "success";
                        result.message = invalidMessage ?? "尚无此权限";
                    }
#endif
                    dsar["d"] = result;
                }
                catch (Exception ex)
                {
                    WAExample.AjaxResponse ar = new AjaxResponse();
                    ar.status = "failure";
                    ar.message = "系统异常，请稍后再试。"; // ex.Message;
                    dsar["d"] = ar;
                }
                Response.ClearContent();
                Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsar));
                Response.End();
            }

            #endregion

            public UserSession UserSession { get; set; }

            public HttpResponse Response { get; set; }
        }

        public partial class WebReference
        {

            public WebReference(HttpResponse response = null)
            {
                Response = response;
            }

            #region for WebReference

            delegate object NoteMangeGet0();
            delegate object NoteMangeGet1(string jsonText = "");
            delegate object NoteMangeGet4(string code = "", string param1 = "", string param2 = "", string param3 = "");
            /// <summary>
            /// 获取SRExample查询结果。
            /// </summary>
            /// <param name="p"></param>
            /// <param name="jsonText"></param>
            /// <param name="obj"></param>
            /// <param name="specifiedFields"></param>
            /// <param name="keyIDpair"></param>
            /// <returns></returns>
            internal SRExample.ResultMsg GetSRExampleResultMsg(string serviceName = "", string methodName = "", string jsonText = "", RequestBus.Passengers obj = null, Dictionary<string, string> specifiedFields = null, Dictionary<string, string> keyIDpair = null)
            {
                Type t = typeof(SRExample.ResultMsg);
                SRExample.ServiceSoapClient ssc = new SRExample.ServiceSoapClient();
                SRExample.ResultMsg resultMsg = null;
                try
                {
                    if (null != obj)
                    {
                        GB.Authorization gba = new Authorization();
                        // jsonText = gba.GetJsonTextWithSignName("WPWeb", obj: obj, specifiedFields: specifiedFields, keyIDpair: keyIDpair);
                        jsonText = gba.GetJsonTextWithSignName(serviceName: serviceName, methodName: methodName, methodParamObjectArray: new object[] { obj, specifiedFields, keyIDpair });
                        // Response.Write(jsonText);
                    }
                    else { }
                    resultMsg = (RF.GlobalClass.Utils.Do.MagicMethod<SRExample.ServiceSoapClient, Func<SRExample.ServiceSoapClient, String, SRExample.ResultMsg>>(typeof(SRExample.ServiceSoapClient).GetMethod(methodName, new[] { typeof(string) })))(ssc, jsonText) as SRExample.ResultMsg;
                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                try
                {
                    if (null == resultMsg)
                    {
                        Response.ClearContent();
                        RequestBus.ajaxResponseOfResultMsgNull(Response: Response);
                        Response.End();
                        return resultMsg;
                    }
                    else { }
                }
                catch (Exception ex)
                {
                }
                return resultMsg;
            }

            /// <summary>
            /// GetInterfaceMethod
            /// </summary>
            /// <typeparam name="V">ServiceSOAPClient</typeparam>
            /// <typeparam name="U">Method Result Class</typeparam>
            /// <typeparam name="T">Method Class : Func&lt;&gt;</typeparam>
            /// <param name="serviceSoapClient"></param>
            /// <param name="interfaceName"></param>
            /// <returns></returns>
            /// <example language="C#">
            /// var c = GetInterfaceMethod&lt;SRExample.ResultMsg, Func&lt;SRExample.ServiceSOAPClient, String, SRExample.ResultMsg&gt;&gt;(new SRExample.ServiceSOAPClient(), "GetExampleData")
            /// // prepare param
            /// GB.Authorization gba = new Authorization();
            ///  jsonText = gba.GetJsonTextWithSignName(methodName: interfaceName, jsonText: String.Empty, methodParamObjectArray: new object[] { obj, specifiedFields, keyIDpair });
            /// // invoke interface method:
            /// c(new SRExample.SeriveSOAPClient(), jsonText);
            /// </example>
            internal T GetInterfaceMethod<V, U, T>(string interfaceName)
            {
                var methodInfo = typeof(V).GetMethod(interfaceName);
                var f = RF.GlobalClass.Utils.Do.MagicMethod<V, T>(method: methodInfo);
                return f;
            }
            /// <summary>
            /// 获取ServiceReference查询结果。
            /// </summary>
            /// <typeparam name="T">serviceSOAPClient</typeparam>
            /// <typeparam name="U">Func<></typeparam>
            /// <typeparam name="V">ResultMsg</typeparam>
            /// <param name="serviceSoapClient"></param>
            /// <param name="interfaceName"></param>
            /// <param name="jsonText"></param>
            /// <param name="obj">passengers</param>
            /// <param name="specifiedFields"></param>
            /// <param name="keyIDpair"></param>
            /// <returns></returns>
            internal V GetResultMsg<T, U, V>(T serviceSoapClient, string interfaceName, string jsonText = "", RequestBus.Passengers obj = null, Dictionary<string, string> specifiedFields = null, Dictionary<string, string> keyIDpair = null)
            {
                V resultMsg = default(V);
                Type type = String.Empty.GetType();
                try
                {
                    if (null != obj)
                    {
                        GB.Authorization gba = new Authorization();
                        jsonText = gba.GetJsonTextWithSignName(methodName: interfaceName, jsonText: String.Empty, methodParamObjectArray: new object[] { obj, specifiedFields, keyIDpair });
                        // Response.Write(jsonText);
                    }
                    else { }

                    var gim = GetInterfaceMethod<T, V, Func<T, String, V>>(interfaceName: interfaceName);
                    //"[{\"RetCode\":\"00\",\"RetMsg\": \"OK\",\"obj\":\"[{powerID:\"\"00\"\"}, {powerID:\"\"01\"\"}]\"}]"
                    resultMsg = gim(serviceSoapClient, jsonText);

                }
                catch (Exception Exception)
                {
                    throw Exception;
                }
                try
                {
                    if (null == resultMsg)
                    {
                        Response.ClearContent();
                        RequestBus.ajaxResponseOfResultMsgNull(Response: Response);
                        Response.End();
                        return resultMsg;
                    }
                    else { }
                }
                catch (Exception ex)
                {
                }
                return resultMsg;
            }

            /// <summary>
            /// Get the site nickname; (as rfoa1 in http://locaohost:80/rfoa1/xxxx/xxxxx.aspx) From the Web.config
            /// </summary>
            /// <param name="Request">the HttpRequest used to find the origin of the location</param>
            /// <param name="RelativeUri">the relative uri string append to the site nickname</param>
            /// <returns></returns>
            public string GetSiteNickname(HttpRequest Request = null, String RelativeUri = "")
            {
                string result = String.Empty;
                try
                {
                    if (null != Request)
                    {
                        result = Request.Url.OriginalString.Replace(Request.Url.PathAndQuery, "/");
                    }
                    else { }
                    result += (System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeSiteNickname") ?? "rf");
                    result += RelativeUri;
                }
                catch (Exception ex)
                {
                }
                return result;
            }

            public bool CheckIEBrowser(HttpRequest Request)
            {
                bool result = true;
                double IEV = RF.GlobalClass.Utils.WebBrowser.getInternetExplorerVersion(Request);
                double WV = RF.GlobalClass.Utils.WebBrowser.getPlatform(Request);
                if ((7.0 > IEV && IEV > 1.0) || ( 7.0 == IEV && Request.UserAgent.IndexOf("Trident/4.0") == -1))
                {
                    if (5.1 == WV)
                    {
                        result = false;
                        //RequestBus.download();
                        // RequestBus.download(filename: "IE浏览器升级程序-IE8-WindowsXP-x86-CHS.2728888507-IE浏览器升级程序-联系信息部升级.exe", dir: "../Resources/Installations/", page: "../WSPublic/download.aspx");
                    }
                    else {
                        //HttpContext.Current.Response.Write("IEV:" + IEV.ToString() + ";WV:" + WV.ToString() + ";R:" + Request.Browser.Platform + ";S:" + Request.UserAgent);
                    }
                }
                else { } 
                // HttpContext.Current.Response.Write("IEV:" + IEV.ToString() + ";WV:" + WV.ToString() + ";R:" + Request.Browser.Platform + ";S:" + Request.UserAgent);
                /*
                if (6.1 == WV)
                {
                    if (1.0 < IEV && IEV < 11.0)
                    {
                        result = false;
                        RequestBus.download(filename: "IE浏览器升级程序-IE11-Windows6.1-x64-zh-cn.exe");
                    }
                    else { }
                }
                else { }
                 * */
                return result;
            }

            #endregion

            public HttpResponse Response { get; set; }

            /// <summary>
            /// Get data for table list
            /// </summary>
            /// <param name="tableName"></param>
            /// <param name="tableNameSpace"></param>
            /// <param name="tableCurrPageNum"></param>
            /// <param name="tablePerPageRowCount"></param>
            /// <param name="beforePageChange"></param>
            /// <param name="whenPageChange"></param>
            /// <param name="afterPageChange"></param>
            /// <param name="request"></param>
            /// <param name="response"></param>
            /// <param name="table"></param>
            /// <param name="columnKeyIDPair"></param>
            /// <param name="perPageRowCountControl"></param>
            /// <param name="currPageNumControl"></param>
            /// <param name="pageTatolCountControl"></param>
            /// <param name="rowTotalCountControl"></param>
            /// <param name="perPageRowCountName"></param>
            /// <param name="currPageNumName"></param>
            /// <param name="pageTotalCountName"></param>
            /// <param name="rowTotalCountName"></param>
            /// <param name="jumpingDataName"></param>
            /// <param name="perPageRowCountParamName"></param>
            /// <param name="dataSet"></param>
            /// <example>
            /// <code language="CSharp" description="">
            /// WebAppOffice.GB.WebReference.getDataForListTable(tableName: "", tableNameSpace: "", tableCurrPageNum: "1", tablePerPageRowCount: "10",
            ///     beforePageChange: (new WebAppOffice.RFDataTable.BeforePageChangeHandler(rfdtarql_BeforePageChange)),
            ///     whenPageChange: (new WebAppOffice.RFDataTable.WhenPageChangeHandler(rfdtarql_WhenPageChange)),
            ///     afterPageChange: (new WebAppOffice.RFDataTable.AfterPageChangeHandler(rfdtarql_AfterPageChange)),
            ///     request: Request, response: Response,
            ///     table: v2_TableInformationQueryItemList,
            ///     columnKeyIDPair: new Dictionary<string, string> { 
            ///     { "Code", "dptid" }, { "Name", "dptdes" },
            ///     { "Address", "adr1" }, { "PhoneNumber", "tel" },
            ///     {"Section","prtdptid"},{"EffectsRadius","jl"},
            ///     {"MasterName","xm"},{"MasterPhoneNumber","sj"},
            ///     {"SectionDirectorName","prtdz"},{"SectionDirectorPhoneNumber","prtdztel"} },
            ///     perPageRowCountControl: v2_TableInformationQueryItemList_PerPageRowCount,
            ///     currPageNumControl: v2_TableInformationQueryItemList_CurrPageNum,
            ///     pageTatolCountControl: v2_TableInformationQueryItemList_PageTotalCount,
            ///     rowTotalCountControl: v2_TableInformationQueryItemList_RowTotalCount,
            ///     perPageRowCountName: "PerPageRowCount",
            ///     currPageNumName: "CurrPageNum",
            ///     pageTotalCountName: "pageTotalCount",
            ///     rowTotalCountName: "RowTotalCount",
            ///     jumpingDataName: "jumpdata",
            ///     perPageRowCountParamName: "PerPageRowCount", dataSet:dataSet);
            /// </code>
            /// </example>
            public static void getDataForListTable(string tableName, string tableNameSpace, string tableCurrPageNum, string tablePerPageRowCount, RFDataTable.BeforePageChangeHandler beforePageChange, RFDataTable.WhenPageChangeHandler whenPageChange, RFDataTable.AfterPageChangeHandler afterPageChange, HttpRequest request, HttpResponse response, System.Web.UI.WebControls.Table table, Dictionary<string, string> columnKeyIDPair, System.Web.UI.WebControls.DropDownList perPageRowCountControl, System.Web.UI.WebControls.TextBox currPageNumControl, System.Web.UI.WebControls.Label pageTatolCountControl, System.Web.UI.WebControls.Label rowTotalCountControl, string perPageRowCountName, string currPageNumName, string pageTotalCountName, string rowTotalCountName, string jumpingDataName, string perPageRowCountParamName, System.Data.DataSet dataSet, RFDataTable rfDataTable)
            {
                Dictionary<string, string> dssPost = new Dictionary<string, string>();
                Dictionary<string, string> dssPut = RequestBus.GetPutData(request);
                Dictionary<String, List<Dictionary<string, string>>> dsldssPost = RequestBus.GetPostData<Dictionary<String, List<Dictionary<string, string>>>>(request);
                AjaxResponse ar = new AjaxResponse();
                ar.data = dsldssPost;
                string jumpdata = RequestBus.Param(jumpingDataName);
                List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();

                rfDataTable = rfDataTable ?? new RFDataTable(tableName, request: request, response: response);
                rfDataTable.Name = tableName ?? rfDataTable.Name;
                tableName = rfDataTable.Name;

                if (null != dsldssPost && ((dsldssPost.TryGetValue(jumpdata, out ldss)) || dsldssPost.ContainsKey(tableName + perPageRowCountParamName)))
                {
                    #region set data to the view
                    // ldss[0].TryGetValue("", out jumpdata);
                    dssPost = (ldss.Count > 0 ? (ldss[0] ?? dssPost) : dssPost);
                    dssPut.ToList().ForEach(x => dssPost[x.Key] = dssPost.ContainsKey(x.Key) ? dssPost[x.Key] : x.Value);
                    //string beginDate = v0_BeginDate.Text = dssPost.TryGetValue("BeginDate", out beginDate) ? beginDate : String.Empty;
                    //string endDate = v0_EndDate.Text = dssPost.TryGetValue("EndDate", out endDate) ? endDate : String.Empty;
                    perPageRowCountControl.Text = tablePerPageRowCount ?? perPageRowCountControl.Text;
                    tablePerPageRowCount = perPageRowCountControl.Text = dssPost.TryGetValue(tableName + perPageRowCountName, out tablePerPageRowCount) ? tablePerPageRowCount : perPageRowCountControl.Text;
                    tableCurrPageNum = currPageNumControl.Text = dssPost.TryGetValue(tableName + currPageNumName, out tableCurrPageNum) ? tableCurrPageNum : tableCurrPageNum;
                    string TablePageTotalCount = pageTatolCountControl.Text = dssPost.TryGetValue(tableName + pageTotalCountName, out TablePageTotalCount) ? TablePageTotalCount : tableCurrPageNum;
                    string TableRowTotalCount = rowTotalCountControl.Text = dssPost.TryGetValue(tableName + rowTotalCountName, out TableRowTotalCount) ? TableRowTotalCount : "X";

                    #endregion
                }
                else
                {
                }

                int perPageRowCount = int.TryParse(tablePerPageRowCount, out perPageRowCount) ? perPageRowCount : 5;

                rfDataTable.BeforePageChange += new RFDataTable.BeforePageChangeHandler(beforePageChange);
                rfDataTable.WhenPageChange += new RFDataTable.WhenPageChangeHandler(whenPageChange);
                rfDataTable.AfterPageChange += new RFDataTable.AfterPageChangeHandler(afterPageChange);
                dataSet.Tables.Add(rfDataTable.ChangePageTo(
                    currPage: tableCurrPageNum,
                    perPageRowCount: perPageRowCount.ToString(),
                    dssPostData: dssPost,
                    dataTableName: tableName,
                    dataTableNamespace: tableNameSpace,
                    columnKeyIDPair: columnKeyIDPair,
                    t: table));
            }

            /// <summary>
            /// Get the key of the enum according to the enum value;
            /// </summary>
            /// <param name="enumValue">enum value</param>
            /// <returns>string</returns>
            public static string getEnumKey(Enum enumValue)
            {
                return rf.RF.GlobalClass.Utils.Convert.EnumValueToString(enumValue);
            }
        }

        public partial class WebControl
        {
            public static void DropDownListBindData(System.Web.UI.WebControls.DropDownList ddl, System.Data.DataTable dt, string dataTextField = "txt", string dataValueField = "val")
            {
                try
                {
                    ddl.DataTextField = dataTextField;
                    ddl.DataValueField = dataValueField;
                    ddl.DataSource = dt;
                    ddl.DataBind();
                }
                catch (Exception ex)
                {
                    ddl.SelectedValue = ddl.Items[0].Value;
                }
            }


            /// <summary>
            /// Bind ListDictionaryStringString to DropDownList the val is the first column and txt is second
            /// </summary>
            /// <param name="ddl"></param>
            /// <param name="ldss"></param>
            /// <param name="dataTextField"></param>
            /// <param name="dataValueField"></param>
            public static void DropDownListBindData(System.Web.UI.WebControls.DropDownList ddl, List<Dictionary<string, string>> ldss, string dataTextField = "txt", string dataValueField = "val")
            {
                try
                {
                    ddl.DataTextField = dataTextField;
                    ddl.DataValueField = dataValueField;
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.Add(dataValueField);
                    dt.Columns.Add(dataTextField);
                    ldss.Select(delegate(Dictionary<String, String> _dss)
                    {
                        try
                        {
                            System.Data.DataRow dr = dt.NewRow();
                            // dr.ItemArray = _dss.Select((KeyValuePair<string, string> x, int i) => { return x.Value; }).ToArray();
                            dr.ItemArray = new object[] { _dss[dataValueField], _dss[dataTextField] };
                            dt.Rows.Add(dr);
                        }
                        catch (Exception exa) { }
                        return _dss;
                    }).ToArray();
                    ddl.DataSource = dt;
                    ddl.DataBind();
                }
                catch (Exception ex)
                {
                    ddl.SelectedValue = ddl.Items[0].Value;
                }
            }
        }
    }
}
