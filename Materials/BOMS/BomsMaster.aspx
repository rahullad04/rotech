<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false" CodeFile="BomsMaster.aspx.vb" Inherits="Materials_BOMS_BomsMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
 <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    BOMS Master</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <div id="alert_container">
                    </div>
                    <div class="btn-group">
                        <button type="button" id="btnNew" class="btn btn-success" onclick="window.open('BomsAdd.aspx?type=Add','_self')">
                            Add New Bom<i class="icon-plus icon-white"></i>
                        </button>
                    </div>
                    <%--<div class="btn-group">
                        <button type="button" id="Button1" class="btn btn-success" onclick="window.open('BomsAdd.aspx?type=Add','_self')">
                            Add New BOMS <i class="icon-plus icon-white"></i>
                        </button>
                    </div>--%>
                    <div class="span12">
                    </div>
                    <asp:GridView ID="vsboms" runat="server" AutoGenerateColumns="False" OnRowEditing="vsboms_RowEditing"
                        DataKeyNames="id,ModelName" CellPadding="4" ForeColor="#333333" GridLines="Both" 
                        AllowSorting="True"  AllowPaging="true" PageSize="100"
                        OnPageIndexChanging="OnPageIndexChanging" OnRowDataBound="OnRowDataBound" ShowFooter="true" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" SortExpression="Id"
                                Visible="false" />
                             <asp:BoundField DataField="modelid" HeaderText="modelID" ReadOnly="True" SortExpression="modelId"
                                Visible="false" />
                            <asp:BoundField DataField="ModelName" ItemStyle-Width="250px" HeaderText="MODEL NAME"
                                ReadOnly="True" SortExpression="ModelName" />
                            
                            <asp:BoundField DataField="Productname" HeaderText="PRODUCT NAME" ItemStyle-Width="150px"
                                ReadOnly="True" SortExpression="Productname" />
                          
                            <asp:BoundField DataField="Stack" HeaderText="STACK" ItemStyle-Width="100px" ReadOnly="True"
                                SortExpression="STACK" />
                       
                            <asp:BoundField DataField="BOMSRATE" HeaderText="BOMSRATE" ItemStyle-Width="100px"
                                ReadOnly="True" SortExpression="BOMSRATE" />
                            <asp:TemplateField>
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
    </div>
    <div>
    </div>
</asp:Content>

