using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    public class RFDataTable : WAExample.IRFDataTable
    {
        public RFDataTable()
        {
        }
        public RFDataTable(HttpRequest request, HttpResponse response)
        {
            Request = request;
            Response = response;
        }
        public RFDataTable(String name, HttpRequest request, HttpResponse response)
        {
            Name = name;
            Request = request;
            Response = response;
        }
        #region service result message

        public partial class ServiceResultMsg : IServiceResultMsg
        {
            public string DefaultSuccessRetCode = "success";
            public string RetCode { get; set; }
            public object Obj { get; set; }
            private static string _RecordCountName = "RowTotalCount";

            public static string RecordCountName
            {
                get { return ServiceResultMsg._RecordCountName; }
                set { ServiceResultMsg._RecordCountName = value; }
            }

            private static string _PageCountName = "PageTotalCount";

            public static string PageCountName
            {
                get { return ServiceResultMsg._PageCountName; }
                set { ServiceResultMsg._PageCountName = value; }
            }

            private static string _CurrentPageName = "PageNum";

            public static string CurrentPageName
            {
                get { return ServiceResultMsg._CurrentPageName; }
                set { ServiceResultMsg._CurrentPageName = value; }
            }

            private static string _DataRecordsName = "Records";
            public static string DataRecordsName
            {
                get { return ServiceResultMsg._DataRecordsName; }
                set { ServiceResultMsg._DataRecordsName = value; }
            }

            private static string _DataInfoName = "Info";
            public static string DataInfoName
            {
                get { return ServiceResultMsg._DataInfoName; }
                set { ServiceResultMsg._DataInfoName = value; }
            }
        }

        #endregion

        #region get data source

        #region Prepare After Page Data Request  Event
        /// <summary>
        /// EventArgs For After Page Data Request
        /// </summary>
        public class EventArgsForAfterPageDataRequest : EventArgs
        {
            public EventArgsForAfterPageDataRequest(ServiceResultMsg srm, Dictionary<string, string> dss, string currPageNum, string perPageRowCount)
            {
                serviceResultMsg = srm;
                postData = dss;
                CurrPageNum = currPageNum;
                PerPageRowCount = perPageRowCount;
            }

            public Dictionary<string, string> postData { get; set; }

            public string CurrPageNum { get; set; }

            public string PerPageRowCount { get; set; }

            public ServiceResultMsg serviceResultMsg { get; set; }
        }

        public delegate Boolean AfterPageDataRequestHandler(object o, EventArgsForAfterPageDataRequest e);

        public event AfterPageDataRequestHandler AfterPageDataRequest;

        /// <summary>
        /// the bridge to trigger AfterPageDataRequestEvent
        /// </summary>
        /// <param name="e">EventArgsForAfterPageDataRequest</param>
        /// <returns></returns>
        public Boolean OnAfterPageDataRequest(EventArgsForAfterPageDataRequest e)
        {
            Boolean result = true;
            try
            {
                if (BeforePageChange != null)
                {
                    result = AfterPageDataRequest(this, e);
                }
                else { }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        public ServiceResultMsg RequestPageData(Dictionary<string, string> dss, string currPageNum, string perPageRowCount)
        {
            ServiceResultMsg srm = new ServiceResultMsg();
            srm.RetCode = "00";
            srm.Obj =
                new Dictionary<string, object>{
                    {"Obj", new List<Dictionary<string, string>>{
                        new Dictionary<string, string> {
                                    {ServiceResultMsg.RecordCountName,"0"},
                                    {ServiceResultMsg.PageCountName,"0"},
                                    {ServiceResultMsg.CurrentPageName,currPageNum}
                                }
                        }
                    },
                    { "Objj", new List<Dictionary<string, string>> { 
                                new Dictionary<string, string> { 
                                    {"A","here is A1" + currPageNum},
                                    {"B","here is B1" + currPageNum}
                                },new Dictionary<string, string> { 
                                    {"A","here is A2" + currPageNum},
                                    {"B","here is B2" + currPageNum}
                                },new Dictionary<string, string> { 
                                    {"A","here is A3" + currPageNum},
                                    {"B","here is B3" + currPageNum}
                                },new Dictionary<string, string> { 
                                    {"A","here is A4" + currPageNum},
                                    {"B","here is B4" + currPageNum}
                                },new Dictionary<string, string> { 
                                    {"A","here is A5" + currPageNum},
                                    {"B","here is B5" + currPageNum}
                                }
                            }
                    }
                };
            #region trigger After PageDataRequest event
            EventArgsForAfterPageDataRequest eventArgsForAfterPageDataRequest = new EventArgsForAfterPageDataRequest(srm: srm, dss: dss, currPageNum: currPageNum, perPageRowCount: perPageRowCount);
            if (false == OnAfterPageDataRequest(eventArgsForAfterPageDataRequest))
            {
                return srm;
            }
            else { }
            #endregion
            return srm;
        }

        #endregion

        #region ChangePage

        public void ChangePageToFirst()
        {
            ChangePageTo(currPage: "First");
        }

        public void ChangePageToPrev()
        {
            ChangePageTo(currPage: "Prev");
        }

        public void ChangePageToNext()
        {
            ChangePageTo(currPage: "Next");
        }

        public void ChangePageToLast()
        {
            ChangePageTo(currPage: "Last");
        }

        #region Prepare Before Page Change Event
        /// <summary>
        /// EventArgs For Before Page Change
        /// </summary>
        public class EventArgsForBeforePageChange : EventArgs
        {
            public EventArgsForBeforePageChange(Dictionary<string, string> dss, string currPageNum, string perPageRowCount)
            {
                postData = dss;
                CurrPageNum = currPageNum;
                PerPageRowCount = perPageRowCount;
            }

            public Dictionary<string, string> postData { get; set; }

            public string CurrPageNum { get; set; }

            public string PerPageRowCount { get; set; }
        }

        public delegate Boolean BeforePageChangeHandler(object o, EventArgsForBeforePageChange e);

        public event BeforePageChangeHandler BeforePageChange;

        /// <summary>
        /// the bridge to trigger BeforePageChangeEvent
        /// </summary>
        /// <param name="e">EventArgsForPageChange</param>
        /// <returns></returns>
        public Boolean OnBeforePageChange(EventArgsForBeforePageChange e)
        {
            Boolean result = true;
            try
            {
                if (BeforePageChange != null)
                {
                    result = BeforePageChange(this, e);
                }
                else { }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Prepare When Page Change Event
        /// <summary>
        /// EventArgs For When Page Change
        /// </summary>
        public class EventArgsForWhenPageChange : EventArgs
        {
            public EventArgsForWhenPageChange(RFDataTable.ServiceResultMsg resultMsg)
            {
                ResultMsg = resultMsg;
            }
            public ServiceResultMsg ResultMsg { get; set; }
        }

        public delegate Boolean WhenPageChangeHandler(object o, EventArgsForWhenPageChange e);

        public event WhenPageChangeHandler WhenPageChange;

        /// <summary>
        /// the bridge to trigger WhenPageChangeEvent
        /// </summary>
        /// <param name="e">EventArgsForPageChange</param>
        public Boolean OnPageChange(EventArgsForWhenPageChange e)
        {
            Boolean result = true;
            try
            {
                if (WhenPageChange != null)
                {
                    result = WhenPageChange(this, e);
                }
                else { }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region Prepare After Page Change Event
        /// <summary>
        /// EventArgs For After Page Change
        /// </summary>
        public class EventArgsForAfterPageChange : EventArgs
        {
            public EventArgsForAfterPageChange(AjaxResponse resultDataForAjaxRequest)
            {
                ResultDataForAjaxRequest = resultDataForAjaxRequest;
            }
            public AjaxResponse ResultDataForAjaxRequest { get; set; }
        }

        public delegate Boolean AfterPageChangeHandler(object o, EventArgsForAfterPageChange e);

        public event AfterPageChangeHandler AfterPageChange;

        /// <summary>
        /// the bridge to trigger AfterPageChangeEvent
        /// </summary>
        /// <param name="e">EventArgsForAfterPageChange</param>
        public Boolean OnAfterPageChange(EventArgsForAfterPageChange e)
        {
            Boolean result = true;
            try
            {
                if (AfterPageChange != null)
                {
                    result = AfterPageChange(this, e);
                }
                else { }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        #endregion

        public void _ChangePageTo(string currPage = "0", string perPageRowCount = "0", Dictionary<string, string> dssPostData = null)
        {
            try
            {
                string date = string.Empty;
                Dictionary<string, string> dss = RequestBus.GetPostData(Request: Request) ?? dssPostData;

                AjaxResponse ar = new AjaxResponse();
                #region set status
                ar.status = "success";
                // ar.location = new Location();
                // ar.location.search = "?view=1&date=" + date;
                #endregion
                Dictionary<string, AjaxResponse> dsar = new Dictionary<string, AjaxResponse>();
                Dictionary<string, List<Dictionary<string, string>>> resultData = new Dictionary<string, List<Dictionary<string, string>>>();
                List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
                Boolean carryOnAfterEvent = false;
                #region TableInfo
                try
                {
                    currPage = RequestBus.GetPageNum(currPage, dss: dss, currPageNumKeyName: this.Name + this.CurrPageNumKey, pageTotalCountKeyName: this.Name + this.PageTotalCountKey);
                    perPageRowCount = "0" == perPageRowCount ? (dss.TryGetValue(this.Name + this.PerPageRowCountKey, out perPageRowCount) ? perPageRowCount : "5") : perPageRowCount;

                    this.ResultMsgFromService = RequestPageData(dss, currPageNum: currPage, perPageRowCount: perPageRowCount);

                    #region trigger Before PageChange event
                    EventArgsForBeforePageChange eventArgsForBeforePageChange = new EventArgsForBeforePageChange(dss, currPageNum: currPage, perPageRowCount: perPageRowCount);
                    if (false == OnBeforePageChange(eventArgsForBeforePageChange))
                    {
                        return;
                    }
                    else { }
                    #endregion

                    RFDataTable.ServiceResultMsg resultMsg = this.ResultMsgFromService;
                    if (null == resultMsg)
                    {
                    }
                    else if ("00" != resultMsg.RetCode)
                    {
                    }
                    else
                    {
                        #region trigger WhenPageChange event
                        EventArgsForWhenPageChange eventArgsForWhenPageChange = new EventArgsForWhenPageChange(this.ResultMsgFromService);
                        carryOnAfterEvent = OnPageChange(eventArgsForWhenPageChange);
                        #endregion

                        if (false != carryOnAfterEvent)
                        {
                            #region fill data to result
                            //ldss = RequestBus.ConvertResultMsgObjj(resultMsg.Obj);
                            // [{"dptid":"合计      ","zs":"1212","ROWSTAT":null},{"dptid":"6156      ","zs":"11","ROWSTAT":null},{"dptid":"1672      ","zs":"8","ROWSTAT":null}]
                            ldss = RequestBus.ConvertResultMsgObjj(resultMsg.Obj);
                            resultData[Name] = ldss;
                            ldss = RequestBus.ConvertResultMsgObj(resultMsg.Obj);
                            // [{"recordcount":"744","pagecount":"248","currentpage":"1"}]
                            string recordCount = "", pageCount = "", currentPage = "";
                            ldss.ForEach(delegate(Dictionary<string, string> _dss)
                            {
                                _dss.TryGetValue(ServiceResultMsg.RecordCountName, out recordCount);
                                _dss.TryGetValue(ServiceResultMsg.PageCountName, out pageCount);
                                _dss.TryGetValue(ServiceResultMsg.CurrentPageName, out currentPage);
                            });
                            currentPage = currentPage ?? currPage;
                            int currentPageIndex = 0;
                            int.TryParse(currentPage.Trim(), out currentPageIndex);
                            currentPageIndex = --currentPageIndex;
                            int rowTotalCount = 0;
                            int pageTotalCount = 0;
                            int.TryParse(recordCount.Trim(), out rowTotalCount);
                            pageCount = pageCount == "0" ? "1" : pageCount;
                            int.TryParse(pageCount.Trim(), out pageTotalCount);
                            resultData[InfoName] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { 
                                        { RowTotalCountKey, recordCount},
                                        { PerPageRowCountKey, Math.Max(int.Parse(perPageRowCount),(0== pageTotalCount? 0: ((rowTotalCount)/(pageTotalCount)+(0==(rowTotalCount)%(pageTotalCount)?0:1)))).ToString() },
                                        { PageTotalCountKey,pageCount},
                                        { CurrPageIndexKey, currentPageIndex.ToString() },
                                        { CurrPageNumKey, currentPage},
                                        { RowCheckedStatusKey, "false"}
                                        } });
                            #endregion
                        }
                        else { }
                    }
                }
                catch (Exception ex)
                {
                }
                #endregion
                ar.data = resultData;
                dsar["d"] = ar;
                ResultDataToAjaxResponse = ar;

                #region trigger After PageChange event
                EventArgsForAfterPageChange eventArgsForAfterPageChange = new EventArgsForAfterPageChange(this.ResultDataToAjaxResponse);
                carryOnAfterEvent = OnAfterPageChange(eventArgsForAfterPageChange);
                #endregion

                if (false != carryOnAfterEvent)
                {
                    Response.ClearContent();
                    Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsar));
                    // Response.End();
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                }
                else { }
            }
            catch (Exception ex)
            {
                Response.ClearContent();
                AjaxResponse ar = new AjaxResponse();
                Dictionary<string, AjaxResponse> dsar = new Dictionary<string, AjaxResponse>();
                ar.status = "failure";
                ar.message = "系统异常，请稍后再试。"; // ex.Message;
                dsar["d"] = ar;
                Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsar));
                Response.End();
            }
        }

        public System.Data.DataTable ChangePageTo(string currPage = "0", string perPageRowCount = "0", Dictionary<string, string> dssPostData = null, string dataTableName = "DataTableName", string dataTableNamespace = "DataTableNameSpace", Dictionary<string, string> columnKeyIDPair = null, System.Web.UI.WebControls.Table t = null)
        {
            #region when dataTable Exist
            columnKeyIDPair = (null == columnKeyIDPair) ? new Dictionary<string, string>() : columnKeyIDPair;
            this.DataTable = new System.Data.DataTable(dataTableName, dataTableNamespace);
            columnKeyIDPair.Select(x => this.DataTable.Columns.Add(x.Key)).ToArray();
            #endregion
            try
            {
                string date = string.Empty;
                Dictionary<string, string> dss = dssPostData ?? RequestBus.GetPostData(Request: Request);

                AjaxResponse ar = new AjaxResponse();
                #region set status
                ar.status = "success";
                // ar.location = new Location();
                // ar.location.search = "?view=1&date=" + date;
                #endregion
                Dictionary<string, AjaxResponse> dsar = new Dictionary<string, AjaxResponse>();
                Dictionary<string, List<Dictionary<string, string>>> resultData = new Dictionary<string, List<Dictionary<string, string>>>();
                List<Dictionary<string, string>> ldss = new List<Dictionary<string, string>>();
                Boolean carryOnAfterEvent = false;
                #region TableInfo
                try
                {
                    currPage = RequestBus.GetPageNum(currPage, dss: dss, currPageNumKeyName: this.Name + this.CurrPageNumKey, pageTotalCountKeyName: this.Name + this.PageTotalCountKey);
                    perPageRowCount = "0" == perPageRowCount ? (dss.TryGetValue(this.Name + this.PerPageRowCountKey, out perPageRowCount) ? perPageRowCount : "5") : perPageRowCount;

                    this.ResultMsgFromService = RequestPageData(dss, currPageNum: currPage, perPageRowCount: perPageRowCount);

                    #region when dataTable Exist
                    try
                    {
                        // fill table with empty data
                        int _perPageRowCount = int.TryParse(perPageRowCount, out _perPageRowCount) ? _perPageRowCount : 5;
                        System.Data.DataRow dr = this.DataTable.NewRow();
                        Object[] oAry = new Object[columnKeyIDPair.Count];
                        while (_perPageRowCount-- > 0)
                        {
                            dr = this.DataTable.NewRow();
                            dr.ItemArray = oAry.Clone() as Object[];
                            this.DataTable.Rows.InsertAt(dr, 0);
                        }
                    }
                    catch (Exception exd)
                    {
                    }
                    #endregion

                    #region trigger Before PageChange event
                    EventArgsForBeforePageChange eventArgsForBeforePageChange = new EventArgsForBeforePageChange(dss, currPageNum: currPage, perPageRowCount: perPageRowCount);
                    if (false == OnBeforePageChange(eventArgsForBeforePageChange))
                    {
                        return this.DataTable;
                    }
                    else { }
                    #endregion

                    #region when dataTable Exist

                    #endregion

                    RFDataTable.ServiceResultMsg resultMsg = this.ResultMsgFromService;
                    if (null == resultMsg)
                    {
                    }
                    else if (resultMsg.DefaultSuccessRetCode != resultMsg.RetCode)
                    {
                    }
                    else
                    {
                        #region trigger WhenPageChange event
                        EventArgsForWhenPageChange eventArgsForWhenPageChange = new EventArgsForWhenPageChange(this.ResultMsgFromService);
                        carryOnAfterEvent = OnPageChange(eventArgsForWhenPageChange);
                        #endregion

                        if (false != carryOnAfterEvent)
                        {
                            #region fill data to result
                            resultData = fillResultDataToResultDict(resultMsg, currPage: currPage, perPageRowCount: perPageRowCount);
                            /*
                            //ldss = RequestBus.ConvertResultMsgObjj(resultMsg.Obj);
                            // [{"dptid":"合计      ","zs":"1212","ROWSTAT":null},{"dptid":"6156      ","zs":"11","ROWSTAT":null},{"dptid":"1672      ","zs":"8","ROWSTAT":null}]
                            ldss = RequestBus.ConvertResultMsgObjj(resultMsg.Obj);
                            resultData[Name] = ldss;
                            ldss = RequestBus.ConvertResultMsgObj(resultMsg.Obj);
                            // [{"recordcount":"744","pagecount":"248","currentpage":"1"}]
                            string recordCount = "", pageCount = "", currentPage = "";
                            ldss.ForEach(delegate(Dictionary<string, string> _dss)
                            {
                                _dss.TryGetValue(ServiceResultMsg.RecordCountName, out recordCount);
                                _dss.TryGetValue(ServiceResultMsg.PageCountName, out pageCount);
                                _dss.TryGetValue(ServiceResultMsg.CurrentPageName, out currentPage);
                            });
                            currentPage = currentPage ?? currPage;
                            int currentPageIndex = 0;
                            int.TryParse(currentPage.Trim(), out currentPageIndex);
                            currentPageIndex = --currentPageIndex;
                            int rowTotalCount = 0;
                            int pageTotalCount = 0;
                            int.TryParse(recordCount.Trim(), out rowTotalCount);
                            pageCount = pageCount == "0" ? "1" : pageCount;
                            int.TryParse(pageCount.Trim(), out pageTotalCount);
                            resultData[InfoName] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { 
                                        { RowTotalCountKey, recordCount},
                                        { PerPageRowCountKey, Math.Max(int.Parse(perPageRowCount),(0== pageTotalCount? 0: ((rowTotalCount)/(pageTotalCount)+(0==(rowTotalCount)%(pageTotalCount)?0:1)))).ToString() },
                                        { PageTotalCountKey,pageCount},
                                        { CurrPageIndexKey, currentPageIndex.ToString() },
                                        { CurrPageNumKey, currentPage},
                                        { RowCheckedStatusKey, "false"}
                                        } });
                             * */
                            #endregion
                        }
                        else { }
                    }
                }
                catch (Exception ex)
                {
                }
                #endregion
                ar.data = resultData;
                dsar["d"] = ar;
                ResultDataToAjaxResponse = ar;

                try
                {

                    int _perPageRowCount = 0;
                    List<Dictionary<string, string>> _ldss = new List<Dictionary<string, string>>();
                    string perPageRowCountStr = String.Empty;
                    #region when dataTable Exist

                    try
                    {
                        // get table info
                        if (this.ResultDataToAjaxResponse.data.TryGetValue(this.InfoName, out _ldss) && null != t)
                        {
                            System.Web.UI.ITextControl itcCurrPageNum = null;
                            itcCurrPageNum = (t.FindControl(t.ID + "_" + this.CurrPageNumKey) as System.Web.UI.ITextControl);
                            string tableCurrPageNum = itcCurrPageNum.Text = (_ldss.FirstOrDefault().TryGetValue(this.CurrPageNumKey, out tableCurrPageNum) ? tableCurrPageNum : itcCurrPageNum.Text);
                            int.TryParse((_ldss.FirstOrDefault().TryGetValue(this.PerPageRowCountKey, out perPageRowCountStr) ? perPageRowCountStr : "1"), out _perPageRowCount);
                            string tablePageTotalCount = (t.FindControl(t.ID + "_" + this.PageTotalCountKey) as System.Web.UI.ITextControl).Text = (_ldss.FirstOrDefault().TryGetValue(this.PageTotalCountKey, out tablePageTotalCount) ? tablePageTotalCount : itcCurrPageNum.Text);
                            string tablePerageRowCount = (t.FindControl(t.ID + "_" + this.PerPageRowCountKey) as System.Web.UI.ITextControl).Text = (_ldss.FirstOrDefault().TryGetValue(this.PerPageRowCountKey, out tablePerageRowCount) ? tablePerageRowCount : "5");
                            string tableRowTotalCount = (t.FindControl(t.ID + "_" + this.RowTotalCountKey) as System.Web.UI.ITextControl).Text = (_ldss.FirstOrDefault().TryGetValue(this.RowTotalCountKey, out tableRowTotalCount) ? tableRowTotalCount : "x");
                            #region set paging button status
                            try
                            {

                                System.Web.UI.WebControls.WebControl itcPageLast = (t.FindControl(t.ID + "_" + this.PageLastKey) as System.Web.UI.WebControls.WebControl);
                                System.Web.UI.WebControls.WebControl itcPageNext = (t.FindControl(t.ID + "_" + this.PageNextKey) as System.Web.UI.WebControls.WebControl);
                                System.Web.UI.WebControls.WebControl itcPageFirst = (t.FindControl(t.ID + "_" + this.PageFirstKey) as System.Web.UI.WebControls.WebControl);
                                System.Web.UI.WebControls.WebControl itcPagePrev = (t.FindControl(t.ID + "_" + this.PagePrevKey) as System.Web.UI.WebControls.WebControl);
                                try
                                {
                                    if (int.Parse(tablePageTotalCount) > int.Parse(itcCurrPageNum.Text ?? "0"))
                                    {
                                        itcPageLast.Enabled = true;
                                        itcPageLast.CssClass = itcPageLast.CssClass.Replace("disabled", "");
                                        itcPageNext.Enabled = true;
                                        itcPageNext.CssClass = itcPageNext.CssClass.Replace("disabled", "");
                                    }
                                    else { }
                                }
                                catch (Exception exe)
                                {
                                }
                                try
                                {

                                    if (int.Parse(itcCurrPageNum.Text ?? "0") > 1)
                                    {
                                        itcPageFirst.Enabled = true;
                                        itcPageFirst.CssClass = itcPageFirst.CssClass.Replace("disabled", "");
                                        itcPagePrev.Enabled = true;
                                        itcPagePrev.CssClass = itcPagePrev.CssClass.Replace("disabled", "");
                                    }
                                    else { }
                                }
                                catch (Exception exe)
                                {
                                }

                            }
                            catch (Exception ex)
                            {
                            }
                            #endregion
                        }
                        else { }
                    }
                    catch (Exception exe)
                    {
                    }
                    #endregion

                    #region trigger After PageChange event
                    EventArgsForAfterPageChange eventArgsForAfterPageChange = new EventArgsForAfterPageChange(this.ResultDataToAjaxResponse);
                    carryOnAfterEvent = OnAfterPageChange(eventArgsForAfterPageChange);
                    #endregion

                    try
                    {
                        // fill the data
                        if (this.ResultDataToAjaxResponse.data.TryGetValue(this.Name, out _ldss))
                        {
                            this.DataTable.Rows.Clear();
                            System.Data.DataRow dr = this.DataTable.NewRow();
                            _ldss.ForEach(delegate(Dictionary<String, String> _dss)
                            {
                                dr = this.DataTable.NewRow();

                                dr.ItemArray = columnKeyIDPair.Select((KeyValuePair<string, string> x, int i) => { string _tmpStr = _dss.ContainsKey(x.Value) ? _dss[x.Value] : String.Empty; return _tmpStr; }).ToArray();

                                this.DataTable.Rows.InsertAt(dr, 0);
                            });

                            Object[] oAry = new Object[columnKeyIDPair.Count];
                            if (_perPageRowCount > this.DataTable.Rows.Count)
                            {
                                int tmpCount = _perPageRowCount - this.DataTable.Rows.Count;
                                while (tmpCount-- > 0)
                                {
                                    dr = this.DataTable.NewRow();
                                    dr.ItemArray = oAry.Clone() as Object[];
                                    this.DataTable.Rows.InsertAt(dr, 0);
                                }
                            }
                            else { }
                        }
                        else { }
                    }
                    catch (Exception exf)
                    {
                    }
                }
                catch (Exception exd)
                {
                }

                if (false != carryOnAfterEvent)
                {
                    Response.ClearContent();
                    List<Dictionary<string, string>> _ldss = new List<Dictionary<string, string>>();
                    if (this.ResultDataToAjaxResponse.data.TryGetValue(this.Name, out _ldss))
                    {
                        columnKeyIDPair.Select((KeyValuePair<string, string> x, int i) => {
                            RequestBus.ChangeKeyNameOfLDSS(x.Value, x.Key, _ldss);
                            return x.Value;
                        }).ToArray();
                    }
                    else { }
                    Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsar));
                    // Response.End();
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                }
                else { }
            }
            catch (Exception ex)
            {
                Response.ClearContent();
                AjaxResponse ar = new AjaxResponse();
                Dictionary<string, AjaxResponse> dsar = new Dictionary<string, AjaxResponse>();
                ar.status = "failure";
                ar.message = "系统异常，请稍后再试。"; // ex.Message;
                dsar["d"] = ar;
                Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsar));
                Response.End();
            }
            return this.DataTable;
        }

        /// <summary>
        /// Fill the Result Data to dsldssData
        /// </summary>
        /// <param name="resultMsg"></param>
        /// <param name="currPage"></param>
        /// <param name="perPageRowCount"></param>
        /// <returns></returns>
        public Dictionary<string, List<Dictionary<string, string>>> fillResultDataToResultDict(ServiceResultMsg resultMsg, String currPage = "1", String perPageRowCount = "5")
        {
            Dictionary<string, List<Dictionary<string, string>>> resultData = new Dictionary<string, List<Dictionary<string, string>>>();
            try
            {
                Dictionary<String, List<Dictionary<String, String>>> dldss = RequestBus.ConvertResultMsgProperty<Dictionary<String, List<Dictionary<String, String>>>>(resultMsg.Obj);
                List<Dictionary<String, String>> ldss = dldss.TryGetValue(ServiceResultMsg.DataRecordsName, out ldss) ? ldss : ldss;
                resultData[Name] = ldss;
                ldss = dldss.TryGetValue(ServiceResultMsg.DataInfoName, out ldss) ? ldss : ldss;
                string recordCount = "", pageCount = "", currentPage = "";
                ldss.ForEach(delegate(Dictionary<string, string> _dss)
                {
                    _dss.TryGetValue(ServiceResultMsg.RecordCountName, out recordCount);
                    _dss.TryGetValue(ServiceResultMsg.PageCountName, out pageCount);
                    _dss.TryGetValue(ServiceResultMsg.CurrentPageName, out currentPage);
                });
                currentPage = currentPage ?? currPage;
                int currentPageIndex = 0;
                int.TryParse(currentPage.Trim(), out currentPageIndex);
                currentPageIndex = --currentPageIndex;
                int rowTotalCount = 0;
                int pageTotalCount = 0;
                int.TryParse(recordCount.Trim(), out rowTotalCount);
                pageCount = pageCount == "0" ? "1" : pageCount;
                int.TryParse(pageCount.Trim(), out pageTotalCount);
                resultData[InfoName] = (new List<Dictionary<string, string>> { new Dictionary<string, string> { 
                                        { RowTotalCountKey, recordCount},
                                        { PerPageRowCountKey, Math.Max(int.Parse(perPageRowCount),(0== pageTotalCount? 0: ((rowTotalCount)/(pageTotalCount)+(0==(rowTotalCount)%(pageTotalCount)?0:1)))).ToString() },
                                        { PageTotalCountKey,pageCount},
                                        { CurrPageIndexKey, currentPageIndex.ToString() },
                                        { CurrPageNumKey, currentPage},
                                        { RowCheckedStatusKey, "false"}
                                        } });
            }
            catch (Exception ex)
            {
            }
            return resultData;
        }

        #endregion


        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        public string Name { get; set; }

        private string _InfoName = "Info";
        public string InfoName
        {
            get
            {
                return Name + _InfoName;
            }
            set
            {
                _InfoName = value.Replace(Name, "");
            }
        }

        private string _RowTotalCountKey = "RowTotalCount";

        public string RowTotalCountKey
        {
            get { return _RowTotalCountKey; }
            set { _RowTotalCountKey = value; }
        }

        private string _CurrPageIndexKey = "CurrPageIndex";

        public string CurrPageIndexKey
        {
            get { return _CurrPageIndexKey; }
            set { _CurrPageIndexKey = value; }
        }
        private string _CurrPageNumKey = "CurrPageNum";

        public string CurrPageNumKey
        {
            get { return _CurrPageNumKey; }
            set { _CurrPageNumKey = value; }
        }

        private string _PageTotalCountKey = "PageTotalCount";

        public string PageTotalCountKey
        {
            get { return _PageTotalCountKey; }
            set { _PageTotalCountKey = value; }
        }

        private string _PerPageRowCountKey = "PerPageRowCount";

        public string PerPageRowCountKey
        {
            get { return _PerPageRowCountKey; }
            set { _PerPageRowCountKey = value; }
        }

        private string _RowCheckedStatusKey = "RowCheckedStatus";

        public string RowCheckedStatusKey
        {
            get { return _RowCheckedStatusKey; }
            set { _RowCheckedStatusKey = value; }
        }

        public ServiceResultMsg ResultMsgFromService { get; set; }

        public AjaxResponse ResultDataToAjaxResponse { get; set; }

        private string _pageLastKey = "PageLast";

        public string PageLastKey
        {
            get { return _pageLastKey; }
            set { _pageLastKey = value; }
        }

        private string _pageNextKey = "PageNext";

        public string PageNextKey
        {
            get { return _pageNextKey; }
            set { _pageNextKey = value; }
        }

        private string _pageFirstKey = "PageFirst";

        public string PageFirstKey
        {
            get { return _pageFirstKey; }
            set { _pageFirstKey = value; }
        }

        private string _pagePrevKey = "PagePrev";

        public string PagePrevKey
        {
            get { return _pagePrevKey; }
            set { _pagePrevKey = value; }
        }

        public System.Data.DataTable DataTable { get; set; }
    }
}