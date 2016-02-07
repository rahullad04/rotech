<%@ Page Language="VB" MasterPageFile="~/PurchaseOrder.master" AutoEventWireup="false"
    CodeFile="PartAddEdit.aspx.vb" Inherits="Part_PartAddEdit" Title="Part Add-Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .lable-control
        {
            text-align: right;
            float: left;
            width: 160px;
            padding-top: 5px;
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
    </style>
      <style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
             top:0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
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
                    Part Detail</div>
            </div>
            <div class="block-content collapse in">
                <div class="span12">
                    <!-- BEGIN FORM-->
                    <form action="#" id="form_sample_1" class="form-horizontal">
                    <fieldset>
                       <div  id="alert_container">
            </div>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                         <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                         
                        <div class="control-group">
                            <label class="lable-control">
                                Class <span class="required">*</span></label>
                            <div class="controls">
                                <asp:DropDownList ID="cmbclass" AutoPostBack="true"  OnSelectedIndexChanged="cmbclass_selectedindexchanged" runat="server">
                                </asp:DropDownList>
                               
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                        
                        <div class="control-group">
                            <label class="lable-control">
                                Category <span class="required">*</span></label>
                            <div class="controls">
                                <asp:DropDownList ID="cmbcategory"  DataTextField="classname" AutoPostBack="true" DataValueField="id" runat="server">
                                    
                                </asp:DropDownList>
                               
                            </div>
                        </div>
                            
                            </ContentTemplate>
                            <Triggers >
                            <asp:AsyncPostBackTrigger ControlID="cmbclass" EventName="selectedindexchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                        <div class="control-group">
                            <label class="lable-control">
                                Part Name<span class="required">*</span></label>
                            <div class="controls">
                                <asp:TextBox ID="txtpartname" data-required="1" class="span6 m-wrap" runat="server"></asp:TextBox>
                            </div>
                        </div>
                      
                        </ContentTemplate>
                            <Triggers >
                            <asp:AsyncPostBackTrigger ControlID="btncancle" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        
                        
                    </fieldset>
                    </form>
                    <div class="form-actions">
                            <asp:Button ID="btnSubmit" class="btn btn-primary" OnClick="btnSubmit_Click"   runat="server" Text="Save" />
                            <asp:Button ID="btncancle" class="btn btn-primary"  OnClick="btncancle_click" runat="server" Text="Reset" />
                            <asp:Label ID="lblid" runat="server"></asp:Label>
                        </div>

                    <!-- END FORM-->
                </div>
            </div>
        </div>
        </div>
</asp:Content>
