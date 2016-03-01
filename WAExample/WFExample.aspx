<%@ Page Title="" Language="C#" MasterPageFile="~/NestedMasterPage.master" AutoEventWireup="true" CodeBehind="WFExample.aspx.cs" Inherits="WAExample.WFExample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedHead" runat="server">
    <link href="Styles/Example/Example.css" rel="stylesheet" type="text/css" />
    <title>编码示例</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContent" runat="server">
    <asp:HiddenField ID="ModuleID" runat="server" Value="0295" />

    <asp:MultiView ID="MultiViewMenu" runat="server" OnLoad="MultiViewMenu_Load">

        <asp:View ID="View00" runat="server">
            <asp:Panel runat="server">
            </asp:Panel>
        </asp:View>

        <asp:View ID="ViewEQ" runat="server" OnActivate="ViewEQ_Activate">
            <asp:Panel runat="server" style="display: none;">
                <asp:Label runat="server" ID="vEQ_ModuleID" CssClass="DATA" Text="0018" ToolTip="您尚无此权限"></asp:Label>
                <asp:Label runat="server" ID="vEQ_PowerID" Text="01" ToolTip="您尚无此权限"></asp:Label>
            </asp:Panel>
            <asp:Panel runat="server" HorizontalAlign="Center" BorderColor="Silver" BackColor="White" CssClass="positionFixed0px0px0px0px yScrollableContainer ResponseBody" Font-Names="微软雅黑,宋体,Ubuntu" ScrollBars="Auto">
                <asp:Panel runat="server" Width="95%" BorderColor="White" HorizontalAlign="Center" CssClass="centre  minWidth700px">
                    <asp:Panel Width="200px" CssClass="centre" runat="server">
                        <asp:Label Text=" " Width="200px" Height="20px" Font-Size="XX-Large" CssClass=" " runat="server" />
                    </asp:Panel>
                    <div class="panelTitle" style="_border-left: steelBlue 5px solid; color: black; font-family: 宋体; font-size: 10pt; font-weight: 500; height: 20px; margin-left: 0px; margin-right: 0px; margin-top: 0px; margin-bottom: 10px; padding-bottom: 0px; padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: left; vertical-align: bottom; ">
						<span style="color: black; display: inline-block; font-family: 宋体; font-size: 10pt; font-weight: 500; margin-top: 5px; text-align: left;">首页 &gt;</span>
						<span style="color: #999; font-family: 宋体; font-size: 10pt; font-weight: 500; text-align: left;">示例查询</span>
					</div>
                    <asp:Panel runat="server" CssClass="doprintnone hide _panelTitle positonFixed0px0pxpx0px" Style=" padding-left: 0px; border: 1px solid Gainsboro; border-top-color: darksalmon; border-top-width: 2px;" HorizontalAlign="Left" BackColor="White">
                        <asp:Image runat="server" ImageAlign="AbsMiddle" ImageUrl="Resources/Images/Example/TitleSaleMain.png" CssClass="Request cursorDefault" request_location_path_name="{window.location.pathname}/donothing" Style="border-bottom: 0px solid #d77ec1; margin: 10px; background-color: #ffffff;" />
                        <asp:Label runat="server" CssClass="inlineblock padding8px" Style="vertical-align: middle; padding-left: 10px;" Visible="false">
							    <asp:Label runat="server" Text="示例查询" CssClass="inlineblock" Font-Bold="False" Font-Size="36px" ForeColor="#171717" Font-Names="微软雅黑,宋体,Ubuntu"></asp:Label>
                                <asp:Panel runat="server">
                                    <asp:Label runat="server" Text="Electronic Example" CssClass="" Font-Size="17px" ForeColor="lightGray" style="font-weight:600;" Font-Names="微软雅黑,Arial,宋体,Ubuntu"></asp:Label>
                                </asp:Panel>
                        </asp:Label>
                        <asp:Panel runat="server" Font-Italic="true" HorizontalAlign="Right" CssClass="inlineblock padding8px" Style="float: right; position: relative;" Visible="false">
                            <asp:Label runat="server" ID="vEQ_EntranceCount" CssClass="" Style="line-height: 100px;" Font-Size="72px" ForeColor="#d77ec1" Font-Names="Microsoft Sans Serif,Arial,Impact,新宋体">6</asp:Label>
                            <asp:Label runat="server" CssClass="" Style="padding-left: 10px;" Font-Size="13px" ForeColor="#000000">个子模块</asp:Label>
                        </asp:Panel>
                        <div class="hide">
                            <div id="vEQ_rfDialog">
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" Font-Size="14px" Style="padding: 4px; text-align:left; height: 27px\9;" Visible="true">
                        <asp:Label runat="server" CssClass="nowrap inlineblock" Style="padding: 4px 2px; float: left\9; height: 27px\9;">
                            <asp:Label runat="server" HorizontalAlign="Left" CssClass="inlineblock" Style="white-space: nowrap; height: 27px\9;">
                                <asp:Label ID="vEQ_TurnBack" runat="server" CssClass="Request _HistoryBack cursorPointer padding10px" request_validation_selector="none" request_location_path_name="{window.location.pathname}/TurnBackFromDetailsView" ForeColor="white" Style="background-color: #f75454; padding: 6px 10px; margin: 0px 0px; vertical-align: middle;" Visible="false">返回</asp:Label>
                                <asp:HiddenField runat="server" ID="vEQ_rfmessage" />
                            </asp:Label>
                        </asp:Label>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="panelTable ResponseBodyPanelTable" BackColor="White">
						<asp:Table runat="server" CssClass="table borderTopCorner"
							CellPadding="0" CellSpacing="0" ForeColor="Black" BorderStyle="None" BorderColor="White" 
                            BackColor="White" HorizontalAlign="Center" Width="100%" Style="margin: 0px auto;">
                            <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color:White;" Height="27px">
                                <asp:TableCell runat="server" ColumnSpan="20">
                                    <asp:Label runat="server"></asp:Label>
                                </asp:TableCell>
								<asp:TableCell runat="server" ColumnSpan="60" HorizontalAlign="Center">
                                    <asp:Table runat="server" CssClass="_table borderTopCorner" CellPadding="0" CellSpacing="0" ForeColor="Black" BorderStyle="None" BorderColor="White" BackColor="White" Width="100%">
                                        <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color: White;" Height="27px" Visible="false">
                                            <asp:TableCell runat="server" ColumnSpan="98" HorizontalAlign="Left">
									            <asp:Label runat="server" Text="Table Row Example" CssClass="padding0px2px0px2px" Font-Size="24px" ForeColor="#171717" style="border-left-color:#8fd446; border-left-style:solid; border-left-width:4px;padding:0px 4px"></asp:Label>
									            <asp:Label runat="server" Text="" CssClass="padding0px2px0px2px"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color: White; border: 0px solid #e1e1e1; height: 27px\9;" Height="23px">
                                            <asp:TableCell runat="server" ColumnSpan="100" HorizontalAlign="Center">
                                                <asp:Panel runat="server" ID="vEQ_modules" CssClass=" ui-icon-stop padding10px" HorizontalAlign="Center" BorderColor="Gainsboro" BorderWidth="0px" BorderStyle="Solid" style="white-space: normal;" Font-Size="17px" Width="500px">
                                                    <asp:Panel runat="server" Font-Size="Large" >示例查询</asp:Panel>
                                                    <hr style="display: block;height: 1px;border: 0;border-top: 1px solid Gainsboro;margin: 1em 0;padding: 0;  width: 90%; *overflow:hidden; *font-size:x-small;*margin:0.5em 0;"/>
                                                    <asp:TextBox ID="vEQ_QStatus" runat="server" Width="120px" CssClass=" DATA QStatus padding0px2px0px2px margin0px2px0px2px hide" PlaceHolder="请输入" validation="" Text="valid"></asp:TextBox>
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass="inlineblock">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <span class="inlineblock"><div style="width: 120px;" class="textAlignJustify">示例项目<span class="justifyPunctuation">:</span></div></span>
                                                        <asp:Label runat="server" Width="180px" Height="28px" CssClass="inlineblock">
                                                            <asp:Label runat="server" CssClass="hide _For_Lowe_Level_IE">
                                                                示例项目：
                                                            </asp:Label>
                                                            <asp:DropDownList ID="vEQ_QExampleTypeList" runat="server" Width="180px" Height="27px" CssClass="Validation DATA QExampleTypeList DropDownList _PureText valueNullable Focus" max_item_to_show="20" validation="">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="vEQ_QExampleType" runat="server" Width="100px" CssClass="hide DATA QExampleType" PlaceHolder="请输入" validation=""></asp:TextBox>
                                                        </asp:Label>
                                                        <%--<span style="color: indianRed;font-size: 0.85em;">不选则查全部</span>--%>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass="inlineblock" Visible="false">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <span class="inlineblock"><img alt="业务部门" src="Resources/Images/Example/icon.png" style="vertical-align: middle;"/></span>
                                                        <span class="inlineblock"><div style="width: 120px;" class="textAlignJustify">业务部门<span class="justifyPunctuation">:</span></div></span>
                                                        <asp:Label runat="server" Width="180px" Height="18px" CssClass="inlineblock">
                                                            <asp:DropDownList ID="vEQ_QDepartmentCodeList" runat="server" Width="180px" Height="27px" CssClass="Validation DATA QDepartmentCodeList DropDownList _PureText valueNullable" max_item_to_show="20" validation="">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="vEQ_QDepartmentCode" runat="server" Width="100px" CssClass=" DATA QDepartmentCode hide" PlaceHolder="请输入" validation=""></asp:TextBox>
                                                        </asp:Label>
                                                        <%--<span style="color: indianRed;font-size: 0.85em;">不选则查全部</span>--%>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass="inlineblock" Visible="false">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <span class="inlineblock"><img alt="商品编码" src="Resources/Images/Example/IconProductCode.png" style="vertical-align: middle;"/></span>
                                                        <span class="inlineblock"><div style="width: 120px;" class="textAlignJustify">商品编码<span class="justifyPunctuation">:</span></div></span>
                                                        <asp:Label runat="server" Width="180px" CssClass="inlineblock">
                                                            <asp:TextBox ID="vEQ_QProductCode" runat="server" Width="180px" CssClass=" Validation DATA QProductCode " PlaceHolder="请输入" validation="ProductCode"></asp:TextBox>
                                                        </asp:Label>
                                                        <%--<span style="color: indianRed;font-size: 0.85em;">不选则查全部</span>--%>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass="inlineblock" Visible="false">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <span class="inlineblock"><img alt="起止日期" src="Resources/Images/Example/IconTime.png" style="vertical-align: middle;"/></span>
                                                        <span class="inlineblock"><div style="width: 120px;" class="textAlignJustify">起止日期<span class="justifyPunctuation">:</span></div></span>
                                                        <asp:Label runat="server" Width="180px" CssClass="inlineblock">
                                                            <asp:TextBox ID="vEQ_QStartOverDate" runat="server" Width="180px" CssClass="DATA QStartOverDate Validation DatePickerRange" PlaceHolder="请输入" validation="Required DateRange"></asp:TextBox>
                                                        </asp:Label>
                                                        <%--<span style="color: indianRed;font-size: 0.85em;">不选则查全部</span>--%>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass="hide" Visible="false">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <span class="inlineblock"><img src="Resources/Images/Example/Icon.png" style="vertical-align: middle;"/></span>
                                                        <span class="inlineblock"><div style="width: 150px;" class="textAlignJustify">开始日期<span class="justifyPunctuation">:</span></div></span>
                                                        <asp:Label runat="server" Width="150px" CssClass="inlineblock">
                                                            <asp:TextBox ID="vEQ_QStartDate" runat="server" Width="150px" CssClass="DATA QStartDate _Validation DatePicker" PlaceHolder="请输入" validation="Required Date"></asp:TextBox>
                                                        </asp:Label>
                                                        <%--<span style="color: indianRed;font-size: 0.85em;">不选则查全部</span>--%>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass="hide" Visible="false">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <span class="inlineblock"><img src="Resources/Images/Example/Icon.png" style="vertical-align: middle;"/></span>
                                                        <span class="inlineblock"><div style="width: 150px;" class="textAlignJustify">结束日期<span class="justifyPunctuation">:</span></div></span>
                                                        <asp:Label runat="server" Width="150px" CssClass="inlineblock">
                                                            <asp:TextBox ID="vEQ_QOverDate" runat="server" Width="150px" CssClass="DATA QOverDate _Validation DatePicker" PlaceHolder="请输入" validation="Required Date"></asp:TextBox>
                                                        </asp:Label>
                                                        <%--<span style="color: indianRed;font-size: 0.85em;">不选则查全部</span>--%>
                                                    </asp:Panel>
                                                    
                                                    <asp:Panel runat="server" HorizontalAlign="Left" CssClass=" ">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;"/>
                                                        <%--<span class="inlineblock" style="width:300px; color: indianRed;font-size: 0.85em;">*</span>--%>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server">
                                                        <hr style="display: inline-block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1px; *overflow:hidden;*font-size:0;*margin:0.5em 0;"/>
                                                        <asp:Label runat="server" HorizontalAlign="Left" CssClass="inlineblock" Style="white-space: nowrap; height: 27px\9;">
                                                            <asp:Label ID="vEQ_d_Back" runat="server" CssClass="Request cursorPointer padding10px" target_selector=".ResponseBody" request_location_path_name="{window.location.pathname}/TurnToBackFromAdditionView" request_location_search="" request_validation_selector="none" ForeColor="Black" Style="background-color: ghostwhite; padding: 6px 10px; margin: 0px 10px; vertical-align: middle; border:Gainsboro 1px solid;">返回</asp:Label>
                                                            <asp:Label ID="vEQ_Query" runat="server" CssClass="Request cursorPointer padding10px" target_selector=".ResponseBody" request_location_path_name="{window.location.pathname}" request_location_search=" " request_method_name="TurnToExampleView" ForeColor="white" Style="background-color: royalblue; padding: 6px 10px; margin: 0px 10px; vertical-align: middle;">查询</asp:Label>
                                                        </asp:Label>
                                                    </asp:Panel>
                                                    <asp:Panel runat="server">
                                                        <hr style="display: block;height: 1px;border: 0;border-top: 1px solid white;margin: 1em 0;padding: 0;  width: 1%;"/>
                                                    </asp:Panel>
                                                </asp:Panel>
                                                <asp:Label runat="server" ></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color: White;" Height="2px">
                                            <asp:TableCell runat="server" HorizontalAlign="Center">
									            <asp:Label runat="server" Text="" CssClass="padding0px2px0px2px"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color: White;" Height="27px">
                                            <asp:TableCell runat="server" ColumnSpan="100" RowSpan="1" HorizontalAlign="Center">
                                                <asp:Panel runat="server" ID="vEQ_ResponseBodyTableExampleInformationQueryItemList" CssClass="ResponseBodyPanelListTable" Width="100%">
                                                </asp:Panel>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color: White;" Height="17px">
                                            <asp:TableCell runat="server" HorizontalAlign="Center">
									            <asp:Label runat="server" Text="" CssClass="padding0px2px0px2px"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow runat="server" CssClass="tableOddRow hide" Style="background-color: White;" Height="7px">
                                            <asp:TableCell runat="server" ColumnSpan="98" HorizontalAlign="Center">
                                                <asp:ImageButton ID="vEQ_ImageButton_ExportExcel" request_location_path_name="/AttendanceRecordsQueryExport.aspx" request_method_name="AttendanceRecordsQueryItemListExcel" Style="vertical-align: bottom; width: auto;" runat="server" CssClass="Export" OnClientClick="return false;" ImageUrl="~/Resources/Images/public/a-13-1_06.png" AlternateText="导出Excel" Visible="false" />
                                                <asp:Label runat="server" CssClass="Export cursorPointer rf-submit-button" Font-Size="17px" request_location_path_name="{window.location.pathname}/../WFExampleFaultReportQueryExport.aspx" request_method_name="WavingEastWindNeighbourExampleInformationItemListExcel" Text="导出Excel表格"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
								</asp:TableCell>
                                <asp:TableCell runat="server" ColumnSpan="20">
                                    <asp:Label runat="server"></asp:Label>
                                </asp:TableCell>
							</asp:TableRow>
                            <asp:TableRow runat="server" CssClass="tableOddRow" Style="background-color:White;" Height="17px">
								<asp:TableCell  runat="server" ColumnSpan="100" HorizontalAlign="Center">
                                </asp:TableCell>
                                <asp:TableCell runat="server" ColumnSpan="1" HorizontalAlign="Right" Visible="false">
								</asp:TableCell>
							</asp:TableRow>
                        </asp:Table>
                        <asp:TextBox ID="vEQ_rfjumpdata" runat="server" Width="100%" CssClass=" DATA pureText padding0px2px0px2px margin0px2px0px2px hide width0px"
										                        required="required" validation="Required" ReadOnly="true"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel Width="200px" CssClass="centre" runat="server">
                        <asp:Label Text=" " Width="200px" Height="20px" Font-Size="XX-Large" CssClass="" runat="server" />
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </asp:View>
        
        <asp:View ID="ViewEL" runat="server" OnActivate="ViewEL_Activate">
            <asp:Label runat="server" ID="vEL_ModuleID" Text="0018" ToolTip="您尚无此权限"></asp:Label>
            <asp:Label runat="server" ID="vEL_PowerID" Text="01" ToolTip="您尚无此权限"></asp:Label>
            <asp:Panel runat="server" HorizontalAlign="Center" BorderColor="Silver" BackColor="White" CssClass="positionFixed0px0px0px0px yScrollableContainer ResponseBody overflowHidden" Font-Names="微软雅黑,宋体,Ubuntu;">
                <asp:Panel runat="server" Width="95%" BorderColor="White" HorizontalAlign="Center" CssClass="centre  minWidth700px">
                    <asp:Panel Width="200px" CssClass="centre" runat="server">
                        <asp:Label Text=" " Width="200px" Height="20px" Font-Size="XX-Large" CssClass=" " runat="server" />
                    </asp:Panel>
                    <div class="panelTitle" style="_border-left: steelBlue 5px solid; color: black; font-family: 宋体; font-size: 10pt; font-weight: 500; height: 20px; margin-left: 0px; margin-right: 0px; margin-top: 0px; margin-bottom: 0px; padding-bottom: 0px; padding-left: 10px; padding-right: 0px; padding-top: 0px; text-align: left; vertical-align: bottom; ">
						<span style="color: black; display: inline-block; font-family: 宋体; font-size: 10pt; font-weight: 500; margin-top: 5px; text-align: left;">首页 &gt;</span>
					    <span id="Nav1" runat="server" style="color: black; font-family: 宋体; font-size: 10pt; font-weight: 500; text-align: left;"  class="Request HistoryBack cursorPointer" target_selector=".ResponseBody" request_location_path_name="{window.location.pathname}/TurnToBackFromExampleListView" request_location_search="">示例查询&gt;</span>
						<span style="color: #999; font-family: 宋体; font-size: 10pt; font-weight: 500; text-align: left;">示例列表</span>
					</div>
                    <asp:Panel runat="server" CssClass="doprintnone hide _panelTitle positonFixed0px0pxpx0px" Style=" padding-left: 0px; border: 1px solid Gainsboro; border-top-color: darksalmon; border-top-width: 2px;" HorizontalAlign="Left" BackColor="White">
                        <asp:Image runat="server" ImageAlign="AbsMiddle" ImageUrl="Resources/Images/Example/TitleSaleMain.png" CssClass="Request cursorDefault" request_location_path_name="{window.location.pathname}/donothing" Style="border-bottom: 0px solid #d77ec1; margin: 10px; background-color: #ffffff;" />
                        <asp:Label runat="server" CssClass="inlineblock padding8px" Style="vertical-align: middle; padding-left: 10px;" Visible="false">
							    <asp:Label runat="server" Text="销售主表" CssClass="inlineblock" Font-Bold="False" Font-Size="36px" ForeColor="#171717" Font-Names="微软雅黑,宋体,Ubuntu"></asp:Label>
                                <asp:Panel runat="server">
                                    <asp:Label runat="server" Text="Electronic Example" CssClass="" Font-Size="17px" ForeColor="lightGray" style="font-weight:600;" Font-Names="微软雅黑,Arial,宋体,Ubuntu"></asp:Label>
                                </asp:Panel>
                        </asp:Label>
                        <asp:Panel runat="server" Font-Italic="true" HorizontalAlign="Right" CssClass="inlineblock padding8px" Style="float: right; position: relative;" Visible="false">
                            <asp:Label runat="server" ID="vEL_EntranceCount" CssClass="" Style="line-height: 100px;" Font-Size="72px" ForeColor="#d77ec1" Font-Names="Microsoft Sans Serif,Arial,Impact,新宋体">6</asp:Label>
                            <asp:Label runat="server" CssClass="" Style="padding-left: 10px;" Font-Size="13px" ForeColor="#000000">个子模块</asp:Label>
                        </asp:Panel>
                        <div class="hide">
                            <div id="vEL_rfDialog">
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="hide" Font-Size="14px" Style="padding: 4px; text-align:left; height: 27px\9;" Visible="true">
                        <asp:Label runat="server" CssClass="nowrap inlineblock" Style="padding: 4px 2px; float: left\9; height: 27px\9;">
                            <asp:Label runat="server" HorizontalAlign="Left" CssClass="hide" Style="white-space: nowrap; height: 27px\9;">
                                <asp:Label ID="vEL_QExampleType" runat="server" CssClass="DATA QExampleType"></asp:Label>
                                <asp:Label ID="vEL_ProductCode" runat="server" CssClass="DATA ProductCode"></asp:Label>
                                <asp:Label ID="vEL_StartDate" runat="server" CssClass="DATA StartDate"></asp:Label>
                                <asp:Label ID="vEL_OverDate" runat="server" CssClass="DATA OverDate"></asp:Label>
                                <asp:HiddenField runat="server" ID="vEL_rfmessage" />
                            </asp:Label>
                        </asp:Label>
                    </asp:Panel>
                    <asp:Panel runat="server" CssClass="panelTable ResponseBodyPanelTable" BackColor="White">
                        <asp:Table ID="vELTableExampleInformationQueryItemList" runat="server" CssClass="DataTable _table borderTopCorner borderAround ellipsis width100Percent tableLayoutFixed minWidth700px" CellPadding="0" CellSpacing="0" ForeColor="Black" BorderStyle="None" BorderColor="White" BackColor="White" Style="overflow: auto; font-family: 微软雅黑,宋体,Ubuntu;" Width="100%">
                            <asp:TableHeaderRow runat="server" CssClass="tableOddRow" Height="37px" Style="min-width: 1280px;">
                                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround hide" ColumnSpan="2" Width="60" style="min-width:60px;">
                                        <asp:Label runat="server"></asp:Label>
							            <asp:Label runat="server" Text="编码" ToolTip="编码" CssClass="ellipsis pureText padding0px2px0px2px textAlignCenter" Style="font-weight:600;width:100%;" ReadOnly="true"></asp:Label>
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround " ColumnSpan="5" Width="100">
							            <asp:Label runat="server" Text="编号" ToolTip="编号" CssClass="ellipsis pureText padding0px2px0px2px textAlignCenter" Style="font-weight:600;" ReadOnly="true"></asp:Label>
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround " ColumnSpan="9" Width="180">
							            <asp:Label runat="server" Text="名称" ToolTip="名称" CssClass="ellipsis pureText padding0px2px0px2px textAlignCenter" Style="font-weight:600;" ReadOnly="true"></asp:Label>
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" ColumnSpan="2" Width="40">
							            <asp:Label runat="server" Text="状态" ToolTip="状态" CssClass="ellipsis pureText padding0px2px0px2px textAlignCenter" Style="font-weight:600;" ReadOnly="true"></asp:Label>
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround" ColumnSpan="10" Width="200">
							            <asp:Label runat="server" Text="描述" ToolTip="描述" CssClass="ellipsis pureText padding0px2px0px2px textAlignCenter" Style="font-weight:600;" ReadOnly="true"></asp:Label>
                                </asp:TableHeaderCell>
                                <asp:TableHeaderCell runat="server" HorizontalAlign="Center" CssClass="tableHeaderRow borderAround " ColumnSpan="7" Width="140">
							            <asp:Label runat="server" Text="操作" ToolTip="操作" CssClass="ellipsis pureText padding0px2px0px2px textAlignCenter" Style="font-weight:600;" ReadOnly="true"></asp:Label> 
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow runat="server" CssClass="DataRow tableOddRow" Height="37px" Font-Names="微软雅黑,宋体,Ubuntu;" Font-Size="Small">
                                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="borderAround hide" BackColor="White" ColumnSpan="2" Width="60">
                                    <asp:TextBox ID="tbbssmil_RowCount" runat="server" Width="90%" Text="" ToolTip="" CssClass="_DATA IdentityCell DataCell RowCount Ellipsis ellipsis textAlignCenter pureText padding0px2px0px2px" Style="font-family: 微软雅黑,宋体,Ubuntu;" ReadOnly="true"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="borderAround " BackColor="White" ColumnSpan="5">
                                    <asp:Label ID="tbbssmil_Code" runat="server" Text="" ToolTip="" CssClass="_DATA DataCell Code Ellipsis ellipsis pureText textAlignCenter inlineblock padding0px2px0px2px" Font-Names="微软雅黑,宋体,Ubuntu" Style="font-family: 微软雅黑,宋体,Ubuntu;" ReadOnly="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="borderAround " BackColor="White" ColumnSpan="9">
                                    <asp:Label ID="tbbssmil_Name" runat="server" Text="" ToolTip="" CssClass="_DATA DataCell Name Ellipsis ellipsis pureText textAlignCenter inlineblock padding0px2px0px2px" Font-Names="微软雅黑,宋体,Ubuntu" Style="font-family: 微软雅黑,宋体,Ubuntu;" ReadOnly="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="borderAround" BackColor="White" ColumnSpan="2">
                                    <asp:Label ID="tbbssmil_Status" runat="server" Text="" ToolTip="" CssClass="_DATA DataCell Status Ellipsis ellipsis pureText textAlignCenter inlineblock padding0px2px0px2px" Font-Names="微软雅黑,宋体,Ubuntu" Style="font-family: 微软雅黑,宋体,Ubuntu;" ReadOnly="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="borderAround" BackColor="White" ColumnSpan="10">
                                    <asp:Label ID="tbbssmil_Description" runat="server" Text="" ToolTip="" CssClass="_DATA DataCell Descriptioin Ellipsis ellipsis pureText textAlignCenter inlineblock padding0px2px0px2px" Font-Names="微软雅黑,宋体,Ubuntu" Style="font-family: 微软雅黑,宋体,Ubuntu;" ReadOnly="true"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="borderAround" BackColor="White" ColumnSpan="7">
                                    <asp:Panel runat="server" CssClass=" textAlignCenter" ForeColor="RoyalBlue">
                                        <asp:Panel runat="server">
                                            <asp:Label runat="server" ID="tbbssmil_powerID01">
                                                    <span class="Request cursorPointer" target_selector=".ResponseBody" request_location_path_name="{window.location.pathname}/TurnToDetailsView" request_location_search="" power_id="01">
                                                        查看详情
                                                    </span>
                                            </asp:Label>
                                        </asp:Panel>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server" CssClass="tableOddRow" Height="37px" Font-Names="微软雅黑,宋体,Ubuntu;" Font-Size="Small">
                                <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Left" CssClass="" style="word-wrap: break-word;" BackColor="White" ColumnSpan="16">
                                    <asp:Label runat="server" CssClass="padding0px2px0px2px nowrap inlineblock">you might manage the data here if you can:<asp:Label runat="server" ID="tbbssmil_CountSummary" Font-Bold="true"></asp:Label></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Right" CssClass="" style="word-wrap: break-word;" BackColor="White" ColumnSpan="17">
                                    <asp:Label runat="server" CssClass="padding0px2px0px2px nowrap inlineblock">if you want to print some thing, print it here:<asp:Label runat="server" ID="tbbssmil_AmountSummary" Font-Bold="true"></asp:Label></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableFooterRow runat="server">
                                <asp:TableCell runat="server" ID="vELTableExampleInformationQueryItemList_ColSpanCell" ColumnSpan="33" Height="37px">
                                    <asp:Panel runat="server" Width="100%" CssClass="inlineblock footer">
                                        <asp:Panel runat="server" Height="100%" CssClass="inlineblock tableLeftArea padding4px">
                                            <asp:Label runat="server">Totally</asp:Label>
                                            <asp:Label runat="server" ID="vELTableExampleInformationQueryItemList_RowTotalCount" CssClass="RowTotalCount">&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                            <asp:Label runat="server">records</asp:Label>
                                            <asp:Label runat="server" Style="margin-left: 15px;">Each page</asp:Label>
                                            <asp:Label runat="server" Style="margin-left: 15px;display:none; *display: inline-block;">10</asp:Label>
                                            <asp:Label runat="server" CssClass="" Style="display:inline-block; *display: none;">
                                                <asp:DropDownList ID="vELTableExampleInformationQueryItemList_PerPageRowCount" data_id="vELTableExampleInformationQueryItemListPerPageRowCount" runat="server" Width="40" CssClass="Request Validation PerPageRowCount DATA DropDownList PureText pureText width40px padding0px2px0px2px margin0px2px0px2px" max_item_to_show="3" target_selector=".ResponseBody" request_location_path_name="{window.location.pathname}/ChangeExampleInformationQueryItemListPerPageRowCountTo" request_trigger_by_event_name="change unchange" sync_request="true" required="required" validation="Required Number">
                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Label>
                                            <%--asp:Label runat="server" ID="vbsml_tbsml_PerPageRowCount" data_id="TableExampleModificationListPerPageRowCount" CssClass="DATA PerPageRowCount"></asp:Label--%>
                                            <asp:Label runat="server" Style="margin-right: 15px;">records</asp:Label>
                                        </asp:Panel>
                                        <asp:Panel runat="server" HorizontalAlign="Right" CssClass=" inlineblock tableRightArea minWidth240px">
                                            <asp:Label runat="server">Totally</asp:Label>
                                            <asp:Label runat="server" ID="vELTableExampleInformationQueryItemList_PageTotalCount" data_id="vELTableExampleInformationQueryItemListPageTotalCount" CssClass="DATA PageTotalCount"></asp:Label>
                                            <%--asp:TextBox runat="server" ID="vbsml_tbsml_PageTotalCount" data_id="TableExampleModificationListPageTotalCount" CssClass="DATA PageTotalCount pureText width80px " Width="40px" ReadOnly="true"></asp:TextBox--%>
                                            <asp:Label runat="server">pages</asp:Label>
                                            <asp:Label runat="server" CssClass="margin0px2px0px2px">Turn to the</asp:Label>
                                            <asp:Label ID="vELTableExampleInformationQueryItemList_PageFirst" runat="server" CssClass="Request cursorPointer pageFirst disabled" target_selector=".ResponseBodyPanelListTable" request_location_search="?ChangeExampleInformationQueryItemListPageTo=First" Style="">FirstPage</asp:Label>
                                            <asp:Label ID="vELTableExampleInformationQueryItemList_PagePrev" runat="server" CssClass="Request cursorPointer pagePrev disabled" target_selector=".ResponseBodyPanelListTable" request_location_search="?ChangeExampleInformationQueryItemListPageTo=Prev" Style="">PrevPage</asp:Label>
                                            <asp:Label ID="vELTableExampleInformationQueryItemList_PageNext" runat="server" CssClass="Request cursorPointer pageNext disabled" target_selector=".ResponseBodyPanelListTable" request_location_search="?ChangeExampleInformationQueryItemListPageTo=Next" Style="">NextPage</asp:Label>
                                            <asp:Label ID="vELTableExampleInformationQueryItemList_PageLast" runat="server" CssClass="Request cursorPointer pageLast disabled" target_selector=".ResponseBodyPanelListTable" request_location_search="?ChangeExampleInformationQueryItemListPageTo=Last" Style="">LastPage</asp:Label>
                                            <%--<img name="ImageButton_pageFirst" class="Request cursorPointer" request_method_name="pageFirst" style="border-width: 0px;height:27px;position:relative;top:8px;" onclick="return false;" alt="" src="Resources/Images/public/内页1-1_03_15.png" />
                                                <img name="ImageButton_pagePrev" class="Request cursorPointer" request_method_name="pagePrev" style="border-width: 0px;height:27px;position:relative;top:8px;" onclick="return false;" alt="" src="Resources/Images/public/内页1-1_03_05.png" />
                                                <img name="ImageButton_pageNext" class="Request cursorPointer" request_method_name="pageNext" style="border-width: 0px;height:27px;position:relative;top:8px;" onclick="return false;" alt="" src="Resources/Images/public/内页1-1_03_17.png" />
                                                <img name="ImageButton_pageLast" class="Request cursorPointer" request_method_name="pageLast" style="border-width: 0px;height:27px;position:relative;top:8px;" onclick="return false;" alt="" src="Resources/Images/public/内页1-1_03_18.png" />
                                            --%>
                                            <asp:Label runat="server" CssClass="inlineblock">
                                                <asp:TextBox runat="server" ID="vELTableExampleInformationQueryItemList_CurrPageNum" data_id="vELTableExampleInformationQueryItemListCurrPageNum" CssClass="Validation DATA CurrPageNum margin0px2px0px2px" Style="width: 40px; text-align: center;" required="required" validation="Required">1</asp:TextBox>
                                            </asp:Label>
                                            <asp:Label runat="server" CssClass="margin0px2px0px2px">page</asp:Label>
                                            <%--img name="ImageButton_changePage" class="Request hide cursorPointer" target_selector=".ResponseBodyPanelListTable" request_location_search="?ChangeExampleInformationQueryItemListPageTo=" style="border-width: 0px;height:27px;position:relative;top:8px;" onclick="return false;" alt="GO" src="../../Resources/Images/public/内页1-1_03.png"/--%>
                                            <asp:Label name="ImageButton_changePage" runat="server" CssClass="Request cursorPointer margin0px2px0px2px pageButton" target_selector=".ResponseBodyPanelListTable" request_location_search="?ChangeExampleInformationQueryItemListPageTo=" Text="GO"></asp:Label>
                                        </asp:Panel>
                                    </asp:Panel>
                                </asp:TableCell>
                            </asp:TableFooterRow>
                        </asp:Table>
                        <asp:TextBox ID="vEL_rfjumpdata" runat="server" Width="100%" CssClass=" DATA pureText hide width0px"
										                        required="required" validation="Required" ReadOnly="true"></asp:TextBox>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
        </asp:View>

    </asp:MultiView>

    <script type="text/javascript" src="Scripts/Example/Example.js"></script>
    <!--/* vim: set si sts=4 ts=4 sw=4 fdm=indent :*/-->
</asp:Content>
