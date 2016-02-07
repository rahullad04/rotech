<%@ Page Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false"
    CodeFile="ClientMaster.aspx.vb" Inherits="Client_ClientMaster" Title="Untitled Page" %>

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
                    Customer Master</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <div class="btn-group">
                        <button type="button" id="btnNew" class="btn btn-success" onclick="window.open('ClientAddEdit.aspx?type=Add','_self')">
                            Add New <i class="icon-plus icon-white"></i>
                        </button>
                    </div>
                    <div class="btn-group">
                        <button data-toggle="dropdown" class="btn btn-info dropdown-toggle">
                            Select <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu">
                            <li><a href="#">By Customer Name</a></li>
                            <li><a href="#">By City</a></li>
                        </ul>
                    </div>
                    <asp:TextBox ID="txtsearch" runat="server"></asp:TextBox>
                    <div class="span12">
                    </div>
                    <asp:GridView ID="vsclient" runat="server" AutoGenerateColumns="False" OnRowEditing="vsclient_RowEditing"
                        DataKeyNames="clientid,Name" CellPadding="4" ForeColor="#333333" GridLines="None"
                        AllowSorting="True" OnSorting="vsclient_Sorting" AllowPaging ="true" PageSize=100  OnPageIndexChanging="OnPageIndexChanging">
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle CssClass="sortasc" BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle CssClass="sortasc" BackColor="#6F8DAE" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" CommandName="Edit" runat="server" ImageUrl="~/img/Edit.jpg"
                                        ToolTip="Edit" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ClientId" HeaderText="ID" ReadOnly="True" SortExpression="ClientId"
                                Visible="false" />
                            <asp:BoundField DataField="Name" HeaderText="NAME" ReadOnly="True" SortExpression="Name" />
                            <asp:BoundField DataField="Address" HeaderText="ADDRESS" ReadOnly="True" SortExpression="Address" />
                            <asp:BoundField DataField="CIty" HeaderText="City" ReadOnly="True" SortExpression="City" />
                            <asp:BoundField DataField="Phoneno" HeaderText="PHONE NO" ReadOnly="True" SortExpression="Phoneno" />
                            <asp:BoundField DataField="faxno" HeaderText="FAX NO" SortExpression="faxno" />
                            <asp:BoundField DataField="TIN" HeaderText="TIN" ReadOnly="True" SortExpression="TIN" />
                            <asp:BoundField DataField="CST" HeaderText="CST" ReadOnly="True" SortExpression="CST" />
                            <asp:BoundField DataField="Email" HeaderText="EMAIL" ReadOnly="True" SortExpression="Email" />
                            <asp:BoundField DataField="cn_name" HeaderText="Contact Person Name" SortExpression="cn_name" />
                            <asp:BoundField DataField="Cnphone_no" HeaderText="Contact Person Phn no" ReadOnly="True"
                                SortExpression="Cnphone_no" Visible="False" />
                            <asp:BoundField DataField="Terms" HeaderText="Terms" SortExpression="Terms" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="../img/delete.jpg"
                                        ToolTip="Delete" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
 </asp:Content>
