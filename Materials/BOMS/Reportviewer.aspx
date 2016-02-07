<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false" CodeFile="Reportviewer.aspx.vb" Inherits="Materials_BOMS_Reportviewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    View PO</div>
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
                                <div class="control-group">
                                    <label class="lable-control">
                                        Model Name </label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cmbModelname" 
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                          </div>
                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                          <asp:Button ID="btnviewreport"  class="btn btn-primary" runat="server" Text="View Report" OnClick ="btnviewreport_click"/>
                          <asp:Button ID="btnprint"  class="btn btn-primary" runat="server" Text="Print Report" OnClick ="btnprint_Click"/>
                        <br />
                        <br>
                        <asp:UpdatePanel ID="UpdatePanel2"  runat="server">
                        <ContentTemplate>
                        <CR:CrystalReportViewer ID="CrytViewer" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1"
                            EnableTheming="True" HasCrystalLogo="False" HasGotoPageButton="False" HasRefreshButton="True"
                            HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" ToolPanelView="None" />
                        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            <Report FileName="..\report\boms.rpt">
                            </Report>
                        </CR:CrystalReportSource>

                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnviewreport" EventName="click" />
                      </Triggers>
                        </asp:UpdatePanel>

                        
                    </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

