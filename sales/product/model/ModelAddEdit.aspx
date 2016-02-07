<%@ Page Language="VB" MasterPageFile="~/sales.master" AutoEventWireup="false" CodeFile="ModelAddEdit.aspx.vb" Inherits="Model_ModelAddEdit" title="Model Add-Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" >
.lable-control
{
 text-align: right;
    float: left;
    width: 160px;
    padding-top: 5px;
}
.lable-control .required {
    color: #E02222;
    font-size: 12px;
    padding-left: 2px;
   }
   .controls
   {
   	margin-left :180px;
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
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
 <div class="row-fluid">
                         <!-- block -->
                  <div class="block">
                            <div class="navbar navbar-inner block-header">
                                <div class="muted pull-left">Model Detail</div>
                            </div>
                            <div class="block-content collapse in">
                                <div class="span12">
					<!-- BEGIN FORM-->
					<form action="#" id="form_sample_1" class="form-horizontal">
						<fieldset>
                        <div id="alert_container">
                        </div>
							  							<div class="control-group">
  								<label class="lable-control">Model Name<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtmodelname" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">Product Type<span class="required">*</span></label>
  								<div class="controls">
                                <asp:DropDownList ID="cmbproduct"  AutoPostBack="true" EnableViewState="true" runat="server">
                                 </asp:DropDownList>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">Stack Type<span class="required">*</span></label>
  								<div class="controls">
                                <asp:DropDownList ID="cmbstack"  AutoPostBack="true" EnableViewState="true" runat="server">
                                 </asp:DropDownList>
  							    </div>
  							</div>
                            <div class="control-group">
  								<label class="lable-control">Rate<span class="required">*</span></label>
  								<div class="controls">
                                <asp:TextBox ID="txtRate" data-required="1" class="span6 m-wrap"  runat="server" ></asp:TextBox>
  							    </div>
  							</div>
                          <div class="form-actions">
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save" />
                                <asp:Button ID="btncancle" class="btn" runat="server" Text="Button" />
  								<asp:Label ID="lblid" runat="server"></asp:Label>
  							</div>                                                
                       </fieldset>
					</form>
					<!-- END FORM-->
				</div>
			   </div>
			</div>
                                 
                    
  
 

</asp:Content>

