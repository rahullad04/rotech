<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="ClassMaster.aspx.vb" Inherits="classmaster" Title="Class Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="span9" id="content">
        <div class="row-fluid">
            <!-- block -->
            <div class="block">
                <div class="navbar navbar-inner block-header">
                    <div class="muted pull-left">
                        Class Master</div>
                </div>
                <div class="block-content collapse in">
                    <div class="span12">
                        <asp:GridView runat="server" ID="vsclass" ShowFooter="True" AutoGenerateColumns="False"
                            DataKeyNames="id,classname" OnRowCancelingEdit="vsclass_RowCancelingEdit" OnRowEditing="vsclass_RowEditing"
                            OnRowUpdating="vsclass_RowUpdating" OnRowDeleting="vsclass_RowDeleting" OnRowCommand="vsclass_RowCommand"
                            CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" OnSorting="vsclass_Sorting"
                            AllowPaging="True" PageSize="100" OnPageIndexChanging="OnPageIndexChanging" OnRowDataBound="OnRowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" runat="server" ImageUrl="~/Img/update.jpg"
                                            ToolTip="Update" Height="20px" Width="20px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Img/Edit.jpg"
                                            ToolTip="Edit" Height="20px" Width="20px" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:ImageButton ID="imgbtnAdd" runat="server" ImageUrl="~/Img/AddNewitem.jpg" CommandName="AddNew"
                                            Width="30px" Height="30px" ToolTip="Add new User" ValidationGroup="validaiton" />
                                    </FooterTemplate>
                                  
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                        <itemtemplate>
                                    <asp:Label ID="lblRowNumber" runat="server" />
                                </itemtemplate>
                                    </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                <asp:TemplateField HeaderText="CLASS NAME" ItemStyle-Width="150px" SortExpression="classname">
                                    <ItemTemplate>
                                        <asp:Label ID="lblclassname" runat="server" Text='<%# Eval("CLASSNAME")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCLASSNAME" runat="server" Text='<%# Eval("CLASSNAME")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtCLASS" runat="server" />
                                       <%-- <asp:RequiredFieldValidator ID="rfvCLASSNAME" runat="server" ControlToValidate="txtCLASS"
                                            Text="*" ValidationGroup="validaiton" />--%>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Img/Cancel.jpg"
                                            ToolTip="Cancel" Height="20px" Width="20px" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                            ImageUrl="~/Img/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
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
    <div>
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>
</asp:Content>
