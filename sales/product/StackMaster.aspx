<%@ Page Language="VB" MasterPageFile="~/sales.master" AutoEventWireup="false"
    CodeFile="StackMaster.aspx.vb" Inherits="StackMaster" Title="Stack Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="span9" id="content">
        <div class="alert alert-error hide">
            <button class="close" data-dismiss="alert">
            </button>
            You have some form errors. Please check below
            <asp:Label ID="lblresult" runat="server"></asp:Label>
        </div>
        <div class="row-fluid">
            <!-- block -->
            <div class="block">
                <div class="navbar navbar-inner block-header">
                    <div class="muted pull-left">
                        Stack Master</div>
                </div>
                <div class="block-content collapse in">
                    <div class="span12">
                        <asp:GridView runat="server" ID="vsStack" ShowFooter="True" AutoGenerateColumns="False"
                            DataKeyNames="id,stackname" OnRowCancelingEdit="vsStack_RowCancelingEdit" OnRowEditing="vsStack_RowEditing"
                            OnRowUpdating="vsStack_RowUpdating" OnRowDeleting="vsStack_RowDeleting" OnRowCommand="vsStack_RowCommand"
                            CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" OnSorting="vsstack_Sorting"
                            OnRowDataBound="OnRowDataBound" AllowPaging="true" PageSize="100" OnPageIndexChanging="OnPageIndexChanging">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/img/update.jpg"
                                            ToolTip="Update" Height="20px" Width="20px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/Edit.jpg"
                                            ToolTip="Edit" Height="20px" Width="20px" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/img/AddNewitem.jpg" CommandName="AddNew"
                                            Width="30px" Height="30px" ToolTip="Add new User" ValidationGroup="validaiton" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="true" SortExpression="id"
                                    Visible="false" />
                                <asp:TemplateField HeaderText="Stack Name" SortExpression="stackname">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstackname" runat="server" Text='<%# Eval("stackname")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtstackname" runat="server" Text='<%# Eval("stackname")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtstack" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvstackname" runat="server" ControlToValidate="txtstack"
                                            Text="*" ValidationGroup="validaiton" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/img/Cancel.jpg"
                                            ToolTip="Cancel" Height="20px" Width="20px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" runat="server" ImageUrl="~/img/delete.jpg"
                                            ToolTip="Delete" Height="20px" Width="20px" />
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
                </div>
            </div>
            <!-- /block -->
        </div>
    </div>
</asp:Content>
