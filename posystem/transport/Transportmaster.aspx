<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="Transportmaster.aspx.vb" Inherits="posystem_transport_Transportmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style type="text/css" >
inputbox
{ width :125px;
	
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
    <div class="span12" id="content">
        <div class="row-fluid">
            <!-- block -->
            <div class="block">
                <div class="navbar navbar-inner block-header">
                    <div class="muted pull-left">
                        Transpot Master</div>
                </div>

                 <div id="alert_container">
                        </div>
                <div class="block-content collapse in">
                    <div class="span12">
                       
                        
                       
                        <asp:GridView runat="server" ID="vstrans" ShowFooter="True" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                            DataKeyNames="id,drivername" OnRowCancelingEdit="vstrans_RowCancelingEdit" OnRowEditing="vstrans_RowEditing"
                            OnRowUpdating="vstrans_RowUpdating" OnRowDeleting="vstrans_RowDeleting" OnRowCommand="vstrans_RowCommand"
                            CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="True" OnSorting="vstrans_Sorting"
                            AllowPaging="True" PageSize="100" OnPageIndexChanging="OnPageIndexChanging">
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
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                <asp:TemplateField HeaderText="TRANSPOT NAME" ItemStyle-Width="50px" SortExpression="transpotname">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltranspot" runat="server" Text='<%# Eval("transpotname")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txttranspot"  Width="125px" runat="server" Text='<%# Eval("transpotname")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txttrans" Width="125px" runat="server" />
                                        <%--<asp:RequiredFieldValidator ID="rfvtrans" runat="server" ControlToValidate="txttrans"
                                            Text="*" ValidationGroup="validaiton" />--%>
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DRIVERNAME" ItemStyle-Width="50px" SortExpression="drivername">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldriver" runat="server"  Text='<%# Eval("drivername")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtdriver"  Width="125px" runat="server" Text='<%# Eval("drivername")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtdrive" Width="125px" runat="server" />
                                      
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VEHICALNO" ItemStyle-Width="50px" SortExpression="vehicalno">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvehical" runat="server" Text='<%# Eval("vehicalno")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtvehical" Width="125px" runat="server" Text='<%# Eval("vehicalno")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtv"  Width="125px" runat="server" />
                                    
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CONTACT NAME" ItemStyle-Width="150px" SortExpression="contactname">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontactname" runat="server" Text='<%# Eval("contactname")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtcontactname" Width="125px" runat="server" Text='<%# Eval("contactname")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtcontact" Width="125px" runat="server" />
                                        
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="CONTACT NO" ItemStyle-Width="50px" SortExpression="contactno">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontactno" runat="server" Text='<%# Eval("contactno")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtcontactno" Width="125px" runat="server" Text='<%# Eval("contactno")%>' />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtphone" Width="125px" runat="server" />
                                      
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
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
