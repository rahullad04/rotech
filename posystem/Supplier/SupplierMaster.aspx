<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="SupplierMaster.aspx.vb" Inherits="SupplierMaster" Title="Supplier Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //    function nav() {
        //    //alert("<% =ResolveClientUrl("SupplierAddEdit.aspx") %>");
        //        window.location = "<% =ResolveClientUrl("SupplierAddEdit.aspx") %>";
        //    }
    </script>
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
                    Supplier Master</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <div id="alert_container">
                    </div>
                    <div class="btn-group">
                        <button type="button" id="btnNew" class="btn btn-success" onclick="window.open('SupplierAddEdit.aspx?type=Add','_self')">
                            Add New <i class="icon-plus icon-white"></i>
                        </button>
                    </div>
                    <div class="span12">
                    </div>
                    <asp:GridView ID="vssupplier" runat="server" AutoGenerateColumns="False" OnRowEditing="vssupplier_RowEditing"
                        DataKeyNames="id,Name" CellPadding="3" AllowSorting="True" ShowFooter="True"
                        OnSorting="vssupplier_Sorting" AllowPaging="True" PageSize="100" 
                        OnPageIndexChanging="OnPageIndexChanging" OnRowDataBound="OnRowDataBound" 
                        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                     
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" CommandName="Edit" runat="server" ImageUrl="~/img/Edit.jpg"
                                        ToolTip="Edit" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" runat="server" />
                                </ItemTemplate>

<ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id"
                                Visible="false" />
                            <asp:BoundField DataField="Name" ItemStyle-Width ="200px"  HeaderText="NAME" 
                                ReadOnly="True" SortExpression="Name" >
<ItemStyle Width="200px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="City" ItemStyle-Width ="100px" HeaderText="CITY" 
                                ReadOnly="True" SortExpression="City" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Phone" ItemStyle-Width ="150px" 
                                HeaderText="PHONE NO" ReadOnly="True" SortExpression="Phone" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CnName" ItemStyle-Width ="150px" 
                                HeaderText="CN NAME"    SortExpression="CnName" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="cnPhoneno" ItemStyle-Width ="150px" 
                                HeaderText="CN PHONENO" SortExpression="cnPhoneno" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Email" ItemStyle-Width ="150px" Visible =false  
                                HeaderText="EMAIL" ReadOnly="True" SortExpression="Email" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/img/delete.jpg"
                                        ToolTip="Delete" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
