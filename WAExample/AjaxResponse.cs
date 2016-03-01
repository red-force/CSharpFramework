using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    public class AjaxResponse
    {
        public string message { get; set; }

        public string status { get; set; }

        public Dictionary<string, List<Dictionary<string, string>>> data { get; set; }

        public Dictionary<string, string> jumpingData { get; set; }

        public string href { get; set; }

        public Location location { get; set; }
    }

    public partial class RequestBus
    {
        #region ajax response reference

        public static AjaxResponse ajaxResponseOfResultMsgNull(HttpResponse Response)
        {
            AjaxResponse ar = new AjaxResponse();
            ar.status = "failure";
            ar.message = "未连接到 所引用的网络服务，或，所引用的网络服务 返回信息 异常。请稍候再试。";
            Dictionary<string, AjaxResponse> dsar = new Dictionary<string, AjaxResponse>();
            dsar["d"] = ar;
            Response.Write(RF.GlobalClass.Utils.Convert.ObjectToJSON(dsar));
            return ar;
        }

        #endregion

    }
}