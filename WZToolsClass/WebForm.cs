extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace RF
{
    public partial class GlobalClass
    {
        /// <summary>
        /// for System.Windows.Forms
        /// </summary>
        public partial class WebForm
        {
            #region Control Collection
            /// <summary>
            /// the collection of control
            /// </summary>
            /// <example>
            /// <see cref="WinForm.ControlCollection.fillControl(String,String,float,float,String)"/>
            /// </example>
            public partial class ControlCollection
            {
                private System.Web.UI.Control[] _controlCollection;

                /// <summary>
                /// 
                /// </summary>
                public System.Web.UI.Control[] Controls
                {
                    get { return _controlCollection; }
                    set { _controlCollection = value; }
                }

                /// <summary>
                /// the collection of control
                /// </summary>
                /// <param name="controls"></param>
                public ControlCollection(System.Web.UI.Control[] controls)
                {
                    this._controlCollection = controls;
                }

                /// <summary>
                /// this method makes a copy of control as a clone function
                /// <note>
                ///     properties not in the object will NOT be cloned.
                ///     LiberalControl is not supported
                /// </note>
                /// </summary>
                /// <param name="o">control to clone from</param>
                /// <returns>Object</returns>
                /// <example title="Clone Control of Table Cells">
                ///     <code>
                ///     foreach (Control control in tempTableCellControls)
                ///     {
                ///         tc.Controls.Add(RF.GlobalClass.WebForm.ControlCollection.CloneControl(control));
                ///     }
                ///     </code>
                /// </example>
                public static Object CloneControlObject(Control o)
                {
                    Object retObject = new Object();
                    try
                    {
                        Type type = o.GetType();
                        PropertyInfo[] properties = type.GetProperties();
                        try{
                            retObject = type.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);
                        }catch(Exception ex){
                        }
                        foreach (PropertyInfo propertyInfo in properties)
                        {
                            if (propertyInfo.CanWrite)
                            {
                                try
                                {
                                    try
                                    {
                                        propertyInfo.SetValue(retObject, propertyInfo.GetValue(o, null), null);
                                    }
                                    catch (System.Reflection.TargetInvocationException tiex)
                                    {
                                        // for Exception Message:"不支持设置 System.Web.UI.WebControls.ImageButton 的属性 GenerateEmptyAlternateText。"
                                        // (Not Support Setting System.Web.UI.WebControls.ImageButton's property GenerateEmptyAlternateText)
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            else { }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    return retObject;
                }
                /// <summary>
                /// this method makes a copy of control as a clone function
                /// <note>
                ///     properties not in the object will NOT be cloned.
                /// </note>
                /// </summary>
                /// <param name="c">control to clone from</param>
                /// <returns>Control</returns>
                /// <example title="Clone Control of Table Cells">
                ///     <code>
                ///     foreach (Control control in tempTableCellControls)
                ///     {
                ///         tc.Controls.Add(RF.GlobalClass.WebForm.ControlCollection.CloneControl(control));
                ///     }
                ///     </code>
                /// </example>
                public static System.Web.UI.Control CloneControl(Control c)
                {
                    Type type = c.GetType();
                    var clone = (0 == type.GetConstructors().Length) ? new Control() : Activator.CreateInstance(type) as Control;
                    try
                    {
                        if (c is HtmlControl)
                        {
                            clone.ID = c.ID;
                            AttributeCollection attributeCollection = ((HtmlControl)c).Attributes;
                            System.Collections.ICollection keys = attributeCollection.Keys;
                            foreach (string key in keys)
                            {
                                ((HtmlControl)c).Attributes.Add(key, (string)attributeCollection[key]);
                            }
                        }
                        else if (c is System.Web.UI.LiteralControl)
                        {
                            clone = new System.Web.UI.LiteralControl(((System.Web.UI.LiteralControl)(c)).Text);
                        }
                        else
                        {
                            PropertyInfo[] properties = c.GetType().GetProperties();
                            foreach (PropertyInfo p in properties)
                            {
                                // "InnerHtml/Text" are generated on the fly, so skip them. "Page" can be ignored, because it will be set when control is added to a Page.
                                if (p.Name != "InnerHtml" && p.Name != "InnerText" && p.Name != "Page" && p.CanRead && p.CanWrite)
                                {
                                    try
                                    {
                                        ParameterInfo[] indexParameters = p.GetIndexParameters();
                                        p.SetValue(clone, p.GetValue(c, indexParameters), indexParameters);
                                    }
                                    catch (System.Reflection.TargetInvocationException tiex)
                                    {
                                    }
                                }
                            }
                        }
                        int cControlsCount = c.Controls.Count;
                        for (int i = 0; i < cControlsCount; ++i)
                        {
                            clone.Controls.Add(CloneControl(c.Controls[i]));
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    return clone;
                }

                /// <summary>
                /// fill the Barcode format code to control as child controls
                /// </summary>
                /// <param name="code">the code to convert</param>
                /// <param name="o">the control to be filled</param>
                /// <param name="height">the height of the child control</param>
                /// <param name="width">the width of the child control</param>
                /// <returns>control its self</returns>
                public static Control fillCodeAsBarCodeToControl(string code, Control o, int height = 12, int width = 2)
                {
                    Utils.Convert.StringToBarcode(code).Select(delegate(char c)
                    {
                        System.Web.UI.HtmlControls.HtmlGenericControl span = new System.Web.UI.HtmlControls.HtmlGenericControl("span");
                        span.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                        span.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                        span.Style.Add(HtmlTextWriterStyle.Height, Unit.Pixel(height).ToString());
                        span.Style.Add(HtmlTextWriterStyle.Width, Unit.Pixel(width).ToString());
                        switch (c)
                        {
                            case '_':
                                span.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                                break;
                            case '|':
                                span.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#000000");
                                break;
                            default: break;
                        }
                        o.Controls.Add(span);
                        return span;
                    }).ToArray();
                    return o;
                }
            }
            #endregion

            #region table

            /// <summary>
            /// Fill Table According to data
            /// <note type="note">
            /// The table should has one template row with cells and controls already.
            /// And the template row will be removed automatically.
            /// The data should in the order of the table column.
            /// </note>
            /// </summary>
            /// <param name="t">Table</param>
            /// <param name="dt">DataBase</param>
            /// <param name="templateRowIndex">templateRowIndex, default is 1</param>
            /// <example>
            ///		<code>
            ///			&lt;asp:Table ID="TableScoreExchangeInfo" runat="server" CssClass="table borderTopCorner borderAround" CellPadding="0" CellSpacing="0" ForeColor="Black" BorderStyle="None"
			///			 BorderColor="White" BackColor="White" style="text-overflow:ellipsis;overflow:auto;" OnPreRender="TableScoreExchangeInfo_OnPreRender"&gt;
			///				&lt;asp:TableRow ID="TableRow32" runat="server" CssClass="tableOddRow" Height="27px"&gt;
			///					&lt;asp:TableCell ID="TableCell100" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label63" runat="server" Text="积分卡" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell101" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label64" runat="server" Text="兑换类型" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell102" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label65" runat="server" Text="兑换积分" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell103" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label66" runat="server" Text="兑换礼品" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell104" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label67" runat="server" Text="重打印兑换单" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///				&lt;/asp:TableRow&gt;
			///				&lt;asp:TableRow ID="TableRow31" runat="server" CssClass="tableOddRow" Height="27px"&gt;
			///					&lt;asp:TableCell ID="TableCell69" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label34" runat="server" Text="" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell73" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label38" runat="server" Text="" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell74" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label39" runat="server" Text="" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell79" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label44" runat="server" Text="" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///					&lt;asp:TableCell ID="TableCell95" runat="server" ColumnSpan="1" HorizontalAlign="Center" CssClass="borderAround" BackColor="White"&gt;
			///						&lt;asp:Label ID="Label62" runat="server" Text="" CssClass="padding0px2px0px2px"&gt;&lt;/asp:Label&gt;
			///					&lt;/asp:TableCell&gt;
			///				&lt;/asp:TableRow&gt;
			///			&lt;/asp:Table&gt;
            ///		</code>
			///		<code>
			///			RF.GlobalClass.WebForm.fillTableAccordingToData(TableScoreExchangeInfo, dataSet.Tables["ScoreData", "ScoreDetails"]);
			///		</code>
            /// </example>
            public static void fillTableAccordingToData(Table t, DataTable dt, DataTable dtInfo = null, int templateRowIndex = 1, int templateFooterRowIndex = -1, String ellipsisTitle="{ellipsis}", String ellipsisCls="Ellipsis")
            {
                if (null == t || null == dt)
                {
                    return;
                }
                else { }
                try
                {
                    TableRow[] trs = new TableRow[] { };
                    List<TableRow> ltr = new List<TableRow>();
                    TableRow tr;
                    TableCell tc;
                    #region draw table

                    DataRow dr;
                    int dtRowsCount = dt.Rows.Count;
                    int drItemArrayLength;
                    TableRow tempTableRow = t.Rows[templateRowIndex];
                    int cellsCount = tempTableRow.Cells.Count;
                    TableCell tempTableCell;
                    AttributeCollection act = tempTableRow.Attributes;
                    for (int dti = 0; dti < dtRowsCount; dti++)
                    {
                        dr = dt.Rows[dti];
                        tr = new System.Web.UI.WebControls.TableRow();
                        drItemArrayLength = dr.ItemArray.Length;

                        #region copy style
                        System.Collections.ICollection attrKeys = act.Keys;
                        string[] keys = new string[] { };
                        attrKeys.CopyTo(keys, 0);
                        int keysLength = keys.Length;
                        string keyName = String.Empty;
                        for (int ki = 0; ki < keysLength; ki++)
                        {
                            keyName = keys[ki];
                            tr.Attributes.Add(keyName, act[keyName]);
                        }
                        tr.CssClass = tempTableRow.CssClass;
                        tr.Height = tempTableRow.Height;
                        tr.Width = tempTableRow.Width;
                        tr.Style.Value = tempTableRow.Style.Value;

                        keys = new string[] { };
                        tempTableRow.Style.Keys.CopyTo(keys, 0);
                        keysLength = keys.Length;
                        for (int ki = 0; ki < keysLength; ki++)
                        {
                            keyName = keys[ki];
                            tr.Style.Add(keyName, tempTableRow.Style[keyName]);
                        }
                        tr.ToolTip = tempTableRow.ToolTip;
                        #endregion

                        for (int dri = 0; dri < cellsCount; dri++)
                        {
                            tempTableCell = tempTableRow.Cells[dri];

                            tc = new System.Web.UI.WebControls.TableCell();
                            #region copy cell controls
                            System.Web.UI.ControlCollection tempTableCellControls = tempTableCell.Controls;
                            foreach (Control control in tempTableCellControls)
                            {
                                Control _c = RF.GlobalClass.WebForm.ControlCollection.CloneControl(control);
                                if (null != _c)
                                {
                                    tc.Controls.Add(_c);
                                }
                                else { }
                            }
                            #endregion

                            #region bind data
                            if (dri < drItemArrayLength && tc.Controls.Count > 0)
                            {
                                try
                                {
                                    (tc.Controls[0] as ITextControl).Text = dr.ItemArray.GetValue(dri) as String;
                                    if ((tc.Controls[0] as WebControl).CssClass.Split(' ').Contains<string>(ellipsisCls))
                                    {
                                        if ((tc.Controls[0] as WebControl).ToolTip.Contains(ellipsisTitle))
                                        {
                                            (tc.Controls[0] as WebControl).ToolTip = (tc.Controls[0] as WebControl).ToolTip.Replace(ellipsisTitle, (tc.Controls[0] as ITextControl).Text);
                                        }
                                        else if ((tc.Controls[0] as WebControl).ToolTip == String.Empty)
                                        {
                                            (tc.Controls[0] as WebControl).ToolTip = (tc.Controls[0] as ITextControl).Text;
                                        }
                                        else { }
                                    }
                                    else { }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            else { }
                            #endregion

                            #region customized attributes
                            
                            keys = new string[] { };
                            tempTableCell.Attributes.Keys.CopyTo(keys, 0);
                            keysLength = keys.Length;
                            for (int ki = 0; ki < keysLength; ki++)
                            {
                                keyName = keys[ki];
                                tc.Attributes.Add(keyName, tempTableCell.Attributes[keyName]);
                            }

                            #endregion

                            #region copy cell style
                            tc.CssClass = tempTableCell.CssClass;
                            tc.Height = tempTableCell.Height;
                            tc.Width = tempTableCell.Width;


                            keys = new string[] { };
                            tempTableCell.Style.Keys.CopyTo(keys, 0);
                            keysLength = keys.Length;
                            for (int ki = 0; ki < keysLength; ki++)
                            {
                                keyName = keys[ki];
                                tc.Style.Add(keyName, tempTableCell.Style[keyName]);
                            }
                            tc.ColumnSpan = tempTableCell.ColumnSpan;
                            tc.RowSpan = tempTableCell.RowSpan;
                            tc.BackColor = tempTableCell.BackColor;
                            tc.ToolTip = tempTableCell.ToolTip;
                            #endregion

                            tr.Cells.Add(tc);
                        }
                        t.Rows.AddAt(templateRowIndex, tr);
                        ltr.Add(tr);
                    }
                    /*
                    trs = ltr.ToArray();
                    if (t.Rows.Count > 2)
                    {
                        TableRow[] ltra = new TableRow[(t.Rows.Count - 2)];
                        t.Rows.CopyTo(ltra, 0);
                        trs = trs.Concat<TableRow>(ltra).ToArray();
                    }
                    else { }
                    t.Rows.AddRange(trs);
                    */
                    t.Rows.Remove(tempTableRow);
                    #endregion

                    #region draw table info
                    if (t.Rows.Count >= dtRowsCount + 2)
                    {
                        if (templateFooterRowIndex > 0 && t.Rows.Count > templateFooterRowIndex)
                        {
                            // dtRowsCount + 1 : consider there is a header.
                            TableRow tempTableFooterRow = t.Rows[dtRowsCount + 1];

                            dtInfo = dtInfo ?? new DataTable();
                            Boolean tmpBool = true;
                            dtInfo.Rows[0].ItemArray.Select(delegate(object obj, int idx)
                            {
                                foreach (Control c in tempTableFooterRow.Controls)
                                {
                                    switch (idx)
                                    {
                                        case 0:
                                            if (true == (c as WebControl).CssClass.Contains("RowTotalCount"))
                                            {
                                                (c as ITextControl).Text = obj + "";
                                            }
                                            else { }
                                            break;
                                        case 1:
                                            if (true == (c as WebControl).CssClass.Contains("PerPageRowCount"))
                                            {
                                                (c as ITextControl).Text = obj + "";
                                            }
                                            else { }
                                            break;
                                        case 2:
                                            if (true == (c as WebControl).CssClass.Contains("PageTotalCount"))
                                            {
                                                (c as ITextControl).Text = obj + "";
                                            }
                                            else { }
                                            break;
                                        case 3:
                                            if (true == (c as WebControl).CssClass.Contains("pageFirst"))
                                            {
                                                tmpBool = (c as WebControl).Enabled = Boolean.TryParse(obj.ToString(), out tmpBool) ? tmpBool : tmpBool;
                                            }
                                            else { }
                                            break;
                                        case 4:
                                            if (true == (c as WebControl).CssClass.Contains("pagePrev"))
                                            {
                                                tmpBool = (c as WebControl).Enabled = Boolean.TryParse(obj.ToString(), out tmpBool) ? tmpBool : tmpBool;
                                            }
                                            else { }
                                            break;
                                        case 5:
                                            if (true == (c as WebControl).CssClass.Contains("pageNext"))
                                            {
                                                tmpBool = (c as WebControl).Enabled = Boolean.TryParse(obj.ToString(), out tmpBool) ? tmpBool : tmpBool;
                                            }
                                            else { }
                                            break;
                                        case 6:
                                            if (true == (c as WebControl).CssClass.Contains("pageLast"))
                                            {
                                                tmpBool = (c as WebControl).Enabled = Boolean.TryParse(obj.ToString(), out tmpBool) ? tmpBool : tmpBool;
                                            }
                                            else { }
                                            break;
                                        case 7:
                                            if (true == (c as WebControl).CssClass.Contains("CurrPageNum"))
                                            {
                                                (c as ITextControl).Text = obj + "";
                                            }
                                            else { }
                                            break;
                                        default: 
                                            break;
                                    }
                                }
                                return obj;
                            });
                        }
                        else { }
                    }
                    else { }
                    #endregion
                }
                catch (Exception ex)
                {
                }
            }

            #endregion

            #region request

            public static string getPostData(System.Web.HttpRequest Request)
            {
                string data = "";
                try
                {
                    #region Request.InputStream
                    try
                    {
                        if (Request.InputStream.Length != 0)
                        {
                            data = (new System.IO.StreamReader(Request.InputStream)).ReadToEnd();
                        }
                        else { }
                    }
                    catch (Exception ex) { }

                    #endregion

                    #region From Request.Form
                    try
                    {
                        if (Request.Form.HasKeys())
                        {
                            Dictionary<string, string> dss = new Dictionary<string, string>();
                            Request.Form.AllKeys.Select(delegate(string name)
                            {
                                dss[name] = Request.Form[name];
                                return name;
                            }).ToArray();
                            data = RF.GlobalClass.Utils.Convert.ObjectToJSON(dss);
                        }
                        else { }
                    }
                    catch (Exception ex) { }

                    #endregion
                }
                catch (Exception ex)
                {
                }
                return data;
            }

            #endregion

            #region response
            public partial class Excel
            {
                private const string defaultDownloadedCookieKey = "Downloaded";
                private const string defaultDownloadedCookieValue = "True";
                /// <summary>
                /// Resposne via OutputStream
                /// </summary>
                /// <param name="package"></param>
                /// <param name="Response"></param>
                public static void saveAs(rf.OfficeOpenXml.ExcelPackage package, System.Web.HttpResponse Response, string name = "temp.xlsx", string cookieKey = defaultDownloadedCookieKey, string cookieValue = defaultDownloadedCookieValue)
                {
                    try
                    {
                        Response.Cookies.Add(new System.Web.HttpCookie(cookieKey, cookieValue));
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", name));
                        Response.Clear();
                        package.SaveAs(Response.OutputStream);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                /// <summary>
                /// Resposne via OutputStream
                /// </summary>
                /// <param name="package"></param>
                /// <param name="Response"></param>
                public static void saveAs(rf.NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook, System.Web.HttpResponse Response, string name = "temp.xls", string cookieKey = defaultDownloadedCookieKey, string cookieValue = defaultDownloadedCookieValue)
                {
                    try
                    {
                        Response.Cookies.Add(new System.Web.HttpCookie(cookieKey, cookieValue));
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", name));
                        Response.Clear(); 
                        System.IO.MemoryStream file = new System.IO.MemoryStream();
                        hssfworkbook.Write(file);
                        file.WriteTo(Response.OutputStream);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                /// <summary>
                /// Resposne via OutputStream
                /// </summary>
                /// <param name="excel">GlobalClass.Excel instance</param>
                /// <param name="Response"></param>
                /// <param name="name"></param>
                /// <param name="suffix">GlobalClass.Excel.suffix define the type of excel to output</param>
                /// <param name="cookieKey"></param>
                /// <param name="cookieValue"></param>
                /// <example>
                /// <code language="C#" title="save excel">
                /// RF.GlobalClass.Excel excel = new RF.GlobalClass.Excel();
                /// excel.createWorksheet(name: "LuckDrawData", headers: new string[] { "NO", "scoreCardCode", "scoreCardOwnerName", "scoreTimes", "Item" }, rowsData: result.data["TableItemList"], suffix:RF.GlobalClass.Excel.suffix.xls);
                /// RF.GlobalClass.WebForm.Excel.saveAs(excel: excel, Response: Response, name: "LuckDrawData" + RF.GlobalClass.Utils.DateTime.GetDateTimeString(DateTime.Now), suffix: RF.GlobalClass.Excel.suffix.xls);
                /// </code>
                /// </example>
                public static void saveAs(GlobalClass.Excel excel, System.Web.HttpResponse Response, string name = "temp", GlobalClass.Excel.Suffix suffix = GlobalClass.Excel.Suffix.xls, string cookieKey = defaultDownloadedCookieKey, string cookieValue = defaultDownloadedCookieValue)
                {
                    try
                    {
                        Response.Cookies.Add(new System.Web.HttpCookie(cookieKey, cookieValue));
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        name = name + "." + suffix.ToString();
                        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", name));
                        // Response.Clear();
                        switch (suffix)
                        {
                            case GlobalClass.Excel.Suffix.xls:
                                System.IO.MemoryStream file = new System.IO.MemoryStream();
                                excel.hssfworkbook.Write(file);
                                file.WriteTo(Response.OutputStream); 
                                break;
                            case GlobalClass.Excel.Suffix.xlsx:
                                excel.package.SaveAs(Response.OutputStream);
                                break;
                            default: break;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                /// <summary>
                /// Response via BinaryWrite
                /// </summary>
                /// <param name="package"></param>
                /// <param name="Response"></param>
                public static void binaryWrite(rf.OfficeOpenXml.ExcelPackage package, System.Web.HttpResponse Response, string name = "temp.xlsx", string cookieKey = "Downloaded", string cookieValue = "True")
                {
                    try
                    {
                        Response.Cookies.Add(new System.Web.HttpCookie(cookieKey, cookieValue));
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;  filename=" + name);
                        Response.Clear();
                        Response.BinaryWrite(package.GetAsByteArray());
                    }
                    catch (Exception ex)
                    {
                    }
                }

                /// <summary>
                /// Store ExcelPackage's Stream to Page.Application
                /// </summary>
                /// <param name="package"></param>
                /// <param name="Application"></param>
                /// <param name="name"></param>
                public static void storeToApplication(rf.OfficeOpenXml.ExcelPackage package, System.Web.HttpApplicationState Application, String name)
                {
                    Application[name] = package.Stream;
                }

                /// <summary>
                /// Get ExcelPackage's Stream from Page.Application
                /// </summary>
                /// <param name="Application"></param>
                /// <param name="name"></param>
                /// <returns></returns>
                public static System.IO.Stream getFromApplication(System.Web.HttpApplicationState Application, String name)
                {
                    return Application[name] as System.IO.Stream;
                }
            }

            /// <summary>
            /// class for Image
            /// </summary>
            public partial class Image
            {
                /// <summary>
                /// Get the src of image by passing bianryArray
                /// </summary>
                /// <param name="imageBytes"></param>
                /// <param name="type"></param>
                /// <returns></returns>
                public static string GetImageSrcFromBinaryData(byte[] imageBytes, string type = "jpeg")
                {
                    string result = String.Empty;
                    try
                    {
                        result = "data:image/" + type + ";base64," + Convert.ToBase64String(imageBytes);
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }

                /// <summary>
                /// Convert local image to binary array
                /// </summary>
                /// <param name="imagePath"></param>
                /// <returns></returns>
                public static byte[] ImageToBinary(string imagePath)
                {
                    byte[] b = new byte[] { };
                    try
                    {
                        System.IO.FileStream fS = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        b = new byte[fS.Length];
                        fS.Read(b, 0, (int)fS.Length);
                        fS.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                    return b;
                }

                /// <summary>
                /// Get thumbnail image
                /// </summary>
                /// <param name="image"></param>
                /// <param name="imageFormat"></param>
                /// <param name="thumbWidth"></param>
                /// <param name="thumbHeight"></param>
                /// <param name="ThumbnailCallback"></param>
                /// <param name="Response"></param>
                /// <param name="responseContentType"></param>
                /// <returns></returns>
                public static byte[] GetThumbImage(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat imageFormat, int thumbWidth = -1, int thumbHeight = -1, Func<bool> ThumbnailCallback = null, System.Web.HttpResponse Response = null, String responseContentType = "image/jpeg")
                {
                    byte[] imageContent = new byte[] { };
                    ThumbnailCallback = null == ThumbnailCallback ? new System.Func<bool>(delegate() { return true; }) : ThumbnailCallback;
                    thumbWidth = (-1 == thumbWidth) ? image.Width : thumbWidth;
                    thumbHeight = (-1 == thumbHeight) ? image.Height : thumbHeight;
                    try
                    {
                        // get the file name -- fall800.jpg
                        //string file = Request.QueryString["file"];

                        // create an image object, using the filename we just retrieved
                        //System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(file));

                        // create the actual thumbnail image
                        System.Drawing.Image thumbnailImage = image.GetThumbnailImage(thumbWidth, thumbHeight, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
                        // make a memory stream to work with the image bytes
                        using (System.IO.MemoryStream imageStream = new System.IO.MemoryStream())
                        {
                            try
                            {
                                // put the image into the memory stream
                                thumbnailImage.Save(imageStream, imageFormat);

                                // make byte array the same size as the image
                                imageContent = new Byte[imageStream.Length];

                                // rewind the memory stream
                                imageStream.Position = 0;

                                // load the byte array with the image
                                imageStream.Read(imageContent, 0, (int)imageStream.Length);
                            }
                            catch (Exception exb)
                            {
                            }
                        }
                        if (null != Response)
                        {
                            // return byte array to caller with image type
                            try
                            {
                                Response.ContentType = responseContentType;
                                Response.BinaryWrite(imageContent);
                            }
                            catch (Exception exc) { }
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex) { }
                    return imageContent;
                }

                /// <summary>
                /// Get Compact Image Src for Low Level Browser
                /// </summary>
                /// <param name="imgBase64Data"></param>
                /// <param name="IEVersion"></param>
                /// <returns></returns>
                public static string GetCompactImageSrcForLowLevelBrowser(string imgBase64Data, double IEVersion, string type="jpeg", System.Drawing.Imaging.ImageFormat type2 = null)
                {
                    string result = String.Empty;
                    type2 = type2 ?? System.Drawing.Imaging.ImageFormat.Jpeg;
                    try
                    {
                        System.Drawing.Image sdImage = RF.GlobalClass.Utils.Convert.Base64ToImage(imgBase64Data);

                        int _limitLength = System.Convert.ToInt32(RF.GlobalClass.WebForm.Image.LimitSize.IE8);
                        byte[] _tmpByte = new byte[] { };
                        if (9.0 > IEVersion && imgBase64Data.Length > _limitLength)
                        {
                            // double _tmpPct = (_limitLength / imgBase64Data.Length);
                            int _tmpWidth = Convert.ToInt32((sdImage.Width * _limitLength / imgBase64Data.Length));
                            int _tmpHeight = Convert.ToInt32((sdImage.Height * _limitLength / imgBase64Data.Length));
                            _tmpByte = RF.GlobalClass.WebForm.Image.GetThumbImage(sdImage, type2, thumbWidth: _tmpWidth, thumbHeight: _tmpHeight);
                        }
                        else {
                            _tmpByte = Convert.FromBase64String(imgBase64Data);
                        }
                        result = RF.GlobalClass.WebForm.Image.GetImageSrcFromBinaryData(_tmpByte, type);
                    }
                    catch (Exception ex)
                    {
                    }
                    return result;
                }

                /// <summary>
                /// the Limit Size that the Browser Supported
                /// </summary>
                public enum LimitSize
                {
                    IE8 = 37245
                }
            }
            #endregion
        }
    }
}
/* vim: set si sts=4 ts=4 sw=4 fdm=indent :*/
