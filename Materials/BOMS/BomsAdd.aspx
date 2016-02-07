<%@ Page Title="" Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="BomsAdd.aspx.vb" Inherits="Materials_BOMS_BomsAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .lable-control
        {
            text-align: right;
            float: left;
            width: 160px;
            padding-top: 5px;
        }
        
        .lable-control2
        {
            text-align: right;
            float: left;
            width: 160px;
            padding-top: 3px;
        }
        .lable-control .required
        {
            color: #E02222;
            font-size: 12px;
            padding-left: 2px;
        }
        .controls
        {
            margin-left: 180px;
        }
        .control-group1
        {
            margin-bottom: 10px;
            float: left;
            width: 50%;
        }
        .control-group2
        {
            margin-bottom: 10px;
        }
        .controls2
        {
            padding-left: 70%;
        }
        
        .leftdiv
        {
            width: 48%;
            float: left;
        }
        .rightdiv
        {
            width: 51%;
            float: right;
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
    <div id="alert_container">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="row-fluid">
        <!-- block -->
        <div class="span6">
            <div class="block">
                <div class="navbar navbar-inner block-header">
                    <div class="muted pull-left">
                        Select Model Name to Create BOMS</div>
                </div>
                <div class="block-content collapse in">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="control-group">
                                <label class="lable-control">
                                    Product Type</label>
                                <div class="controls">
                                    <asp:DropDownList ID="cmbproducttype" AutoPostBack="true" EnableViewState="true"
                                        OnSelectedIndexChanged="cmbproducttype_selectedindexchanged" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblmodelid" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="lable-control">
                                    Model Name<span class="required">*</span></label>
                                <div class="controls">
                                    <asp:DropDownList ID="cmbmodel" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="cmbmodel_selectedindexchanged"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="block">
                <div class="navbar navbar-inner block-header">
                    <div class="muted pull-left">
                        Copy Data From Existing</div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="block-content collapse in">
                            <!-- BEGIN FORM-->
                            <div class="control-group">
                                <label class="lable-control">
                                    Search ModelName</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtmodelname" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="lable-control">
                                    Model Name<span class="required">*</span></label>
                                <div class="controls">
                                    <asp:DropDownList ID="cmbmodelname" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="cmbmodelname_selectedindexchanged"
                                        runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblbid" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnCopy" class="btn btn-primary" runat="server" Text="Insert To BOMS"
                                    OnClick="btncopy_click" />
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Reset" />
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- END FORM-->
            </div>
        </div>
    </div>
    <div class="span6">
        <div class="block">
            <div class="navbar navbar-inner block-header">
                <div class="muted pull-left">
                    Insert Data One By One</div>
            </div>
            <div class="block-content collapse in">
                <!-- BEGIN FORM-->
                <form action="#" id="form_sample_1" class="form-horizontal">
                <fieldset>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="control-group">
                                <label class="lable-control">
                                    Class <span class="required">*</span></label>
                                <div class="controls">
                                    <asp:DropDownList ID="cmbclass" AutoPostBack="true" OnSelectedIndexChanged="cmbclass_selectedindexchanged"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="control-group">
                                        <label class="lable-control">
                                            Category <span class="required">*</span></label>
                                        <div class="controls">
                                            <asp:DropDownList ID="cmbcategory" DataTextField="classname" OnSelectedIndexChanged="cmbcategory_selectedindexchanged"
                                                AutoPostBack="true" DataValueField="id" runat="server">
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
                                            <asp:DropDownList ID="cmbpartname" OnSelectedIndexChanged="cmbpartname_selectedindexchanged"
                                                AutoPostBack="true" DataTextField="Partname" runat="server">
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
                                    Qty</label>
                                <div class="controls">
                                    <asp:TextBox ID="txtqty" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                                    <asp:DropDownList ID="cmbunit" AutoPostBack="true" Width="100px" EnableViewState="true"
                                        runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="uplsuppliername" UpdateMode="Conditional" ChildrenAsTriggers="true"
                                runat="server">
                                <ContentTemplate>
                                    <div class="control-group">
                                        <label class="lable-control">
                                            Rate</label>
                                        <div class="controls">
                                            <asp:TextBox ID="txtrate" data-required="1" Width="85px" class="span6 m-wrap" runat="server"></asp:TextBox>
                                            <asp:DropDownList ID="cmbsupplier" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="cmbsupplier_selectedindexchanged"
                                                EnableViewState="true" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btncancle" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </fieldset>
                </form>
                <div class="form-actions">
                    <asp:Button ID="btnSubmit" class="btn btn-primary" OnClick="btnSubmit_Click" runat="server"
                        Text="Insert To BOMS" />
                    <asp:Button ID="btncancle" class="btn btn-primary" OnClick="btncancle_click" runat="server"
                        Text="Reset" />
                    <asp:Label ID="lblid" runat="server"></asp:Label>
                </div>
                <!-- END FORM-->
            </div>
        </div>
    </div>
    <!-- block -->
    <div class="row-fluid">
        <div class="span12">
            <!-- block -->
            <asp:UpdatePanel ID="Uplboms" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="block">
                        <div class="navbar navbar-inner block-header">
                            <div class="muted pull-left">
                                MODEL BOMS
                            </div>
                            <div class="muted pull-right">
                                <asp:Label ID="lblname" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblbomid" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="block-content collapse in">
                            <div style="padding-left: 35px">
                                <asp:GridView ID="vscopyboms" runat="server" BackColor="White" 
                                    BorderColor="#999999" DataKeyNames="id,modelid"
                                    AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="both"  Width="600px"  
                                    OnRowDeleting="vscopybom_rowdeleting">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" Visible="false"  />
                                        <asp:BoundField DataField="refbomid" HeaderText="REFRENCEBOMID" SortExpression="refbomid" />
                                        <asp:BoundField DataField="MODELID" HeaderText="MODELID" SortExpression="MODELID" />
                                        <asp:BoundField DataField="MODELNAME" HeaderText="MODEL NAME" 
                                            SortExpression="MODEL NAME" ItemStyle-Width="40%" >
                                        <ItemStyle Width="40%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="BOMS RATE" SortExpression="rate" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrate" runat="server" Text='<%# Eval("bomsrate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalrate" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                                    ImageUrl="~/img/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                                <br />
                                <br />
                                <asp:GridView ID="vsBoms" runat="server" CellPadding="3" GridLines="both" DataKeyNames="partid,partname"
                                    AutoGenerateColumns="False" ShowFooter="True" OnRowDeleting="vsboms_rowdeleting"
                                    OnRowDataBound="OnRowDataBound" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="classid" HeaderText="classid" SortExpression="classid"
                                            Visible="false" ItemStyle-Width="100">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="class" HeaderText="ClassName" SortExpression="class" ItemStyle-Width="100">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="catid" HeaderText="catid" SortExpression="catid" Visible="false"
                                            ItemStyle-Width="100">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Category" HeaderText="CategoryName" SortExpression="Category"
                                            ItemStyle-Width="200">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="partid" HeaderText="partid" SortExpression="partid" Visible="false"
                                            ItemStyle-Width="100">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Partname" HeaderText="PartName" SortExpression="Partname"
                                            ItemStyle-Width="200">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="qty" ItemStyle-Width="50">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="unit" ItemStyle-Width="100">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Rate" SortExpression="rate" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalrate" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="sid" HeaderText="sid" SortExpression="sid" Visible="false"
                                            ItemStyle-Width="100">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" Text="Edit" runat="server"
                                                    ImageUrl="~/img/delete.jpg" ToolTip="Delete" Height="20px" Width="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </div>
                            <div class="form-actions">
                                <asp:Button ID="btnsave" class="btn btn-primary" OnClick="btnSave_Click" runat="server"
                                    Text="Save" />
                                <asp:Button ID="btnview" class="btn btn-primary" OnClick="btnView_click" runat="server"
                                    Text="View Report" />
                                <asp:Label ID="Label2" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
</asp:Content>
