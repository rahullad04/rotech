<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    EnableViewState="true" CodeFile="Createpo.aspx.vb" Inherits="Po_Createpo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .lable-control
        {
            text-align: right;
            float: left;
            width: 160px;
            padding-top: 5px;
        }
        
        .lable-control2
        {
            text-align: right;
            float: left;
            width: 150px;
            padding-top: 3px;
        }
        .lable-control .required
        {
            color: #E02222;
            font-size: 12px;
            padding-left: 2px;
        }
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
        }
        .controls2
        {
            padding-left: 70%;
        }
        .opt
        {
            border-radius: 25px;
            border: 2px solid #73AD21;
            padding: 20px;
            width: 200px;
            height: 150px;
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
        .callabel
        {
            float: left;
            padding-top: 0;
            text-align: right;
            width: 310px;
        }
        
        .datalabel
        {
        	width =90px;
        }
    </style>
    <style type="text/css">
        .messagealert
        {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            $(".close").click(function () {
                $("#alert_container").fadeOut("slow", function () {
                    $(this).html("");
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Create PO</div>
                <div class="muted pull-right">
                    <asp:Label ID="lblpono" class="controls" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <div class="block-content collapse in">
                <div class="span12">
                    <!-- BEGIN FORM-->
                    <form action="#" id="form_sample_1" class="form-horizontal">
                    <fieldset>
                        <div id="alert_container">
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="control-group1">
                                    <label class="lable-control">
                                        Supplier Name<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cmbsupplier" AutoPostBack="true" OnSelectedIndexChanged="cmbsupplier_selectedindexchanged"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group2">
                                    <label class="lable-control">
                                        Refreance No <span class="required">*</span></label>
                                    <div class="controls2">
                                        <asp:TextBox ID="txtrefno" Text="As per last supply" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group1">
                                    <label class="lable-control">
                                        Delivery At<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cmbdeliveryat" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group2">
                                <label class="lable-control">
                                    Refrence Date <span class="required">*</span></label>
                                <div class="controls2">
                                    <asp:TextBox ID="txtrefdate" runat="server" EnableViewState="true" ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="refimgpop" ImageUrl="~/img/calender.png" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="refimgpop" TargetControlID="txtrefdate"
                                        runat="server" Format="yyyy/MM/dd"  />
                                </div>
                                </div>
                                <div class="control-group1">
                                    <label class="lable-control">
                                        Transpoter Name<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cmbtranspoter" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="control-group2">
                                    <label class="lable-control">
                                        Payment Terms<span class="required">*</span></label>
                                    <div class="controls2">
                                        <asp:TextBox ID="txtterms" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group1">
                                    <label class="lable-control">
                                        Price Basis<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:RadioButtonList ID="radioprice" CssClass="radioButtonList" RepeatDirection="Horizontal"
                                            runat="server">
                                            <asp:ListItem>To Pay</asp:ListItem>
                                            <asp:ListItem>Paid </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="control-group2">
                                    <label class="lable-control">
                                        Frieght & Other Charges<span class="required">*</span></label>
                                    <div class="controls2">
                                        <asp:TextBox ID="txtotherchanrges" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group1">
                                    <label class="lable-control">
                                        Special Instruation <span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtinstruct" TextMode="MultiLine" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Due Date <span class="required">*</span></label>
                                    <div class="controls2">
                                        <asp:TextBox ID="txtduedate" runat="server" EnableViewState="true" ReadOnly="true"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup" ImageUrl="~/img/calender.png" ImageAlign="Bottom"
                                            runat="server" />
                                        <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  PopupButtonID="imgpopup"  TargetControlID="txtduedate" runat="server"  Format="dd/MM/yyyy" />--%>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgpopup" TargetControlID="txtduedate"
                                            runat="server" Format="yyyy/MM/dd"  />
                                    </div>
                                </div>
                            
                        <br />
                        <asp:UpdatePanel ID="Updategridview" UpdateMode="Conditional" 
                            runat="server">
                            <ContentTemplate>
                                <div style="padding-left: 6%;">
                                    <asp:GridView ID="vscreatepo" runat="server" ShowFooter="True" AutoGenerateColumns="False" 
                                        EnableViewState="true" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="true"
                                        OnDataBound="OnDataBound" OnRowCommand="vscreatepo_RowCommand" OnRowDeleting="vscreatepo_RowDeleting">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/img/AddNewitem.jpg" CommandName="AddNew"
                                                        Width="30px" Height="30px" ToolTip="Add New part" ValidationGroup="validaiton" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PartName" ItemStyle-Width="250">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartname" runat="server" Text='<%# Bind("partname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="cmbpartname" OnSelectedIndexChanged="cmbpartname_selectedindexchanged"
                                                                runat="server" DataTextField="PartName" AutoPostBack="true" DataValueField="id">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cmbsupplier" EventName="selectedindexchanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                    <%-- <asp:DropDownList ID="cmbpartname" CssClass="cmbpartname" runat="server">
                                                        </asp:DropDownList>--%>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrate" runat="server" Text='<%# Bind("Rate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtpartrate" Width="100px" runat="server"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="cmbpartname" EventName="selectedindexchanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblqty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtpartqty" Width="50px" AutoPostBack="true" OnTextChanged="txtpartqty_textchanged"
                                                                runat="server"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="unit" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblunit" runat="server" Text='<%# Bind("unit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbunit" runat="server" Width="100px" DataTextField="unittype"
                                                        DataValueField="id">
                                                    </asp:DropDownList>
                                                    </ContentTemplate>
                                                    </asp:UpdatePanel> 
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <%--<asp:Label ID="lblamt" runat="server" Text=""></asp:Label>--%>
                                                    <asp:TextBox ID="lblamt" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                                        ImageUrl="~/img/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row-fluid">
                            <div class="span2">
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                            
                                                        <div class="span3">
                                <div>
                                    <asp:RadioButtonList ID="rblexise" CssClass="radioButtonList" AutoPostBack="true"
                                        RepeatDirection="Vertical" runat="server" OnSelectedIndexChanged="rblexise_selectedindexchanged">
                                        <asp:ListItem>Excise duty(12.5%)</asp:ListItem>
                                        <asp:ListItem>As Applicable </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="span3">
                                <div>
                                    <asp:RadioButtonList ID="rblcal" CssClass="radioButtonList" AutoPostBack="true" RepeatDirection="Vertical"
                                        runat="server" OnSelectedIndexChanged="rblcal_selectedindexchanged">
                                        <asp:ListItem>In Gujarat</asp:ListItem>
                                        <asp:ListItem>Out Gujarat(2%) </asp:ListItem>
                                        <asp:ListItem>Out Gujarat(15%) </asp:ListItem>
                                        <asp:ListItem>Others </asp:ListItem>
                                        <asp:ListItem>Expert </asp:ListItem>
                                        <asp:ListItem>As Applicable </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="span3">
                                <asp:UpdatePanel ID="Updatepanelcontrol" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="control-group2">
                                           <asp:Label ID="Label2" CssClass="lable-control2" runat="server" Text="Sub Total"></asp:Label>
                                            <div class="controls2" style="width :100px;text-align:right;" >
                                                <asp:Label ID="lblsubtotal"  runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="control-group2">
                                            <asp:Label ID="labelrate" CssClass="lable-control2" runat="server" Text="Excise Duty %"></asp:Label>
                                            <div class="controls2" style="width :100px;text-align:right;" >
                                                <asp:Label ID="lblrate" runat="server" Text="0"> </asp:Label>
                                            </div>
                                        </div>
                                        <div class="control-group2">
                                            <asp:Label ID="lblvat" CssClass="lable-control2" runat="server" Text=" Vat/CST@ %"></asp:Label>
                                            <div class="controls2" style="width :100px;text-align:right;" >
                                                <asp:Label ID="lblvatcst"  runat="server" Text="0"> </asp:Label>
                                            </div>
                                        </div>
                                        <div class="control-group2">
                                            <asp:Label ID="lbladl" CssClass="lable-control2" runat="server" Text="Adl Tax@%"></asp:Label>
                                            <div class="controls2" style="width :100px;text-align:right;"  >
                                                <asp:Label ID="lbladltax" runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="control-group2">
                                            <asp:Label ID="Label1" CssClass="lable-control2" runat="server" Text="Total"></asp:Label>
                                            <div class="controls2" style="width :100px;text-align:right;"  >
                                                <asp:Label ID="lbltotal"  runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rblcal" EventName="selectedindexchanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <br />

                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btncancle" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="form-actions">
                            <asp:Button ID="btnSuccess" runat="server" Text="View Report" type="Button" class="btn btn-primary"
                                OnClick="btnSubmit_Click" />
                            <asp:Button ID="btncancle" class="btn" runat="server" Text="Cancle" OnClick="btncancle_Click" />
                            <asp:Label ID="lblpoid" runat="server" Visible="false"></asp:Label><asp:Label ID="lbluname"
                                runat="server" Text="Label"></asp:Label>
                        </div>
                    </fieldset>
                    </form>
                </div>
            </div>
            <!-- END FORM-->
        </div>
    </div>
</asp:Content>
