<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="UnitMaster.aspx.vb" Inherits="UnitMaster" Title="Unit Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="span9" id="content">
        <div class="row-fluid">
            <!-- block -->
            <div class="block">
                <div class="navbar navbar-inner block-header">
                    <div class="muted pull-left">
                        Unit Master</div>
                </div>
                <div class="block-content collapse in">
                    <div class="span12">
                        <%-- <div class="table-toolbar">
                                      <div class="btn-group">
                                         <a href="#"><button class="btn btn-success">Add New <i class="icon-plus icon-white"></i></button></a>
                                      </div>
                                      <div class="btn-group pull-right">
                                         <button data-toggle="dropdown" class="btn dropdown-toggle">Tools <span class="caret"></span></button>
                                         <ul class="dropdown-menu">
                                            <li><a href="#"Z>Print</a></li>
                                            <li><a href="#">Save as PDF</a></li>
                                            <li><a href="#">Export to Excel</a></li>
                                         </ul>
                                      </div>
                                   </div>

                                      <div id="example2_wrapper" class="dataTables_wrapper form-inline" role="grid">
                                                                    

                                        </div>--%>
                        <asp:GridView runat="server" ID="vsUnit" ShowFooter="True" AutoGenerateColumns="False"
                            DataKeyNames="id,unittype" OnRowCancelingEdit="vsunit_RowCancelingEdit" OnRowEditing="vsunit_RowEditing"
                            OnRowUpdating="vsunit_RowUpdating" OnRowDeleting="vsunit_RowDeleting" OnRowCommand="vsunit_RowCommand"
                            CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" OnSorting="vsUnit_Sorting"
                            PageSize="100" OnPageIndexChanging="OnPageIndexChanging" OnRowDataBound="OnRowDataBound">
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
                                            Width="30px" Height="30px" ToolTip="Add New Unit" ValidationGroup="validaiton" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="Id" SortExpression="Id" ReadOnly="true"
                                    Visible="false" />
                                <asp:TemplateField HeaderText="Unit Type" ItemStyle-Width="150px" SortExpression="UnitType">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnittype" runat="server" Text='<%# Eval("unittype")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtunittype" runat="server" Text='<%# Eval("unittype")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtunit" runat="server" />
                                       <%-- <asp:RequiredFieldValidator ID="rfvunittype" runat="server" ControlToValidate="txtunit"
                                            Text="*" ValidationGroup="validaiton" />--%>
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/img/Cancel.jpg"
                                            ToolTip="Cancel" Height="20px" Width="20px" />
                                    </EditItemTemplate>
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
                </div>
            </div>
            <!-- /block -->
        </div>
    </div>
    <div>
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>
</asp:Content>
