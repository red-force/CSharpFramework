using System;
namespace WAExample
{
    interface IRFDataTable
    {
        event RFDataTable.AfterPageChangeHandler AfterPageChange;
        event RFDataTable.AfterPageDataRequestHandler AfterPageDataRequest;
        event RFDataTable.BeforePageChangeHandler BeforePageChange;
        System.Data.DataTable ChangePageTo(string currPage = "0", string perPageRowCount = "0", System.Collections.Generic.Dictionary<string, string> dssPostData = null, string dataTableName = "DataTableName", string dataTableNamespace = "DataTableNameSpace", System.Collections.Generic.Dictionary<string, string> columnKeyIDPair = null, System.Web.UI.WebControls.Table t = null);
        void ChangePageToFirst();
        void ChangePageToLast();
        void ChangePageToNext();
        void ChangePageToPrev();
        string CurrPageIndexKey { get; set; }
        string CurrPageNumKey { get; set; }
        System.Data.DataTable DataTable { get; set; }
        string InfoName { get; set; }
        string Name { get; set; }
        bool OnAfterPageChange(RFDataTable.EventArgsForAfterPageChange e);
        bool OnAfterPageDataRequest(RFDataTable.EventArgsForAfterPageDataRequest e);
        bool OnBeforePageChange(RFDataTable.EventArgsForBeforePageChange e);
        bool OnPageChange(RFDataTable.EventArgsForWhenPageChange e);
        string PageFirstKey { get; set; }
        string PageLastKey { get; set; }
        string PageNextKey { get; set; }
        string PagePrevKey { get; set; }
        string PageTotalCountKey { get; set; }
        string PerPageRowCountKey { get; set; }
        System.Web.HttpRequest Request { get; set; }
        RFDataTable.ServiceResultMsg RequestPageData(System.Collections.Generic.Dictionary<string, string> dss, string currPageNum, string perPageRowCount);
        System.Web.HttpResponse Response { get; set; }
        AjaxResponse ResultDataToAjaxResponse { get; set; }
        RFDataTable.ServiceResultMsg ResultMsgFromService { get; set; }
        string RowCheckedStatusKey { get; set; }
        string RowTotalCountKey { get; set; }
        event RFDataTable.WhenPageChangeHandler WhenPageChange;
    }
}
