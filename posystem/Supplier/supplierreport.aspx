<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="supplierreport.aspx.vb" Inherits="posystem_Supplier_supplierreport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .controls
        {
            margin-left: 180px;
        }
        .control-group1
        {
            margin-bottom: 10px;
            float: left;
            width: 50%;
        }
        .control-group2
        {
            margin-bottom: 10px;
            float: Right;
            width: 50%;
        }
        .lable-control
        {
            text-align: right;
            float: left;
            width: 160px;
            padding-top: 5px;
        }
        .lable-control .required
        {
            color: #E02222;
            font-size: 12px;
            padding-left: 2px;
        }
        .radioButtonList
        {
            list-style: none;
            margin: 0;
            padding: 0;
            text-align: left;
        }
        .radioButtonList.horizontal li
        {
            display: inline;
        }
        
        .radioButtonList label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Supplier Report</div>
                <div class="muted pull-right">
                    <asp:Label ID="lblpono" class="controls" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <!-- BEGIN FORM-->
                    <form action="#" id="form_sample_1" class="form-horizontal">
                    <fieldset>
                        <div id="alert_container">
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="radiobutton" CssClass="radioButtonList" RepeatDirection="Vertical"
                                    runat="server">
                                    <asp:ListItem>Classwise Supplier Report</asp:ListItem>
                                    <asp:ListItem>Citywise Supplier Report</asp:ListItem>
                                    <asp:ListItem>Supplier Price list</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="lblconn" class="controls" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button ID="btnviewreport" class="btn btn-primary" runat="server" Text="View Report"
                            OnClick="btnviewreport_click" />
                        <asp:Button ID="btnprint" class="btn btn-primary" runat="server" Text="Print Report"
                            OnClick="btnprint_Click" />
                        <br />
                        <br>
                    </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="uplreportview" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            
            <CR:CrystalReportViewer ID="CrytViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1"
                EnableTheming="True" HasCrystalLogo="False" HasGotoPageButton="False" HasRefreshButton="True" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="True"
                HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" ToolPanelView="None"  ShowAllPageIds="True" />
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="..\report\SupplierDetail.rpt">
                </Report>
            </CR:CrystalReportSource>
              <CR:CrystalReportViewer ID="CrytViewer2" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource2"
                EnableTheming="True" HasCrystalLogo="False" HasGotoPageButton="False" HasRefreshButton="True" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="True"
                HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" ToolPanelView="None"  ShowAllPageIds="True" />
                <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
                <Report FileName="..\report\Supplierdetailcitywise.rpt">
                </Report>
            </CR:CrystalReportSource>
              <CR:CrystalReportViewer ID="CrytViewer3" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource3"
                EnableTheming="True" HasCrystalLogo="False" HasGotoPageButton="False" HasRefreshButton="True" EnableDatabaseLogonPrompt="False" ReuseParameterValuesOnRefresh="True"
                HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" ToolPanelView="None"   ShowAllPageIds="True" />
                <CR:CrystalReportSource ID="CrystalReportSource3" runat="server">
                <Report FileName="..\report\supplierpart.rpt">
                </Report>
            </CR:CrystalReportSource>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnviewreport" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
