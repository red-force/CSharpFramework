extern alias carfs;
extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAExample
{
    public partial class WFExample
    {
        private double IEV;
        float WV;
        string tmpStr = String.Empty;

        #region load page

        protected void InitPage()
        {
#if ! DEBUG
            if ("" == GBA.PowerID)
            {
                // 查询权限失败
            }
            else
            {
#endif
                LoadPage();
#if ! DEBUG
            }
#endif
        }

        protected void ReLoadPage()
        {
            LoadPage();
        }

        protected void LoadPage()
        {
            try
            {
                IEV = RF.GlobalClass.Utils.WebBrowser.getInternetExplorerVersion(Request);
                WV = float.Parse(RF.GlobalClass.Utils.WebBrowser.getPlatform(Request).ToString());
                GBW.CheckIEBrowser(Request: Request);
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
        }

        #endregion

        #region data interface

        private SRExample.ResultMsg GetExampleType()
        {
            SRExample.ResultMsg resultMsg = new SRExample.ResultMsg();
            try
            {
                RequestBus.Passengers psg= new RequestBus.Passengers();
                psg.passenger0 = "0";
                psg.passenger1 = "0";
                psg.passenger2 = "0";
                psg.passenger3 = "3";
                // carfs.CARFSecurer.Program.GetEncrytedMessage(encrytionName: RF.GlobalClass.Utils.Convert.EnumValueToString(enumValue: carfs.RF.GlobalClass.Securer.Names.MD5), originMessage: "");
                resultMsg = GBW.GetSRExampleResultMsg( serviceName:"SRExample",
                methodName: "GetExampleType",
                obj: psg,
                specifiedFields: RequestBus.SRExampleGetRequestSpecifiedFields,
                keyIDpair: RequestBus.SRExampleGetRequestKeyIDPair);
            }
            catch (Exception ex)
            {
            }
            return resultMsg;
            // [{"code":"1","name":"Web"}]
        }

        private SRExample.ResultMsg GetExampleItem(String type = "", String pageNum = "1", String perPageRowCount = "10")
        {
            SRExample.ResultMsg resultMsg = new SRExample.ResultMsg();
            try
            {
                RequestBus.Passengers psg = new RequestBus.Passengers();
                psg.passenger0 = "0";
                psg.passenger1 = pageNum;
                psg.passenger2 = perPageRowCount;
                psg.passenger3 = type;
                resultMsg = GBW.GetSRExampleResultMsg(serviceName: "SRExample",
                methodName: "GetExampleItem",
                obj: psg,
                specifiedFields: RequestBus.SRExampleGetRequestSpecifiedFields,
                keyIDpair: RequestBus.SRExampleGetRequestKeyIDPair);
                // [{"id":"0","code":"0","name":"QueryExample","status":"1","description":"with dropdownlist, view request exmaple","url":""}]'
            }
            catch (Exception ex)
            {
            }
            return resultMsg;
        }

        #endregion

        #region View EQ load drop down list

        private void DrawItemDropDownList()
        {
            Dictionary<String, String> dssPostPut = RequestBus.GetPostPutData(Request: Request);
            SRExample.ResultMsg resultMsg = GetExampleType();
            if (resultMsg.RetCode != RequestBus.RetCode.success.ToString())
            {
                return;
            }
            else { }
            Dictionary<String, List<Dictionary<String, String>>> dsldss = RequestBus.ConvertResultMsgProperty<Dictionary<String, List<Dictionary<String, String>>>>(resultMsg.Obj);
            List<Dictionary<String, String>> ldss = RequestBus.ConvertResultMsgObj(resultMsg.Obj);
            ldss = dsldss.TryGetValue(RFDataTable.ServiceResultMsg.DataRecordsName, out ldss) ? ldss : ldss;
            GB.WebControl.DropDownListBindData(ddl: vEQ_QExampleTypeList, ldss: ldss, dataTextField: "name", dataValueField: "code");
            string exampleType = vEQ_QExampleTypeList.SelectedValue = dssPostPut.TryGetValue("QExampleType", out exampleType) ? exampleType : vEQ_QExampleTypeList.SelectedValue;
        }

        #endregion

        #region View EL 
        #region initValue

        private void InitValueFromRequest(Dictionary<String,String> dssPostPut)
        {
            //String exampleType = dssPostPut.TryGetValue("QExampleTypeList", out exampleType) ? exampleType : String.Empty;
            //vEL_QExampleType.Text = exampleType;
        }
        #endregion
        #region load table

        private void DrawItemListTable(Dictionary<String,String> dssPostPut = null)
        {
            #region prepare data for table
            DataTable dt = new DataTable("ExampleItem", "ExampleList");
            dssPostPut = dssPostPut ?? RequestBus.GetPostPutData(Request: Request);

            string jumpdata = RequestBus.Param("jumpdata");
            List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();

            string tableExampleInformationQueryItemListPerPageRowCount = dssPostPut.TryGetValue("ChangeExampleInformationQueryItemListPerPageRowCountTo", out tmpStr) ? tmpStr : "5";
            string tableExampleInformationQueryItemListCurrPageNum = "1";

            #region set data to the view

            string exampleType = vEL_QExampleType.Text = dssPostPut.TryGetValue("QExampleType", out exampleType) ? exampleType : String.Empty;
            tableExampleInformationQueryItemListPerPageRowCount = vELTableExampleInformationQueryItemList_PerPageRowCount.Text = dssPostPut.TryGetValue("vELTableExampleInformationQueryItemListPerPageRowCount", out tmpStr) ? tmpStr : tableExampleInformationQueryItemListPerPageRowCount;
            tableExampleInformationQueryItemListCurrPageNum = vELTableExampleInformationQueryItemList_CurrPageNum.Text = dssPostPut.TryGetValue("vELTableExampleInformationQueryItemListCurrPageNum", out tmpStr) ? tmpStr : tableExampleInformationQueryItemListCurrPageNum;
            string tableExampleInformationQueryItemListPageTotalCount = vELTableExampleInformationQueryItemList_PageTotalCount.Text = dssPostPut.TryGetValue("vELTableExampleInformationQueryItemListPerPageRowCount", out tmpStr) ? tmpStr : tableExampleInformationQueryItemListCurrPageNum;
            string tableExampleInformationQueryItemListRowTotalCount = vELTableExampleInformationQueryItemList_RowTotalCount.Text = dssPostPut.TryGetValue("vELTableExampleInformationQueryItemListRowTotalCount", out tmpStr) ? tmpStr : "X";

            #endregion

            int perPageRowCount = int.TryParse(tableExampleInformationQueryItemListPerPageRowCount, out perPageRowCount) ? perPageRowCount : 5;

            /*
             * Use RFDataTable to deal with the data.
             * */
            RFDataTable rfdteiqil = new RFDataTable("vELTableExampleInformationQueryItemList", request: Request, response: Response);
            rfdteiqil.BeforePageChange += new RFDataTable.BeforePageChangeHandler(rfdteiqil_BeforePageChange);
            rfdteiqil.WhenPageChange += new RFDataTable.WhenPageChangeHandler(rfdteiqil_WhenPageChange);
            rfdteiqil.AfterPageChange += new RFDataTable.AfterPageChangeHandler(rfdteiqil_AfterPageChange);
            dataSet.Tables.Add(
                rfdteiqil.ChangePageTo(
                    currPage: tableExampleInformationQueryItemListCurrPageNum,
                    perPageRowCount: perPageRowCount.ToString(),
                    dssPostData: dssPostPut,
                    dataTableName: "TableExampleInformationQueryItemList",
                    dataTableNamespace: "ExampleList",
                    columnKeyIDPair: new Dictionary<string, string> {
                        { "RowCount", "id" }
                        , { "Code", "code" }
                        , { "Name", "name" }
                        , { "Status", "status" }
                        , { "Description", "description" }
                    }
                    , t: vELTableExampleInformationQueryItemList
                )
            );
#endregion

            #region draw data table with data
            try
            {
                RF.GlobalClass.WebForm.fillTableAccordingToData(vELTableExampleInformationQueryItemList, dataSet.Tables["TableExampleInformationQueryItemList", "ExampleList"]);
            }
            catch (Exception ex) { }
            #endregion
        }

        bool rfdteiqil_BeforePageChange(object o, RFDataTable.EventArgsForBeforePageChange e)
        {
            try
            {
                string currPageNum = e.CurrPageNum;
                string perPageRowCount = e.PerPageRowCount;
                string exampleType = (e.postData.TryGetValue("QExampleTypeList", out exampleType) ? exampleType : String.Empty).Replace("-", "");
                try
                {
                    vEL_QExampleType.Text = exampleType;
                }
                catch (Exception exa)
                {
                }
                // SRExample.ResultMsg cwrRsultMsg = RequestAttendanceRecordsQueryListData(beginDate: beginDate, endDate: endDate, nowPage: currPageNum, pageSize: perPageRowCount);
                SRExample.ResultMsg resultMsg = GetExampleItem(type: exampleType, pageNum: currPageNum, perPageRowCount: perPageRowCount);
                if (resultMsg.RetCode != RequestBus.RetCode.success.ToString())
                {
                    
                }
                else { }
                // List<Dictionary<String, String>> ldss = RequestBus.ConvertResultMsgObj(resultMsg.Obj);

                // (o as RFDataTable).ResultMsgFromService.Obj = resultMsg.Obj;
                (o as RFDataTable).ResultMsgFromService.Obj = resultMsg.Obj;
                (o as RFDataTable).ResultMsgFromService.RetCode = resultMsg.RetCode;
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        bool rfdteiqil_WhenPageChange(object o, RFDataTable.EventArgsForWhenPageChange e)
        {
            return true;
        }
        bool rfdteiqil_AfterPageChange(object o, RFDataTable.EventArgsForAfterPageChange e)
        {
            //RFDataTable rft = (o as RFDataTable);
            //List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
            //if (e.ResultDataForAjaxRequest.data.TryGetValue(rft.Name, out ldss))
            //{
            //    RequestBus.ChangeKeyNameOfLDSS("empid", "EmployeeCode", ldss);
            //    RequestBus.ChangeKeyNameOfLDSS("xm", "EmployeeName", ldss);
            //    RequestBus.ChangeKeyNameOfLDSS("code", "Code", ldss);
            //    RequestBus.ChangeKeyNameOfLDSS("time", "RegisterTime", ldss);
            //}
            //else { }
            return false;
        }

        #endregion
        #endregion

        #region method

        #region View EQ
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static AjaxResponse TurnToExampleView(string data)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                Dictionary<String, String> dssData = rf.RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<String, String>>(Uri.UnescapeDataString(data));

                //ajaxResponse = "{status:'failure',message:'查询失败',data:{a:'A'}}";
                ajaxResponse.status = RequestBus.RetCode.success.ToString();
                ajaxResponse.message = "查询成功";
                Location location = new Location();
                //location.protocol = "http:";
                //location.hostname = "localhost";
                //location.port = "1352";
                //location.pathname = "";
                location.search = "?view=ViewEL";
                //location.hash = "";
                ajaxResponse.location = location;
                Dictionary<string, List<Dictionary<string, string>>> ajaxResponseData = new Dictionary<string, List<Dictionary<string, string>>>();
                // var a = (new List<Dictionary<string,string>>{new Dictionary<string, string> { { "a", "A" } }}).ToList<Dictionary<string,string>>();
                // var a = (new List<Dictionary<string,string>>{new Dictionary<string, string> { { "a", "A" } }}).ToList<Dictionary<string,string>>();
                ajaxResponseData["info"] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { { "author", "dear.will" } } });
                ajaxResponseData[RequestBus.Param("jumpdata")] = new List<Dictionary<string, string>> { dssData };
                ajaxResponse.jumpingData = dssData;
                ajaxResponse.data = ajaxResponseData;
            }
            catch (Exception ex)
            {
            }
            return ajaxResponse;
        }
        #endregion

        #region View EL

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static AjaxResponse ChangeExampleInformationQueryItemListPerPageRowCountTo(string data)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                Dictionary<String, String> dssData = rf.RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<String, String>>(Uri.UnescapeDataString(data));
                //ajaxResponse = "{status:'failure',message:'查询失败',data:{a:'A'}}";
                ajaxResponse.status = RequestBus.RetCode.success.ToString();
                ajaxResponse.message = "";
                Location location = new Location();
                //location.protocol = "http:";
                //location.hostname = "localhost";
                //location.port = "1352";
                //location.pathname = "";
                location.search = "?view=ViewEL&ChangeExampleInformationQueryItemListPerPageRowCountTo=";
                //location.hash = "";
                ajaxResponse.location = location;
                Dictionary<string, List<Dictionary<string, string>>> ajaxResponseData = new Dictionary<string, List<Dictionary<string, string>>>();
                ajaxResponseData["info"] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { { "author", "dear.will" } } });
                ajaxResponseData[RequestBus.Param("jumpdata")] = new List<Dictionary<string, string>> { dssData };
                ajaxResponse.jumpingData = dssData;
                ajaxResponse.data = ajaxResponseData;
            }
            catch (Exception ex)
            {
            }
            return ajaxResponse;
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static AjaxResponse TurnToBackFromExampleListView(string data)
        {
            AjaxResponse ajaxResponse = new AjaxResponse();
            try
            {
                Dictionary<String, String> dssData = rf.RF.GlobalClass.Utils.Convert.JSONToObject<Dictionary<String, String>>(Uri.UnescapeDataString(data));
                //ajaxResponse = "{status:'failure',message:'查询失败',data:{a:'A'}}";
                ajaxResponse.status = RequestBus.RetCode.success.ToString();
                ajaxResponse.message = "";
                Location location = new Location();
                //location.protocol = "http:";
                //location.hostname = "localhost";
                //location.port = "1352";
                //location.pathname = "";
                location.search = "?view=ViewEQ";
                //location.hash = "";
                ajaxResponse.location = location;
                Dictionary<string, List<Dictionary<string, string>>> ajaxResponseData = new Dictionary<string, List<Dictionary<string, string>>>();
                ajaxResponseData["info"] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { { "author", "dear.will" } } });
                ajaxResponseData[RequestBus.Param("jumpdata")] = new List<Dictionary<string, string>> { dssData };
                ajaxResponse.jumpingData = dssData;
                ajaxResponse.data = ajaxResponseData;
            }
            catch (Exception ex)
            {
            }

            return ajaxResponse;
        }
        #endregion


        #endregion
    }
}