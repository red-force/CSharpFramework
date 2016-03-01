using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAExample
{
    /// <summary>
    /// SpecifiedFieldsName: serviceName + interfaceName + specifiedFiledSuffixName;
    /// KeyIDPair: serviceName + interfaceName + keyIDPairSuffixName;
    /// </summary>
    public partial class WAInterfaceTest : System.Web.UI.Page
    {
        GB.Authorization GBA;
        GB.WebReference GBW;
        RF.GlobalClass.WebForm.ControlCollection cc;
        RequestBus.Passengers passengers;
        Dictionary<String, String> dssKeyIDPair = new Dictionary<string, string>();
        Dictionary<String, String> dssSpecifiedFields = new Dictionary<string, string>();
        string serviceName = "SRExample";
        string interfaceName = String.Empty;
        string specifiedFiledSuffixName = "GetRequestSpecifiedFields";
        string keyIDPairSuffixName = "GetRequestKeyIDPair";
        protected void Page_Load(object sender, EventArgs e)
        {
            GBA = RequestBus.getGBA(Response: Response, moduleID: ModuleID.Value, openName: "rfOaService", methodName: "redforce");//getGBA(Response, ModuleID.Value);
            GBW = RequestBus.getGBW(Response: Response);// getGBW(Response);
            passengers = new RequestBus.Passengers();
            Dictionary<string, string> dssPP = RequestBus.GetPostPutData(Request: Request);
            string tmp = String.Empty;
            serviceName = dssPP.TryGetValue("serviceName", out tmp) ? tmp : serviceName;
            interfaceName = dssPP.TryGetValue("interfaceName", out tmp) ? tmp : interfaceName;
            specifiedFiledSuffixName = dssPP.TryGetValue("specifiedFiledSuffixName", out tmp) ? tmp : specifiedFiledSuffixName;
            keyIDPairSuffixName = dssPP.TryGetValue("keyIDPairSuffixName", out tmp) ? tmp : keyIDPairSuffixName;
            Type requestBusType = (new RequestBus()).GetType();
            dssSpecifiedFields = RF.GlobalClass.Utils.Do.MagicField<Dictionary<string, string>>((new RequestBus()), serviceName + interfaceName + specifiedFiledSuffixName) ?? RF.GlobalClass.Utils.Do.MagicField<Dictionary<string, string>>((new RequestBus()), serviceName + specifiedFiledSuffixName) ?? dssSpecifiedFields;
            dssKeyIDPair = RF.GlobalClass.Utils.Do.MagicField<Dictionary<string, string>>((new RequestBus()), serviceName + interfaceName + keyIDPairSuffixName) ?? RF.GlobalClass.Utils.Do.MagicField<Dictionary<string, string>>((new RequestBus()), serviceName + keyIDPairSuffixName) ?? dssKeyIDPair;

            #region interface name
            SRExample.ServiceSoapClient rsc = new SRExample.ServiceSoapClient();
            DropDownList ddlInterfaceName = new DropDownList();
            ddlInterfaceName.ID = "InterfaceName";
            ddlInterfaceName.DataTextField = "txt";
            ddlInterfaceName.DataValueField = "val";
            DataTable dt = new DataTable();
            dt.Columns.Add("txt");
            dt.Columns.Add("val");
            rsc.GetType().GetMethods().Select(x => (x.ReturnType == typeof(SRExample.ResultMsg)) ? (dt.Rows.Add(new object[] { x.Name, x.Name }) == null ? true : true) : false).ToArray();
            /* might meet methods with same name and different return type
            RF.GlobalClass.Utils.Do.getMethodNamesOfObject(CWRCS).Select(delegate(string name)
            {
                // CWRCS.GetType().GetMethods().Select(x=> dt.Rows.Add(new object[]{x,x})).ToArray();
                
                if (CWRCS.GetType().GetMethod(name).ReturnType == typeof(CheckinWebReference.ResultMsg))
                {
                    dt.Rows.Add(new object[]{ name, name });
                }
                else { }
                return name;
            }).ToArray();
            * */
            ddlInterfaceName.DataSource = dt;
            ddlInterfaceName.DataBind();
            ddlInterfaceName.SelectedValue = interfaceName;
            ddlInterfaceName.TextChanged +=new EventHandler(ddlInterfaceName_TextChanged);
            Label lbInterfaceName = new Label();
            lbInterfaceName.Text = "Interface Name:";
            form1.Controls.Add(lbInterfaceName);
            form1.Controls.Add(ddlInterfaceName);
            #endregion
            Label lb = new Label();
            CheckBox cb = new CheckBox();
            TextBox tx = new TextBox();
            Panel pl = new Panel();
            cc = new RF.GlobalClass.WebForm.ControlCollection(RF.GlobalClass.Utils.Do.getPropertyNamesOfObject(passengers).Select(delegate(string _name)
            {
                string name = _name;
                if (dssKeyIDPair.ContainsKey(_name))
                {
                    name = dssKeyIDPair[_name];
                }
                else { }
                lb = new Label();
                lb.Text = name;
                lb.Style.Add(HtmlTextWriterStyle.Margin, "2px 2px 2px 4px");
                cb = new CheckBox();
                cb.ID = "cb_" + name;
                cb.Checked = dssSpecifiedFields.ContainsKey(name);
                cb.ToolTip = "check to use it creating sign name.";
                tx = new TextBox();
                if (cb.Checked)
                {
                    tx.ToolTip = dssSpecifiedFields[name] ?? String.Empty;
                    if (!String.IsNullOrEmpty(tx.ToolTip))
                    {
                        lb.Text = tx.ToolTip;
                    }
                    else { }
                }
                else { }
                tx.ID = name;
                tx.Style.Add(HtmlTextWriterStyle.Margin, "2px 4px 2px 2px");
                switch (_name)
                {
                    case "empid": tx.Text = GBA.UserSession.empid;
                        break;
                    case "mdlid": tx.Text = GBA.ModuleID;
                        break;
                    case "pwrid": tx.Text = GBA.PowerID;
                        break;
                    case "dptid": tx.Text = GBA.UserSession.dptid;
                        break;
                    case "TimeStamp":
                        tx.Text = RequestBus.GetDateTimeString(DateTime.Now);
                        break;
                    case "openName":
                        tx.Text = GBA.OpenName;
                        break;
                    case "methodName":
                        tx.Text = GBA.MethodName;
                        break;
                    case "Sign":
                        tx.Width = Unit.Pixel(300);
                        tx.ReadOnly = true;
                        break;
                    default: break;
                }
                System.Web.UI.HtmlControls.HtmlGenericControl span = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
                span.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                span.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                span.Controls.Add(lb);
                span.Controls.Add(cb);
                span.Controls.Add(tx);
                form1.Controls.Add(span);
                return tx;
            }).ToArray());

            System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            div.Style.Add(HtmlTextWriterStyle.Margin, "4px");

            Button btn = new Button();
            btn.Text = "Generate Sign";
            btn.Click += new EventHandler(btnGenerateSign_Click);
            div.Controls.Add(btn);

            btn = new Button();
            btn.Text = "Send Request";
            btn.Click += new EventHandler(btnSendRequest_Click);
            div.Controls.Add(btn);
            form1.Controls.Add(div);

            div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            div.Style.Add(HtmlTextWriterStyle.Margin, "4px");
            #region result
            lb = new Label();
            lb.Text = "ResultMsg.RetCode:";
            div.Controls.Add(lb);
            tx = new TextBox();
            tx.ID = "ResultMsgRetCode";
            div.Controls.Add(tx);

            div.Controls.Add(new Label());
            lb = new Label();
            lb.Text = "ResultMsg.RetMsg:";
            div.Controls.Add(lb);
            tx = new TextBox();
            tx.ID = "ResultMsgRetMsg";
            div.Controls.Add(tx);
            form1.Controls.Add(div);

            div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            div.Style.Add(HtmlTextWriterStyle.Margin, "4px");
            lb = new Label();
            lb.Text = "ResultMsg.Obj:";
            div.Controls.Add(lb);
            tx = new TextBox();
            tx.Rows = 10;
            tx.ID = "ResultMsgObj";
            tx.Width = Unit.Percentage(80.0);
            tx.TextMode = TextBoxMode.MultiLine;
            div.Controls.Add(tx);
            form1.Controls.Add(div);

            div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            div.Style.Add(HtmlTextWriterStyle.Margin, "4px");
            lb = new Label();
            lb.Text = "ResultMsg.Objj:";
            div.Controls.Add(lb);
            tx = new TextBox();
            tx.Rows = 10;
            tx.ID = "ResultMsgObjj";
            tx.Width = Unit.Percentage(80.0);
            tx.TextMode = TextBoxMode.MultiLine;
            div.Controls.Add(tx);
            form1.Controls.Add(div);

            div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            div.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            div.Style.Add(HtmlTextWriterStyle.Margin, "4px");
            div.Controls.Add(new Panel());
            lb = new Label();
            lb.Text = "Request JSON:";
            div.Controls.Add(lb);
            tx = new TextBox();
            tx.Rows = 10;
            tx.ID = "RequestJSON";
            tx.Width = Unit.Percentage(80.0);
            tx.TextMode = TextBoxMode.MultiLine;
            div.Controls.Add(tx);
            form1.Controls.Add(div);

            #endregion
        }

        void ddlInterfaceName_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        void btnGenerateSign_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> openWith = new Dictionary<string, string>();
            Dictionary<string, string> specifiedFields = new Dictionary<string, string>();
            Dictionary<string, string> keyIDPair = new Dictionary<string, string>();
            cc.Controls.Select(delegate(Control c)
            {
                string t = ((TextBox)c).Text;
                string cID = c.ID;
                if (dssKeyIDPair.ContainsValue(cID))
                {
                    cID = dssKeyIDPair.Keys.Where(delegate(string _i)
                    {
                        return dssKeyIDPair[_i] == cID;
                    }).FirstOrDefault<string>();
                }
                else { }
                // will load the assembly
                // System.Reflection.Assembly myAssembly = System.Reflection.Assembly.LoadFile(Environment.CurrentDirectory + "\\MyClassLibrary.dll");
                // get the class. Always give fully qualified name.
                // Type ReflectionObject = myAssembly.GetType("MyClassLibrary.ReflectionClass");
                System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetAssembly(passengers.GetType());
                Type ReflectionObject = myAssembly.GetType();
                // create an instance of the class
                //object classObject = Activator.CreateInstance(ReflectionObject);
                // set the property of Age to 10. last parameter null is for index. If you want to send any value for collection type
                // then you can specify the index here. Here we are not using the collection. So we pass it as null
                //ReflectionObject.GetProperty("Age").SetValue(classObject, 10, null);
                //ReflectionObject.GetProperty(c.ID).SetValue(mf, ((TextBox)c).Text, null);
                // get the value from the property Age which we set it in our previous example
                //object age = ReflectionObject.GetProperty("Age").GetValue(classObject, null);
                // write the age.
                //Console.WriteLine(age.ToString());
                passengers.GetType().GetProperty(cID).SetValue(passengers, t, null);
                keyIDPair[cID] = c.ID;
                if (cID != "Sign")
                {
                    if ((form1.FindControl("cb_" + c.ID) as CheckBox).Checked)
                    {
                        specifiedFields[c.ID] = (c as TextBox).ToolTip ?? String.Empty;
                        if (String.IsNullOrEmpty(specifiedFields[c.ID]))
                        {
                            openWith.Add(c.ID, t);
                        }
                        else
                        {
                            openWith.Add(specifiedFields[c.ID], t);
                        }
                    }
                    else
                    {
                        openWith.Add(c.ID, t);
                    }
                }
                else { }
                return c;
            }).ToArray();
            string sign = String.Empty;
            // string jsonStr = GBA.GetJsonTextWithSignName(((DropDownList)form1.FindControl("InterfaceName")).Text, obj: passengers, specifiedFields: specifiedFields, keyIDpair: keyIDPair);
            string jsonStr = GBA.GetJsonTextWithSignName(serviceName: serviceName, methodName: ((DropDownList)form1.FindControl("InterfaceName")).Text, methodParamObjectArray: new object[] { passengers, specifiedFields, keyIDPair });
            ((TextBox)form1.FindControl("RequestJSON")).Text = jsonStr;
            ((Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(jsonStr, type: typeof(Dictionary<string, string>))).TryGetValue("Sign", out sign);
            ((TextBox)form1.FindControl("Sign")).Text = sign;
            passengers.passenger0 = sign;
        }

        void btnSendRequest_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> specifiedFields = new Dictionary<string, string>();
            Dictionary<string, string> keyIDPair = new Dictionary<string, string>();
            cc.Controls.Select(delegate(Control c)
            {
                string t = ((TextBox)c).Text;
                string cID = c.ID;
                if (dssKeyIDPair.ContainsValue(cID))
                {
                    cID = dssKeyIDPair.Keys.Where(delegate(string _i)
                    {
                        return dssKeyIDPair[_i] == cID;
                    }).FirstOrDefault<string>();
                }
                else { }
                passengers.GetType().GetProperty(cID).SetValue(passengers, t, null);
                keyIDPair[cID] = c.ID;
                if (cID != "Sign")
                {
                    if ((form1.FindControl("cb_" + c.ID) as CheckBox).Checked)
                    {
                        specifiedFields[c.ID] = (c as TextBox).ToolTip ?? String.Empty;
                        if (String.IsNullOrEmpty(specifiedFields[c.ID]))
                        {
                            // openWith.Add(c.ID, t);
                        }
                        else
                        {
                            // openWith.Add(specifiedFields[c.ID], t);
                        }
                    }
                    else
                    {
                        // openWith.Add(c.ID, t);
                    }
                }
                else { }
                return c;
            }).ToArray();
            string sign = String.Empty;
            ((Dictionary<string, string>)RF.GlobalClass.Utils.Convert.JSONToObject(GBA.GetJsonTextWithSignName(methodName: ((DropDownList)form1.FindControl("InterfaceName")).Text, methodParamObjectArray: new Object [] {passengers, specifiedFields, keyIDPair}), type: typeof(Dictionary<string, string>))).TryGetValue("Sign", out sign);
            ((TextBox)form1.FindControl("Sign")).Text = sign;
            passengers.passenger0 = sign;

            // SRExample.ResultMsg resultMsg = GBW.GetSRExampleResultMsg(((DropDownList)form1.FindControl("InterfaceName")).Text, obj: passengers, specifiedFields: specifiedFields, keyIDpair: keyIDPair);

            #region test

            string @namespace = "WAExample";
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes())
                .Where(t => t.IsClass && t.Namespace == @namespace && t.Name == serviceName).FirstOrDefault();

            var q = from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == @namespace &&t.Name == serviceName
                    select t;
            q.ToList().ForEach(t => Console.WriteLine(t.Name));

            #endregion


            GB.Authorization gba = new WAExample.GB.Authorization();
            string jsonText = gba.GetJsonTextWithSignName(methodName: interfaceName, jsonText: String.Empty, methodParamObjectArray: new object[] { passengers, specifiedFields, keyIDPair });
            
            var gim = GBW.GetInterfaceMethod<SRExample.ServiceSoapClient, SRExample.ResultMsg, Func<SRExample.ServiceSoapClient, String, SRExample.ResultMsg>>( interfaceName:((DropDownList)form1.FindControl("InterfaceName")).Text);
            SRExample.ResultMsg resultMsg = gim(new SRExample.ServiceSoapClient(), jsonText);

            resultMsg = GBW.GetResultMsg<SRExample.ServiceSoapClient
                , Func<SRExample.ServiceSoapClient,String,SRExample.ResultMsg>
                , SRExample.ResultMsg
                >
                (serviceSoapClient: new SRExample.ServiceSoapClient(), interfaceName: ((DropDownList)form1.FindControl("InterfaceName")).Text, jsonText: "", obj: passengers, specifiedFields: specifiedFields, keyIDpair: keyIDPair) as SRExample.ResultMsg;
            resultMsg = GBW.GetResultMsg<SRExample.ServiceSoapClient
                , Func<SRExample.ServiceSoapClient, String, SRExample.ResultMsg>
                , SRExample.ResultMsg
                >
                ( serviceSoapClient: new SRExample.ServiceSoapClient()
                , interfaceName: ((DropDownList)form1.FindControl("InterfaceName")).Text
                , jsonText: "", obj: passengers
                , specifiedFields: specifiedFields
                , keyIDpair: keyIDPair) as SRExample.ResultMsg;
            if (null != resultMsg)
            {
                ((TextBox)form1.FindControl("ResultMsgRetCode")).Text = resultMsg.RetCode;
                ((TextBox)form1.FindControl("ResultMsgRetMsg")).Text = resultMsg.RetMsg;
                ((TextBox)form1.FindControl("ResultMsgObj")).Text = RF.GlobalClass.Utils.Convert.ObjectToJSON(RequestBus.ConvertResultMsgObj(resultMsg.Obj));
                ((TextBox)form1.FindControl("ResultMsgObjj")).Text = RF.GlobalClass.Utils.Convert.ObjectToJSON(RequestBus.ConvertResultMsgObjj(resultMsg.Obj));
            }
            else
            {
                ((TextBox)form1.FindControl("ResultMsgRetCode")).Text = String.Empty;
                ((TextBox)form1.FindControl("ResultMsgRetMsg")).Text = String.Empty;
                ((TextBox)form1.FindControl("ResultMsgObj")).Text = String.Empty;
            }
        }
    }
}