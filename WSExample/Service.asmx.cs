extern alias carfs;
extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;

namespace WSExample
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        private Dictionary<string, Dictionary<string, string>> dbUserNames = new Dictionary<string, Dictionary<string, string>>() {
            {
                "192.10.200.5", new Dictionary<string, string>() {
                    { "User ID", "#web#610041#" }, { "Password", carfs.CARFSecurer.Program.GetDecryptedMessage("des", "2r31eCRiIAg=") } 
                }
            }, {
                "DBExample", new Dictionary<string, string>() {
                      { "DataSource", @".\SQLEXPRESS"}
                    , { "AttachDbFilename", @"C:\Users\Will_2\Documents\GitHub\mainCSharp\DBExample.mdf;"}
                    , { "User ID", "will" }
                    , { "Password", carfs.CARFSecurer.Program.GetDecryptedMessage("des", "z4SwFzT7Rf0=") } // will
                }
            }
        };
        
        private const int CacheTime = 1;	// seconds
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #region Example

        [WebMethod(Description = "ExampleData1", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg ExampleData1(string param = "")
        {
            String code = carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug"));
            String supposedData = String.Empty;
            string result = @"[{""RetMsg"": """ + "" + @""", ""obj"": ""[" + "" + @"]""}]";
            string retCode = "";
            string retMsg = "";
            try
            {
                #region request data
                if (code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: (param)) || code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug")))
                {
                    using (RF.GlobalClass.DB.ConnLocalDB cldb = new RF.GlobalClass.DB.ConnLocalDB())
                    {
                        try
                        {
                            retMsg = String.Empty;
                            String value = String.Empty;
                            retCode = "OK" == retMsg ? "00" : "unknown";
                            result = @"[{""RetCode"":""" + retCode + @""",""RetMsg"": """ + retMsg + @""",""obj"":" + value + @"}]";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    //string value = "";
                    //string status = "";
                    //value = gpi.getmdlids("0017", "system", "47", out status);
                }
                else { }
                #endregion
            }
            catch (Exception ex)
            {
            }
            supposedData = String.IsNullOrEmpty(supposedData) ? result : supposedData;
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            return rm;
        }

        [WebMethod(Description = "ExampleData", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg ExampleData(string code = null, string param1 = "", string param2 = "", string param3 = "", string supposedData=null)
        {
            string result = @"[{""RetMsg"": """ + "" + @""", ""obj"": ""[" + "" + @"]""}]";
            string retCode = "";
            string retMsg = "";
            try
            {
                #region request data
                if (code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: (param1 + param2 + param3)) || code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug")))
                {   
                    using (RF.GlobalClass.DB.ConnLocalDB cldb = new RF.GlobalClass.DB.ConnLocalDB())
                    {
                        try
                        {
                            retMsg = String.Empty;
                            String value = String.Empty;
                            retCode = "OK" == retMsg ? "00" : "unknown";
                            result = @"[{""RetCode"":""" + retCode + @""",""RetMsg"": """ + retMsg + @""",""obj"":" + value + @"}]";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    //string value = "";
                    //string status = "";
                    //value = gpi.getmdlids("0017", "system", "47", out status);
                }
                else { }
                #endregion
            }
            catch (Exception ex)
            {
            }
            supposedData = String.IsNullOrEmpty(supposedData) ? result : supposedData;
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            return rm;
        }

        private Dictionary<String, String> getExampleSpecialKeys(Dictionary<String, String> dss)
        {
            return new Dictionary<string, string>(){
                    {"PageNum", dss["PageNum"]}
                    , {"PerPageRowCount", dss["PerPageRowCount"]}
                    , {"Type", dss["Type"]}
                };
        }

        [WebMethod(Description = "GetExampleType.", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetExampleType(string param = null)
        {
            #region prepare param
            // {"passenger1":"1","passenger2":"2","passenger3":"3","Sign":"bc3b309524bb7cedf96be4f206b88c"}
            Dictionary<String, String> dss = RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<String, String>>(param) as Dictionary<String, String>;
            string pageNum = dss.TryGetValue("PageNum", out pageNum) ? pageNum : "1";
            string perPageRowCount = dss.TryGetValue("perPageRowCount", out perPageRowCount) ? perPageRowCount : "10";
            string Sign = dss.TryGetValue("Sign", out Sign) ? Sign : String.Empty;
            string supposedData = dss.TryGetValue("SupposedData", out supposedData) ? supposedData : String.Empty;

            #endregion
            string retCode = rf.RF.GlobalClass.Utils.Convert.EnumValueToString(ResultMsg.Code.failure);
            string retMsg = "提交失败";
            try
            {
                Dictionary<String, String> dssSpecialKeys = getExampleSpecialKeys(dss: dss);
                // p.passenger0 = carfs.CARFSecurer.Program.GetEncrytedMessage("MD5", RF.GlobalClass.Utils.Convert.ObjectToJSON(openWith));
                carfs.CARFSecurer.Program.GetEncrytedMessage("MD5", RF.GlobalClass.Utils.Convert.ObjectToJSON(dssSpecialKeys));
                if (carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", RF.GlobalClass.Utils.Convert.ObjectToJSON(dssSpecialKeys)) != Sign)
                {
                    retMsg = "签名错误";
                    throw new FormatException(retMsg);
                }
                else { }
                // Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Will_2\Documents\GitHub\mainCSharp\DBExample.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True
                String // connectionStr = "Data Source=127.0.0.1;  Initial Catalog=syslog; User ID=" + dbUserNames["192.10.200.5"]["User ID"] + "; Password=" + dbUserNames["192.10.200.5"]["Password"] + ";";
                connectionStr = @"Data Source=" + dbUserNames["DBExample"]["DataSource"] + ";AttachDbFilename=" + dbUserNames["DBExample"]["AttachDbFilename"] + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
                RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(connectionStr);
                DataSet ds = new DataSet();
                pageNum = String.IsNullOrEmpty(pageNum) ? "1" : pageNum;
                perPageRowCount = String.IsNullOrEmpty(perPageRowCount) ? "10" : perPageRowCount;
                int startRowNum = (int.Parse(pageNum) - 1) * int.Parse(perPageRowCount) + 1;
                int endRowNum = startRowNum + int.Parse(perPageRowCount);
                try
                {
//                    ds = cdb.ReturnDataSet(@"
//	                      SELECT code, name
//	                      FROM ExampleType");
                    ds = cdb.RunProcedure("sp_getExampleType", new System.Data.IDataParameter[]{
                        new System.Data.SqlClient.SqlParameter("@pageNum", "" +  pageNum)
                        ,new System.Data.SqlClient.SqlParameter("@perPageRowCount", "" + perPageRowCount)
                        ,new System.Data.SqlClient.SqlParameter("@idColName", "id")
                    }, new string[] { "Records", "Info" });
                    supposedData = RF.GlobalClass.Utils.Convert.ObjectToJSON(ds);
                    retCode = rf.RF.GlobalClass.Utils.Convert.EnumValueToString(ResultMsg.Code.success);
                    retCode = ResultMsg.Code.success.ToString();
                    retMsg = "查询成功";
                }
                catch (Exception ex)
                {
                    retMsg = "查询失败" + ex.Message;
                }
                finally
                {
                    cdb.Close();
                }
            }
            catch (Exception ex)
            {
                retMsg = ex.Message;
                supposedData = "{}";
            }
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            supposedData = @"[{""retCode"":""" + retCode + @""",""retMsg"":""" + retMsg + @""",""obj"":""" + supposedData.Replace("\"", "\"\"") + @"""}]" ?? @"[{""retCode"":""failure"",""retMsg"":""提交失败"",""obj"":""""}]";

            return rm;
        }

        [WebMethod(Description = "GetExampleItem.", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetExampleItem(string param = null)
        {
            #region prepare param
            // {"passenger1":"1","passenger2":"2","passenger3":"3","Sign":"bc3b309524bb7cedf96be4f206b88c"}
            Dictionary<String, String> dss = RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<String, String>>(param) as Dictionary<String, String>;
            string pageNum = dss.TryGetValue("PageNum", out pageNum) ? pageNum : "1";
            string perPageRowCount = dss.TryGetValue("PerPageRowCount", out perPageRowCount) ? perPageRowCount : "10";
            string Sign = dss.TryGetValue("Sign", out Sign) ? Sign : String.Empty;
            string supposedData = dss.TryGetValue("SupposedData", out supposedData) ? supposedData : String.Empty;

            #endregion
            string retCode = rf.RF.GlobalClass.Utils.Convert.EnumValueToString(ResultMsg.Code.failure);
            string retMsg = "提交失败";
            try
            {
                Dictionary<String, String> dssSpecialKeys = getExampleSpecialKeys(dss: dss);
                // p.passenger0 = carfs.CARFSecurer.Program.GetEncrytedMessage("MD5", RF.GlobalClass.Utils.Convert.ObjectToJSON(openWith));
                carfs.CARFSecurer.Program.GetEncrytedMessage("MD5", RF.GlobalClass.Utils.Convert.ObjectToJSON(dssSpecialKeys));
                if (carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", RF.GlobalClass.Utils.Convert.ObjectToJSON(dssSpecialKeys)) != Sign && Sign != String.Empty)
                {
                    retMsg = "签名错误";
                    throw new FormatException(retMsg);
                }
                else { }
                // Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Will_2\Documents\GitHub\mainCSharp\DBExample.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True
                String // connectionStr = "Data Source=127.0.0.1;  Initial Catalog=syslog; User ID=" + dbUserNames["192.10.200.5"]["User ID"] + "; Password=" + dbUserNames["192.10.200.5"]["Password"] + ";";
                connectionStr = @"Data Source=" + dbUserNames["DBExample"]["DataSource"] + ";AttachDbFilename=" + dbUserNames["DBExample"]["AttachDbFilename"] + ";Integrated Security=True;Connect Timeout=30;User Instance=True";
                RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(connectionStr);
                DataSet ds = new DataSet();
                pageNum = String.IsNullOrEmpty(pageNum) ? "1" : pageNum;
                perPageRowCount = String.IsNullOrEmpty(perPageRowCount) ? "10" : perPageRowCount;
                int startRowNum = (int.Parse(pageNum) - 1) * int.Parse(perPageRowCount) + 1;
                int endRowNum = startRowNum + int.Parse(perPageRowCount);
                try
                {
//                    ds = cdb.ReturnDataSet(@"
//	                      SELECT *
//	                      FROM ExampleItem;
//                          SELECT count(1) FROM ExampleItem;");
                    //SqlDataReader dr = cdb.RunProcedure("p_getExampleType", new System.Data.IDataParameter[]{
                    //    new System.Data.SqlClient.SqlParameter("@beginNum", ((int.Parse(pageNum) -1) * int.Parse(rowCount)))
                    //    ,new System.Data.SqlClient.SqlParameter("@endNum", ((int.Parse(pageNum)) * int.Parse(rowCount)))
                    //    ,new System.Data.SqlClient.SqlParameter("@idColName", "id")
                    //});
                    ds = cdb.RunProcedure("sp_getTable", new System.Data.IDataParameter[]{
                        new System.Data.SqlClient.SqlParameter("@tableName", "" +  "ExampleItem")
                        ,new System.Data.SqlClient.SqlParameter("@pageNum", "" +  pageNum)
                        ,new System.Data.SqlClient.SqlParameter("@perPageRowCount", "" + perPageRowCount)
                        ,new System.Data.SqlClient.SqlParameter("@idColName", "id")
                    }, new string[] {"Records", "Info"});
                    supposedData = RF.GlobalClass.Utils.Convert.ObjectToJSON(ds);
                    retCode = rf.RF.GlobalClass.Utils.Convert.EnumValueToString(ResultMsg.Code.success);
                    retCode = ResultMsg.Code.success.ToString();
                    retMsg = "查询成功";
                }
                catch (Exception ex)
                {
                    retMsg = "查询失败" + ex.Message;
                }
                finally
                {
                    cdb.Close();
                }
            }
            catch (Exception ex)
            {
                supposedData = "{}";
            }
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            supposedData = @"[{""retCode"":""" + retCode + @""",""retMsg"":""" + retMsg + @""",""obj"":""" + supposedData.Replace("\"", "\"\"") + @"""}]" ?? @"[{""retCode"":""failure"",""retMsg"":""提交失败"",""obj"":""""}]";

            return rm;
        }

        #endregion

        [WebMethod(Description = "GetPowerInfo", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetPowerInfo(string param1 = "")
        {
            String code = carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug"));
            String supposedData = String.Empty;
            String retMsg = String.Empty;
            retMsg = "OK";
            String value = String.Empty;
            String retCode = "OK" == retMsg ? "00" : "unknown";
            string result = @"{""RetCode"": """ + retCode + "" + @""",""RetMsg"": """ + retMsg + @""", ""obj"": ""[" + "" + @"]""}";
            try
            {
                if (code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: (param1)) || code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug")))
                {
                    try
                    {
                        value = "[{powerID:\"00\"}, {powerID:\"01\"}]";
                        result = @"{""RetCode"":""" + retCode + @""",""RetMsg"": """ + retMsg + @""",""Obj"":""" + value.Replace("\"", "\\\"") + @""", ""ExtensionData"":""" + @"""}";
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
            }
            supposedData = String.IsNullOrEmpty(supposedData) ? result : supposedData;
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            return rm;
        }

        [WebMethod(Description = "GetPowerInfo3", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetPowerInfo3(string code = null, string param1 = "", string supposedData = null)
        {
            String retMsg = String.Empty;
            retMsg = "OK";
            String value = String.Empty;
            String retCode = "OK" == retMsg ? "00" : "unknown";
            string result = @"{""RetCode"": """ + retCode + "" + @""",""RetMsg"": """ + retMsg + @""", ""obj"": ""[" + "" + @"]""}";
            try
            {
                if (code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: (param1 )) || code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug")))
                {

                    try
                    {
                        value = "[{powerID:\"00\"}, {powerID:\"01\"}]";
                        result = @"{""RetCode"":""" + retCode + @""",""RetMsg"": """ + retMsg + @""",""Obj"":""" + value.Replace("\"", "\\\"") + @""", ""ExtensionData"":""" + @"""}";
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
            }
            supposedData = String.IsNullOrEmpty(supposedData)? result: supposedData;
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            
            return rm;
        }


        [WebMethod(Description = "GetPowerInfo3", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetExampleID(string code = null, string param1 = "", string supposedData = null)
        {
            String retMsg = String.Empty;
            retMsg = "OK";
            String value = String.Empty;
            String retCode = "OK" == retMsg ? "00" : "unknown";
            string result = @"{""RetCode"": """ + retCode + "" + @""",""RetMsg"": """ + retMsg + @""", ""obj"": ""[" + "" + @"]""}";
            try
            {
                if (code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: (param1)) || code == carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: Enum.GetName(typeof(carfs.RF.GlobalClass.Securer.Names), 0), originMessage: ("debug")))
                {

                    try
                    {

                        RF.GlobalClass.DB.ConnLocalDB cldb = new RF.GlobalClass.DB.ConnLocalDB(@"Data Source=C:\Users\Will_2\Documents\GitHub\mainCSharp\DBExample.sdf;Password=Red.Force0;Persist Security Info=True");
                        DataSet ds = cldb.ReturnDataSet("SELECT * FROM example_info");
                        value = "[{powerID:\"00\"}, {powerID:\"01\"}]";
                        result = @"{""RetCode"":""" + retCode + @""",""RetMsg"": """ + retMsg + @""",""Obj"":""" + value.Replace("\"", "\\\"") + @""", ""ExtensionData"":""" + @"""}";
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
            }
            supposedData = String.IsNullOrEmpty(supposedData) ? result : supposedData;
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            return rm;
        }


        [WebMethod(Description = "GetIEVersionCount.", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetIEVersionCount(string version = null, string pageNum = null, string rowCount = null, string TimeStamp = "", string Sign = "", string supposedData = null)
        {
            string retCode = "failure";
            string retMsg = "提交失败";
            try
            {
                if (TimeStamp == Sign || carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", TimeStamp) != Sign)
                {
                    retMsg = "签名错误";
                    throw new FormatException(retMsg);
                }
                else { }
                String connectionStr = "Data Source=192.10.200.5;  Initial Catalog=syslog; User ID=" + dbUserNames["192.10.200.5"]["User ID"] + "; Password=" + dbUserNames["192.10.200.5"]["Password"] + ";";
                RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(connectionStr);
                DataSet ds = new DataSet();
                pageNum = String.IsNullOrEmpty(pageNum) ? "1" : pageNum;
                rowCount = String.IsNullOrEmpty(rowCount) ? "10" : rowCount;
                int startRowNum = (int.Parse(pageNum) - 1) * int.Parse(rowCount) + 1;
                int endRowNum = startRowNum + int.Parse(rowCount);
                try
                {
                    ds = cdb.ReturnDataSet(@"
	                      SELECT Count(1) as count
	                      FROM [dbo].[IEVersionRecords]
	                      WHERE branch_store_code not like '[9SB]%' 
	                      AND status in (1,3) AND ie_version_id in (" + version + @")");
                    supposedData = RF.GlobalClass.Utils.Convert.ObjectToJSON(ds.Tables[0]);
                    retCode = "success";
                    retMsg = "查询成功";
                }
                catch (Exception ex)
                {
                    retMsg = "查询失败" + ex.Message;
                }
                finally
                {
                    cdb.Close();
                }
            }
            catch (Exception ex)
            {
            }
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            supposedData = @"[{""retCode"":""" + retCode + @""",""retMsg"":""" + retMsg + @""",""obj"":""" + supposedData.Replace("\"", "\"\"") + @"""}]" ?? @"[{""retCode"":""failure"",""retMsg"":""提交失败"",""obj"":""""}]";

            return rm;
        }

        [WebMethod(Description = "GetOldIEInfoList.", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetOldIEInfoList(string versionLevelFrom = null, string versionLevelTo = null, string pageNum = null, string rowCount = null, string TimeStamp = "", string Sign = "", string supposedData = null)
        {
            string retCode = "failure";
            string retMsg = "提交失败";
            versionLevelFrom = String.IsNullOrEmpty(versionLevelFrom) ? "1" : versionLevelFrom;
            versionLevelTo = String.IsNullOrEmpty(versionLevelTo) ? "8" : versionLevelTo;
            int recordsCount = 0;
            try
            {
                if (TimeStamp == Sign || carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", TimeStamp) != Sign)
                {
                    retMsg = "签名错误";
                    throw new FormatException(retMsg);
                }
                else { }
                String connectionStr = "Data Source=192.10.200.5;  Initial Catalog=syslog; User ID=" + dbUserNames["192.10.200.5"]["User ID"] + "; Password=" + dbUserNames["192.10.200.5"]["Password"] + ";";
                RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(connectionStr);
                DataSet ds = new DataSet();
                pageNum = String.IsNullOrEmpty(pageNum) ? "1" : pageNum;
                rowCount = String.IsNullOrEmpty(rowCount) ? "10" : rowCount;
                int startRowNum = (int.Parse(pageNum) - 1) * int.Parse(rowCount) + 1;
                int endRowNum = startRowNum + int.Parse(rowCount);
                try
                {
                    //SELECT * FROM ( SELECT *,ROW_NUMBER() OVER(ORDER BY id asc) AS row_num
                    //FROM [dbo].[IEVersion] AS tmpa ) AS tmpb
                    //    WHERE row_num BETWEEN 2 AND 5
                    //                    ds = cdb.ReturnDataSet(@"SELECT * FROM
                    //                                                (SELECT TOP " + endRowNum + @"
                    //                                                         [id]
                    //                                                        ,[branch_store_code]
                    //                                                        ,[ie_version_id]
                    //                                                        ,[record_date_time]
                    //                                                        ,[status]
                    //                                                        ,ROW_NUMBER() OVER (ORDER BY ie_version_id ASC, record_date_time ASC) as row_num
                    //                                                    FROM [dbo].[IEVersionRecords]
                    //                                                    WHERE  status = 1 AND ie_version_id >= " + versionLevelFrom + @"
                    //                                                        AND ie_version_id <= " + versionLevelTo + @")
                    //                                                    as tmpT
                    //                                                WHERE row_num BETWEEN " + startRowNum + @"
                    //                                                AND " + endRowNum + @";
                    //                                            SELECT COUNT(1) FROM tmpT;
                    //                              ");
                    ds = cdb.ReturnDataSet(@"SELECT 
                                        [id]
                                    ,[branch_store_code]
                                    ,[ie_version_id]
                                    ,[record_date_time]
                                    ,[status]
                                    ,ROW_NUMBER() OVER (ORDER BY ie_version_id ASC, record_date_time ASC) as row_num
                                FROM [dbo].[IEVersionRecords]
                                WHERE  status <> 0 AND ie_version_id >= " + versionLevelFrom + @"
                                    AND ie_version_id <= " + versionLevelTo + @";
                              ");
                    recordsCount = ds.Tables[0].AsEnumerable().Count();
                    // DataRow dr = ds.Tables[0].NewRow();
                    // DataTable dtT = ds.Tables[0].AsEnumerable().Where(o=> o.Field<int>("row_num") >= startRowNum).Where(o=> o.Field<int>("row_num") < endRowNum).AsDataView<DataRow>().Table;
                    IEnumerable<DataRow> query =
                        from a in ds.Tables[0].AsEnumerable()
                        where int.Parse(a["row_num"].ToString()) >= startRowNum && int.Parse(a["row_num"].ToString()) < endRowNum
                        select a;
                    supposedData = RF.GlobalClass.Utils.Convert.ObjectToJSON(query.CopyToDataTable<DataRow>());

                    retCode = "success";
                    retMsg = "查询成功";
                }
                catch (Exception ex)
                {
                    retMsg = "查询失败" + ex.Message;
                }
                finally
                {
                    cdb.Close();
                }
            }
            catch (Exception ex)
            {
                retMsg = ex.Message;
            }
            ResultMsg rm = new ResultMsg();
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            rm.Obj = supposedData;
            rm.Obj = @"{Obj:""[{\""recordcount\"":" + recordsCount + @",\""pagecount\"":" + (recordsCount / int.Parse(rowCount) + ((recordsCount % int.Parse(rowCount)) == 0 ? 0 : 1)) + @",\""currentpage\"":" + pageNum + @"}]"", Objj:""" + supposedData.Replace("\"", "\\\"") + @"""}";

            // obj [{"recordcount":"432","pagecount":"87","currentpage":"1"}]
            // objj [{"dptid":"001     ","dptdes":"001 解放北路分场","adr1":"成都市解放路一段96、98、100、102号","tel":"83312293","prtdptid":"金牛1","prtdptid1":"金牛1","prtdz":"姚丽","jl":"200       ","ROWSTAT":null},{"dptid":"011     ","dptdes":"011 新鸿路40号分场","adr1":"成都市新鸿路36号","tel":"84333741","prtdptid":"成华区3","prtdptid1":"成华区3","prtdz":"何立琴","jl":"200       ","ROWSTAT":null},{"dptid":"018     ","dptdes":"018 肖家河分场","adr1":"成都高新区肖家河街38号","tel":"85161152","prtdptid":"高新区1","prtdptid1":"高新区1","prtdz":"洪霞","jl":"200       ","ROWSTAT":null},{"dptid":"019     ","dptdes":"019 九里堤中路分场","adr1":"成都市九里堤中路93-95号","tel":"87613201","prtdptid":"金牛1","prtdptid1":"金牛1","prtdz":"姚丽","jl":"200       ","ROWSTAT":null},{"dptid":"020     ","dptdes":"020 石人北路红旗24","adr1":"成都市青羊区石人北路3-5号1楼","tel":"87317268","prtdptid":"青羊区4","prtdptid1":"青羊区4","prtdz":"邓祖彪","jl":"200       ","ROWSTAT":null}]

            supposedData = @"[{""retCode"":""" + retCode + @""",""retMsg"":""" + retMsg + @""",""obj"":""" + supposedData.Replace("\"", "\"\"") + @"""}]" ?? @"[{""retCode"":""failure"",""retMsg"":""提交失败"",""obj"":""""}]";
            return rm;
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            System.Reflection.PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (System.Reflection.PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public DataTable ToDataTable(System.Data.Linq.DataContext ctx, object query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            IDbCommand cmd = ctx.GetCommand(query as IQueryable);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = (SqlCommand)cmd;
            DataTable dt = new DataTable("sd");

            try
            {
                cmd.Connection.Open();
                adapter.FillSchema(dt, SchemaType.Source);
                adapter.Fill(dt);
            }
            finally
            {
                cmd.Connection.Close();
            }
            return dt;
        }

        [WebMethod(Description = "GetIEVersionList.", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg GetIEVersionList(string versionLevelFrom = null, string versionLevelTo = null, string pageNum = null, string rowCount = null, string TimeStamp = "", string Sign = "", string supposedData = null)
        {
            string retCode = "failure";
            string retMsg = "提交失败";
            versionLevelFrom = String.IsNullOrEmpty(versionLevelFrom) ? "1" : versionLevelFrom;
            versionLevelTo = String.IsNullOrEmpty(versionLevelTo) ? "11" : versionLevelTo;
            try
            {
                if (TimeStamp == Sign || carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", TimeStamp) != Sign)
                {
                    retMsg = "签名错误";
                    throw new FormatException(retMsg);
                }
                else { }
                String connectionStr = "Data Source=192.10.200.5;  Initial Catalog=syslog; User ID=" + dbUserNames["192.10.200.5"]["User ID"] + "; Password=" + dbUserNames["192.10.200.5"]["Password"] + ";";
                RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(connectionStr);
                DataSet ds = new DataSet();
                pageNum = String.IsNullOrEmpty(pageNum) ? "1" : pageNum;
                rowCount = String.IsNullOrEmpty(rowCount) ? "10" : rowCount;
                int startRowNum = (int.Parse(pageNum) - 1) * int.Parse(rowCount) + 1;
                int endRowNum = startRowNum + int.Parse(rowCount);
                try
                {
                    ds = cdb.ReturnDataSet(@"SELECT * FROM
                                                (SELECT TOP " + versionLevelTo + @"
                                                        [id]
                                                        ,[name]
                                                        ,ROW_NUMBER() OVER (ORDER BY id ASC) as row_num
                                                    FROM [dbo].[IEVersion]
                                                    WHERE  id >= " + versionLevelFrom + @"
                                                        AND id <= " + versionLevelTo + @" )
                                                as tmpT
                                                WHERE row_num BETWEEN " + startRowNum + @"
                                                AND " + endRowNum + @"");
                    supposedData = RF.GlobalClass.Utils.Convert.ObjectToJSON(ds.Tables[0]);
                    retCode = "success";
                    retMsg = "查询成功";
                }
                catch (Exception ex)
                {
                    retMsg = "查询失败" + ex.Message;
                }
                finally
                {
                    cdb.Close();
                }
            }
            catch (Exception ex)
            {
            }
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            supposedData = @"[{""retCode"":""" + retCode + @""",""retMsg"":""" + retMsg + @""",""obj"":""" + supposedData.Replace("\"", "\"\"") + @"""}]" ?? @"[{""retCode"":""failure"",""retMsg"":""提交失败"",""obj"":""""}]";

            return rm;
        }

        [WebMethod(Description = "PrepareForIE8Downloading.", CacheDuration = CacheTime)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public ResultMsg PrepareForIE8Downloading(string Code = null, string TimeStamp = "", string Sign = "", string supposedData = null)
        {
            string retCode = "failure";
            string retMsg = "提交失败";
            Code = String.IsNullOrEmpty(Code) ? "1" : Code;
            try
            {
                if (TimeStamp == Sign || carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", TimeStamp) != Sign)
                {
                    retMsg = "签名错误";
                    throw new FormatException(retMsg);
                }
                else { }
                String connectionStr = "Data Source=192.10.200.5;  Initial Catalog=syslog; User ID=" + dbUserNames["192.10.200.5"]["User ID"] + "; Password=" + dbUserNames["192.10.200.5"]["Password"] + ";";
                RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(connectionStr);
                DataSet ds = new DataSet();
                try
                {
                    ds = cdb.ReturnDataSet(@"  UPDATE [syslog].[dbo].[IEVersionRecords]
                           SET [status] = 2
                         WHERE [branch_store_code] = N'" + Code + @"'");
                    supposedData = RF.GlobalClass.Utils.Convert.ObjectToJSON(ds.Tables[0]);
                    retCode = "success";
                    retMsg = "设置成功";
                }
                catch (Exception ex)
                {
                    retMsg = "设置失败" + ex.Message;
                }
                finally
                {
                    cdb.Close();
                }
            }
            catch (Exception ex)
            {
            }
            ResultMsg rm = new ResultMsg();
            rm.Obj = supposedData;
            rm.RetCode = retCode;
            rm.RetMsg = retMsg;
            supposedData = @"[{""retCode"":""" + retCode + @""",""retMsg"":""" + retMsg + @""",""obj"":""" + supposedData.Replace("\"", "\"\"") + @"""}]" ?? @"[{""retCode"":""failure"",""retMsg"":""提交失败"",""obj"":""""}]";

            return rm;
        }

        public partial class ResultMsg
        {

            // private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

            //private string RetCodeField;

            //private string RetMsgField;

            //private object ObjField;

            //private enum CodeField
            //{
            //}

            public string RetCode;

            public string RetMsg;

            public object Obj;

            public enum Code
            {
                failure =  rf.RF.GlobalClass.Const.ValidationResult.Failed,
                success = rf.RF.GlobalClass.Const.ValidationResult.Passed
            }
        }

    }
}