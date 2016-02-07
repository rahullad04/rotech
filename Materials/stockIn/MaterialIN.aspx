<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="MaterialIN.aspx.vb" Inherits="Materials_stockIn_MaterialIN" %>

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
            width: 160px;
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
        
        .leftdiv
        {
            width: 48%;
            float: left;
        }
        .rightdiv
        {
            width: 51%;
            float: right;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <div id="alert_container">
    </div>
    <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Stock IN Entry</div>
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
                        <div class="control-group1">
                            <label class="lable-control">
                                Purchase Order No<span class="required">*</span></label>
                            <div class="controls">
                                <asp:DropDownList ID="cmbpono" AutoPostBack="true" OnSelectedIndexChanged="cmbpono_selectedindexchanged"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="control-group1">
                            <label class="lable-control">
                                Supplier Name<span class="required">*</span></label>
                            <div class="controls">
                                <asp:DropDownList ID="cmbsuppliername" AutoPostBack="true" OnSelectedIndexChanged="cmbsuppliername_selectedindexchanged"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                       <%-- <asp:UpdatePanel ID="Updategridview" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>--%>
                                <div style="padding-left: 6%;">
                                    <asp:GridView ID="vsstockin" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                        EnableViewState="true" CellPadding="4" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="true"
                                        OnDataBound="OnDataBound" OnRowCommand="vsstockin_RowCommand" OnRowDeleting="vsstockin_RowDeleting">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/img/AddNewitem.jpg" CommandName="AddNew"
                                                        Width="30px" Height="30px" ToolTip="Add New part" ValidationGroup="validaiton" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="true" />
                                            <asp:TemplateField HeaderText="CategorName" ItemStyle-Width="250">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcatname" runat="server" Text='<%# Bind("catname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="cmbcatname" OnSelectedIndexChanged="cmbcatname_selectedindexchanged"
                                                        runat="server" DataTextField="catname" AutoPostBack="true" DataValueField="id">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PartName" ItemStyle-Width="250">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartname" runat="server" Text='<%# Bind("partname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="cmbpartname" OnSelectedIndexChanged="cmbpartname_selectedindexchanged"
                                                        runat="server" DataTextField="PartName" AutoPostBack="true" DataValueField="id">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Orderqty" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbloqty" runat="server" Text='<%# Bind("orderqty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtoqty" Width="100px" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receive qty" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRqty" runat="server" Text='<%# Bind("receiveqty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtrqty" Width="100px" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Accept Qty" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblaqty" runat="server" Text='<%# Bind("acceptqty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtaqty" Width="100px" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reject Qty" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrjqty" runat="server" Text='<%# Bind("rejectqty") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtrjqty" Width="100px" runat="server"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </fieldset>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
