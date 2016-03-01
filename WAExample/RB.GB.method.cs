extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    public partial class RequestBus
    {
        public static GB.WebReference getGBW(HttpResponse Response = null)
        {
            // return pir.WAPublicInformationReference.RequestBus.getGBW(Response: Response) as WAPublicInformationReferenceB.GB.WebReference;
            GB.WebReference GBW = new GB.WebReference(response: Response);
            return GBW;
        }

        public static GB.Authorization getGBA(HttpResponse Response = null, string moduleID = "0057", string openName = "WangZhi", string methodName = "")
        {
            // return pir.WAPublicInformationReference.RequestBus.getGBA(Response: Response, moduleID: moduleID, openName: openName, methodName: methodName) as WAPublicInformationReferenceB.GB.Authorization;
            GB.Authorization GBA = new GB.Authorization(response: Response);
            GBA.ModuleID = moduleID;
            GBA.OpenName = openName;
            GBA.MethodName = methodName;
            GBA.RequestUserSession();
            GBA.RequestPowerID();
            return GBA;
        }

        public static void jumpToAuthorization(HttpResponse Response = null)
        {
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("WebAppOfficeNoAuthorizationPageURL") ?? "newitems/nopower.htm"); // newitems/nopower.htm
        }

        public enum RetCode
        {
            failure = rf.RF.GlobalClass.Const.ValidationResult.Failed,
            success = rf.RF.GlobalClass.Const.ValidationResult.Passed
        }
    }
}