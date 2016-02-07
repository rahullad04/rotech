<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="PartMaster.aspx.vb" Inherits="Part_PartMaster" Title="Part Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      
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
                    Part Master</div>
            </div>
            <div id="alert_container">
                        </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <div class="btn-group">
                        <button type="button" id="btnNew" class="btn btn-success" onclick="window.open('PartAddEdit.aspx?type=Add','_self')">
                            Add New <i class="icon-plus icon-white"></i>
                        </button>
                    </div>
                    <div class="span12">
                    </div>
                    <asp:GridView runat="server" ID="vspart" ShowFooter="True" AutoGenerateColumns="False"
                        AllowSorting="True" OnSorting="vspart_Sorting"  
                        DataKeyNames="pmid,partname" CellPadding="4"
                        ForeColor="#333333" GridLines="both" AllowPaging="True" 
                        OnRowDataBound="OnRowDataBound" PageSize="100" 
                        OnPageIndexChanging="OnPageIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" runat="server" ImageUrl="~/Img/Edit.jpg"
                                        ToolTip="Edit" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" runat="server" />
                                </ItemTemplate>

<ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="pmid" SortExpression="pmid" ItemStyle-Width="150px" 
                                HeaderText="Id" ReadOnly="True"
                                Visible="false" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="clsname" SortExpression="clsname" 
                                ItemStyle-Width="80px" HeaderText="CLASS" ReadOnly="True" >
<ItemStyle Width="80px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Catname" SortExpression="Catname" 
                                ItemStyle-Width="150px" HeaderText="CATEGORY NAME" ReadOnly="True" >
<ItemStyle Width="150px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Partname" SortExpression ="Partname" 
                                ItemStyle-Width="350px" HeaderText="PART NAME"  ReadOnly="True" >
                          
<ItemStyle Width="350px"></ItemStyle>
                            </asp:BoundField>
                          
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                        ImageUrl="~/Img/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:Label ID="lblresult" runat="server"></asp:Label>
    </div>
</asp:Content>
