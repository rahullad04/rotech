<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="CatagoryMaster.aspx.vb" Inherits="CatagoryMaster" Title="Category Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        th.sortasc a
        {
            display: block;
            padding: 0 4px 0 15px;
            background: url(img/asc.gif) no-repeat;
        }
        
        th.sortdesc a
        {
            display: block;
            padding: 0 4px 0 15px;
            background: url(img/desc.gif) no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Category Master</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <div class="btn-group">
                        <button type="button" id="btnNew" class="btn btn-success" onclick="window.open('CatagoryAddEdit.aspx?type=Add','_self')">
                            Add New <i class="icon-plus icon-white"></i>
                        </button>
                    </div>
                    <div class="span12">
                    </div>
                    <asp:GridView runat="server" ID="vscatagory" ShowFooter="True" AutoGenerateColumns="False"
                        DataKeyNames="catid,CATEGORYNAME" OnRowDeleting="vsCatagory_RowDeleting" CellPadding="4"
                        ForeColor="#333333" GridLines="None" AllowSorting="True" OnSorting="vscategory_Sorting"
                        OnRowEditing="vscategory_RowEditing" OnRowDataBound="OnRowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/Edit.jpg"
                                        ToolTip="Edit" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="catid" SortExpression="catid" HeaderText="Id" ReadOnly="True"
                                Visible="false" />
                            <asp:BoundField DataField="clsname" SortExpression="clsname" ItemStyle-Width="100px"
                                HeaderText="CLASS" ReadOnly="True" />
                            <asp:BoundField DataField="CATEGORYNAME" SortExpression="CATEGORYNAME" ItemStyle-Width="200px"
                                HeaderText="CATEGORY NAME" ReadOnly="True" />
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
                        <SortedAscendingHeaderStyle CssClass="sortasc" BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle CssClass="sortdesc" BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            FormValidation.init();
        });
    </script>
</asp:Content>
