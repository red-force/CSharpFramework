extern alias carfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAExample
{
    public partial class WFExample: System.Web.UI.Page
    {
        internal GB.Authorization GBA;
        internal GB.WebReference GBW;
        DataSet dataSet;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dss = RequestBus.GetPutData(Request: Request);
            string moduleIDStr = String.Empty;
            if (!dss.TryGetValue("PageRoleID", out moduleIDStr))
            {
                moduleIDStr = ModuleID.Value;
            }
            else
            {
                ModuleID.Value = moduleIDStr;
                vEQ_ModuleID.Text = moduleIDStr;
                vEL_ModuleID.Text = moduleIDStr;
            }
            GBA = RequestBus.getGBA(Response: Response, moduleID: moduleIDStr, openName: "WangZhi", methodName: "");// getGBA(Response, ModuleID.Value);
            GBW = RequestBus.getGBW(Response: Response);// getGBW(Response);

            dataSet = new DataSet();
            if (!IsPostBack)
            {
                InitPage();
            }
            else
            {
                ReLoadPage();
            }
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

        protected void MultiViewMenu_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dss = RequestBus.GetPutData(Request: Request);
            string check = String.Empty;
            string change = string.Empty;
            string view = String.Empty;
            int viewIndex = 0;

            if (dss.TryGetValue("go", out check))
            {
                switch (check)
                {
                    default: break;
                }
            }
            else { }
            if (dss.TryGetValue("do", out check))
            {
                switch (check)
                {
                    //case "addJobTransfer":
                    //    addJobTransfer();
                    //    break;
                    //case "checkJobTransfer":
                    //    checkJobTransfer();
                    //break;
                    default: break;
                }
            }
            //else if (dss.TryGetValue("ChangeExampleInformationQueryItemListPerPageRowCountTo", out change))
            //{
            //        DrawItemListTable();
            //     // reloadMainInformationQueryItemList(change);
            //}
            else if (dss.TryGetValue("ChangeExampleInformationQueryItemListPageTo", out change))
            {
                RFDataTable rfdteiqil = new RFDataTable("vELTableExampleInformationQueryItemList", request: Request, response: Response);
                rfdteiqil.BeforePageChange += new RFDataTable.BeforePageChangeHandler(rfdteiqil_BeforePageChange);
                rfdteiqil.WhenPageChange += new RFDataTable.WhenPageChangeHandler(rfdteiqil_WhenPageChange);
                rfdteiqil.AfterPageChange += new RFDataTable.AfterPageChangeHandler(delegate(object o, RFDataTable.EventArgsForAfterPageChange _e)
                {
                    return !rfdteiqil_AfterPageChange(o, _e);
                });
                rfdteiqil.ChangePageTo(currPage: change
                    , perPageRowCount: "0"
                    , dataTableName: "TableExampleInformationQueryItemList"
                    , dataTableNamespace: "ExampleList"
                    , columnKeyIDPair: new Dictionary<string, string> {
                        { "RowCount", "id" }
                        , { "Code", "code" }
                        , { "Name", "name" }
                        , { "Status", "status" }
                        , { "Description", "description" }
                    });
            }
            else if (dss.TryGetValue("view", out view) && !int.TryParse(view, out viewIndex))
            {
                try
                {
                    MultiViewMenu.SetActiveView(MultiViewMenu.FindControl(view) as View);
                }
                catch (Exception ex)
                {
                    MultiViewMenu.ActiveViewIndex = viewIndex;
                }
            }
            else
            {
                try
                {
                    int.TryParse(view, out viewIndex);
                    MultiViewMenu.ActiveViewIndex = viewIndex;
                }
                catch (Exception ex)
                {

                    MultiViewMenu.ActiveViewIndex = MultiViewMenu.Views.Count - 1;
                    RequestBus.jumpToAuthorization(Response: Response);
                }
            }
        }

        #region on active
        protected void ViewEQ_Activate(object sender, EventArgs e)
        {
            DrawItemDropDownList();
        }

        protected void ViewEL_Activate(object sender, EventArgs e)
        {

            Dictionary<String, String> dssPostPut = RequestBus.GetPostPutData(Request: Request);
            InitValueFromRequest(dssPostPut:dssPostPut);
            DrawItemListTable(dssPostPut:dssPostPut);
        }

        #endregion
    }
}