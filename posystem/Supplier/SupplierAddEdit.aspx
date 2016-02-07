<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="SupplierAddEdit.aspx.vb" Inherits="Supplier_SupplierAddEdit" Title="Supplier Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .lable-control
        {
            text-align: right;
            float: left;
            width: 110px;
            padding-top: 4px;
        }
        .lable-control .required
        {
            color: #E02222;
            font-size: 12px;
            padding-left: 2px;
        }
        .controls
        {
            margin-left: 120px;
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
    <div class="row-fluid">
        <!-- block -->
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Supplier Detail</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <!-- BEGIN FORM-->
                    <form action="#" id="form_sample_1" class="form-horizontal">
                    <fieldset>
                        <div id="alert_container">
                        </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" ChildrenAsTriggers="true"
                            runat="server">
                            <ContentTemplate>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Supplier Name<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtname" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Address<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtaddress" data-required="1" TextMode="MultiLine" class="span6 m-wrap"
                                            runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        CITY<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtcity" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        TIN<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txttin" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        CST<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtcst" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Eccno<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txteccno" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Phone No<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtphone" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Contact Name<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtcnname" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Contact PhoneNo<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtmobile" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Email<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtemail" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="control-group">
                                    <label class="lable-control">
                                        Supplier Class<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtclass" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="control-group">
                                    <label class="lable-control">
                                        Terms
                                    </label>
                                    <div class="controls">
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="against" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label1" runat="server" Text="  Against Perform"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="Immediate" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label2" runat="server" Text="  Immediate"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="days15" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label3" runat="server" Text="15 Days"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="days30" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label4" runat="server" Text="30 Days"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="days45" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label5" runat="server" Text="45 Days"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="days60" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label6" runat="server" Text="60 Days"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButton ID="days90" GroupName="Terms" runat="server" />
                                                        <asp:Label ID="Label7" runat="server" Text="90 Days"></asp:Label>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="form-actions">
                            <asp:Button ID="btnSubmit" class="btn btn-primary" OnClick="btnsubmit_Click" runat="server"
                                Text="Save" />
                            <asp:Button ID="btncancle" class="btn btn-primary" runat="server" Text="Reset" />
                            <asp:Label ID="lblid" runat="server"></asp:Label>
                        </div>
                    </fieldset>
                    </form>
                    <!-- END FORM-->
                </div>
            </div>
        </div>
        <!-- /block -->
        <!-- /validation -->
        <script src='<%= ResolveClientUrl("~/js/validation/form-validation.js")%>' type="text/javascript"></script>
        <script src='<%= ResolveClientUrl("~/js/validation/jquery.validate.min.js")%>' type="text/javascript"></script>
      
            <!-- block -->
            <div class="row-fluid">
        <!-- block -->
        
            <div class="span7">
                <div class="block">
                    <div class="navbar navbar-inner block-header">
                        <div class="muted pull-left">
                            Supplier Model
                        </div>
                    </div>
                    <div class="block-content collapse in">
                        <asp:UpdatePanel ID="updatesuppliermodel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="vssuppliermodel" runat="server" AutoGenerateColumns="False" Width="535px"
                                    Height="45px" CellPadding="4" DataKeyNames="id,partid" ForeColor="#333333" GridLines="None"
                                    OnRowDeleting="vssuppliermodel_rowdeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" CommandName="Edit" runat="server" ImageUrl="~/img/Edit.jpg"
                                                    ToolTip="Edit" Height="20px" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" Visible="false" />
                                        <asp:BoundField DataField="classname" HeaderText="classname" ItemStyle-Width="5%"
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="categoryname" HeaderText="categoryname" ItemStyle-Width="30%"
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="partid" HeaderText="partid" ReadOnly="True" Visible="False" />
                                        <asp:BoundField DataField="Partname" HeaderText="Partname" ItemStyle-Width="40%"
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </div>
            </div>
            <div class="span5">
            <div class="block">
                    <div class="navbar navbar-inner block-header">
                        <div class="muted pull-left">
                            Supplier Model Detail</div>
                    </div>
                     <asp:UpdatePanel ID="Updatesupplier" UpdateMode="Conditional" ChildrenAsTriggers="true"
                        runat="server">
                        <ContentTemplate>
                            <div class="block-content collapse in">
                                <div class="control-group">
                                    <label class="lable-control">
                                        Class Name <span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="cmbclass" AutoPostBack="true" OnSelectedIndexChanged="cmbclass_selectedindexchanged"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="control-group">
                                            <label class="lable-control">
                                                Category Name<span class="required">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="cmbcategory" AutoPostBack="true" OnSelectedIndexChanged="cmbcategory_selectedindexchanged"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cmbclass" EventName="selectedindexchanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="control-group">
                                            <label class="lable-control">
                                                Part Name<span class="required">*</span></label>
                                            <div class="controls">
                                                <asp:DropDownList ID="cmbpartname" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cmbcategory" EventName="selectedindexchanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="control-group">
                                    <label class="lable-control">
                                        Rate<span class="required">*</span></label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtrate" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Label ID="lblspid" runat="server" Visible="false" Text="Label"></asp:Label>
                                    <asp:Button ID="btnAdd" class="btn btn-primary" runat="server" Text="Add" />
                                    <%-- <a href="#myAlert" data-toggle="modal" class="btn btn-danger">Alert</a>
                            <div id="myAlert" class="modal hide">
											<div class="modal-header">
												<button data-dismiss="modal" class="close" type="button">&times;</button>
												<h3>Alert modal</h3>
											</div>
											<div class="modal-body">
												<p>Lorem ipsum dolor sit amet...</p>
											</div>
											<div class="modal-footer">

                                <asp:Button ID="Button1" runat="server" Text="Button" />
												<a data-dismiss="modal" class="btn btn-primary" href="#">Confirm</a>
												<a data-dismiss="modal" class="btn" href="#">Cancel</a>
											</div>
										</div>--%>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    </div> 
                    
            </div>
            </div> 
           
        
    </div>
    <script type="text/javascript" src="../../js/bootstrap.min.js"></script>
</asp:Content>
