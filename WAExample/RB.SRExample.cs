extern alias carfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    public partial class RequestBus
    {
        public static string SRExampleGetRequest(object[] objectAry) // object obj, Dictionary<string, string> specifiedFields, Dictionary<string, string> keyIDpair = null})
        {
            Object obj = objectAry.GetValue(0);
            Dictionary<String, String> specifiedFields = objectAry.GetValue(1) as Dictionary<String, String>;
            Dictionary<String, String> keyIDpair = objectAry.GetValue(2) as Dictionary<String, String>;

            Passengers p = obj as Passengers;

            Dictionary<string, string> openWith = new Dictionary<string, string>();

            #region comment
            /*
            签名：

            */
            #endregion

            #region specifiedFields default value
            specifiedFields = specifiedFields ?? SRExampleGetRequestSpecifiedFields;
            #endregion

            keyIDpair = keyIDpair ?? SRExampleGetRequestKeyIDPair;
            RequestBus.PackagePassengersToDSS(p: p, specifiedFields: specifiedFields, openWith: out openWith, keyIDpair: keyIDpair);

            p.passenger0 = carfs.CARFSecurer.Program.GetEncrytedMessage("QADHOAEBPEUISDTAPJOPNGOERINRVF", RF.GlobalClass.Utils.Convert.ObjectToJSON(openWith));

            #region modified in programming of SRExample
            p.GetType().GetProperties().Select(delegate(System.Reflection.PropertyInfo pi)
            {
                if (keyIDpair.ContainsKey(pi.Name))
                {
                    string kipValue = keyIDpair[pi.Name];
                    if (!openWith.ContainsKey(kipValue) && !String.IsNullOrEmpty(kipValue))
                    {
                        openWith.Add(kipValue, pi.GetValue(p, null) as String);
                    }
                    else { }
                    //if (specifiedFields.ContainsKey(keyIDpair[pi.Name]))
                    //{
                    //    if (!openWith.ContainsKey(specifiedFields[keyIDpair[pi.Name]]))
                    //    {
                    //        openWith.Add(specifiedFields[keyIDpair[pi.Name]], pi.GetValue(p, null) as String);
                    //    }
                    //    else { }
                    //}
                    //else { }
                }
                else if (keyIDpair.Count == 0 && !openWith.ContainsKey(pi.Name))
                {
                    openWith.Add(pi.Name, pi.GetValue(p, null) as String);
                }
                else { }
                return pi;
            }).ToArray();
            return RF.GlobalClass.Utils.Convert.ObjectToJSON(openWith);
            // return RF.GlobalClass.Utils.Convert.ObjectToJSON(p); 
            #endregion

        }

        public static Dictionary<string, string> SRExampleGetRequestSpecifiedFields = new Dictionary<string, string>()
        {
             {"PageNum","PageNum"}
            ,{"PerPageRowCount","PerPageRowCount"}
            ,{"Type","Type"}
        };

        public static Dictionary<string, string> SRExampleGetRequestKeyIDPair = new Dictionary<string, string>()
        {
             {"passenger0","Sign"}
            ,{"passenger1","PageNum"}
            ,{"passenger2","PerPageRowCount"}
            ,{"passenger3","Type"}
        };


    }
}